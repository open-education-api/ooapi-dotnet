using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using OEAPI.Infrastructure.Data.Context;
using Testcontainers.PostgreSql;
using Xunit;

namespace OEAPI.API.Tests.Infrastructure;

/// <summary>
///     Starts a real, disposable PostgreSQL container for the lifetime of the test collection that
///     references it, applies the real migrations via <see cref="PostgreSqlOEAPIDbContext" /> (the same
///     derived context type <c>Program.cs</c> uses at runtime), and hands out
///     <see cref="OeapiWebApplicationFactory" /> instances wired to it.
/// </summary>
public sealed class PostgreSqlContainerFixture : IAsyncLifetime
{
    private PostgreSqlContainer _container = null!;

    /// <summary>Gets the connection string of the running container.</summary>
    public string ConnectionString { get; private set; } = string.Empty;

    /// <inheritdoc />
    public async Task InitializeAsync()
    {
        _container = new PostgreSqlBuilder("postgres:15.1").Build();
        await _container.StartAsync();
        ConnectionString = _container.GetConnectionString();

        DbContextOptions<PostgreSqlOEAPIDbContext> options = new DbContextOptionsBuilder<PostgreSqlOEAPIDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;
        await using PostgreSqlOEAPIDbContext dbContext = new(options);
        await dbContext.Database.MigrateAsync();
    }

    /// <inheritdoc />
    public async Task DisposeAsync()
    {
        await _container.DisposeAsync();
    }

    /// <summary>Creates a <see cref="WebApplicationFactory{Program}" /> targeting this container.</summary>
    public OeapiWebApplicationFactory CreateFactory()
    {
        return new OeapiWebApplicationFactory("PostgreSQL", ConnectionString);
    }
}

[CollectionDefinition(Name)]
public sealed class PostgreSqlCollection : ICollectionFixture<PostgreSqlContainerFixture>
{
    public const string Name = "PostgreSQL container";
}
