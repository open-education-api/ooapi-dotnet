using IO.Swagger.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace IO.Swagger.Models
{
    /// <summary>
    /// A description of a group of people working together to achieve a goal
    /// </summary>
    [DataContract]
    public partial class Organization
    {
        /// <summary>
        /// Unique id of this organization
        /// </summary>
        /// <value>Unique id of this organization</value>
        [JsonRequired]

        [DataMember(Name = "organizationId")]
        public Guid OrganizationId { get; set; }

        /// <summary>
        /// Gets or Sets PrimaryCode
        /// </summary>
        [JsonRequired]

        [DataMember(Name = "primaryCode")]
        public PrimaryCode PrimaryCode { get; set; }


        /// <summary>
        /// The type of this organization. Each OOAPI endpoint should have a single organization with type &#x60;root&#x60;, describing the root organization. - root: the root of this organization, representing the Educational Institution itself - institute: instituut - department: departement - faculty: faculteit - branch: vestiging - academy: academie - school: school 
        /// </summary>
        /// <value>The type of this organization. Each OOAPI endpoint should have a single organization with type &#x60;root&#x60;, describing the root organization. - root: the root of this organization, representing the Educational Institution itself - institute: instituut - department: departement - faculty: faculteit - branch: vestiging - academy: academie - school: school </value>
        [JsonRequired]

        [DataMember(Name = "organizationType")]
        public OrganizationTypeEnum? OrganizationType { get; set; }

        /// <summary>
        /// The name of the organization
        /// </summary>
        /// <value>The name of the organization</value>
        [JsonRequired]

        [DataMember(Name = "name")]
        public List<LanguageValueItem> Name { get; set; }

        /// <summary>
        /// Short name of the organization
        /// </summary>
        /// <value>Short name of the organization</value>
        [JsonRequired]

        [MaxLength(256)]
        [DataMember(Name = "shortName")]
        public string ShortName { get; set; }

        /// <summary>
        /// Any general description of the organization should clearly mention the type of higher education organization, especially in the case of a binary system. In Dutch; universiteit (university) or hogeschool (university of applied sciences).
        /// </summary>
        /// <value>Any general description of the organization should clearly mention the type of higher education organization, especially in the case of a binary system. In Dutch; universiteit (university) or hogeschool (university of applied sciences).</value>

        [DataMember(Name = "description")]
        public List<LanguageValueItem> Description { get; set; }

        /// <summary>
        /// Addresses of this organization
        /// </summary>
        /// <value>Addresses of this organization</value>

        [DataMember(Name = "addresses")]
        public List<Address> Addresses { get; set; }

        /// <summary>
        /// URL of the organization&#x27;s website
        /// </summary>
        /// <value>URL of the organization&#x27;s website</value>

        [MaxLength(2048)]
        [DataMember(Name = "link")]
        public string Link { get; set; }

        /// <summary>
        /// Logo of this organization
        /// </summary>
        /// <value>Logo of this organization</value>

        [MaxLength(2048)]
        [DataMember(Name = "logo")]
        public string Logo { get; set; }

        /// <summary>
        /// An array of additional human readable codes/identifiers for the entity being described.
        /// </summary>
        /// <value>An array of additional human readable codes/identifiers for the entity being described.</value>

        [DataMember(Name = "otherCodes")]
        public List<OtherCodes> OtherCodes { get; set; }

        /// <summary>
        /// The organizational unit which is the parent of this organization. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
        /// </summary>
        /// <value>The organizational unit which is the parent of this organization. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

        [DataMember(Name = "parent")]
        public OneOfOrganization Parent { get; set; }

        /// <summary>
        /// All the organizational units for which this organization is the parent. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
        /// </summary>
        /// <value>All the organizational units for which this organization is the parent. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

        [DataMember(Name = "children")]
        public List<OneOfOrganization> Children { get; set; }

        /// <summary>
        /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
        /// </summary>
        /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>

        [DataMember(Name = "consumers")]
        public List<Consumer> Consumers { get; set; }

        /// <summary>
        /// Object for additional non-standard attributes
        /// </summary>
        /// <value>Object for additional non-standard attributes</value>

        [DataMember(Name = "ext")]
        public Object Ext { get; set; }
    }
}
