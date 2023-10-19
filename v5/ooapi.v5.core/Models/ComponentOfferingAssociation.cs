using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;

[DataContract]
public partial class ComponentOfferingAssociation
{
    /// <summary>
    /// Gets or Sets Result
    /// </summary>
    [JsonProperty(PropertyName = "result")]
    public object Result { get; set; } = default!;
}
