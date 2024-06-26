using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ooapi.v5.Attributes;
using ooapi.v5.core.Extensions;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Enums;
using ooapi.v5.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ooapi.v5.Models;

public class Offering : ModelBase
{
    /// <summary>
    /// Unique id of this offering
    /// </summary>
    /// <value>Unique id of this offering</value>
    [JsonRequired]
    [JsonProperty("offeringId")]
    [SortAllowed]
    public Guid OfferingId { get; set; }

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
    /// The type of this offering
    /// </summary>
    /// <value>The type of this offering</value>
    [JsonRequired]
    [JsonProperty(PropertyName = "offeringType")]
    public OfferingType? OfferingType { get; set; }

    /// <summary>
    /// The academicsession during which this ffering takes place. [&#x60;expandable&#x60;](#tag/academic_session_model) By default only the &#x60;academicSessionId&#x60; (a string) is returned. If the client requested an expansion of &#x60;academicSession&#x60; the full academicsession object should be returned. 
    /// </summary>
    /// <value>The academicsession during which this offering takes place. [&#x60;expandable&#x60;](#tag/academic_session_model) By default only the &#x60;academicSessionId&#x60; (a string) is returned. If the client requested an expansion of &#x60;academicSession&#x60; the full academicsession object should be returned. </value>
    [JsonProperty(PropertyName = "academicSession")]
    [NotMapped]
    [JsonConverter(typeof(OneOfConverter))]
    public OneOfAcademicSession? OneOfAcademicSession
    {
        get
        {
            if (AcademicSessionId == null)
            {
                return null;
            }

            return new OneOfAcademicSessionInstance(AcademicSessionId, AcademicSession);
        }
    }

    [JsonIgnore]
    public Guid? AcademicSessionId { get; set; }

    [JsonIgnore]
    public AcademicSession? AcademicSession { get; set; }

    /// <summary>
    /// The name of this offering
    /// </summary>
    /// <value>The name of this offering</value>
    [JsonRequired]
    [JsonProperty(PropertyName = "name")]
    [NotMapped]
    public List<LanguageTypedString> name => Attributes.ExtractStringsByPropertyName(nameof(name));

    /// <summary>
    /// List of attribtes
    /// </summary>
    [JsonIgnore]
    [SortAllowed]
    [SortDefault]
    public List<LanguageTypedProperty> Attributes { get; set; } = default!;

    /// <summary>
    /// The abbreviation or internal code used to identify this offering
    /// </summary>
    /// <value>The abbreviation or internal code used to identify this offering</value>
    [MaxLength(256)]
    [JsonProperty(PropertyName = "abbreviation")]
    public string Abbreviation { get; set; } = default!;

    /// <summary>
    /// The description of this offering.
    /// </summary>
    /// <value>The description of this offering.</value>
    [JsonRequired]
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
    /// The (primary) teaching language in which this offering is given, should be a three-letter language code as specified by ISO 639-2.
    /// </summary>
    /// <value>The (primary) teaching language in which this offering is given, should be a three-letter language code as specified by ISO 639-2.</value>
    [JsonRequired]
    [RegularExpression("/^[a-z]{3}$/")]
    [StringLength(3, MinimumLength = 3)]
    [JsonProperty(PropertyName = "teachingLanguage")]
    public string TeachingLanguage { get; set; } = default!;

    /// <summary>
    /// Mode of delivery
    /// </summary>
    [JsonIgnore]
    public string? ModeOfDelivery { get; set; }

    /// <summary>
    /// The mode of delivery of the component (ECTS-mode of delivery) - distance-learning: afstandsleren - on campus: op de campus - online: online - hybrid: hybride - situated: op locatie 
    /// </summary>
    /// <value>The mode of delivery of the component (ECTS-mode of delivery) - distance-learning: afstandsleren - on campus: op de campus - online: online - hybrid: hybride - situated: op locatie </value>
    [JsonProperty(PropertyName = "modeOfDelivery")]
    [NotMapped]
    public List<ModeOfDelivery> ModeOfDel
    {
        get
        {
            if (!string.IsNullOrEmpty(ModeOfDelivery))
            {
                var list = new List<ModeOfDelivery>();
                var affiliations = ModeOfDelivery.Split(',');
                foreach (var affiliation in affiliations)
                {
                    switch (affiliation)
                    {
                        case "distance-learning":
                            {
                                list.Add(Enums.ModeOfDelivery.distance_learning);
                                break;
                            }
                        case "on campus":
                            {
                                list.Add(Enums.ModeOfDelivery.on_campus);
                                break;
                            }
                        case "online":
                            {
                                list.Add(Enums.ModeOfDelivery.online);
                                break;
                            }
                        case "hybrid":
                            {
                                list.Add(Enums.ModeOfDelivery.hybrid);
                                break;
                            }
                        case "situated":
                            {
                                list.Add(Enums.ModeOfDelivery.situated);
                                break;
                            }
                        default:
                            break;
                    }
                }
                return list;
            }
            return new List<ModeOfDelivery>();
        }
    }

    /// <summary>
    /// The maximum number of students allowed to enroll for this offering
    /// </summary>
    /// <value>The maximum number of students allowed to enroll for this offering</value>
    [JsonProperty(PropertyName = "maxNumberStudents")]
    public int MaxNumberStudents { get; set; }

    /// <summary>
    /// The number of students that have already enrolled for this offering
    /// </summary>
    /// <value>The number of students that have already enrolled for this offering</value>
    [JsonProperty(PropertyName = "enrolledNumberStudents")]
    public int EnrolledNumberStudents { get; set; }

    /// <summary>
    /// The number of students that have a pending enrollment request for this offering
    /// </summary>
    /// <value>The number of students that have a pending enrollment request for this offering</value>
    [JsonProperty(PropertyName = "pendingNumberStudents")]
    public int? PendingNumberStudents { get; set; }

    /// <summary>
    /// The minimum number of students needed for this offering to proceed
    /// </summary>
    /// <value>The minimum number of students needed for this offering to proceed</value>
    [JsonProperty(PropertyName = "minNumberStudents")]
    public int? MinNumberStudents { get; set; }

    /// <summary>
    /// resultExpected, previously knwon as isLineItem is used so the specific instance of the object is identified as being an element that CAN contain �grade� information. Offerings do not always have to result in a grade or an other type of result.  If there is a result expected from a programOffering/courseOffering/componentOffering the is resultExpected field should parse true 
    /// </summary>
    /// <value>resultExpected, previously knwon as isLineItem is used so the specific instance of the object is identified as being an element that CAN contain �grade� information. Offerings do not always have to result in a grade or an other type of result.  If there is a result expected from a programOffering/courseOffering/componentOffering the is resultExpected field should parse true </value>
    [JsonRequired]
    [JsonProperty(PropertyName = "resultExpected")]
    public bool ResultExpected { get; set; }

    /// <summary>
    /// The result value type for this offering
    /// </summary>
    /// <value>The result value type for this offering</value>
    [JsonProperty(PropertyName = "resultValueType")]
    public ResultValueType? ResultValueType { get; set; }

    /// <summary>
    /// URL of this offering&#x27;s webpage.
    /// </summary>
    /// <value>URL of this offering&#x27;s webpage.</value>
    [MaxLength(2048)]
    [JsonProperty(PropertyName = "link")]
    public string? Link { get; set; }

    /// <summary>
    /// An array of additional human readable codes/identifiers for the entity being described.
    /// </summary>
    /// <value>An array of additional human readable codes/identifiers for the entity being described.</value>
    [JsonProperty(PropertyName = "otherCodes")]
    public List<OtherCodes> OtherCodes { get; set; } = default!;

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

    /// <summary>
    /// List of consumers
    /// </summary>
    [JsonIgnore]
    public List<ConsumerBase> Consumers { get; set; } = default!;

    /// <summary>
    /// The first day on which a student can enroll for this course.
    /// </summary>
    /// <value>The first day on which a student can enroll for this course.</value>
    [JsonProperty(PropertyName = "enrollStartDate")]
    [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
    public DateTime? EnrollStartDate { get; set; }

    /// <summary>
    /// The last day on which a student can enroll for this course.
    /// </summary>
    /// <value>The last day on which a student can enroll for this course.</value>
    [JsonProperty(PropertyName = "enrollEndDate")]
    [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
    public DateTime? EnrollEndDate { get; set; }

    /// <summary>
    /// The organization that manages this courseoffering. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
    /// </summary>
    /// <value>The organization that manages this courseoffering. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

    [JsonProperty(PropertyName = "organization")]
    [NotMapped]
    [JsonConverter(typeof(OneOfConverter))]
    public OneOfOrganization? OneOfOrganization
    {
        get
        {
            if (OrganizationId == null)
            {
                return null;
            }

            return new OneOfOrganizationInstance(OrganizationId, Organization);
        }
    }

    /// <summary>
    /// Id of the organization
    /// </summary>
    [JsonIgnore]
    public Guid? OrganizationId { get; set; }

    /// <summary>
    /// The organization
    /// </summary>
    [JsonIgnore]
    public Organization? Organization { get; set; }

    /// <summary>
    /// Collection of addresses
    /// </summary>
    [JsonIgnore]
    public virtual ICollection<Address> Address { get; set; } = default!;

    /// <summary>
    /// Collection of costs
    /// </summary>
    [JsonIgnore]
    public virtual ICollection<Cost> Costs { get; set; } = default!;
}
