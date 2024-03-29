using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;

[DataContract]
[ExcludeFromCodeCoverage]
public partial class ProgramExpanded
{
    /// <summary>
    /// Timeline overrides allow an implementation to provide versions of entities that will be valid in the future or have been in the past.
    /// </summary>
    /// <value>Timeline overrides allow an implementation to provide versions of entities that will be valid in the future or have been in the past.</value>

    [JsonProperty(PropertyName = "timelineOverrides")]
    public List<ProgramExpandedTimelineOverrides> TimelineOverrides { get; set; } = default!;
}
