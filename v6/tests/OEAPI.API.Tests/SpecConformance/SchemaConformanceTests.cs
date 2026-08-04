using System.Net.Http.Json;
using System.Text.Json.Nodes;
using Json.Schema;
using OEAPI.API.Tests.Infrastructure;
using OEAPI.Core.Models.ApiModels;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Proof of concept for automated JSON Schema spec-conformance validation: validates a real
///     response body against the spec's own JSON Schema for that operation, not just status
///     codes/hand-picked field checks.
/// </summary>
[Collection(SqlServerCollection.Name)]
public class SchemaConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Fact(Skip = "Paused pending spec maintainer clarification on whether optional Identifier-ref " +
                 "fields (parentId, rootId, etc.) should omit-when-absent, since the spec doesn't declare " +
                 "null as valid for them - unlike every sibling field for the same relationship (the array " +
                 "and expanded-object versions both correctly allow null). Also blocked on a secondary, " +
                 "separate SpecSchemaValidator/JsonSchema.Net investigation (nullable expanded-object fields " +
                 "like `parent` are incorrectly flagged invalid).")]
    public async Task GetOrganisations_ResponseBody_MatchesSpecSchema()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        await client.PutAsJsonAsync($"/organisations/{id}", new Organisation
        {
            OrganisationId = id,
            PrimaryCode = new IdentifierEntry { CodeType = "organisation_id", Code = $"SCHEMA-{id[..8]}" },
            OrganisationType = "institute",
            Name = [new LanguageTypedString { Language = "en", Value = "Schema Test Org" }]
        });

        HttpResponseMessage response = await client.GetAsync("/organisations");
        JsonNode? body = JsonNode.Parse(await response.Content.ReadAsStringAsync());

        SpecOperation operation = SpecManifest.Operations.Single(o => o.Method == "GET" && o.Path == "/organisations");
        EvaluationResults results = SpecSchemaValidator.Validate(operation, body);

        Assert.True(results.IsValid, SpecSchemaValidator.DescribeErrors(results));
    }
}
