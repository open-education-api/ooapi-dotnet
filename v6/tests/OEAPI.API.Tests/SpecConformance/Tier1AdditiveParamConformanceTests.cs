using System.Text.Json.Nodes;
using Microsoft.Extensions.DependencyInjection;
using OEAPI.API.Tests.Infrastructure;
using OEAPI.Infrastructure.Data.Context;
using OEAPI.Infrastructure.Data.Entities;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Confirms a representative sample of the query parameters added by the
///     <c>fix-spec-conformance-tier1-additive-gaps</c> change actually filter/prune, not just accept
///     the parameter without effect. One test per distinct filter <i>mechanism</i> introduced by that
///     change (see its <c>tasks.md</c> 9.1) - not exhaustive over every one of the ~40 endpoints
///     touched, since every addition follows an existing sibling pattern 1:1.
/// </summary>
/// <remarks>
///     Every fixture is seeded directly through <see cref="SqlServerOEAPIDbContext" />, mirroring
///     <see cref="NestedEndpointSpecParamGapsConformanceTests" />'s own pattern.
/// </remarks>
[Collection(SqlServerCollection.Name)]
public class Tier1AdditiveParamConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Fact]
    public async Task Rooms_QSearch_RestrictsResults()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid matchId = Guid.CreateVersion7();
        Guid otherId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            RoomEntity matching = new()
            {
                Id = matchId,
                RoomId = matchId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1P-ROOM-MATCH-{matchId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Lecture Hall Alpha\"}]"
            };
            dbContext.Rooms.Add(matching);

            RoomEntity other = new()
            {
                Id = otherId,
                RoomId = otherId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1P-ROOM-OTHER-{otherId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Storage Closet Beta\"}]"
            };
            dbContext.Rooms.Add(other);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        JsonObject json = await GetJsonAsync(client, "/rooms?q=Alpha");
        JsonArray items = json["items"]!.AsArray();

        Assert.Contains(items, i => i!["roomId"]!.GetValue<string>() == matchId.ToString());
        Assert.DoesNotContain(items, i => i!["roomId"]!.GetValue<string>() == otherId.ToString());
    }

    [Fact]
    public async Task Rooms_RoomTypeFilter_RestrictsResults()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid matchId = Guid.CreateVersion7();
        Guid otherId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            RoomEntity matching = new()
            {
                Id = matchId,
                RoomId = matchId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1P-ROOM-RT-MATCH-{matchId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Room Type Lecture Hall\"}]",
                RoomType = "lecture_room"
            };
            dbContext.Rooms.Add(matching);

            RoomEntity other = new()
            {
                Id = otherId,
                RoomId = otherId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1P-ROOM-RT-OTHER-{otherId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Room Type Office\"}]",
                RoomType = "office"
            };
            dbContext.Rooms.Add(other);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        JsonObject json = await GetJsonAsync(client, "/rooms?roomType=lecture_room");
        JsonArray items = json["items"]!.AsArray();

        Assert.Contains(items, i => i!["roomId"]!.GetValue<string>() == matchId.ToString());
        Assert.DoesNotContain(items, i => i!["roomId"]!.GetValue<string>() == otherId.ToString());
    }

    [Fact]
    public async Task LearningOutcomes_SinceUntilFilters_RestrictResults()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid matchId = Guid.CreateVersion7();
        Guid otherId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            LearningOutcomeEntity matching = new()
            {
                Id = matchId,
                LearningOutcomeId = matchId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1P-LO-MATCH-{matchId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier1 Learning Outcome In Window\"}]",
                ValidFrom = "2025-09-01T00:00:00+01:00",
                ValidTo = "2025-12-01T00:00:00+01:00"
            };
            dbContext.LearningOutcomes.Add(matching);

            LearningOutcomeEntity other = new()
            {
                Id = otherId,
                LearningOutcomeId = otherId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1P-LO-OTHER-{otherId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier1 Learning Outcome Out Of Window\"}]",
                ValidFrom = "2020-09-01T00:00:00+01:00",
                ValidTo = "2020-12-01T00:00:00+01:00"
            };
            dbContext.LearningOutcomes.Add(other);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        JsonObject json = await GetJsonAsync(client,
            "/learning-outcomes?since=2025-01-01T00:00:00%2B01:00&until=2026-01-01T00:00:00%2B01:00");
        JsonArray items = json["items"]!.AsArray();

        Assert.Contains(items, i => i!["learningOutcomeId"]!.GetValue<string>() == matchId.ToString());
        Assert.DoesNotContain(items, i => i!["learningOutcomeId"]!.GetValue<string>() == otherId.ToString());
    }

    [Fact]
    public async Task LearningComponentOfferingAssociationsByPersonId_RoleAndResultStateFilters_RestrictResults()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid orgId = Guid.CreateVersion7();
        Guid personId = Guid.CreateVersion7();
        Guid courseId = Guid.CreateVersion7();
        Guid learningComponentId = Guid.CreateVersion7();
        Guid learningComponentOfferingId = Guid.CreateVersion7();
        Guid matchAssocId = Guid.CreateVersion7();
        Guid otherRoleAssocId = Guid.CreateVersion7();
        Guid otherResultAssocId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            OrganisationEntity org = new()
            {
                Id = orgId,
                OrganisationId = orgId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1P-ORG-{orgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier1 Assoc Org\"}]"
            };
            dbContext.Organisations.Add(org);

            PersonEntity person = new()
            {
                Id = personId,
                PersonId = personId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1P-PER-{personId.ToString()[..8]}",
                GivenName = "Tier1",
                Surname = "Person"
            };
            dbContext.Persons.Add(person);

            CourseEntity course = new()
            {
                Id = courseId,
                CourseId = courseId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1P-CRS-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier1 Assoc Course\"}]"
            };
            dbContext.Courses.Add(course);

            LearningComponentEntity learningComponent = new()
            {
                Id = learningComponentId,
                ComponentId = learningComponentId.ToString(),
                ComponentType = "lecture",
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1P-LC-{learningComponentId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier1 Assoc Component\"}]",
                CourseEntityId = course.Id,
                OrganisationEntityId = org.Id
            };
            dbContext.LearningComponents.Add(learningComponent);

            LearningComponentOfferingEntity learningComponentOffering = new()
            {
                Id = learningComponentOfferingId,
                LearningComponentOfferingIdValue = learningComponentOfferingId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1P-LCO-{learningComponentOfferingId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier1 Assoc Offering\"}]",
                LearningComponentEntityId = learningComponent.Id,
                OrganisationEntityId = org.Id
            };
            dbContext.LearningComponentOfferings.Add(learningComponentOffering);

            LearningComponentOfferingAssociationEntity matching = new()
            {
                Id = matchAssocId,
                LearningComponentOfferingAssociationIdValue = matchAssocId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1P-LCOA-MATCH-{matchAssocId.ToString()[..8]}",
                Role = "student",
                LearningComponentOfferingEntityId = learningComponentOffering.Id,
                PersonEntityId = person.Id,
                OrganisationEntityId = org.Id,
                ResultJson = "{\"state\":\"submitted\",\"resultDateTime\":\"2026-01-15T00:00:00+01:00\"}"
            };
            dbContext.LearningComponentOfferingAssociations.Add(matching);

            LearningComponentOfferingAssociationEntity otherRole = new()
            {
                Id = otherRoleAssocId,
                LearningComponentOfferingAssociationIdValue = otherRoleAssocId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1P-LCOA-ROLE-{otherRoleAssocId.ToString()[..8]}",
                Role = "instructor",
                LearningComponentOfferingEntityId = learningComponentOffering.Id,
                PersonEntityId = person.Id,
                OrganisationEntityId = org.Id,
                ResultJson = "{\"state\":\"submitted\",\"resultDateTime\":\"2026-01-15T00:00:00+01:00\"}"
            };
            dbContext.LearningComponentOfferingAssociations.Add(otherRole);

            LearningComponentOfferingAssociationEntity otherResult = new()
            {
                Id = otherResultAssocId,
                LearningComponentOfferingAssociationIdValue = otherResultAssocId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1P-LCOA-RESULT-{otherResultAssocId.ToString()[..8]}",
                Role = "student",
                LearningComponentOfferingEntityId = learningComponentOffering.Id,
                PersonEntityId = person.Id,
                OrganisationEntityId = org.Id,
                ResultJson = "{\"state\":\"pending\",\"resultDateTime\":\"2026-01-15T00:00:00+01:00\"}"
            };
            dbContext.LearningComponentOfferingAssociations.Add(otherResult);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        JsonObject roleFiltered = await GetJsonAsync(client,
            $"/persons/{personId}/learning-component-offering-associations?role=student");
        JsonArray roleItems = roleFiltered["items"]!.AsArray();
        Assert.Contains(roleItems,
            i => i!["associationId"]!.GetValue<string>() == matchAssocId.ToString());
        Assert.Contains(roleItems,
            i => i!["associationId"]!.GetValue<string>() == otherResultAssocId.ToString());
        Assert.DoesNotContain(roleItems,
            i => i!["associationId"]!.GetValue<string>() == otherRoleAssocId.ToString());

        JsonObject resultStateFiltered = await GetJsonAsync(client,
            $"/persons/{personId}/learning-component-offering-associations?resultState=submitted");
        JsonArray resultStateItems = resultStateFiltered["items"]!.AsArray();
        Assert.Contains(resultStateItems,
            i => i!["associationId"]!.GetValue<string>() == matchAssocId.ToString());
        Assert.Contains(resultStateItems,
            i => i!["associationId"]!.GetValue<string>() == otherRoleAssocId.ToString());
        Assert.DoesNotContain(resultStateItems,
            i => i!["associationId"]!.GetValue<string>() == otherResultAssocId.ToString());
    }

    [Fact]
    public async Task GroupsByCourseOfferingId_ConsumerFilterQueryFields_WorkTogether()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid orgId = Guid.CreateVersion7();
        Guid courseId = Guid.CreateVersion7();
        Guid courseOfferingId = Guid.CreateVersion7();
        Guid cohortGroupId = Guid.CreateVersion7();
        Guid committeeGroupId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            OrganisationEntity org = new()
            {
                Id = orgId,
                OrganisationId = orgId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1P-ORG2-{orgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier1 Groups Org\"}]"
            };
            dbContext.Organisations.Add(org);

            CourseEntity course = new()
            {
                Id = courseId,
                CourseId = courseId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1P-CRS2-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier1 Groups Course\"}]"
            };
            dbContext.Courses.Add(course);

            CourseOfferingEntity courseOffering = new()
            {
                Id = courseOfferingId,
                CourseOfferingId = courseOfferingId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1P-CO-{courseOfferingId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier1 Groups Offering\"}]",
                CourseEntityId = course.Id,
                OrganisationEntityId = org.Id
            };
            dbContext.CourseOfferings.Add(courseOffering);

            GroupEntity cohortGroup = new()
            {
                Id = cohortGroupId,
                GroupId = cohortGroupId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1P-GRP-COHORT-{cohortGroupId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier1 Cohort Group\"}]",
                GroupType = "cohort",
                OrganisationEntityId = org.Id
            };
            dbContext.Groups.Add(cohortGroup);

            GroupEntity committeeGroup = new()
            {
                Id = committeeGroupId,
                GroupId = committeeGroupId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1P-GRP-COMMITTEE-{committeeGroupId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier1 Committee Group\"}]",
                GroupType = "committee",
                OrganisationEntityId = org.Id
            };
            dbContext.Groups.Add(committeeGroup);

            // Many-to-many via the GroupCourseOfferings join table (see
            // OEAPIDbContext.ConfigureGroupOfferingRelationships) - both sides must already be tracked.
            courseOffering.Groups.Add(cohortGroup);
            courseOffering.Groups.Add(committeeGroup);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        // fields=(groupId): the explicitly requested optional field must survive, spec-required
        // fields (groupType/name/primaryCode) always survive regardless, and an unrequested optional
        // field (organisationId) must be pruned.
        JsonObject fieldsPruned = await GetJsonAsync(client,
            $"/course-offerings/{courseOfferingId}/groups?fields=(groupId)");
        JsonArray fieldsPrunedItems = fieldsPruned["items"]!.AsArray();
        JsonObject cohortItem = fieldsPrunedItems
            .Select(i => i!.AsObject())
            .Single(i => i["groupId"]!.GetValue<string>() == cohortGroupId.ToString());
        Assert.True(cohortItem.ContainsKey("groupId"), "explicitly requested field must survive");
        Assert.True(cohortItem.ContainsKey("groupType"), "spec-required field must always survive");
        Assert.True(cohortItem.ContainsKey("name"), "spec-required field must always survive");
        Assert.True(cohortItem.ContainsKey("primaryCode"), "spec-required field must always survive");
        Assert.False(cohortItem.ContainsKey("organisationId"), "unrequested optional field must be pruned");

        // filterQuery/consumer: basic 200 + correctly restricted/scoped result, not a deep proof of
        // either mechanism (both are already covered elsewhere) - just confirms they were wired up on
        // this endpoint rather than silently ignored.
        JsonObject filterQueried = await GetJsonAsync(client,
            $"/course-offerings/{courseOfferingId}/groups?filter_query[groupType][in]=cohort&consumer=any-consumer");
        JsonArray filterQueriedItems = filterQueried["items"]!.AsArray();
        Assert.Contains(filterQueriedItems, i => i!["groupId"]!.GetValue<string>() == cohortGroupId.ToString());
        Assert.DoesNotContain(filterQueriedItems, i => i!["groupId"]!.GetValue<string>() == committeeGroupId.ToString());
    }

    private static async Task<JsonObject> GetJsonAsync(HttpClient client, string requestUri)
    {
        HttpResponseMessage response = await client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        return JsonNode.Parse(await response.Content.ReadAsStringAsync())!.AsObject();
    }
}
