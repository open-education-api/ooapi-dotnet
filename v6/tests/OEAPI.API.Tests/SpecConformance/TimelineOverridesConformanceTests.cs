using System.Text.Json.Nodes;
using Microsoft.Extensions.DependencyInjection;
using OEAPI.API.Tests.Infrastructure;
using OEAPI.Infrastructure.Data.Context;
using OEAPI.Infrastructure.Data.Entities;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Confirms the spec's <c>timelineOverrides</c> mechanism (<c>GET /courses/{id}</c> and
///     <c>GET /programmes/{id}</c> with <c>?returnTimelineOverrides=true</c>) actually returns
///     historical/future alternate snapshots with the correct shape, ordering, presence semantics and
///     <c>expand=</c> composition - not just <c>200</c>.
/// </summary>
/// <remarks>
///     Course/Programme have no write endpoint in the canonical spec (GET-only resources), and there is
///     no write path for <c>timelineOverrides</c> data either, so every fixture here is seeded directly
///     through <see cref="SqlServerOEAPIDbContext" /> rather than via HTTP, mirroring
///     <c>DemoDataSeeder.cs</c>'s own entity-construction pattern (see also
///     <see cref="EmptyArrayNotNullConformanceTests" />).
/// </remarks>
[Collection(SqlServerCollection.Name)]
public class TimelineOverridesConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Fact]
    public async Task Course_ReturnTimelineOverridesAbsent_OmitsField()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid courseId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();
            dbContext.Courses.Add(new CourseEntity
            {
                Id = courseId,
                CourseId = courseId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"TLO-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Timeline Overrides Absent Course\"}]"
            });
            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        JsonObject json = await GetJsonAsync(client, $"/courses/{courseId}");

        Assert.False(json.ContainsKey("timelineOverrides"));
    }

    [Fact]
    public async Task Course_ReturnTimelineOverridesFalse_OmitsField()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid courseId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();
            dbContext.Courses.Add(new CourseEntity
            {
                Id = courseId,
                CourseId = courseId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"TLO-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Timeline Overrides False Course\"}]"
            });
            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        JsonObject json = await GetJsonAsync(client, $"/courses/{courseId}?returnTimelineOverrides=false");

        Assert.False(json.ContainsKey("timelineOverrides"));
    }

    [Fact]
    public async Task Course_ReturnTimelineOverridesTrue_NoSeededRows_OmitsField()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid courseId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();
            dbContext.Courses.Add(new CourseEntity
            {
                Id = courseId,
                CourseId = courseId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"TLO-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"No Overrides Seeded Course\"}]"
            });
            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        JsonObject json = await GetJsonAsync(client, $"/courses/{courseId}?returnTimelineOverrides=true");

        Assert.False(json.ContainsKey("timelineOverrides"));
    }

    [Fact]
    public async Task Course_ReturnTimelineOverridesTrue_ReturnsOrderedArrayWithIndependentData()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid courseId = Guid.CreateVersion7();
        Guid orgAId = Guid.CreateVersion7();
        Guid orgBId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            dbContext.Organisations.Add(new OrganisationEntity
            {
                Id = orgAId,
                OrganisationId = orgAId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"TLO-ORG-A-{orgAId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Timeline Org A\"}]"
            });
            dbContext.Organisations.Add(new OrganisationEntity
            {
                Id = orgBId,
                OrganisationId = orgBId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"TLO-ORG-B-{orgBId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Timeline Org B\"}]"
            });
            dbContext.Courses.Add(new CourseEntity
            {
                Id = courseId,
                CourseId = courseId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"TLO-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Current Course Name\"}]"
            });
            dbContext.CourseTimelineOverrides.Add(new TimelineOverrideCourseEntity
            {
                CourseEntityId = courseId,
                ValidFrom = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                ValidTo = null,
                PrimaryCodeType = "identifier",
                PrimaryCode = $"TLO-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Future Override Name\"}]",
                OrganisationEntityId = orgBId
            });
            dbContext.CourseTimelineOverrides.Add(new TimelineOverrideCourseEntity
            {
                CourseEntityId = courseId,
                ValidFrom = new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                ValidTo = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"TLO-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Past Override Name\"}]",
                OrganisationEntityId = orgAId
            });
            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        JsonObject json = await GetJsonAsync(client, $"/courses/{courseId}?returnTimelineOverrides=true");

        Assert.True(json.ContainsKey("timelineOverrides"));
        JsonArray overrides = json["timelineOverrides"]!.AsArray();
        Assert.Equal(2, overrides.Count);

        // Ordered by validFrom ascending - the past window (2018) must come first.
        string firstName = overrides[0]!["course"]!["name"]![0]!["value"]!.GetValue<string>();
        string secondName = overrides[1]!["course"]!["name"]![0]!["value"]!.GetValue<string>();
        Assert.Equal("Past Override Name", firstName);
        Assert.Equal("Future Override Name", secondName);

        // Values differ from the parent course's own current values.
        Assert.NotEqual("Current Course Name", firstName);
        Assert.NotEqual("Current Course Name", secondName);

        // Independent relationships between override entries - not shared with each other.
        string firstOrgId = overrides[0]!["course"]!["organisationId"]!.GetValue<string>();
        string secondOrgId = overrides[1]!["course"]!["organisationId"]!.GetValue<string>();
        Assert.Equal(orgAId.ToString(), firstOrgId);
        Assert.Equal(orgBId.ToString(), secondOrgId);
        Assert.NotEqual(firstOrgId, secondOrgId);
    }

    [Fact]
    public async Task Programme_ReturnTimelineOverridesTrue_ReturnsParentAndChildIds()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid programmeId = Guid.CreateVersion7();
        Guid parentId = Guid.CreateVersion7();
        Guid child1Id = Guid.CreateVersion7();
        Guid child2Id = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            ProgrammeEntity parent = new()
            {
                Id = parentId,
                ProgrammeId = parentId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"TLO-PRG-PARENT-{parentId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Override Parent Programme\"}]",
                ProgrammeType = "programme"
            };
            ProgrammeEntity child1 = new()
            {
                Id = child1Id,
                ProgrammeId = child1Id.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"TLO-PRG-CHILD1-{child1Id.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Override Child Programme 1\"}]",
                ProgrammeType = "specialisation"
            };
            ProgrammeEntity child2 = new()
            {
                Id = child2Id,
                ProgrammeId = child2Id.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"TLO-PRG-CHILD2-{child2Id.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Override Child Programme 2\"}]",
                ProgrammeType = "specialisation"
            };
            dbContext.Programmes.AddRange(parent, child1, child2);

            ProgrammeEntity programme = new()
            {
                Id = programmeId,
                ProgrammeId = programmeId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"TLO-PRG-{programmeId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Current Programme Name\"}]",
                ProgrammeType = "programme"
            };
            dbContext.Programmes.Add(programme);

            TimelineOverrideProgrammeEntity programmeOverride = new()
            {
                ProgrammeEntityId = programmeId,
                ValidFrom = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"TLO-PRG-{programmeId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Future Programme Override\"}]",
                ProgrammeType = "programme",
                ParentEntityId = parentId
            };
            programmeOverride.Children.Add(child1);
            programmeOverride.Children.Add(child2);
            dbContext.ProgrammeTimelineOverrides.Add(programmeOverride);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        JsonObject json = await GetJsonAsync(client, $"/programmes/{programmeId}?returnTimelineOverrides=true");

        JsonArray overrides = json["timelineOverrides"]!.AsArray();
        Assert.Single(overrides);
        JsonObject overrideProgramme = overrides[0]!["programme"]!.AsObject();

        Assert.Equal(parentId.ToString(), overrideProgramme["parentId"]!.GetValue<string>());
        JsonArray childIds = overrideProgramme["childIds"]!.AsArray();
        Assert.Equal(2, childIds.Count);
        Assert.Contains(childIds, c => c!.GetValue<string>() == child1Id.ToString());
        Assert.Contains(childIds, c => c!.GetValue<string>() == child2Id.ToString());
    }

    [Fact]
    public async Task Course_ReturnTimelineOverridesTrue_WithExpandOrganisation_PopulatesNestedOrganisation()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid courseId = Guid.CreateVersion7();
        Guid courseOrgId = Guid.CreateVersion7();
        Guid overrideOrgId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            dbContext.Organisations.Add(new OrganisationEntity
            {
                Id = courseOrgId,
                OrganisationId = courseOrgId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"TLO-ORG-CUR-{courseOrgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Current Course Organisation\"}]"
            });
            dbContext.Organisations.Add(new OrganisationEntity
            {
                Id = overrideOrgId,
                OrganisationId = overrideOrgId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"TLO-ORG-OVR-{overrideOrgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Override Course Organisation\"}]"
            });
            dbContext.Courses.Add(new CourseEntity
            {
                Id = courseId,
                CourseId = courseId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"TLO-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Expand Composition Course\"}]",
                OrganisationEntityId = courseOrgId
            });
            dbContext.CourseTimelineOverrides.Add(new TimelineOverrideCourseEntity
            {
                CourseEntityId = courseId,
                ValidFrom = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"TLO-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Expand Composition Override\"}]",
                OrganisationEntityId = overrideOrgId
            });
            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        JsonObject json = await GetJsonAsync(
            client, $"/courses/{courseId}?returnTimelineOverrides=true&expand=organisation");

        // The outer expand=organisation still applies to the course itself.
        Assert.NotNull(json["organisation"]);
        Assert.Equal(courseOrgId.ToString(), json["organisation"]!["organisationId"]!.GetValue<string>());

        // The same expand=organisation composes into the nested override entry.
        JsonObject overrideCourse = json["timelineOverrides"]!.AsArray()[0]!["course"]!.AsObject();
        Assert.NotNull(overrideCourse["organisation"]);
        Assert.Equal(overrideOrgId.ToString(), overrideCourse["organisation"]!["organisationId"]!.GetValue<string>());
    }

    [Fact]
    public async Task Course_ReturnTimelineOverridesTrue_WithoutExpand_NestedOrganisationIdPresentButOrganisationNull()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid courseId = Guid.CreateVersion7();
        Guid overrideOrgId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            dbContext.Organisations.Add(new OrganisationEntity
            {
                Id = overrideOrgId,
                OrganisationId = overrideOrgId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"TLO-ORG-NOEXP-{overrideOrgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"No Expand Override Organisation\"}]"
            });
            dbContext.Courses.Add(new CourseEntity
            {
                Id = courseId,
                CourseId = courseId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"TLO-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"No Expand Course\"}]"
            });
            dbContext.CourseTimelineOverrides.Add(new TimelineOverrideCourseEntity
            {
                CourseEntityId = courseId,
                ValidFrom = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"TLO-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"No Expand Override\"}]",
                OrganisationEntityId = overrideOrgId
            });
            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        JsonObject json = await GetJsonAsync(client, $"/courses/{courseId}?returnTimelineOverrides=true");

        JsonObject overrideCourse = json["timelineOverrides"]!.AsArray()[0]!["course"]!.AsObject();
        Assert.Equal(overrideOrgId.ToString(), overrideCourse["organisationId"]!.GetValue<string>());
        Assert.True(!overrideCourse.ContainsKey("organisation") || overrideCourse["organisation"] == null);
    }

    [Fact]
    public async Task Room_ReturnTimelineOverridesTrue_SilentlyIgnored()
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
                PrimaryCode = $"TLO-ROOM-{roomId.ToString()[..8]}",
                RoomType = "classroom",
                NameJson = "[{\"language\":\"en\",\"value\":\"Timeline Overrides Ignored Room\"}]"
            });
            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        HttpResponseMessage response =
            await client.GetAsync($"/rooms/{roomId}?returnTimelineOverrides=true");

        response.EnsureSuccessStatusCode();
        JsonObject json = JsonNode.Parse(await response.Content.ReadAsStringAsync())!.AsObject();
        Assert.False(json.ContainsKey("timelineOverrides"));
    }

    private static async Task<JsonObject> GetJsonAsync(HttpClient client, string requestUri)
    {
        HttpResponseMessage response = await client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        return JsonNode.Parse(await response.Content.ReadAsStringAsync())!.AsObject();
    }
}
