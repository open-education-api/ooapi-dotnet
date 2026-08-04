using System.Net;
using Xunit;

namespace OEAPI.API.Tests.Infrastructure;

/// <summary>
///     Proves the Testcontainers + <see cref="OeapiWebApplicationFactory" /> pipeline actually works end
///     to end: a container starts, the real migrations apply via the real derived <c>DbContext</c>
///     type, and the real <c>Program.cs</c> startup path serves a request against it. Deliberately not
///     spec-conformance coverage - that's what the other tests in <c>SpecConformance/</c> are for.
/// </summary>
[Collection(SqlServerCollection.Name)]
public class SqlServerContainerInfrastructureTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Fact]
    public async Task GetOrganisations_FreshlyMigratedContainer_ReturnsEmptyPagedResult()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();

        HttpResponseMessage response = await client.GetAsync("/organisations");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        string body = await response.Content.ReadAsStringAsync();
        Assert.Contains("\"items\":[]", body);
    }
}

[Collection(PostgreSqlCollection.Name)]
public class PostgreSqlContainerInfrastructureTests(PostgreSqlContainerFixture fixture)
{
    private readonly PostgreSqlContainerFixture _fixture = fixture;

    [Fact]
    public async Task GetOrganisations_FreshlyMigratedContainer_ReturnsEmptyPagedResult()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();

        HttpResponseMessage response = await client.GetAsync("/organisations");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        string body = await response.Content.ReadAsStringAsync();
        Assert.Contains("\"items\":[]", body);
    }
}
