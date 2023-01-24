using Newtonsoft.Json;
using ooapi.v5.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class ComponentOffering : Offering
    {


        [Range(0, 100)]
        [JsonProperty(PropertyName = "resultWeight")]
        public int? ResultWeight { get; set; }



        /// <summary>
        /// Gets or Sets Room
        /// </summary>

        [JsonProperty(PropertyName = "room")]
        public Room? Room { get; set; }

        //[JsonIgnore]
        //public Guid? RoomId { get; set; }

        /// <summary>
        /// The component that is offered in this componentoffering. [&#x60;expandable&#x60;](#tag/component_model) By default only the &#x60;componentId&#x60; (a string) is returned. If the client requested an expansion of &#x60;component&#x60; the full component object should be returned. 
        /// </summary>
        /// <value>The component that is offered in this componentoffering. [&#x60;expandable&#x60;](#tag/component_model) By default only the &#x60;componentId&#x60; (a string) is returned. If the client requested an expansion of &#x60;component&#x60; the full component object should be returned. </value>

        [JsonProperty(PropertyName = "component")]
        public OneOfComponent Component { get; set; }

        //[JsonIgnore]
        //public Guid? ComponentId { get; set; }

        /// <summary>
        /// The courseoffering where this componentoffering is related to. [&#x60;expandable&#x60;](#tag/course_offering_model) By default only the &#x60;courseOfferingId&#x60; (a string) is returned. If the client requested an expansion of &#x60;courseOffering&#x60; the full courseOffering object should be returned. 
        /// </summary>
        /// <value>The courseoffering where this componentoffering is related to. [&#x60;expandable&#x60;](#tag/course_offering_model) By default only the &#x60;courseOfferingId&#x60; (a string) is returned. If the client requested an expansion of &#x60;courseOffering&#x60; the full courseOffering object should be returned. </value>

        [JsonProperty(PropertyName = "course")]
        public OneOfCourse Course { get; set; }

        [JsonIgnore]
        public Guid? CourseId { get; set; }


        /// <summary>
        /// The moment on which this offering starts, RFC3339 (full-date)
        /// </summary>
        /// <value>The moment on which this offering starts, RFC3339 (full-date)</value>
        [JsonRequired]
        [JsonProperty(PropertyName = "startDateTime")]
        [SortAllowed]
        [SortDefault]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// The moment on which this offering ends, RFC3339 (full-date)
        /// </summary>
        /// <value>The moment on which this offering ends, RFC3339 (full-date)</value>
        [JsonRequired]
        [JsonProperty(PropertyName = "endDateTime")]
        [SortAllowed]
        public DateTime? EndDate { get; set; }


    }
}
