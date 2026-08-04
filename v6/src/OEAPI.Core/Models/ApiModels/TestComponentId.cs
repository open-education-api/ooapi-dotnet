using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     Test component identifier.
/// </summary>
public class TestComponentId
{
    /// <summary>
    ///     Unique id for this test component.
    /// </summary>
    /// <example>123e4567-e89b-12d3-a456-426614174000</example>
    [JsonPropertyName("componentId")]
    public string ComponentIdValue { get; set; } = string.Empty;

    /// <summary>
    ///     Unique id for this test component (API property name).
    /// </summary>
    [JsonIgnore]
    public string ComponentId => ComponentIdValue;
}
