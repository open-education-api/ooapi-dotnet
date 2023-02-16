using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ooapi.v5.Attributes;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Enums;
using ooapi.v5.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// A group is simply a collection of persons. Groups can be used to accommodate various usecases.  Groups MAY optionally have a relation to an Offering, however the meaning of such relations is left unspecified and is left up to the implementer. 
    /// </summary>
    [DataContract]
    public class Group : ModelBase
    {
        /// <summary>
        /// Unique id for this group
        /// </summary>
        /// <value>Unique id for this group</value>
        [JsonRequired]

        [JsonProperty(PropertyName = "groupId")]
        [SortAllowed]
        public Guid GroupId { get; set; }

        /// <summary>
        /// Gets or Sets PrimaryCode
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "primaryCode")]
        [NotMapped]
        public IdentifierEntry primaryCode
        {
            get
            {
                return new IdentifierEntry() { CodeType = PrimaryCodeType, Code = PrimaryCode };
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
        /// The type of this group - learning group: A collection of participants carrying out common learning activities - class: A collection of participants carrying out jointly scheduled educational activities - team: A collection of members of a team, either students, employees or mixed. 
        /// </summary>
        /// <value>The type of this group - learning group: A collection of participants carrying out common learning activities - class: A collection of participants carrying out jointly scheduled educational activities - team: A collection of members of a team, either students, employees or mixed. </value>
        [JsonRequired]

        [JsonProperty(PropertyName = "groupType")]
        public GroupTypeEnum? GroupType { get; set; }

        /// <summary>
        /// The name of this group
        /// </summary>
        /// <value>The name of this group</value>
        [JsonRequired]
        [JsonProperty(PropertyName = "name")]
        [NotMapped]
        public List<LanguageTypedString> name
        {
            get
            {
                List<LanguageTypedString> result = new List<LanguageTypedString>();
                if (Attributes != null && Attributes.Any())
                {
                    result = Attributes.Where(x => x.PropertyName.Equals("name")).Select(x => new LanguageTypedString() { Language = x.Language, Value = x.Value }).ToList();
                }
                return result;
            }
        }


        [JsonIgnore]
        [SortAllowed]
        [SortDefault]
        public List<Attribute> Attributes { get; set; }



        /// <summary>
        /// The description of this group
        /// </summary>
        /// <value>The description of this group</value>
        [JsonProperty(PropertyName = "description")]
        [NotMapped]
        public List<LanguageTypedString> description
        {
            get
            {
                List<LanguageTypedString> result = new List<LanguageTypedString>();
                if (Attributes != null && Attributes.Any())
                {
                    result = Attributes.Where(x => x.PropertyName.Equals("description")).Select(x => new LanguageTypedString() { Language = x.Language, Value = x.Value }).ToList();
                }
                return result;
            }
        }


        /// <summary>
        /// The day on which this group starts being active, RFC3339 (full-date)
        /// </summary>
        /// <value>The day on which this group starts being active, RFC3339 (full-date)</value>

        [JsonProperty(PropertyName = "startDate")]
        [SortAllowed]
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// The day on which this group ends being active, RFC3339 (full-date)
        /// </summary>
        /// <value>The day on which this group ends being active, RFC3339 (full-date)</value>

        [JsonProperty(PropertyName = "endDate")]
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// The number of persons that are member of this group
        /// </summary>
        /// <value>The number of persons that are member of this group</value>

        [NotMapped]
        [JsonProperty(PropertyName = "personCount")]
        public int PersonCount { get; set; }

        /// <summary>
        /// An array of additional human readable codes/identifiers for the entity being described.
        /// </summary>
        /// <value>An array of additional human readable codes/identifiers for the entity being described.</value>

        [JsonProperty(PropertyName = "otherCodes")]
        public List<OtherCodes> OtherCodes { get; set; }

        /// <summary>
        /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
        /// </summary>
        /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>

        [JsonProperty(PropertyName = "consumers")]
        [NotMapped]
        public List<JObject>? ConsumersList
        {
            get
            {
                if (Consumers != null && Consumers.Any())
                    return ConsumerConverter.GetDynamicConsumers(Consumers);
                return null;
            }
        }

        [JsonIgnore]
        public List<Consumer>? Consumers { get; set; }

        /// <summary>
        /// The organization that manages this group. [&#x60;expandable&#x60;](.#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
        /// </summary>
        /// <value>The organization that manages this group. [&#x60;expandable&#x60;](.#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

        [JsonProperty(PropertyName = "organization")]
        [NotMapped]
        [JsonConverter(typeof(OneOfConverter))]
        public OneOfOrganization OneOfOrganization
        {
            get
            {
                if (OrganizationId == null) return null;
                return new OneOfOrganizationInstance(OrganizationId, Organization);
            }
        }

        [JsonIgnore]
        public Guid? OrganizationId { get; set; }

        [JsonIgnore]
        public Organization? Organization { get; set; }


        [JsonIgnore]
        public virtual ICollection<Person> Persons { get; set; }


    }
}