using System.Net;
using System.Text.Json;
using OEAPI.API.Tests.Infrastructure;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Confirms the self-generated OpenAPI document (<c>GET /openapi/v1.json</c>) correctly identifies
///     which OEAPI version this deployment implements and links back to the public specification and
///     documentation, via <c>ServiceInfoDocumentTransformer</c> - without it, the document falls back to
///     <c>AddOpenApi()</c>'s meaningless defaults (title <c>"OEAPI.API | v1"</c>, version
///     <c>"1.0.0"</c>, no <c>externalDocs</c>).
/// </summary>
[Collection(SqlServerCollection.Name)]
public class OpenApiDocumentInfoConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Fact]
    public async Task GetOpenApiDocument_InfoBlock_DescribesTheOeapiVersionImplemented()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();

        HttpResponseMessage response = await client.GetAsync("/openapi/v1.json");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        using JsonDocument document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        JsonElement info = document.RootElement.GetProperty("info");

        Assert.Equal("6.0", info.GetProperty("version").GetString());
        Assert.Contains("OOAPI", info.GetProperty("title").GetString());
        Assert.Contains("6.0", info.GetProperty("description").GetString());
    }

    [Fact]
    public async Task GetOpenApiDocument_License_MatchesTheSpecsOwnEupl12Licence()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();

        HttpResponseMessage response = await client.GetAsync("/openapi/v1.json");

        using JsonDocument document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        JsonElement license = document.RootElement.GetProperty("info").GetProperty("license");

        Assert.Equal("EUPL-1.2", license.GetProperty("name").GetString());
        Assert.Equal(
            "https://github.com/open-education-api/specification/blob/release/6.0/LICENSE.md",
            license.GetProperty("url").GetString());
    }

    [Fact]
    public async Task GetOpenApiDocument_ExternalDocs_LinksToThePublicOeapiDocumentation()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();

        HttpResponseMessage response = await client.GetAsync("/openapi/v1.json");

        using JsonDocument document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        JsonElement externalDocs = document.RootElement.GetProperty("externalDocs");

        Assert.Equal("https://oeapi.eu/", externalDocs.GetProperty("url").GetString());
    }
}
