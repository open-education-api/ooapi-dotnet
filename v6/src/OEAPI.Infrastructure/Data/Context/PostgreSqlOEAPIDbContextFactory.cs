using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace OEAPI.Infrastructure.Data.Context;

/// <summary>
///     Design-time factory for <see cref="PostgreSqlOEAPIDbContext" />, used by
///     <c>dotnet ef migrations add ... --context PostgreSqlOEAPIDbContext</c> to generate/maintain the
///     PostgreSQL migration history independently of SQL Server's.
/// </summary>
public class PostgreSqlOEAPIDbContextFactory : IDesignTimeDbContextFactory<PostgreSqlOEAPIDbContext>
{
    /// <inheritdoc />
    public PostgreSqlOEAPIDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<PostgreSqlOEAPIDbContext> optionsBuilder = new();

        // dotnet ef's working directory varies by how it's invoked (--project vs --startup-project),
        // so try the current directory first, then fall back to the API project's appsettings.json
        // (the one actually read by Program.cs at runtime) via a path relative to this project.
        string apiAppSettingsPath =
            Path.Combine(Directory.GetCurrentDirectory(), "..", "OEAPI.API", "appsettings.json");
        string basePath = File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"))
            ? Directory.GetCurrentDirectory()
            : Path.GetDirectoryName(Path.GetFullPath(apiAppSettingsPath)) ?? Directory.GetCurrentDirectory();

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", true, true)
            .Build();

        string connectionString = configuration["Database:ConnectionStrings:PostgreSQL"]
                                  ?? "Host=localhost;Port=5432;Database=OEAPI_Dev;Username=postgres;Password=postgres";

        optionsBuilder.UseNpgsql(connectionString);

        return new PostgreSqlOEAPIDbContext(optionsBuilder.Options);
    }
}
