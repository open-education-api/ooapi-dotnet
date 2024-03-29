using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;

/// <summary>
/// The amount of effort to complete this education in the specified unit.
/// </summary>
[DataContract]
[ExcludeFromCodeCoverage]
public partial class StudyLoadDescriptor
{
    /// <summary>
    /// The unit in which the studyload is specfied - contacttime: CONTACTUUR amount of time spent in classes - ects: ECTS_PUNT European Credit Transfer System - sbu: SBU studentloadhours - sp: STUDIEPUNT studentpoints - hour: UUR hours 
    /// </summary>
    /// <value>The unit in which the studyload is specfied - contacttime: CONTACTUUR amount of time spent in classes - ects: ECTS_PUNT European Credit Transfer System - sbu: SBU studentloadhours - sp: STUDIEPUNT studentpoints - hour: UUR hours </value>

    [JsonProperty(PropertyName = "studyLoadUnit")]
    public string? StudyLoadUnit { get; set; }

    /// <summary>
    /// The amount of load depicted in numbers
    /// </summary>
    /// <value>The amount of load depicted in numbers</value>

    [JsonProperty(PropertyName = "value")]
    public int? Value { get; set; }
}
