using Microsoft.Net.Http.Headers;

namespace OEAPI.API.Middleware.VersionNegotiation;

/// <summary>
///     Builds the negotiated-version <c>Content-Type</c> (<c>application/vnd.oeapi+json;version=X.Y
///     [;consumer=...;consumer-version=...]</c>) for a successful response - separated from
///     <see cref="VersionNegotiationMiddleware" /> so the string-building logic is testable without an
///     <c>HttpContext</c>.
/// </summary>
/// <remarks>
///     Deliberately scoped to plain <c>application/json</c> responses only - never
///     <c>application/problem+json</c> (error responses keep that content type regardless of version
///     negotiation, per the spec's own error-format rules) and never a non-JSON content type (e.g. the
///     binary <c>application/octet-stream</c> download from <c>GET /documents/{documentId}</c>, which
///     the versioned media type was never meant to apply to).
/// </remarks>
public static class VersionedContentType
{
    private const string OeapiMediaType = "application/vnd.oeapi+json";

    /// <summary>
    ///     <see langword="true" /> when <paramref name="contentType" />'s media type is exactly
    ///     <c>application/json</c> (parameters like <c>charset</c> ignored for this check) - the only
    ///     shape this class ever rewrites.
    /// </summary>
    public static bool IsPlainJson(string? contentType)
    {
        return contentType != null
               && MediaTypeHeaderValue.TryParse(contentType, out MediaTypeHeaderValue? parsed)
               && string.Equals(parsed.MediaType.ToString(), "application/json", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    ///     Rewrites <paramref name="originalContentType" />'s media type to
    ///     <c>application/vnd.oeapi+json</c>, adding <c>version</c> (and <c>consumer</c>/
    ///     <c>consumer-version</c>, when a consumer was resolved) as media-type parameters. Preserves
    ///     <c>charset</c> from the original, if present, rather than silently dropping it.
    /// </summary>
    public static string Build(string originalContentType, string oeapiVersion, string? consumer,
        string? consumerVersion)
    {
        MediaTypeHeaderValue original = MediaTypeHeaderValue.Parse(originalContentType);
        MediaTypeHeaderValue versioned = new(OeapiMediaType);
        versioned.Parameters.Add(new NameValueHeaderValue("version", oeapiVersion));

        if (consumer != null)
        {
            versioned.Parameters.Add(new NameValueHeaderValue("consumer", consumer));
            versioned.Parameters.Add(new NameValueHeaderValue("consumer-version", consumerVersion ?? string.Empty));
        }

        if (original.Charset.HasValue)
            versioned.Parameters.Add(new NameValueHeaderValue("charset", original.Charset.Value));

        return versioned.ToString();
    }
}
