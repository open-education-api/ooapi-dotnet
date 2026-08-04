using System.Text.Json.Nodes;
using Microsoft.Extensions.DependencyInjection;
using OEAPI.API.Tests.Infrastructure;
using OEAPI.Infrastructure.Data.Context;
using OEAPI.Infrastructure.Data.Entities;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Confirms the fix-spec-conformance-tier3-breaking-wire-changes removals/renames actually took
///     effect on the wire: the 4 offering-association types serialize their id under
///     <c>associationId</c> and never emit <c>organisationId</c>/<c>organisation</c>;
///     <c>Address</c>/<c>Membership</c>/<c>LearningOutcome</c> never emit their removed fields;
///     <c>ProgrammeOffering</c> never emits <c>academicSessionId</c>/<c>academicSession</c>. Follows
///     the same seed-via-<see cref="SqlServerOEAPIDbContext" />-then-real-HTTP-request pattern as
///     <see cref="Tier2BindingBugConformanceTests" />.
/// </summary>
[Collection(SqlServerCollection.Name)]
public class Tier3BreakingWireChangesConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Fact]
    public async Task CourseOfferingAssociationById_UsesAssociationId_NeverOrganisation()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid orgId = Guid.CreateVersion7();
        Guid personId = Guid.CreateVersion7();
        Guid courseId = Guid.CreateVersion7();
        Guid courseOfferingId = Guid.CreateVersion7();
        Guid assocId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            OrganisationEntity org = new()
            {
                Id = orgId,
                OrganisationId = orgId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T3-ORG-{orgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier3 Org\"}]"
            };
            dbContext.Organisations.Add(org);

            PersonEntity person = new()
            {
                Id = personId,
                PersonId = personId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T3-PER-{personId.ToString()[..8]}",
                GivenName = "Tier3",
                Surname = "Person"
            };
            dbContext.Persons.Add(person);

            CourseEntity course = new()
            {
                Id = courseId,
                CourseId = courseId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T3-CRS-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier3 Course\"}]"
            };
            dbContext.Courses.Add(course);

            CourseOfferingEntity courseOffering = new()
            {
                Id = courseOfferingId,
                CourseOfferingId = courseOfferingId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T3-CO-{courseOfferingId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier3 Offering\"}]",
                CourseEntityId = course.Id,
                OrganisationEntityId = org.Id
            };
            dbContext.CourseOfferings.Add(courseOffering);

            CourseOfferingAssociationEntity association = new()
            {
                Id = assocId,
                CourseOfferingAssociationIdValue = assocId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T3-COA-{assocId.ToString()[..8]}",
                Role = "student",
                CourseOfferingEntityId = courseOffering.Id,
                PersonEntityId = person.Id,
                OrganisationEntityId = org.Id
            };
            dbContext.CourseOfferingAssociations.Add(association);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        JsonObject json = await GetJsonAsync(client, $"/course-offering-associations/{assocId}");
        Assert.Equal(assocId.ToString(), json["associationId"]!.GetValue<string>());
        Assert.False(json.ContainsKey("courseOfferingAssociationId"));
        Assert.False(json.ContainsKey("organisationId"));
        Assert.False(json.ContainsKey("organisation"));
    }

    [Fact]
    public async Task TestComponentOfferingAssociationById_UsesAssociationId_NeverOrganisation()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid orgId = Guid.CreateVersion7();
        Guid personId = Guid.CreateVersion7();
        Guid testComponentId = Guid.CreateVersion7();
        Guid testComponentOfferingId = Guid.CreateVersion7();
        Guid assocId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            OrganisationEntity org = new()
            {
                Id = orgId,
                OrganisationId = orgId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T3-ORG2-{orgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier3 Org2\"}]"
            };
            dbContext.Organisations.Add(org);

            PersonEntity person = new()
            {
                Id = personId,
                PersonId = personId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T3-PER2-{personId.ToString()[..8]}",
                GivenName = "Tier3",
                Surname = "Person2"
            };
            dbContext.Persons.Add(person);

            TestComponentEntity testComponent = new()
            {
                Id = testComponentId,
                ComponentId = testComponentId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T3-TC-{testComponentId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier3 Test Component\"}]",
                OrganisationEntityId = org.Id
            };
            dbContext.TestComponents.Add(testComponent);

            TestComponentOfferingEntity testComponentOffering = new()
            {
                Id = testComponentOfferingId,
                TestComponentOfferingIdValue = testComponentOfferingId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T3-TCO-{testComponentOfferingId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier3 TC Offering\"}]",
                TestComponentEntityId = testComponent.Id,
                OrganisationEntityId = org.Id
            };
            dbContext.TestComponentOfferings.Add(testComponentOffering);

            TestComponentOfferingAssociationEntity association = new()
            {
                Id = assocId,
                TestComponentOfferingAssociationIdValue = assocId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T3-TCOA-{assocId.ToString()[..8]}",
                Role = "student",
                TestComponentOfferingEntityId = testComponentOffering.Id,
                PersonEntityId = person.Id,
                OrganisationEntityId = org.Id
            };
            dbContext.TestComponentOfferingAssociations.Add(association);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        JsonObject json = await GetJsonAsync(client, $"/test-component-offering-associations/{assocId}");
        Assert.Equal(assocId.ToString(), json["associationId"]!.GetValue<string>());
        Assert.False(json.ContainsKey("testComponentOfferingAssociationId"));
        Assert.False(json.ContainsKey("organisationId"));
        Assert.False(json.ContainsKey("organisation"));
    }

    [Fact]
    public async Task GroupMembershipsByGroupId_PersonIdAndGroupId_ArePlainStrings_NoFabricatedFields()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid groupId = Guid.CreateVersion7();
        Guid personId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            GroupEntity group = new()
            {
                Id = groupId,
                GroupId = groupId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T3-GRP-{groupId.ToString()[..8]}",
                GroupType = "class"
            };
            dbContext.Groups.Add(group);

            PersonEntity person = new()
            {
                Id = personId,
                PersonId = personId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T3-PER3-{personId.ToString()[..8]}",
                GivenName = "Tier3",
                Surname = "MembershipPerson"
            };
            dbContext.Persons.Add(person);

            MembershipEntity membership = new()
            {
                MembershipIdValue = Guid.NewGuid().ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = "T3-MEM-1",
                Role = "student",
                State = "active",
                GroupId = group.Id,
                PersonId = person.Id
            };
            dbContext.Memberships.Add(membership);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        JsonObject json = await GetJsonAsync(client, $"/groups/{groupId}/memberships");
        JsonObject item = json["items"]!.AsArray()[0]!.AsObject();

        Assert.Equal(personId.ToString(), item["personId"]!.GetValue<string>());
        Assert.Equal(groupId.ToString(), item["groupId"]!.GetValue<string>());
        Assert.False(item.ContainsKey("membershipId"));
        Assert.False(item.ContainsKey("primaryCode"));
        Assert.False(item.ContainsKey("person"));
        Assert.False(item.ContainsKey("group"));
        Assert.False(item.ContainsKey("otherCodes"));
    }

    [Fact]
    public async Task LearningOutcomeById_NeverEmitsOrganisation_ComplexityLevelIsBareString()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid orgId = Guid.CreateVersion7();
        Guid learningOutcomeId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            OrganisationEntity org = new()
            {
                Id = orgId,
                OrganisationId = orgId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T3-ORG3-{orgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier3 Org3\"}]"
            };
            dbContext.Organisations.Add(org);

            LearningOutcomeEntity learningOutcome = new()
            {
                Id = learningOutcomeId,
                LearningOutcomeId = learningOutcomeId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T3-LO-{learningOutcomeId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier3 Outcome\"}]",
                OrganisationEntityId = org.Id,
                ComplexityLevel = "bloom_1"
            };
            dbContext.LearningOutcomes.Add(learningOutcome);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        // Even with expand=organisation requested (the path's own declared-but-unbacked enum value),
        // no organisation/organisationId field should ever appear.
        JsonObject json = await GetJsonAsync(client, $"/learning-outcomes/{learningOutcomeId}?expand=organisation");
        Assert.False(json.ContainsKey("organisationId"));
        Assert.False(json.ContainsKey("organisation"));
        Assert.Equal("bloom_1", json["complexityLevel"]!.GetValue<string>());
    }

    // OfferingProperties.yaml (composed via allOf into all 4 offering types, including
    // ProgrammeOffering) declares academicSessionId/academicSession, matching its 3 sibling
    // offering types.
    [Fact]
    public async Task ProgrammeOfferingById_EmitsAcademicSessionId_AndExpandsAcademicSession()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid orgId = Guid.CreateVersion7();
        Guid programmeId = Guid.CreateVersion7();
        Guid academicSessionId = Guid.CreateVersion7();
        Guid programmeOfferingId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            OrganisationEntity org = new()
            {
                Id = orgId,
                OrganisationId = orgId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T3-ORG4-{orgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier3 Org4\"}]"
            };
            dbContext.Organisations.Add(org);

            ProgrammeEntity programme = new()
            {
                Id = programmeId,
                ProgrammeId = programmeId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T3-PRG-{programmeId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier3 Programme\"}]",
                ProgrammeType = "programme"
            };
            dbContext.Programmes.Add(programme);

            AcademicSessionEntity academicSession = new()
            {
                Id = academicSessionId,
                AcademicSessionId = academicSessionId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T3-AS-{academicSessionId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier3 Session\"}]",
                AcademicSessionType = "semester",
                StartDateTime = "2025-09-01T00:00:00+01:00",
                EndDateTime = "2026-01-31T23:59:59+01:00"
            };
            dbContext.AcademicSessions.Add(academicSession);

            ProgrammeOfferingEntity programmeOffering = new()
            {
                Id = programmeOfferingId,
                ProgrammeOfferingIdValue = programmeOfferingId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T3-PO-{programmeOfferingId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier3 Programme Offering\"}]",
                ProgrammeEntityId = programme.Id,
                OrganisationEntityId = org.Id,
                AcademicSessionEntityId = academicSession.Id
            };
            dbContext.ProgrammeOfferings.Add(programmeOffering);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        JsonObject withoutExpand = await GetJsonAsync(client, $"/programme-offerings/{programmeOfferingId}");
        Assert.Equal(academicSessionId.ToString(), withoutExpand["academicSessionId"]!.GetValue<string>());
        Assert.False(withoutExpand.ContainsKey("academicSession"));

        JsonObject withExpand =
            await GetJsonAsync(client, $"/programme-offerings/{programmeOfferingId}?expand=academic_session");
        Assert.Equal(academicSessionId.ToString(), withExpand["academicSessionId"]!.GetValue<string>());
        Assert.Equal(academicSessionId.ToString(), withExpand["academicSession"]!["academicSessionId"]!.GetValue<string>());
    }

    [Fact]
    public async Task CourseById_EmbeddedAddress_NeverEmitsFabricatedFields()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid courseId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            CourseEntity course = new()
            {
                Id = courseId,
                CourseId = courseId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T3-CRS2-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier3 Course2\"}]",
                AddressesJson =
                    "[{\"addressType\":\"visit\",\"street\":\"Moreelsepark\",\"streetNumber\":\"48\"," +
                    "\"postCode\":\"3511 EP\",\"city\":\"Utrecht\",\"countryCode\":{\"iso3166-1-alpha2\":\"NL\"}," +
                    "\"geolocation\":{\"latitude\":52.089123,\"longitude\":5.113337}}]"
            };
            dbContext.Courses.Add(course);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        JsonObject json = await GetJsonAsync(client, $"/courses/{courseId}");
        JsonObject address = json["addresses"]!.AsArray()[0]!.AsObject();

        Assert.Equal("3511 EP", address["postCode"]!.GetValue<string>());
        Assert.Equal("NL", address["countryCode"]!["iso3166-1-alpha2"]!.GetValue<string>());
        Assert.NotNull(address["geolocation"]);
        Assert.False(address.ContainsKey("primaryCode"));
        Assert.False(address.ContainsKey("houseNumber"));
        Assert.False(address.ContainsKey("houseNumberAddition"));
        Assert.False(address.ContainsKey("postalCode"));
        Assert.False(address.ContainsKey("country"));
        Assert.False(address.ContainsKey("otherCodes"));
        Assert.False(address.ContainsKey("latitude"));
        Assert.False(address.ContainsKey("longitude"));
    }

    private static async Task<JsonObject> GetJsonAsync(HttpClient client, string requestUri)
    {
        HttpResponseMessage response = await client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        return JsonNode.Parse(await response.Content.ReadAsStringAsync())!.AsObject();
    }
}
