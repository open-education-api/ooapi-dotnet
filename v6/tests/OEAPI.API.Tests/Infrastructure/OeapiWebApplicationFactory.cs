using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace OEAPI.API.Tests.Infrastructure;

/// <summary>
///     A <see cref="WebApplicationFactory{TEntryPoint}" /> that boots the real <c>Program.cs</c> startup
///     path (migrations, optional seeding, routing, etc.) against a Testcontainers-provided database
///     instead of the shared local dev databases.
/// </summary>
/// <remarks>
///     <c>Program.cs</c> reads <c>Database:Provider</c>/<c>Database:ConnectionStrings:{provider}</c>
///     <em>eagerly</em>, in top-level code, before <c>builder.Build()</c> is called.
///     <see cref="WebApplicationFactory{TEntryPoint}" />'s
///     usual override hooks (<see cref="IWebHostBuilder.ConfigureAppConfiguration" />, <c>UseSetting</c>)
///     only take effect at the point the test host intercepts the entry point - which, for the minimal
///     hosting model, is exactly at <c>builder.Build()</c>, i.e. <em>after</em> that eager read already
///     happened. Those hooks are provably too late for this app (confirmed empirically: the final
///     resolved <c>IConfiguration</c> shows the override, but the connection string closure captured by
///     <c>AddDbContext</c> inside <c>ConfigureDatabaseServices</c> does not). Process environment
///     variables are read by <c>WebApplication.CreateBuilder</c> itself, before any of Program.cs's own
///     code runs, so they're the only override mechanism early enough to affect this eager read.
///     Consequently, this factory mutates process-wide environment variables - safe only because
///     <c>OEAPI.API.Tests</c> disables test parallelization (see <c>AssemblyInfo.cs</c>), so no other
///     test can observe a different factory's variables mid-flight.
/// </remarks>
public sealed class OeapiWebApplicationFactory : WebApplicationFactory<Program>
{
    public OeapiWebApplicationFactory(string provider, string connectionString)
    {
        Environment.SetEnvironmentVariable("Database__Provider", provider);
        Environment.SetEnvironmentVariable($"Database__ConnectionStrings__{provider}", connectionString);
        // Tests seed their own data explicitly; the demo dataset would only add noise.
        Environment.SetEnvironmentVariable("Database__SeedDemoData", "false");
    }

    /// <inheritdoc />
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Development is required for Program.cs's auto-migrate-on-startup step to run at all.
        builder.UseEnvironment("Development");
    }
}
