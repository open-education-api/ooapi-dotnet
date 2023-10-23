using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ooapi.v5.Attributes;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Enums;
using ooapi.v5.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;

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
    [SortAllowed]
    public Guid OrganizationId { get; set; }

    /// <summary>
    /// Gets or Sets PrimaryCode
    /// </summary>
    [JsonProperty(PropertyName = "primaryCode")]
    [NotMapped]
    public IdentifierEntry? PrimaryCodeIdentifier
    {
        get
        {
            if (PrimaryCodeType is not null && PrimaryCode is not null)
            {
                return new IdentifierEntry() { CodeType = PrimaryCodeType, Code = PrimaryCode };
            }

            return null;
        }
        set
        {
            if (value is not null)
            {
                PrimaryCode = value.Code;
                PrimaryCodeType = value.CodeType;
            }
        }
    }

    [JsonIgnore]
    public string? PrimaryCodeType { get; set; }

    [JsonIgnore]
    public string? PrimaryCode { get; set; }

    /// <summary>
    /// The type of this organization. Each OOAPI endpoint should have a single organization with type &#x60;root&#x60;, describing the root organization. - root: the root of this organization, representing the Educational Institution itself - institute: instituut - department: departement - faculty: faculteit - branch: vestiging - academy: academie - school: school 
    /// </summary>
    /// <value>The type of this organization. Each OOAPI endpoint should have a single organization with type &#x60;root&#x60;, describing the root organization. - root: the root of this organization, representing the Educational Institution itself - institute: instituut - department: departement - faculty: faculteit - branch: vestiging - academy: academie - school: school </value>
    [JsonRequired]

    [JsonProperty(PropertyName = "organizationType")]
    public OrganizationType? OrganizationType { get; set; }

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
            var result = new List<LanguageTypedString>();
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
    public List<Attribute> Attributes { get; set; } = default!;

    /// <summary>
    /// Short name of the organization
    /// </summary>
    /// <value>Short name of the organization</value>
    [JsonRequired]
    [MaxLength(256)]
    [JsonProperty(PropertyName = "shortName")]
    public string ShortName { get; set; } = default!;

    /// <summary>
    /// Any general description of the organization should clearly mention the type of higher education organization, especially in the case of a binary system. In Dutch; universiteit (university) or hogeschool (university of applied sciences).
    /// </summary>
    /// <value>Any general description of the organization should clearly mention the type of higher education organization, especially in the case of a binary system. In Dutch; universiteit (university) or hogeschool (university of applied sciences).</value>
    [JsonProperty(PropertyName = "description")]
    [NotMapped]
    public List<LanguageTypedString> description
    {
        get
        {
            var result = new List<LanguageTypedString>();
            if (Attributes != null && Attributes.Any())
            {
                result = Attributes.Where(x => x.PropertyName.Equals("description")).Select(x => new LanguageTypedString() { Language = x.Language, Value = x.Value }).ToList();
            }
            return result;
        }
    }


    /// <summary>
    /// Addresses of this organization
    /// </summary>
    /// <value>Addresses of this organization</value>
    [JsonProperty(PropertyName = "addresses")]
    [NotMapped]
    public List<Address> Addresses { get; set; } = default!;


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
    public List<OtherCodes> OtherCodes { get; set; } = default!;

    /// <summary>
    /// The organizational unit which is the parent of this organization. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
    /// </summary>
    /// <value>The organizational unit which is the parent of this organization. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>
    [JsonProperty(PropertyName = "parent")]
    [NotMapped]
    [JsonConverter(typeof(OneOfConverter))]
    public OneOfOrganization? OneOfOrganization
    {
        get
        {
            if (ParentId == null) return null;
            return new OneOfOrganizationInstance(ParentId, Parent);
        }
    }


    [JsonIgnore]
    public Guid? ParentId { get; set; }


    [JsonIgnore]
    [NotMapped]
    public Organization? Parent { get; set; }

    /// <summary>
    /// All the organizational units for which this organization is the parent. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
    /// </summary>
    /// <value>All the organizational units for which this organization is the parent. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

    [JsonProperty("children")]
    [NotMapped]
    [JsonConverter(typeof(ListOneOfConverter))]
    public List<OneOfOrganization> ChildrenList
    {
        get
        {
            if (ChildrenIds == null || !ChildrenIds.Any())
            {
                return new List<OneOfOrganization>();
            }

            var result = new List<OneOfOrganization>();
            foreach (var ChildId in ChildrenIds)
            {
                result.Add(new OneOfOrganizationInstance(ChildId, Children?.Find(x => x.OrganizationId.Equals(ChildId))));
            }
            return result;
        }
    }


    [JsonIgnore]
    [NotMapped]
    public List<Guid> ChildrenIds { get; set; } = default!;


    [JsonIgnore]
    [NotMapped]
    public List<Organization> Children { get; set; } = default!;

    /// <summary>
    /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
    /// </summary>
    /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>
    [JsonProperty(PropertyName = "consumers")]
    [NotMapped]
    public List<JObject> ConsumersList
    {
        get
        {
            if (Consumers != null && Consumers.Any())
            {
                return ConsumerConverter.GetDynamicConsumers(Consumers);
            }

            return new List<JObject>();
        }
    }


    [JsonIgnore]
    public List<Consumer> Consumers { get; set; } = default!;


    [JsonIgnore]
    public virtual ICollection<Address> Address { get; set; } = default!;
}
