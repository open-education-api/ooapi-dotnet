using Newtonsoft.Json;
using ooapi.v5.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// A description of a group of people working together to achieve a goal
    /// </summary>
    [DataContract]
    public partial class Organization : ModelBase
    {
        /// <summary>
        /// Unique id of this organization
        /// </summary>
        /// <value>Unique id of this organization</value>
        [JsonRequired]
        [JsonProperty(PropertyName = "organizationId")]
        public Guid OrganizationId { get; set; }

        /// <summary>
        /// Gets or Sets PrimaryCode
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "primaryCode")]
        [NotMapped]
        public PrimaryCode primaryCode
        {
            get
            {
                return new PrimaryCode() { CodeType = PrimaryCodeType, Code = PrimaryCode };
            }
            set
            {
                PrimaryCode = value.Code;
                PrimaryCodeType = value.CodeType;
            }
        }


        [JsonIgnore]
        public string PrimaryCodeType { get; set; }

        [JsonIgnore]
        public string PrimaryCode { get; set; }


        /// <summary>
        /// The type of this organization. Each OOAPI endpoint should have a single organization with type &#x60;root&#x60;, describing the root organization. - root: the root of this organization, representing the Educational Institution itself - institute: instituut - department: departement - faculty: faculteit - branch: vestiging - academy: academie - school: school 
        /// </summary>
        /// <value>The type of this organization. Each OOAPI endpoint should have a single organization with type &#x60;root&#x60;, describing the root organization. - root: the root of this organization, representing the Educational Institution itself - institute: instituut - department: departement - faculty: faculteit - branch: vestiging - academy: academie - school: school </value>
        [JsonRequired]

        [JsonProperty(PropertyName = "organizationType")]
        public OrganizationTypeEnum? OrganizationType { get; set; }

        /// <summary>
        /// The name of the organization
        /// </summary>
        /// <value>The name of the organization</value>
        [JsonRequired]
        [JsonProperty(PropertyName = "name")]
        [NotMapped]
        public List<LanguageTypedString> name
        {
            get
            {
                return (List<LanguageTypedString>)JsonConvert.DeserializeObject(Name);
            }
            set
            {
                Name = JsonConvert.SerializeObject(value);
            }
        }


        [JsonIgnore]
        public string Name { get; set; }



        /// <summary>
        /// Short name of the organization
        /// </summary>
        /// <value>Short name of the organization</value>
        [JsonRequired]

        [MaxLength(256)]
        [JsonProperty(PropertyName = "shortName")]
        public string ShortName { get; set; }

        /// <summary>
        /// Any general description of the organization should clearly mention the type of higher education organization, especially in the case of a binary system. In Dutch; universiteit (university) or hogeschool (university of applied sciences).
        /// </summary>
        /// <value>Any general description of the organization should clearly mention the type of higher education organization, especially in the case of a binary system. In Dutch; universiteit (university) or hogeschool (university of applied sciences).</value>
        [JsonProperty(PropertyName = "description")]
        [NotMapped]
        public List<LanguageTypedString>? description
        {
            get
            {
                return (List<LanguageTypedString>)JsonConvert.DeserializeObject(Description);
            }
            set
            {
                Description = JsonConvert.SerializeObject(value);
            }
        }

        [JsonIgnore]
        public string? Description { get; set; }

        /// <summary>
        /// Addresses of this organization
        /// </summary>
        /// <value>Addresses of this organization</value>

        [JsonProperty(PropertyName = "addresses")]
        [NotMapped]
        public List<Address>? Addresses { get; set; }

        [JsonIgnore]
        public List<OrganizationAddress>? OrganizationAddresses { get; set; }


        /// <summary>
        /// URL of the organization&#x27;s website
        /// </summary>
        /// <value>URL of the organization&#x27;s website</value>

        [MaxLength(2048)]
        [JsonProperty(PropertyName = "link")]
        public string? Link { get; set; }

        /// <summary>
        /// Logo of this organization
        /// </summary>
        /// <value>Logo of this organization</value>

        [MaxLength(2048)]
        [JsonProperty(PropertyName = "logo")]
        public string? Logo { get; set; }

        /// <summary>
        /// An array of additional human readable codes/identifiers for the entity being described.
        /// </summary>
        /// <value>An array of additional human readable codes/identifiers for the entity being described.</value>

        [JsonProperty(PropertyName = "otherCodes")]
        public List<OtherCodes>? OtherCodes { get; set; }

        /// <summary>
        /// The organizational unit which is the parent of this organization. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
        /// </summary>
        /// <value>The organizational unit which is the parent of this organization. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

        [JsonProperty(PropertyName = "parent")]
        public OneOfOrganization? Parent { get; set; }

        /// <summary>
        /// All the organizational units for which this organization is the parent. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
        /// </summary>
        /// <value>All the organizational units for which this organization is the parent. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

        [JsonProperty(PropertyName = "children")]
        [NotMapped]
        public List<OneOfOrganization>? Children { get; set; }

        /// <summary>
        /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
        /// </summary>
        /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>

        [JsonProperty(PropertyName = "consumers")]
        public List<Consumer>? Consumers { get; set; }


    }
}
