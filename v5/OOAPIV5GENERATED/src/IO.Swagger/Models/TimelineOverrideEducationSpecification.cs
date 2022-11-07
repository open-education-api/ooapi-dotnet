using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace IO.Swagger.Models
{
    /// <summary>
    /// A timeline override of the EducationSpecification.
    /// </summary>
    [DataContract]
    public partial class TimelineOverrideEducationSpecification
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
        /// Gets or Sets EducationSpecification
        /// </summary>
        [JsonRequired]

        [DataMember(Name = "educationSpecification")]
        public EducationSpecification EducationSpecification { get; set; }
    }
}
