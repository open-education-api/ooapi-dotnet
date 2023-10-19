using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;

[DataContract]
public partial class ComponentOfferingAssociationExpandable
{
    /// <summary>
    /// Gets or Sets Result
    /// </summary>
    [JsonProperty(PropertyName = "result")]
    public object Result { get; set; } = default!;

    /// <summary>
    /// Gets or Sets Person
    /// </summary>
    [JsonProperty(PropertyName = "person")]
    public ProgramOfferingAssociationExpandablePerson Person { get; set; } = default!;
}
