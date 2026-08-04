using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     Learning outcome identifier.
/// </summary>
public class LearningOutcomeId
{
    /// <summary>
    ///     Unique id for this learning outcome.
    /// </summary>
    /// <example>123e4567-e89b-12d3-a456-426614174000</example>
    [JsonPropertyName("learningOutcomeId")]
    public string LearningOutcomeIdValue { get; set; } = string.Empty;
}
