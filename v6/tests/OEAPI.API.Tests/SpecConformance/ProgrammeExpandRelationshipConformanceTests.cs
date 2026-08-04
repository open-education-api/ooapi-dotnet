using System.Text.Json.Nodes;
using Microsoft.Extensions.DependencyInjection;
using OEAPI.API.Tests.Infrastructure;
using OEAPI.Infrastructure.Data.Context;
using OEAPI.Infrastructure.Data.Entities;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Guards three bugs found and fixed in `ProgrammesController`: (1) `ApplyExpandAsync` fetched its
///     working entity via a raw, Include-less <c>_dbContext.Programmes.AsNoTracking()</c> query, so
///     `children`/`coordinators`/`instructors` were always empty regardless of what was requested,
///     found while auditing `extend-demo-data-seeder-coverage-phase-2` section 4; (2) a mapped
///     `parent`/`children` programme's own `organisationId` came back as an empty string rather than its
///     real value, because `ProgrammeMappingExtensions.ToApiModel` reads
///     `entity.Organisation?.OrganisationId` (the navigation object, not the raw FK) and `Parent`/
///     `Children`'s own `Organisation` navigation wasn't eagerly loaded either, found in the same audit;
///     (3) `ApplyExpandAsync` had no `"learningoutcomes"` case at all - `learningOutcomeIds` resolved
///     correctly (the relationship was genuinely connected) but `?expand=learning_outcomes` (the
///     spec's own snake_case wire value) silently returned nothing, found via
///     `fix-conformance-audit-findings`'s own live spot-check. All three were only ever caught by live
///     `curl` - this locks the same assertions into a `dotnet test` run instead.
/// </summary>
[Collection(SqlServerCollection.Name)]
public class ProgrammeExpandRelationshipConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Fact]
    public async Task ExpandChildrenCoordinatorsInstructors_ReturnsPopulatedObjects_WithRealOrganisationId()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid parentOrgId = Guid.CreateVersion7();
        Guid childOrgId = Guid.CreateVersion7();
        Guid coordinatorId = Guid.CreateVersion7();
        Guid instructorId = Guid.CreateVersion7();
        Guid parentProgrammeId = Guid.CreateVersion7();
        Guid childProgrammeId = Guid.CreateVersion7();
        Guid learningOutcomeId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            // Two distinct organisations - the child programme deliberately belongs to a *different*
            // one than the parent, so a wrongly-empty organisationId on the expanded child can't be
            // confused with it coincidentally matching the parent's own value.
            OrganisationEntity parentOrg = new()
            {
                Id = parentOrgId,
                OrganisationId = parentOrgId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"PROGX-ORG-P-{parentOrgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Parent Org\"}]"
            };
            OrganisationEntity childOrg = new()
            {
                Id = childOrgId,
                OrganisationId = childOrgId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"PROGX-ORG-C-{childOrgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Child Org\"}]"
            };
            dbContext.Organisations.AddRange(parentOrg, childOrg);

            PersonEntity coordinator = new()
            {
                Id = coordinatorId,
                PersonId = coordinatorId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"PROGX-COORD-{coordinatorId.ToString()[..8]}",
                GivenName = "Coordinator"
            };
            PersonEntity instructor = new()
            {
                Id = instructorId,
                PersonId = instructorId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"PROGX-INSTR-{instructorId.ToString()[..8]}",
                GivenName = "Instructor"
            };
            dbContext.Persons.AddRange(coordinator, instructor);

            ProgrammeEntity parentProgramme = new()
            {
                Id = parentProgrammeId,
                ProgrammeId = parentProgrammeId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"PROGX-PRG-{parentProgrammeId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Expand Bug Parent Programme\"}]",
                ProgrammeType = "programme",
                OrganisationEntityId = parentOrg.Id
            };
            parentProgramme.Coordinators.Add(coordinator);
            parentProgramme.Instructors.Add(instructor);
            dbContext.Programmes.Add(parentProgramme);

            LearningOutcomeEntity learningOutcome = new()
            {
                Id = learningOutcomeId,
                LearningOutcomeId = learningOutcomeId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"PROGX-LO-{learningOutcomeId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Expand Bug Learning Outcome\"}]"
            };
            dbContext.LearningOutcomes.Add(learningOutcome);
            parentProgramme.LearningOutcomes.Add(learningOutcome);

            ProgrammeEntity childProgramme = new()
            {
                Id = childProgrammeId,
                ProgrammeId = childProgrammeId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"PROGX-SUB-{childProgrammeId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Expand Bug Child Programme\"}]",
                ProgrammeType = "programme",
                OrganisationEntityId = childOrg.Id,
                ParentEntityId = parentProgramme.Id
            };
            dbContext.Programmes.Add(childProgramme);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        HttpResponseMessage response =
            await client.GetAsync(
                $"/programmes/{parentProgrammeId}?expand=children,coordinators,instructors,learning_outcomes");
        response.EnsureSuccessStatusCode();
        JsonObject programme = JsonNode.Parse(await response.Content.ReadAsStringAsync())!.AsObject();

        JsonArray children = programme["children"]!.AsArray();
        Assert.Single(children);
        JsonObject child = children[0]!.AsObject();
        Assert.Equal(childProgrammeId.ToString(), child["programmeId"]!.GetValue<string>());
        // The bug: this came back as "" instead of the real value.
        Assert.Equal(childOrgId.ToString(), child["organisationId"]!.GetValue<string>());

        JsonArray coordinators = programme["coordinators"]!.AsArray();
        Assert.Contains(coordinators, c => c!["personId"]!.GetValue<string>() == coordinatorId.ToString());

        JsonArray instructors = programme["instructors"]!.AsArray();
        Assert.Contains(instructors, i => i!["personId"]!.GetValue<string>() == instructorId.ToString());

        JsonArray learningOutcomes = programme["learningOutcomes"]!.AsArray();
        Assert.Contains(learningOutcomes,
            lo => lo!["learningOutcomeId"]!.GetValue<string>() == learningOutcomeId.ToString());
    }
}
