using OEAPI.Core.Interfaces;

namespace OEAPI.API.Services;

/// <summary>
///     Safe default <see cref="ICurrentPersonProvider" />: treats <see cref="AuthenticationResult.UserId" />
///     as directly matching <c>PersonEntity.PersonId</c>, and never auto-provisions a person. This
///     requires no external configuration, so it is registered by default. Institutions whose IAM's
///     subject claim doesn't already equal an OOAPI <c>personId</c> - or who want first-login
///     auto-provisioning from well-known-endpoint claims - should register their own
///     <see cref="ICurrentPersonProvider" /> implementation instead (see
///     <c>ConfigureAuthenticationServices</c> in <c>Program.cs</c>).
/// </summary>
public class DefaultCurrentPersonProvider : ICurrentPersonProvider
{
    /// <inheritdoc />
    public Task<string?> GetCurrentPersonIdAsync(AuthenticationResult? authenticationResult,
        CancellationToken cancellationToken = default)
    {
        string? personId = authenticationResult?.IsAuthenticated == true ? authenticationResult.UserId : null;
        return Task.FromResult(personId);
    }
}
