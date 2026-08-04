using System.Text.Json.Nodes;
using Microsoft.Extensions.DependencyInjection;
using OEAPI.API.Tests.Infrastructure;
using OEAPI.Infrastructure.Data.Context;
using OEAPI.Infrastructure.Data.Entities;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Confirms the fix-spec-conformance-tier2-binding-bugs fixes actually take effect: <c>q</c> now
///     filters where <c>search</c> used to be the only working key, spec-spelled underscored
///     <c>expand=</c> values now match, and the 3 newly-added composed expand cases (rooms x2,
///     academicSession) populate real nested data. Follows the same seed-via-<see cref="SqlServerOEAPIDbContext"/>-
///     then-real-HTTP-request pattern as <see cref="NestedEndpointSpecParamGapsConformanceTests"/>.
/// </summary>
[Collection(SqlServerCollection.Name)]
public class Tier2BindingBugConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Fact]
    public async Task RoomsByBuildingId_QSearch_RestrictsResults()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid buildingId = Guid.CreateVersion7();
        Guid matchId = Guid.CreateVersion7();
        Guid otherId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            BuildingEntity building = new()
            {
                Id = buildingId,
                BuildingId = buildingId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T2-BLD-{buildingId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier2 Building\"}]"
            };
            dbContext.Buildings.Add(building);

            RoomEntity matching = new()
            {
                Id = matchId,
                RoomId = matchId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T2-ROOM-MATCH-{matchId.ToString()[..8]}",
                Name = "Alpha Lecture Hall",
                NameJson = "[{\"language\":\"en\",\"value\":\"Alpha Lecture Hall\"}]",
                BuildingEntityId = building.Id
            };
            dbContext.Rooms.Add(matching);

            RoomEntity other = new()
            {
                Id = otherId,
                RoomId = otherId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T2-ROOM-OTHER-{otherId.ToString()[..8]}",
                Name = "Beta Storage Closet",
                NameJson = "[{\"language\":\"en\",\"value\":\"Beta Storage Closet\"}]",
                BuildingEntityId = building.Id
            };
            dbContext.Rooms.Add(other);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        // The old (wrong) key must no longer filter anything in.
        JsonObject viaOldKey = await GetJsonAsync(client, $"/buildings/{buildingId}/rooms?search=Alpha");
        Assert.Equal(2, viaOldKey["items"]!.AsArray().Count);

        // The spec-conformant key must filter correctly.
        JsonObject viaNewKey = await GetJsonAsync(client, $"/buildings/{buildingId}/rooms?q=Alpha");
        JsonArray items = viaNewKey["items"]!.AsArray();
        Assert.Contains(items, i => i!["roomId"]!.GetValue<string>() == matchId.ToString());
        Assert.DoesNotContain(items, i => i!["roomId"]!.GetValue<string>() == otherId.ToString());
    }

    [Fact]
    public async Task CourseOfferingById_ExpandUnderscoredValue_Matches()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid orgId = Guid.CreateVersion7();
        Guid courseId = Guid.CreateVersion7();
        Guid academicSessionId = Guid.CreateVersion7();
        Guid courseOfferingId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            OrganisationEntity org = new()
            {
                Id = orgId,
                OrganisationId = orgId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T2-ORG-{orgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier2 Org\"}]"
            };
            dbContext.Organisations.Add(org);

            CourseEntity course = new()
            {
                Id = courseId,
                CourseId = courseId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T2-CRS-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier2 Course\"}]"
            };
            dbContext.Courses.Add(course);

            AcademicSessionEntity academicSession = new()
            {
                Id = academicSessionId,
                AcademicSessionId = academicSessionId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T2-AS-{academicSessionId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier2 Session\"}]",
                AcademicSessionType = "semester",
                StartDateTime = "2025-09-01T00:00:00+01:00",
                EndDateTime = "2026-01-31T23:59:59+01:00"
            };
            dbContext.AcademicSessions.Add(academicSession);

            CourseOfferingEntity courseOffering = new()
            {
                Id = courseOfferingId,
                CourseOfferingId = courseOfferingId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T2-CO-{courseOfferingId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier2 Offering\"}]",
                CourseEntityId = course.Id,
                OrganisationEntityId = org.Id,
                AcademicSessionEntityId = academicSession.Id
            };
            dbContext.CourseOfferings.Add(courseOffering);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        // The spec-spelled, underscored value must now match (before the S2 fix this silently no-opped).
        JsonObject json = await GetJsonAsync(client, $"/course-offerings/{courseOfferingId}?expand=academic_session");
        Assert.NotNull(json["academicSession"]);
        Assert.Equal(academicSessionId.ToString(), json["academicSession"]!["academicSessionId"]!.GetValue<string>());
    }

    [Fact]
    public async Task LearningComponentOfferingAssociationById_ExpandRooms_PopulatesNestedOfferingRooms()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid orgId = Guid.CreateVersion7();
        Guid personId = Guid.CreateVersion7();
        Guid courseId = Guid.CreateVersion7();
        Guid learningComponentId = Guid.CreateVersion7();
        Guid offeringId = Guid.CreateVersion7();
        Guid buildingId = Guid.CreateVersion7();
        Guid roomId = Guid.CreateVersion7();
        Guid assocId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            OrganisationEntity org = new()
            {
                Id = orgId,
                OrganisationId = orgId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T2-ORG2-{orgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier2 Org2\"}]"
            };
            dbContext.Organisations.Add(org);

            PersonEntity person = new()
            {
                Id = personId,
                PersonId = personId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T2-PER-{personId.ToString()[..8]}",
                GivenName = "Tier2",
                Surname = "Person"
            };
            dbContext.Persons.Add(person);

            CourseEntity course = new()
            {
                Id = courseId,
                CourseId = courseId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T2-CRS2-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier2 Course2\"}]"
            };
            dbContext.Courses.Add(course);

            LearningComponentEntity learningComponent = new()
            {
                Id = learningComponentId,
                ComponentId = learningComponentId.ToString(),
                ComponentType = "lecture",
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T2-LC-{learningComponentId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier2 Component\"}]",
                CourseEntityId = course.Id,
                OrganisationEntityId = org.Id
            };
            dbContext.LearningComponents.Add(learningComponent);

            BuildingEntity building = new()
            {
                Id = buildingId,
                BuildingId = buildingId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T2-BLD2-{buildingId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier2 Building2\"}]"
            };
            dbContext.Buildings.Add(building);

            RoomEntity room = new()
            {
                Id = roomId,
                RoomId = roomId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T2-ROOM2-{roomId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier2 Room\"}]",
                BuildingEntityId = building.Id
            };
            dbContext.Rooms.Add(room);

            LearningComponentOfferingEntity offering = new()
            {
                Id = offeringId,
                LearningComponentOfferingIdValue = offeringId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T2-LCO-{offeringId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier2 Offering2\"}]",
                LearningComponentEntityId = learningComponent.Id,
                OrganisationEntityId = org.Id,
                Rooms = [room]
            };
            dbContext.LearningComponentOfferings.Add(offering);

            LearningComponentOfferingAssociationEntity association = new()
            {
                Id = assocId,
                LearningComponentOfferingAssociationIdValue = assocId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T2-LCOA-{assocId.ToString()[..8]}",
                Role = "student",
                LearningComponentOfferingEntityId = offering.Id,
                PersonEntityId = person.Id,
                OrganisationEntityId = org.Id
            };
            dbContext.LearningComponentOfferingAssociations.Add(association);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        // Without expand=rooms: no nested offering, so no rooms either.
        JsonObject withoutExpand = await GetJsonAsync(client, $"/learning-component-offering-associations/{assocId}");
        Assert.False(withoutExpand.ContainsKey("learningComponentOffering"));

        // With expand=rooms: the association has no rooms field of its own - it must compose into the
        // nested learningComponentOffering.rooms field instead.
        JsonObject withExpand =
            await GetJsonAsync(client, $"/learning-component-offering-associations/{assocId}?expand=rooms");
        Assert.NotNull(withExpand["learningComponentOffering"]);
        JsonArray rooms = withExpand["learningComponentOffering"]!["rooms"]!.AsArray();
        Assert.Contains(rooms, r => r!["roomId"]!.GetValue<string>() == roomId.ToString());
    }

    [Fact]
    public async Task CourseOfferingAssociationById_ExpandAcademicSession_PopulatesNestedCourseOfferingAcademicSession()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid orgId = Guid.CreateVersion7();
        Guid personId = Guid.CreateVersion7();
        Guid courseId = Guid.CreateVersion7();
        Guid academicSessionId = Guid.CreateVersion7();
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
                PrimaryCode = $"T2-ORG3-{orgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier2 Org3\"}]"
            };
            dbContext.Organisations.Add(org);

            PersonEntity person = new()
            {
                Id = personId,
                PersonId = personId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T2-PER2-{personId.ToString()[..8]}",
                GivenName = "Tier2",
                Surname = "Person2"
            };
            dbContext.Persons.Add(person);

            CourseEntity course = new()
            {
                Id = courseId,
                CourseId = courseId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T2-CRS3-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier2 Course3\"}]"
            };
            dbContext.Courses.Add(course);

            AcademicSessionEntity academicSession = new()
            {
                Id = academicSessionId,
                AcademicSessionId = academicSessionId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T2-AS2-{academicSessionId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier2 Session2\"}]",
                AcademicSessionType = "semester",
                StartDateTime = "2025-09-01T00:00:00+01:00",
                EndDateTime = "2026-01-31T23:59:59+01:00"
            };
            dbContext.AcademicSessions.Add(academicSession);

            CourseOfferingEntity courseOffering = new()
            {
                Id = courseOfferingId,
                CourseOfferingId = courseOfferingId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T2-CO2-{courseOfferingId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier2 Offering3\"}]",
                CourseEntityId = course.Id,
                OrganisationEntityId = org.Id,
                AcademicSessionEntityId = academicSession.Id
            };
            dbContext.CourseOfferings.Add(courseOffering);

            CourseOfferingAssociationEntity association = new()
            {
                Id = assocId,
                CourseOfferingAssociationIdValue = assocId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T2-COA-{assocId.ToString()[..8]}",
                Role = "student",
                CourseOfferingEntityId = courseOffering.Id,
                PersonEntityId = person.Id,
                OrganisationEntityId = org.Id
            };
            dbContext.CourseOfferingAssociations.Add(association);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        JsonObject json = await GetJsonAsync(client,
            $"/course-offering-associations/{assocId}?expand=academic_session,course_offering");

        // Composes into the nested courseOffering.academicSession field...
        Assert.NotNull(json["courseOffering"]);
        Assert.NotNull(json["courseOffering"]!["academicSession"]);
        Assert.Equal(academicSessionId.ToString(),
            json["courseOffering"]!["academicSession"]!["academicSessionId"]!.GetValue<string>());

        // ...never the top-level field, which stays exclusive to the person-nested endpoint (regression
        // guard already covered by NestedEndpointSpecParamGapsConformanceTests, re-asserted here too).
        Assert.False(json.ContainsKey("academicSession"));
    }

    [Fact]
    public async Task Programmes_QualificationAwardedFilter_RestrictsResults()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid matchId = Guid.CreateVersion7();
        Guid otherId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            ProgrammeEntity matching = new()
            {
                Id = matchId,
                ProgrammeId = matchId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T2-PRG-MATCH-{matchId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier2 Bachelor Programme\"}]",
                QualificationAwarded = "bachelor"
            };
            dbContext.Programmes.Add(matching);

            ProgrammeEntity other = new()
            {
                Id = otherId,
                ProgrammeId = otherId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T2-PRG-OTHER-{otherId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier2 Master Programme\"}]",
                QualificationAwarded = "master"
            };
            dbContext.Programmes.Add(other);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        // The old (wrong) key must no longer filter anything out.
        JsonObject viaOldKey = await GetJsonAsync(client, "/programmes?qualification=bachelor");
        JsonArray viaOldKeyItems = viaOldKey["items"]!.AsArray();
        Assert.Contains(viaOldKeyItems, i => i!["programmeId"]!.GetValue<string>() == matchId.ToString());
        Assert.Contains(viaOldKeyItems, i => i!["programmeId"]!.GetValue<string>() == otherId.ToString());

        // The spec-conformant key must filter correctly.
        JsonObject viaNewKey = await GetJsonAsync(client, "/programmes?qualificationAwarded=bachelor");
        JsonArray items = viaNewKey["items"]!.AsArray();
        Assert.Contains(items, i => i!["programmeId"]!.GetValue<string>() == matchId.ToString());
        Assert.DoesNotContain(items, i => i!["programmeId"]!.GetValue<string>() == otherId.ToString());
    }

    private static async Task<JsonObject> GetJsonAsync(HttpClient client, string requestUri)
    {
        HttpResponseMessage response = await client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        return JsonNode.Parse(await response.Content.ReadAsStringAsync())!.AsObject();
    }
}
