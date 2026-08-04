using Microsoft.Extensions.DependencyInjection;
using OEAPI.API.Tests.Infrastructure;
using OEAPI.Infrastructure.Data.Context;
using OEAPI.Infrastructure.Data.Entities;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     The spec declares no custom <c>X-*</c> response headers anywhere - every field a client can
///     rely on is described in the response body schema. Guards generally against any custom header
///     creeping back in, not just a specific past one, since anything living only in a header is
///     invisible to schema-based conformance checking.
/// </summary>
[Collection(SqlServerCollection.Name)]
public class RemoveUndeclaredPaginationHeadersConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Fact]
    public async Task GetAll_ResponseCarriesNoCustomHeaders()
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
                PrimaryCode = $"HDR-ROOM-{roomId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Header Test Room\"}]"
            });
            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();
        HttpResponseMessage response = await client.GetAsync("/rooms");
        response.EnsureSuccessStatusCode();

        Assert.DoesNotContain(response.Headers, h => h.Key.StartsWith("X-", StringComparison.OrdinalIgnoreCase));
    }
}
