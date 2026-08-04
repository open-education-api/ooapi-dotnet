namespace OEAPI.Core.Interfaces;

/// <summary>
///     Interface for authentication services.
///     Institutions must implement this interface to provide their own authentication logic.
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    ///     Authenticates the current request and returns the authentication result.
    /// </summary>
    /// <returns>Authentication result containing principal, claims, and authentication status.</returns>
    Task<AuthenticationResult> AuthenticateAsync();
}

/// <summary>
///     Result of an authentication attempt.
/// </summary>
public class AuthenticationResult
{
    /// <summary>
    ///     Gets a value indicating whether authentication was successful.
    /// </summary>
    public bool IsAuthenticated { get; set; }

    /// <summary>
    ///     Gets or sets the authenticated user identifier (e.g., username, user ID).
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    ///     Gets or sets the user's name for display purposes.
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    ///     Gets or sets the authentication scheme that was used (e.g., "Bearer", "ApiKey").
    /// </summary>
    public string? AuthenticationScheme { get; set; }

    /// <summary>
    ///     Gets or sets the institution identifier that the user belongs to.
    ///     This can be used for multi-tenancy scenarios.
    /// </summary>
    public string? InstitutionId { get; set; }

    /// <summary>
    ///     Gets or sets the roles associated with the authenticated user.
    /// </summary>
    public string[]? Roles { get; set; }

    /// <summary>
    ///     Gets or sets additional claims associated with the authenticated user.
    /// </summary>
    public Dictionary<string, string>? Claims { get; set; }

    /// <summary>
    ///     Gets or sets error message if authentication failed.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    ///     Gets or sets the consumer key associated with this authentication context.
    ///     This can be used to filter data based on the consumer.
    /// </summary>
    public string? ConsumerKey { get; set; }
}
