using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using OEAPI.API.Tests.Infrastructure;
using OEAPI.Core.Interfaces;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Core.Models.ApiModels.Validation;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Regression tests confirming a write request with a string value exceeding a length the
///     persistence layer enforces, or violating a spec `pattern`, gets a clean <c>400</c>, never an
///     unhandled <c>500</c> from the database itself rejecting the value.
/// </summary>
[Collection(SqlServerCollection.Name)]
public class WriteBodyStringValidationConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Fact]
    public async Task PutOrganisation_PrimaryCodeOverLengthLimit_ReturnsBadRequest()
    {
        // Regression test for the crash this change was opened to fix: OrganisationEntity.PrimaryCode
        // is [StringLength(256)] at the persistence layer; without a matching attribute on
        // IdentifierEntry.Code, a longer value reached the database and crashed with
        // "String or binary data would be truncated" instead of a clean 400.
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        Organisation created = new()
        {
            OrganisationId = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = new string('x', 257) },
            OrganisationType = "institute",
            Name = [new LanguageTypedString { Language = "en", Value = "Over-Length Code Org" }]
        };

        HttpResponseMessage response = await client.PutAsJsonAsync($"/organisations/{id}", created);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PutOrganisation_PrimaryCodeAtExactLengthLimit_Succeeds()
    {
        // The fix must not be off-by-one in the strict direction either - exactly 256 characters
        // (OrganisationEntity.PrimaryCode's own limit) must still succeed.
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        Organisation created = new()
        {
            OrganisationId = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = new string('x', 256) },
            OrganisationType = "institute",
            Name = [new LanguageTypedString { Language = "en", Value = "At-Limit Code Org" }]
        };

        HttpResponseMessage response = await client.PutAsJsonAsync($"/organisations/{id}", created);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task PutOrganisation_CodeTypeOverLengthLimit_ReturnsBadRequest()
    {
        // "x-" prefixed so this is otherwise a schema-valid extensible-enum custom value (see
        // ExtensibleEnumAttribute) - isolates the length violation from that separate check, rather
        // than failing both at once for an unrelated reason.
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        Organisation created = new()
        {
            OrganisationId = id,
            PrimaryCode = new IdentifierEntry { CodeType = "x-" + new string('y', 256), Code = "OK-CODE" },
            OrganisationType = "institute",
            Name = [new LanguageTypedString { Language = "en", Value = "Over-Length CodeType Org" }]
        };

        HttpResponseMessage response = await client.PutAsJsonAsync($"/organisations/{id}", created);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PutOrganisation_CodeTypeAtExactLengthLimit_Succeeds()
    {
        // Symmetry with PrimaryCodeAtExactLengthLimit_Succeeds above - every over-limit test in this
        // file should have an at-limit counterpart proving the fix isn't off-by-one in the strict
        // direction. "x-" prefixed for the same reason as the over-limit test above.
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        Organisation created = new()
        {
            OrganisationId = id,
            PrimaryCode = new IdentifierEntry { CodeType = "x-" + new string('y', 254), Code = "OK-CODE" },
            OrganisationType = "institute",
            Name = [new LanguageTypedString { Language = "en", Value = "At-Limit CodeType Org" }]
        };

        HttpResponseMessage response = await client.PutAsJsonAsync($"/organisations/{id}", created);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task PutOrganisation_OtherCodesEntryOverLengthLimit_ReturnsBadRequest()
    {
        // Task 5.2: IdentifierEntry's [StringLength(256)] is shared by PrimaryCode and every element of
        // OtherCodes[] - the tests above only exercise it via PrimaryCode. ASP.NET Core's model-state
        // validation recurses into array elements of a validatable complex type by default, but that's
        // a distinct mechanism from validating a single nested object, so it's worth confirming
        // directly rather than assuming: OrganisationMappingExtensions.cs confirms OtherCodes is
        // genuinely read from the incoming model and persisted (audit.md's "shared value objects" note
        // for task 2.1).
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        Organisation created = new()
        {
            OrganisationId = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"OC-{id[..8]}" },
            OrganisationType = "institute",
            Name = [new LanguageTypedString { Language = "en", Value = "Over-Length OtherCode Org" }],
            OtherCodes = [new IdentifierEntry { CodeType = "identifier", Code = new string('x', 257) }]
        };

        HttpResponseMessage response = await client.PutAsJsonAsync($"/organisations/{id}", created);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PutOrganisation_OtherCodesEntryAtExactLengthLimit_Succeeds()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        Organisation created = new()
        {
            OrganisationId = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"OC-{id[..8]}" },
            OrganisationType = "institute",
            Name = [new LanguageTypedString { Language = "en", Value = "At-Limit OtherCode Org" }],
            OtherCodes = [new IdentifierEntry { CodeType = "identifier", Code = new string('x', 256) }]
        };

        HttpResponseMessage response = await client.PutAsJsonAsync($"/organisations/{id}", created);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task PutOrganisation_NameLanguageDoesNotMatchRfc5646Pattern_ReturnsBadRequest()
    {
        // RegexPatternAttribute case: LanguageTypedString.Language is a plain string - exercises the
        // built-in-[RegularExpression]-compatible path. RFC 5646, not the spec's own (imprecise)
        // "RFC 4647" citation - see RegexPatterns.Values' "language" entry.
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        Organisation created = new()
        {
            OrganisationId = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"LANG-{id[..8]}" },
            OrganisationType = "institute",
            Name = [new LanguageTypedString { Language = "not_a_valid_tag", Value = "Bad Language Org" }]
        };

        HttpResponseMessage response = await client.PutAsJsonAsync($"/organisations/{id}", created);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PutOrganisation_NameLanguageComplexButValidTag_Succeeds()
    {
        // The pattern must not over-reject legitimate, more specific tags - canonically-cased
        // script+region ("zh-Hant-TW"), one of the spec's own three documented examples that its
        // literal (buggy) pattern rejects but the corrected RFC-5646-based one accepts. See
        // RegexPatternAttributeTests.MatchingValue_IsValid.
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        Organisation created = new()
        {
            OrganisationId = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"LANG-{id[..8]}" },
            OrganisationType = "institute",
            Name = [new LanguageTypedString { Language = "zh-Hant-TW", Value = "Complex Language Org" }]
        };

        HttpResponseMessage response = await client.PutAsJsonAsync($"/organisations/{id}", created);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task PutCourseOffering_TeachingLanguagesContainsInvalidTag_ReturnsBadRequest()
    {
        // RegexPatternAttribute's string[] case (a real array property, not just IEnumerable<string>
        // in the abstract) - exercises the path built-in [RegularExpression] can't handle at all.
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        CourseOffering created = new()
        {
            CourseOfferingIdValue = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"TL-{id[..8]}" },
            Name = [new LanguageTypedString { Language = "en", Value = "Bad Teaching Language Offering" }],
            TeachingLanguages = ["en", "not_a_valid_tag"]
        };

        HttpResponseMessage response = await client.PutAsJsonAsync($"/course-offerings/{id}", created);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    // --- Task 4.1: Cost.amount/vatAmount/amountWithoutVat pattern (^\d+(?:\.\d+)?$) ------------------
    //
    // No entity-level [StringLength] to match here - Cost is persisted as part of the unbounded
    // PriceInformationJson blob on every -offerings entity (see audit.md's pattern-fields table), so
    // the spec's own pattern is the only source of truth. Uses CourseOffering as the vehicle, same as
    // the TeachingLanguages test above - PriceInformation is confirmed write-reachable via
    // CourseOfferingMappingExtensions.cs reading and persisting model.PriceInformation.

    [Fact]
    public async Task PutCourseOffering_PriceInformationAmountDoesNotMatchPattern_ReturnsBadRequest()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        CourseOffering created = new()
        {
            CourseOfferingIdValue = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"COST-{id[..8]}" },
            Name = [new LanguageTypedString { Language = "en", Value = "Bad Cost Amount Offering" }],
            PriceInformation =
            [
                new Cost { CostType = "total_costs", Amount = "12,34" } // comma, not the required dot
            ]
        };

        HttpResponseMessage response = await client.PutAsJsonAsync($"/course-offerings/{id}", created);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PutCourseOffering_PriceInformationAmountValid_Succeeds()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        CourseOffering created = new()
        {
            CourseOfferingIdValue = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"COST-{id[..8]}" },
            Name = [new LanguageTypedString { Language = "en", Value = "Valid Cost Amount Offering" }],
            PriceInformation =
            [
                new Cost
                {
                    CostType = "total_costs", Amount = "340.84", VatAmount = "40", AmountWithoutVat = "300.84"
                }
            ]
        };

        HttpResponseMessage response = await client.PutAsJsonAsync($"/course-offerings/{id}", created);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    // --- Task 3.1: OrganisationEntity's own resource-specific fields --------------------------------
    //
    // One theory per entity rather than one near-identical Fact per field (per tasks.md 5.2), covering
    // every field task 3.1's audit confirmed write-reachable (OrganisationType/ShortName/Link/Logo -
    // see audit.md: OrganisationId is always taken from the route, never the body;
    // ContactEmail/ContactTelephone/ConsumerKey/Addresses are never read by the write path at all).

    /// <summary>
    ///     Fields that also carry <see cref="ExtensibleEnumAttribute" /> (requiring an "x-" prefix or a
    ///     known value) - a generic length-boundary test on one of these needs an "x-"-prefixed value,
    ///     not an arbitrary string, or it fails for the wrong reason (the enum check, not the length
    ///     check being tested).
    /// </summary>
    private static readonly HashSet<string> ExtensibleEnumFields =
    [
        nameof(Organisation.OrganisationType), nameof(Person.Gender), nameof(Group.GroupType),
        nameof(Membership.Role), nameof(Membership.State), // also covers CourseOffering.State - same name
        nameof(CourseOffering.RosteringState), nameof(CourseOffering.ResultValueType),
        nameof(CourseOfferingAssociation.RemoteState),
        nameof(LearningComponentOfferingAssociation.Attendance)
    ];

    /// <summary>
    ///     A string of exactly <paramref name="length" /> characters, valid against every *other*
    ///     constraint the given field might carry - see <see cref="ExtensibleEnumFields" /> - so an
    ///     over-length/at-limit length test on that field doesn't fail for the wrong reason.
    /// </summary>
    private static string ValueOfLength(string fieldName, int length) =>
        ExtensibleEnumFields.Contains(fieldName) ? "x-" + new string('y', length - 2) : new string('x', length);

    [Theory]
    [InlineData(nameof(Organisation.OrganisationType), 256)]
    [InlineData(nameof(Organisation.ShortName), 256)]
    [InlineData(nameof(Organisation.Link), 2048)]
    [InlineData(nameof(Organisation.Logo), 2048)]
    public async Task PutOrganisation_FieldOverLengthLimit_ReturnsBadRequest(string fieldName, int maxLength)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        Organisation created = new()
        {
            OrganisationId = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"F-{id[..8]}" },
            OrganisationType = "institute",
            Name = [new LanguageTypedString { Language = "en", Value = "Field Length Test Org" }]
        };
        typeof(Organisation).GetProperty(fieldName)!.SetValue(created, ValueOfLength(fieldName, maxLength + 1));

        HttpResponseMessage response = await client.PutAsJsonAsync($"/organisations/{id}", created);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData(nameof(Organisation.OrganisationType), 256)]
    [InlineData(nameof(Organisation.ShortName), 256)]
    [InlineData(nameof(Organisation.Link), 2048)]
    [InlineData(nameof(Organisation.Logo), 2048)]
    public async Task PutOrganisation_FieldAtExactLengthLimit_Succeeds(string fieldName, int maxLength)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        Organisation created = new()
        {
            OrganisationId = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"F-{id[..8]}" },
            OrganisationType = "institute",
            Name = [new LanguageTypedString { Language = "en", Value = "Field Length Test Org" }]
        };
        typeof(Organisation).GetProperty(fieldName)!.SetValue(created, ValueOfLength(fieldName, maxLength));

        HttpResponseMessage response = await client.PutAsJsonAsync($"/organisations/{id}", created);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    // --- Task 3.2: PersonEntity's own resource-specific fields ---------------------------------------
    //
    // 23 of PersonEntity's 27 [StringLength] attributes needed a new attribute here (2 already covered
    // by IdentifierEntry - task 2.1; PersonId is always taken from the route, same pattern as
    // Organisation; ConsumerKey is never read by any write path at all, not even the demo seeder - see
    // audit.md). Person (PUT) and PersonProperties (POST) both hand-map the identical field set (not
    // shared mapping code - PersonProperties has no personId at all, so it can't reuse
    // PersonMappingExtensions), so both need the same 23 attributes and both get covered here.

    private static readonly (string Field, int MaxLength)[] PersonFields =
    [
        (nameof(Person.GivenName), 256), (nameof(Person.AlternateName), 256),
        (nameof(Person.PreferredName), 256), (nameof(Person.SurnamePrefix), 256),
        (nameof(Person.Surname), 256), (nameof(Person.DisplayName), 256), (nameof(Person.Initials), 256),
        (nameof(Person.IdCheckName), 2048), (nameof(Person.DateOfBirth), 256),
        (nameof(Person.CityOfBirth), 256), (nameof(Person.DateOfNationality), 256),
        (nameof(Person.Email), 256), (nameof(Person.SecondaryEmail), 256),
        (nameof(Person.TelephoneNumber), 256), (nameof(Person.MobileNumber), 256),
        (nameof(Person.PhotoSocial), 2048), (nameof(Person.PhotoOfficial), 2048),
        // Widened from the original 1 char (which left no room for an "x-"-prefixed custom value at
        // all) to 256, matching every other extensible-enum field - see PersonEntity.Gender's own doc
        // comment and the WidenPersonGender migration.
        (nameof(Person.Gender), 256),
        (nameof(Person.TitlePrefix), 256), (nameof(Person.TitleSuffix), 256), (nameof(Person.Office), 256),
        (nameof(Person.IceName), 256), (nameof(Person.IcePhoneNumber), 256)
    ];

    public static TheoryData<string, int> PersonFieldTheoryData()
    {
        TheoryData<string, int> data = [];
        foreach ((string field, int maxLength) in PersonFields) data.Add(field, maxLength);
        return data;
    }

    [Theory]
    [MemberData(nameof(PersonFieldTheoryData))]
    public async Task PutPerson_FieldOverLengthLimit_ReturnsBadRequest(string fieldName, int maxLength)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        Person created = new()
        {
            PersonIdValue = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"P-{id[..8]}" },
            Surname = "Field Length Test"
        };
        typeof(Person).GetProperty(fieldName)!.SetValue(created, ValueOfLength(fieldName, maxLength + 1));

        HttpResponseMessage response = await client.PutAsJsonAsync($"/persons/{id}", created);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [MemberData(nameof(PersonFieldTheoryData))]
    public async Task PutPerson_FieldAtExactLengthLimit_Succeeds(string fieldName, int maxLength)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        Person created = new()
        {
            PersonIdValue = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"P-{id[..8]}" },
            Surname = "Field Length Test"
        };
        typeof(Person).GetProperty(fieldName)!.SetValue(created, ValueOfLength(fieldName, maxLength));

        HttpResponseMessage response = await client.PutAsJsonAsync($"/persons/{id}", created);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Theory]
    [MemberData(nameof(PersonFieldTheoryData))]
    public async Task PostPerson_FieldOverLengthLimit_ReturnsBadRequest(string fieldName, int maxLength)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();

        PersonProperties created = new()
        {
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"PP-{Guid.NewGuid():N}"[..16] },
            Surname = "Field Length Test"
        };
        typeof(PersonProperties).GetProperty(fieldName)!.SetValue(created, ValueOfLength(fieldName, maxLength + 1));

        HttpResponseMessage response = await client.PostAsJsonAsync("/persons", created);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [MemberData(nameof(PersonFieldTheoryData))]
    public async Task PostPerson_FieldAtExactLengthLimit_Succeeds(string fieldName, int maxLength)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();

        PersonProperties created = new()
        {
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"PP-{Guid.NewGuid():N}"[..16] },
            Surname = "Field Length Test"
        };
        typeof(PersonProperties).GetProperty(fieldName)!.SetValue(created, ValueOfLength(fieldName, maxLength));

        HttpResponseMessage response = await client.PostAsJsonAsync("/persons", created);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    // --- Task 3.3: GroupEntity's own resource-specific fields -----------------------------------------
    //
    // Only 3 of GroupEntity's 8 attrs needed a new attribute: GroupId not write-reachable (same
    // route-overwrite pattern), PrimaryCodeType/PrimaryCode already covered by IdentifierEntry,
    // ConsumerKey not write-reachable (never assigned anywhere, not even the demo seeder), and
    // Abbreviation isn't write-reachable *or* read-reachable - it has no corresponding property on the
    // Group API model at all, and no `abbreviation` field in the spec's own Group.yaml either (unlike
    // every other entity, which all correctly map an Abbreviation field) - a genuinely dead entity
    // column, not just an unwired one.

    [Theory]
    [InlineData(nameof(Group.GroupType), 256)]
    [InlineData(nameof(Group.StartDateTime), 256)]
    [InlineData(nameof(Group.EndDateTime), 256)]
    public async Task PutGroup_FieldOverLengthLimit_ReturnsBadRequest(string fieldName, int maxLength)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        Group created = new()
        {
            GroupIdValue = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"G-{id[..8]}" },
            GroupType = "class",
            Name = [new LanguageTypedString { Language = "en", Value = "Field Length Test Group" }]
        };
        typeof(Group).GetProperty(fieldName)!.SetValue(created, ValueOfLength(fieldName, maxLength + 1));

        HttpResponseMessage response = await client.PutAsJsonAsync($"/groups/{id}", created);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData(nameof(Group.GroupType), 256)]
    [InlineData(nameof(Group.StartDateTime), 256)]
    [InlineData(nameof(Group.EndDateTime), 256)]
    public async Task PutGroup_FieldAtExactLengthLimit_Succeeds(string fieldName, int maxLength)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        Group created = new()
        {
            GroupIdValue = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"G-{id[..8]}" },
            GroupType = "class",
            Name = [new LanguageTypedString { Language = "en", Value = "Field Length Test Group" }]
        };
        typeof(Group).GetProperty(fieldName)!.SetValue(created, ValueOfLength(fieldName, maxLength));

        HttpResponseMessage response = await client.PutAsJsonAsync($"/groups/{id}", created);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    // --- Task 3.4: MembershipEntity's own resource-specific fields -------------------------------------
    //
    // MembershipIdValue is not write-reachable - GroupsController.ReplaceMembershipInGroup always
    // generates a fresh one on create and never reads model.MembershipIdValue at all (the resource is
    // keyed by groupId+personId in the URL, not its own id). ConsumerKey not write-reachable (never
    // assigned anywhere). Role/State/StartDateTime/EndDateTime all needed a new attribute - confirmed
    // via the controller's own inline upsert logic (this endpoint isn't backed by
    // MembershipMappingExtensions.cs at all - that file only has the GET-side ToApiModel).

    private async Task<(string GroupId, string PersonId)> CreateGroupAndPersonAsync(HttpClient client)
    {
        string groupId = Guid.NewGuid().ToString();
        string personId = Guid.NewGuid().ToString();

        await client.PutAsJsonAsync($"/groups/{groupId}", new Group
        {
            GroupIdValue = groupId,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"MG-{groupId[..8]}" },
            GroupType = "class"
        });
        await client.PutAsJsonAsync($"/persons/{personId}", new Person
        {
            PersonIdValue = personId,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"MP-{personId[..8]}" },
            Surname = "MembershipFieldLengthTestPerson"
        });

        return (groupId, personId);
    }

    [Theory]
    [InlineData(nameof(Membership.Role), 256)]
    [InlineData(nameof(Membership.State), 64)]
    [InlineData(nameof(Membership.StartDateTime), 256)]
    [InlineData(nameof(Membership.EndDateTime), 256)]
    public async Task PutGroupMembership_FieldOverLengthLimit_ReturnsBadRequest(string fieldName, int maxLength)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        (string groupId, string personId) = await CreateGroupAndPersonAsync(client);

        Membership created = new() { Role = "student", State = "active" };
        typeof(Membership).GetProperty(fieldName)!.SetValue(created, ValueOfLength(fieldName, maxLength + 1));

        HttpResponseMessage response =
            await client.PutAsJsonAsync($"/groups/{groupId}/memberships/{personId}", created);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData(nameof(Membership.Role), 256)]
    [InlineData(nameof(Membership.State), 64)]
    [InlineData(nameof(Membership.StartDateTime), 256)]
    [InlineData(nameof(Membership.EndDateTime), 256)]
    public async Task PutGroupMembership_FieldAtExactLengthLimit_Succeeds(string fieldName, int maxLength)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        (string groupId, string personId) = await CreateGroupAndPersonAsync(client);

        Membership created = new() { Role = "student", State = "active" };
        typeof(Membership).GetProperty(fieldName)!.SetValue(created, ValueOfLength(fieldName, maxLength));

        HttpResponseMessage response =
            await client.PutAsJsonAsync($"/groups/{groupId}/memberships/{personId}", created);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    // --- Task 3.5: CourseOfferingEntity's own resource-specific fields ---------------------------------
    //
    // CourseOfferingId not write-reachable (route overwrite), PrimaryCodeType/PrimaryCode already
    // covered, ConsumerKey not write-reachable (never assigned anywhere - same pattern as every prior
    // entity). The other 9 string fields all needed [StringLength]. `ConsumerJson`'s own
    // [StringLength(2048)] (unlike every sibling entity's ConsumerJson, and this entity's own other
    // *Json columns, all unbounded) was an implementation-introduced outlier with no basis in the spec
    // (Consumer.yaml declares no size constraint at all) - fixed by widening the entity (unbounded,
    // matching the established convention), not by validating Consumer against it. See tasks.md 3.5's
    // "Correction" note.

    private static readonly (string Field, int MaxLength)[] CourseOfferingFields =
    [
        (nameof(CourseOffering.StartDateTime), 256), (nameof(CourseOffering.EndDateTime), 256),
        (nameof(CourseOffering.FlexibleEntryPeriodStartDateTime), 256),
        (nameof(CourseOffering.FlexibleEntryPeriodEndDateTime), 256),
        (nameof(CourseOffering.State), 256), (nameof(CourseOffering.RosteringState), 256),
        (nameof(CourseOffering.Abbreviation), 256), (nameof(CourseOffering.ResultValueType), 256),
        (nameof(CourseOffering.Link), 2048)
    ];

    public static TheoryData<string, int> CourseOfferingFieldTheoryData()
    {
        TheoryData<string, int> data = [];
        foreach ((string field, int maxLength) in CourseOfferingFields) data.Add(field, maxLength);
        return data;
    }

    [Theory]
    [MemberData(nameof(CourseOfferingFieldTheoryData))]
    public async Task PutCourseOffering_FieldOverLengthLimit_ReturnsBadRequest(string fieldName, int maxLength)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        CourseOffering created = new()
        {
            CourseOfferingIdValue = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"CO-{id[..8]}" },
            Name = [new LanguageTypedString { Language = "en", Value = "Field Length Test Offering" }]
        };
        typeof(CourseOffering).GetProperty(fieldName)!.SetValue(created, ValueOfLength(fieldName, maxLength + 1));

        HttpResponseMessage response = await client.PutAsJsonAsync($"/course-offerings/{id}", created);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [MemberData(nameof(CourseOfferingFieldTheoryData))]
    public async Task PutCourseOffering_FieldAtExactLengthLimit_Succeeds(string fieldName, int maxLength)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        CourseOffering created = new()
        {
            CourseOfferingIdValue = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"CO-{id[..8]}" },
            Name = [new LanguageTypedString { Language = "en", Value = "Field Length Test Offering" }]
        };
        typeof(CourseOffering).GetProperty(fieldName)!.SetValue(created, ValueOfLength(fieldName, maxLength));

        HttpResponseMessage response = await client.PutAsJsonAsync($"/course-offerings/{id}", created);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task PutCourseOffering_LargeConsumerObject_Succeeds()
    {
        // Regression test for the live-reproduced crash: a legitimate object whose JSON serialization
        // exceeds 2048 chars previously hit ConsumerJson's [StringLength(2048)] (an implementation-
        // introduced outlier - see tasks.md 3.5) and crashed with "String or binary data would be
        // truncated". Fixed by widening the entity to match every sibling ConsumerJson (unbounded), not
        // by validating Consumer - so the correct behaviour here is that this now just succeeds.
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        CourseOffering created = new()
        {
            CourseOfferingIdValue = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"COC-{id[..8]}" },
            Name = [new LanguageTypedString { Language = "en", Value = "Big Consumer Offering" }],
            Consumer = new { note = new string('x', 2100) }
        };

        HttpResponseMessage response = await client.PutAsJsonAsync($"/course-offerings/{id}", created);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    // --- Task 3.6: LearningComponentOfferingEntity's own resource-specific fields ----------------------
    //
    // Identical shape to CourseOffering (3.5): LearningComponentOfferingIdValue not write-reachable
    // (route overwrite), PrimaryCodeType/PrimaryCode already covered, ConsumerKey not write-reachable
    // (never assigned anywhere). ConsumerJson has no surprise length limit this time (checked first,
    // given 3.5's finding) - unbounded, matching every other sibling. The other 9 string fields all
    // needed [StringLength]; State/RosteringState/ResultValueType already in ExtensibleEnumFields from
    // 3.5 (same bare property names), so no test-helper changes needed here.

    private static readonly (string Field, int MaxLength)[] LearningComponentOfferingFields =
    [
        (nameof(LearningComponentOffering.StartDateTime), 256),
        (nameof(LearningComponentOffering.EndDateTime), 256),
        (nameof(LearningComponentOffering.FlexibleEntryPeriodStartDateTime), 256),
        (nameof(LearningComponentOffering.FlexibleEntryPeriodEndDateTime), 256),
        (nameof(LearningComponentOffering.State), 256), (nameof(LearningComponentOffering.RosteringState), 256),
        (nameof(LearningComponentOffering.Abbreviation), 256),
        (nameof(LearningComponentOffering.ResultValueType), 256), (nameof(LearningComponentOffering.Link), 2048)
    ];

    public static TheoryData<string, int> LearningComponentOfferingFieldTheoryData()
    {
        TheoryData<string, int> data = [];
        foreach ((string field, int maxLength) in LearningComponentOfferingFields) data.Add(field, maxLength);
        return data;
    }

    [Theory]
    [MemberData(nameof(LearningComponentOfferingFieldTheoryData))]
    public async Task PutLearningComponentOffering_FieldOverLengthLimit_ReturnsBadRequest(
        string fieldName, int maxLength)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        LearningComponentOffering created = new()
        {
            LearningComponentOfferingIdValue = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"LCO-{id[..8]}" },
            Name = [new LanguageTypedString { Language = "en", Value = "Field Length Test LC Offering" }]
        };
        typeof(LearningComponentOffering).GetProperty(fieldName)!
            .SetValue(created, ValueOfLength(fieldName, maxLength + 1));

        HttpResponseMessage response = await client.PutAsJsonAsync($"/learning-component-offerings/{id}", created);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [MemberData(nameof(LearningComponentOfferingFieldTheoryData))]
    public async Task PutLearningComponentOffering_FieldAtExactLengthLimit_Succeeds(string fieldName, int maxLength)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        LearningComponentOffering created = new()
        {
            LearningComponentOfferingIdValue = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"LCO-{id[..8]}" },
            Name = [new LanguageTypedString { Language = "en", Value = "Field Length Test LC Offering" }]
        };
        typeof(LearningComponentOffering).GetProperty(fieldName)!.SetValue(created, ValueOfLength(fieldName, maxLength));

        HttpResponseMessage response = await client.PutAsJsonAsync($"/learning-component-offerings/{id}", created);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    // --- Task 3.7: ProgrammeOfferingEntity's own resource-specific fields ------------------------------
    //
    // Identical shape to CourseOffering (3.5)/LearningComponentOffering (3.6): ProgrammeOfferingIdValue
    // not write-reachable (route overwrite), PrimaryCodeType/PrimaryCode already covered, ConsumerKey
    // not write-reachable (never assigned anywhere). ConsumerJson has no surprise length limit (checked
    // first, per the 3.5 lesson) - unbounded, matching every other sibling. The other 9 string fields
    // all needed [StringLength]; State/RosteringState/ResultValueType already in ExtensibleEnumFields
    // from 3.5 (same bare property names), so no test-helper changes needed here.

    private static readonly (string Field, int MaxLength)[] ProgrammeOfferingFields =
    [
        (nameof(ProgrammeOffering.StartDateTime), 256),
        (nameof(ProgrammeOffering.EndDateTime), 256),
        (nameof(ProgrammeOffering.FlexibleEntryPeriodStartDateTime), 256),
        (nameof(ProgrammeOffering.FlexibleEntryPeriodEndDateTime), 256),
        (nameof(ProgrammeOffering.State), 256), (nameof(ProgrammeOffering.RosteringState), 256),
        (nameof(ProgrammeOffering.Abbreviation), 256),
        (nameof(ProgrammeOffering.ResultValueType), 256), (nameof(ProgrammeOffering.Link), 2048)
    ];

    public static TheoryData<string, int> ProgrammeOfferingFieldTheoryData()
    {
        TheoryData<string, int> data = [];
        foreach ((string field, int maxLength) in ProgrammeOfferingFields) data.Add(field, maxLength);
        return data;
    }

    [Theory]
    [MemberData(nameof(ProgrammeOfferingFieldTheoryData))]
    public async Task PutProgrammeOffering_FieldOverLengthLimit_ReturnsBadRequest(string fieldName, int maxLength)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        ProgrammeOffering created = new()
        {
            ProgrammeOfferingIdValue = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"PO-{id[..8]}" },
            Name = [new LanguageTypedString { Language = "en", Value = "Field Length Test Programme Offering" }]
        };
        typeof(ProgrammeOffering).GetProperty(fieldName)!
            .SetValue(created, ValueOfLength(fieldName, maxLength + 1));

        HttpResponseMessage response = await client.PutAsJsonAsync($"/programme-offerings/{id}", created);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [MemberData(nameof(ProgrammeOfferingFieldTheoryData))]
    public async Task PutProgrammeOffering_FieldAtExactLengthLimit_Succeeds(string fieldName, int maxLength)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        ProgrammeOffering created = new()
        {
            ProgrammeOfferingIdValue = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"PO-{id[..8]}" },
            Name = [new LanguageTypedString { Language = "en", Value = "Field Length Test Programme Offering" }]
        };
        typeof(ProgrammeOffering).GetProperty(fieldName)!.SetValue(created, ValueOfLength(fieldName, maxLength));

        HttpResponseMessage response = await client.PutAsJsonAsync($"/programme-offerings/{id}", created);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    // --- Task 3.8: TestComponentOfferingEntity's own resource-specific fields --------------------------
    //
    // Identical shape to CourseOffering (3.5)/LearningComponentOffering (3.6)/ProgrammeOffering (3.7):
    // TestComponentOfferingIdValue not write-reachable (route overwrite), PrimaryCodeType/PrimaryCode
    // already covered, ConsumerKey not write-reachable. Checked ConsumerJson first, per the 3.5 lesson -
    // unbounded here too, no fix needed. The other 9 string fields all needed [StringLength];
    // State/RosteringState/ResultValueType already in ExtensibleEnumFields from 3.5 (same bare property
    // names shared across classes), so no test-helper changes needed here.

    private static readonly (string Field, int MaxLength)[] TestComponentOfferingFields =
    [
        (nameof(TestComponentOffering.StartDateTime), 256),
        (nameof(TestComponentOffering.EndDateTime), 256),
        (nameof(TestComponentOffering.FlexibleEntryPeriodStartDateTime), 256),
        (nameof(TestComponentOffering.FlexibleEntryPeriodEndDateTime), 256),
        (nameof(TestComponentOffering.State), 256), (nameof(TestComponentOffering.RosteringState), 256),
        (nameof(TestComponentOffering.Abbreviation), 256),
        (nameof(TestComponentOffering.ResultValueType), 256), (nameof(TestComponentOffering.Link), 2048)
    ];

    public static TheoryData<string, int> TestComponentOfferingFieldTheoryData()
    {
        TheoryData<string, int> data = [];
        foreach ((string field, int maxLength) in TestComponentOfferingFields) data.Add(field, maxLength);
        return data;
    }

    [Theory]
    [MemberData(nameof(TestComponentOfferingFieldTheoryData))]
    public async Task PutTestComponentOffering_FieldOverLengthLimit_ReturnsBadRequest(string fieldName, int maxLength)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        TestComponentOffering created = new()
        {
            TestComponentOfferingIdValue = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"TCO-{id[..8]}" },
            Name = [new LanguageTypedString { Language = "en", Value = "Field Length Test Component Offering" }]
        };
        typeof(TestComponentOffering).GetProperty(fieldName)!
            .SetValue(created, ValueOfLength(fieldName, maxLength + 1));

        HttpResponseMessage response = await client.PutAsJsonAsync($"/test-component-offerings/{id}", created);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [MemberData(nameof(TestComponentOfferingFieldTheoryData))]
    public async Task PutTestComponentOffering_FieldAtExactLengthLimit_Succeeds(string fieldName, int maxLength)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        TestComponentOffering created = new()
        {
            TestComponentOfferingIdValue = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"TCO-{id[..8]}" },
            Name = [new LanguageTypedString { Language = "en", Value = "Field Length Test Component Offering" }]
        };
        typeof(TestComponentOffering).GetProperty(fieldName)!.SetValue(created, ValueOfLength(fieldName, maxLength));

        HttpResponseMessage response = await client.PutAsJsonAsync($"/test-component-offerings/{id}", created);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    // --- Task 3.9: CourseOfferingAssociationEntity's own resource-specific fields ----------------------
    //
    // CourseOfferingAssociationIdValue not write-reachable (route overwrite on PUT; server-generated
    // Guid on POST external/me), PrimaryCodeType/PrimaryCode already covered, ConsumerKey not
    // write-reachable. ConsumerJson unbounded, no fix needed. The other 6 fields all needed
    // [StringLength]. Unlike every entity so far, this one is write-reachable via *two* distinct API
    // model classes with independent [FromBody] bindings - PUT /{id} (CourseOfferingAssociation) and
    // POST /external/me (CourseOfferingAssociationExternalMeRequest, a narrower, endpoint-specific
    // model) - so both needed the attributes applied and both are covered below, not just one.
    //
    // Role is [StringLength(64)] at the entity layer, narrower than every other string field in this
    // change - checked against the spec (source/enumerations/associationRole.yaml: plain `type:
    // string`, no maxLength) and against sibling entities before treating this as another Gender-style
    // outlier (task 3.2): all four *OfferingAssociationEntity classes (this one plus 3.10-3.13) share
    // the identical [StringLength(64)] on Role for the same associationRole enum - a consistent design
    // choice repeated across the whole Association family, not a lone outlier, so validated at 64 as
    // given rather than widened.

    private static readonly (string Field, int MaxLength)[] CourseOfferingAssociationFields =
    [
        (nameof(CourseOfferingAssociation.Role), 64),
        (nameof(CourseOfferingAssociation.StartDateTime), 256),
        (nameof(CourseOfferingAssociation.ExpectedEndDateTime), 256),
        (nameof(CourseOfferingAssociation.ActualEndDateTime), 256),
        (nameof(CourseOfferingAssociation.State), 256), (nameof(CourseOfferingAssociation.RemoteState), 256)
    ];

    public static TheoryData<string, int> CourseOfferingAssociationFieldTheoryData()
    {
        TheoryData<string, int> data = [];
        foreach ((string field, int maxLength) in CourseOfferingAssociationFields) data.Add(field, maxLength);
        return data;
    }

    [Theory]
    [MemberData(nameof(CourseOfferingAssociationFieldTheoryData))]
    public async Task PutCourseOfferingAssociation_FieldOverLengthLimit_ReturnsBadRequest(
        string fieldName, int maxLength)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        CourseOfferingAssociation created = new() { AssociationIdValue = id };
        typeof(CourseOfferingAssociation).GetProperty(fieldName)!
            .SetValue(created, ValueOfLength(fieldName, maxLength + 1));

        HttpResponseMessage response = await client.PutAsJsonAsync($"/course-offering-associations/{id}", created);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [MemberData(nameof(CourseOfferingAssociationFieldTheoryData))]
    public async Task PutCourseOfferingAssociation_FieldAtExactLengthLimit_Succeeds(string fieldName, int maxLength)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        CourseOfferingAssociation created = new() { AssociationIdValue = id };
        typeof(CourseOfferingAssociation).GetProperty(fieldName)!.SetValue(created, ValueOfLength(fieldName, maxLength));

        HttpResponseMessage response = await client.PutAsJsonAsync($"/course-offering-associations/{id}", created);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    private WebApplicationFactory<Program> CreateAuthenticatedFactory(string personId)
    {
        return _fixture.CreateFactory().WithWebHostBuilder(builder =>
            builder.ConfigureTestServices(services =>
                services.AddScoped<ICurrentPersonProvider>(_ => new FakeCurrentPersonProvider(personId))));
    }

    [Fact]
    public async Task PostCourseOfferingAssociationExternalMe_RoleOverLengthLimit_ReturnsBadRequest()
    {
        string personId = Guid.NewGuid().ToString();
        string courseOfferingId = Guid.NewGuid().ToString();
        string issuerId = Guid.NewGuid().ToString();
        using WebApplicationFactory<Program> factory = CreateAuthenticatedFactory(personId);
        using HttpClient client = factory.CreateClient();
        await client.PutAsJsonAsync($"/persons/{personId}",
            new Person
            {
                PersonIdValue = personId,
                PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"COAEM-{personId[..8]}" },
                Surname = "FieldLengthTestPerson"
            });
        await client.PutAsJsonAsync($"/course-offerings/{courseOfferingId}", new CourseOffering
        {
            CourseOfferingIdValue = courseOfferingId,
            PrimaryCode = new IdentifierEntry
            { CodeType = "identifier", Code = $"COAEM-CO-{courseOfferingId[..8]}" },
            Name = [new LanguageTypedString { Language = "en", Value = "Field Length Test Offering" }]
        });
        await client.PutAsJsonAsync($"/organisations/{issuerId}", new Organisation
        {
            OrganisationId = issuerId,
            PrimaryCode = new IdentifierEntry { CodeType = "organisation_id", Code = $"COAEM-ORG-{issuerId[..8]}" },
            OrganisationType = "root",
            Name = [new LanguageTypedString { Language = "en", Value = "Field Length Test Issuer" }]
        });

        HttpResponseMessage response = await client.PostAsJsonAsync("/course-offering-associations/external/me",
            new CourseOfferingAssociationExternalMeRequest
            {
                Role = "x-" + new string('y', 63), // 65 chars, one over the 64 limit
                State = "pending",
                CourseOfferingId = new Identifier { Value = courseOfferingId },
                Issuer = new Organisation
                {
                    OrganisationId = issuerId,
                    PrimaryCode = new IdentifierEntry
                    { CodeType = "organisation_id", Code = $"COAEM-ORG-{issuerId[..8]}" },
                    OrganisationType = "root",
                    Name = [new LanguageTypedString { Language = "en", Value = "Field Length Test Issuer" }]
                }
            });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PostCourseOfferingAssociationExternalMe_RoleAtExactLengthLimit_Succeeds()
    {
        string personId = Guid.NewGuid().ToString();
        string courseOfferingId = Guid.NewGuid().ToString();
        string issuerId = Guid.NewGuid().ToString();
        using WebApplicationFactory<Program> factory = CreateAuthenticatedFactory(personId);
        using HttpClient client = factory.CreateClient();
        await client.PutAsJsonAsync($"/persons/{personId}",
            new Person
            {
                PersonIdValue = personId,
                PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"COAEM-{personId[..8]}" },
                Surname = "FieldLengthTestPerson"
            });
        await client.PutAsJsonAsync($"/course-offerings/{courseOfferingId}", new CourseOffering
        {
            CourseOfferingIdValue = courseOfferingId,
            PrimaryCode = new IdentifierEntry
            { CodeType = "identifier", Code = $"COAEM-CO-{courseOfferingId[..8]}" },
            Name = [new LanguageTypedString { Language = "en", Value = "Field Length Test Offering" }]
        });
        await client.PutAsJsonAsync($"/organisations/{issuerId}", new Organisation
        {
            OrganisationId = issuerId,
            PrimaryCode = new IdentifierEntry { CodeType = "organisation_id", Code = $"COAEM-ORG-{issuerId[..8]}" },
            OrganisationType = "root",
            Name = [new LanguageTypedString { Language = "en", Value = "Field Length Test Issuer" }]
        });

        HttpResponseMessage response = await client.PostAsJsonAsync("/course-offering-associations/external/me",
            new CourseOfferingAssociationExternalMeRequest
            {
                Role = "x-" + new string('y', 62), // exactly 64 chars
                State = "pending",
                CourseOfferingId = new Identifier { Value = courseOfferingId },
                Issuer = new Organisation
                {
                    OrganisationId = issuerId,
                    PrimaryCode = new IdentifierEntry
                    { CodeType = "organisation_id", Code = $"COAEM-ORG-{issuerId[..8]}" },
                    OrganisationType = "root",
                    Name = [new LanguageTypedString { Language = "en", Value = "Field Length Test Issuer" }]
                }
            });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    // --- Task 3.10: LearningComponentOfferingAssociationEntity's own resource-specific fields ----------
    //
    // LearningComponentOfferingAssociationIdValue not write-reachable (route overwrite),
    // PrimaryCodeType/PrimaryCode already covered, ConsumerKey not write-reachable, ConsumerJson
    // unbounded (checked first, per the 3.5 lesson). Only one write path this time (PUT /{id}) - unlike
    // CourseOfferingAssociation (3.9), this resource has no `external/me` endpoint. The other 7 fields
    // all needed [StringLength]; Role/State/RemoteState already validated at the same limits for
    // CourseOfferingAssociation (3.9). Attendance is new here - [StringLength(64)] at the entity layer,
    // checked against the spec (source/enumerations/associationAttendance.yaml: plain `type: string`, no
    // maxLength) - no conflict with ExtensibleEnum's "x-" prefix. Noted, not yet acted on: `Attendance`
    // also exists on TestComponentOfferingAssociationEntity (64, matches) and
    // TestComponentOfferingAssociationAttemptEntity (256, does not) - a genuine 2-vs-1 inconsistency to
    // resolve when reaching whichever of 3.12/3.13 is affected, not blocking this task.

    private static readonly (string Field, int MaxLength)[] LearningComponentOfferingAssociationFields =
    [
        (nameof(LearningComponentOfferingAssociation.Role), 64),
        (nameof(LearningComponentOfferingAssociation.Attendance), 64),
        (nameof(LearningComponentOfferingAssociation.StartDateTime), 256),
        (nameof(LearningComponentOfferingAssociation.ExpectedEndDateTime), 256),
        (nameof(LearningComponentOfferingAssociation.ActualEndDateTime), 256),
        (nameof(LearningComponentOfferingAssociation.State), 256),
        (nameof(LearningComponentOfferingAssociation.RemoteState), 256)
    ];

    public static TheoryData<string, int> LearningComponentOfferingAssociationFieldTheoryData()
    {
        TheoryData<string, int> data = [];
        foreach ((string field, int maxLength) in LearningComponentOfferingAssociationFields)
            data.Add(field, maxLength);
        return data;
    }

    [Theory]
    [MemberData(nameof(LearningComponentOfferingAssociationFieldTheoryData))]
    public async Task PutLearningComponentOfferingAssociation_FieldOverLengthLimit_ReturnsBadRequest(
        string fieldName, int maxLength)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        LearningComponentOfferingAssociation created = new() { AssociationIdValue = id };
        typeof(LearningComponentOfferingAssociation).GetProperty(fieldName)!
            .SetValue(created, ValueOfLength(fieldName, maxLength + 1));

        HttpResponseMessage response =
            await client.PutAsJsonAsync($"/learning-component-offering-associations/{id}", created);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [MemberData(nameof(LearningComponentOfferingAssociationFieldTheoryData))]
    public async Task PutLearningComponentOfferingAssociation_FieldAtExactLengthLimit_Succeeds(
        string fieldName, int maxLength)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        LearningComponentOfferingAssociation created = new() { AssociationIdValue = id };
        typeof(LearningComponentOfferingAssociation).GetProperty(fieldName)!
            .SetValue(created, ValueOfLength(fieldName, maxLength));

        HttpResponseMessage response =
            await client.PutAsJsonAsync($"/learning-component-offering-associations/{id}", created);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    // --- Task 3.11: ProgrammeOfferingAssociationEntity's own resource-specific fields -------------------
    //
    // Identical shape to CourseOfferingAssociation (3.9): ProgrammeOfferingAssociationIdValue not
    // write-reachable (route overwrite on PUT; server-generated Guid on POST external/me),
    // PrimaryCodeType/PrimaryCode already covered, ConsumerKey not write-reachable, ConsumerJson
    // unbounded. Two write-reachable model classes again - ProgrammeOfferingAssociation (PUT /{id}) and
    // ProgrammeOfferingAssociationExternalMeRequest (POST /external/me) - both needed the attributes,
    // per the 3.9 lesson. Role/StartDateTime/ExpectedEndDateTime/ActualEndDateTime/State/RemoteState all
    // already validated at the same limits for CourseOfferingAssociation (3.9); Role's [StringLength(64)]
    // confirmed consistent with the whole Association family per 3.9's finding.

    private static readonly (string Field, int MaxLength)[] ProgrammeOfferingAssociationFields =
    [
        (nameof(ProgrammeOfferingAssociation.Role), 64),
        (nameof(ProgrammeOfferingAssociation.StartDateTime), 256),
        (nameof(ProgrammeOfferingAssociation.ExpectedEndDateTime), 256),
        (nameof(ProgrammeOfferingAssociation.ActualEndDateTime), 256),
        (nameof(ProgrammeOfferingAssociation.State), 256), (nameof(ProgrammeOfferingAssociation.RemoteState), 256)
    ];

    public static TheoryData<string, int> ProgrammeOfferingAssociationFieldTheoryData()
    {
        TheoryData<string, int> data = [];
        foreach ((string field, int maxLength) in ProgrammeOfferingAssociationFields) data.Add(field, maxLength);
        return data;
    }

    [Theory]
    [MemberData(nameof(ProgrammeOfferingAssociationFieldTheoryData))]
    public async Task PutProgrammeOfferingAssociation_FieldOverLengthLimit_ReturnsBadRequest(
        string fieldName, int maxLength)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        ProgrammeOfferingAssociation created = new() { AssociationIdValue = id };
        typeof(ProgrammeOfferingAssociation).GetProperty(fieldName)!
            .SetValue(created, ValueOfLength(fieldName, maxLength + 1));

        HttpResponseMessage response = await client.PutAsJsonAsync($"/programme-offering-associations/{id}", created);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [MemberData(nameof(ProgrammeOfferingAssociationFieldTheoryData))]
    public async Task PutProgrammeOfferingAssociation_FieldAtExactLengthLimit_Succeeds(
        string fieldName, int maxLength)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        ProgrammeOfferingAssociation created = new() { AssociationIdValue = id };
        typeof(ProgrammeOfferingAssociation).GetProperty(fieldName)!.SetValue(created, ValueOfLength(fieldName, maxLength));

        HttpResponseMessage response = await client.PutAsJsonAsync($"/programme-offering-associations/{id}", created);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task PostProgrammeOfferingAssociationExternalMe_RoleOverLengthLimit_ReturnsBadRequest()
    {
        string personId = Guid.NewGuid().ToString();
        string programmeOfferingId = Guid.NewGuid().ToString();
        string issuerId = Guid.NewGuid().ToString();
        using WebApplicationFactory<Program> factory = CreateAuthenticatedFactory(personId);
        using HttpClient client = factory.CreateClient();
        await client.PutAsJsonAsync($"/persons/{personId}",
            new Person
            {
                PersonIdValue = personId,
                PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"POAEM-{personId[..8]}" },
                Surname = "FieldLengthTestPerson"
            });
        await client.PutAsJsonAsync($"/programme-offerings/{programmeOfferingId}", new ProgrammeOffering
        {
            ProgrammeOfferingIdValue = programmeOfferingId,
            PrimaryCode = new IdentifierEntry
            { CodeType = "identifier", Code = $"POAEM-PO-{programmeOfferingId[..8]}" },
            Name = [new LanguageTypedString { Language = "en", Value = "Field Length Test Offering" }]
        });
        await client.PutAsJsonAsync($"/organisations/{issuerId}", new Organisation
        {
            OrganisationId = issuerId,
            PrimaryCode = new IdentifierEntry { CodeType = "organisation_id", Code = $"POAEM-ORG-{issuerId[..8]}" },
            OrganisationType = "root",
            Name = [new LanguageTypedString { Language = "en", Value = "Field Length Test Issuer" }]
        });

        HttpResponseMessage response = await client.PostAsJsonAsync("/programme-offering-associations/external/me",
            new ProgrammeOfferingAssociationExternalMeRequest
            {
                Role = "x-" + new string('y', 63), // 65 chars, one over the 64 limit
                State = "pending",
                ProgrammeOfferingId = new Identifier { Value = programmeOfferingId },
                Issuer = new Organisation
                {
                    OrganisationId = issuerId,
                    PrimaryCode = new IdentifierEntry
                    { CodeType = "organisation_id", Code = $"POAEM-ORG-{issuerId[..8]}" },
                    OrganisationType = "root",
                    Name = [new LanguageTypedString { Language = "en", Value = "Field Length Test Issuer" }]
                }
            });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PostProgrammeOfferingAssociationExternalMe_RoleAtExactLengthLimit_Succeeds()
    {
        string personId = Guid.NewGuid().ToString();
        string programmeOfferingId = Guid.NewGuid().ToString();
        string issuerId = Guid.NewGuid().ToString();
        using WebApplicationFactory<Program> factory = CreateAuthenticatedFactory(personId);
        using HttpClient client = factory.CreateClient();
        await client.PutAsJsonAsync($"/persons/{personId}",
            new Person
            {
                PersonIdValue = personId,
                PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"POAEM-{personId[..8]}" },
                Surname = "FieldLengthTestPerson"
            });
        await client.PutAsJsonAsync($"/programme-offerings/{programmeOfferingId}", new ProgrammeOffering
        {
            ProgrammeOfferingIdValue = programmeOfferingId,
            PrimaryCode = new IdentifierEntry
            { CodeType = "identifier", Code = $"POAEM-PO-{programmeOfferingId[..8]}" },
            Name = [new LanguageTypedString { Language = "en", Value = "Field Length Test Offering" }]
        });
        await client.PutAsJsonAsync($"/organisations/{issuerId}", new Organisation
        {
            OrganisationId = issuerId,
            PrimaryCode = new IdentifierEntry { CodeType = "organisation_id", Code = $"POAEM-ORG-{issuerId[..8]}" },
            OrganisationType = "root",
            Name = [new LanguageTypedString { Language = "en", Value = "Field Length Test Issuer" }]
        });

        HttpResponseMessage response = await client.PostAsJsonAsync("/programme-offering-associations/external/me",
            new ProgrammeOfferingAssociationExternalMeRequest
            {
                Role = "x-" + new string('y', 62), // exactly 64 chars
                State = "pending",
                ProgrammeOfferingId = new Identifier { Value = programmeOfferingId },
                Issuer = new Organisation
                {
                    OrganisationId = issuerId,
                    PrimaryCode = new IdentifierEntry
                    { CodeType = "organisation_id", Code = $"POAEM-ORG-{issuerId[..8]}" },
                    OrganisationType = "root",
                    Name = [new LanguageTypedString { Language = "en", Value = "Field Length Test Issuer" }]
                }
            });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    // --- Task 3.12: TestComponentOfferingAssociationEntity's own resource-specific fields ---------------
    //
    // Identical shape to LearningComponentOfferingAssociation (3.10): TestComponentOfferingAssociationIdValue
    // not write-reachable (route overwrite), PrimaryCodeType/PrimaryCode already covered, ConsumerKey
    // not write-reachable, ConsumerJson unbounded. Only one write path (PUT /{id}) - the other PUT on
    // this controller targets the nested test-component-offering-association-attempt sub-resource
    // (task 3.13's TestComponentOfferingAssociationAttemptEntity, a different entity), not this one. All
    // 7 fields already validated at the same limits established for CourseOfferingAssociation (3.9) /
    // LearningComponentOfferingAssociation (3.10): Role/Attendance both [StringLength(64)], matching
    // the 3.10 note's "2-vs-1" observation (this entity's Attendance is one of the two 64-length ones).

    private static readonly (string Field, int MaxLength)[] TestComponentOfferingAssociationFields =
    [
        (nameof(TestComponentOfferingAssociation.Role), 64),
        (nameof(TestComponentOfferingAssociation.Attendance), 64),
        (nameof(TestComponentOfferingAssociation.StartDateTime), 256),
        (nameof(TestComponentOfferingAssociation.ExpectedEndDateTime), 256),
        (nameof(TestComponentOfferingAssociation.ActualEndDateTime), 256),
        (nameof(TestComponentOfferingAssociation.State), 256),
        (nameof(TestComponentOfferingAssociation.RemoteState), 256)
    ];

    public static TheoryData<string, int> TestComponentOfferingAssociationFieldTheoryData()
    {
        TheoryData<string, int> data = [];
        foreach ((string field, int maxLength) in TestComponentOfferingAssociationFields) data.Add(field, maxLength);
        return data;
    }

    [Theory]
    [MemberData(nameof(TestComponentOfferingAssociationFieldTheoryData))]
    public async Task PutTestComponentOfferingAssociation_FieldOverLengthLimit_ReturnsBadRequest(
        string fieldName, int maxLength)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        TestComponentOfferingAssociation created = new() { AssociationIdValue = id };
        typeof(TestComponentOfferingAssociation).GetProperty(fieldName)!
            .SetValue(created, ValueOfLength(fieldName, maxLength + 1));

        HttpResponseMessage response =
            await client.PutAsJsonAsync($"/test-component-offering-associations/{id}", created);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [MemberData(nameof(TestComponentOfferingAssociationFieldTheoryData))]
    public async Task PutTestComponentOfferingAssociation_FieldAtExactLengthLimit_Succeeds(
        string fieldName, int maxLength)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        TestComponentOfferingAssociation created = new() { AssociationIdValue = id };
        typeof(TestComponentOfferingAssociation).GetProperty(fieldName)!
            .SetValue(created, ValueOfLength(fieldName, maxLength));

        HttpResponseMessage response =
            await client.PutAsJsonAsync($"/test-component-offering-associations/{id}", created);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    // --- Task 3.13: TestComponentOfferingAssociationAttemptEntity's own resource-specific fields --------
    //
    // No mapping-extensions file for this entity - write logic lives inline in
    // TestComponentOfferingAssociationAttemptsController.UpdateEntityFromApiModel, confirmed via direct
    // read. AttemptId (the resource's own id) not write-reachable (route id used instead, model.AttemptId
    // never read); ConsumerKey not write-reachable (orphaned); ConsumerJson unbounded. `Attempt` is an
    // int, not a string field. The other 6 string fields all needed [StringLength]. Route is the spec's
    // singular "test-component-offering-associations-attempt", not the plural used by the nested list
    // endpoint - confirmed from the controller's own [Route] attribute, not the [controller] convention.
    //
    // Resolves the open question from 3.10/3.12: `Attendance` here is [StringLength(256)], looking like
    // a 3rd, inconsistent instance of the same field those tasks validated at 64. It isn't - this one
    // carries [ExtensibleEnum("attendance")], a genuinely different spec enum
    // (source/enumerations/attendance.yaml) from the other two entities'
    // [ExtensibleEnum("associationAttendance")] (source/enumerations/associationAttendance.yaml). Same
    // property name by coincidence of the C# binding, different spec field. Neither schema declares a
    // maxLength, so there's no spec conflict either way; 256 here just matches this codebase's ordinary
    // extensible-enum convention (same as State's [ExtensibleEnum("attemptState")], also 256) rather than
    // the narrower 64 the Association family's Role/Attendance share. Validated at the entity's own 256,
    // no migration needed - there was never an actual inconsistency, just two same-named fields from
    // different schemas.

    private static readonly (string Field, int MaxLength)[] TestComponentOfferingAssociationAttemptFields =
    [
        (nameof(TestComponentOfferingAssociationAttempt.Opportunity), 256),
        (nameof(TestComponentOfferingAssociationAttempt.State), 256),
        (nameof(TestComponentOfferingAssociationAttempt.StartDateTime), 256),
        (nameof(TestComponentOfferingAssociationAttempt.EndDateTime), 256),
        (nameof(TestComponentOfferingAssociationAttempt.Attendance), 256),
        (nameof(TestComponentOfferingAssociationAttempt.Irregularities), 2048)
    ];

    public static TheoryData<string, int> TestComponentOfferingAssociationAttemptFieldTheoryData()
    {
        TheoryData<string, int> data = [];
        foreach ((string field, int maxLength) in TestComponentOfferingAssociationAttemptFields)
            data.Add(field, maxLength);
        return data;
    }

    [Theory]
    [MemberData(nameof(TestComponentOfferingAssociationAttemptFieldTheoryData))]
    public async Task PutTestComponentOfferingAssociationAttempt_FieldOverLengthLimit_ReturnsBadRequest(
        string fieldName, int maxLength)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        TestComponentOfferingAssociationAttempt created = new() { AttemptId = id };
        typeof(TestComponentOfferingAssociationAttempt).GetProperty(fieldName)!
            .SetValue(created, ValueOfLength(fieldName, maxLength + 1));

        HttpResponseMessage response =
            await client.PutAsJsonAsync($"/test-component-offering-associations-attempt/{id}", created);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [MemberData(nameof(TestComponentOfferingAssociationAttemptFieldTheoryData))]
    public async Task PutTestComponentOfferingAssociationAttempt_FieldAtExactLengthLimit_Succeeds(
        string fieldName, int maxLength)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        TestComponentOfferingAssociationAttempt created = new() { AttemptId = id };
        typeof(TestComponentOfferingAssociationAttempt).GetProperty(fieldName)!
            .SetValue(created, ValueOfLength(fieldName, maxLength));

        HttpResponseMessage response =
            await client.PutAsJsonAsync($"/test-component-offering-associations-attempt/{id}", created);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    // --- Found during the 5.2 test-coverage review: AssociationPatchRequest.RemoteState -----------------
    //
    // A genuine gap the task 3.9-3.12 audits never covered: PATCH /course-offering-associations/{id} (and
    // the same PATCH endpoint on the other 3 offering-association resources) binds to
    // AssociationPatchRequest, not the PUT model - a distinct write path this change's per-entity audit
    // never looked at. Its RemoteState is written straight to entity.RemoteState
    // (CourseOfferingAssociationsController.PatchCourseOfferingAssociation and its 3 siblings all do
    // `if (request.RemoteState != null) entity.RemoteState = request.RemoteState;`), the exact same
    // [StringLength(256)] column the PUT-path tests already protect - so an over-length remoteState via
    // PATCH hit the identical truncation crash this whole change exists to fix. Fixed by adding
    // [StringLength(256)] to AssociationPatchRequest.RemoteState itself.
    //
    // AssociationPatchRequest is the literal same class for all 4 controllers (see its own doc comment) -
    // ASP.NET's automatic model validation runs on the DTO type before any controller action code
    // executes, so testing via one endpoint proves the attribute for all 4; re-testing per controller
    // would only be exercising routing, not this change's concern. Uses the same PatchMergeAsync pattern
    // as PatchAssociationConformanceTests.cs (application/merge-patch+json is required - a plain
    // application/json PATCH is rejected with 415 before validation ever runs, so PatchAsJsonAsync can't
    // be used here).

    private static async Task<HttpResponseMessage> PatchMergeAsync(HttpClient client, string path,
        AssociationPatchRequest request)
    {
        using StringContent content = new(JsonSerializer.Serialize(request));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/merge-patch+json");
        return await client.PatchAsync(path, content);
    }

    [Fact]
    public async Task PatchCourseOfferingAssociation_RemoteStateOverLengthLimit_ReturnsBadRequest()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        await client.PutAsJsonAsync($"/course-offering-associations/{id}", new CourseOfferingAssociation
        {
            AssociationIdValue = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"COAP-{id[..8]}" },
            Role = "student",
            State = "pending"
        });

        HttpResponseMessage response = await PatchMergeAsync(client, $"/course-offering-associations/{id}",
            new AssociationPatchRequest { RemoteState = "x-" + new string('y', 255) }); // 257 chars

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PatchCourseOfferingAssociation_RemoteStateAtExactLengthLimit_Succeeds()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        await client.PutAsJsonAsync($"/course-offering-associations/{id}", new CourseOfferingAssociation
        {
            AssociationIdValue = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"COAP-{id[..8]}" },
            Role = "student",
            State = "pending"
        });

        HttpResponseMessage response = await PatchMergeAsync(client, $"/course-offering-associations/{id}",
            new AssociationPatchRequest { RemoteState = "x-" + new string('y', 254) }); // exactly 256 chars

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
