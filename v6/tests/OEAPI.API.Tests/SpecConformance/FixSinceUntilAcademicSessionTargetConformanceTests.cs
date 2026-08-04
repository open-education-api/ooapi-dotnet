using System.Text.Json.Nodes;
using Microsoft.Extensions.DependencyInjection;
using OEAPI.API.Tests.Infrastructure;
using OEAPI.Infrastructure.Data.Context;
using OEAPI.Infrastructure.Data.Entities;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Confirms the fix-since-until-academic-session-target fixes actually take effect: since/until on
///     offering-collection endpoints filter by the linked academic session's own dates (not the
///     offering's own), a null-safety guard keeps an offering with no linked session from crashing the
///     request, and a differently-formatted-but-equal-instant value still matches. Follows the same
///     seed-via-<see cref="SqlServerOEAPIDbContext" />-then-real-HTTP-request pattern as
///     <see cref="FixResponseFieldAndParameterBugsConformanceTests" />.
/// </summary>
[Collection(SqlServerCollection.Name)]
public class FixSinceUntilAcademicSessionTargetConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Fact]
    public async Task CourseLearningComponentOfferings_SinceUntil_FilterByAcademicSessionNotOwnDates()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid orgId = Guid.CreateVersion7();
        Guid courseId = Guid.CreateVersion7();
        Guid learningComponentId = Guid.CreateVersion7();
        Guid sessionInsideRangeId = Guid.CreateVersion7();
        Guid sessionOutsideRangeId = Guid.CreateVersion7();
        Guid offeringOwnDatesOutsideId = Guid.CreateVersion7();
        Guid offeringOwnDatesInsideId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            OrganisationEntity org = new()
            {
                Id = orgId,
                OrganisationId = orgId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SUT-ORG-{orgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"SinceUntil Org\"}]"
            };
            dbContext.Organisations.Add(org);

            CourseEntity course = new()
            {
                Id = courseId,
                CourseId = courseId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SUT-CRS-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"SinceUntil Course\"}]"
            };
            dbContext.Courses.Add(course);

            LearningComponentEntity learningComponent = new()
            {
                Id = learningComponentId,
                ComponentId = learningComponentId.ToString(),
                ComponentType = "lecture",
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SUT-LC-{learningComponentId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"SinceUntil Component\"}]",
                CourseEntityId = course.Id,
                OrganisationEntityId = org.Id
            };
            dbContext.LearningComponents.Add(learningComponent);

            // Filter window used below: since=2025-01-01, until=2026-01-01.
            dbContext.AcademicSessions.Add(new AcademicSessionEntity
            {
                Id = sessionInsideRangeId,
                AcademicSessionId = sessionInsideRangeId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SUT-AS-IN-{sessionInsideRangeId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Session Inside Range\"}]",
                AcademicSessionType = "semester",
                StartDateTime = "2025-09-01T00:00:00+01:00",
                EndDateTime = "2025-12-01T00:00:00+01:00"
            });
            dbContext.AcademicSessions.Add(new AcademicSessionEntity
            {
                Id = sessionOutsideRangeId,
                AcademicSessionId = sessionOutsideRangeId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SUT-AS-OUT-{sessionOutsideRangeId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Session Outside Range\"}]",
                AcademicSessionType = "semester",
                StartDateTime = "2020-09-01T00:00:00+01:00",
                EndDateTime = "2020-12-01T00:00:00+01:00"
            });

            // Offering's OWN dates are OUTSIDE the filter window, but its linked session's dates are
            // INSIDE - must be INCLUDED under the fix (was excluded under the old, wrong field target).
            dbContext.LearningComponentOfferings.Add(new LearningComponentOfferingEntity
            {
                Id = offeringOwnDatesOutsideId,
                LearningComponentOfferingIdValue = offeringOwnDatesOutsideId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SUT-LCO-A-{offeringOwnDatesOutsideId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Own Dates Outside, Session Inside\"}]",
                LearningComponentEntityId = learningComponent.Id,
                OrganisationEntityId = org.Id,
                AcademicSessionEntityId = sessionInsideRangeId,
                StartDateTime = "2020-09-01T00:00:00+01:00",
                EndDateTime = "2020-12-01T00:00:00+01:00"
            });

            // Offering's OWN dates are INSIDE the filter window, but its linked session's dates are
            // OUTSIDE - must be EXCLUDED under the fix (was included under the old, wrong field
            // target).
            dbContext.LearningComponentOfferings.Add(new LearningComponentOfferingEntity
            {
                Id = offeringOwnDatesInsideId,
                LearningComponentOfferingIdValue = offeringOwnDatesInsideId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SUT-LCO-B-{offeringOwnDatesInsideId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Own Dates Inside, Session Outside\"}]",
                LearningComponentEntityId = learningComponent.Id,
                OrganisationEntityId = org.Id,
                AcademicSessionEntityId = sessionOutsideRangeId,
                StartDateTime = "2025-09-01T00:00:00+01:00",
                EndDateTime = "2025-12-01T00:00:00+01:00"
            });

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        JsonObject json = await GetJsonAsync(client,
            $"/courses/{courseId}/learning-component-offerings" +
            "?since=2025-01-01T00:00:00%2B01:00&until=2026-01-01T00:00:00%2B01:00");
        JsonArray items = json["items"]!.AsArray();

        Assert.Contains(items,
            i => i!["learningComponentOfferingId"]!.GetValue<string>() == offeringOwnDatesOutsideId.ToString());
        Assert.DoesNotContain(items,
            i => i!["learningComponentOfferingId"]!.GetValue<string>() == offeringOwnDatesInsideId.ToString());
    }

    [Fact]
    public async Task AcademicSessionLearningComponentOfferings_SinceUntil_CompareAgainstTheSessionItself()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid orgId = Guid.CreateVersion7();
        Guid courseId = Guid.CreateVersion7();
        Guid learningComponentId = Guid.CreateVersion7();
        Guid sessionId = Guid.CreateVersion7();
        Guid offeringId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            OrganisationEntity org = new()
            {
                Id = orgId,
                OrganisationId = orgId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SUT-ORG2-{orgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"SinceUntil Org2\"}]"
            };
            dbContext.Organisations.Add(org);

            CourseEntity course = new()
            {
                Id = courseId,
                CourseId = courseId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SUT-CRS2-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"SinceUntil Course2\"}]"
            };
            dbContext.Courses.Add(course);

            LearningComponentEntity learningComponent = new()
            {
                Id = learningComponentId,
                ComponentId = learningComponentId.ToString(),
                ComponentType = "lecture",
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SUT-LC2-{learningComponentId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"SinceUntil Component2\"}]",
                CourseEntityId = course.Id,
                OrganisationEntityId = org.Id
            };
            dbContext.LearningComponents.Add(learningComponent);

            AcademicSessionEntity session = new()
            {
                Id = sessionId,
                AcademicSessionId = sessionId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SUT-AS2-{sessionId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Nested Endpoint Session\"}]",
                AcademicSessionType = "semester",
                StartDateTime = "2025-09-01T00:00:00+01:00",
                EndDateTime = "2025-12-01T00:00:00+01:00"
            };
            dbContext.AcademicSessions.Add(session);

            dbContext.LearningComponentOfferings.Add(new LearningComponentOfferingEntity
            {
                Id = offeringId,
                LearningComponentOfferingIdValue = offeringId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SUT-LCO2-{offeringId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Nested Endpoint Offering\"}]",
                LearningComponentEntityId = learningComponent.Id,
                OrganisationEntityId = org.Id,
                AcademicSessionEntityId = sessionId
            });

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        // since= exactly the session's own start moment - all-or-nothing at the session level (see
        // design.md Decision 2), so the offering is included.
        JsonObject matching = await GetJsonAsync(client,
            $"/academic-sessions/{sessionId}/learning-component-offerings?since=2025-09-01T00:00:00%2B01:00");
        Assert.Contains(matching["items"]!.AsArray(),
            i => i!["learningComponentOfferingId"]!.GetValue<string>() == offeringId.ToString());

        // since= after the session's own start moment - the whole session fails the filter, so the
        // page is empty even though the offering itself would otherwise match.
        JsonObject nonMatching = await GetJsonAsync(client,
            $"/academic-sessions/{sessionId}/learning-component-offerings?since=2025-10-01T00:00:00%2B01:00");
        Assert.Empty(nonMatching["items"]!.AsArray());
    }

    [Fact]
    public async Task CourseLearningComponentOfferings_SinceUntil_DifferentPrecisionSameInstantStillMatches()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid orgId = Guid.CreateVersion7();
        Guid courseId = Guid.CreateVersion7();
        Guid learningComponentId = Guid.CreateVersion7();
        Guid sessionId = Guid.CreateVersion7();
        Guid offeringId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            OrganisationEntity org = new()
            {
                Id = orgId,
                OrganisationId = orgId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SUT-ORG3-{orgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"SinceUntil Org3\"}]"
            };
            dbContext.Organisations.Add(org);

            CourseEntity course = new()
            {
                Id = courseId,
                CourseId = courseId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SUT-CRS3-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"SinceUntil Course3\"}]"
            };
            dbContext.Courses.Add(course);

            LearningComponentEntity learningComponent = new()
            {
                Id = learningComponentId,
                ComponentId = learningComponentId.ToString(),
                ComponentType = "lecture",
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SUT-LC3-{learningComponentId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"SinceUntil Component3\"}]",
                CourseEntityId = course.Id,
                OrganisationEntityId = org.Id
            };
            dbContext.LearningComponents.Add(learningComponent);

            // Stored exactly as this codebase's own convention: no fractional seconds.
            AcademicSessionEntity session = new()
            {
                Id = sessionId,
                AcademicSessionId = sessionId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SUT-AS3-{sessionId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Precision Session\"}]",
                AcademicSessionType = "semester",
                StartDateTime = "2025-09-01T00:00:00+01:00",
                EndDateTime = "2025-12-01T00:00:00+01:00"
            };
            dbContext.AcademicSessions.Add(session);

            dbContext.LearningComponentOfferings.Add(new LearningComponentOfferingEntity
            {
                Id = offeringId,
                LearningComponentOfferingIdValue = offeringId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SUT-LCO3-{offeringId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Precision Offering\"}]",
                LearningComponentEntityId = learningComponent.Id,
                OrganisationEntityId = org.Id,
                AcademicSessionEntityId = sessionId
            });

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        // Same instant as the session's own StartDateTime, but with fractional-second precision the
        // stored value doesn't have (as a client re-serializing a parsed DateTimeOffset commonly
        // would) - must still match after normalization.
        JsonObject json = await GetJsonAsync(client,
            $"/courses/{courseId}/learning-component-offerings?since=2025-09-01T00:00:00.0000000%2B01:00");
        Assert.Contains(json["items"]!.AsArray(),
            i => i!["learningComponentOfferingId"]!.GetValue<string>() == offeringId.ToString());
    }

    [Fact]
    public async Task CourseLearningComponentOfferings_SinceUntil_OfferingWithNoLinkedSessionIsExcludedNot500()
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
                PrimaryCode = $"SUT-ORG4-{orgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"SinceUntil Org4\"}]"
            };
            dbContext.Organisations.Add(org);

            CourseEntity course = new()
            {
                Id = courseId,
                CourseId = courseId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SUT-CRS4-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"SinceUntil Course4\"}]"
            };
            dbContext.Courses.Add(course);

            LearningComponentEntity learningComponent = new()
            {
                Id = learningComponentId,
                ComponentId = learningComponentId.ToString(),
                ComponentType = "lecture",
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SUT-LC4-{learningComponentId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"SinceUntil Component4\"}]",
                CourseEntityId = course.Id,
                OrganisationEntityId = org.Id
            };
            dbContext.LearningComponents.Add(learningComponent);

            // Deliberately no AcademicSessionEntityId set.
            dbContext.LearningComponentOfferings.Add(new LearningComponentOfferingEntity
            {
                Id = offeringId,
                LearningComponentOfferingIdValue = offeringId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"SUT-LCO4-{offeringId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"No Session Offering\"}]",
                LearningComponentEntityId = learningComponent.Id,
                OrganisationEntityId = org.Id
            });

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        HttpResponseMessage response = await client.GetAsync(
            $"/courses/{courseId}/learning-component-offerings?since=2025-01-01T00:00:00%2B01:00");
        response.EnsureSuccessStatusCode();

        JsonObject json = JsonNode.Parse(await response.Content.ReadAsStringAsync())!.AsObject();
        Assert.DoesNotContain(json["items"]!.AsArray(),
            i => i!["learningComponentOfferingId"]!.GetValue<string>() == offeringId.ToString());
    }

    private static async Task<JsonObject> GetJsonAsync(HttpClient client, string requestUri)
    {
        HttpResponseMessage response = await client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        return JsonNode.Parse(await response.Content.ReadAsStringAsync())!.AsObject();
    }
}
