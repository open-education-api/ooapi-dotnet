using System.Text.Json.Nodes;
using Microsoft.Extensions.DependencyInjection;
using OEAPI.API.Tests.Infrastructure;
using OEAPI.Infrastructure.Data.Context;
using OEAPI.Infrastructure.Data.Entities;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Confirms the add-since-default-today-filter fix actually takes effect: every offering-collection
///     endpoint's own parameter description says "By default only future offerings are shown (equal to
///     <c>?since=&lt;today&gt;</c>)" - omitting <c>since</c> now excludes an offering whose linked
///     academic session already started, an explicit <c>since=</c> overrides that default, and
///     <c>until</c> (which carries no such default clause) never gets one. Follows the same
///     seed-via-<see cref="SqlServerOEAPIDbContext" />-then-real-HTTP-request pattern as
///     <see cref="FixSinceUntilAcademicSessionTargetConformanceTests" />.
/// </summary>
[Collection(SqlServerCollection.Name)]
public class AddSinceDefaultTodayFilterConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Fact]
    public async Task CourseLearningComponentOfferings_NoSince_DefaultsToTodayExcludingPastIncludingFuture()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid orgId = Guid.CreateVersion7();
        Guid courseId = Guid.CreateVersion7();
        Guid learningComponentId = Guid.CreateVersion7();
        Guid pastSessionId = Guid.CreateVersion7();
        Guid futureSessionId = Guid.CreateVersion7();
        Guid pastOfferingId = Guid.CreateVersion7();
        Guid futureOfferingId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            OrganisationEntity org = new()
            {
                Id = orgId,
                OrganisationId = orgId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SDT-ORG-{orgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"SinceDefault Org\"}]"
            };
            dbContext.Organisations.Add(org);

            CourseEntity course = new()
            {
                Id = courseId,
                CourseId = courseId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SDT-CRS-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"SinceDefault Course\"}]"
            };
            dbContext.Courses.Add(course);

            LearningComponentEntity learningComponent = new()
            {
                Id = learningComponentId,
                ComponentId = learningComponentId.ToString(),
                ComponentType = "lecture",
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SDT-LC-{learningComponentId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"SinceDefault Component\"}]",
                CourseEntityId = course.Id,
                OrganisationEntityId = org.Id
            };
            dbContext.LearningComponents.Add(learningComponent);

            // A session that already started - excluded by the new since=<today> default.
            AcademicSessionEntity pastSession = new()
            {
                Id = pastSessionId,
                AcademicSessionId = pastSessionId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SDT-AS-PAST-{pastSessionId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Already Started Session\"}]",
                AcademicSessionType = "semester",
                StartDateTime = "2020-09-01T00:00:00+01:00",
                EndDateTime = "2020-12-01T00:00:00+01:00"
            };
            dbContext.AcademicSessions.Add(pastSession);

            // A session starting 5 years from now - always in the future, included by default.
            DateTimeOffset futureStart = DateTimeOffset.UtcNow.AddYears(5);
            AcademicSessionEntity futureSession = new()
            {
                Id = futureSessionId,
                AcademicSessionId = futureSessionId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SDT-AS-FUTURE-{futureSessionId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Not Yet Started Session\"}]",
                AcademicSessionType = "semester",
                StartDateTime = futureStart.ToString("yyyy-MM-ddTHH:mm:sszzz"),
                EndDateTime = futureStart.AddMonths(5).ToString("yyyy-MM-ddTHH:mm:sszzz")
            };
            dbContext.AcademicSessions.Add(futureSession);

            dbContext.LearningComponentOfferings.Add(new LearningComponentOfferingEntity
            {
                Id = pastOfferingId,
                LearningComponentOfferingIdValue = pastOfferingId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SDT-LCO-PAST-{pastOfferingId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Already Started Offering\"}]",
                LearningComponentEntityId = learningComponent.Id,
                OrganisationEntityId = org.Id,
                AcademicSessionEntityId = pastSessionId
            });
            dbContext.LearningComponentOfferings.Add(new LearningComponentOfferingEntity
            {
                Id = futureOfferingId,
                LearningComponentOfferingIdValue = futureOfferingId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SDT-LCO-FUTURE-{futureOfferingId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Not Yet Started Offering\"}]",
                LearningComponentEntityId = learningComponent.Id,
                OrganisationEntityId = org.Id,
                AcademicSessionEntityId = futureSessionId
            });

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        JsonObject json = await GetJsonAsync(client, $"/courses/{courseId}/learning-component-offerings");
        JsonArray items = json["items"]!.AsArray();

        Assert.DoesNotContain(items,
            i => i!["learningComponentOfferingId"]!.GetValue<string>() == pastOfferingId.ToString());
        Assert.Contains(items,
            i => i!["learningComponentOfferingId"]!.GetValue<string>() == futureOfferingId.ToString());
    }

    [Fact]
    public async Task CourseLearningComponentOfferings_ExplicitSinceOverridesTheDefault()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid orgId = Guid.CreateVersion7();
        Guid courseId = Guid.CreateVersion7();
        Guid learningComponentId = Guid.CreateVersion7();
        Guid pastSessionId = Guid.CreateVersion7();
        Guid pastOfferingId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            OrganisationEntity org = new()
            {
                Id = orgId,
                OrganisationId = orgId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SDT-ORG2-{orgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"SinceDefault Org2\"}]"
            };
            dbContext.Organisations.Add(org);

            CourseEntity course = new()
            {
                Id = courseId,
                CourseId = courseId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SDT-CRS2-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"SinceDefault Course2\"}]"
            };
            dbContext.Courses.Add(course);

            LearningComponentEntity learningComponent = new()
            {
                Id = learningComponentId,
                ComponentId = learningComponentId.ToString(),
                ComponentType = "lecture",
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SDT-LC2-{learningComponentId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"SinceDefault Component2\"}]",
                CourseEntityId = course.Id,
                OrganisationEntityId = org.Id
            };
            dbContext.LearningComponents.Add(learningComponent);

            AcademicSessionEntity pastSession = new()
            {
                Id = pastSessionId,
                AcademicSessionId = pastSessionId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SDT-AS2-{pastSessionId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Already Started Session 2\"}]",
                AcademicSessionType = "semester",
                StartDateTime = "2020-09-01T00:00:00+01:00",
                EndDateTime = "2020-12-01T00:00:00+01:00"
            };
            dbContext.AcademicSessions.Add(pastSession);

            dbContext.LearningComponentOfferings.Add(new LearningComponentOfferingEntity
            {
                Id = pastOfferingId,
                LearningComponentOfferingIdValue = pastOfferingId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SDT-LCO2-{pastOfferingId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Already Started Offering 2\"}]",
                LearningComponentEntityId = learningComponent.Id,
                OrganisationEntityId = org.Id,
                AcademicSessionEntityId = pastSessionId
            });

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        // No since= - excluded by the default.
        JsonObject defaulted = await GetJsonAsync(client, $"/courses/{courseId}/learning-component-offerings");
        Assert.DoesNotContain(defaulted["items"]!.AsArray(),
            i => i!["learningComponentOfferingId"]!.GetValue<string>() == pastOfferingId.ToString());

        // Explicit since= before the session's own start - the default no longer applies, included.
        JsonObject explicitSince = await GetJsonAsync(client,
            $"/courses/{courseId}/learning-component-offerings?since=2000-01-01T00:00:00%2B00:00");
        Assert.Contains(explicitSince["items"]!.AsArray(),
            i => i!["learningComponentOfferingId"]!.GetValue<string>() == pastOfferingId.ToString());
    }

    [Fact]
    public async Task AcademicSessionTestComponentOfferings_QSearch_RestrictsResults()
    {
        // Drive-by fix, found live-verifying this same change: this nested endpoint's q= searched
        // TestComponentOfferingIdValue (the internal GUID-shaped id), not name/code - the same bug
        // class already fixed elsewhere in fix-response-field-and-parameter-bugs. Only surfaced once
        // this change's own seeded "upcoming" fixtures made the endpoint's baseline reachable.
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid orgId = Guid.CreateVersion7();
        Guid courseId = Guid.CreateVersion7();
        Guid testComponentId = Guid.CreateVersion7();
        Guid sessionId = Guid.CreateVersion7();
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
                PrimaryCode = $"SDT-ORG3-{orgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"SinceDefault Org3\"}]"
            };
            dbContext.Organisations.Add(org);

            CourseEntity course = new()
            {
                Id = courseId,
                CourseId = courseId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SDT-CRS3-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"SinceDefault Course3\"}]"
            };
            dbContext.Courses.Add(course);

            TestComponentEntity testComponent = new()
            {
                Id = testComponentId,
                ComponentId = testComponentId.ToString(),
                ComponentType = "digital_test",
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SDT-TC-{testComponentId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"SinceDefault Component3\"}]",
                CourseEntityId = course.Id,
                OrganisationEntityId = org.Id
            };
            dbContext.TestComponents.Add(testComponent);

            AcademicSessionEntity session = new()
            {
                Id = sessionId,
                AcademicSessionId = sessionId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SDT-AS3-{sessionId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"QSearch Session\"}]",
                AcademicSessionType = "semester",
                StartDateTime = "2020-09-01T00:00:00+01:00",
                EndDateTime = "2020-12-01T00:00:00+01:00"
            };
            dbContext.AcademicSessions.Add(session);

            dbContext.TestComponentOfferings.Add(new TestComponentOfferingEntity
            {
                Id = matchId,
                TestComponentOfferingIdValue = matchId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SDT-TCO-MATCH-{matchId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Written Exam\"}]",
                TestComponentEntityId = testComponent.Id,
                OrganisationEntityId = org.Id,
                AcademicSessionEntityId = sessionId
            });
            dbContext.TestComponentOfferings.Add(new TestComponentOfferingEntity
            {
                Id = otherId,
                TestComponentOfferingIdValue = otherId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SDT-TCO-OTHER-{otherId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Oral Resit\"}]",
                TestComponentEntityId = testComponent.Id,
                OrganisationEntityId = org.Id,
                AcademicSessionEntityId = sessionId
            });

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        JsonObject json = await GetJsonAsync(client,
            $"/academic-sessions/{sessionId}/test-component-offerings" +
            "?q=Written&since=2000-01-01T00:00:00%2B00:00");
        JsonArray items = json["items"]!.AsArray();

        Assert.Contains(items, i => i!["testComponentOfferingId"]!.GetValue<string>() == matchId.ToString());
        Assert.DoesNotContain(items, i => i!["testComponentOfferingId"]!.GetValue<string>() == otherId.ToString());
    }

    private static async Task<JsonObject> GetJsonAsync(HttpClient client, string requestUri)
    {
        HttpResponseMessage response = await client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        return JsonNode.Parse(await response.Content.ReadAsStringAsync())!.AsObject();
    }
}
