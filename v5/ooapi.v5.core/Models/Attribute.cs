using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace ooapi.v5.Models;

[ExcludeFromCodeCoverage(Justification = "Get/Set")]
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