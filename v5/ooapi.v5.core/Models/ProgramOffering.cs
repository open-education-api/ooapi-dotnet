using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class ProgramOffering : OfferingShared
    {


        /// <summary>
        /// If this is a program wherein participants can start at various moments, without missing anything, use this attribute in combination with &#x60;flexibleEntryPeriodEnd&#x60;.
        /// </summary>
        /// <value>If this is a program wherein participants can start at various moments, without missing anything, use this attribute in combination with &#x60;flexibleEntryPeriodEnd&#x60;.</value>

        [DataMember(Name = "flexibleEntryPeriodStart")]
        public DateTime? FlexibleEntryPeriodStart { get; set; }

        /// <summary>
        /// If this is a program wherein participants can start at various moments, without missing anything, use this attribute in combination with &#x60;flexibleEntryPeriodStart&#x60;.
        /// </summary>
        /// <value>If this is a program wherein participants can start at various moments, without missing anything, use this attribute in combination with &#x60;flexibleEntryPeriodStart&#x60;.</value>

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

        /// <summary>
        /// The Program that is offered in this programoffering. [&#x60;expandable&#x60;](#tag/program_model) By default only the &#x60;programId&#x60; (a string) is returned. If the client requested an expansion of &#x60;program&#x60; the full program object should be returned. 
        /// </summary>
        /// <value>The Program that is offered in this programoffering. [&#x60;expandable&#x60;](#tag/program_model) By default only the &#x60;programId&#x60; (a string) is returned. If the client requested an expansion of &#x60;program&#x60; the full program object should be returned. </value>

        [DataMember(Name = "program")]
        public OneOfProgram Program { get; set; }

        /// <summary>
        /// The organization that manages this programeoffering. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
        /// </summary>
        /// <value>The organization that manages this programeoffering. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

        [DataMember(Name = "organization")]
        public OneOfOrganization Organization { get; set; }




    }
}
