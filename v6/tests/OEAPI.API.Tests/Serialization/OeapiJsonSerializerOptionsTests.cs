using System.Text.Json;
using System.Text.Json.Nodes;
using OEAPI.API.Serialization;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Infrastructure.Query;
using Xunit;

namespace OEAPI.API.Tests.Serialization;

/// <summary>
///     Unit-level (no HTTP/database) coverage of <see cref="OeapiJsonSerializerOptions" />'s
///     omit-null-optional-fields modifier, added for the <c>omit-null-optional-fields</c> change. Tests
///     the serializer in isolation rather than through a live endpoint, since the behavior under test is
///     a pure function of the object graph and this serializer options instance.
/// </summary>
public class OeapiJsonSerializerOptionsTests
{
    [Fact]
    public void OptionalNullFields_AreOmittedFromJson()
    {
        Organisation organisation = new()
        {
            OrganisationId = Guid.NewGuid().ToString(),
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = "ORG-1" },
            OrganisationType = "institute",
            Name = [new LanguageTypedString { Language = "en", Value = "Test Org" }],
            ShortName = null,
            Parent = null,
            Description = null
        };

        JsonObject json = SerializeToJsonObject(organisation);

        Assert.False(json.ContainsKey("shortName"));
        Assert.False(json.ContainsKey("parent"));
        Assert.False(json.ContainsKey("description"));
    }

    [Fact]
    public void OptionalFieldWithValue_IsPresentInJson()
    {
        Organisation organisation = new()
        {
            OrganisationId = Guid.NewGuid().ToString(),
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = "ORG-2" },
            OrganisationType = "institute",
            Name = [new LanguageTypedString { Language = "en", Value = "Test Org" }],
            ShortName = "TO"
        };

        JsonObject json = SerializeToJsonObject(organisation);

        Assert.True(json.ContainsKey("shortName"));
        Assert.Equal("TO", json["shortName"]!.GetValue<string>());
    }

    [Fact]
    public void RequiredFields_AreAlwaysPresent_EvenWhenNull()
    {
        // AcademicSession.StartDateTime is a required field per the spec manifest, declared as a
        // non-nullable string (defaults to string.Empty) in normal use - forced to null here via `!`
        // purely to exercise the serializer's defensive guarantee that a required key is never omitted,
        // independent of whether this state is otherwise reachable through the running application.
        AcademicSession academicSession = new()
        {
            AcademicSessionId = Guid.NewGuid().ToString(),
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = "AS-1" },
            Name = [new LanguageTypedString { Language = "en", Value = "Test Session" }],
            AcademicSessionType = "semester",
            StartDateTime = null!,
            EndDateTime = "2026-01-01T00:00:00+01:00"
        };

        JsonObject json = SerializeToJsonObject(academicSession);

        Assert.True(json.ContainsKey("startDateTime"), "a required field must remain present even when null");
        Assert.Null(json["startDateTime"]);
    }

    [Fact]
    public void RenamedSchemaMatchingClasses_NowOmitNullOptionalFields()
    {
        // EnrolmentPeriod/SupplementaryInformationItem/PersonPostResponse/AssignedNeed were renamed to
        // EnrolmentPeriods/SupplementaryInformation/PostResponse/PersonalNeed (matching their spec
        // schema name exactly) so this modifier's typeInfo.Type.Name lookup can actually find them in
        // the manifest - previously they were silently treated as "unknown schema" and never had null
        // optionals omitted at all. Only EnrolmentPeriods/PostResponse/PersonalNeed have a nullable
        // optional field to exercise here; SupplementaryInformation's 3 properties are all non-nullable.
        EnrolmentPeriods enrolmentPeriod = new() { StartDateTime = "2026-01-01T00:00:00+01:00", Comment = null };
        PostResponse postResponse = new() { PersonId = Guid.NewGuid().ToString(), Redirect = null };
        PersonalNeed personalNeed = new() { Code = "x", Description = null };

        JsonObject enrolmentPeriodJson = SerializeToJsonObject(enrolmentPeriod);
        JsonObject postResponseJson = SerializeToJsonObject(postResponse);
        JsonObject personalNeedJson = SerializeToJsonObject(personalNeed);

        Assert.False(enrolmentPeriodJson.ContainsKey("comment"));
        Assert.False(postResponseJson.ContainsKey("redirect"));
        Assert.False(personalNeedJson.ContainsKey("description"));
    }

    [Fact]
    public void UnknownTypeToManifest_KeepsExistingExtOmissionBehavior()
    {
        // PagedResult<T>'s reflected type name (e.g. "PagedResult`1") never matches a spec schema name,
        // so it's outside the new modifier's scope entirely - its one nullable field, Ext, must still be
        // omitted-when-null via the pre-existing, independent OmitNullExt modifier.
        PagedResult<Organisation> result = new([], 0, 1, 20);

        JsonObject json = SerializeToJsonObject(result);

        Assert.False(json.ContainsKey("ext"));
        Assert.True(json.ContainsKey("items"));

        // totalCount is never part of the spec's Pagination schema - excluded from serialization via
        // [JsonIgnore] on PagedResult<T>.TotalCount regardless of this modifier's own behavior.
        Assert.False(json.ContainsKey("totalCount"));
    }

    private static JsonObject SerializeToJsonObject<T>(T value)
    {
        string json = JsonSerializer.Serialize(value, OeapiJsonSerializerOptions.Instance);
        return JsonNode.Parse(json)!.AsObject();
    }
}
