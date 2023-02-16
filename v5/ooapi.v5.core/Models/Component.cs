using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ooapi.v5.Attributes;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Enums;
using ooapi.v5.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
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
        public Guid? ComponentId { get; set; }

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
        /// The component type - test: tentamen - lecture: college - practical: practicum - tutorial: werkcollege - consultation: consultatie - project: project - workshop: workshop - excursion: excursie - independent study: zelfstudie - external: extern - skills training: vaardighedentraining 
        /// </summary>
        /// <value>The component type - test: tentamen - lecture: college - practical: practicum - tutorial: werkcollege - consultation: consultatie - project: project - workshop: workshop - excursion: excursie - independent study: zelfstudie - external: extern - skills training: vaardighedentraining </value>
        [JsonRequired]

        [JsonProperty(PropertyName = "componentType")]
        public ComponentTypeEnum? ComponentType { get; set; }

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
        /// The abbreviation of this component
        /// </summary>
        /// <value>The abbreviation of this component</value>
        [JsonRequired]

        [MaxLength(256)]
        [JsonProperty(PropertyName = "abbreviation")]
        public string Abbreviation { get; set; }



        [JsonIgnore]
        public string ModeOfDelivery { get; set; }


        /// <summary>
        /// The mode of delivery of the component (ECTS-mode of delivery) - distance-learning: afstandsleren - on campus: op de campus - online: online - hybrid: hybride - situated: op locatie 
        /// </summary>
        /// <value>The mode of delivery of the component (ECTS-mode of delivery) - distance-learning: afstandsleren - on campus: op de campus - online: online - hybrid: hybride - situated: op locatie </value>

        [JsonProperty(PropertyName = "modeOfDelivery")]
        [NotMapped]
        public List<ModeOfDeliveryEnum> ModeOfDel
        {
            get
            {
                if (!string.IsNullOrEmpty(ModeOfDelivery))
                {
                    List<ModeOfDeliveryEnum> list = new List<ModeOfDeliveryEnum>();
                    var affiliations = ModeOfDelivery.Split(',');
                    foreach (var affiliation in affiliations)
                    {
                        switch (affiliation)
                        {
                            case "distance-learning":
                                {
                                    list.Add(ModeOfDeliveryEnum.distance_learning);
                                    break;
                                }
                            case "on campus":
                                {
                                    list.Add(ModeOfDeliveryEnum.on_campus);
                                    break;
                                }
                            case "online":
                                {
                                    list.Add(ModeOfDeliveryEnum.online);
                                    break;
                                }
                            case "hybrid":
                                {
                                    list.Add(ModeOfDeliveryEnum.hybrid);
                                    break;
                                }
                            case "situated":
                                {
                                    list.Add(ModeOfDeliveryEnum.situated);
                                    break;
                                }
                            default:
                                break;
                        }
                    }
                    return list;
                }
                return new List<ModeOfDeliveryEnum>();
            }
        }

        /// <summary>
        /// The duration of this component. The duration format is from the ISO 8601 ABNF as given in Appendix A of RFC 3339.
        /// </summary>
        /// <value>The duration of this component. The duration format is from the ISO 8601 ABNF as given in Appendix A of RFC 3339.</value>
        [RegularExpression("/^(-?)P(?=\\d|T\\d)(?:(\\d+)Y)?(?:(\\d+)M)?(?:(\\d+)([DW]))?(?:T(?:(\\d+)H)?(?:(\\d+)M)?(?:(\\d+(?:\\.\\d+)?)S)?)?$/")]
        [JsonProperty(PropertyName = "duration")]
        public string Duration { get; set; }

        /// <summary>
        /// The description of this component.
        /// </summary>
        /// <value>The description of this component.</value>
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
        /// The (primary) teaching language in which this component is given, should be a three-letter language code as specified by ISO 639-2.
        /// </summary>
        /// <value>The (primary) teaching language in which this component is given, should be a three-letter language code as specified by ISO 639-2.</value>
        [JsonRequired]
        [RegularExpression("/^[a-z]{3}$/")]
        [StringLength(3, MinimumLength = 3)]
        [JsonProperty(PropertyName = "teachingLanguage")]
        public string TeachingLanguage { get; set; }



        /// <summary>
        /// Statements that describe the knowledge or skills students should acquire by the end of a particular course (ECTS-learningoutcome).
        /// </summary>
        /// <value>Statements that describe the knowledge or skills students should acquire by the end of a particular course (ECTS-learningoutcome).</value>

        [JsonProperty(PropertyName = "learningOutcomes")]
        [NotMapped]
        public List<List<LanguageTypedString>> LearningOutcomes { get; set; }

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
                List<LanguageTypedString> result = new List<LanguageTypedString>();
                if (Attributes != null && Attributes.Any())
                {
                    result = Attributes.Where(x => x.PropertyName.Equals("enrollment")).Select(x => new LanguageTypedString() { Language = x.Language, Value = x.Value }).ToList();
                }
                return result;
            }
        }



        ////[JsonIgnore]
        ////public List<string> Resources { get; set; }

        /////// <summary>
        /////// An overview of the literature and other resources that is used in this course (ECTS-recommended reading and other sources)
        /////// </summary>
        /////// <value>An overview of the literature and other resources that is used in this course (ECTS-recommended reading and other sources)</value>

        ////[JsonProperty(PropertyName = "resources")]
        ////[NotMapped]
        ////public List<Resource> resources { get; set; }

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
                List<LanguageTypedString> result = new List<LanguageTypedString>();
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
        public List<Address>? Addresses { get; set; }


        /// <summary>
        /// An array of additional human readable codes/identifiers for the entity being described.
        /// </summary>
        /// <value>An array of additional human readable codes/identifiers for the entity being described.</value>

        [JsonProperty(PropertyName = "otherCodes")]
        public List<OtherCodes> OtherCodes { get; set; }

        /// <summary>
        /// The course of which this component is a part. [&#x60;expandable&#x60;](#tag/course_model) By default only the &#x60;courseId&#x60; (a string) is returned. If the client requested an expansion of &#x60;course&#x60; the full course object should be returned. 
        /// </summary>
        /// <value>The course of which this component is a part. [&#x60;expandable&#x60;](#tag/course_model) By default only the &#x60;courseId&#x60; (a string) is returned. If the client requested an expansion of &#x60;course&#x60; the full course object should be returned. </value>

        [JsonProperty(PropertyName = "course")]
        [NotMapped]
        [JsonConverter(typeof(OneOfConverter))]
        public OneOfCourse OneOfCourse
        {
            get
            {
                if (CourseId == null) return null;
                return new OneOfCourseInstance(CourseId, Course);
            }
        }


        [JsonIgnore]
        public Guid? CourseId { get; set; }

        [JsonIgnore]
        public Course? Course { get; set; }



        /// <summary>
        /// The organization which provides this component. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
        /// </summary>
        /// <value>The organization which provides this component. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

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

        [JsonIgnore]
        public virtual ICollection<Address> Address { get; set; }

    }
}
