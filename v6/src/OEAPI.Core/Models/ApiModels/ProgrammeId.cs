using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     A collection of courses that lead to a certifiable learning outcome.
/// </summary>
public class ProgrammeId
{
    /// <summary>
    ///     Unique id for this programme.
    /// </summary>
    /// <example>123e4567-e89b-12d3-a456-426614174000</example>
    [JsonPropertyName("programmeId")]
    public string ProgrammeIdValue { get; set; } = string.Empty;
}
