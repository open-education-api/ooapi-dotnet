using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Validation;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     A string with an associated language code. If this object is used, both fields are mandatory.
/// </summary>
public class LanguageTypedString
{
    /// <summary>
    ///     The language used in the described entity. A string formatted according to RFC 4647.
    /// </summary>
    /// <example>en-GB</example>
    [JsonPropertyName("language")]
    [RegexPattern("language")]
    public string Language { get; set; } = string.Empty;

    /// <summary>
    ///     String to describe the entity.
    /// </summary>
    /// <example>programme that is a place holder for all courses that are made available for student mobility</example>
    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
}
