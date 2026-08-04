using System.Text.Json.Nodes;
using Microsoft.Extensions.DependencyInjection;
using OEAPI.API.Tests.Infrastructure;
using OEAPI.Infrastructure.Data.Context;
using OEAPI.Infrastructure.Data.Entities;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Guards `rename-entity-fields-to-match-api-contract`: several entities' CLR property names used
///     to differ from their own API contract field name (e.g. `OrganisationEntity.Abbreviation` for
///     the API's `shortName`), which broke `filter_query` entirely for those fields -
///     `FilterQueryTranslator` resolves purely by reflection against entity property names (no
///     per-entity catalogue), so a name mismatch meant the whole clause silently dropped and every row
///     came back unfiltered. Fixed by renaming the entity properties (and their DB columns) to match,
///     rather than adding an exception table to the translator. `FilterQueryTranslatorTests.cs`
///     (`OEAPI.Infrastructure.Tests`) can't catch this class of bug - it exercises the translator
///     against its own synthetic `TestEntity`, whose properties are already name-aligned by
///     construction - so this needs a real, seeded-entity, real-HTTP-request test instead.
///
///     Covers one representative field per distinct rename shape found (not all 18 individually):
///     `Organisation`'s 3-field group, the `*Date`→`*DateTime` group shared identically by
///     `Course`/`Programme`/`LearningComponent`/`TestComponent` (via `Course`, plus `Programme`'s own
///     extra `FirstStartDate`→`FirstStartDateTime`), and `Address`'s 2-field group (via `Building`,
///     since `Address` has no endpoint of its own).
/// </summary>
[Collection(SqlServerCollection.Name)]
public class EntityFieldFilterQueryConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Fact]
    public async Task FilterQuery_OrganisationShortName_ActuallyFilters()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid matchId = Guid.CreateVersion7();
        Guid otherId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();
            dbContext.Organisations.AddRange(
                new OrganisationEntity
                {
                    Id = matchId,
                    OrganisationId = matchId.ToString(),
                    PrimaryCodeType = "identifier",
                    PrimaryCode = $"EFFQ-ORG-{matchId.ToString()[..8]}",
                    NameJson = "[{\"language\":\"en\",\"value\":\"Filter Regression Org Match\"}]",
                    ShortName = "EFFQ-MATCH"
                },
                new OrganisationEntity
                {
                    Id = otherId,
                    OrganisationId = otherId.ToString(),
                    PrimaryCodeType = "identifier",
                    PrimaryCode = $"EFFQ-ORG-{otherId.ToString()[..8]}",
                    NameJson = "[{\"language\":\"en\",\"value\":\"Filter Regression Org Other\"}]",
                    ShortName = "EFFQ-OTHER"
                });
            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        JsonObject json = await GetJsonAsync(client, "/organisations?filter_query[shortName][in]=EFFQ-MATCH");
        JsonArray items = json["items"]!.AsArray();

        Assert.Contains(items, i => i!["organisationId"]!.GetValue<string>() == matchId.ToString());
        Assert.DoesNotContain(items, i => i!["organisationId"]!.GetValue<string>() == otherId.ToString());
    }

    [Fact]
    public async Task FilterQuery_CourseFirstPossibleOfferingStartDateTime_ActuallyFilters()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid matchId = Guid.CreateVersion7();
        Guid otherId = Guid.CreateVersion7();
        DateTime matchDate = new(2022, 3, 1, 0, 0, 0, DateTimeKind.Utc);

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();
            dbContext.Courses.AddRange(
                new CourseEntity
                {
                    Id = matchId,
                    CourseId = matchId.ToString(),
                    PrimaryCodeType = "identifier",
                    PrimaryCode = $"EFFQ-CRS-{matchId.ToString()[..8]}",
                    NameJson = "[{\"language\":\"en\",\"value\":\"Filter Regression Course Match\"}]",
                    FirstPossibleOfferingStartDateTime = matchDate
                },
                new CourseEntity
                {
                    Id = otherId,
                    CourseId = otherId.ToString(),
                    PrimaryCodeType = "identifier",
                    PrimaryCode = $"EFFQ-CRS-{otherId.ToString()[..8]}",
                    NameJson = "[{\"language\":\"en\",\"value\":\"Filter Regression Course Other\"}]"
                });
            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        JsonObject json = await GetJsonAsync(client,
            $"/courses?filter_query[firstPossibleOfferingStartDateTime][in]={Uri.EscapeDataString(matchDate.ToString("O"))}");
        JsonArray items = json["items"]!.AsArray();

        Assert.Contains(items, i => i!["courseId"]!.GetValue<string>() == matchId.ToString());
        Assert.DoesNotContain(items, i => i!["courseId"]!.GetValue<string>() == otherId.ToString());
    }

    [Fact]
    public async Task FilterQuery_ProgrammeFirstStartDateTime_ActuallyFilters()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid matchId = Guid.CreateVersion7();
        Guid otherId = Guid.CreateVersion7();
        DateTime matchDate = new(2021, 9, 1, 0, 0, 0, DateTimeKind.Utc);

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();
            dbContext.Programmes.AddRange(
                new ProgrammeEntity
                {
                    Id = matchId,
                    ProgrammeId = matchId.ToString(),
                    PrimaryCodeType = "identifier",
                    PrimaryCode = $"EFFQ-PRG-{matchId.ToString()[..8]}",
                    NameJson = "[{\"language\":\"en\",\"value\":\"Filter Regression Programme Match\"}]",
                    ProgrammeType = "programme",
                    FirstStartDateTime = matchDate
                },
                new ProgrammeEntity
                {
                    Id = otherId,
                    ProgrammeId = otherId.ToString(),
                    PrimaryCodeType = "identifier",
                    PrimaryCode = $"EFFQ-PRG-{otherId.ToString()[..8]}",
                    NameJson = "[{\"language\":\"en\",\"value\":\"Filter Regression Programme Other\"}]",
                    ProgrammeType = "programme"
                });
            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        JsonObject json = await GetJsonAsync(client,
            $"/programmes?filter_query[firstStartDateTime][in]={Uri.EscapeDataString(matchDate.ToString("O"))}");
        JsonArray items = json["items"]!.AsArray();

        Assert.Contains(items, i => i!["programmeId"]!.GetValue<string>() == matchId.ToString());
        Assert.DoesNotContain(items, i => i!["programmeId"]!.GetValue<string>() == otherId.ToString());
    }

    [Fact]
    public async Task FilterQuery_AddressStreetNumberAndPostCode_ActuallyFilter()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid matchAddressId = Guid.CreateVersion7();
        Guid otherAddressId = Guid.CreateVersion7();
        Guid matchBuildingId = Guid.CreateVersion7();
        Guid otherBuildingId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();
            AddressEntity matchAddress = new()
            {
                Id = matchAddressId,
                AddressId = matchAddressId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"EFFQ-ADDR-{matchAddressId.ToString()[..8]}",
                StreetNumber = "48",
                PostCode = "3511 EP"
            };
            AddressEntity otherAddress = new()
            {
                Id = otherAddressId,
                AddressId = otherAddressId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"EFFQ-ADDR-{otherAddressId.ToString()[..8]}",
                StreetNumber = "8",
                PostCode = "3584 CH"
            };
            dbContext.Addresses.AddRange(matchAddress, otherAddress);
            dbContext.Buildings.AddRange(
                new BuildingEntity
                {
                    Id = matchBuildingId,
                    BuildingId = matchBuildingId.ToString(),
                    PrimaryCodeType = "identifier",
                    PrimaryCode = $"EFFQ-BLD-{matchBuildingId.ToString()[..8]}",
                    NameJson = "[{\"language\":\"en\",\"value\":\"Filter Regression Building Match\"}]",
                    AddressEntityId = matchAddress.Id
                },
                new BuildingEntity
                {
                    Id = otherBuildingId,
                    BuildingId = otherBuildingId.ToString(),
                    PrimaryCodeType = "identifier",
                    PrimaryCode = $"EFFQ-BLD-{otherBuildingId.ToString()[..8]}",
                    NameJson = "[{\"language\":\"en\",\"value\":\"Filter Regression Building Other\"}]",
                    AddressEntityId = otherAddress.Id
                });
            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        JsonObject byStreetNumber = await GetJsonAsync(client, "/buildings?filter_query[address.streetNumber][in]=48");
        JsonArray streetNumberItems = byStreetNumber["items"]!.AsArray();
        Assert.Contains(streetNumberItems, i => i!["buildingId"]!.GetValue<string>() == matchBuildingId.ToString());
        Assert.DoesNotContain(streetNumberItems, i => i!["buildingId"]!.GetValue<string>() == otherBuildingId.ToString());

        JsonObject byPostCode = await GetJsonAsync(client,
            $"/buildings?filter_query[address.postCode][in]={Uri.EscapeDataString("3511 EP")}");
        JsonArray postCodeItems = byPostCode["items"]!.AsArray();
        Assert.Contains(postCodeItems, i => i!["buildingId"]!.GetValue<string>() == matchBuildingId.ToString());
        Assert.DoesNotContain(postCodeItems, i => i!["buildingId"]!.GetValue<string>() == otherBuildingId.ToString());
    }

    private static async Task<JsonObject> GetJsonAsync(HttpClient client, string requestUri)
    {
        HttpResponseMessage response = await client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        return JsonNode.Parse(await response.Content.ReadAsStringAsync())!.AsObject();
    }
}
