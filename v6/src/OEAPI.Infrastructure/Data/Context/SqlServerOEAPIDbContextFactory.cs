using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace OEAPI.Infrastructure.Data.Context;

/// <summary>
///     Design-time factory for <see cref="SqlServerOEAPIDbContext" />, used by
///     <c>dotnet ef migrations add ... --context SqlServerOEAPIDbContext</c> to generate/maintain the
///     SQL Server migration history independently of PostgreSQL's.
/// </summary>
public class SqlServerOEAPIDbContextFactory : IDesignTimeDbContextFactory<SqlServerOEAPIDbContext>
{
    /// <inheritdoc />
    public SqlServerOEAPIDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<SqlServerOEAPIDbContext> optionsBuilder = new();

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

        string connectionString = configuration["Database:ConnectionStrings:SqlServer"]
                                  ??
                                  "Server=(localdb)\\mssqllocaldb;Database=OEAPI_Dev;Trusted_Connection=True;MultipleActiveResultSets=true";

        optionsBuilder.UseSqlServer(connectionString);

        return new SqlServerOEAPIDbContext(optionsBuilder.Options);
    }
}
