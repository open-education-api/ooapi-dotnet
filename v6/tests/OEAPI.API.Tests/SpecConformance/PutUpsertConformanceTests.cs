using System.Net;
using System.Net.Http.Json;
using OEAPI.API.Tests.Infrastructure;
using OEAPI.Core.Models.ApiModels;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Covers real <c>PUT</c> upsert semantics, not just status codes. For
///     the 11 core resources with a spec-required <c>PUT</c> (organisations, persons, groups, the 4
///     "-offerings" resources, and the 4 "-associations" resources), does a genuine create-then-replace
///     round trip against a fresh id - first <c>PUT</c> must be <c>201</c> and the created data must
///     read back correctly, second <c>PUT</c> (same id, changed field) must be <c>200</c> and the
///     change must be visible on a subsequent <c>GET</c>. FK reference fields (<c>organisationId</c>
///     etc.) are deliberately omitted from the bodies here - every PUT controller resolves them
///     best-effort (silently skipped if the referenced entity doesn't exist, confirmed by reading the
///     controllers directly), so they're not required for a real upsert round trip and adding seeded
///     prerequisite data for all of them would be a lot of unnecessary setup for what this phase is
///     checking. `groups/{id}/memberships/{personId}` and the `test-component-offering-associations
///     -attempt` resource are separate, more involved upsert contracts, tracked as their own follow-up
///     rather than force-fit into this same pattern.
/// </summary>
[Collection(SqlServerCollection.Name)]
public class PutUpsertConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Fact]
    public async Task PutOrganisation_CreateThenReplace_UpsertsCorrectly()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        Organisation created = new()
        {
            OrganisationId = id,
            PrimaryCode = new IdentifierEntry { CodeType = "organisation_id", Code = "ORG-C-1" },
            OrganisationType = "institute",
            Name = [new LanguageTypedString { Language = "en", Value = "Phase C Org" }]
        };

        HttpResponseMessage createResponse = await client.PutAsJsonAsync($"/organisations/{id}", created);
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        Organisation? afterCreate = await client.GetFromJsonAsync<Organisation>($"/organisations/{id}");
        Assert.Equal("Phase C Org", afterCreate!.Name.Single().Value);

        created.Name = [new LanguageTypedString { Language = "en", Value = "Phase C Org Renamed" }];
        HttpResponseMessage replaceResponse = await client.PutAsJsonAsync($"/organisations/{id}", created);
        Assert.Equal(HttpStatusCode.OK, replaceResponse.StatusCode);

        Organisation? afterReplace = await client.GetFromJsonAsync<Organisation>($"/organisations/{id}");
        Assert.Equal("Phase C Org Renamed", afterReplace!.Name.Single().Value);
    }

    [Fact]
    public async Task PutPerson_CreateThenReplace_UpsertsCorrectly()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        Person created = new()
        {
            PersonIdValue = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"PC-{id[..8]}" },
            GivenName = "Ada",
            Surname = "Lovelace"
        };

        HttpResponseMessage createResponse = await client.PutAsJsonAsync($"/persons/{id}", created);
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        Person? afterCreate = await client.GetFromJsonAsync<Person>($"/persons/{id}");
        Assert.Equal("Lovelace", afterCreate!.Surname);

        created.Surname = "King";
        HttpResponseMessage replaceResponse = await client.PutAsJsonAsync($"/persons/{id}", created);
        Assert.Equal(HttpStatusCode.OK, replaceResponse.StatusCode);

        Person? afterReplace = await client.GetFromJsonAsync<Person>($"/persons/{id}");
        Assert.Equal("King", afterReplace!.Surname);
    }

    [Fact]
    public async Task PutPerson_NoPrimaryCode_SucceedsWithoutValidationError()
    {
        // Regression test: primaryCode is optional per spec (not in Person's own required list).
        // Omitting it used to leave a default-constructed IdentifierEntry with an empty codeType,
        // which [ExtensibleEnum] then rejected as "'' is not a recognised value" - a confusing 400
        // for a field the client never sent.
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        HttpResponseMessage response = await client.PutAsJsonAsync($"/persons/{id}", new Person
        {
            PersonIdValue = id,
            GivenName = "No",
            Surname = "PrimaryCode"
        });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task PutPerson_NoNameFieldAtAll_ReturnsBadRequest()
    {
        // Regression test: PersonProperties' spec schema (shared by Person) requires at least one of
        // surname/givenName/preferredName via its own anyOf - without IValidatableObject enforcing
        // this, a client could create a person whose own GET response then fails that same schema.
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        HttpResponseMessage response = await client.PutAsJsonAsync($"/persons/{id}", new Person
        {
            PersonIdValue = id
        });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PutGroup_CreateThenReplace_UpsertsCorrectly()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        Group created = new()
        {
            GroupIdValue = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"GC-{id[..8]}" },
            GroupType = "class",
            Name = [new LanguageTypedString { Language = "en", Value = "Phase C Group" }]
        };

        HttpResponseMessage createResponse = await client.PutAsJsonAsync($"/groups/{id}", created);
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        Group? afterCreate = await client.GetFromJsonAsync<Group>($"/groups/{id}");
        Assert.Equal("Phase C Group", afterCreate!.Name.Single().Value);

        created.Name = [new LanguageTypedString { Language = "en", Value = "Phase C Group Renamed" }];
        HttpResponseMessage replaceResponse = await client.PutAsJsonAsync($"/groups/{id}", created);
        Assert.Equal(HttpStatusCode.OK, replaceResponse.StatusCode);

        Group? afterReplace = await client.GetFromJsonAsync<Group>($"/groups/{id}");
        Assert.Equal("Phase C Group Renamed", afterReplace!.Name.Single().Value);
    }

    [Fact]
    public async Task PutCourseOffering_CreateThenReplace_UpsertsCorrectly()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        CourseOffering created = new()
        {
            CourseOfferingIdValue = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"COC-{id[..8]}" },
            Name = [new LanguageTypedString { Language = "en", Value = "Phase C Course Offering" }]
        };

        HttpResponseMessage createResponse = await client.PutAsJsonAsync($"/course-offerings/{id}", created);
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        CourseOffering? afterCreate = await client.GetFromJsonAsync<CourseOffering>($"/course-offerings/{id}");
        Assert.Equal("Phase C Course Offering", afterCreate!.Name.Single().Value);

        created.Name = [new LanguageTypedString { Language = "en", Value = "Phase C Course Offering Renamed" }];
        HttpResponseMessage replaceResponse = await client.PutAsJsonAsync($"/course-offerings/{id}", created);
        Assert.Equal(HttpStatusCode.OK, replaceResponse.StatusCode);

        CourseOffering? afterReplace = await client.GetFromJsonAsync<CourseOffering>($"/course-offerings/{id}");
        Assert.Equal("Phase C Course Offering Renamed", afterReplace!.Name.Single().Value);
    }

    [Fact]
    public async Task PutProgrammeOffering_CreateThenReplace_UpsertsCorrectly()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        ProgrammeOffering created = new()
        {
            ProgrammeOfferingIdValue = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"POC-{id[..8]}" },
            Name = [new LanguageTypedString { Language = "en", Value = "Phase C Programme Offering" }]
        };

        HttpResponseMessage createResponse = await client.PutAsJsonAsync($"/programme-offerings/{id}", created);
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        ProgrammeOffering? afterCreate = await client.GetFromJsonAsync<ProgrammeOffering>($"/programme-offerings/{id}");
        Assert.Equal("Phase C Programme Offering", afterCreate!.Name.Single().Value);

        created.Name = [new LanguageTypedString { Language = "en", Value = "Phase C Programme Offering Renamed" }];
        HttpResponseMessage replaceResponse = await client.PutAsJsonAsync($"/programme-offerings/{id}", created);
        Assert.Equal(HttpStatusCode.OK, replaceResponse.StatusCode);

        ProgrammeOffering? afterReplace =
            await client.GetFromJsonAsync<ProgrammeOffering>($"/programme-offerings/{id}");
        Assert.Equal("Phase C Programme Offering Renamed", afterReplace!.Name.Single().Value);
    }

    [Fact]
    public async Task PutLearningComponentOffering_CreateThenReplace_UpsertsCorrectly()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        LearningComponentOffering created = new()
        {
            LearningComponentOfferingIdValue = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"LCOC-{id[..8]}" },
            Name = [new LanguageTypedString { Language = "en", Value = "Phase C LC Offering" }]
        };

        HttpResponseMessage createResponse =
            await client.PutAsJsonAsync($"/learning-component-offerings/{id}", created);
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        LearningComponentOffering? afterCreate =
            await client.GetFromJsonAsync<LearningComponentOffering>($"/learning-component-offerings/{id}");
        Assert.Equal("Phase C LC Offering", afterCreate!.Name.Single().Value);

        created.Name = [new LanguageTypedString { Language = "en", Value = "Phase C LC Offering Renamed" }];
        HttpResponseMessage replaceResponse =
            await client.PutAsJsonAsync($"/learning-component-offerings/{id}", created);
        Assert.Equal(HttpStatusCode.OK, replaceResponse.StatusCode);

        LearningComponentOffering? afterReplace =
            await client.GetFromJsonAsync<LearningComponentOffering>($"/learning-component-offerings/{id}");
        Assert.Equal("Phase C LC Offering Renamed", afterReplace!.Name.Single().Value);
    }

    [Fact]
    public async Task PutTestComponentOffering_CreateThenReplace_UpsertsCorrectly()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        TestComponentOffering created = new()
        {
            TestComponentOfferingIdValue = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"TCOC-{id[..8]}" },
            Name = [new LanguageTypedString { Language = "en", Value = "Phase C TC Offering" }]
        };

        HttpResponseMessage createResponse = await client.PutAsJsonAsync($"/test-component-offerings/{id}", created);
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        TestComponentOffering? afterCreate =
            await client.GetFromJsonAsync<TestComponentOffering>($"/test-component-offerings/{id}");
        Assert.Equal("Phase C TC Offering", afterCreate!.Name.Single().Value);

        created.Name = [new LanguageTypedString { Language = "en", Value = "Phase C TC Offering Renamed" }];
        HttpResponseMessage replaceResponse = await client.PutAsJsonAsync($"/test-component-offerings/{id}", created);
        Assert.Equal(HttpStatusCode.OK, replaceResponse.StatusCode);

        TestComponentOffering? afterReplace =
            await client.GetFromJsonAsync<TestComponentOffering>($"/test-component-offerings/{id}");
        Assert.Equal("Phase C TC Offering Renamed", afterReplace!.Name.Single().Value);
    }

    [Fact]
    public async Task PutCourseOfferingAssociation_CreateThenReplace_UpsertsCorrectly()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        CourseOfferingAssociation created = new()
        {
            AssociationIdValue = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"COAC-{id[..8]}" },
            Role = "student",
            State = "pending"
        };

        HttpResponseMessage createResponse =
            await client.PutAsJsonAsync($"/course-offering-associations/{id}", created);
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        CourseOfferingAssociation? afterCreate =
            await client.GetFromJsonAsync<CourseOfferingAssociation>($"/course-offering-associations/{id}");
        Assert.Equal("pending", afterCreate!.State);

        created.State = "associated";
        HttpResponseMessage replaceResponse =
            await client.PutAsJsonAsync($"/course-offering-associations/{id}", created);
        Assert.Equal(HttpStatusCode.OK, replaceResponse.StatusCode);

        CourseOfferingAssociation? afterReplace =
            await client.GetFromJsonAsync<CourseOfferingAssociation>($"/course-offering-associations/{id}");
        Assert.Equal("associated", afterReplace!.State);
    }

    [Fact]
    public async Task PutCourseOfferingAssociation_NoPrimaryCode_SucceedsWithoutValidationError()
    {
        // Regression test: primaryCode is optional per spec for all 4 "-association" resources (not
        // in their own required list). Omitting it used to leave a default-constructed
        // IdentifierEntry with an empty codeType, which [ExtensibleEnum] then rejected as "'' is not
        // a recognised value" - a confusing 400 for a field the client never sent. Representative of
        // the identical mapping-extension pattern shared by all 4 "-association" resources.
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        HttpResponseMessage response = await client.PutAsJsonAsync($"/course-offering-associations/{id}",
            new CourseOfferingAssociation
            {
                AssociationIdValue = id,
                Role = "student",
                State = "pending"
            });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task PutGroupMembership_MinimalBody_SucceedsWithoutValidationError()
    {
        // Regression test for GroupsController's own inline membership upsert path (distinct code
        // from the mapping-extension-based association resources above) - Membership.yaml declares
        // no primaryCode at all (see tier3), so only role/state (its required fields) are needed here.
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string groupId = Guid.NewGuid().ToString();
        string personId = Guid.NewGuid().ToString();

        await client.PutAsJsonAsync($"/groups/{groupId}", new Group
        {
            GroupIdValue = groupId,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"GRP-{groupId[..8]}" },
            GroupType = "class"
        });
        await client.PutAsJsonAsync($"/persons/{personId}", new Person
        {
            PersonIdValue = personId,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"PER-{personId[..8]}" },
            Surname = "MembershipTestPerson"
        });

        HttpResponseMessage response = await client.PutAsJsonAsync($"/groups/{groupId}/memberships/{personId}",
            new Membership { Role = "student", State = "active" });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task PutProgrammeOfferingAssociation_CreateThenReplace_UpsertsCorrectly()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        ProgrammeOfferingAssociation created = new()
        {
            AssociationIdValue = id,
            PrimaryCode = new IdentifierEntry
            { CodeType = "identifier", Code = $"POAC-{id[..8]}" },
            Role = "student",
            State = "pending"
        };

        HttpResponseMessage createResponse =
            await client.PutAsJsonAsync($"/programme-offering-associations/{id}", created);
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        ProgrammeOfferingAssociation? afterCreate =
            await client.GetFromJsonAsync<ProgrammeOfferingAssociation>($"/programme-offering-associations/{id}");
        Assert.Equal("pending", afterCreate!.State);

        created.State = "associated";
        HttpResponseMessage replaceResponse =
            await client.PutAsJsonAsync($"/programme-offering-associations/{id}", created);
        Assert.Equal(HttpStatusCode.OK, replaceResponse.StatusCode);

        ProgrammeOfferingAssociation? afterReplace =
            await client.GetFromJsonAsync<ProgrammeOfferingAssociation>($"/programme-offering-associations/{id}");
        Assert.Equal("associated", afterReplace!.State);
    }

    [Fact]
    public async Task PutLearningComponentOfferingAssociation_CreateThenReplace_UpsertsCorrectly()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        LearningComponentOfferingAssociation created = new()
        {
            AssociationIdValue = id,
            PrimaryCode = new IdentifierEntry
            { CodeType = "identifier", Code = $"LCOAC-{id[..8]}" },
            Role = "student",
            State = "pending"
        };

        HttpResponseMessage createResponse =
            await client.PutAsJsonAsync($"/learning-component-offering-associations/{id}", created);
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        LearningComponentOfferingAssociation? afterCreate =
            await client.GetFromJsonAsync<LearningComponentOfferingAssociation>(
                $"/learning-component-offering-associations/{id}");
        Assert.Equal("pending", afterCreate!.State);

        created.State = "associated";
        HttpResponseMessage replaceResponse =
            await client.PutAsJsonAsync($"/learning-component-offering-associations/{id}", created);
        Assert.Equal(HttpStatusCode.OK, replaceResponse.StatusCode);

        LearningComponentOfferingAssociation? afterReplace =
            await client.GetFromJsonAsync<LearningComponentOfferingAssociation>(
                $"/learning-component-offering-associations/{id}");
        Assert.Equal("associated", afterReplace!.State);
    }

    [Fact]
    public async Task PutTestComponentOfferingAssociation_CreateThenReplace_UpsertsCorrectly()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        TestComponentOfferingAssociation created = new()
        {
            AssociationIdValue = id,
            PrimaryCode = new IdentifierEntry
            { CodeType = "identifier", Code = $"TCOAC-{id[..8]}" },
            Role = "student",
            State = "pending"
        };

        HttpResponseMessage createResponse =
            await client.PutAsJsonAsync($"/test-component-offering-associations/{id}", created);
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        TestComponentOfferingAssociation? afterCreate =
            await client.GetFromJsonAsync<TestComponentOfferingAssociation>(
                $"/test-component-offering-associations/{id}");
        Assert.Equal("pending", afterCreate!.State);

        created.State = "associated";
        HttpResponseMessage replaceResponse =
            await client.PutAsJsonAsync($"/test-component-offering-associations/{id}", created);
        Assert.Equal(HttpStatusCode.OK, replaceResponse.StatusCode);

        TestComponentOfferingAssociation? afterReplace =
            await client.GetFromJsonAsync<TestComponentOfferingAssociation>(
                $"/test-component-offering-associations/{id}");
        Assert.Equal("associated", afterReplace!.State);
    }
}
