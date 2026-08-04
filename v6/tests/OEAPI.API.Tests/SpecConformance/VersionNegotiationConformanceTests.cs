using System.Net;
using System.Net.Http.Headers;
using OEAPI.API.Tests.Infrastructure;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Confirms the spec's closed, <c>Accept</c>-header version-negotiation behaviour (see
///     <c>VersionNegotiationMiddleware</c>) end to end, over real HTTP - both the rejection path and
///     the <c>Content-Type</c>-echoing success path.
/// </summary>
[Collection(SqlServerCollection.Name)]
public class VersionNegotiationConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Fact]
    public async Task GetOrganisations_NoAcceptHeader_ReturnsOk()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();

        HttpResponseMessage response = await client.GetAsync("/organisations");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetOrganisations_CompatibleOeapiVersionRequested_ReturnsOk()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            MediaTypeWithQualityHeaderValue.Parse("application/vnd.oeapi+json;version=6.0"));

        HttpResponseMessage response = await client.GetAsync("/organisations");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetOrganisations_CompatibleMinorFallbackRequested_ReturnsOk()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            MediaTypeWithQualityHeaderValue.Parse("application/vnd.oeapi+json;version=6.7"));

        HttpResponseMessage response = await client.GetAsync("/organisations");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetOrganisations_IncompatibleMajorVersionRequested_Returns406WithProblemVersionNotAcceptable()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            MediaTypeWithQualityHeaderValue.Parse("application/vnd.oeapi+json;version=7.0"));

        HttpResponseMessage response = await client.GetAsync("/organisations");

        Assert.Equal(HttpStatusCode.NotAcceptable, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);
        string body = await response.Content.ReadAsStringAsync();
        Assert.Contains("\"requestedVersion\":\"7.0\"", body);
        Assert.Contains("\"supportedVersions\":[\"6.0\"]", body);
    }

    [Fact]
    public async Task GetOrganisations_UnknownConsumerRequested_Returns406WithConsumerPopulated()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            MediaTypeWithQualityHeaderValue.Parse("application/vnd.oeapi+json;version=6.0;consumer=nonexistent"));

        HttpResponseMessage response = await client.GetAsync("/organisations");

        Assert.Equal(HttpStatusCode.NotAcceptable, response.StatusCode);
        string body = await response.Content.ReadAsStringAsync();
        Assert.Contains("\"consumerKey\":\"nonexistent\"", body);
    }

    [Fact]
    public async Task GetOrganisations_NoAcceptHeader_ContentTypeStaysPlainJson()
    {
        // Lenient default: no version was requested, so nothing is echoed either - a generic client
        // (including this codebase's own conformance tooling) keeps getting plain application/json,
        // matching what this deployment's self-generated OpenAPI document declares. Echoing
        // unconditionally here previously caused real Schemathesis "undocumented Content-Type"
        // failures against that document - the same class of problem as upstream issue #681.
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();

        HttpResponseMessage response = await client.GetAsync("/organisations");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
    }

    [Fact]
    public async Task GetOrganisations_ExplicitCompatibleVersion_ContentTypeEchoesRequestedVersion()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            MediaTypeWithQualityHeaderValue.Parse("application/vnd.oeapi+json;version=6.0"));

        HttpResponseMessage response = await client.GetAsync("/organisations");

        Assert.Equal("application/vnd.oeapi+json", response.Content.Headers.ContentType?.MediaType);
        Assert.Contains(response.Content.Headers.ContentType!.Parameters,
            p => p.Name == "version" && p.Value == "6.0");
    }

    [Fact]
    public async Task GetOrganisations_WithConsumer_ContentTypeIncludesConsumerAndConsumerVersion()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            MediaTypeWithQualityHeaderValue.Parse("application/vnd.oeapi+json;version=6.0;consumer=nl-edusites"));

        HttpResponseMessage response = await client.GetAsync("/organisations");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        MediaTypeHeaderValue contentType = response.Content.Headers.ContentType!;
        Assert.Contains(contentType.Parameters, p => p.Name == "consumer" && p.Value == "nl-edusites");
        Assert.Contains(contentType.Parameters, p => p.Name == "consumer-version" && p.Value == "1.0");
    }

    [Fact]
    public async Task GetOrganisations_FieldsQueryParameter_ContentTypeStillEchoesVersion()
    {
        // Exercises PagedResponseWithFieldSelection's raw JsonNode-pruning response path (fields=
        // present), a different code path from the plain-object serialization the other tests here
        // exercise (fields= absent) - both must get the same Content-Type treatment.
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            MediaTypeWithQualityHeaderValue.Parse("application/vnd.oeapi+json;version=6.0"));

        HttpResponseMessage response = await client.GetAsync("/organisations?fields=(organisationId)");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/vnd.oeapi+json", response.Content.Headers.ContentType?.MediaType);
        Assert.Contains(response.Content.Headers.ContentType!.Parameters,
            p => p.Name == "version" && p.Value == "6.0");
    }

    [Fact]
    public async Task GetNonexistentOrganisation_CompatibleVersionRequested_ErrorResponseNotRelabelled()
    {
        // A downstream error response (404, via ProblemDetailsOutputFormatter) keeps
        // application/problem+json regardless of a successfully-negotiated version - the version-echo
        // behaviour only ever applies to plain application/json (successful) responses.
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            MediaTypeWithQualityHeaderValue.Parse("application/vnd.oeapi+json;version=6.0"));

        HttpResponseMessage response = await client.GetAsync($"/organisations/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);
    }

    // "Runs before AuthenticationMiddleware" is a Program.cs registration-order fact (verified by
    // reading it directly), not covered here: OeapiWebApplicationFactory's own doc comment explains
    // why the usual WebApplicationFactory config-override hooks don't reliably affect settings
    // Program.cs reads eagerly before builder.Build() - Authentication:RequireAuthentication is one
    // of them - so toggling it per-test would need the same env-var workaround already used for
    // Database:Provider, not attempted here for a single ordering-confirmation test.
}
