using System.Text.Json.Nodes;
using Microsoft.Extensions.DependencyInjection;
using OEAPI.API.Tests.Infrastructure;
using OEAPI.Infrastructure.Data.Context;
using OEAPI.Infrastructure.Data.Entities;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Confirms the two nested list endpoints identified as missing spec-declared parameters
///     (<c>GET /courses/{courseId}/learning-component-offerings</c> and
///     <c>GET /persons/{personId}/course-offering-associations</c> - see the
///     <c>fix-nested-endpoint-spec-param-gaps</c> change) actually filter/expand, not just accept the
///     parameter without effect.
/// </summary>
/// <remarks>
///     Every fixture is seeded directly through <see cref="SqlServerOEAPIDbContext" />, mirroring
///     <see cref="TimelineOverridesConformanceTests" />'s own pattern - simpler and more explicit than
///     round-tripping through write endpoints for entities this test doesn't otherwise need to exercise
///     writes for.
/// </remarks>
[Collection(SqlServerCollection.Name)]
public class NestedEndpointSpecParamGapsConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Fact]
    public async Task LearningComponentOfferingsByCourseId_NewFilters_RestrictResults()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid orgId = Guid.CreateVersion7();
        Guid courseId = Guid.CreateVersion7();
        Guid learningComponentId = Guid.CreateVersion7();
        Guid matchId = Guid.CreateVersion7();
        Guid otherId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            OrganisationEntity org = new()
            {
                Id = orgId,
                OrganisationId = orgId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"NEG-ORG-{orgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Nested Gap Org\"}]"
            };
            dbContext.Organisations.Add(org);

            CourseEntity course = new()
            {
                Id = courseId,
                CourseId = courseId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"NEG-CRS-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Nested Gap Course\"}]"
            };
            dbContext.Courses.Add(course);

            LearningComponentEntity learningComponent = new()
            {
                Id = learningComponentId,
                ComponentId = learningComponentId.ToString(),
                ComponentType = "lecture",
                PrimaryCodeType = "identifier",
                PrimaryCode = $"NEG-LC-{learningComponentId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Nested Gap Component\"}]",
                CourseEntityId = course.Id,
                OrganisationEntityId = org.Id
            };
            dbContext.LearningComponents.Add(learningComponent);

            // since/until filter by the linked academic session's own dates (fix-since-until-
            // academic-session-target), not the offering's own StartDateTime/EndDateTime - each
            // offering needs its own session with dates that do/don't fall in the queried range.
            Guid matchingSessionId = Guid.CreateVersion7();
            AcademicSessionEntity matchingSession = new()
            {
                Id = matchingSessionId,
                AcademicSessionId = matchingSessionId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"NEG-AS-MATCH-{matchingSessionId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Nested Gap Matching Session\"}]",
                AcademicSessionType = "semester",
                StartDateTime = "2025-09-01T00:00:00+01:00",
                EndDateTime = "2025-12-01T00:00:00+01:00"
            };
            dbContext.AcademicSessions.Add(matchingSession);

            Guid otherSessionId = Guid.CreateVersion7();
            AcademicSessionEntity otherSession = new()
            {
                Id = otherSessionId,
                AcademicSessionId = otherSessionId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"NEG-AS-OTHER-{otherSessionId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Nested Gap Other Session\"}]",
                AcademicSessionType = "semester",
                StartDateTime = "2020-09-01T00:00:00+01:00",
                EndDateTime = "2020-12-01T00:00:00+01:00"
            };
            dbContext.AcademicSessions.Add(otherSession);

            LearningComponentOfferingEntity matching = new()
            {
                Id = matchId,
                LearningComponentOfferingIdValue = matchId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"NEG-LCO-MATCH-{matchId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Nested Gap Matching Offering\"}]",
                LearningComponentEntityId = learningComponent.Id,
                OrganisationEntityId = org.Id,
                AcademicSessionEntityId = matchingSessionId,
                State = "active",
                TeachingLanguagesJson = "[\"en\"]",
                ModesOfDeliveryJson = "[\"online\"]",
                ResultExpected = true,
                StartDateTime = "2025-09-01T00:00:00+01:00",
                EndDateTime = "2025-12-01T00:00:00+01:00"
            };
            dbContext.LearningComponentOfferings.Add(matching);

            LearningComponentOfferingEntity other = new()
            {
                Id = otherId,
                LearningComponentOfferingIdValue = otherId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"NEG-LCO-OTHER-{otherId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Nested Gap Other Offering\"}]",
                LearningComponentEntityId = learningComponent.Id,
                OrganisationEntityId = org.Id,
                AcademicSessionEntityId = otherSessionId,
                State = "cancelled",
                TeachingLanguagesJson = "[\"fr\"]",
                ModesOfDeliveryJson = "[\"onsite\"]",
                ResultExpected = false,
                StartDateTime = "2020-09-01T00:00:00+01:00",
                EndDateTime = "2020-12-01T00:00:00+01:00"
            };
            dbContext.LearningComponentOfferings.Add(other);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        async Task AssertOnlyMatchingReturned(string query)
        {
            JsonObject json = await GetJsonAsync(client, $"/courses/{courseId}/learning-component-offerings{query}");
            JsonArray items = json["items"]!.AsArray();
            Assert.Contains(items, i => i!["learningComponentOfferingId"]!.GetValue<string>() == matchId.ToString());
            Assert.DoesNotContain(items, i => i!["learningComponentOfferingId"]!.GetValue<string>() == otherId.ToString());
        }

        // Both fixtures' linked academic sessions predate "today" (add-since-default-today-filter's
        // since=<today> default would otherwise exclude both regardless of these other filters) - an
        // explicit early since= bypasses the default without affecting what's actually under test.
        const string bypassDefaultSince = "&since=2000-01-01T00:00:00%2B00:00";
        await AssertOnlyMatchingReturned($"?state=active{bypassDefaultSince}");
        await AssertOnlyMatchingReturned($"?teachingLanguage=en{bypassDefaultSince}");
        await AssertOnlyMatchingReturned($"?modeOfDelivery=online{bypassDefaultSince}");
        await AssertOnlyMatchingReturned($"?resultExpected=true{bypassDefaultSince}");
        await AssertOnlyMatchingReturned("?since=2025-01-01T00:00:00%2B01:00&until=2026-01-01T00:00:00%2B01:00");
        await AssertOnlyMatchingReturned($"?q=Matching{bypassDefaultSince}");
    }

    [Fact]
    public async Task LearningComponentOfferingsByCourseId_Expand_PopulatesRelations()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid orgId = Guid.CreateVersion7();
        Guid courseId = Guid.CreateVersion7();
        Guid learningComponentId = Guid.CreateVersion7();
        Guid offeringId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            OrganisationEntity org = new()
            {
                Id = orgId,
                OrganisationId = orgId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"NEG-ORG2-{orgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Nested Gap Expand Org\"}]"
            };
            dbContext.Organisations.Add(org);

            CourseEntity course = new()
            {
                Id = courseId,
                CourseId = courseId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"NEG-CRS2-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Nested Gap Expand Course\"}]"
            };
            dbContext.Courses.Add(course);

            LearningComponentEntity learningComponent = new()
            {
                Id = learningComponentId,
                ComponentId = learningComponentId.ToString(),
                ComponentType = "lecture",
                PrimaryCodeType = "identifier",
                PrimaryCode = $"NEG-LC2-{learningComponentId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Nested Gap Expand Component\"}]",
                CourseEntityId = course.Id,
                OrganisationEntityId = org.Id
            };
            dbContext.LearningComponents.Add(learningComponent);

            // A linked academic session is required for this offering to appear at all under the new
            // since=<today> default (add-since-default-today-filter) - since always applies now
            // (defaulted when omitted), and an offering with no linked session is unconditionally
            // excluded by that filter (fix-since-until-academic-session-target's own established
            // "excluded, not a 500" precedent) - not specific to this test's expand behaviour.
            Guid sessionId = Guid.CreateVersion7();
            AcademicSessionEntity session = new()
            {
                Id = sessionId,
                AcademicSessionId = sessionId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"NEG-AS2-{sessionId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Nested Gap Expand Session\"}]",
                AcademicSessionType = "semester",
                StartDateTime = "2020-09-01T00:00:00+01:00",
                EndDateTime = "2020-12-01T00:00:00+01:00"
            };
            dbContext.AcademicSessions.Add(session);

            LearningComponentOfferingEntity offering = new()
            {
                Id = offeringId,
                LearningComponentOfferingIdValue = offeringId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"NEG-LCO2-{offeringId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Nested Gap Expand Offering\"}]",
                LearningComponentEntityId = learningComponent.Id,
                OrganisationEntityId = org.Id,
                AcademicSessionEntityId = sessionId
            };
            dbContext.LearningComponentOfferings.Add(offering);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        // The linked session predates "today" - bypass the since=<today> default with an explicit
        // early since=, unrelated to what this test actually checks (expand).
        JsonObject json = await GetJsonAsync(client,
            $"/courses/{courseId}/learning-component-offerings" +
            "?expand=learning_component,organisation&since=2000-01-01T00:00:00%2B00:00");
        JsonObject item = json["items"]!.AsArray().Single()!.AsObject();

        Assert.NotNull(item["learningComponent"]);
        Assert.Equal(learningComponentId.ToString(), item["learningComponent"]!["componentId"]!.GetValue<string>());
        Assert.NotNull(item["organisation"]);
        Assert.Equal(orgId.ToString(), item["organisation"]!["organisationId"]!.GetValue<string>());
    }

    [Fact]
    public async Task CourseOfferingAssociationsByPersonId_RoleAndResultStateFilters_RestrictResults()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid orgId = Guid.CreateVersion7();
        Guid personId = Guid.CreateVersion7();
        Guid courseId = Guid.CreateVersion7();
        Guid academicSessionId = Guid.CreateVersion7();
        Guid courseOfferingId = Guid.CreateVersion7();
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
                PrimaryCode = $"NEG-ORG3-{orgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Nested Gap Assoc Org\"}]"
            };
            dbContext.Organisations.Add(org);

            PersonEntity person = new()
            {
                Id = personId,
                PersonId = personId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"NEG-PER-{personId.ToString()[..8]}",
                GivenName = "Nested",
                Surname = "Gap"
            };
            dbContext.Persons.Add(person);

            CourseEntity course = new()
            {
                Id = courseId,
                CourseId = courseId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"NEG-CRS3-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Nested Gap Assoc Course\"}]"
            };
            dbContext.Courses.Add(course);

            AcademicSessionEntity academicSession = new()
            {
                Id = academicSessionId,
                AcademicSessionId = academicSessionId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"NEG-AS-{academicSessionId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Nested Gap Assoc Session\"}]",
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
                PrimaryCode = $"NEG-CO-{courseOfferingId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Nested Gap Assoc Offering\"}]",
                CourseEntityId = course.Id,
                OrganisationEntityId = org.Id,
                AcademicSessionEntityId = academicSession.Id
            };
            dbContext.CourseOfferings.Add(courseOffering);

            CourseOfferingAssociationEntity matching = new()
            {
                Id = matchAssocId,
                CourseOfferingAssociationIdValue = matchAssocId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"NEG-COA-MATCH-{matchAssocId.ToString()[..8]}",
                Role = "student",
                CourseOfferingEntityId = courseOffering.Id,
                PersonEntityId = person.Id,
                OrganisationEntityId = org.Id,
                ResultJson = "{\"state\":\"submitted\",\"resultDateTime\":\"2026-01-15T00:00:00+01:00\"}"
            };
            dbContext.CourseOfferingAssociations.Add(matching);

            CourseOfferingAssociationEntity otherRole = new()
            {
                Id = otherRoleAssocId,
                CourseOfferingAssociationIdValue = otherRoleAssocId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"NEG-COA-ROLE-{otherRoleAssocId.ToString()[..8]}",
                Role = "instructor",
                CourseOfferingEntityId = courseOffering.Id,
                PersonEntityId = person.Id,
                OrganisationEntityId = org.Id,
                ResultJson = "{\"state\":\"submitted\",\"resultDateTime\":\"2026-01-15T00:00:00+01:00\"}"
            };
            dbContext.CourseOfferingAssociations.Add(otherRole);

            CourseOfferingAssociationEntity otherResult = new()
            {
                Id = otherResultAssocId,
                CourseOfferingAssociationIdValue = otherResultAssocId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"NEG-COA-RESULT-{otherResultAssocId.ToString()[..8]}",
                Role = "student",
                CourseOfferingEntityId = courseOffering.Id,
                PersonEntityId = person.Id,
                OrganisationEntityId = org.Id,
                ResultJson = "{\"state\":\"pending\",\"resultDateTime\":\"2026-01-15T00:00:00+01:00\"}"
            };
            dbContext.CourseOfferingAssociations.Add(otherResult);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        JsonObject roleFiltered =
            await GetJsonAsync(client, $"/persons/{personId}/course-offering-associations?role=student");
        JsonArray roleItems = roleFiltered["items"]!.AsArray();
        Assert.Contains(roleItems, i => i!["associationId"]!.GetValue<string>() == matchAssocId.ToString());
        Assert.Contains(roleItems, i => i!["associationId"]!.GetValue<string>() == otherResultAssocId.ToString());
        Assert.DoesNotContain(roleItems, i => i!["associationId"]!.GetValue<string>() == otherRoleAssocId.ToString());

        JsonObject resultStateFiltered =
            await GetJsonAsync(client, $"/persons/{personId}/course-offering-associations?resultState=submitted");
        JsonArray resultStateItems = resultStateFiltered["items"]!.AsArray();
        Assert.Contains(resultStateItems, i => i!["associationId"]!.GetValue<string>() == matchAssocId.ToString());
        Assert.Contains(resultStateItems, i => i!["associationId"]!.GetValue<string>() == otherRoleAssocId.ToString());
        Assert.DoesNotContain(resultStateItems, i => i!["associationId"]!.GetValue<string>() == otherResultAssocId.ToString());
    }

    [Fact]
    public async Task CourseOfferingAssociationsByPersonId_Expand_PopulatesCourseOfferingAndAcademicSession()
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
                PrimaryCode = $"NEG-ORG4-{orgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Nested Gap Expand Assoc Org\"}]"
            };
            dbContext.Organisations.Add(org);

            PersonEntity person = new()
            {
                Id = personId,
                PersonId = personId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"NEG-PER2-{personId.ToString()[..8]}",
                GivenName = "Nested",
                Surname = "GapExpand"
            };
            dbContext.Persons.Add(person);

            CourseEntity course = new()
            {
                Id = courseId,
                CourseId = courseId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"NEG-CRS4-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Nested Gap Expand Assoc Course\"}]"
            };
            dbContext.Courses.Add(course);

            AcademicSessionEntity academicSession = new()
            {
                Id = academicSessionId,
                AcademicSessionId = academicSessionId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"NEG-AS2-{academicSessionId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Nested Gap Expand Assoc Session\"}]",
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
                PrimaryCode = $"NEG-CO2-{courseOfferingId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Nested Gap Expand Assoc Offering\"}]",
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
                PrimaryCode = $"NEG-COA-EXP-{assocId.ToString()[..8]}",
                Role = "student",
                CourseOfferingEntityId = courseOffering.Id,
                PersonEntityId = person.Id,
                OrganisationEntityId = org.Id
            };
            dbContext.CourseOfferingAssociations.Add(association);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        // Without expand: academicSession key must be entirely absent (JsonIgnore WhenWritingNull).
        JsonObject withoutExpand =
            await GetJsonAsync(client, $"/persons/{personId}/course-offering-associations");
        JsonObject itemWithoutExpand = withoutExpand["items"]!.AsArray().Single()!.AsObject();
        Assert.False(itemWithoutExpand.ContainsKey("academicSession"));

        // With expand: both courseOffering and academicSession are populated.
        JsonObject withExpand = await GetJsonAsync(client,
            $"/persons/{personId}/course-offering-associations?expand=course_offering,academic_session");
        JsonObject itemWithExpand = withExpand["items"]!.AsArray().Single()!.AsObject();

        Assert.NotNull(itemWithExpand["courseOffering"]);
        Assert.Equal(courseOfferingId.ToString(), itemWithExpand["courseOffering"]!["courseOfferingId"]!.GetValue<string>());
        Assert.NotNull(itemWithExpand["academicSession"]);
        Assert.Equal(academicSessionId.ToString(), itemWithExpand["academicSession"]!["academicSessionId"]!.GetValue<string>());

        // Regression guard: the canonical single-item endpoint for the same association never gets
        // academicSession, regardless of expand - it's declared only on the nested-by-person response.
        JsonObject canonical = await GetJsonAsync(client,
            $"/course-offering-associations/{assocId}?expand=academic_session");
        Assert.False(canonical.ContainsKey("academicSession"));
    }

    private static async Task<JsonObject> GetJsonAsync(HttpClient client, string requestUri)
    {
        HttpResponseMessage response = await client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        return JsonNode.Parse(await response.Content.ReadAsStringAsync())!.AsObject();
    }
}
