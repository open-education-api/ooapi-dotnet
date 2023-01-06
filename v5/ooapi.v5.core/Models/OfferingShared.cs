using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class OfferingShared : Offering
    {


        /// <summary>
        /// The moment on which this offering starts, RFC3339 (full-date)
        /// </summary>
        /// <value>The moment on which this offering starts, RFC3339 (full-date)</value>
        [JsonRequired]
        [JsonProperty(PropertyName = "startDate")]
        public DateOnly? StartDate { get; set; }

        /// <summary>
        /// The moment on which this offering ends, RFC3339 (full-date)
        /// </summary>
        /// <value>The moment on which this offering ends, RFC3339 (full-date)</value>
        [JsonRequired]
        [JsonProperty(PropertyName = "endDate")]
        public DateOnly? EndDate { get; set; }


        /// <summary>
        /// If this is a course wherein participants can start at various moments, without missing anything, use this attribute in combination with &#x60;flexibleEntryPeriodEnd&#x60;.
        /// </summary>
        /// <value>If this is a course wherein participants can start at various moments, without missing anything, use this attribute in combination with &#x60;flexibleEntryPeriodEnd&#x60;.</value>

        [JsonProperty(PropertyName = "flexibleEntryPeriodStart")]
        public DateOnly? FlexibleEntryPeriodStart { get; set; }

        /// <summary>
        /// If this is a course wherein participants can start at various moments, without missing anything, use this attribute in combination with &#x60;flexibleEntryPeriodStart&#x60;.
        /// </summary>
        /// <value>If this is a course wherein participants can start at various moments, without missing anything, use this attribute in combination with &#x60;flexibleEntryPeriodStart&#x60;.</value>

        [JsonProperty(PropertyName = "flexibleEntryPeriodEnd")]
        public DateOnly? FlexibleEntryPeriodEnd { get; set; }


        /// <summary>
        /// Price information for this offering.
        /// </summary>
        /// <value>Price information for this offering.</value>

        [JsonProperty(PropertyName = "priceInformation")]
        [NotMapped]
        public List<Cost> PriceInformation { get; set; }



        //[JsonIgnore]
        //public virtual ICollection<Cost> Costs { get; set; }






    }
}
