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
        [DataMember(Name = "primaryCode")]
        public PrimaryCode PrimaryCode { get; set; }

        /// <summary>
        /// The name of this course (ECTS-title)
        /// </summary>
        /// <value>The name of this course (ECTS-title)</value>
        [JsonRequired]

        [DataMember(Name = "name")]
        public List<LanguageTypedString> Name { get; set; }

        /// <summary>
        /// The abbreviation or internal code used to identify this course (ECTS-code)
        /// </summary>
        /// <value>The abbreviation or internal code used to identify this course (ECTS-code)</value>
        [JsonRequired]

        [MaxLength(256)]
        [DataMember(Name = "abbreviation")]
        public string Abbreviation { get; set; }

        /// <summary>
        /// Gets or Sets StudyLoad
        /// </summary>

        [DataMember(Name = "studyLoad")]
        public ProgramResultStudyLoad StudyLoad { get; set; }


        [JsonIgnore]
        public string ModeOfDelivery { get; set; }


        /// <summary>
        /// The mode of delivery of the component (ECTS-mode of delivery) - distance-learning: afstandsleren - on campus: op de campus - online: online - hybrid: hybride - situated: op locatie 
        /// </summary>
        /// <value>The mode of delivery of the component (ECTS-mode of delivery) - distance-learning: afstandsleren - on campus: op de campus - online: online - hybrid: hybride - situated: op locatie </value>

        [DataMember(Name = "modeOfDelivery")]
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
        [DataMember(Name = "duration")]
        public string Duration { get; set; }

        /// <summary>
        /// The date when participants can follow this course for the first time.
        /// </summary>
        /// <value>The date when participants can follow this course for the first time.</value>

        [DataMember(Name = "firstStartDate")]
        public DateTime? FirstStartDate { get; set; }

        /// <summary>
        /// The description of this course (ECTS-description).
        /// </summary>
        /// <value>The description of this course (ECTS-description).</value>
        [JsonRequired]

        [DataMember(Name = "description")]
        public List<LanguageTypedString> Description { get; set; }

        /// <summary>
        /// The (primary) teaching language in which this course is given, should be a three-letter language code as specified by ISO 639-2.
        /// </summary>
        /// <value>The (primary) teaching language in which this course is given, should be a three-letter language code as specified by ISO 639-2.</value>
        [JsonRequired]
        [RegularExpression("/^[a-z]{3}$/")]
        [StringLength(3, MinimumLength = 3)]
        [DataMember(Name = "teachingLanguage")]
        public string TeachingLanguage { get; set; }

        /// <summary>
        /// Field(s) of study (e.g. ISCED-F) (http://uis.unesco.org/sites/default/files/documents/isced-fields-of-education-and-training-2013-en.pdf.
        /// </summary>
        /// <value>Field(s) of study (e.g. ISCED-F) (http://uis.unesco.org/sites/default/files/documents/isced-fields-of-education-and-training-2013-en.pdf.</value>

        [MaxLength(4)]
        [DataMember(Name = "fieldsOfStudy")]
        public string FieldsOfStudy { get; set; }

        /// <summary>
        /// Statements that describe the knowledge or skills students should acquire by the end of a particular course (ECTS-learningoutcome).
        /// </summary>
        /// <value>Statements that describe the knowledge or skills students should acquire by the end of a particular course (ECTS-learningoutcome).</value>

        [DataMember(Name = "learningOutcomes")]
        [NotMapped]
        public List<List<LanguageTypedString>> LearningOutcomes { get; set; }

        /// <summary>
        /// This information may be given at an institutional level and/or at the level of individual programmes. Make sure that it is clear whether the information applies to fee-paying students (national and/or international) or to exchange students.
        /// </summary>
        /// <value>This information may be given at an institutional level and/or at the level of individual programmes. Make sure that it is clear whether the information applies to fee-paying students (national and/or international) or to exchange students.</value>

        [DataMember(Name = "admissionRequirements")]
        public List<LanguageTypedString> AdmissionRequirements { get; set; }

        /// <summary>
        /// Normally, students will receive a diploma when they have completed the (official) study program and have obtained the required number of credits. If there are any other specific requirements that students need to have fulfilled, mention them here.
        /// </summary>
        /// <value>Normally, students will receive a diploma when they have completed the (official) study program and have obtained the required number of credits. If there are any other specific requirements that students need to have fulfilled, mention them here.</value>

        [DataMember(Name = "qualificationRequirements")]
        public List<LanguageTypedString> QualificationRequirements { get; set; }

        /// <summary>
        /// The level of this course (ECTS-year of study if applicable) - secondary vocational education: mbo - secondary vocational education 1: mbo 1, corresponds to levelOfQualification 1 - secondary vocational education 2: mbo 2, corresponds to levelOfQualification 2 - secondary vocational education 3: mbo 3, corresponds to levelOfQualification 3 - secondary vocational education 4: mbo 4, corresponds to levelOfQualification 4 - associate degree: associate degree, corresponds to levelOfQualification 5 - bachelor: bachelor, corresponds to levelOfQualification 6 - master: master, corresponds to levelOfQualification 7 - doctoral: doctoraal, corresponds to levelOfQualification 8 - undefined: onbepaald - undivided: ongedeeld - nt2-1: NT2 niveau 1 - nt2-2: NT2 niveau 2 
        /// </summary>
        /// <value>The level of this course (ECTS-year of study if applicable) - secondary vocational education: mbo - secondary vocational education 1: mbo 1, corresponds to levelOfQualification 1 - secondary vocational education 2: mbo 2, corresponds to levelOfQualification 2 - secondary vocational education 3: mbo 3, corresponds to levelOfQualification 3 - secondary vocational education 4: mbo 4, corresponds to levelOfQualification 4 - associate degree: associate degree, corresponds to levelOfQualification 5 - bachelor: bachelor, corresponds to levelOfQualification 6 - master: master, corresponds to levelOfQualification 7 - doctoral: doctoraal, corresponds to levelOfQualification 8 - undefined: onbepaald - undivided: ongedeeld - nt2-1: NT2 niveau 1 - nt2-2: NT2 niveau 2 </value>
        [JsonRequired]

        [DataMember(Name = "level")]
        public LevelEnum? Level { get; set; }

        /// <summary>
        /// The extra information that is provided for enrollment
        /// </summary>
        /// <value>The extra information that is provided for enrollment</value>

        [DataMember(Name = "enrollment")]
        public List<LanguageTypedString> Enrollment { get; set; }

        [JsonIgnore]
        public List<Resource> Resources { get; set; }

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

        [DataMember(Name = "assessment")]
        public List<LanguageTypedString> Assessment { get; set; }

        /// <summary>
        /// URL of the course&#x27;s website
        /// </summary>
        /// <value>URL of the course&#x27;s website</value>

        [MaxLength(2048)]
        [DataMember(Name = "link")]
        public string Link { get; set; }

        /// <summary>
        /// The educationSpecification of which this course is a more concrete implementation. [&#x60;expandable&#x60;](#tag/education_specification_model)
        /// </summary>
        /// <value>The educationSpecification of which this course is a more concrete implementation. [&#x60;expandable&#x60;](#tag/education_specification_model)</value>

        [DataMember(Name = "educationSpecification")]
        public OneOfEducationSpecification EducationSpecification { get; set; }

        /// <summary>
        /// Addresses for this course
        /// </summary>
        /// <value>Addresses for this course</value>

        [DataMember(Name = "addresses")]
        public List<Address> Addresses { get; set; }

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
        /// The program of which this course is a part of. This object is [&#x60;expandable&#x60;](#tag/program_model)
        /// </summary>
        /// <value>The program of which this course is a part of. This object is [&#x60;expandable&#x60;](#tag/program_model)</value>

        [DataMember(Name = "programs")]
        public List<OneOfProgram> Programs { get; set; }

        /// <summary>
        /// The person(s) responsible for this course. This object is [&#x60;expandable&#x60;](#tag/person_model)
        /// </summary>
        /// <value>The person(s) responsible for this course. This object is [&#x60;expandable&#x60;](#tag/person_model)</value>

        [DataMember(Name = "coordinators")]
        public List<OneOfPerson> Coordinators { get; set; }

        /// <summary>
        /// The organization that manages this group. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
        /// </summary>
        /// <value>The organization that manages this group. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

        [DataMember(Name = "organization")]
        public OneOfOrganization Organization { get; set; }

        /// <summary>
        /// The first day this course is valid (inclusive).
        /// </summary>
        /// <value>The first day this course is valid (inclusive).</value>

        [DataMember(Name = "validFrom")]
        public DateTime? ValidFrom { get; set; }

        /// <summary>
        /// The day this course ceases to be valid (e.g. exclusive).
        /// </summary>
        /// <value>The day this course ceases to be valid (e.g. exclusive).</value>

        [DataMember(Name = "validTo")]
        public DateTime? ValidTo { get; set; }


    }
}
