using Newtonsoft.Json;
using ooapi.v5.Helpers;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;

/// <summary>
/// A time-line override of the program.
/// </summary>
[DataContract]
public partial class TimelineOverrideProgram
{
    /// <summary>
    /// The day on which this timelineOverride starts (inclusive), RFC3339 (date)
    /// </summary>
    /// <value>The day on which this timelineOverride starts (inclusive), RFC3339 (date)</value>
    [JsonRequired]
    [JsonProperty(PropertyName = "validFrom")]
    [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
    public DateTime? ValidFrom { get; set; }

    /// <summary>
    /// The day on which this timelineOverride ends (exclusive), RFC3339 (date)
    /// </summary>
    /// <value>The day on which this timelineOverride ends (exclusive), RFC3339 (date)</value>
    [JsonProperty(PropertyName = "validTo")]
    [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
    public DateTime? ValidTo { get; set; }

    /// <summary>
    /// Gets or Sets Program
    /// </summary>
    [JsonRequired]
    [JsonProperty(PropertyName = "program")]
    public Program Program { get; set; } = default!;
}
