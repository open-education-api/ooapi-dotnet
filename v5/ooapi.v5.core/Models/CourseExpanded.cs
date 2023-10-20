using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;

[DataContract]
public partial class CourseExpanded
{
    /// <summary>
    /// Timeline overrides allow an implementation to provide versions of entities that will be valid in the future or have been in the past.
    /// </summary>
    /// <value>Timeline overrides allow an implementation to provide versions of entities that will be valid in the future or have been in the past.</value>
    [ExcludeFromCodeCoverage(Justification = "Get/Set")]
    [JsonProperty(PropertyName = "timelineOverrides")]
    public List<CourseExpandedTimelineOverrides> TimelineOverrides { get; set; } = default!;
}