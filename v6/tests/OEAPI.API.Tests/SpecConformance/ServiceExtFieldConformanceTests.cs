using System.Linq;
using System.Text.Json.Nodes;
using OEAPI.API.Tests.Infrastructure;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Guards a gap found and fixed while auditing `extend-demo-data-seeder-coverage-phase-2` section
///     12: `Service.ext` was declared on the spec's `Service.yaml` but `ServiceController` never set it
///     at all, and `ServiceConfiguration` (the deployment-level config class backing this
///     config-driven, non-database-backed resource) had no property for it either. Fixed by adding an
///     `ExtJson` string property to `ServiceConfiguration` (matching every other free-form `Ext`
///     field's own `*Json`-string-plus-deserialize-on-read convention) and wiring it into
///     `ServiceController.CreateServiceMetadata()`. Only ever caught by live `curl` at the time - this
///     locks the same assertion into a `dotnet test` run instead, using the real
///     `Service:ExtJson` value already configured in `appsettings.json`.
/// </summary>
[Collection(SqlServerCollection.Name)]
public class ServiceExtFieldConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Fact]
    public async Task GetService_ReturnsConfiguredExtValue()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();

        HttpResponseMessage response = await client.GetAsync("/");
        response.EnsureSuccessStatusCode();
        JsonObject service = JsonNode.Parse(await response.Content.ReadAsStringAsync())!.AsObject();

        JsonNode? ext = service["ext"];
        Assert.NotNull(ext);
        Assert.Equal(
            "Example institution-specific field, for local conformance testing.",
            ext!["x-demo-note"]!.GetValue<string>());
    }

    /// <summary>
    ///     Guards a `docs/TODO-LIST.md` finding: `appsettings.json`'s `Service:SupportedExpands` didn't
    ///     list `learning_outcomes` for `/courses`, even though `CoursesController.ApplyExpandAsync`
    ///     has a real, working `learningoutcomes` case - a pure Service-metadata discoverability gap,
    ///     not a functional one. Found to affect all 4 resources with a real `learningOutcomes` expand
    ///     (`/courses`, `/learning-components`, `/programmes`, `/test-components`), not just `/courses`
    ///     as originally scoped, once checked directly against every controller that implements the
    ///     case.
    /// </summary>
    [Theory]
    [InlineData("/courses")]
    [InlineData("/learning-components")]
    [InlineData("/programmes")]
    [InlineData("/test-components")]
    public async Task GetService_ListsLearningOutcomesAsExpandableFor(string path)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();

        HttpResponseMessage response = await client.GetAsync("/");
        response.EnsureSuccessStatusCode();
        JsonObject service = JsonNode.Parse(await response.Content.ReadAsStringAsync())!.AsObject();

        JsonArray supportedExpands = service["supportedExpands"]!.AsArray();
        JsonObject? entry = supportedExpands
            .Select(e => e!.AsObject())
            .SingleOrDefault(e => e["path"]!.GetValue<string>() == path);

        Assert.NotNull(entry);
        JsonArray expandableObjects = entry!["expandableObjects"]!.AsArray();
        Assert.Contains(expandableObjects, o => o!.GetValue<string>() == "learning_outcomes");
    }

    /// <summary>
    ///     Guards that <c>GroupsController</c>'s working <c>academicSession</c> expand case and
    ///     <c>OrganisationsController</c>'s working <c>root</c> expand case are both declared in
    ///     `appsettings.json`'s `Service:SupportedExpands`.
    /// </summary>
    [Theory]
    [InlineData("/groups", "academic_session")]
    [InlineData("/organisations", "root")]
    public async Task GetService_ListsExpandableObjectFor(string path, string expandableObject)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();

        HttpResponseMessage response = await client.GetAsync("/");
        response.EnsureSuccessStatusCode();
        JsonObject service = JsonNode.Parse(await response.Content.ReadAsStringAsync())!.AsObject();

        JsonArray supportedExpands = service["supportedExpands"]!.AsArray();
        JsonObject? entry = supportedExpands
            .Select(e => e!.AsObject())
            .SingleOrDefault(e => e["path"]!.GetValue<string>() == path);

        Assert.NotNull(entry);
        JsonArray expandableObjects = entry!["expandableObjects"]!.AsArray();
        Assert.Contains(expandableObjects, o => o!.GetValue<string>() == expandableObject);
    }
}
