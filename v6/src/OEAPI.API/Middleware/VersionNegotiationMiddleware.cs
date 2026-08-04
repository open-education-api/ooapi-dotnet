using OEAPI.API.Configuration;
using OEAPI.API.Middleware.VersionNegotiation;
using OEAPI.API.Serialization;

namespace OEAPI.API.Middleware;

/// <summary>
///     Enforces the spec's closed, <c>Accept</c>-header-based OEAPI/consumer version negotiation (see
///     <see cref="VersionNegotiation.VersionNegotiator" /> for the resolution rules, including the
///     lenient default for a request with no version preference at all): rejects an explicit,
///     incompatible request, and - only when the client's <c>Accept</c> header actually named a
///     version (<see cref="VersionNegotiationResult.WasExplicitlyRequested" />) - echoes the resolved
///     version(s) back via <c>Content-Type</c> on a successful, plain-JSON response. A fully generic
///     request is passed through untouched, matching this deployment's own self-generated OpenAPI
///     document.
/// </summary>
/// <remarks>
///     Registered ahead of <see cref="AuthenticationMiddleware" />: version compatibility is a
///     wire-protocol concern prior to identity, so an incompatible request is rejected before an
///     authentication check is even attempted.
///     <br />
///     <c>Content-Type</c> is rewritten via <see cref="HttpResponse.OnStarting(Func{Task})" />, not a
///     direct assignment before calling <see cref="_next" /> - a naive early assignment gets silently
///     overwritten by ASP.NET Core's own content-type selection once the actual response is written
///     (the exact failure mode <see cref="ProblemDetailsOutputFormatter" /> exists to work around for
///     the error-response case; <c>OnStarting</c> runs immediately before headers are flushed, after
///     that selection has already happened, so it can react to - and override - the real, final value).
/// </remarks>
/// <param name="next">The next middleware in the pipeline.</param>
/// <param name="serviceConfig">Supplies the OEAPI/consumer versions this deployment can serve.</param>
public class VersionNegotiationMiddleware(RequestDelegate next, ServiceConfiguration serviceConfig)
{
    private static readonly VersionNegotiator Negotiator = new();
    private readonly RequestDelegate _next = next;
    private readonly ServiceConfiguration _serviceConfig = serviceConfig;

    public async Task InvokeAsync(HttpContext context)
    {
        string acceptHeader = context.Request.Headers.Accept.ToString();
        VersionNegotiationResult result = Negotiator.Negotiate(
            acceptHeader, _serviceConfig.SupportedOeapiVersions, _serviceConfig.SupportedConsumers);

        if (result.IsAccepted)
        {
            context.Response.OnStarting(() =>
            {
                if (result.WasExplicitlyRequested
                    && context.Response.StatusCode is >= 200 and < 300
                    && VersionedContentType.IsPlainJson(context.Response.ContentType))
                    context.Response.ContentType = VersionedContentType.Build(
                        context.Response.ContentType!, result.ResolvedOeapiVersion, result.ResolvedConsumer,
                        result.ResolvedConsumerVersion);
                return Task.CompletedTask;
            });

            await _next(context).ConfigureAwait(false);
            return;
        }

        bool consumerKeyUnknown = result.Consumer != null && result.SupportedVersions.Count == 0;
        string detail = result.Consumer is null
            ? $"The requested OEAPI version '{result.RequestedVersion}' cannot be served."
            : consumerKeyUnknown
                ? $"The consumer '{result.Consumer.ConsumerKey}' is not known to this deployment."
                : $"The requested consumer version '{result.RequestedVersion}' cannot be served.";

        ProblemVersionNotAcceptable problem = new()
        {
            Type = "https://tools.ietf.org/html/rfc9110#section-15.5.7",
            Title = "Version not acceptable",
            Status = StatusCodes.Status406NotAcceptable,
            Detail = detail,
            Consumer = result.Consumer,
            RequestedVersion = result.RequestedVersion,
            SupportedVersions = [.. result.SupportedVersions]
        };

        context.Response.StatusCode = StatusCodes.Status406NotAcceptable;
        // The 2-arg WriteAsJsonAsync overload ignores any Content-Type set beforehand and always
        // writes its own default (application/json) - the contentType parameter is the only way to
        // make it actually write application/problem+json, confirmed empirically (a naive
        // ContentType assignment before this call was silently overwritten).
        await context.Response
            .WriteAsJsonAsync(problem, OeapiJsonSerializerOptions.Instance, "application/problem+json")
            .ConfigureAwait(false);
    }
}
