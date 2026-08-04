using System.Net;
using System.Net.Http.Headers;
using OEAPI.API.Tests.Infrastructure;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Walks every <c>PUT</c>/<c>POST</c>/<c>PATCH</c> operation in the real spec and asserts the route
///     resolves without a routing/server-level failure, using a minimal (structurally valid but
///     semantically empty, <c>{}</c>) body of the content type the spec declares. This is deliberately
///     an "existence" check, not a correctness check - it proves the route and content-type are wired
///     up and that missing/incomplete data is rejected gracefully (400/404/401), not that a write with
///     real data actually persists correctly. That's what <see cref="PutUpsertConformanceTests" /> and
///     <see cref="PatchAssociationConformanceTests" /> are for, once seeded/valid request bodies are involved.
/// </summary>
/// <remarks>
///     A <c>{}</c> body against a required-fields schema is exactly the kind of input a defensive
///     controller should turn into a clean <c>400</c> - if it instead throws (e.g. a null-reference
///     resolving a required FK that model binding left <see langword="null" />), that surfaces as a real
///     <c>500</c> here, which this test correctly flags. Runs against SQL Server only, same reasoning
///     as <see cref="GetOperationConformanceTests" />.
/// </remarks>
[Collection(SqlServerCollection.Name)]
public class WriteOperationConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    public static IEnumerable<object[]> WriteOperations()
    {
        return SpecManifest.Operations
            .Where(operation => operation.Method is "PUT" or "POST" or "PATCH")
            .Select(operation => new object[] { operation });
    }

    [Theory]
    [MemberData(nameof(WriteOperations))]
    public async Task Write_SpecPath_ResolvesWithoutRoutingError(SpecOperation operation)
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();

        string path = SpecManifest.BuildConcretePath(operation);
        string contentType = operation.RequestBody?.ContentType ?? "application/json";
        using StringContent content = new("{}");
        content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

        HttpResponseMessage response = operation.Method switch
        {
            "PUT" => await client.PutAsync(path, content),
            "POST" => await client.PostAsync(path, content),
            "PATCH" => await client.PatchAsync(path, content),
            _ => throw new InvalidOperationException($"Unexpected write method '{operation.Method}'.")
        };

        // Same universal invariant as the GET conformance test: 405 (no route registered for this
        // method/content-type at this path) and 500 (unhandled exception) can only mean a
        // routing/server bug. Everything else - 200/201/202 (unlikely with an empty body, but
        // possible for schemas with no required fields), 400 (missing required fields, the
        // expected outcome for most of these), 401 (the external/me endpoints, unauthenticated),
        // 404 (placeholder id/parent doesn't exist) - is a legitimate business outcome.
        Assert.True(
            response.StatusCode is not (HttpStatusCode.MethodNotAllowed or HttpStatusCode.InternalServerError),
            $"{operation} returned {(int)response.StatusCode} {response.StatusCode} - indicates a routing or server bug.");
    }
}
