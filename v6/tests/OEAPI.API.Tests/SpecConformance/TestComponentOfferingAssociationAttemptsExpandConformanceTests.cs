using System.Text.Json.Nodes;
using Microsoft.Extensions.DependencyInjection;
using OEAPI.API.Tests.Infrastructure;
using OEAPI.Infrastructure.Data.Context;
using OEAPI.Infrastructure.Data.Entities;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Guards a real functional gap found and fixed while auditing
///     `extend-demo-data-seeder-coverage-phase-2` section 10:
///     `TestComponentOfferingAssociationsController.ApplyExpandAsync` had no <c>"attempts"</c> case at
///     all, so <c>?expand=attempts</c> was silently a no-op regardless of what was requested -
///     <c>attemptIds</c> already resolved correctly via the real <c>Attempts</c> relationship, only the
///     full expanded objects were never wired up. Only ever caught by live `curl` against
///     `DemoDataSeeder.cs` output during that audit - this locks the same assertion into a
///     `dotnet test` run instead.
/// </summary>
[Collection(SqlServerCollection.Name)]
public class TestComponentOfferingAssociationAttemptsExpandConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Fact]
    public async Task ExpandAttempts_ReturnsFullAttemptObjects_NotJustIds()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid orgId = Guid.CreateVersion7();
        Guid courseId = Guid.CreateVersion7();
        Guid testComponentId = Guid.CreateVersion7();
        Guid academicSessionId = Guid.CreateVersion7();
        Guid testComponentOfferingId = Guid.CreateVersion7();
        Guid personId = Guid.CreateVersion7();
        Guid associationId = Guid.CreateVersion7();
        Guid attemptId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            OrganisationEntity org = new()
            {
                Id = orgId,
                OrganisationId = orgId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"ATTX-ORG-{orgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"AttemptsExpand Org\"}]"
            };
            dbContext.Organisations.Add(org);

            CourseEntity course = new()
            {
                Id = courseId,
                CourseId = courseId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"ATTX-CRS-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"AttemptsExpand Course\"}]"
            };
            dbContext.Courses.Add(course);

            TestComponentEntity testComponent = new()
            {
                Id = testComponentId,
                ComponentId = testComponentId.ToString(),
                ComponentType = "digital_test",
                PrimaryCodeType = "identifier",
                PrimaryCode = $"ATTX-TC-{testComponentId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"AttemptsExpand Component\"}]",
                CourseEntityId = course.Id,
                OrganisationEntityId = org.Id
            };
            dbContext.TestComponents.Add(testComponent);

            AcademicSessionEntity academicSession = new()
            {
                Id = academicSessionId,
                AcademicSessionId = academicSessionId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"ATTX-AS-{academicSessionId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"AttemptsExpand Session\"}]",
                AcademicSessionType = "semester",
                StartDateTime = "2025-09-01T00:00:00+01:00",
                EndDateTime = "2026-01-31T23:59:59+01:00"
            };
            dbContext.AcademicSessions.Add(academicSession);

            TestComponentOfferingEntity testComponentOffering = new()
            {
                Id = testComponentOfferingId,
                TestComponentOfferingIdValue = testComponentOfferingId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"ATTX-TCO-{testComponentOfferingId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"AttemptsExpand Offering\"}]",
                TestComponentEntityId = testComponent.Id,
                OrganisationEntityId = org.Id,
                AcademicSessionEntityId = academicSession.Id
            };
            dbContext.TestComponentOfferings.Add(testComponentOffering);

            PersonEntity person = new()
            {
                Id = personId,
                PersonId = personId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"ATTX-PER-{personId.ToString()[..8]}",
                GivenName = "Attemptee"
            };
            dbContext.Persons.Add(person);

            TestComponentOfferingAssociationEntity associationEntity = new()
            {
                Id = associationId,
                TestComponentOfferingAssociationIdValue = associationId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"ATTX-TCOA-{associationId.ToString()[..8]}",
                Role = "student",
                State = "associated",
                TestComponentOfferingEntityId = testComponentOffering.Id,
                PersonEntityId = person.Id,
                OrganisationEntityId = org.Id
            };
            dbContext.TestComponentOfferingAssociations.Add(associationEntity);

            dbContext.TestComponentOfferingAssociationAttempts.Add(new TestComponentOfferingAssociationAttemptEntity
            {
                Id = attemptId,
                AttemptIdValue = attemptId.ToString(),
                Opportunity = "2025Semester1",
                Attempt = 1,
                State = "finished",
                TestComponentOfferingAssociationEntityId = associationEntity.Id
            });

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        HttpResponseMessage response =
            await client.GetAsync($"/test-component-offering-associations/{associationId}?expand=attempts");
        response.EnsureSuccessStatusCode();
        JsonObject association = JsonNode.Parse(await response.Content.ReadAsStringAsync())!.AsObject();

        // attemptIds already worked before the fix - the bug was specifically that the full expanded
        // objects never got populated.
        Assert.Contains(association["attemptIds"]!.AsArray(), a => a!.GetValue<string>() == attemptId.ToString());

        JsonArray attempts = association["attempts"]!.AsArray();
        Assert.Single(attempts);
        Assert.Equal(attemptId.ToString(), attempts[0]!["attemptId"]!.GetValue<string>());
        Assert.Equal("2025Semester1", attempts[0]!["opportunity"]!.GetValue<string>());
    }
}
