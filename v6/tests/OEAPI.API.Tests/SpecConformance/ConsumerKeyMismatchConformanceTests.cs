using System.Text.Json.Nodes;
using Microsoft.Extensions.DependencyInjection;
using OEAPI.API.Tests.Infrastructure;
using OEAPI.Infrastructure.Data.Context;
using OEAPI.Infrastructure.Data.Entities;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Guards a bug found and fixed via `fix-conformance-audit-findings` task 2:
///     `GenericEntityController.IsConsumerMatchAsync` (the single-item `GetById` path) had no
///     null-`ConsumerKey` exemption, unlike `ConsumerKeyFilter` (the list path), which explicitly
///     treats a `null` `ConsumerKey` as "not tied to any particular consumer, fine to return to
///     anyone" and always matches. Confirmed live before the fix: `GET /academic-sessions?consumer=rio`
///     correctly included a null-`ConsumerKey` row, but `GET /academic-sessions/{that-same-id}
///     ?consumer=rio` 404'd for the exact same resource - a client could list a resource, then fail to
///     fetch it by the id the list just gave them. Also guards that a genuinely *different*, non-null
///     `ConsumerKey` still 404s on mismatch - only the null case was ever wrong.
/// </summary>
[Collection(SqlServerCollection.Name)]
public class ConsumerKeyMismatchConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Fact]
    public async Task GetById_WithNullConsumerKey_MatchesAnyRequestedConsumer()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid sessionId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            // No ConsumerKey/ConsumerJson set at all - not tied to any particular consumer.
            AcademicSessionEntity session = new()
            {
                Id = sessionId,
                AcademicSessionId = sessionId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"CKM-AS-{sessionId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Consumer Mismatch Session\"}]",
                AcademicSessionType = "semester",
                StartDateTime = "2025-09-01T00:00:00+01:00",
                EndDateTime = "2026-01-31T23:59:59+01:00"
            };
            dbContext.AcademicSessions.Add(session);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        // The list endpoint already includes a null-ConsumerKey row for any requested consumer.
        JsonObject list = await GetJsonAsync(client, "/academic-sessions?consumer=rio");
        Assert.Contains(list["items"]!.AsArray(),
            i => i!["academicSessionId"]!.GetValue<string>() == sessionId.ToString());

        // The single-item lookup for that exact same resource must not 404 just because a consumer
        // was requested that the resource happens to have no opinion about.
        HttpResponseMessage response = await client.GetAsync($"/academic-sessions/{sessionId}?consumer=rio");
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetById_WithGenuinelyDifferentConsumerKey_StillReturnsNotFound()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid sessionId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            AcademicSessionEntity session = new()
            {
                Id = sessionId,
                AcademicSessionId = sessionId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"CKM-AS2-{sessionId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Consumer Mismatch Session 2\"}]",
                AcademicSessionType = "semester",
                StartDateTime = "2025-09-01T00:00:00+01:00",
                EndDateTime = "2026-01-31T23:59:59+01:00",
                ConsumerKey = "eduxchange",
                ConsumerJson = "{\"consumerKey\":\"eduxchange\"}"
            };
            dbContext.AcademicSessions.Add(session);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        HttpResponseMessage matching = await client.GetAsync($"/academic-sessions/{sessionId}?consumer=eduxchange");
        Assert.Equal(System.Net.HttpStatusCode.OK, matching.StatusCode);

        HttpResponseMessage mismatched = await client.GetAsync($"/academic-sessions/{sessionId}?consumer=rio");
        Assert.Equal(System.Net.HttpStatusCode.NotFound, mismatched.StatusCode);
    }

    private static async Task<JsonObject> GetJsonAsync(HttpClient client, string requestUri)
    {
        HttpResponseMessage response = await client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        return JsonNode.Parse(await response.Content.ReadAsStringAsync())!.AsObject();
    }
}
