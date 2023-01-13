using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using ooapi.v5.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Course : ModelBase
    {

        /// <summary>
        /// Unique id of this course
        /// </summary>
        /// <value>Unique id of this course</value>
        [JsonRequired]
        [JsonProperty("courseId")]
        public Guid CourseId { get; set; }

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
        /// The name of this course (ECTS-title)
        /// </summary>
        /// <value>The name of this course (ECTS-title)</value>
        [JsonRequired]

        [JsonProperty(PropertyName = "name")]
        [NotMapped]
        public List<LanguageTypedString> name
        {
            get
            {
                return Helpers.JsonConverter.GetLanguageTypesStringList(Name);
            }
            set
            {
                if (value != null)
                    Name = JsonConvert.SerializeObject(value);
            }
        }


        [JsonIgnore]
        public string Name { get; set; }



        /// <summary>
        /// The abbreviation or internal code used to identify this course (ECTS-code)
        /// </summary>
        /// <value>The abbreviation or internal code used to identify this course (ECTS-code)</value>
        [JsonRequired]

        [MaxLength(256)]
        [JsonProperty(PropertyName = "abbreviation")]
        public string Abbreviation { get; set; }

        /// <summary>
        /// Gets or Sets StudyLoad
        /// </summary>
        [JsonProperty(PropertyName = "studyLoad")]
        [NotMapped]
        public StudyLoadDescriptor? StudyLoad
        {
            get
            {
                if (string.IsNullOrEmpty(StudyLoadUnit) || StudyLoadValue == 0)
                    return null;
                try
                {

                    StudyLoadDescriptor result = new StudyLoadDescriptor();
                    result.StudyLoadUnit = StudyLoadUnit;
                    result.Value = StudyLoadValue;
                    return result;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            set
            {
                StudyLoadUnit = value.StudyLoadUnit;
                StudyLoadValue = value.Value;
            }
        }

        [JsonIgnore]
        public string? StudyLoadUnit { get; set; }

        [JsonIgnore]
        public int StudyLoadValue { get; set; }

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
                                    list.Add(ModeOfDeliveryEnum.DistanceLearningEnum);
                                    break;
                                }
                            case "on campus":
                                {
                                    list.Add(ModeOfDeliveryEnum.OnCampusEnum);
                                    break;
                                }
                            case "online":
                                {
                                    list.Add(ModeOfDeliveryEnum.OnlineEnum);
                                    break;
                                }
                            case "hybrid":
                                {
                                    list.Add(ModeOfDeliveryEnum.HybridEnum);
                                    break;
                                }
                            case "situated":
                                {
                                    list.Add(ModeOfDeliveryEnum.SituatedEnum);
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
        /// The duration of this course. The duration format is from the ISO 8601 ABNF as given in Appendix A of RFC 3339.
        /// </summary>
        /// <value>The duration of this course. The duration format is from the ISO 8601 ABNF as given in Appendix A of RFC 3339.</value>
        [RegularExpression("/^(-?)P(?=\\d|T\\d)(?:(\\d+)Y)?(?:(\\d+)M)?(?:(\\d+)([DW]))?(?:T(?:(\\d+)H)?(?:(\\d+)M)?(?:(\\d+(?:\\.\\d+)?)S)?)?$/")]
        [JsonProperty(PropertyName = "duration")]
        public string Duration { get; set; }

        /// <summary>
        /// The date when participants can follow this course for the first time.
        /// </summary>
        /// <value>The date when participants can follow this course for the first time.</value>

        [JsonProperty(PropertyName = "firstStartDate")]
        public DateTime? FirstStartDate { get; set; }

        /// <summary>
        /// The description of this course (ECTS-description).
        /// </summary>
        /// <value>The description of this course (ECTS-description).</value>
        [JsonRequired]
        [JsonProperty(PropertyName = "description")]
        [NotMapped]
        public List<LanguageTypedString> description
        {
            get
            {
                return Helpers.JsonConverter.GetLanguageTypesStringList(Description);
            }
            set
            {
                if (value != null)
                    Description = JsonConvert.SerializeObject(value);
            }
        }

        [JsonIgnore]
        public string Description { get; set; }

        /// <summary>
        /// The (primary) teaching language in which this course is given, should be a three-letter language code as specified by ISO 639-2.
        /// </summary>
        /// <value>The (primary) teaching language in which this course is given, should be a three-letter language code as specified by ISO 639-2.</value>
        [JsonRequired]
        [RegularExpression("/^[a-z]{3}$/")]
        [StringLength(3, MinimumLength = 3)]
        [JsonProperty(PropertyName = "teachingLanguage")]
        public string TeachingLanguage { get; set; }

        /// <summary>
        /// Field(s) of study (e.g. ISCED-F) (http://uis.unesco.org/sites/default/files/documents/isced-fields-of-education-and-training-2013-en.pdf.
        /// </summary>
        /// <value>Field(s) of study (e.g. ISCED-F) (http://uis.unesco.org/sites/default/files/documents/isced-fields-of-education-and-training-2013-en.pdf.</value>

        [MaxLength(4)]
        [JsonProperty(PropertyName = "fieldsOfStudy")]
        public string FieldsOfStudy { get; set; }

        /// <summary>
        /// Statements that describe the knowledge or skills students should acquire by the end of a particular course (ECTS-learningoutcome).
        /// </summary>
        /// <value>Statements that describe the knowledge or skills students should acquire by the end of a particular course (ECTS-learningoutcome).</value>

        [JsonProperty(PropertyName = "learningOutcomes")]
        [NotMapped]
        public List<LearningOutcomes>? learningOutcomes { get; set; }

        [JsonIgnore]
        public string LearningOutcomes { get; set; }

        /// <summary>
        /// This information may be given at an institutional level and/or at the level of individual programmes. Make sure that it is clear whether the information applies to fee-paying students (national and/or international) or to exchange students.
        /// </summary>
        /// <value>This information may be given at an institutional level and/or at the level of individual programmes. Make sure that it is clear whether the information applies to fee-paying students (national and/or international) or to exchange students.</value>

        [JsonProperty(PropertyName = "admissionRequirements")]
        [NotMapped]
        public List<LanguageTypedString>? admissionRequirements
        {
            get
            {
                return Helpers.JsonConverter.GetLanguageTypesStringList(AdmissionRequirements);
            }
            set
            {
                if (value != null)
                    AdmissionRequirements = JsonConvert.SerializeObject(value);
            }
        }

        [JsonIgnore]
        public string AdmissionRequirements { get; set; }


        /// <summary>
        /// Normally, students will receive a diploma when they have completed the (official) study program and have obtained the required number of credits. If there are any other specific requirements that students need to have fulfilled, mention them here.
        /// </summary>
        /// <value>Normally, students will receive a diploma when they have completed the (official) study program and have obtained the required number of credits. If there are any other specific requirements that students need to have fulfilled, mention them here.</value>

        [JsonProperty(PropertyName = "qualificationRequirements")]
        [NotMapped]
        public List<LanguageTypedString> qualificationRequirements { get; set; }

        [JsonIgnore]
        public string QualificationRequirements { get; set; }

        /// <summary>
        /// The level of this course (ECTS-year of study if applicable) - secondary vocational education: mbo - secondary vocational education 1: mbo 1, corresponds to levelOfQualification 1 - secondary vocational education 2: mbo 2, corresponds to levelOfQualification 2 - secondary vocational education 3: mbo 3, corresponds to levelOfQualification 3 - secondary vocational education 4: mbo 4, corresponds to levelOfQualification 4 - associate degree: associate degree, corresponds to levelOfQualification 5 - bachelor: bachelor, corresponds to levelOfQualification 6 - master: master, corresponds to levelOfQualification 7 - doctoral: doctoraal, corresponds to levelOfQualification 8 - undefined: onbepaald - undivided: ongedeeld - nt2-1: NT2 niveau 1 - nt2-2: NT2 niveau 2 
        /// </summary>
        /// <value>The level of this course (ECTS-year of study if applicable) - secondary vocational education: mbo - secondary vocational education 1: mbo 1, corresponds to levelOfQualification 1 - secondary vocational education 2: mbo 2, corresponds to levelOfQualification 2 - secondary vocational education 3: mbo 3, corresponds to levelOfQualification 3 - secondary vocational education 4: mbo 4, corresponds to levelOfQualification 4 - associate degree: associate degree, corresponds to levelOfQualification 5 - bachelor: bachelor, corresponds to levelOfQualification 6 - master: master, corresponds to levelOfQualification 7 - doctoral: doctoraal, corresponds to levelOfQualification 8 - undefined: onbepaald - undivided: ongedeeld - nt2-1: NT2 niveau 1 - nt2-2: NT2 niveau 2 </value>
        [JsonRequired]

        [JsonProperty(PropertyName = "level")]
        public LevelEnum? Level { get; set; }

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
                return Helpers.JsonConverter.GetLanguageTypesStringList(Enrollment);
            }
            set
            {
                if (value != null)
                    Enrollment = JsonConvert.SerializeObject(value);
            }
        }

        [JsonIgnore]
        public string Enrollment { get; set; }

        [JsonIgnore]
        public string Resources { get; set; }

        /// <summary>
        /// An overview of the literature and other resources that is used in this course (ECTS-recommended reading and other sources)
        /// </summary>
        /// <value>An overview of the literature and other resources that is used in this course (ECTS-recommended reading and other sources)</value>

        [DataMember(Name = "resources")]
        [NotMapped]
        public List<string> resources { get; set; }


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
                return Helpers.JsonConverter.GetLanguageTypesStringList(Assessment);
            }
            set
            {
                if (value != null)
                    Assessment = JsonConvert.SerializeObject(value);
            }
        }

        [JsonIgnore]
        public string Assessment { get; set; }


        /// <summary>
        /// URL of the course&#x27;s website
        /// </summary>
        /// <value>URL of the course&#x27;s website</value>

        [MaxLength(2048)]
        [JsonProperty(PropertyName = "link")]
        public string Link { get; set; }

        /// <summary>
        /// The educationSpecification of which this course is a more concrete implementation. [&#x60;expandable&#x60;](#tag/education_specification_model)
        /// </summary>
        /// <value>The educationSpecification of which this course is a more concrete implementation. [&#x60;expandable&#x60;](#tag/education_specification_model)</value>

        [JsonProperty(PropertyName = "educationSpecification")]
        public OneOfEducationSpecification EducationSpecification { get; set; }


        [JsonIgnore]
        public Guid? EducationSpecificationId { get; set; }

        /// <summary>
        /// Addresses for this course
        /// </summary>
        /// <value>Addresses for this course</value>
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
        /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
        /// </summary>
        /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>

        [JsonProperty("consumers")]
        [NotMapped]
        public List<dynamic>? Consumers { get; set; }

        /// <summary>
        /// The program of which this course is a part of. This object is [&#x60;expandable&#x60;](#tag/program_model)
        /// </summary>
        /// <value>The program of which this course is a part of. This object is [&#x60;expandable&#x60;](#tag/program_model)</value>

        [JsonProperty(PropertyName = "programs")]
        public List<OneOfProgram> Programs { get; set; }

        /// <summary>
        /// The person(s) responsible for this course. This object is [&#x60;expandable&#x60;](#tag/person_model)
        /// </summary>
        /// <value>The person(s) responsible for this course. This object is [&#x60;expandable&#x60;](#tag/person_model)</value>

        [JsonProperty(PropertyName = "coordinators")]
        public List<OneOfPerson> Coordinators { get; set; }

        /// <summary>
        /// The organization that manages this group. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
        /// </summary>
        /// <value>The organization that manages this group. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

        [JsonProperty(PropertyName = "organization")]
        public OneOfOrganization Organization { get; set; }

        [JsonIgnore]
        public Guid? OrganizationId { get; set; }


        /// <summary>
        /// The first day this course is valid (inclusive).
        /// </summary>
        /// <value>The first day this course is valid (inclusive).</value>

        [JsonProperty(PropertyName = "validFrom")]
        public DateTime? ValidFrom { get; set; }

        /// <summary>
        /// The day this course ceases to be valid (e.g. exclusive).
        /// </summary>
        /// <value>The day this course ceases to be valid (e.g. exclusive).</value>

        [JsonProperty(PropertyName = "validTo")]
        public DateTime? ValidTo { get; set; }


        [JsonIgnore]
        public virtual ICollection<Program> ProgramsRef { get; set; }

        [JsonIgnore]
        public virtual ICollection<Person> CoordinatorsRef { get; set; }


        [JsonIgnore]
        public virtual ICollection<Address> Address { get; set; }


    }
}
