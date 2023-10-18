using Newtonsoft.Json;

namespace ooapi.v5.Models;

/// <summary>
/// 
/// </summary>
public class Attribute : LanguageTypedProperty
{
    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public Guid Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public string ModelTypeName { get; set; } = default!;
}
