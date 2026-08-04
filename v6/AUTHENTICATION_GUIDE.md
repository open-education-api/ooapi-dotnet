# Authentication Implementation Guide

This is a supplement to the [README](README.md#authentication) for institutions implementing one of
the three authentication provider interfaces. See the README first for the provider table,
registration pattern, and `RequireAuthentication` behaviour - this guide covers implementation
detail the README doesn't.

## Implementing JWT authentication

Extend `BaseJwtAuthenticationService` (`OEAPI.API.Services`) and implement token extraction and
validation:

```csharp
// src/OEAPI.API/Services/MyJwtAuthenticationService.cs
using Microsoft.AspNetCore.Http;
using OEAPI.API.Configuration;
using OEAPI.API.Services;
using OEAPI.Core.Interfaces;

public class MyJwtAuthenticationService(JwtConfiguration jwtConfig, IHttpContextAccessor httpContextAccessor)
    : BaseJwtAuthenticationService(jwtConfig)
{
    protected override string? GetTokenFromRequest()
    {
        string? authHeader = httpContextAccessor.HttpContext?.Request.Headers.Authorization.FirstOrDefault();
        if (string.IsNullOrEmpty(authHeader))
            return null;
        return authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
            ? authHeader["Bearer ".Length..].Trim()
            : authHeader;
    }

    protected override async Task<AuthenticationResult> ValidateTokenInternalAsync(string token)
    {
        // Validate with Microsoft.IdentityModel.Tokens.Jwt or any other JWT library.
        var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        var validationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = JwtConfig.ValidateIssuer,
            ValidateAudience = JwtConfig.ValidateAudience,
            ValidateLifetime = JwtConfig.ValidateLifetime,
            ValidateIssuerSigningKey = JwtConfig.ValidateSignature,
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(JwtConfig.SecretKey!)),
            ValidIssuer = JwtConfig.Issuer,
            ValidAudience = JwtConfig.Audience,
            ClockSkew = TimeSpan.FromSeconds(JwtConfig.ClockSkewSeconds)
        };

        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
            return new AuthenticationResult
            {
                IsAuthenticated = true,
                UserId = principal.FindFirstValue(ClaimTypes.NameIdentifier),
                UserName = principal.FindFirstValue(ClaimTypes.Name),
                InstitutionId = principal.FindFirstValue("institution_id"),
                ConsumerKey = principal.FindFirstValue("consumer_key"),
                Roles = principal.FindAll(ClaimTypes.Role).Select(c => c.Value).ToArray(),
                Claims = principal.Claims.ToDictionary(c => c.Type, c => c.Value)
            };
        }
        catch (Exception ex)
        {
            return new AuthenticationResult { IsAuthenticated = false, ErrorMessage = ex.Message };
        }
    }
}
```

## Implementing API key authentication

Extend `BaseApiKeyAuthenticationService` the same way, implementing `GetAuthorizationHeader`/
`GetHeaderValue` (where to read the key from) and `LookupApiKeyAsync` (how to resolve it to a
user/institution).

## Reading the result in a controller

```csharp
[HttpGet]
public IActionResult Get()
{
    if (!HttpContext.IsAuthenticated())
        return Unauthorized();

    string? consumerKey = HttpContext.GetConsumerKey();
    return Ok(new { UserId = HttpContext.GetUserId(), ConsumerKey = consumerKey });
}

[AllowAnonymous]
[HttpGet("public")]
public IActionResult GetPublic() => Ok("Public data");
```

`HttpContext` extension methods (`OEAPI.API.Middleware.AuthenticationMiddlewareExtensions`):
`IsAuthenticated()`, `GetAuthenticationResult()`, `GetUserId()`, `GetUserName()`,
`GetInstitutionId()`, `GetConsumerKey()`, `GetRoles()`, `GetClaims()`.

A common pattern: fall back to the authenticated caller's consumer key when the `consumer` query
parameter isn't supplied explicitly.

```csharp
if (string.IsNullOrEmpty(consumer) && HttpContext.IsAuthenticated())
    consumer = HttpContext.GetConsumerKey();
```

## `appsettings.json` reference

```json
{
  "Authentication": {
    "Providers": { "JwtEnabled": true, "ApiKeyEnabled": false, "CustomEnabled": false },
    "RequireAuthentication": false,
    "Jwt": {
      "Issuer": "YourInstitution",
      "Audience": "YourClientApp",
      "SecretKey": "your-very-secure-secret-key",
      "ValidateSignature": true,
      "ValidateIssuer": true,
      "ValidateAudience": true,
      "ValidateLifetime": true,
      "ClockSkewSeconds": 30
    },
    "ApiKey": {
      "HeaderName": "X-API-Key",
      "UseAuthorizationHeader": false,
      "EnableCaching": true,
      "CacheDurationMinutes": 60
    }
  }
}
```

## Security

- Use HTTPS in production.
- Never commit secrets - use environment variables or a secrets manager for `SecretKey` and API
  keys.
- Use a strong, random `SecretKey` (32+ characters).
- Validate every JWT claim your library supports (issuer, audience, signature, expiration).
