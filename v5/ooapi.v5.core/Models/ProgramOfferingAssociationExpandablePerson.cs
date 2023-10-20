using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;

[DataContract]
[ExcludeFromCodeCoverage]
public partial class ProgramOfferingAssociationExpandablePerson
{
    /// <summary>
    /// Unique id of this person
    /// </summary>
    /// <value>Unique id of this person</value>
    [JsonRequired]
    [JsonProperty(PropertyName = "personId")]
    public Guid PersonId { get; set; }
}
