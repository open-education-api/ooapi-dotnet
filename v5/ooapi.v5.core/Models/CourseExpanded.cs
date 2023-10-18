using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;

/// <summary>
/// 
/// </summary>
[DataContract]
public partial class CourseExpanded
{
    /// <summary>
    /// Timeline overrides allow an implementation to provide versions of entities that will be valid in the future or have been in the past.
    /// </summary>
    /// <value>Timeline overrides allow an implementation to provide versions of entities that will be valid in the future or have been in the past.</value>

    [JsonProperty(PropertyName = "timelineOverrides")]
    public List<CourseExpandedTimelineOverrides> TimelineOverrides { get; set; } = default!;
}
