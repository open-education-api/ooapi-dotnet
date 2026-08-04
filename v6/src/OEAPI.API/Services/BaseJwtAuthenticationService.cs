using OEAPI.API.Configuration;
using OEAPI.Core.Interfaces;

namespace OEAPI.API.Services;

/// <summary>
///     Base implementation of JWT authentication service that institutions can extend.
///     This provides a starting point for JWT validation with configurable options.
///     Institutions must implement the abstract methods to provide their own JWT validation logic.
/// </summary>
/// <remarks>
///     Initializes a new instance of the BaseJwtAuthenticationService class.
/// </remarks>
/// <param name="jwtConfig">The JWT configuration.</param>
public abstract class BaseJwtAuthenticationService(JwtConfiguration jwtConfig) : IJwtAuthenticationService
{

    /// <summary>
    ///     Gets the JWT configuration for use by derived classes.
    /// </summary>
    protected JwtConfiguration JwtConfig { get; } = jwtConfig ?? throw new ArgumentNullException(nameof(jwtConfig));

    /// <summary>
    ///     Gets the JWT bearer token from the current request.
    /// </summary>
    /// <returns>The JWT token or null if not present.</returns>
    public virtual string? GetToken()
    {
        // Institutions should implement this based on their HTTP context access
        // This is typically done by accessing HttpContext.Request.Headers["Authorization"]
        return GetTokenFromRequest();
    }

    /// <summary>
    ///     Authenticates the current request using JWT.
    /// </summary>
    /// <returns>Authentication result.</returns>
    public async Task<AuthenticationResult> AuthenticateAsync()
    {
        string? token = GetToken();
        if (string.IsNullOrEmpty(token))
            return new AuthenticationResult
            {
                IsAuthenticated = false,
                ErrorMessage = "No JWT token found in request"
            };

        return await ValidateTokenAsync(token);
    }

    /// <summary>
    ///     Validates the provided JWT token and returns the authentication result.
    /// </summary>
    /// <param name="token">The JWT token to validate.</param>
    /// <returns>Authentication result with claims and user information.</returns>
    public async Task<AuthenticationResult> ValidateTokenAsync(string token)
    {
        try
        {
            AuthenticationResult result = await ValidateTokenInternalAsync(token);
            if (result.IsAuthenticated) result.AuthenticationScheme = "Bearer";
            return result;
        }
        catch (Exception ex)
        {
            return new AuthenticationResult
            {
                IsAuthenticated = false,
                ErrorMessage = $"JWT validation failed: {ex.Message}"
            };
        }
    }

    /// <summary>
    ///     Gets the token from the current HTTP request.
    ///     Institutions must implement this method to provide access to the HTTP context.
    /// </summary>
    /// <returns>The JWT token or null if not present.</returns>
    protected abstract string? GetTokenFromRequest();

    /// <summary>
    ///     Validates the provided JWT token and returns the authentication result.
    ///     Institutions must implement this method to provide their own JWT validation logic.
    ///     This is where institutions can use their preferred JWT library (e.g., Microsoft.IdentityModel.Tokens.Jwt, JWT,
    ///     etc.)
    /// </summary>
    /// <param name="token">The JWT token to validate.</param>
    /// <returns>Authentication result with claims and user information.</returns>
    protected abstract Task<AuthenticationResult> ValidateTokenInternalAsync(string token);
}
