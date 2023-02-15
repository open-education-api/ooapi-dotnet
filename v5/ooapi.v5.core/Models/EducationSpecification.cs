using Newtonsoft.Json;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Enums;
using ooapi.v5.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class EducationSpecification : ModelBase
    {

        /// <summary>
        /// Unique id of this educationSpecification
        /// </summary>
        /// <value>Unique id of this educationSpecification</value>
        [JsonRequired]
        [JsonProperty("educationSpecificationId")]
        public Guid EducationSpecificationId { get; set; }


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
        /// An array of additional human readable codes/identifiers for the entity being described.
        /// </summary>
        /// <value>An array of additional human readable codes/identifiers for the entity being described.</value>

        [JsonProperty(PropertyName = "otherCodes")]
        public List<OtherCodes>? OtherCodes { get; set; }

        /// <summary>
        /// The type of education specification   - program: HOOPLEIDING   - privateProgram: PARTICULIEREOPLEIDING   - programCluster: HOONDERWIJSEENHEDENCLUSTER   - course: HOONDERWIJSEENHEID 
        /// </summary>
        /// <value>The type of education specification   - program: HOOPLEIDING   - privateProgram: PARTICULIEREOPLEIDING   - programCluster: HOONDERWIJSEENHEDENCLUSTER   - course: HOONDERWIJSEENHEID </value>
        //[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]


        /// <summary>
        /// The type of education specification   - program: HOOPLEIDING   - privateProgram: PARTICULIEREOPLEIDING   - programCluster: HOONDERWIJSEENHEDENCLUSTER   - course: HOONDERWIJSEENHEID 
        /// </summary>
        /// <value>The type of education specification   - program: HOOPLEIDING   - privateProgram: PARTICULIEREOPLEIDING   - programCluster: HOONDERWIJSEENHEDENCLUSTER   - course: HOONDERWIJSEENHEID </value>
        [JsonRequired]

        [JsonProperty(PropertyName = "educationSpecificationType")]
        public EducationSpecificationTypeEnum? EducationSpecificationType { get; set; }

        /// <summary>
        /// The name of this education specification
        /// </summary>
        /// <value>The name of this education specification</value>
        [JsonRequired]
        [JsonProperty("name")]
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
        public List<Attribute> Attributes { get; set; }



        /// <summary>
        /// The abbreviation of this program
        /// </summary>
        /// <value>The abbreviation of this program</value>

        [MaxLength(256)]
        [JsonProperty(PropertyName = "abbreviation")]
        public string? Abbreviation { get; set; }

        /// <summary>
        /// The description of this program. [The limited implementation of Git Hub Markdown syntax](#tag/formatting-and-displaying-results-from-API) MAY be used for rich text representation.
        /// </summary>
        /// <value>The description of this program. [The limited implementation of Git Hub Markdown syntax](#tag/formatting-and-displaying-results-from-API) MAY be used for rich text representation.</value>
        [JsonProperty(PropertyName = "description")]
        [NotMapped]
        public List<LanguageTypedString>? description
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
        /// The type of formal document obtained after completion of this education   - diploma: DIPLOMA   - certificate: CERTIFICAAT   - no official document: GEEN OFFICIEEL DOCUMENT   - testimonial: GETUIGSCHRIFT   - school advice: SCHOOLADVIES 
        /// </summary>
        /// <value>The type of formal document obtained after completion of this education   - diploma: DIPLOMA   - certificate: CERTIFICAAT   - no official document: GEEN OFFICIEEL DOCUMENT   - testimonial: GETUIGSCHRIFT   - school advice: SCHOOLADVIES </value>

        [JsonProperty(PropertyName = "formalDocument")]
        public FormalDocumentEnum? FormalDocument { get; set; }

        /// <summary>
        /// The level of this course (ECTS-year of study if applicable) - secondary vocational education: mbo - secondary vocational education 1: mbo 1, corresponds to levelOfQualification 1 - secondary vocational education 2: mbo 2, corresponds to levelOfQualification 2 - secondary vocational education 3: mbo 3, corresponds to levelOfQualification 3 - secondary vocational education 4: mbo 4, corresponds to levelOfQualification 4 - associate degree: associate degree, corresponds to levelOfQualification 5 - bachelor: bachelor, corresponds to levelOfQualification 6 - master: master, corresponds to levelOfQualification 7 - doctoral: doctoraal, corresponds to levelOfQualification 8 - undefined: onbepaald - undivided: ongedeeld - nt2-1: NT2 niveau 1 - nt2-2: NT2 niveau 2 
        /// </summary>
        /// <value>The level of this course (ECTS-year of study if applicable) - secondary vocational education: mbo - secondary vocational education 1: mbo 1, corresponds to levelOfQualification 1 - secondary vocational education 2: mbo 2, corresponds to levelOfQualification 2 - secondary vocational education 3: mbo 3, corresponds to levelOfQualification 3 - secondary vocational education 4: mbo 4, corresponds to levelOfQualification 4 - associate degree: associate degree, corresponds to levelOfQualification 5 - bachelor: bachelor, corresponds to levelOfQualification 6 - master: master, corresponds to levelOfQualification 7 - doctoral: doctoraal, corresponds to levelOfQualification 8 - undefined: onbepaald - undivided: ongedeeld - nt2-1: NT2 niveau 1 - nt2-2: NT2 niveau 2 </value>
        [JsonProperty(PropertyName = "level")]
        public LevelEnum? Level { get; set; }


        /// <summary>
        /// The sector for this program - secondary vocational education: middelbaar beroepsonderwijs - higher professional education: hoger beroepsonderwijs - university education: universitair onderwijs 
        /// </summary>
        /// <value>The sector for this program - secondary vocational education: middelbaar beroepsonderwijs - higher professional education: hoger beroepsonderwijs - university education: universitair onderwijs </value>

        [JsonProperty(PropertyName = "sector")]
        public ProgramSectorEnum? Sector { get; set; }



        /// <summary>
        /// Level of qualification according to the Dutch National Qualification Framework and the European Qualifications Framework, see [this overview](https://nlqf.nl/images/downloads/English2018/Schematic_overview_NLQF_2020.pdf) for more information.
        /// </summary>
        /// <value>Level of qualification according to the Dutch National Qualification Framework and the European Qualifications Framework, see [this overview](https://nlqf.nl/images/downloads/English2018/Schematic_overview_NLQF_2020.pdf) for more information.</value>

        [JsonProperty(PropertyName = "levelOfQualification")]
        public LevelOfQualificationEnum? LevelOfQualification { get; set; }

        /// <summary>
        /// Field(s) of study (e.g. ISCED-F) (http://uis.unesco.org/sites/default/files/documents/isced-fields-of-education-and-training-2013-en.pdf.
        /// </summary>
        /// <value>Field(s) of study (e.g. ISCED-F) (http://uis.unesco.org/sites/default/files/documents/isced-fields-of-education-and-training-2013-en.pdf.</value>

        [MaxLength(4)]
        [JsonProperty(PropertyName = "fieldsOfStudy")]
        public string? FieldsOfStudy { get; set; }

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


        /// <summary>
        /// Statements that describe the knowledge or skills students should acquire by the end of a particular course or program (ECTS-learningoutcome).
        /// </summary>
        /// <value>Statements that describe the knowledge or skills students should acquire by the end of a particular course or program (ECTS-learningoutcome).</value>

        [JsonProperty(PropertyName = "learningOutcomes")]
        [NotMapped]
        public List<List<LanguageTypedString>>? LearningOutcomes { get; set; }

        /// <summary>
        /// URL of the program&#x27;s website
        /// </summary>
        /// <value>URL of the program&#x27;s website</value>

        [MaxLength(2048)]
        [JsonProperty(PropertyName = "link")]
        public string? Link { get; set; }

        /// <summary>
        /// The educationSpecification that is the parent of this educationSpecification if it exists. [&#x60;expandable&#x60;](#tag/education_specification_model)
        /// </summary>
        /// <value>The educationSpecification that is the parent of this educationSpecification if it exists. [&#x60;expandable&#x60;](#tag/education_specification_model)</value>
        [JsonProperty(PropertyName = "parent")]
        [NotMapped]
        [JsonConverter(typeof(OneOfConverter))]
        public OneOfEducationSpecification OneOfParent
        {
            get
            {
                if (ParentId == null) return null;
                return new OneOfEducationSpecificationInstance(ParentId, Parent);
            }
        }

        [JsonIgnore]
        public Guid? ParentId { get; set; }

        [JsonIgnore]
        public EducationSpecification? Parent { get; set; }

        /// <summary>
        /// The EducationSpecifications that have this EducationSpecification as their parent. [&#x60;expandable&#x60;](#tag/education_specification_model)
        /// </summary>
        /// <value>The EducationSpecifications that have this EducationSpecification as their parent. [&#x60;expandable&#x60;](#tag/education_specification_model)</value>

        [JsonProperty(PropertyName = "children")]
        public List<OneOfEducationSpecification>? Children { get; set; }

        /// <summary>
        /// The organization that manages this group. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
        /// </summary>
        /// <value>The organization that manages this group. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

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


        /// <summary>
        /// Gets or Sets PrimaryCode
        /// </summary>
        //[JsonProperty(PropertyName = "consumers")]
        //[NotMapped]
        //public List<Consumer> consumersList
        //{
        //    get
        //    {
        //        return new List<Consumer> { CodeType = PrimaryCodeType, Code = PrimaryCode };
        //    }
        //    set
        //    {
        //        PrimaryCode = value.Code;
        //        PrimaryCodeType = value.CodeType;
        //    }
        //}

        [JsonProperty(PropertyName = "consumers")]
        [NotMapped]
        public List<Consumer>? ConsumersList
        {
            get
            {
                return Consumers;
            }
        }


        [JsonIgnore]
        public List<Consumer> Consumers { get; set; }




        /// <summary>
        /// The first day this EducationSpecification is valid (inclusive).
        /// </summary>
        /// <value>The first day this EducationSpecification is valid (inclusive).</value>

        [JsonProperty(PropertyName = "validFrom")]
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
        public DateTime? ValidFrom { get; set; }

        /// <summary>
        /// The day this EducationSpecification ceases to be valid (e.g. exclusive).
        /// </summary>
        /// <value>The day this EducationSpecification ceases to be valid (e.g. exclusive).</value>

        [JsonProperty(PropertyName = "validTo")]
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
        public DateTime? ValidTo { get; set; }



    }
}
