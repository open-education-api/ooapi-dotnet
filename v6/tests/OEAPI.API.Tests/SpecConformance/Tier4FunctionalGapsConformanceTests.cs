using System.Net.Http.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.DependencyInjection;
using OEAPI.API.Tests.Infrastructure;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Infrastructure.Data.Context;
using OEAPI.Infrastructure.Data.Entities;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Regression coverage for the 3 functional gaps found by <c>extend-demo-data-seeder-coverage</c>'s
///     field-by-field audit and fixed by this change: <c>Organisation.addresses</c> and
///     <c>Room.geolocation</c> had no backing storage at all, and the nested
///     <c>test-component-offerings/{id}/test-component-offering-associations</c> endpoint never
///     returned <c>attemptIds</c> due to a missing <c>Include</c>.
/// </summary>
[Collection(SqlServerCollection.Name)]
public class Tier4FunctionalGapsConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Fact]
    public async Task Organisation_WithAddresses_ReturnsThemOnGet()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        await client.PutAsJsonAsync($"/organisations/{id}", new Organisation
        {
            OrganisationId = id,
            PrimaryCode = new IdentifierEntry { CodeType = "organisation_id", Code = $"TIER4-ORG-{id[..8]}" },
            OrganisationType = "institute",
            Name = [new LanguageTypedString { Language = "en", Value = "Tier 4 Regression Org" }],
            Addresses =
            [
                new Address
                {
                    AddressType = "visit",
                    Street = "Moreelsepark",
                    StreetNumber = "48",
                    City = "Utrecht"
                }
            ]
        });

        Organisation? organisation = await client.GetFromJsonAsync<Organisation>($"/organisations/{id}");

        Assert.NotNull(organisation!.Addresses);
        Address address = Assert.Single(organisation.Addresses!);
        Assert.Equal("visit", address.AddressType);
        Assert.Equal("Moreelsepark", address.Street);
        Assert.Equal("48", address.StreetNumber);
        Assert.Equal("Utrecht", address.City);
    }

    [Fact]
    public async Task Room_WithGeolocation_ReturnsItOnGet()
    {
        // Rooms have no write endpoint in the canonical spec, so the fixture is seeded directly
        // through SqlServerOEAPIDbContext, mirroring DemoDataSeeder.cs's own pattern (see also
        // ExpandFieldsFilterQueryConformanceTests.Expand_RoomBuilding_ReturnsFullNestedBuildingObject).
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
                PrimaryCode = $"TIER4-ROOM-{roomId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier 4 Regression Room\"}]",
                Latitude = 52.089123,
                Longitude = 5.113337
            });
            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        Room? room = await client.GetFromJsonAsync<Room>($"/rooms/{roomId}");

        Assert.NotNull(room!.Geolocation);
        Assert.Equal(52.089123, room.Geolocation!.Latitude);
        Assert.Equal(5.113337, room.Geolocation.Longitude);
    }

    [Fact]
    public async Task NestedTestComponentOfferingAssociations_WithAttempt_ReturnsAttemptIds()
    {
        // TestComponentOffering/TestComponentOfferingAssociation/Attempt have no simple direct write
        // path exercised by this test - seeded directly through SqlServerOEAPIDbContext, same rationale
        // as the Room fixture above.
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid offeringId = Guid.CreateVersion7();
        Guid associationId = Guid.CreateVersion7();
        Guid attemptId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();
            dbContext.TestComponentOfferings.Add(new TestComponentOfferingEntity
            {
                Id = offeringId,
                TestComponentOfferingIdValue = offeringId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"TIER4-TCO-{offeringId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier 4 Regression Offering\"}]"
            });
            dbContext.TestComponentOfferingAssociations.Add(new TestComponentOfferingAssociationEntity
            {
                Id = associationId,
                TestComponentOfferingAssociationIdValue = associationId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"TIER4-TCOA-{associationId.ToString()[..8]}",
                Role = "student",
                State = "associated",
                TestComponentOfferingEntityId = offeringId
            });
            dbContext.TestComponentOfferingAssociationAttempts.Add(new TestComponentOfferingAssociationAttemptEntity
            {
                Id = attemptId,
                AttemptIdValue = attemptId.ToString(),
                Attempt = 1,
                State = "completed",
                TestComponentOfferingAssociationEntityId = associationId
            });
            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        JsonObject json = await GetJsonAsync(client,
            $"/test-component-offerings/{offeringId}/test-component-offering-associations");
        JsonObject association = json["items"]!.AsArray().Single()!.AsObject();

        JsonArray attemptIds = Assert.IsType<JsonArray>(association["attemptIds"]);
        Assert.Equal(attemptId.ToString(), Assert.Single(attemptIds)!.GetValue<string>());
    }

    private static async Task<JsonObject> GetJsonAsync(HttpClient client, string requestUri)
    {
        HttpResponseMessage response = await client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        return JsonNode.Parse(await response.Content.ReadAsStringAsync())!.AsObject();
    }
}
