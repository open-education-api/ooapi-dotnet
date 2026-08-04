using Microsoft.AspNetCore.Authorization;
using OEAPI.API.Configuration;
using OEAPI.Core.Interfaces;
using IAuthenticationService = OEAPI.Core.Interfaces.IAuthenticationService;

namespace OEAPI.API.Middleware;

/// <summary>
///     Base authentication middleware that institutions can extend or use as-is.
///     This middleware attempts authentication using all enabled providers and sets
///     the authentication context for the current request.
/// </summary>
/// <remarks>
///     Initializes a new instance of the AuthenticationMiddleware class.
/// </remarks>
/// <param name="next">The next middleware in the pipeline.</param>
/// <param name="serviceProvider">The service provider.</param>
/// <param name="authConfig">The authentication configuration.</param>
/// <param name="logger">Logger for authentication failures.</param>
public class AuthenticationMiddleware(
    RequestDelegate next,
    IServiceProvider serviceProvider,
    AuthenticationConfiguration authConfig,
    ILogger<AuthenticationMiddleware> logger)
{
    private readonly AuthenticationConfiguration _authConfig = authConfig;
    private readonly ILogger<AuthenticationMiddleware> _logger = logger;
    private readonly RequestDelegate _next = next;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    /// <summary>
    ///     Invokes the middleware and attempts authentication.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        // Skip authentication if not required or for anonymous endpoints
        if (!_authConfig.RequireAuthentication &&
            context.GetEndpoint()?.Metadata.GetMetadata<AllowAnonymousAttribute>() != null)
        {
            await _next(context);
            return;
        }

        try
        {
            AuthenticationResult? authResult;

            // Try JWT authentication if enabled
            if (_authConfig.Providers.JwtEnabled)
            {
                IJwtAuthenticationService? jwtAuthService = _serviceProvider.GetService<IJwtAuthenticationService>();
                if (jwtAuthService != null)
                {
                    authResult = await jwtAuthService.AuthenticateAsync();
                    if (authResult.IsAuthenticated)
                    {
                        SetAuthenticationContext(context, authResult);
                        await _next(context);
                        return;
                    }
                }
            }

            // Try API key authentication if enabled
            if (_authConfig.Providers.ApiKeyEnabled)
            {
                IApiKeyAuthenticationService? apiKeyAuthService =
                    _serviceProvider.GetService<IApiKeyAuthenticationService>();
                if (apiKeyAuthService != null)
                {
                    authResult = await apiKeyAuthService.AuthenticateAsync();
                    if (authResult.IsAuthenticated)
                    {
                        SetAuthenticationContext(context, authResult);
                        await _next(context);
                        return;
                    }
                }
            }

            // Try custom authentication if enabled
            if (_authConfig.Providers.CustomEnabled)
            {
                IAuthenticationService? customAuthService =
                    _serviceProvider.GetService<IAuthenticationService>();
                if (customAuthService != null)
                {
                    authResult = await customAuthService.AuthenticateAsync();
                    if (authResult.IsAuthenticated)
                    {
                        SetAuthenticationContext(context, authResult);
                        await _next(context);
                        return;
                    }
                }
            }

            // If no authentication succeeded and authentication is required, return 401
            if (_authConfig.RequireAuthentication)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Authentication required");
                return;
            }
        }
        catch (Exception ex)
        {
            // ex.Message is deliberately not written to the response - a registered auth service
            // (e.g. one wrapping a database lookup) could throw an exception whose message carries
            // internal implementation details, and this response reaches unauthenticated callers.
            _logger.LogError(ex, "Authentication failed");
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Authentication failed");
            return;
        }

        // Continue without authentication (if allowed)
        await _next(context);
    }

    /// <summary>
    ///     Sets the authentication context on the HTTP context.
    ///     Institutions can override this method to customize how authentication
    ///     information is stored on the context.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <param name="authResult">The authentication result.</param>
    protected virtual void SetAuthenticationContext(HttpContext context, AuthenticationResult authResult)
    {
        // Store authentication result in HttpContext Items
        context.Items["AuthenticationResult"] = authResult;

        // Store user information in items for easy access
        context.Items["UserId"] = authResult.UserId;
        context.Items["UserName"] = authResult.UserName;
        context.Items["InstitutionId"] = authResult.InstitutionId;
        context.Items["ConsumerKey"] = authResult.ConsumerKey;
        context.Items["IsAuthenticated"] = authResult.IsAuthenticated;

        // Store roles if present
        if (authResult.Roles != null && authResult.Roles.Length > 0) context.Items["Roles"] = authResult.Roles;

        // Store claims if present
        if (authResult.Claims != null && authResult.Claims.Count > 0) context.Items["Claims"] = authResult.Claims;
    }
}

/// <summary>
///     Extension methods for authentication middleware.
/// </summary>
public static class AuthenticationMiddlewareExtensions
{
    /// <summary>
    ///     Adds the authentication middleware to the application pipeline.
    /// </summary>
    /// <param name="builder">The application builder.</param>
    /// <returns>The application builder for chaining.</returns>
    public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthenticationMiddleware>();
    }

    /// <summary>
    ///     Gets the authentication result from the current HTTP context.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>The authentication result or null if not authenticated.</returns>
    public static AuthenticationResult? GetAuthenticationResult(this HttpContext context)
    {
        return context.Items["AuthenticationResult"] as AuthenticationResult;
    }

    /// <summary>
    ///     Gets a value indicating whether the current request is authenticated.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>True if authenticated, false otherwise.</returns>
    public static bool IsAuthenticated(this HttpContext context)
    {
        return context.Items["IsAuthenticated"] as bool? ?? false;
    }

    /// <summary>
    ///     Gets the authenticated user ID from the current HTTP context.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>The user ID or null if not authenticated.</returns>
    public static string? GetUserId(this HttpContext context)
    {
        return context.Items["UserId"] as string;
    }

    /// <summary>
    ///     Gets the authenticated user name from the current HTTP context.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>The user name or null if not authenticated.</returns>
    public static string? GetUserName(this HttpContext context)
    {
        return context.Items["UserName"] as string;
    }

    /// <summary>
    ///     Gets the institution ID from the current HTTP context.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>The institution ID or null if not available.</returns>
    public static string? GetInstitutionId(this HttpContext context)
    {
        return context.Items["InstitutionId"] as string;
    }

    /// <summary>
    ///     Gets the consumer key from the current HTTP context.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>The consumer key or null if not available.</returns>
    public static string? GetConsumerKey(this HttpContext context)
    {
        return context.Items["ConsumerKey"] as string;
    }

    /// <summary>
    ///     Gets the roles from the current HTTP context.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>The roles array or null if not available.</returns>
    public static string[]? GetRoles(this HttpContext context)
    {
        return context.Items["Roles"] as string[];
    }

    /// <summary>
    ///     Gets the claims from the current HTTP context.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>The claims dictionary or null if not available.</returns>
    public static Dictionary<string, string>? GetClaims(this HttpContext context)
    {
        return context.Items["Claims"] as Dictionary<string, string>;
    }
}
