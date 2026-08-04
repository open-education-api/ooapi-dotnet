using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace OEAPI.API.Middleware.VersionNegotiation;

/// <summary>
///     The spec's <c>ProblemVersionNotAcceptable</c> response body for a <c>406</c> version-negotiation
///     rejection - <c>ProblemDetails</c> (<c>type</c>/<c>title</c>/<c>status</c>/<c>detail</c>) plus the
///     version-negotiation-specific fields.
/// </summary>
public class ProblemVersionNotAcceptable : ProblemDetails
{
    /// <summary>
    ///     Identifies which party caused the mismatch: <see langword="null" /> when the OEAPI version
    ///     itself was unsupported, populated with the mismatched consumer when a consumer/consumer-version
    ///     tuple was the problem.
    /// </summary>
    [JsonPropertyName("consumer")]
    public ConsumerReference? Consumer { get; set; }

    /// <summary>The version requested by the client.</summary>
    [JsonPropertyName("requestedVersion")]
    public string RequestedVersion { get; set; } = string.Empty;

    /// <summary>Versions the server can serve, typically in descending order.</summary>
    [JsonPropertyName("supportedVersions")]
    public string[] SupportedVersions { get; set; } = [];
}

/// <summary>
///     Minimal identification of a consumer for <see cref="ProblemVersionNotAcceptable.Consumer" /> -
///     just the key, matching the spec's <c>Consumer</c> schema's only required field. Not the full
///     consumer-scoped data shape used elsewhere in this codebase, since a version-negotiation failure
///     has no consumer-specific business data to report.
/// </summary>
public class ConsumerReference
{
    [JsonPropertyName("consumerKey")]
    public string ConsumerKey { get; set; } = string.Empty;
}
