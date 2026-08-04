using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Validation;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     The amount of effort to complete this education in the specified unit.
/// </summary>
public class StudyLoadDescriptor
{
    /// <summary>
    ///     The unit in which the study load is specified.
    /// </summary>
    [JsonPropertyName("studyLoadUnit")]
    [ExtensibleEnum("studyLoadUnit")]
    public string StudyLoadUnit { get; set; } = string.Empty;

    /// <summary>
    ///     The amount of load depicted in numbers.
    /// </summary>
    [JsonPropertyName("value")]
    public double Value { get; set; }
}
