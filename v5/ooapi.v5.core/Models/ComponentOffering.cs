using Newtonsoft.Json;
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
        /// Addresses for this offering
        /// </summary>
        /// <value>Addresses for this offering</value>

        [JsonProperty(PropertyName = "addresses")]
        public List<Address> Addresses { get; set; }

        /// <summary>
        /// Price information for this offering.
        /// </summary>
        /// <value>Price information for this offering.</value>

        [JsonProperty(PropertyName = "priceInformation")]
        public List<Cost> PriceInformation { get; set; }

        /////// <summary>
        /////// Gets or Sets Room
        /////// </summary>

        ////[JsonProperty(PropertyName = "room")]
        ////public Room Room { get; set; }

        /// <summary>
        /// The component that is offered in this componentoffering. [&#x60;expandable&#x60;](#tag/component_model) By default only the &#x60;componentId&#x60; (a string) is returned. If the client requested an expansion of &#x60;component&#x60; the full component object should be returned. 
        /// </summary>
        /// <value>The component that is offered in this componentoffering. [&#x60;expandable&#x60;](#tag/component_model) By default only the &#x60;componentId&#x60; (a string) is returned. If the client requested an expansion of &#x60;component&#x60; the full component object should be returned. </value>

        [JsonProperty(PropertyName = "component")]
        public OneOfComponent Component { get; set; }

        /// <summary>
        /// The courseoffering where this componentoffering is related to. [&#x60;expandable&#x60;](#tag/course_offering_model) By default only the &#x60;courseOfferingId&#x60; (a string) is returned. If the client requested an expansion of &#x60;courseOffering&#x60; the full courseOffering object should be returned. 
        /// </summary>
        /// <value>The courseoffering where this componentoffering is related to. [&#x60;expandable&#x60;](#tag/course_offering_model) By default only the &#x60;courseOfferingId&#x60; (a string) is returned. If the client requested an expansion of &#x60;courseOffering&#x60; the full courseOffering object should be returned. </value>

        [JsonProperty(PropertyName = "courseOffering")]
        public OneOfCourseOffering CourseOffering { get; set; }




    }
}
