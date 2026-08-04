using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.DependencyInjection;
using OEAPI.API.Tests.Infrastructure;
using OEAPI.Infrastructure.Data.Context;
using OEAPI.Infrastructure.Data.Entities;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Confirms optional fields whose spec schema type is array-only (no <c>null</c> alternative -
///     e.g. <c>Course.supplementaryInformation</c>, <c>Programme.qualificationDesignations</c>,
///     <c>TestComponent.childIds</c>) come back as an empty array <c>[]</c> when there's no data,
///     never <c>null</c>. Fields whose spec type explicitly allows <c>null</c> (e.g.
///     <c>Organisation.childIds</c>, <c>Programme.childIds</c> - the expand-target-identifiers
///     convention) are correctly left returning <c>null</c> and aren't covered here.
/// </summary>
/// <remarks>
///     Course/Programme/TestComponent have no write endpoint in the canonical spec (GET-only
///     resources), so fixtures are seeded directly through <see cref="SqlServerOEAPIDbContext" />
///     rather than via HTTP, mirroring <c>DemoDataSeeder.cs</c>'s own entity-construction pattern.
/// </remarks>
[Collection(SqlServerCollection.Name)]
public class EmptyArrayNotNullConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Fact]
    public async Task Course_WithNoSupplementaryInformation_ReturnsEmptyArrayNotNull()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid id = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();
            dbContext.Courses.Add(new CourseEntity
            {
                Id = id,
                CourseId = id.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"EAR-{id.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Empty Array Regression Course\"}]"
            });
            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        HttpResponseMessage response = await client.GetAsync($"/courses/{id}");
        JsonObject json = JsonNode.Parse(await response.Content.ReadAsStringAsync())!.AsObject();

        Assert.True(json.ContainsKey("supplementaryInformation"));
        Assert.Equal(JsonValueKind.Array, json["supplementaryInformation"]!.GetValueKind());
        Assert.Empty(json["supplementaryInformation"]!.AsArray());
    }

    [Fact]
    public async Task Programme_WithNoQualificationDesignations_ReturnsEmptyArrayNotNull()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid id = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();
            dbContext.Programmes.Add(new ProgrammeEntity
            {
                Id = id,
                ProgrammeId = id.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"EAR-{id.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Empty Array Regression Programme\"}]",
                ProgrammeType = "programme"
            });
            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        HttpResponseMessage response = await client.GetAsync($"/programmes/{id}");
        JsonObject json = JsonNode.Parse(await response.Content.ReadAsStringAsync())!.AsObject();

        Assert.True(json.ContainsKey("qualificationDesignations"));
        Assert.Equal(JsonValueKind.Array, json["qualificationDesignations"]!.GetValueKind());
        Assert.Empty(json["qualificationDesignations"]!.AsArray());
    }

    /// <summary>
    ///     Regression test for `fix-programme-qualification-designations-array`:
    ///     `ProgrammeEntity.QualificationLevels` was a scalar `string?`, so only one designation could
    ///     ever be stored, even though the spec explicitly documents multiple designations applying to
    ///     interdisciplinary programmes. Fixed by storing a real JSON-serialized array
    ///     (`QualificationDesignationsJson`), matching `TeachingLanguagesJson`'s own established
    ///     pattern on the same entity.
    /// </summary>
    [Fact]
    public async Task Programme_WithMultipleQualificationDesignations_ReturnsAllOfThemInOrder()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid id = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();
            dbContext.Programmes.Add(new ProgrammeEntity
            {
                Id = id,
                ProgrammeId = id.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"QD-{id.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Qualification Designations Regression Programme\"}]",
                ProgrammeType = "programme",
                QualificationDesignationsJson = "[\"of Arts\",\"of Sciences\"]"
            });
            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        HttpResponseMessage response = await client.GetAsync($"/programmes/{id}");
        JsonObject json = JsonNode.Parse(await response.Content.ReadAsStringAsync())!.AsObject();

        JsonArray designations = json["qualificationDesignations"]!.AsArray();
        Assert.Equal(2, designations.Count);
        Assert.Equal("of Arts", designations[0]!.GetValue<string>());
        Assert.Equal("of Sciences", designations[1]!.GetValue<string>());
    }

    [Fact]
    public async Task TestComponent_WithNoChildren_ReturnsEmptyChildIdsArrayNotNull()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid id = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();
            dbContext.TestComponents.Add(new TestComponentEntity
            {
                Id = id,
                ComponentId = id.ToString(),
                ComponentType = "written_exam",
                PrimaryCodeType = "identifier",
                PrimaryCode = $"EAR-{id.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Empty Array Regression Test Component\"}]"
            });
            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        HttpResponseMessage response = await client.GetAsync($"/test-components/{id}");
        JsonObject json = JsonNode.Parse(await response.Content.ReadAsStringAsync())!.AsObject();

        Assert.True(json.ContainsKey("childIds"));
        Assert.Equal(JsonValueKind.Array, json["childIds"]!.GetValueKind());
        Assert.Empty(json["childIds"]!.AsArray());
    }
}
