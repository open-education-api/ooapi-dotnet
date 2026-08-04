namespace OEAPI.Core.Interfaces;

/// <summary>
///     Interface for JWT (Bearer token) authentication services.
///     Institutions can implement this to provide their own JWT validation logic.
/// </summary>
public interface IJwtAuthenticationService : IAuthenticationService
{
    /// <summary>
    ///     Gets the JWT bearer token from the current request.
    /// </summary>
    /// <returns>The JWT token or null if not present.</returns>
    string? GetToken();

    /// <summary>
    ///     Validates the provided JWT token and returns the authentication result.
    /// </summary>
    /// <param name="token">The JWT token to validate.</param>
    /// <returns>Authentication result with claims and user information.</returns>
    Task<AuthenticationResult> ValidateTokenAsync(string token);
}

/// <summary>
///     Interface for API key (header-based) authentication services.
///     Institutions can implement this to provide their own API key validation logic.
/// </summary>
public interface IApiKeyAuthenticationService : IAuthenticationService
{
    /// <summary>
    ///     Gets the header name used for API key authentication.
    ///     Default is typically "X-API-Key" or similar.
    /// </summary>
    string ApiKeyHeaderName { get; }

    /// <summary>
    ///     Gets the API key from the current request headers.
    /// </summary>
    /// <returns>The API key or null if not present.</returns>
    string? GetApiKey();

    /// <summary>
    ///     Validates the provided API key and returns the authentication result.
    /// </summary>
    /// <param name="apiKey">The API key to validate.</param>
    /// <returns>Authentication result with user information.</returns>
    Task<AuthenticationResult> ValidateApiKeyAsync(string apiKey);
}
