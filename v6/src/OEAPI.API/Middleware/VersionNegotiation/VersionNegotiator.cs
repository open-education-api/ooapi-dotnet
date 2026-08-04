using Microsoft.Net.Http.Headers;
using OEAPI.Core.Models.ApiModels;

namespace OEAPI.API.Middleware.VersionNegotiation;

/// <summary>
///     Pure resolution logic for the spec's closed, <c>Accept</c>-header-based OEAPI/consumer version
///     negotiation - deliberately separated from <see cref="VersionNegotiationMiddleware" /> so it's
///     testable directly, with no <c>HttpContext</c> involved.
/// </summary>
/// <remarks>
///     <b>Lenient default</b>: an <c>Accept</c> header with no <c>application/vnd.oeapi+json</c> media
///     type at all (missing header, or one present but without an OEAPI version parameter) is treated
///     as "no preference stated" - the caller's default version is served, never rejected. Only an
///     <c>Accept</c> that <i>does</i> name a <c>version</c> is resolved strictly, so a typo'd/malformed
///     value still gets a real rejection rather than silently falling back.
///     <br />
///     <b>Resolution rule</b> (both OEAPI and consumer versions): the requested major version must
///     match a known major version exactly; any higher-or-lower minor version within that major is an
///     acceptable fallback. Major-version fallback is never permitted.
/// </remarks>
public sealed class VersionNegotiator
{
    private const string OeapiMediaType = "application/vnd.oeapi+json";

    public VersionNegotiationResult Negotiate(
        string? acceptHeader,
        IReadOnlyList<string> supportedOeapiVersions,
        IReadOnlyList<SupportedConsumer> supportedConsumers)
    {
        string defaultOeapiVersion = supportedOeapiVersions.Count > 0 ? supportedOeapiVersions[0] : string.Empty;

        RequestedVersionParameters? requested = ParseAccept(acceptHeader);
        if (requested is null || requested.OeapiVersion is null)
            // Lenient default: no version parameter present at all (or no Accept header) - never
            // rejected, and (wasExplicitlyRequested: false) never echoed back via Content-Type either
            // - see VersionNegotiationResult.WasExplicitlyRequested for why.
            return VersionNegotiationResult.Accepted(defaultOeapiVersion, null, null, wasExplicitlyRequested: false);

        string? resolvedOeapiVersion = ResolveCompatibleVersion(requested.OeapiVersion, supportedOeapiVersions);
        if (resolvedOeapiVersion is null)
            return VersionNegotiationResult.Rejected(requested.OeapiVersion, supportedOeapiVersions, consumer: null);

        if (requested.Consumer is null)
            return VersionNegotiationResult.Accepted(resolvedOeapiVersion, null, null, wasExplicitlyRequested: true);

        SupportedConsumer? matchedConsumer = supportedConsumers.FirstOrDefault(c =>
            string.Equals(c.ConsumerKey, requested.Consumer, StringComparison.OrdinalIgnoreCase));
        ConsumerReference consumerReference = new() { ConsumerKey = requested.Consumer };

        if (matchedConsumer is null)
            // Unknown consumer key entirely - nothing to list as a supported version for it.
            return VersionNegotiationResult.Rejected(
                requested.ConsumerVersion ?? string.Empty, [], consumerReference);

        if (requested.ConsumerVersion is null)
            return VersionNegotiationResult.Accepted(
                resolvedOeapiVersion, requested.Consumer, matchedConsumer.Version, wasExplicitlyRequested: true);

        string? resolvedConsumerVersion = ResolveCompatibleVersion(requested.ConsumerVersion, [matchedConsumer.Version]);
        return resolvedConsumerVersion is null
            ? VersionNegotiationResult.Rejected(requested.ConsumerVersion, [matchedConsumer.Version], consumerReference)
            : VersionNegotiationResult.Accepted(
                resolvedOeapiVersion, requested.Consumer, resolvedConsumerVersion, wasExplicitlyRequested: true);
    }

    /// <summary>
    ///     Finds the media-type entry in <paramref name="acceptHeader" /> whose media type is
    ///     <c>application/vnd.oeapi+json</c> (there SHALL only ever be one, per the spec's closed,
    ///     single-choice model - the first one found is used if a client sends more than one) and reads
    ///     its <c>version</c>/<c>consumer</c>/<c>consumer-version</c> parameters. Returns
    ///     <see langword="null" /> if no such media type is present at all.
    /// </summary>
    private static RequestedVersionParameters? ParseAccept(string? acceptHeader)
    {
        if (string.IsNullOrWhiteSpace(acceptHeader))
            return null;

        if (!MediaTypeHeaderValue.TryParseList([acceptHeader], out IList<MediaTypeHeaderValue>? mediaTypes))
            return null;

        MediaTypeHeaderValue? oeapiMediaType = mediaTypes.FirstOrDefault(m =>
            string.Equals(m.MediaType.ToString(), OeapiMediaType, StringComparison.OrdinalIgnoreCase));
        if (oeapiMediaType is null)
            return null;

        return new RequestedVersionParameters(
            GetParameter(oeapiMediaType, "version"),
            GetParameter(oeapiMediaType, "consumer"),
            GetParameter(oeapiMediaType, "consumer-version"));
    }

    private static string? GetParameter(MediaTypeHeaderValue mediaType, string name)
    {
        return mediaType.Parameters
            .FirstOrDefault(p => string.Equals(p.Name.ToString(), name, StringComparison.OrdinalIgnoreCase))
            ?.Value.ToString().Trim('"');
    }

    /// <summary>
    ///     Finds the first entry in <paramref name="supported" /> whose major version matches
    ///     <paramref name="requested" />'s - any minor version within that major is an acceptable
    ///     fallback, in either direction. Returns <see langword="null" /> if <paramref name="requested" />
    ///     doesn't parse as <c>MAJOR.MINOR</c>, or no supported entry shares its major version.
    /// </summary>
    private static string? ResolveCompatibleVersion(string requested, IReadOnlyList<string> supported)
    {
        if (!TryParseMajorMinor(requested, out int requestedMajor))
            return null;

        return supported.FirstOrDefault(candidate =>
            TryParseMajorMinor(candidate, out int candidateMajor) && candidateMajor == requestedMajor);
    }

    private static bool TryParseMajorMinor(string version, out int major)
    {
        major = 0;
        string[] parts = version.Split('.');
        return parts.Length == 2 && int.TryParse(parts[0], out major) && int.TryParse(parts[1], out _);
    }

    private sealed record RequestedVersionParameters(string? OeapiVersion, string? Consumer, string? ConsumerVersion);
}
