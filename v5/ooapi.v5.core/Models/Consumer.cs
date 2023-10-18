using Newtonsoft.Json;
using ooapi.v5.Enums;

namespace ooapi.v5.Models;

/// <summary>
/// 
/// </summary>
public class Consumer
{
    [JsonIgnore]
    public Guid Id { get; set; }

    [JsonIgnore]
    public string ModelTypeName { get; set; } = default!;

    [JsonRequired]
    [JsonProperty(PropertyName = "consumerKey")]
    public string ConsumerKey { get; set; } = default!;

    [JsonIgnore]
    public ConsumerPropertyType PropertyType { get; set; } = 0;

    [JsonProperty(PropertyName = "propertyName")]
    public string PropertyName { get; set; } = default!;

    [JsonProperty(PropertyName = "propertyValue")]
    public string PropertyValue { get; set; } = default!;
}
