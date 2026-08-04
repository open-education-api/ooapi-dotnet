using OEAPI.Core.Interfaces;

namespace OEAPI.API.Tests.Infrastructure;

/// <summary>
///     Test-only <see cref="ICurrentPersonProvider" /> that always resolves to a fixed personId,
///     regardless of the (nonexistent, in these tests) <see cref="AuthenticationResult" /> - simulates
///     "the caller is authenticated as this person" without needing to fake the actual authentication
///     middleware pipeline. Register via
///     <c>
///         factory.WithWebHostBuilder(b => b.ConfigureTestServices(s => s.AddScoped&lt;ICurrentPersonProvider&gt;(_ =&gt;
///         new FakeCurrentPersonProvider(personId))))
///     </c>
///     - unlike <c>Program.cs</c>'s eager configuration read (see <see cref="OeapiWebApplicationFactory" />'s
///     doc comment), plain DI service registration overrides via <c>ConfigureTestServices</c> are
///     resolved lazily per-request, so this timing-sensitive-override problem doesn't apply here.
/// </summary>
public sealed class FakeCurrentPersonProvider(string personId) : ICurrentPersonProvider
{
    private readonly string _personId = personId;

    public Task<string?> GetCurrentPersonIdAsync(AuthenticationResult? authenticationResult,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult<string?>(_personId);
    }
}
