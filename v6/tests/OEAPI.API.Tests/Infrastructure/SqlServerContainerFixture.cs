using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using OEAPI.Infrastructure.Data.Context;
using Testcontainers.MsSql;
using Xunit;

namespace OEAPI.API.Tests.Infrastructure;

/// <summary>
///     Starts a real, disposable SQL Server container for the lifetime of the test collection that
///     references it, applies the real migrations via <see cref="SqlServerOEAPIDbContext" /> (the same
///     derived context type <c>Program.cs</c> uses at runtime), and hands out
///     <see cref="OeapiWebApplicationFactory" /> instances wired to it.
/// </summary>
public sealed class SqlServerContainerFixture : IAsyncLifetime
{
    private MsSqlContainer _container = null!;

    /// <summary>Gets the connection string of the running container.</summary>
    public string ConnectionString { get; private set; } = string.Empty;

    /// <inheritdoc />
    public async Task InitializeAsync()
    {
        _container = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-CU14-ubuntu-22.04").Build();
        await _container.StartAsync();
        ConnectionString = _container.GetConnectionString();

        DbContextOptions<SqlServerOEAPIDbContext> options = new DbContextOptionsBuilder<SqlServerOEAPIDbContext>()
            .UseSqlServer(ConnectionString)
            .Options;
        await using SqlServerOEAPIDbContext dbContext = new(options);
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
        return new OeapiWebApplicationFactory("SqlServer", ConnectionString);
    }
}

[CollectionDefinition(Name)]
public sealed class SqlServerCollection : ICollectionFixture<SqlServerContainerFixture>
{
    public const string Name = "SQL Server container";
}
