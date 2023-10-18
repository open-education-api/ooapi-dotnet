using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ooapi.v5.Models;

/// <summary>
/// A String with an associated language code.
/// </summary>
public class LanguageTypedString
{
    /// <summary>
    /// The language used in the described entity. A string formatted according to RFC3066.
    /// </summary>
    /// <value>The language used in the described entity. A string formatted according to RFC3066.</value>
    [RegularExpression("/^[a-z]{2,4}(-[A-Z][a-z]{3})?(-([A-Z]{2}|[0-9]{3}))?$/")]
    [JsonProperty(PropertyName = "language")]
    public string Language { get; set; }

    /// <summary>
    /// String to describe the entity.
    /// </summary>
    /// <value>String to describe the entity.</value>
    [JsonProperty(PropertyName = "value")]
    public string Value { get; set; }
}
