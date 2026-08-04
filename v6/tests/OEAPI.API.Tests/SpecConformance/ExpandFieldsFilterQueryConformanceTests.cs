using System.Net.Http.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using OEAPI.API.Controllers;
using OEAPI.API.Tests.Infrastructure;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Infrastructure.Data.Context;
using OEAPI.Infrastructure.Data.Entities;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Confirms <c>expand</c>/<c>fields</c>/<c>filter_query</c> actually do what they claim against real
///     seeded data - not just returning <c>200</c>. Uses <see cref="OrganisationsController" /> as the
///     vehicle since it has both a real object relationship (<c>parent</c>/<c>children</c>) and a plain filterable field
///     (<c>primaryCode</c>); the underlying <c>fields</c>/<c>filter_query</c> engines are shared by
///     every controller (see <see cref="BaseApiController" />), so this is representative, not
///     organisation-specific behaviour.
/// </summary>
[Collection(SqlServerCollection.Name)]
public class ExpandFieldsFilterQueryConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Fact]
    public async Task Expand_ParentAndChildren_ReturnsFullNestedObjects()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string parentId = Guid.NewGuid().ToString();
        string childId = Guid.NewGuid().ToString();

        await client.PutAsJsonAsync($"/organisations/{parentId}", new Organisation
        {
            OrganisationId = parentId,
            PrimaryCode = new IdentifierEntry { CodeType = "organisation_id", Code = $"EXP-P-{parentId[..8]}" },
            OrganisationType = "root",
            Name = [new LanguageTypedString { Language = "en", Value = "Phase E Parent Org" }]
        });
        await client.PutAsJsonAsync($"/organisations/{childId}", new Organisation
        {
            OrganisationId = childId,
            PrimaryCode = new IdentifierEntry { CodeType = "organisation_id", Code = $"EXP-C-{childId[..8]}" },
            OrganisationType = "institute",
            Name = [new LanguageTypedString { Language = "en", Value = "Phase E Child Org" }],
            ParentId = new Identifier { Value = parentId }
        });

        Organisation? child = await client.GetFromJsonAsync<Organisation>($"/organisations/{childId}?expand=parent");
        Assert.NotNull(child!.Parent);
        Assert.Equal(parentId, child.Parent!.OrganisationId);
        Assert.Equal("Phase E Parent Org", child.Parent.Name.Single().Value);

        Organisation? parent =
            await client.GetFromJsonAsync<Organisation>($"/organisations/{parentId}?expand=children");
        Assert.NotNull(parent!.Children);
        Assert.Contains(parent.Children!, c => c.OrganisationId == childId);
    }

    [Fact]
    public async Task Expand_RoomBuilding_ReturnsFullNestedBuildingObject()
    {
        // Rooms/Buildings have no write endpoint in the canonical spec, so fixtures are seeded
        // directly through SqlServerOEAPIDbContext, mirroring DemoDataSeeder.cs's own pattern (see
        // also EmptyArrayNotNullConformanceTests.cs).
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid buildingId = Guid.CreateVersion7();
        Guid roomId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();
            dbContext.Buildings.Add(new BuildingEntity
            {
                Id = buildingId,
                BuildingId = buildingId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"EXP-BLD-{buildingId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Expand Regression Building\"}]"
            });
            dbContext.Rooms.Add(new RoomEntity
            {
                Id = roomId,
                RoomId = roomId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"EXP-ROOM-{roomId.ToString()[..8]}",
                RoomType = "classroom",
                NameJson = "[{\"language\":\"en\",\"value\":\"Expand Regression Room\"}]",
                BuildingEntityId = buildingId
            });
            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        Room? room = await client.GetFromJsonAsync<Room>($"/rooms/{roomId}?expand=building");

        Assert.NotNull(room!.Building);
        Assert.Equal(buildingId.ToString(), room.Building!.BuildingId);
        Assert.Equal("Expand Regression Building", room.Building.Name.Single().Value);
    }

    /// <summary>
    ///     Regression test: unlike a real relationship (e.g. <c>Room.building</c> above, which has a
    ///     <c>buildingId</c> sibling and is only populated on request), the spec declares
    ///     <c>Building.address</c> as a plain <c>oneOf[Address, null]</c> field with no id-based
    ///     indirection and no <c>expand</c> parameter at all on either <c>GET /buildings</c> or
    ///     <c>GET /buildings/{buildingId}</c> - so it must always be populated directly.
    ///     <c>BuildingMappingExtensions.ToApiModel</c> used to hardcode it to <see langword="null" />,
    ///     requiring a non-spec-declared <c>expand=address</c> that no compliant client would ever send.
    /// </summary>
    [Fact]
    public async Task Building_WithAddress_AlwaysPopulatesAddressWithoutExpand()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid addressId = Guid.CreateVersion7();
        Guid buildingId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();
            dbContext.Addresses.Add(new AddressEntity
            {
                Id = addressId,
                AddressId = addressId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"EXP-ADDR-{addressId.ToString()[..8]}",
                AddressType = "visit",
                Street = "Regression Street",
                StreetNumber = "1"
            });
            dbContext.Buildings.Add(new BuildingEntity
            {
                Id = buildingId,
                BuildingId = buildingId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"EXP-BLD2-{buildingId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Address Regression Building\"}]",
                AddressEntityId = addressId
            });
            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        Building? building = await client.GetFromJsonAsync<Building>($"/buildings/{buildingId}");

        Assert.NotNull(building!.Address);
        Assert.Equal("Regression Street", building.Address!.Street);
    }

    /// <summary>
    ///     Regression test: <c>ProgrammesController</c>/<c>LearningComponentsController</c>/
    ///     <c>TestComponentsController</c>'s <c>ApplyExpandAsync</c> switches normalized
    ///     <c>expandOption.ToLower()</c> only, never stripping underscores - so the spec's own
    ///     snake_case wire value (<c>learning_outcomes</c>, per <c>source/enumerations/
    ///     expandableObjects.yaml</c>) never matched the <c>"learningoutcomes"</c> case label and
    ///     silently no-op'd. <c>CoursesController</c> had the same bug in a second, separate switch (its
    ///     main <c>GetById</c> expand handler used a bare <c>switch (option)</c> with no case-folding at
    ///     all). Codebase-wide, every <c>expand</c> switch has since been normalized to match the spec's
    ///     literal snake_case enum values directly (case labels like <c>"learning_outcomes"</c>,
    ///     <c>"academic_session"</c>) rather than concatenated-lowercase labels plus an underscore-
    ///     stripping <c>Replace("_", "")</c> on the input - the latter was only ever a minimal-diff
    ///     historical shortcut (see <c>fix-spec-conformance-tier2-binding-bugs</c>'s own design.md) that
    ///     had the undocumented side effect of also accepting camelCase (<c>learningOutcomes</c>), never
    ///     a spec requirement or a verified real-client need.
    /// </summary>
    [Fact]
    public async Task Expand_LearningOutcomesSnakeCase_ResolvesOnCourseProgrammeLearningComponentTestComponent()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid outcomeId = Guid.CreateVersion7();
        Guid courseId = Guid.CreateVersion7();
        Guid programmeId = Guid.CreateVersion7();
        Guid learningComponentId = Guid.CreateVersion7();
        Guid testComponentId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();
            LearningOutcomeEntity outcome = new()
            {
                Id = outcomeId,
                LearningOutcomeId = outcomeId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"EXP-LO-{outcomeId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Snake-Case Expand Regression Outcome\"}]"
            };
            dbContext.LearningOutcomes.Add(outcome);

            CourseEntity course = new()
            {
                Id = courseId,
                CourseId = courseId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"EXP-CRS-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Snake-Case Expand Regression Course\"}]"
            };
            course.LearningOutcomes.Add(outcome);
            dbContext.Courses.Add(course);

            ProgrammeEntity programme = new()
            {
                Id = programmeId,
                ProgrammeId = programmeId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"EXP-PRG-{programmeId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Snake-Case Expand Regression Programme\"}]",
                ProgrammeType = "programme"
            };
            programme.LearningOutcomes.Add(outcome);
            dbContext.Programmes.Add(programme);

            LearningComponentEntity learningComponent = new()
            {
                Id = learningComponentId,
                ComponentId = learningComponentId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"EXP-LC-{learningComponentId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Snake-Case Expand Regression Learning Component\"}]"
            };
            learningComponent.LearningOutcomes.Add(outcome);
            dbContext.LearningComponents.Add(learningComponent);

            TestComponentEntity testComponent = new()
            {
                Id = testComponentId,
                ComponentId = testComponentId.ToString(),
                ComponentType = "written_exam",
                PrimaryCodeType = "identifier",
                PrimaryCode = $"EXP-TC-{testComponentId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Snake-Case Expand Regression Test Component\"}]"
            };
            testComponent.LearningOutcomes.Add(outcome);
            dbContext.TestComponents.Add(testComponent);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        Course? course2 = await client.GetFromJsonAsync<Course>($"/courses/{courseId}?expand=learning_outcomes");
        Assert.NotNull(course2!.LearningOutcomes);
        Assert.Single(course2.LearningOutcomes!);

        Programme? programme2 =
            await client.GetFromJsonAsync<Programme>($"/programmes/{programmeId}?expand=learning_outcomes");
        Assert.NotNull(programme2!.LearningOutcomes);
        Assert.Single(programme2.LearningOutcomes!);

        LearningComponent? learningComponent2 = await client.GetFromJsonAsync<LearningComponent>(
            $"/learning-components/{learningComponentId}?expand=learning_outcomes");
        Assert.NotNull(learningComponent2!.LearningOutcomes);
        Assert.Single(learningComponent2.LearningOutcomes!);

        TestComponent? testComponent2 = await client.GetFromJsonAsync<TestComponent>(
            $"/test-components/{testComponentId}?expand=learning_outcomes");
        Assert.NotNull(testComponent2!.LearningOutcomes);
        Assert.Single(testComponent2.LearningOutcomes!);
    }

    /// <summary>
    ///     Regression test found while removing the codebase-wide <c>Replace("_", "")</c> expand
    ///     normalization: <c>TestComponentOfferingsController.ApplyExpandAsync</c>'s case label for the
    ///     spec's <c>test_component</c> expand value was literally <c>"component"</c>, not
    ///     <c>"testcomponent"</c> - so even with the old normalization in place (which produces
    ///     <c>"testcomponent"</c> from <c>test_component</c>), the case could never match anything a
    ///     real client could send. <c>TestComponentOffering.testComponent</c> could never be populated
    ///     via <c>expand=</c> by any caller, ever.
    /// </summary>
    [Fact]
    public async Task Expand_TestComponentOnTestComponentOffering_Resolves()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid orgId = Guid.CreateVersion7();
        Guid testComponentId = Guid.CreateVersion7();
        Guid testComponentOfferingId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();
            OrganisationEntity org = new()
            {
                Id = orgId,
                OrganisationId = orgId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"EXP-TCO-ORG-{orgId.ToString()[..8]}",
                OrganisationType = "root",
                NameJson = "[{\"language\":\"en\",\"value\":\"Expand Regression Org\"}]"
            };
            dbContext.Organisations.Add(org);

            TestComponentEntity testComponent = new()
            {
                Id = testComponentId,
                ComponentId = testComponentId.ToString(),
                ComponentType = "written_exam",
                PrimaryCodeType = "identifier",
                PrimaryCode = $"EXP-TC-{testComponentId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Expand Regression Test Component\"}]",
                OrganisationEntityId = orgId
            };
            dbContext.TestComponents.Add(testComponent);

            TestComponentOfferingEntity testComponentOffering = new()
            {
                Id = testComponentOfferingId,
                TestComponentOfferingIdValue = testComponentOfferingId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"EXP-TCO-{testComponentOfferingId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Expand Regression TC Offering\"}]",
                TestComponentEntityId = testComponentId,
                OrganisationEntityId = orgId
            };
            dbContext.TestComponentOfferings.Add(testComponentOffering);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        TestComponentOffering? offering = await client.GetFromJsonAsync<TestComponentOffering>(
            $"/test-component-offerings/{testComponentOfferingId}?expand=test_component");

        Assert.NotNull(offering!.TestComponent);
        Assert.Equal(testComponentId.ToString(), offering.TestComponent!.ComponentId);
    }

    [Fact]
    public async Task Fields_NarrowSelection_PrunesUnrequestedFields()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        await client.PutAsJsonAsync($"/organisations/{id}", new Organisation
        {
            OrganisationId = id,
            PrimaryCode = new IdentifierEntry { CodeType = "organisation_id", Code = $"FLD-{id[..8]}" },
            OrganisationType = "institute",
            Name = [new LanguageTypedString { Language = "en", Value = "Phase E Fields Org" }],
            ShortName = "Phase E Org"
        });

        // Only "primaryCode" is explicitly requested - "shortName" is optional (not in the spec's
        // Organisation.required) so it must be pruned, but "name"/"organisationType"/"organisationId"
        // are all spec-required and must survive regardless of what's requested (see
        // FieldPruner/SpecFieldRequirements - a fields= selection can only narrow optional fields).
        HttpResponseMessage response = await client.GetAsync($"/organisations/{id}?fields=(primaryCode)");
        JsonObject json = JsonNode.Parse(await response.Content.ReadAsStringAsync())!.AsObject();

        Assert.True(json.ContainsKey("primaryCode"), "explicitly requested field must survive");
        Assert.True(json.ContainsKey("organisationId"), "spec-required field must always survive");
        Assert.True(json.ContainsKey("name"), "spec-required field must always survive");
        Assert.True(json.ContainsKey("organisationType"), "spec-required field must always survive");
        Assert.False(json.ContainsKey("shortName"), "unrequested optional field must be pruned");
    }

    [Fact]
    public async Task FilterQuery_InOperator_ReturnsOnlyMatchingRows()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string matchId = Guid.NewGuid().ToString();
        string otherId = Guid.NewGuid().ToString();
        string matchCode = $"FQ-MATCH-{matchId[..8]}";
        string otherCode = $"FQ-OTHER-{otherId[..8]}";

        await client.PutAsJsonAsync($"/organisations/{matchId}", new Organisation
        {
            OrganisationId = matchId,
            PrimaryCode = new IdentifierEntry { CodeType = "organisation_id", Code = matchCode },
            OrganisationType = "institute",
            Name = [new LanguageTypedString { Language = "en", Value = "Phase E Filter Match" }]
        });
        await client.PutAsJsonAsync($"/organisations/{otherId}", new Organisation
        {
            OrganisationId = otherId,
            PrimaryCode = new IdentifierEntry { CodeType = "organisation_id", Code = otherCode },
            OrganisationType = "institute",
            Name = [new LanguageTypedString { Language = "en", Value = "Phase E Filter Other" }]
        });

        PagedResultDto<Organisation>? result = await client.GetFromJsonAsync<PagedResultDto<Organisation>>(
            $"/organisations?filter_query[primaryCode][in]={Uri.EscapeDataString(matchCode)}");

        Assert.NotEmpty(result!.Items);
        Assert.All(result.Items, org => Assert.Equal(matchCode, org.PrimaryCode.Code));
        Assert.DoesNotContain(result.Items, org => org.OrganisationId == otherId);
    }

    [Fact]
    public async Task Expand_ProgrammeOfferingRealAndSpuriousTargetsTogether_RealTargetResolvesRestIgnored()
    {
        // ProgrammeOfferingInstance.yaml's expand enum declares 8 targets, but ProgrammeOffering only
        // has 3 real relationships (programme/organisation/academic_session) - the other 5 (course,
        // course_offering, learning_component, programme_offering, test_component) don't correspond to
        // any property on this resource at all, unlike its 3 sibling -offerings resources, which each
        // have a clean, minimal expand enum matching their actual references (see docs/TODO-LIST.md
        // for the full comparison - likely a spec copy-paste defect, not an implementation gap).
        // ProgrammeOfferingsController.ApplyExpandAsync has no case for any of the 5 spurious values,
        // so they're silently no-op'd rather than crashing - confirmed directly here, together with two
        // real targets (organisation, academic_session) actually resolving in the same request.
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string offeringId = Guid.NewGuid().ToString();
        string orgId = Guid.NewGuid().ToString();
        Guid academicSessionGuid = Guid.CreateVersion7();
        string academicSessionId = academicSessionGuid.ToString();

        await client.PutAsJsonAsync($"/organisations/{orgId}", new Organisation
        {
            OrganisationId = orgId,
            PrimaryCode = new IdentifierEntry { CodeType = "organisation_id", Code = $"EXP-ORG-{orgId[..8]}" },
            OrganisationType = "root",
            Name = [new LanguageTypedString { Language = "en", Value = "Expand Test Org" }]
        });

        // AcademicSession has no write endpoint in the canonical spec, so it's seeded directly
        // through SqlServerOEAPIDbContext, mirroring Expand_RoomBuilding_...'s own pattern above.
        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();
            dbContext.AcademicSessions.Add(new AcademicSessionEntity
            {
                Id = academicSessionGuid,
                AcademicSessionId = academicSessionId,
                PrimaryCodeType = "identifier",
                PrimaryCode = $"EXP-AS-{academicSessionId[..8]}",
                AcademicSessionType = "semester",
                NameJson = "[{\"language\":\"en\",\"value\":\"Expand Test Academic Session\"}]"
            });
            await dbContext.SaveChangesAsync();
        }

        await client.PutAsJsonAsync($"/programme-offerings/{offeringId}", new ProgrammeOffering
        {
            ProgrammeOfferingIdValue = offeringId,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"EXP-PO-{offeringId[..8]}" },
            Name = [new LanguageTypedString { Language = "en", Value = "Expand Test Offering" }],
            OrganisationId = new Identifier { Value = orgId },
            AcademicSessionId = new Identifier { Value = academicSessionId }
        });

        ProgrammeOffering? result = await client.GetFromJsonAsync<ProgrammeOffering>(
            $"/programme-offerings/{offeringId}?expand=organisation,academic_session,course,course_offering," +
            "learning_component,programme_offering,test_component");

        Assert.NotNull(result!.Organisation);
        Assert.Equal(orgId, result.Organisation!.OrganisationId);
        Assert.NotNull(result.AcademicSession);
        Assert.Equal(academicSessionId, result.AcademicSession!.AcademicSessionId);
    }

    private sealed class PagedResultDto<T>
    {
        [JsonPropertyName("items")] public T[] Items { get; set; } = [];
    }
}
