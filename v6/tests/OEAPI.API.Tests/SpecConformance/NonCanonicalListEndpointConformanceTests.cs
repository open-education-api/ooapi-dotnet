using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using OEAPI.API.Configuration;
using OEAPI.API.Tests.Infrastructure;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Covers the 8 top-level "list all" endpoints with no canonical-spec counterpart
///     (<see cref="OEAPI.API.Controllers.NonCanonicalTopLevelListRoutes" />) - <c>404</c> by default,
///     re-enabled per deployment via <see cref="ServiceConfiguration.ExposeNonCanonicalListEndpoints" />.
///     A canonically top-level resource's own list (e.g. <c>/organisations</c>) must stay unaffected
///     either way - covered directly here so a future change to the check in
///     <c>GenericEntityController.GetAll</c> can't accidentally widen its scope.
/// </summary>
[Collection(SqlServerCollection.Name)]
public class NonCanonicalListEndpointConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    public static TheoryData<string> NonCanonicalRoutes()
    {
        TheoryData<string> data =
        [
            "course-offerings",
            "course-offering-associations",
            "learning-component-offerings",
            "learning-component-offering-associations",
            "programme-offerings",
            "programme-offering-associations",
            "test-component-offerings",
            "test-component-offering-associations"
        ];
        return data;
    }

    [Theory]
    [MemberData(nameof(NonCanonicalRoutes))]
    public async Task GetNonCanonicalTopLevelList_Default_ReturnsNotFound(string routeName)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();

        HttpResponseMessage response = await client.GetAsync($"/{routeName}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetOrganisations_Default_StillSucceeds()
    {
        // Sanity check that the 404 above is scoped to the 8 non-canonical routes only - a
        // canonically top-level resource sharing the same GenericEntityController base must never
        // be affected by this toggle, on or off.
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();

        HttpResponseMessage response = await client.GetAsync("/organisations");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    private WebApplicationFactory<Program> CreateFactoryWithNonCanonicalListsEnabled()
    {
        return _fixture.CreateFactory().WithWebHostBuilder(builder =>
            builder.ConfigureTestServices(services =>
                services.AddSingleton(new ServiceConfiguration { ExposeNonCanonicalListEndpoints = true })));
    }

    [Fact]
    public async Task GetCourseOfferings_WhenEnabled_ReturnsOkWithPagedBody()
    {
        using WebApplicationFactory<Program> factory = CreateFactoryWithNonCanonicalListsEnabled();
        using HttpClient client = factory.CreateClient();

        HttpResponseMessage response = await client.GetAsync("/course-offerings");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        string body = await response.Content.ReadAsStringAsync();
        Assert.Contains("\"items\"", body);
    }

    [Fact]
    public async Task OpenApiDocument_Default_OmitsNonCanonicalListPaths()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();

        JsonDocumentPaths document = await client.GetFromJsonAsync<JsonDocumentPaths>("/openapi/v1.json")
                                      ?? throw new InvalidOperationException("No OpenAPI document returned.");

        Assert.DoesNotContain("/course-offerings", document.Paths.Keys);
        Assert.DoesNotContain("/course-offering-associations", document.Paths.Keys);
    }

    [Fact]
    public async Task OpenApiDocument_WhenEnabled_IncludesNonCanonicalListPaths()
    {
        using WebApplicationFactory<Program> factory = CreateFactoryWithNonCanonicalListsEnabled();
        using HttpClient client = factory.CreateClient();

        JsonDocumentPaths document = await client.GetFromJsonAsync<JsonDocumentPaths>("/openapi/v1.json")
                                      ?? throw new InvalidOperationException("No OpenAPI document returned.");

        Assert.Contains("/course-offerings", document.Paths.Keys);
    }

    /// <summary>
    ///     Only the one field this test file needs from the generated OpenAPI document - avoids
    ///     depending on the full <c>Microsoft.OpenApi</c> object model just to read path keys.
    /// </summary>
    private sealed class JsonDocumentPaths
    {
        public Dictionary<string, object> Paths { get; set; } = [];
    }
}
