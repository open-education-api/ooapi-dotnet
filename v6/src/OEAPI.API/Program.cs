using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using OEAPI.API;
using OEAPI.API.Configuration;
using OEAPI.API.Middleware;
using OEAPI.API.OpenApi;
using OEAPI.API.Routing;
using OEAPI.API.Serialization;
using OEAPI.API.Services;
using OEAPI.Core.Interfaces;
using OEAPI.Infrastructure.Data.Context;
using OEAPI.Infrastructure.Data.Seeding;
using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    options.AddSchemaTransformer<ProblemDetailsExampleTransformer>();
    options.AddSchemaTransformer<ValidationAttributeSchemaTransformer>();
    // Resolved from DI, so it can read the same ServiceConfiguration singleton registered by
    // ConfigureServiceMetadata below - registration order here doesn't matter, only resolution
    // order at request time, by which point that singleton is registered regardless.
    options.AddDocumentTransformer<ServiceInfoDocumentTransformer>();
    options.AddDocumentTransformer<NonCanonicalListEndpointDocumentTransformer>();
    options.AddOperationTransformer<TimelineOverridesOperationTransformer>();
});

// Opts into the framework's built-in problem-details infrastructure (IProblemDetailsService) - needed
// for ControllerBase.Problem(...)/ProblemDetailsFactory to backfill ProblemDetails.Type from the
// status code (e.g. https://tools.ietf.org/html/rfc9110#section-15.5.5 for 404) the same way the
// framework's own automatic model-validation error responses already do. Doesn't by itself fix the
// response Content-Type - see ProblemDetailsOutputFormatter, registered below, for why that needs
// its own explicit handling.
builder.Services.AddProblemDetails();

// Configure authentication
ConfigureAuthenticationServices(builder);

// Configure database context based on configuration
ConfigureDatabaseServices(builder);

// Configure GET /'s Service metadata (contact/spec/documentation links, supported consumers/expands)
ConfigureServiceMetadata(builder);

// Add controllers. Route tokens ([controller]/[action]) are kebab-cased globally so controllers can
// stay [Route("[controller]")] instead of each hardcoding a spec-matching literal - see
// KebabCaseParameterTransformer for why this doesn't touch hand-written nested-endpoint route
// segments.
builder.Services.AddControllers(options =>
    {
        options.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseParameterTransformer()));
        // See ProblemDetailsOutputFormatter's own doc comment for why a dedicated, type-specific
        // formatter - inserted ahead of the general-purpose JSON one - is the mechanism that
        // actually works here, unlike several other standard-looking approaches that don't.
        options.OutputFormatters.Insert(0, new ProblemDetailsOutputFormatter(OeapiJsonSerializerOptions.Instance));
    })
    // ext is the spec's free-form extension anchor - it should only ever appear in a response when
    // there's real data to put there, not as an explicit null. See OeapiJsonSerializerOptions for
    // the full reasoning; this applies the same TypeInfoResolver to the ordinary MVC JSON output
    // path (used whenever the fields query param isn't requested).
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.TypeInfoResolver = OeapiJsonSerializerOptions.Instance.TypeInfoResolver;
    });

// Add health checks
builder.Services.AddHealthChecks();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // Interactive API docs UI at /scalar/v1, reading the OpenAPI document MapOpenApi() serves above.
    app.MapScalarApiReference();
}

// Rejects a request for an explicitly-incompatible OEAPI/consumer version before authentication is
// even attempted - version compatibility is a wire-protocol concern prior to identity. See
// VersionNegotiationMiddleware's own doc comment for how a successful response's Content-Type
// reflects the resolved version.
app.UseMiddleware<VersionNegotiationMiddleware>();

// Use authentication middleware
app.UseMiddleware<AuthenticationMiddleware>();

// Apply database migrations on startup (for development)
if (app.Environment.IsDevelopment())
{
    using IServiceScope scope = app.Services.CreateScope();
    OEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<OEAPIDbContext>();
    dbContext.Database.Migrate();
}

// Optionally seed a small demo dataset (Database:SeedDemoData, default false / true in
// Development - see DatabaseConfiguration.SeedDemoData and DemoDataSeeder). Kept independent of
// the Development-only migration step above: a production deployment that applies migrations
// manually (per DATABASE_CONFIGURATION.md) can still opt into demo data purely via config.
DatabaseConfiguration databaseConfiguration = app.Services.GetRequiredService<DatabaseConfiguration>();
if (databaseConfiguration.SeedDemoData)
{
    using IServiceScope scope = app.Services.CreateScope();
    OEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<OEAPIDbContext>();
    await DemoDataSeeder.SeedAsync(dbContext);
}

app.MapHealthChecks("/health");
app.MapControllers();

app.Run();

// Configures database services based on the configuration.
// Supports both SQL Server and PostgreSQL providers.
static void ConfigureDatabaseServices(WebApplicationBuilder builder)
{
    ConfigurationManager configuration = builder.Configuration;

    // Bind database configuration
    DatabaseConfiguration dbConfig = new();
    configuration.GetSection("Database").Bind(dbConfig);

    // Registered so it can be resolved after the app is built (e.g. to check SeedDemoData below)
    // without re-binding configuration a second time.
    builder.Services.AddSingleton(dbConfig);

    // Validate configuration
    if (string.IsNullOrEmpty(dbConfig.Provider))
        throw new InvalidOperationException(
            "Database provider is not configured. Please set Database:Provider in appsettings.json");

    // Get the appropriate connection string based on the selected provider
    string connectionString = dbConfig.Provider switch
    {
        "SqlServer" => dbConfig.ConnectionStrings.SqlServer,
        "PostgreSQL" => dbConfig.ConnectionStrings.PostgreSql,
        _ => throw new InvalidOperationException($"Unsupported database provider: {dbConfig.Provider}")
    };

    // Validate connection string
    if (string.IsNullOrEmpty(connectionString))
        throw new InvalidOperationException(
            $"Connection string for {dbConfig.Provider} is not configured in appsettings.json");

    // Add DbContext based on the selected provider. Registers the provider-specific derived context
    // (SqlServerOEAPIDbContext/PostgreSqlOEAPIDbContext) - not the base OEAPIDbContext directly -
    // because every migration file is scoped to one of those derived types (see their own doc
    // comments). OEAPIDbContext is then redirected to resolve to that same scoped instance, so
    // every controller's existing OEAPIDbContext constructor injection keeps working unchanged,
    // while EF's migration lookup (which matches on the instance's actual runtime type) now finds
    // the right migrations - fixing the startup auto-migrate step, which previously silently found
    // zero migrations for the base type and did nothing against a genuinely fresh database.
    switch (dbConfig.Provider)
    {
        case "SqlServer":
            builder.Services.AddDbContext<SqlServerOEAPIDbContext>(options =>
                options.UseSqlServer(connectionString,
                    sqlServerOptions => sqlServerOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
            builder.Services.AddScoped<OEAPIDbContext>(sp => sp.GetRequiredService<SqlServerOEAPIDbContext>());
            break;

        case "PostgreSQL":
            builder.Services.AddDbContext<PostgreSqlOEAPIDbContext>(options =>
                options.UseNpgsql(connectionString,
                    npgsqlOptions => npgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
            builder.Services.AddScoped<OEAPIDbContext>(sp => sp.GetRequiredService<PostgreSqlOEAPIDbContext>());
            break;

        default:
            throw new InvalidOperationException($"Unsupported database provider: {dbConfig.Provider}");
    }
}

// Binds and registers ServiceConfiguration from the Service configuration section - see that
// type's doc comment for why this is configuration rather than a database table.
static void ConfigureServiceMetadata(WebApplicationBuilder builder)
{
    ServiceConfiguration serviceConfig = new();
    builder.Configuration.GetSection("Service").Bind(serviceConfig);
    builder.Services.AddSingleton(serviceConfig);
}

// Configures authentication services based on the configuration.
// Institutions can implement their own authentication services and register them here.
static void ConfigureAuthenticationServices(WebApplicationBuilder builder)
{
    ConfigurationManager configuration = builder.Configuration;

    // Bind authentication configuration
    AuthenticationConfiguration authConfig = new();
    configuration.GetSection("Authentication").Bind(authConfig);

    // Register authentication configuration for dependency injection
    builder.Services.AddSingleton(authConfig);

    // Register JWT configuration if JWT is enabled
    if (authConfig.Providers.JwtEnabled)
        builder.Services.Configure<JwtConfiguration>(configuration.GetSection("Authentication:Jwt"));

    // Register API key configuration if API key authentication is enabled  
    if (authConfig.Providers.ApiKeyEnabled)
        builder.Services.Configure<ApiKeyConfiguration>(configuration.GetSection("Authentication:ApiKey"));

    // Note: Institutions must register their own implementations of:
    // - IJwtAuthenticationService (if JWT is enabled)
    // - IApiKeyAuthenticationService (if API key authentication is enabled)
    // - IAuthenticationService (for custom authentication)
    //
    // Example:
    // builder.Services.AddScoped<IJwtAuthenticationService, MyJwtAuthenticationService>();
    // builder.Services.AddScoped<IApiKeyAuthenticationService, MyApiKeyAuthenticationService>();

    // ICurrentPersonProvider resolves the "me" in GET /persons/me and the external/me POST
    // endpoints. Unlike the providers above it needs no secrets/external config, so a safe default
    // is registered here. Institutions can replace this registration with their own implementation
    // (e.g. to call a well-known/UserInfo endpoint or auto-provision persons on first login).
    builder.Services
        .AddScoped<ICurrentPersonProvider, DefaultCurrentPersonProvider>();

    // IDocumentStorageProvider resolves document content for GET /documents/{documentId} (a
    // binary file download per spec, not a JSON resource). The default reads from this project's
    // own database; institutions with an existing document store/object storage should replace
    // this registration with their own implementation instead.
    builder.Services
        .AddScoped<IDocumentStorageProvider,
            DatabaseDocumentStorageProvider>();
}

// Exposes the top-level-statements-generated Program class to OEAPI.API.Tests, which needs it as
// the type argument for WebApplicationFactory<Program> in integration tests.
public partial class Program
{
}
