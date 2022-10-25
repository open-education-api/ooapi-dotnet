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
        /// If this is a course wherein participants can start at various moments, without missing anything, use this attribute in combination with &#x60;flexibleEntryPeriodEnd&#x60;.
        /// </summary>
        /// <value>If this is a course wherein participants can start at various moments, without missing anything, use this attribute in combination with &#x60;flexibleEntryPeriodEnd&#x60;.</value>

        [DataMember(Name = "flexibleEntryPeriodStart")]
        public DateTime? FlexibleEntryPeriodStart { get; set; }

        /// <summary>
        /// If this is a course wherein participants can start at various moments, without missing anything, use this attribute in combination with &#x60;flexibleEntryPeriodStart&#x60;.
        /// </summary>
        /// <value>If this is a course wherein participants can start at various moments, without missing anything, use this attribute in combination with &#x60;flexibleEntryPeriodStart&#x60;.</value>

        [DataMember(Name = "flexibleEntryPeriodEnd")]
        public DateTime? FlexibleEntryPeriodEnd { get; set; }

        /// <summary>
        /// Addresses for this offering
        /// </summary>
        /// <value>Addresses for this offering</value>

        [DataMember(Name = "addresses")]
        public List<Address> Addresses { get; set; }

        /// <summary>
        /// Price information for this offering.
        /// </summary>
        /// <value>Price information for this offering.</value>

        [DataMember(Name = "priceInformation")]
        public List<Cost> PriceInformation { get; set; }








    }
}
