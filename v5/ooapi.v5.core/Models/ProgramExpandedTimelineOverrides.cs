using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// A time-line override of the program.
    /// </summary>
    [DataContract]
    public partial class ProgramExpandedTimelineOverrides
    {
        /// <summary>
        /// The day on which this timelineOverride starts (inclusive), RFC3339 (date)
        /// </summary>
        /// <value>The day on which this timelineOverride starts (inclusive), RFC3339 (date)</value>
        [JsonRequired]

        [JsonProperty(PropertyName = "validFrom")]
        public DateTime? ValidFrom { get; set; }

        /// <summary>
        /// The day on which this timelineOverride ends (exclusive), RFC3339 (date)
        /// </summary>
        /// <value>The day on which this timelineOverride ends (exclusive), RFC3339 (date)</value>

        [JsonProperty(PropertyName = "validTo")]
        public DateTime? ValidTo { get; set; }

        /// <summary>
        /// Gets or Sets Program
        /// </summary>
        [JsonRequired]

        [JsonProperty(PropertyName = "program")]
        public Program Program { get; set; }
    }

}
