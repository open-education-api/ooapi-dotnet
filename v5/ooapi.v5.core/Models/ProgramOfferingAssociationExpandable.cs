using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;

[DataContract]
[ExcludeFromCodeCoverage]
public partial class ProgramOfferingAssociationExpandable
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
