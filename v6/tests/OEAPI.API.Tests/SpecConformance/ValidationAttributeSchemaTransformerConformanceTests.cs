using System.Net;
using System.Text.Json;
using OEAPI.API.Tests.Infrastructure;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Confirms the self-generated OpenAPI document (<c>GET /openapi/v1.json</c>) reflects the two
///     custom validation attributes the built-in generator can't discover on its own -
///     <c>[RegexPattern]</c> and <c>[ExtensibleEnum]</c> - via <c>ValidationAttributeSchemaTransformer</c>.
///     Without it, both attributes are enforced at request time but leave no trace in the generated
///     schema, since ASP.NET Core's OpenAPI generator only recognises a fixed allowlist of
///     <c>System.ComponentModel.DataAnnotations</c> types.
/// </summary>
[Collection(SqlServerCollection.Name)]
public class ValidationAttributeSchemaTransformerConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    private async Task<JsonElement> GetSchemasAsync()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();

        HttpResponseMessage response = await client.GetAsync("/openapi/v1.json");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        using JsonDocument document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return document.RootElement
            .GetProperty("components")
            .GetProperty("schemas")
            .Clone();
    }

    [Fact]
    public async Task RegexPattern_OnAPlainStringProperty_EmitsPatternOnThePropertysOwnSchema()
    {
        JsonElement schemas = await GetSchemasAsync();

        JsonElement amount = schemas.GetProperty("Cost").GetProperty("properties").GetProperty("amount");

        Assert.Equal(@"^\d+(?:\.\d+)?$", amount.GetProperty("pattern").GetString());
    }

    [Fact]
    public async Task ExtensibleEnum_OnAPlainStringProperty_EmitsAnyOfWithKnownValuesAndXPrefixEscape()
    {
        JsonElement schemas = await GetSchemasAsync();

        JsonElement state = schemas.GetProperty("CourseOffering").GetProperty("properties")
            .GetProperty("state");
        JsonElement anyOf = state.GetProperty("anyOf");

        string[] knownValues = [.. anyOf[0].GetProperty("enum").EnumerateArray().Select(v => v.GetString()!)];
        Assert.Contains("active", knownValues);
        Assert.Contains("inactive", knownValues);
        Assert.Equal("^x-", anyOf[1].GetProperty("pattern").GetString());
    }

    [Fact]
    public async Task ExtensibleEnum_OnAStringArrayProperty_EmitsAnyOfOnItemsNotTheArraySchema()
    {
        JsonElement schemas = await GetSchemasAsync();

        JsonElement modesOfDelivery = schemas.GetProperty("Course").GetProperty("properties")
            .GetProperty("modesOfDelivery");

        Assert.False(modesOfDelivery.TryGetProperty("anyOf", out _));

        JsonElement items = modesOfDelivery.GetProperty("items");
        JsonElement anyOf = items.GetProperty("anyOf");
        string[] knownValues = [.. anyOf[0].GetProperty("enum").EnumerateArray().Select(v => v.GetString()!)];
        Assert.Contains("online", knownValues);
        Assert.Equal("^x-", anyOf[1].GetProperty("pattern").GetString());
    }

    [Fact]
    public async Task RegexPattern_OnAStringArrayProperty_EmitsPatternOnItemsNotTheArraySchema()
    {
        JsonElement schemas = await GetSchemasAsync();

        JsonElement languageOfChoice = schemas.GetProperty("PersonProperties").GetProperty("properties")
            .GetProperty("languageOfChoice");

        Assert.False(languageOfChoice.TryGetProperty("pattern", out _));

        JsonElement items = languageOfChoice.GetProperty("items");
        Assert.True(items.TryGetProperty("pattern", out JsonElement pattern));
        Assert.False(string.IsNullOrEmpty(pattern.GetString()));
    }

    [Fact]
    public async Task StringArrayPropertyWithoutEitherAttribute_ItemsSchemaCarriesNoPatternOrAnyOf()
    {
        // Regression guard: JsonSchemaExporter could in principle hand back a shared schema instance
        // for the plain "string" item type across every string-array property in the document -
        // this confirms the transformer's writes onto one property's `items` schema (e.g.
        // Course.modesOfDelivery above) don't leak onto an unrelated string array with neither
        // attribute, like Course.teachingLanguages.
        JsonElement schemas = await GetSchemasAsync();

        JsonElement teachingLanguages = schemas.GetProperty("Course").GetProperty("properties")
            .GetProperty("teachingLanguages");
        JsonElement items = teachingLanguages.GetProperty("items");

        Assert.False(items.TryGetProperty("pattern", out _));
        Assert.False(items.TryGetProperty("anyOf", out _));
    }
}
