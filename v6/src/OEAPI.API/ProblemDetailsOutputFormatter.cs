using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace OEAPI.API;

/// <summary>
///     Writes every <see cref="ProblemDetails" />-typed response body as <c>application/problem+json</c>.
/// </summary>
/// <remarks>
///     ASP.NET Core MVC controllers have no purely-declarative mechanism to produce this content
///     type - a real, documented gap between Minimal APIs (whose <c>TypedResults.Problem()</c>
///     returns a dedicated <c>ProblemHttpResult</c> that handles this correctly) and traditional MVC
///     controllers, which have no equivalent.
///
///     A type-specific output formatter, registered ahead of the general-purpose JSON formatter, is
///     the correct mechanism for this: MVC's default (no-<c>Accept</c>-preference) selection works by
///     picking the first formatter whose <see cref="CanWriteType" /> returns <see langword="true" />
///     for the response's CLR type - a formatter here that only ever claims
///     <see cref="ProblemDetails" /> wins for exactly the responses that need it, without touching how
///     any other response type is formatted.
/// </remarks>
public sealed class ProblemDetailsOutputFormatter : SystemTextJsonOutputFormatter
{
    public ProblemDetailsOutputFormatter(JsonSerializerOptions jsonSerializerOptions)
        : base(jsonSerializerOptions)
    {
        SupportedMediaTypes.Clear();
        SupportedMediaTypes.Add("application/problem+json");
    }

    protected override bool CanWriteType(Type? type)
    {
        return type != null && typeof(ProblemDetails).IsAssignableFrom(type);
    }
}
