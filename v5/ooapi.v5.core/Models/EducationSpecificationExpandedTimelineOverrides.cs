using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using ooapi.v5.Helpers;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;

/// <summary>
/// A timeline override of the EducationSpecification.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Get/Set")]
[DataContract]
public partial class EducationSpecificationExpandedTimelineOverrides
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
    /// Gets or Sets EducationSpecification
    /// </summary>
    [JsonRequired]

    [JsonProperty(PropertyName = "educationSpecification")]
    public EducationSpecification EducationSpecification { get; set; } = default!;
}