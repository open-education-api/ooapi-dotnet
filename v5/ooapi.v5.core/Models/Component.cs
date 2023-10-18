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
/// A component is a part of a course
/// </summary>
[DataContract]
public partial class Component : ModelBase
{
    /// <summary>
    /// Unique id of this component
    /// </summary>
    /// <value>Unique id of this component</value>
    [JsonRequired]
    [JsonProperty(PropertyName = "componentId")]
    [SortAllowed]
    [SortDefault]
    public Guid ComponentId { get; set; }

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

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public string? PrimaryCodeType { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public string? PrimaryCode { get; set; }


    /// <summary>
    /// The component type - test: tentamen - lecture: college - practical: practicum - tutorial: werkcollege - consultation: consultatie - project: project - workshop: workshop - excursion: excursie - independent study: zelfstudie - external: extern - skills training: vaardighedentraining 
    /// </summary>
    /// <value>The component type - test: tentamen - lecture: college - practical: practicum - tutorial: werkcollege - consultation: consultatie - project: project - workshop: workshop - excursion: excursie - independent study: zelfstudie - external: extern - skills training: vaardighedentraining </value>
    [JsonRequired]

    [JsonProperty(PropertyName = "componentType")]
    public ComponentType? ComponentType { get; set; }

    /// <summary>
    /// The name of this component
    /// </summary>
    /// <value>The name of this component</value>
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

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    [SortAllowed]
    [SortDefault]
    public List<Attribute> Attributes { get; set; } = default!;

    /// <summary>
    /// The abbreviation of this component
    /// </summary>
    /// <value>The abbreviation of this component</value>
    [JsonRequired]
    [MaxLength(256)]
    [JsonProperty(PropertyName = "abbreviation")]
    public string Abbreviation { get; set; } = default!;

    /// <summary>
    /// 
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
    /// The duration of this component. The duration format is from the ISO 8601 ABNF as given in Appendix A of RFC 3339.
    /// </summary>
    /// <value>The duration of this component. The duration format is from the ISO 8601 ABNF as given in Appendix A of RFC 3339.</value>
    [RegularExpression("/^(-?)P(?=\\d|T\\d)(?:(\\d+)Y)?(?:(\\d+)M)?(?:(\\d+)([DW]))?(?:T(?:(\\d+)H)?(?:(\\d+)M)?(?:(\\d+(?:\\.\\d+)?)S)?)?$/")]
    [JsonProperty(PropertyName = "duration")]
    public string Duration { get; set; } = default!;

    /// <summary>
    /// The description of this component.
    /// </summary>
    /// <value>The description of this component.</value>
    [JsonProperty(PropertyName = "description")]
    [NotMapped]
    public List<LanguageTypedString> Description
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
    /// The (primary) teaching language in which this component is given, should be a three-letter language code as specified by ISO 639-2.
    /// </summary>
    /// <value>The (primary) teaching language in which this component is given, should be a three-letter language code as specified by ISO 639-2.</value>
    [JsonRequired]
    [RegularExpression("/^[a-z]{3}$/")]
    [StringLength(3, MinimumLength = 3)]
    [JsonProperty(PropertyName = "teachingLanguage")]
    public string TeachingLanguage { get; set; } = default!;

    /// <summary>
    /// Statements that describe the knowledge or skills students should acquire by the end of a particular course (ECTS-learningoutcome).
    /// </summary>
    /// <value>Statements that describe the knowledge or skills students should acquire by the end of a particular course (ECTS-learningoutcome).</value>

    [JsonProperty(PropertyName = "learningOutcomes")]
    [NotMapped]
    public List<List<LanguageTypedString>> LearningOutcomes { get; set; } = default!;

    /// <summary>
    /// The extra information that is provided for enrollment
    /// </summary>
    /// <value>The extra information that is provided for enrollment</value>

    [JsonProperty(PropertyName = "enrollment")]
    [NotMapped]
    public List<LanguageTypedString> enrollment
    {
        get
        {
            var result = new List<LanguageTypedString>();
            if (Attributes != null && Attributes.Any())
            {
                result = Attributes.Where(x => x.PropertyName.Equals("enrollment")).Select(x => new LanguageTypedString() { Language = x.Language, Value = x.Value }).ToList();
            }
            return result;
        }
    }

    /// <summary>
    /// A description of the way exams for this course are taken (ECTS-assessment method and criteria).
    /// </summary>
    /// <value>A description of the way exams for this course are taken (ECTS-assessment method and criteria).</value>
    [JsonProperty(PropertyName = "assessment")]
    [NotMapped]
    public List<LanguageTypedString> assessment
    {
        get
        {
            var result = new List<LanguageTypedString>();
            if (Attributes != null && Attributes.Any())
            {
                result = Attributes.Where(x => x.PropertyName.Equals("assessment")).Select(x => new LanguageTypedString() { Language = x.Language, Value = x.Value }).ToList();
            }
            return result;
        }
    }

    /// <summary>
    /// Addresses for this component
    /// </summary>
    /// <value>Addresses for this component</value>
    [JsonProperty(PropertyName = "addresses")]
    [NotMapped]
    public List<Address> Addresses { get; set; } = default!;


    /// <summary>
    /// An array of additional human readable codes/identifiers for the entity being described.
    /// </summary>
    /// <value>An array of additional human readable codes/identifiers for the entity being described.</value>
    [JsonProperty(PropertyName = "otherCodes")]
    public List<OtherCodes> OtherCodes { get; set; } = default!;

    /// <summary>
    /// The course of which this component is a part. [&#x60;expandable&#x60;](#tag/course_model) By default only the &#x60;courseId&#x60; (a string) is returned. If the client requested an expansion of &#x60;course&#x60; the full course object should be returned. 
    /// </summary>
    /// <value>The course of which this component is a part. [&#x60;expandable&#x60;](#tag/course_model) By default only the &#x60;courseId&#x60; (a string) is returned. If the client requested an expansion of &#x60;course&#x60; the full course object should be returned. </value>

    [JsonProperty(PropertyName = "course")]
    [NotMapped]
    [JsonConverter(typeof(OneOfConverter))]
    public OneOfCourse? OneOfCourse
    {
        get
        {
            if (CourseId == null)
            {
                return null;
            }

            return new OneOfCourseInstance(CourseId, Course);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public Guid? CourseId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public Course? Course { get; set; }

    /// <summary>
    /// The organization which provides this component. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
    /// </summary>
    /// <value>The organization which provides this component. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

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
    /// 
    /// </summary>
    [JsonIgnore]
    public Guid? OrganizationId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public Organization? Organization { get; set; }

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
    public List<Consumer> Consumers { get; set; } = default!;

    /// <summary>
    /// Collection of addresses
    /// </summary>
    [JsonIgnore]
    public virtual ICollection<Address> Address { get; set; } = default!;
}
