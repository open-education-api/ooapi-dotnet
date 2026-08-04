namespace OEAPI.API.Configuration;

/// <summary>
///     Configuration for authentication settings.
///     Institutions can configure their preferred authentication method and settings.
/// </summary>
public class AuthenticationConfiguration
{
    /// <summary>
    ///     Gets or sets the enabled authentication providers.
    ///     Multiple providers can be enabled simultaneously.
    /// </summary>
    public AuthenticationProviders Providers { get; set; } = new();

    /// <summary>
    ///     Gets or sets JWT-specific configuration.
    /// </summary>
    public JwtConfiguration Jwt { get; set; } = new();

    /// <summary>
    ///     Gets or sets API key-specific configuration.
    /// </summary>
    public ApiKeyConfiguration ApiKey { get; set; } = new();

    /// <summary>
    ///     Gets or sets a value indicating whether authentication is required for all endpoints.
    ///     If false, authentication is optional and endpoints can be accessed without authentication.
    /// </summary>
    public bool RequireAuthentication { get; set; } = true;

    /// <summary>
    ///     Gets or sets a value indicating whether to enable anonymous access for specific endpoints.
    ///     This allows for public endpoints that don't require authentication.
    /// </summary>
    public bool AllowAnonymousAccess { get; set; } = true;
}

/// <summary>
///     Configuration for enabled authentication providers.
/// </summary>
public class AuthenticationProviders
{
    /// <summary>
    ///     Gets or sets a value indicating whether JWT authentication is enabled.
    /// </summary>
    public bool JwtEnabled { get; set; } = false;

    /// <summary>
    ///     Gets or sets a value indicating whether API key authentication is enabled.
    /// </summary>
    public bool ApiKeyEnabled { get; set; } = false;

    /// <summary>
    ///     Gets or sets a value indicating whether custom authentication is enabled.
    ///     This allows institutions to implement their own authentication schemes.
    /// </summary>
    public bool CustomEnabled { get; set; } = false;
}

/// <summary>
///     JWT-specific configuration.
/// </summary>
public class JwtConfiguration
{
    /// <summary>
    ///     Gets or sets the JWT issuer to validate against.
    /// </summary>
    public string? Issuer { get; set; }

    /// <summary>
    ///     Gets or sets the JWT audience to validate against.
    /// </summary>
    public string? Audience { get; set; }

    /// <summary>
    ///     Gets or sets the JWT secret key used for signature validation.
    ///     This is typically used for symmetric key validation.
    /// </summary>
    public string? SecretKey { get; set; }

    /// <summary>
    ///     Gets or sets the JWT validation parameters.
    ///     This can include clock skew, algorithm requirements, etc.
    /// </summary>
    public Dictionary<string, string> ValidationParameters { get; set; } = [];

    /// <summary>
    ///     Gets or sets a value indicating whether to validate the JWT signature.
    /// </summary>
    public bool ValidateSignature { get; set; } = true;

    /// <summary>
    ///     Gets or sets a value indicating whether to validate the JWT issuer.
    /// </summary>
    public bool ValidateIssuer { get; set; } = true;

    /// <summary>
    ///     Gets or sets a value indicating whether to validate the JWT audience.
    /// </summary>
    public bool ValidateAudience { get; set; } = true;

    /// <summary>
    ///     Gets or sets a value indicating whether to validate the JWT expiration.
    /// </summary>
    public bool ValidateLifetime { get; set; } = true;

    /// <summary>
    ///     Gets or sets the clock skew in seconds for JWT expiration validation.
    /// </summary>
    public int ClockSkewSeconds { get; set; } = 300; // 5 minutes default
}

/// <summary>
///     API key-specific configuration.
/// </summary>
public class ApiKeyConfiguration
{
    /// <summary>
    ///     Gets or sets the header name for API key authentication.
    ///     Default is "X-API-Key".
    /// </summary>
    public string HeaderName { get; set; } = "X-API-Key";

    /// <summary>
    ///     Gets or sets the prefix for the API key header (e.g., "Bearer " for Authorization header).
    ///     Leave empty if no prefix is used.
    /// </summary>
    public string? HeaderPrefix { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether to use the Authorization header instead of a custom header.
    ///     When true, the API key is expected in the Authorization header with the specified prefix.
    /// </summary>
    public bool UseAuthorizationHeader { get; set; } = false;

    /// <summary>
    ///     Gets or sets a value indicating whether to enable caching of validated API keys.
    /// </summary>
    public bool EnableCaching { get; set; } = true;

    /// <summary>
    ///     Gets or sets the cache duration in minutes for validated API keys.
    /// </summary>
    public int CacheDurationMinutes { get; set; } = 60;
}
