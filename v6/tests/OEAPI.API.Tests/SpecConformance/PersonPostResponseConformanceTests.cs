using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using OEAPI.API.Tests.Infrastructure;
using OEAPI.Core.Models.ApiModels;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Confirms <c>POST /persons</c> responds with the spec's <c>PersonId</c> + <c>PostResponse</c>
///     composed shape (<see cref="PostResponse" />), not the full <see cref="Person" /> resource.
/// </summary>
[Collection(SqlServerCollection.Name)]
public class PersonPostResponseConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Fact]
    public async Task CreatePerson_ReturnsPersonIdAndMessageNotFullPersonObject()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();

        HttpResponseMessage response = await client.PostAsJsonAsync("/persons", new PersonProperties
        {
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = "POST-RESPONSE-TEST" },
            Surname = "PostResponseTest"
        });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        JsonObject json = JsonNode.Parse(await response.Content.ReadAsStringAsync())!.AsObject();

        Assert.True(json.ContainsKey("personId"));
        Assert.False(string.IsNullOrEmpty(json["personId"]!.GetValue<string>()));

        Assert.True(json.ContainsKey("message"));
        Assert.NotEmpty(json["message"]!.AsArray());

        // The spec's PersonId + PostResponse shape has no primaryCode/givenName/etc. - if the full
        // Person resource were still being returned, these would be present.
        Assert.False(json.ContainsKey("primaryCode"));
        Assert.False(json.ContainsKey("givenName"));
    }

    [Fact]
    public async Task CreatePerson_NoPrimaryCode_ReturnsCreatedWithoutValidationError()
    {
        // Regression test for PersonsController.CreatePerson's own inline entity-construction path
        // (distinct code from PutPerson_NoPrimaryCode_SucceedsWithoutValidationError's
        // PersonMappingExtensions.ToEntity path) - primaryCode is optional per spec for
        // PersonProperties too.
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();

        HttpResponseMessage response = await client.PostAsJsonAsync("/persons", new PersonProperties
        {
            GivenName = "No",
            Surname = "PrimaryCode"
        });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task CreatePerson_NoNameFieldAtAll_ReturnsBadRequest()
    {
        // Regression test: PersonProperties' spec schema requires at least one of surname/givenName/
        // preferredName via its own anyOf - without IValidatableObject enforcing this, a client could
        // create a person whose own GET response then fails that same schema.
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();

        HttpResponseMessage response = await client.PostAsJsonAsync("/persons", new PersonProperties
        {
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = "NO-NAME-TEST" }
        });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreatePerson_OnlyPreferredNameProvided_ReturnsCreated()
    {
        // Any one of the three name fields alone is enough to satisfy the anyOf - not just surname/
        // givenName.
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();

        HttpResponseMessage response = await client.PostAsJsonAsync("/persons", new PersonProperties
        {
            PreferredName = "OnlyPreferredName"
        });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}
