using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;

[ExcludeFromCodeCoverage(Justification = "Get/Set")]
[DataContract]
public partial class CourseOfferingAssociation
{
    /// <summary>
    /// Gets or Sets Result
    /// </summary>
    [JsonProperty(PropertyName = "result")]
    public object Result { get; set; } = default!;
}