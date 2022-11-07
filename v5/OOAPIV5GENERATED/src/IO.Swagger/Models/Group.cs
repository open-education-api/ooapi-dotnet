using IO.Swagger.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IO.Swagger.Models
{
    /// <summary>
    /// A group is simply a collection of persons. Groups can be used to accommodate various usecases.  Groups MAY optionally have a relation to an Offering, however the meaning of such relations is left unspecified and is left up to the implementer. 
    /// </summary>
    [DataContract]
    public partial class Group
    {
        /// <summary>
        /// Unique id for this group
        /// </summary>
        /// <value>Unique id for this group</value>
        [JsonRequired]

        [DataMember(Name = "groupId")]
        public Guid GroupId { get; set; }

        /// <summary>
        /// Gets or Sets PrimaryCode
        /// </summary>
        [JsonRequired]

        [DataMember(Name = "primaryCode")]
        public PrimaryCode PrimaryCode { get; set; }



        /// <summary>
        /// The type of this group - learning group: A collection of participants carrying out common learning activities - class: A collection of participants carrying out jointly scheduled educational activities - team: A collection of members of a team, either students, employees or mixed. 
        /// </summary>
        /// <value>The type of this group - learning group: A collection of participants carrying out common learning activities - class: A collection of participants carrying out jointly scheduled educational activities - team: A collection of members of a team, either students, employees or mixed. </value>
        [JsonRequired]

        [DataMember(Name = "groupType")]
        public GroupTypeEnum? GroupType { get; set; }

        /// <summary>
        /// The name of this group
        /// </summary>
        /// <value>The name of this group</value>
        [JsonRequired]

        [DataMember(Name = "name")]
        public List<LanguageValueItem> Name { get; set; }

        /// <summary>
        /// The description of this group
        /// </summary>
        /// <value>The description of this group</value>

        [DataMember(Name = "description")]
        public List<LanguageValueItem> Description { get; set; }

        /// <summary>
        /// The day on which this group starts being active, RFC3339 (full-date)
        /// </summary>
        /// <value>The day on which this group starts being active, RFC3339 (full-date)</value>

        [DataMember(Name = "startDate")]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// The day on which this group ends being active, RFC3339 (full-date)
        /// </summary>
        /// <value>The day on which this group ends being active, RFC3339 (full-date)</value>

        [DataMember(Name = "endDate")]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// The number of persons that are member of this group
        /// </summary>
        /// <value>The number of persons that are member of this group</value>

        [DataMember(Name = "personCount")]
        public decimal? PersonCount { get; set; }

        /// <summary>
        /// An array of additional human readable codes/identifiers for the entity being described.
        /// </summary>
        /// <value>An array of additional human readable codes/identifiers for the entity being described.</value>

        [DataMember(Name = "otherCodes")]
        public List<OtherCodes> OtherCodes { get; set; }

        /// <summary>
        /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
        /// </summary>
        /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>

        [DataMember(Name = "consumers")]
        public List<Consumer> Consumers { get; set; }

        /// <summary>
        /// The organization that manages this group. [&#x60;expandable&#x60;](.#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
        /// </summary>
        /// <value>The organization that manages this group. [&#x60;expandable&#x60;](.#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

        [DataMember(Name = "organization")]
        public OneOfOrganization Organization { get; set; }

        /// <summary>
        /// Object for additional non-standard attributes
        /// </summary>
        /// <value>Object for additional non-standard attributes</value>

        [DataMember(Name = "ext")]
        public Object Ext { get; set; }
    }
}
