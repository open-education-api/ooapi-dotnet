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
    [SortAllowed]
    public Guid ProgramId { get; set; }

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
    [SortAllowed]
    public string? PrimaryCode { get; set; }

    /// <summary>
    /// The type of this program - program: opleiding - minor: minor - honours: honours - specialization: specialisatie - track: track 
    /// </summary>
    /// <value>The type of this program - program: opleiding - minor: minor - honours: honours - specialization: specialisatie - track: track </value>
    [JsonRequired]
    [JsonProperty(PropertyName = "programType")]
    public ProgramType? ProgramType { get; set; }

    /// <summary>
    /// The name of this program
    /// </summary>
    /// <value>The name of this program</value>
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
    /// The abbreviation of this program
    /// </summary>
    /// <value>The abbreviation of this program</value>
    [JsonRequired]
    [MaxLength(256)]
    [JsonProperty(PropertyName = "abbreviation")]
    public string Abbreviation { get; set; } = default!;

    /// <summary>
    /// The description of this program
    /// </summary>
    /// <value>The description of this program</value>
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
    /// The (primary) teaching language in which this program is given, should be a three-letter language code as specified by ISO 639-2.
    /// </summary>
    /// <value>The (primary) teaching language in which this program is given, should be a three-letter language code as specified by ISO 639-2.</value>
    [JsonRequired]
    [RegularExpression("/^[a-z]{3}$/")]
    [StringLength(3, MinimumLength = 3)]
    [JsonProperty(PropertyName = "teachingLanguage")]
    public string TeachingLanguage { get; set; } = default!;

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
            {
                return null;
            }

            try
            {

                var result = new StudyLoadDescriptor
                {
                    StudyLoadUnit = StudyLoadUnit,
                    Value = StudyLoadValue
                };
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        set
        {
            if (value is not null)
            {
                StudyLoadUnit = value.StudyLoadUnit;
                StudyLoadValue = value.Value;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public string? StudyLoadUnit { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public int StudyLoadValue { get; set; }

    /// <summary>
    /// Type of qualificaton that can be obtained on finishing the program
    /// </summary>
    /// <value>Type of qualificaton that can be obtained on finishing the program</value>

    [JsonProperty(PropertyName = "qualificationAwarded")]
    public QualificationAwarded? QualificationAwarded { get; set; }


    /// <summary>
    /// Indicates whether the education is full-time, part-time, dual or self-paced.   - full-time: fulltime   - part-time: parttime   - dual training: duaal   - self-paced: eigen tempo 
    /// </summary>
    /// <value>Indicates whether the education is full-time, part-time, dual or self-paced.   - full-time: fulltime   - part-time: parttime   - dual training: duaal   - self-paced: eigen tempo </value>

    [JsonProperty(PropertyName = "modeOfStudy")]
    public ModeOfStudy? ModeOfStudy { get; set; }


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
    /// The duration of this program. The duration format is from the ISO 8601 ABNF as given in Appendix A of RFC 3339.
    /// </summary>
    /// <value>The duration of this program. The duration format is from the ISO 8601 ABNF as given in Appendix A of RFC 3339.</value>
    [RegularExpression("/^(-?)P(?=\\d|T\\d)(?:(\\d+)Y)?(?:(\\d+)M)?(?:(\\d+)([DW]))?(?:T(?:(\\d+)H)?(?:(\\d+)M)?(?:(\\d+(?:\\.\\d+)?)S)?)?$/")]
    [JsonProperty(PropertyName = "duration")]
    public string? Duration { get; set; }

    /// <summary>
    /// The date when participants can follow this program for the first time.
    /// </summary>
    /// <value>The date when participants can follow this program for the first time.</value>

    [JsonProperty(PropertyName = "firstStartDate")]
    [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
    public DateTime? FirstStartDate { get; set; }

    /// <summary>
    /// Level of qualification according to the Dutch National Qualification Framework and the European Qualifications Framework, see [this overview](https://nlqf.nl/images/downloads/English2018/Schematic_overview_NLQF_2020.pdf) for more information.
    /// </summary>
    /// <value>Level of qualification according to the Dutch National Qualification Framework and the European Qualifications Framework, see [this overview](https://nlqf.nl/images/downloads/English2018/Schematic_overview_NLQF_2020.pdf) for more information.</value>

    [JsonProperty(PropertyName = "levelOfQualification")]
    public LevelOfQualification? LevelOfQualification { get; set; }

    /// <summary>
    /// The level of this course (ECTS-year of study if applicable) - secondary vocational education: mbo - secondary vocational education 1: mbo 1, corresponds to levelOfQualification 1 - secondary vocational education 2: mbo 2, corresponds to levelOfQualification 2 - secondary vocational education 3: mbo 3, corresponds to levelOfQualification 3 - secondary vocational education 4: mbo 4, corresponds to levelOfQualification 4 - associate degree: associate degree, corresponds to levelOfQualification 5 - bachelor: bachelor, corresponds to levelOfQualification 6 - master: master, corresponds to levelOfQualification 7 - doctoral: doctoraal, corresponds to levelOfQualification 8 - undefined: onbepaald - undivided: ongedeeld - nt2-1: NT2 niveau 1 - nt2-2: NT2 niveau 2 
    /// </summary>
    /// <value>The level of this course (ECTS-year of study if applicable) - secondary vocational education: mbo - secondary vocational education 1: mbo 1, corresponds to levelOfQualification 1 - secondary vocational education 2: mbo 2, corresponds to levelOfQualification 2 - secondary vocational education 3: mbo 3, corresponds to levelOfQualification 3 - secondary vocational education 4: mbo 4, corresponds to levelOfQualification 4 - associate degree: associate degree, corresponds to levelOfQualification 5 - bachelor: bachelor, corresponds to levelOfQualification 6 - master: master, corresponds to levelOfQualification 7 - doctoral: doctoraal, corresponds to levelOfQualification 8 - undefined: onbepaald - undivided: ongedeeld - nt2-1: NT2 niveau 1 - nt2-2: NT2 niveau 2 </value>

    [JsonProperty(PropertyName = "level")]
    public Level? Level { get; set; }

    /// <summary>
    /// The sector for this program - secondary vocational education: middelbaar beroepsonderwijs - higher professional education: hoger beroepsonderwijs - university education: universitair onderwijs 
    /// </summary>
    /// <value>The sector for this program - secondary vocational education: middelbaar beroepsonderwijs - higher professional education: hoger beroepsonderwijs - university education: universitair onderwijs </value>

    [JsonProperty(PropertyName = "sector")]
    public ProgramSector? Sector { get; set; }

    /// <summary>
    /// Field(s) of study (e.g. ISCED-F) (http://uis.unesco.org/sites/default/files/documents/isced-fields-of-education-and-training-2013-en.pdf.
    /// </summary>
    /// <value>Field(s) of study (e.g. ISCED-F) (http://uis.unesco.org/sites/default/files/documents/isced-fields-of-education-and-training-2013-en.pdf.</value>

    [MaxLength(4)]
    [JsonProperty(PropertyName = "fieldsOfStudy")]
    public string? FieldsOfStudy { get; set; }

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
    /// List of learning outcomes at program level. It is advisable to limit the number of learning outcomes to approximately 20. It is also advisable to make sure that the program learning outcomes in the course catalogue correspond with those on the Diploma Supplement.
    /// </summary>
    /// <value>List of learning outcomes at program level. It is advisable to limit the number of learning outcomes to approximately 20. It is also advisable to make sure that the program learning outcomes in the course catalogue correspond with those on the Diploma Supplement.</value>

    [JsonProperty(PropertyName = "learningOutcomes")]
    [NotMapped]
    public List<List<LanguageTypedString>> LearningOutcomes { get; set; } = default!;

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
    /// This information may be given at an institutional level and/or at the level of individual programmes. Make sure that it is clear whether the information applies to fee-paying students (national and/or international) or to exchange students.
    /// </summary>
    /// <value>This information may be given at an institutional level and/or at the level of individual programmes. Make sure that it is clear whether the information applies to fee-paying students (national and/or international) or to exchange students.</value>

    [JsonProperty(PropertyName = "admissionRequirements")]
    [NotMapped]
    public List<LanguageTypedString> admissionRequirements
    {
        get
        {
            var result = new List<LanguageTypedString>();
            if (Attributes != null && Attributes.Any())
            {
                result = Attributes.Where(x => x.PropertyName.Equals("admissionRequirements")).Select(x => new LanguageTypedString() { Language = x.Language, Value = x.Value }).ToList();
            }
            return result;
        }
    }

    /// <summary>
    /// Normally, students will receive a diploma when they have completed the (official) study program and have obtained the required number of credits. If there are any other specific requirements that students need to have fulfilled, mention them here.
    /// </summary>
    /// <value>Normally, students will receive a diploma when they have completed the (official) study program and have obtained the required number of credits. If there are any other specific requirements that students need to have fulfilled, mention them here.</value>

    [JsonProperty(PropertyName = "qualificationRequirements")]
    [NotMapped]
    public List<LanguageTypedString> qualificationRequirements
    {
        get
        {
            var result = new List<LanguageTypedString>();
            if (Attributes != null && Attributes.Any())
            {
                result = Attributes.Where(x => x.PropertyName.Equals("qualificationRequirements")).Select(x => new LanguageTypedString() { Language = x.Language, Value = x.Value }).ToList();
            }
            return result;
        }
    }

    /// <summary>
    /// URL of the program&#x27;s website
    /// </summary>
    /// <value>URL of the program&#x27;s website</value>
    [MaxLength(2048)]
    [JsonProperty(PropertyName = "link")]
    public string? Link { get; set; }

    /// <summary>
    /// The educationSpecification of which this program is a more concrete implementation. [&#x60;expandable&#x60;](#tag/education_specification_model)
    /// </summary>
    /// <value>The educationSpecification of which this program is a more concrete implementation. [&#x60;expandable&#x60;](#tag/education_specification_model)</value>
    [JsonProperty(PropertyName = "educationSpecification")]
    [NotMapped]
    [JsonConverter(typeof(OneOfConverter))]
    public OneOfEducationSpecification? OneOfEducationSpecification
    {
        get
        {
            if (EducationSpecificationId == null)
            {
                return null;
            }

            return new OneOfEducationSpecificationInstance(EducationSpecificationId, EducationSpecification);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public Guid? EducationSpecificationId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public EducationSpecification? EducationSpecification { get; set; }

    /// <summary>
    /// An array of additional human readable codes/identifiers for the entity being described.
    /// </summary>
    /// <value>An array of additional human readable codes/identifiers for the entity being described.</value>
    [JsonProperty(PropertyName = "otherCodes")]
    public List<OtherCodes> OtherCodes { get; set; } = default!;

    /// <summary>
    /// Addresses for this program
    /// </summary>
    /// <value>Addresses for this program</value>
    /// <summary>
    /// Addresses for this offering
    /// </summary>
    /// <value>Addresses for this offering</value>
    [JsonProperty(PropertyName = "addresses")]
    [NotMapped]
    public List<Address> Addresses { get; set; } = default!;

    /// <summary>
    /// Parent program of which the current program is a child. This object is [&#x60;expandable&#x60;](#tag/program_model)
    /// </summary>
    /// <value>Parent program of which the current program is a child. This object is [&#x60;expandable&#x60;](#tag/program_model)</value>
    [JsonProperty(PropertyName = "parent")]
    [NotMapped]
    [JsonConverter(typeof(OneOfConverter))]
    public OneOfProgram? OneOfParent
    {
        get
        {
            if (ParentId == null)
            {
                return null;
            }

            return new OneOfProgramInstance(ParentId, Parent);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    [NotMapped]
    public Program? Parent { get; set; }

    /// <summary>
    /// Programs which are a part of this program (e.g specializations). This object is [&#x60;expandable&#x60;](#tag/program_model)
    /// </summary>
    /// <value>Programs which are a part of this program (e.g specializations). This object is [&#x60;expandable&#x60;](#tag/program_model)</value>
    [JsonProperty("children")]
    [NotMapped]
    [JsonConverter(typeof(OneOfConverter))]
    public List<OneOfProgram> ChildrenList
    {
        get
        {
            if (ChildrenIds == null || !ChildrenIds.Any())
            {
                return new List<OneOfProgram>();
            }

            var result = new List<OneOfProgram>();
            foreach (var ChildId in ChildrenIds)
            {
                result.Add(new OneOfProgramInstance(ChildId, Children?.Find(x => x.ProgramId.Equals(ChildId))));
            }
            return result;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    [NotMapped]
    public List<Guid> ChildrenIds { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    [NotMapped]
    public List<Program> Children { get; set; } = default!;

    /// <summary>
    /// The person(s) responsible for this program. This object is [&#x60;expandable&#x60;](#tag/person_model)
    /// </summary>
    /// <value>The person(s) responsible for this program. This object is [&#x60;expandable&#x60;](#tag/person_model)</value>
    [JsonProperty(PropertyName = "coordinators")]
    public List<OneOfPerson> Coordinators { get; set; } = default!;

    /// <summary>
    /// The organization providing this program. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
    /// </summary>
    /// <value>The organization providing this program. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

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
    /// 
    /// </summary>
    [JsonIgnore]
    public List<Consumer> Consumers { get; set; } = default!;

    /// <summary>
    /// The first day this program is valid (inclusive).
    /// </summary>
    /// <value>The first day this program is valid (inclusive).</value>

    [JsonProperty(PropertyName = "validFrom")]
    [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
    public DateTime? ValidFrom { get; set; }

    /// <summary>
    /// The day this program ceases to be valid (e.g. exclusive).
    /// </summary>
    /// <value>The day this program ceases to be valid (e.g. exclusive).</value>

    [JsonProperty(PropertyName = "validTo")]
    [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
    public DateTime? ValidTo { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public virtual ICollection<Person> CoordinatorsRef { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public virtual ICollection<Address> Address { get; set; } = default!;
}
