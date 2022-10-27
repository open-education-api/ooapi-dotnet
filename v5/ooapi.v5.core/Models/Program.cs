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
    public partial class Program : ModelBase
    {
        /// <summary>
        /// Unique id of this program
        /// </summary>
        /// <value>Unique id of this program</value>
        [JsonRequired]
        [JsonProperty("programId")]
        public Guid ProgramId { get; set; }

        /// <summary>
        /// Gets or Sets PrimaryCode
        /// </summary>
        [JsonRequired]

        [DataMember(Name = "primaryCode")]
        public PrimaryCode PrimaryCode { get; set; }



        /// <summary>
        /// The type of this program - program: opleiding - minor: minor - honours: honours - specialization: specialisatie - track: track 
        /// </summary>
        /// <value>The type of this program - program: opleiding - minor: minor - honours: honours - specialization: specialisatie - track: track </value>
        [JsonRequired]

        [DataMember(Name = "programType")]
        public ProgramTypeEnum? ProgramType { get; set; }

        /// <summary>
        /// The name of this program
        /// </summary>
        /// <value>The name of this program</value>
        [JsonRequired]

        [DataMember(Name = "name")]
        public List<LanguageTypedString> Name { get; set; }

        /// <summary>
        /// The abbreviation of this program
        /// </summary>
        /// <value>The abbreviation of this program</value>
        [JsonRequired]

        [MaxLength(256)]
        [DataMember(Name = "abbreviation")]
        public string Abbreviation { get; set; }

        /// <summary>
        /// The description of this program
        /// </summary>
        /// <value>The description of this program</value>
        [JsonRequired]

        [DataMember(Name = "description")]
        public List<LanguageTypedString> Description { get; set; }

        /// <summary>
        /// The (primary) teaching language in which this program is given, should be a three-letter language code as specified by ISO 639-2.
        /// </summary>
        /// <value>The (primary) teaching language in which this program is given, should be a three-letter language code as specified by ISO 639-2.</value>
        [JsonRequired]
        [RegularExpression("/^[a-z]{3}$/")]
        [StringLength(3, MinimumLength = 3)]
        [DataMember(Name = "teachingLanguage")]
        public string TeachingLanguage { get; set; }

        /// <summary>
        /// Gets or Sets StudyLoad
        /// </summary>

        [DataMember(Name = "studyLoad")]
        public ProgramResultStudyLoad StudyLoad { get; set; }



        /// <summary>
        /// Type of qualificaton that can be obtained on finishing the program
        /// </summary>
        /// <value>Type of qualificaton that can be obtained on finishing the program</value>

        [DataMember(Name = "qualificationAwarded")]
        public QualificationAwardedEnum? QualificationAwarded { get; set; }


        /// <summary>
        /// Indicates whether the education is full-time, part-time, dual or self-paced.   - full-time: fulltime   - part-time: parttime   - dual training: duaal   - self-paced: eigen tempo 
        /// </summary>
        /// <value>Indicates whether the education is full-time, part-time, dual or self-paced.   - full-time: fulltime   - part-time: parttime   - dual training: duaal   - self-paced: eigen tempo </value>

        [DataMember(Name = "modeOfStudy")]
        public ModeOfStudyEnum? ModeOfStudy { get; set; }



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
        /// The duration of this program. The duration format is from the ISO 8601 ABNF as given in Appendix A of RFC 3339.
        /// </summary>
        /// <value>The duration of this program. The duration format is from the ISO 8601 ABNF as given in Appendix A of RFC 3339.</value>
        [RegularExpression("/^(-?)P(?=\\d|T\\d)(?:(\\d+)Y)?(?:(\\d+)M)?(?:(\\d+)([DW]))?(?:T(?:(\\d+)H)?(?:(\\d+)M)?(?:(\\d+(?:\\.\\d+)?)S)?)?$/")]
        [DataMember(Name = "duration")]
        public string Duration { get; set; }

        /// <summary>
        /// The date when participants can follow this program for the first time.
        /// </summary>
        /// <value>The date when participants can follow this program for the first time.</value>

        [DataMember(Name = "firstStartDate")]
        public DateTime? FirstStartDate { get; set; }


        /// <summary>
        /// Level of qualification according to the Dutch National Qualification Framework and the European Qualifications Framework, see [this overview](https://nlqf.nl/images/downloads/English2018/Schematic_overview_NLQF_2020.pdf) for more information.
        /// </summary>
        /// <value>Level of qualification according to the Dutch National Qualification Framework and the European Qualifications Framework, see [this overview](https://nlqf.nl/images/downloads/English2018/Schematic_overview_NLQF_2020.pdf) for more information.</value>

        [DataMember(Name = "levelOfQualification")]
        public LevelOfQualificationEnum? LevelOfQualification { get; set; }



        /// <summary>
        /// The level of this course (ECTS-year of study if applicable) - secondary vocational education: mbo - secondary vocational education 1: mbo 1, corresponds to levelOfQualification 1 - secondary vocational education 2: mbo 2, corresponds to levelOfQualification 2 - secondary vocational education 3: mbo 3, corresponds to levelOfQualification 3 - secondary vocational education 4: mbo 4, corresponds to levelOfQualification 4 - associate degree: associate degree, corresponds to levelOfQualification 5 - bachelor: bachelor, corresponds to levelOfQualification 6 - master: master, corresponds to levelOfQualification 7 - doctoral: doctoraal, corresponds to levelOfQualification 8 - undefined: onbepaald - undivided: ongedeeld - nt2-1: NT2 niveau 1 - nt2-2: NT2 niveau 2 
        /// </summary>
        /// <value>The level of this course (ECTS-year of study if applicable) - secondary vocational education: mbo - secondary vocational education 1: mbo 1, corresponds to levelOfQualification 1 - secondary vocational education 2: mbo 2, corresponds to levelOfQualification 2 - secondary vocational education 3: mbo 3, corresponds to levelOfQualification 3 - secondary vocational education 4: mbo 4, corresponds to levelOfQualification 4 - associate degree: associate degree, corresponds to levelOfQualification 5 - bachelor: bachelor, corresponds to levelOfQualification 6 - master: master, corresponds to levelOfQualification 7 - doctoral: doctoraal, corresponds to levelOfQualification 8 - undefined: onbepaald - undivided: ongedeeld - nt2-1: NT2 niveau 1 - nt2-2: NT2 niveau 2 </value>

        [DataMember(Name = "level")]
        public LevelEnum? Level { get; set; }



        /// <summary>
        /// The sector for this program - secondary vocational education: middelbaar beroepsonderwijs - higher professional education: hoger beroepsonderwijs - university education: universitair onderwijs 
        /// </summary>
        /// <value>The sector for this program - secondary vocational education: middelbaar beroepsonderwijs - higher professional education: hoger beroepsonderwijs - university education: universitair onderwijs </value>

        [DataMember(Name = "sector")]
        public SectorEnum? Sector { get; set; }

        /// <summary>
        /// Field(s) of study (e.g. ISCED-F) (http://uis.unesco.org/sites/default/files/documents/isced-fields-of-education-and-training-2013-en.pdf.
        /// </summary>
        /// <value>Field(s) of study (e.g. ISCED-F) (http://uis.unesco.org/sites/default/files/documents/isced-fields-of-education-and-training-2013-en.pdf.</value>

        [MaxLength(4)]
        [DataMember(Name = "fieldsOfStudy")]
        public string FieldsOfStudy { get; set; }

        /// <summary>
        /// The extra information that is provided for enrollment
        /// </summary>
        /// <value>The extra information that is provided for enrollment</value>

        [DataMember(Name = "enrollment")]
        public List<LanguageTypedString> Enrollment { get; set; }

        //[JsonIgnore]
        //public List<Resource> Resources { get; set; }

        ///// <summary>
        ///// An overview of the literature and other resources that is used in this course (ECTS-recommended reading and other sources)
        ///// </summary>
        ///// <value>An overview of the literature and other resources that is used in this course (ECTS-recommended reading and other sources)</value>

        //[DataMember(Name = "resources")]
        //[NotMapped]
        //public List<string> resources { get; set; }

        /// <summary>
        /// List of learning outcomes at program level. It is advisable to limit the number of learning outcomes to approximately 20. It is also advisable to make sure that the program learning outcomes in the course catalogue correspond with those on the Diploma Supplement.
        /// </summary>
        /// <value>List of learning outcomes at program level. It is advisable to limit the number of learning outcomes to approximately 20. It is also advisable to make sure that the program learning outcomes in the course catalogue correspond with those on the Diploma Supplement.</value>

        [DataMember(Name = "learningOutcomes")]
        [NotMapped]
        public List<List<LanguageTypedString>> LearningOutcomes { get; set; }

        /// <summary>
        /// A description of the way exams for this course are taken (ECTS-assessment method and criteria).
        /// </summary>
        /// <value>A description of the way exams for this course are taken (ECTS-assessment method and criteria).</value>

        [DataMember(Name = "assessment")]
        public List<LanguageTypedString> Assessment { get; set; }

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
        /// URL of the program&#x27;s website
        /// </summary>
        /// <value>URL of the program&#x27;s website</value>

        [MaxLength(2048)]
        [DataMember(Name = "link")]
        public string Link { get; set; }

        /// <summary>
        /// The educationSpecification of which this program is a more concrete implementation. [&#x60;expandable&#x60;](#tag/education_specification_model)
        /// </summary>
        /// <value>The educationSpecification of which this program is a more concrete implementation. [&#x60;expandable&#x60;](#tag/education_specification_model)</value>

        [DataMember(Name = "educationSpecification")]
        public OneOfEducationSpecification EducationSpecification { get; set; }

        /// <summary>
        /// An array of additional human readable codes/identifiers for the entity being described.
        /// </summary>
        /// <value>An array of additional human readable codes/identifiers for the entity being described.</value>

        [DataMember(Name = "otherCodes")]
        public List<OtherCodes> OtherCodes { get; set; }

        /// <summary>
        /// Addresses for this program
        /// </summary>
        /// <value>Addresses for this program</value>

        [DataMember(Name = "addresses")]
        public List<Address> Addresses { get; set; }

        /// <summary>
        /// Parent program of which the current program is a child. This object is [&#x60;expandable&#x60;](#tag/program_model)
        /// </summary>
        /// <value>Parent program of which the current program is a child. This object is [&#x60;expandable&#x60;](#tag/program_model)</value>

        [DataMember(Name = "parent")]
        public OneOfProgram Parent { get; set; }

        /// <summary>
        /// Programs which are a part of this program (e.g specializations). This object is [&#x60;expandable&#x60;](#tag/program_model)
        /// </summary>
        /// <value>Programs which are a part of this program (e.g specializations). This object is [&#x60;expandable&#x60;](#tag/program_model)</value>

        [DataMember(Name = "children")]
        public List<OneOfProgram> Children { get; set; }

        /// <summary>
        /// The person(s) responsible for this program. This object is [&#x60;expandable&#x60;](#tag/person_model)
        /// </summary>
        /// <value>The person(s) responsible for this program. This object is [&#x60;expandable&#x60;](#tag/person_model)</value>

        [DataMember(Name = "coordinators")]
        public List<OneOfPerson> Coordinators { get; set; }

        /// <summary>
        /// The organization providing this program. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
        /// </summary>
        /// <value>The organization providing this program. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

        [DataMember(Name = "organization")]
        public OneOfOrganization Organization { get; set; }

        /// <summary>
        /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
        /// </summary>
        /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>

        [DataMember(Name = "consumers")]
        public List<Consumer> Consumers { get; set; }


        /// <summary>
        /// The first day this program is valid (inclusive).
        /// </summary>
        /// <value>The first day this program is valid (inclusive).</value>

        [DataMember(Name = "validFrom")]
        public DateTime? ValidFrom { get; set; }

        /// <summary>
        /// The day this program ceases to be valid (e.g. exclusive).
        /// </summary>
        /// <value>The day this program ceases to be valid (e.g. exclusive).</value>

        [DataMember(Name = "validTo")]
        public DateTime? ValidTo { get; set; }

    }
}
