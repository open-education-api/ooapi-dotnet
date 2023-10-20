using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;

[DataContract]
[ExcludeFromCodeCoverage]
public partial class ProgramOfferingAssociation
{
    /// <summary>
    /// Gets or Sets Result
    /// </summary>

    [JsonProperty(PropertyName = "result")]
    public object Result { get; set; } = default!;
}
