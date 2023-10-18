using Newtonsoft.Json;

namespace ooapi.v5.Models;

/// <summary>
/// 
/// </summary>
public class Attribute : LanguageTypedProperty
{
    /// <summary>
    /// The id
    /// </summary>
    [JsonIgnore]
    public Guid Id { get; set; }

    /// <summary>
    /// The model type name
    /// </summary>
    [JsonIgnore]
    public string ModelTypeName { get; set; } = default!;
}
