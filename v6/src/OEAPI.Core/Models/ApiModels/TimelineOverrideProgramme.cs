using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     A single historical or future alternate snapshot of a programme, returned inside
///     <see cref="Programme.TimelineOverrides" /> when the client requests
///     <c>returnTimelineOverrides=true</c>.
/// </summary>
public class TimelineOverrideProgramme
{
    /// <summary>
    ///     The date and time from which this override is valid.
    /// </summary>
    [JsonPropertyName("validFrom")]
    public string ValidFrom { get; set; } = string.Empty;

    /// <summary>
    ///     The date and time until which this override is valid. If not present, the override is valid indefinitely.
    /// </summary>
    [JsonPropertyName("validTo")]
    public string? ValidTo { get; set; }

    /// <summary>
    ///     The alternate snapshot of the programme's own properties for this validity window.
    /// </summary>
    [JsonPropertyName("programme")]
    public ProgrammeProperties Programme { get; set; } = new();
}
