using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Validation;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     A supplementary content item associated with a resource. <c>type</c> defines the technical media
///     form (e.g. text, image, video, http) and <c>role</c> defines the semantic intent (e.g. badge,
///     marketing); the two are independent of each other.
/// </summary>
public class SupplementaryInformation
{
    /// <summary>
    ///     The semantic purpose of this item (e.g. announcement, badge, marketing, promo).
    /// </summary>
    [JsonPropertyName("role")]
    [ExtensibleEnum("supplementaryRole")]
    public string Role { get; set; } = string.Empty;

    /// <summary>
    ///     The technical media form of this item (e.g. image, text_http, text_md, text_plain, uri, video).
    /// </summary>
    [JsonPropertyName("type")]
    [ExtensibleEnum("supplementaryType")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    ///     The content of this item, allowing the same item to be expressed in multiple languages or
    ///     alternative textual variants.
    /// </summary>
    [JsonPropertyName("value")]
    public LanguageTypedString[] Value { get; set; } = [];
}
