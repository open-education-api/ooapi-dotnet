namespace OEAPI.API.Configuration;

/// <summary>
///     Configuration for database providers.
/// </summary>
public class DatabaseConfiguration
{
    /// <summary>
    ///     Gets or sets the database provider to use (SqlServer, PostgreSQL).
    /// </summary>
    public string Provider { get; set; } = "SqlServer";

    /// <summary>
    ///     Gets or sets the connection strings for different database providers.
    /// </summary>
    public ConnectionStrings ConnectionStrings { get; set; } = new();

    /// <summary>
    ///     Gets or sets whether to seed a small demo dataset on startup (see
    ///     <see cref="OEAPI.Infrastructure.Data.Seeding.DemoDataSeeder" />). Idempotent - only seeds if
    ///     the database has no data yet. Defaults to <see langword="false" />; <c>appsettings.Development.json</c>
    ///     overrides this to <see langword="true" /> for a convenient local dev-loop. Institutions
    ///     deploying for real should leave this <see langword="false" /> (the default) unless they
    ///     specifically want sample data to try the API out with.
    /// </summary>
    public bool SeedDemoData { get; set; }
}

/// <summary>
///     Connection strings for different database providers.
/// </summary>
public class ConnectionStrings
{
    /// <summary>
    ///     Gets or sets the SQL Server connection string.
    /// </summary>
    public string SqlServer { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the PostgreSQL connection string.
    /// </summary>
    public string PostgreSql { get; set; } = string.Empty;
}
