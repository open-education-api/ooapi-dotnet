using System.Net;
using System.Net.Http.Json;
using OEAPI.API.Tests.Infrastructure;
using OEAPI.Core.Models.ApiModels;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Regression test for <c>GenericEntityController.GetAll</c>'s pagination offset calculation: a
///     large enough <c>pageNumber</c> would overflow <c>(validatedPage - 1) * validatedPageSize</c> as
///     a 32-bit int into a negative number, which SQL Server rejects as an invalid <c>OFFSET</c>,
///     crashing with an unhandled <c>500</c>. The offset is computed as <c>long</c> instead, so this
///     always returns a well-formed response.
/// </summary>
[Collection(SqlServerCollection.Name)]
public class PaginationConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Theory]
    [InlineData("/learning-outcomes")]
    [InlineData("/courses")]
    [InlineData("/organisations")]
    public async Task GetAll_PageNumberAtIntMaxValue_DoesNotCrash(string path)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();

        HttpResponseMessage response = await client.GetAsync($"{path}?pageNumber=2147483647&pageSize=10");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    /// <summary>
    ///     The fix above only touched <c>GenericEntityController.GetAll</c> - every entity-specific
    ///     nested-collection <c>GET</c> action (e.g. <c>GET /organisations/{id}/courses</c>) had its own
    ///     separately hand-rolled copy of the exact same overflow-prone arithmetic, confirmed live to
    ///     500 the same way. All of them now go through the shared
    ///     <c>GenericEntityController.ComputePageOffset</c> instead.
    /// </summary>
    [Theory]
    [InlineData("courses")]
    [InlineData("groups")]
    [InlineData("programme-offerings")]
    public async Task GetNestedCollection_PageNumberAtIntMaxValue_DoesNotCrash(string nestedPath)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string organisationId = Guid.NewGuid().ToString();
        Organisation organisation = new()
        {
            OrganisationId = organisationId,
            PrimaryCode = new IdentifierEntry { CodeType = "organisation_id", Code = $"ORG-{organisationId[..8]}" },
            OrganisationType = "institute",
            Name = [new LanguageTypedString { Language = "en", Value = "Pagination Test Org" }]
        };
        await client.PutAsJsonAsync($"/organisations/{organisationId}", organisation);

        HttpResponseMessage response =
            await client.GetAsync($"/organisations/{organisationId}/{nestedPath}?pageNumber=2147483647&pageSize=10");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
