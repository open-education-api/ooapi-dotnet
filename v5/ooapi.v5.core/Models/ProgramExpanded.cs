using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class ProgramExpanded
    {
        /// <summary>
        /// Timeline overrides allow an implementation to provide versions of entities that will be valid in the future or have been in the past.
        /// </summary>
        /// <value>Timeline overrides allow an implementation to provide versions of entities that will be valid in the future or have been in the past.</value>

        [DataMember(Name = "timelineOverrides")]
        public List<ProgramExpandedTimelineOverrides> TimelineOverrides { get; set; }
    }

}
