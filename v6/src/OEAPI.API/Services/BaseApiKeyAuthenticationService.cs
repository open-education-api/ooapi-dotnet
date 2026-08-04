using OEAPI.API.Configuration;
using OEAPI.Core.Interfaces;

namespace OEAPI.API.Services;

/// <summary>
///     Base implementation of API key authentication service that institutions can extend.
///     This provides a starting point for API key validation.
///     Institutions must implement the abstract methods to provide their own API key validation logic.
/// </summary>
/// <remarks>
///     Initializes a new instance of the BaseApiKeyAuthenticationService class.
/// </remarks>
/// <param name="apiKeyConfig">The API key configuration.</param>
public abstract class BaseApiKeyAuthenticationService(ApiKeyConfiguration apiKeyConfig) : IApiKeyAuthenticationService
{

    /// <summary>
    ///     Gets the API key configuration for use by derived classes.
    /// </summary>
    protected ApiKeyConfiguration ApiKeyConfig { get; } = apiKeyConfig ?? throw new ArgumentNullException(nameof(apiKeyConfig));

    /// <summary>
    ///     Gets the API key header name.
    /// </summary>
    public string ApiKeyHeaderName => ApiKeyConfig.HeaderName;

    /// <summary>
    ///     Gets the API key from the current request headers.
    /// </summary>
    /// <returns>The API key or null if not present.</returns>
    public virtual string? GetApiKey()
    {
        if (ApiKeyConfig.UseAuthorizationHeader)
            return GetApiKeyFromAuthorizationHeader();
        return GetApiKeyFromCustomHeader();
    }

    /// <summary>
    ///     Authenticates the current request using API key.
    /// </summary>
    /// <returns>Authentication result.</returns>
    public async Task<AuthenticationResult> AuthenticateAsync()
    {
        string? apiKey = GetApiKey();
        if (string.IsNullOrEmpty(apiKey))
            return new AuthenticationResult
            {
                IsAuthenticated = false,
                ErrorMessage = "No API key found in request"
            };

        return await ValidateApiKeyAsync(apiKey);
    }

    /// <summary>
    ///     Validates the provided API key and returns the authentication result.
    /// </summary>
    /// <param name="apiKey">The API key to validate.</param>
    /// <returns>Authentication result with user information.</returns>
    public async Task<AuthenticationResult> ValidateApiKeyAsync(string apiKey)
    {
        try
        {
            // Lookup the API key and get user information
            ApiKeyUserInfo? userInfo = await LookupApiKeyAsync(apiKey);

            if (userInfo == null)
                return new AuthenticationResult
                {
                    IsAuthenticated = false,
                    ErrorMessage = "Invalid API key"
                };

            return new AuthenticationResult
            {
                IsAuthenticated = true,
                UserId = userInfo.UserId,
                UserName = userInfo.UserName,
                InstitutionId = userInfo.InstitutionId,
                ConsumerKey = userInfo.ConsumerKey,
                Roles = userInfo.Roles,
                AuthenticationScheme = "ApiKey"
            };
        }
        catch (Exception ex)
        {
            return new AuthenticationResult
            {
                IsAuthenticated = false,
                ErrorMessage = $"API key validation failed: {ex.Message}"
            };
        }
    }

    /// <summary>
    ///     Gets the API key from the Authorization header.
    /// </summary>
    /// <returns>The API key or null if not present.</returns>
    protected virtual string? GetApiKeyFromAuthorizationHeader()
    {
        string? authHeader = GetAuthorizationHeader();
        if (string.IsNullOrEmpty(authHeader)) return null;

        // Remove prefix if present
        if (!string.IsNullOrEmpty(ApiKeyConfig.HeaderPrefix) &&
            authHeader.StartsWith(ApiKeyConfig.HeaderPrefix, StringComparison.OrdinalIgnoreCase))
            return authHeader.Substring(ApiKeyConfig.HeaderPrefix.Length).Trim();

        return authHeader;
    }

    /// <summary>
    ///     Gets the API key from a custom header.
    /// </summary>
    /// <returns>The API key or null if not present.</returns>
    protected virtual string? GetApiKeyFromCustomHeader()
    {
        return GetHeaderValue(ApiKeyConfig.HeaderName);
    }

    /// <summary>
    ///     Gets the Authorization header value.
    ///     Institutions must implement this method to provide access to HTTP headers.
    /// </summary>
    /// <returns>The Authorization header value or null if not present.</returns>
    protected abstract string? GetAuthorizationHeader();

    /// <summary>
    ///     Gets a header value by name.
    ///     Institutions must implement this method to provide access to HTTP headers.
    /// </summary>
    /// <param name="headerName">The header name.</param>
    /// <returns>The header value or null if not present.</returns>
    protected abstract string? GetHeaderValue(string headerName);

    /// <summary>
    ///     Looks up an API key and returns user information.
    ///     Institutions must implement this method to provide their own API key validation.
    ///     This typically involves checking against a database or external service.
    /// </summary>
    /// <param name="apiKey">The API key to look up.</param>
    /// <returns>User information or null if API key is invalid.</returns>
    protected abstract Task<ApiKeyUserInfo?> LookupApiKeyAsync(string apiKey);
}

/// <summary>
///     User information returned from API key lookup.
/// </summary>
public class ApiKeyUserInfo
{
    /// <summary>
    ///     Gets or sets the user identifier.
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the user name for display purposes.
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    ///     Gets or sets the institution identifier.
    /// </summary>
    public string? InstitutionId { get; set; }

    /// <summary>
    ///     Gets or sets the consumer key for data filtering.
    /// </summary>
    public string? ConsumerKey { get; set; }

    /// <summary>
    ///     Gets or sets the roles associated with the user.
    /// </summary>
    public string[]? Roles { get; set; }

    /// <summary>
    ///     Gets or sets additional claims for the user.
    /// </summary>
    public Dictionary<string, string>? Claims { get; set; }
}
