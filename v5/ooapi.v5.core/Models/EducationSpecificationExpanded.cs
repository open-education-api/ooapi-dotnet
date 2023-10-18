using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;


[DataContract]
public partial class EducationSpecificationExpanded
{
    /// <summary>
    /// Timeline overrides allow an implementation to provide versions of entities that will be valid in the future or have been in the past.
    /// </summary>
    /// <value>Timeline overrides allow an implementation to provide versions of entities that will be valid in the future or have been in the past.</value>
    [JsonProperty(PropertyName = "timelineOverrides")]
    public List<EducationSpecificationExpandedTimelineOverrides> TimelineOverrides { get; set; } = default!;
}
