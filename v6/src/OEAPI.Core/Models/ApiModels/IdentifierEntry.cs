using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Validation;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     An identifier entry containing a code type and code value.
/// </summary>
public class IdentifierEntry
{
    /// <summary>
    ///     The type of code or identifier.
    /// </summary>
    /// <example>studentNumber</example>
    [JsonPropertyName("codeType")]
    [ExtensibleEnum("codeType")]
    [StringLength(256)]
    public string CodeType { get; set; } = string.Empty;

    /// <summary>
    ///     Human readable value for the code/identifier.
    /// </summary>
    /// <example>1234qwe12</example>
    [JsonPropertyName("code")]
    [StringLength(256)]
    public string Code { get; set; } = string.Empty;
}
