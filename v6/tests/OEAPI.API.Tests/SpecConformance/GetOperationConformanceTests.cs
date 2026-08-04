using System.Net;
using OEAPI.API.Tests.Infrastructure;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Walks every <c>GET</c> operation in the real spec (via the generated <c>spec-manifest.json</c>)
///     and asserts the route actually resolves - no 405/500 on
///     any endpoint, and a real 200 with a correctly-shaped page for every top-level list endpoint.
///     This is the automated replacement for manually hitting individual endpoints to confirm no
///     routing errors: it runs against every GET path the spec defines, not just a hand-picked subset.
/// </summary>
/// <remarks>
///     Runs against SQL Server only - this test is a routing/contract-shape concern, not a
///     provider-specific one (provider differences, e.g. <c>LIKE</c> casing, are covered by the
///     targeted deep tests instead, once written).
/// </remarks>
[Collection(SqlServerCollection.Name)]
public class GetOperationConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    public static IEnumerable<object[]> GetOperations()
    {
        return SpecManifest.Operations
            .Where(operation => operation.Method == "GET")
            .Select(operation => new object[] { operation });
    }

    [Theory]
    [MemberData(nameof(GetOperations))]
    public async Task Get_SpecPath_ResolvesWithoutRoutingError(SpecOperation operation)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();

        string path = SpecManifest.BuildConcretePath(operation);
        HttpResponseMessage response = await client.GetAsync(path);

        // Universal invariant, true for every GET the spec defines: 405 (no route registered for
        // this method at this path) and 500 (an unhandled exception) can only mean a routing/server
        // bug, never a legitimate business outcome. A 404 is deliberately *not* excluded here - a
        // fresh, empty container has no rows, so by-id/nested-by-id GETs legitimately 404; a 401 is
        // also legitimate for the one auth-gated GET (/persons/me) since no auth provider is
        // configured in this test host.
        Assert.True(
            response.StatusCode is not (HttpStatusCode.MethodNotAllowed or HttpStatusCode.InternalServerError),
            $"{operation} returned {(int)response.StatusCode} {response.StatusCode} - indicates a routing or server bug.");

        // Stronger invariant for top-level paginated lists only (e.g. /organisations, not
        // /organisations/{id}/course-offerings, which legitimately 404s if the parent id doesn't
        // exist): there's no parent to be missing, so this must always succeed with a real,
        // correctly-shaped empty page - proves pagination wiring and DB connectivity, not just
        // that a route exists.
        if (operation.IsTopLevelListGet)
        {
            Assert.True(
                response.StatusCode == HttpStatusCode.OK,
                $"{operation} (top-level list) returned {(int)response.StatusCode} {response.StatusCode}, expected 200.");

            string body = await response.Content.ReadAsStringAsync();
            Assert.Contains("\"items\"", body);
        }
    }
}
