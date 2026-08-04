namespace OEAPI.Core.Interfaces;

/// <summary>
///     Resolves which Person the current authenticated request corresponds to. This is the one piece of
///     the "external/me" endpoints (e.g. <c>GET /persons/me</c>, <c>POST .../external/me</c>) that is
///     genuinely deployment specific: the spec deliberately requires person info to be obtained "from a
///     well known endpoint" (e.g. an institution's OIDC UserInfo endpoint) rather than defining any
///     concrete mechanism itself. Institutions implement this to map their own IAM's authenticated
///     identity onto an OOAPI <c>personId</c> - whether by looking up an existing person, or by
///     provisioning one from claims obtained out of band. Everything else these endpoints do (parsing
///     the request body, resolving the offering, building the association/response) is ordinary
///     business logic that does not depend on this interface.
/// </summary>
public interface ICurrentPersonProvider
{
    /// <summary>
    ///     Resolves the OOAPI <c>personId</c> of the currently authenticated caller, or <c>null</c> if
    ///     none can be resolved (e.g. the request isn't authenticated, or the identity has no
    ///     corresponding person).
    /// </summary>
    /// <param name="authenticationResult">
    ///     The result produced by the API's authentication middleware for the current request, or
    ///     <c>null</c> if authentication did not run or did not succeed.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<string?> GetCurrentPersonIdAsync(AuthenticationResult? authenticationResult,
        CancellationToken cancellationToken = default);
}
