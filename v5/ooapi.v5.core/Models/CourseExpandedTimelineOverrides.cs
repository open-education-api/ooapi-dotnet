using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// A timeline override of the course.
    /// </summary>
    [DataContract]
    public partial class CourseExpandedTimelineOverrides
    {
        /// <summary>
        /// The day on which this timelineOverride starts (inclusive), RFC3339 (date)
        /// </summary>
        /// <value>The day on which this timelineOverride starts (inclusive), RFC3339 (date)</value>
        [JsonRequired]

        [DataMember(Name = "validFrom")]
        public DateTime? ValidFrom { get; set; }

        /// <summary>
        /// The day on which this timelineOverride ends (exclusive), RFC3339 (date)
        /// </summary>
        /// <value>The day on which this timelineOverride ends (exclusive), RFC3339 (date)</value>

        [DataMember(Name = "validTo")]
        public DateTime? ValidTo { get; set; }

        /// <summary>
        /// Gets or Sets Course
        /// </summary>
        [JsonRequired]

        [DataMember(Name = "course")]
        public Course Course { get; set; }
    }

}
