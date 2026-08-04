using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using OEAPI.API.Tests.Infrastructure;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Infrastructure.Data.Context;
using OEAPI.Infrastructure.Data.Entities;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Guards a real functional gap found and fixed while auditing the flat attempt-resource
///     real-id-resolution item on `docs/TODO-LIST.md`: <c>GET /test-component-offering-associations/
///     {id}/url</c> unconditionally returned <c>""</c>, violating the spec's required, non-nullable
///     <c>format: uri</c> <c>Url</c> schema - no test ever caught it because no backing field existed
///     to test against. Now backed by a real <c>Url</c> column, populated by <c>DemoDataSeeder.cs</c>
///     for seeded fixtures and auto-generated server-side (never client-writable - it isn't a property
///     of the <c>TestComponentOfferingAssociation</c> model) for associations created via <c>PUT</c>.
/// </summary>
[Collection(SqlServerCollection.Name)]
public class TestComponentOfferingAssociationUrlConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Fact]
    public async Task GetUrl_ForASeededAssociation_ReturnsTheRealUrl_NotAnEmptyPlaceholder()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid associationId = Guid.CreateVersion7();
        const string expectedUrl = "https://osiris.example.org/test-tool/start/regression-check";

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();
            dbContext.TestComponentOfferingAssociations.Add(new TestComponentOfferingAssociationEntity
            {
                Id = associationId,
                TestComponentOfferingAssociationIdValue = associationId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"URLTEST-{associationId.ToString()[..8]}",
                Role = "student",
                State = "associated",
                Url = expectedUrl
            });
            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        HttpResponseMessage response =
            await client.GetAsync($"/test-component-offering-associations/{associationId}/url");
        response.EnsureSuccessStatusCode();
        string url = await response.Content.ReadAsStringAsync();

        Assert.Equal($"\"{expectedUrl}\"", url);
        Assert.True(Uri.TryCreate(expectedUrl, UriKind.Absolute, out _));
    }

    [Fact]
    public async Task PutTestComponentOfferingAssociation_NewAssociation_GetsARealGeneratedUrl()
    {
        // Url isn't a property of the TestComponentOfferingAssociation write model at all - the spec
        // only exposes it via the dedicated GET .../url sub-resource, always server-generated. This
        // proves the PUT-created (not seeded) path also ends up with a real, non-empty URI, not the
        // string.Empty a freshly-constructed entity would default to if nothing set it explicitly.
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        TestComponentOfferingAssociation created = new()
        {
            AssociationIdValue = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"URLPUT-{id[..8]}" },
            Role = "student",
            State = "pending"
        };

        HttpResponseMessage createResponse =
            await client.PutAsJsonAsync($"/test-component-offering-associations/{id}", created);
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        HttpResponseMessage urlResponse =
            await client.GetAsync($"/test-component-offering-associations/{id}/url");
        urlResponse.EnsureSuccessStatusCode();
        string url = (await urlResponse.Content.ReadFromJsonAsync<string>())!;

        Assert.False(string.IsNullOrEmpty(url));
        Assert.True(Uri.TryCreate(url, UriKind.Absolute, out _), $"'{url}' is not a valid absolute URI.");
    }
}
