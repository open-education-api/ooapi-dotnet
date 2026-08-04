namespace OEAPI.API.Middleware.VersionNegotiation;

/// <summary>
///     The outcome of <see cref="VersionNegotiator.Negotiate" />: either the resolved version(s) to
///     serve, or the data needed to build a <see cref="ProblemVersionNotAcceptable" /> rejection.
/// </summary>
public sealed class VersionNegotiationResult
{
    public bool IsAccepted { get; private init; }

    /// <summary>
    ///     <see langword="true" /> when the client's <c>Accept</c> header actually named an OEAPI
    ///     version (whether or not it round-tripped to that exact value, e.g. after a minor-version
    ///     fallback) - <see langword="false" /> when this result came from the lenient default (no
    ///     <c>application/vnd.oeapi+json</c> version parameter present at all). Only meaningful when
    ///     <see cref="IsAccepted" />: <see cref="VersionNegotiationMiddleware" /> only echoes the
    ///     resolved version via <c>Content-Type</c> when this is <see langword="true" /> - a fully
    ///     generic request keeps getting plain <c>application/json</c>, matching what this
    ///     deployment's self-generated OpenAPI document declares.
    /// </summary>
    public bool WasExplicitlyRequested { get; private init; }

    /// <summary>The OEAPI version to actually serve. Only meaningful when <see cref="IsAccepted" />.</summary>
    public string ResolvedOeapiVersion { get; private init; } = string.Empty;

    /// <summary>The consumer key resolved, if one was requested and matched.</summary>
    public string? ResolvedConsumer { get; private init; }

    /// <summary>The consumer version resolved, if a consumer was requested and matched.</summary>
    public string? ResolvedConsumerVersion { get; private init; }

    /// <summary>The version the client requested that couldn't be satisfied. Only set on rejection.</summary>
    public string RequestedVersion { get; private init; } = string.Empty;

    /// <summary>Versions the server can actually serve. Only set on rejection.</summary>
    public IReadOnlyList<string> SupportedVersions { get; private init; } = [];

    /// <summary>
    ///     Identifies which party caused the rejection: <see langword="null" /> when the OEAPI version
    ///     itself was the problem, populated when a consumer/consumer-version was. Only set on rejection.
    /// </summary>
    public ConsumerReference? Consumer { get; private init; }

    public static VersionNegotiationResult Accepted(string resolvedOeapiVersion, string? resolvedConsumer,
        string? resolvedConsumerVersion, bool wasExplicitlyRequested)
    {
        return new VersionNegotiationResult
        {
            IsAccepted = true,
            WasExplicitlyRequested = wasExplicitlyRequested,
            ResolvedOeapiVersion = resolvedOeapiVersion,
            ResolvedConsumer = resolvedConsumer,
            ResolvedConsumerVersion = resolvedConsumerVersion
        };
    }

    public static VersionNegotiationResult Rejected(string requestedVersion, IReadOnlyList<string> supportedVersions,
        ConsumerReference? consumer)
    {
        return new VersionNegotiationResult
        {
            IsAccepted = false,
            RequestedVersion = requestedVersion,
            SupportedVersions = supportedVersions,
            Consumer = consumer
        };
    }
}
