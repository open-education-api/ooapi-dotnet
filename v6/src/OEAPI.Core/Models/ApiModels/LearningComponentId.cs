using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     Learning component identifier.
/// </summary>
public class LearningComponentId
{
    /// <summary>
    ///     Unique id for this learning component.
    /// </summary>
    /// <example>123e4567-e89b-12d3-a456-426614174000</example>
    [JsonPropertyName("componentId")]
    public string ComponentIdValue { get; set; } = string.Empty;
}
