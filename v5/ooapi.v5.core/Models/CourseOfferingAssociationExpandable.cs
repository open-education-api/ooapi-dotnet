using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;

/// <summary>
/// 
/// </summary>
[DataContract]
public partial class CourseOfferingAssociationExpandable
{
    /// <summary>
    /// Gets or Sets Result
    /// </summary>
    [JsonProperty(PropertyName = "result")]
    public object Result { get; set; }

    /// <summary>
    /// Gets or Sets Person
    /// </summary>
    [JsonProperty(PropertyName = "person")]
    public ProgramOfferingAssociationExpandablePerson Person { get; set; }
}
