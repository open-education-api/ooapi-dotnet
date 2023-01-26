using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class ProgramOffering: OfferingShared
    {



        /// <summary>
        /// The Program that is offered in this programoffering. [&#x60;expandable&#x60;](#tag/program_model) By default only the &#x60;programId&#x60; (a string) is returned. If the client requested an expansion of &#x60;program&#x60; the full program object should be returned. 
        /// </summary>
        /// <value>The Program that is offered in this programoffering. [&#x60;expandable&#x60;](#tag/program_model) By default only the &#x60;programId&#x60; (a string) is returned. If the client requested an expansion of &#x60;program&#x60; the full program object should be returned. </value>

        [JsonProperty(PropertyName = "programOffering")]
        public OneOfProgram? Program { get; set; }

        [JsonIgnore]
        public Guid? ProgramId { get; set; }


        /// <summary>
        /// The organization that manages this programmoffering. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
        /// </summary>
        /// <value>The organization that manages this programmoffering. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

        [JsonProperty(PropertyName = "organization")]
        public OneOfOrganization Organization { get; set; }

    }
}
