using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     An assigned resource or time allowance based on the needs of a person, describing which needs
///     the person requires under which conditions (e.g. 15% extra time for tests requiring maths skills).
/// </summary>
public class PersonalNeed
{
    /// <summary>
    ///     Human readable value for the code/identifier of this assigned need.
    /// </summary>
    /// <example>ExtraTimeOnlyMaths25%</example>
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    /// <summary>
    ///     The description of this assigned need.
    /// </summary>
    [JsonPropertyName("description")]
    public LanguageTypedString[]? Description { get; set; }

    /// <summary>
    ///     The moment on which this assigned need starts.
    /// </summary>
    [JsonPropertyName("startDateTime")]
    public string? StartDateTime { get; set; }

    /// <summary>
    ///     The moment on which this assigned need ends.
    /// </summary>
    [JsonPropertyName("endDateTime")]
    public string? EndDateTime { get; set; }
}
