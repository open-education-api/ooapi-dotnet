using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using OEAPI.API.Tests.Infrastructure;
using OEAPI.Core.Interfaces;
using OEAPI.Infrastructure.Data.Context;
using OEAPI.Infrastructure.Data.Entities;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Confirms <c>GET /documents/{documentId}</c> - which has no write endpoint, so its fixture is
///     seeded directly through <see cref="SqlServerOEAPIDbContext" />, mirroring
///     <c>ExpandFieldsFilterQueryConformanceTests.Expand_RoomBuilding_...</c>'s own pattern - accepts
///     the spec-declared <c>consumer</c> query parameter without erroring, and that the default,
///     database-backed <c>IDocumentStorageProvider</c> still returns the same content regardless of
///     which (or no) consumer is requested, since it stores exactly one content blob per document.
/// </summary>
[Collection(SqlServerCollection.Name)]
public class DocumentsControllerConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Fact]
    public async Task GetDocumentContent_WithConsumerParameter_StillReturnsContent()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid documentId = Guid.CreateVersion7();
        byte[] expectedBytes = "Consumer-parameter regression test content."u8.ToArray();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();
            dbContext.Documents.Add(new DocumentEntity
            {
                Id = documentId,
                DocumentId = documentId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"CONS-DOC-{documentId.ToString()[..8]}",
                Name = "consumer-test.txt",
                DocumentType = "additional_document",
                MimeType = "text/plain",
                Content = expectedBytes
            });
            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        HttpResponseMessage withoutConsumer = await client.GetAsync($"/documents/{documentId}");
        HttpResponseMessage withConsumer = await client.GetAsync($"/documents/{documentId}?consumer=rio");

        Assert.Equal(HttpStatusCode.OK, withoutConsumer.StatusCode);
        Assert.Equal(HttpStatusCode.OK, withConsumer.StatusCode);
        Assert.Equal(expectedBytes, await withoutConsumer.Content.ReadAsByteArrayAsync());
        Assert.Equal(expectedBytes, await withConsumer.Content.ReadAsByteArrayAsync());
    }

    [Fact]
    public async Task GetDocumentContent_ConsumerQueryParameter_ReachesStorageProvider()
    {
        // The database-backed default provider ignores consumer entirely (see
        // GetDocumentContent_WithConsumerParameter_StillReturnsContent above), so a 200/content
        // assertion against it alone can't actually prove the controller's [FromQuery] consumer
        // parameter reaches IDocumentStorageProvider.GetContentAsync - only that nothing crashes. This
        // swaps in a recording test double via ConfigureTestServices to prove the wiring directly.
        byte[] content = "Wiring-proof content."u8.ToArray();
        RecordingDocumentStorageProvider recordingProvider = new(content);

        using WebApplicationFactory<Program> factory = _fixture.CreateFactory()
            .WithWebHostBuilder(builder => builder.ConfigureTestServices(services =>
                services.AddScoped<IDocumentStorageProvider>(_ => recordingProvider)));

        using HttpClient client = factory.CreateClient();
        HttpResponseMessage response = await client.GetAsync("/documents/any-id?consumer=rio");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("rio", recordingProvider.LastReceivedConsumer);
    }
}
