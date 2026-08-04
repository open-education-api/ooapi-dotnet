using System.Text.Json.Nodes;
using Microsoft.Extensions.DependencyInjection;
using OEAPI.API.Tests.Infrastructure;
using OEAPI.Infrastructure.Data.Context;
using OEAPI.Infrastructure.Data.Entities;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Confirms the fix-response-field-and-parameter-bugs fixes actually take effect: no fabricated
///     <c>totalCount</c>/<c>studyLoad</c> fields, and the <c>q</c>/<c>teachingLanguage</c>/<c>roomType</c>
///     named parameters actually restrict results. Follows the same seed-via-
///     <see cref="SqlServerOEAPIDbContext" />-then-real-HTTP-request pattern as
///     <see cref="Tier2BindingBugConformanceTests" />. The since/until bug was pulled back out of this
///     change (see design.md) and has no coverage here.
/// </summary>
[Collection(SqlServerCollection.Name)]
public class FixResponseFieldAndParameterBugsConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Fact]
    public async Task GetAll_NeverIncludesTotalCount()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid roomId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();
            dbContext.Rooms.Add(new RoomEntity
            {
                Id = roomId,
                RoomId = roomId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"FIX-ROOM-{roomId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"TotalCount Test Room\"}]"
            });
            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        HttpResponseMessage response = await client.GetAsync("/rooms");
        response.EnsureSuccessStatusCode();

        JsonObject body = JsonNode.Parse(await response.Content.ReadAsStringAsync())!.AsObject();
        Assert.False(body.ContainsKey("totalCount"));
    }

    [Fact]
    public async Task LearningComponentById_NeverIncludesStudyLoad()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid componentId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();
            dbContext.LearningComponents.Add(new LearningComponentEntity
            {
                Id = componentId,
                ComponentId = componentId.ToString(),
                ComponentType = "lecture",
                PrimaryCodeType = "identifier",
                PrimaryCode = $"FIX-LC-{componentId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"StudyLoad Test Component\"}]"
            });
            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        JsonObject byId = await GetJsonAsync(client, $"/learning-components/{componentId}");
        Assert.False(byId.ContainsKey("studyLoad"));

        JsonObject list = await GetJsonAsync(client, "/learning-components");
        JsonArray items = list["items"]!.AsArray();
        Assert.Contains(items, i => i!["componentId"]!.GetValue<string>() == componentId.ToString());
        Assert.All(items, i => Assert.False(i!.AsObject().ContainsKey("studyLoad")));
    }

    [Fact]
    public async Task TestComponentById_NeverIncludesStudyLoad()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid componentId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();
            dbContext.TestComponents.Add(new TestComponentEntity
            {
                Id = componentId,
                ComponentId = componentId.ToString(),
                ComponentType = "digital_test",
                PrimaryCodeType = "identifier",
                PrimaryCode = $"FIX-TC-{componentId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"StudyLoad Test Component\"}]"
            });
            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        JsonObject byId = await GetJsonAsync(client, $"/test-components/{componentId}");
        Assert.False(byId.ContainsKey("studyLoad"));
    }

    [Fact]
    public async Task LearningComponents_QAndTeachingLanguage_RestrictResults()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid matchId = Guid.CreateVersion7();
        Guid otherId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            dbContext.LearningComponents.Add(new LearningComponentEntity
            {
                Id = matchId,
                ComponentId = matchId.ToString(),
                ComponentType = "lecture",
                PrimaryCodeType = "identifier",
                PrimaryCode = $"FIX-LC-MATCH-{matchId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Quantum Mechanics Lecture\"}]",
                TeachingLanguagesJson = "[\"en-GB\"]"
            });
            dbContext.LearningComponents.Add(new LearningComponentEntity
            {
                Id = otherId,
                ComponentId = otherId.ToString(),
                ComponentType = "tutorial",
                PrimaryCodeType = "identifier",
                PrimaryCode = $"FIX-LC-OTHER-{otherId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Beginner Pottery Tutorial\"}]",
                TeachingLanguagesJson = "[\"nl-NL\"]"
            });

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        JsonObject viaQ = await GetJsonAsync(client, "/learning-components?q=Quantum");
        JsonArray qItems = viaQ["items"]!.AsArray();
        Assert.Contains(qItems, i => i!["componentId"]!.GetValue<string>() == matchId.ToString());
        Assert.DoesNotContain(qItems, i => i!["componentId"]!.GetValue<string>() == otherId.ToString());

        JsonObject viaLanguage = await GetJsonAsync(client, "/learning-components?teachingLanguage=en-GB");
        JsonArray languageItems = viaLanguage["items"]!.AsArray();
        Assert.Contains(languageItems, i => i!["componentId"]!.GetValue<string>() == matchId.ToString());
        Assert.DoesNotContain(languageItems, i => i!["componentId"]!.GetValue<string>() == otherId.ToString());
    }

    [Fact]
    public async Task Rooms_RoomTypeFilter_MatchesDefaultedValue()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid buildingId = Guid.CreateVersion7();
        Guid defaultedRoomId = Guid.CreateVersion7();
        Guid lectureHallId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            BuildingEntity building = new()
            {
                Id = buildingId,
                BuildingId = buildingId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"FIX-BLD-{buildingId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"RoomType Test Building\"}]"
            };
            dbContext.Buildings.Add(building);

            // RoomType left null on purpose - the response defaults this to "general_purpose".
            dbContext.Rooms.Add(new RoomEntity
            {
                Id = defaultedRoomId,
                RoomId = defaultedRoomId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"FIX-ROOM-DEF-{defaultedRoomId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Undesignated Room\"}]",
                BuildingEntityId = building.Id
            });
            dbContext.Rooms.Add(new RoomEntity
            {
                Id = lectureHallId,
                RoomId = lectureHallId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"FIX-ROOM-LEC-{lectureHallId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Big Lecture Hall\"}]",
                RoomType = "lecture_hall",
                BuildingEntityId = building.Id
            });

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        JsonObject topLevel = await GetJsonAsync(client, "/rooms?roomType=general_purpose");
        JsonArray topLevelItems = topLevel["items"]!.AsArray();
        Assert.Contains(topLevelItems, i => i!["roomId"]!.GetValue<string>() == defaultedRoomId.ToString());
        Assert.DoesNotContain(topLevelItems, i => i!["roomId"]!.GetValue<string>() == lectureHallId.ToString());

        JsonObject nested =
            await GetJsonAsync(client, $"/buildings/{buildingId}/rooms?roomType=general_purpose");
        JsonArray nestedItems = nested["items"]!.AsArray();
        Assert.Contains(nestedItems, i => i!["roomId"]!.GetValue<string>() == defaultedRoomId.ToString());
        Assert.DoesNotContain(nestedItems, i => i!["roomId"]!.GetValue<string>() == lectureHallId.ToString());
    }

    [Fact]
    public async Task ProgrammeOfferings_QSearch_RestrictsResults()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid academicSessionId = Guid.CreateVersion7();
        Guid programmeId = Guid.CreateVersion7();
        Guid matchViaSessionId = Guid.CreateVersion7();
        Guid otherViaSessionId = Guid.CreateVersion7();
        Guid matchViaProgrammeId = Guid.CreateVersion7();
        Guid otherViaProgrammeId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            AcademicSessionEntity academicSession = new()
            {
                Id = academicSessionId,
                AcademicSessionId = academicSessionId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"FIX-AS-{academicSessionId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"FixBugs Session\"}]",
                AcademicSessionType = "semester",
                StartDateTime = "2025-09-01T00:00:00+01:00",
                EndDateTime = "2026-01-31T23:59:59+01:00"
            };
            dbContext.AcademicSessions.Add(academicSession);

            ProgrammeEntity programme = new()
            {
                Id = programmeId,
                ProgrammeId = programmeId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"FIX-PRG-{programmeId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"FixBugs Programme\"}]"
            };
            dbContext.Programmes.Add(programme);

            dbContext.ProgrammeOfferings.Add(new ProgrammeOfferingEntity
            {
                Id = matchViaSessionId,
                ProgrammeOfferingIdValue = matchViaSessionId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"FIX-PO-MATCH-{matchViaSessionId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Astrophysics Cohort\"}]",
                AcademicSessionEntityId = academicSession.Id
            });
            dbContext.ProgrammeOfferings.Add(new ProgrammeOfferingEntity
            {
                Id = otherViaSessionId,
                ProgrammeOfferingIdValue = otherViaSessionId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"FIX-PO-OTHER-{otherViaSessionId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Botany Cohort\"}]",
                AcademicSessionEntityId = academicSession.Id
            });

            // A linked academic session is required for these to appear at all under the new
            // since=<today> default (add-since-default-today-filter) - since always applies now
            // (defaulted when omitted), and an offering with no linked session is unconditionally
            // excluded by that filter (fix-since-until-academic-session-target's own established
            // "excluded, not a 500" precedent), regardless of the bypass since= below.
            dbContext.ProgrammeOfferings.Add(new ProgrammeOfferingEntity
            {
                Id = matchViaProgrammeId,
                ProgrammeOfferingIdValue = matchViaProgrammeId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"FIX-PO-PMATCH-{matchViaProgrammeId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Astrophysics Cohort\"}]",
                ProgrammeEntityId = programme.Id,
                AcademicSessionEntityId = academicSession.Id
            });
            dbContext.ProgrammeOfferings.Add(new ProgrammeOfferingEntity
            {
                Id = otherViaProgrammeId,
                ProgrammeOfferingIdValue = otherViaProgrammeId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"FIX-PO-POTHER-{otherViaProgrammeId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Botany Cohort\"}]",
                ProgrammeEntityId = programme.Id,
                AcademicSessionEntityId = academicSession.Id
            });

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        // Both fixtures below predate "today" (the session itself, and the programme-offerings with
        // no linked session at all) - add-since-default-today-filter's since=<today> default would
        // otherwise exclude everything regardless of q=, so bypass it with an explicit early since=,
        // unrelated to what this test actually checks (q search).
        const string bypassDefaultSince = "&since=2000-01-01T00:00:00%2B00:00";

        JsonObject viaSession =
            await GetJsonAsync(client,
                $"/academic-sessions/{academicSessionId}/programme-offerings?q=Astro{bypassDefaultSince}");
        JsonArray sessionItems = viaSession["items"]!.AsArray();
        Assert.Contains(sessionItems, i => i!["programmeOfferingId"]!.GetValue<string>() == matchViaSessionId.ToString());
        Assert.DoesNotContain(sessionItems, i => i!["programmeOfferingId"]!.GetValue<string>() == otherViaSessionId.ToString());

        JsonObject viaProgramme =
            await GetJsonAsync(client,
                $"/programmes/{programmeId}/programme-offerings?q=Astro{bypassDefaultSince}");
        JsonArray programmeItems = viaProgramme["items"]!.AsArray();
        Assert.Contains(programmeItems,
            i => i!["programmeOfferingId"]!.GetValue<string>() == matchViaProgrammeId.ToString());
        Assert.DoesNotContain(programmeItems,
            i => i!["programmeOfferingId"]!.GetValue<string>() == otherViaProgrammeId.ToString());
    }

    private static async Task<JsonObject> GetJsonAsync(HttpClient client, string requestUri)
    {
        HttpResponseMessage response = await client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        return JsonNode.Parse(await response.Content.ReadAsStringAsync())!.AsObject();
    }
}
