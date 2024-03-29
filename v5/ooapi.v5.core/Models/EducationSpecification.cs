using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ooapi.v5.Attributes;
using ooapi.v5.core.Extensions;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Enums;
using ooapi.v5.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;

[DataContract]
public partial class EducationSpecification : ModelBase
{
    /// <summary>
    /// Unique id of this educationSpecification
    /// </summary>
    /// <value>Unique id of this educationSpecification</value>
    [JsonRequired]
    [SortAllowed]
    [JsonProperty("educationSpecificationId")]
    public Guid EducationSpecificationId { get; set; }

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
    [SortAllowed]
    public string? PrimaryCode { get; set; }

    /// <summary>
    /// An array of additional human readable codes/identifiers for the entity being described.
    /// </summary>
    /// <value>An array of additional human readable codes/identifiers for the entity being described.</value>
    [JsonProperty(PropertyName = "otherCodes")]
    public List<OtherCodes> OtherCodes { get; set; } = default!;

    /// <summary>
    /// The type of education specification   - program: HOOPLEIDING   - privateProgram: PARTICULIEREOPLEIDING   - programCluster: HOONDERWIJSEENHEDENCLUSTER   - course: HOONDERWIJSEENHEID 
    /// </summary>
    /// <value>The type of education specification   - program: HOOPLEIDING   - privateProgram: PARTICULIEREOPLEIDING   - programCluster: HOONDERWIJSEENHEDENCLUSTER   - course: HOONDERWIJSEENHEID </value>
    [JsonRequired]
    [SortAllowed]
    [JsonProperty(PropertyName = "educationSpecificationType")]
    public EducationSpecificationType? EducationSpecificationType { get; set; }

    /// <summary>
    /// The name of this education specification
    /// </summary>
    /// <value>The name of this education specification</value>
    [JsonRequired]
    [JsonProperty("name")]
    [NotMapped]
    public List<LanguageTypedString> name => Attributes.ExtractStringsByPropertyName(nameof(name));

    [JsonIgnore]
    [SortAllowed]
    public List<LanguageTypedProperty> Attributes { get; set; } = default!;

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
    public List<LanguageTypedString> description => Attributes.ExtractStringsByPropertyName(nameof(description));

    /// <summary>
    /// The type of formal document obtained after completion of this education   - diploma: DIPLOMA   - certificate: CERTIFICAAT   - no official document: GEEN OFFICIEEL DOCUMENT   - testimonial: GETUIGSCHRIFT   - school advice: SCHOOLADVIES 
    /// </summary>
    /// <value>The type of formal document obtained after completion of this education   - diploma: DIPLOMA   - certificate: CERTIFICAAT   - no official document: GEEN OFFICIEEL DOCUMENT   - testimonial: GETUIGSCHRIFT   - school advice: SCHOOLADVIES </value>
    [JsonProperty(PropertyName = "formalDocument")]
    public FormalDocument? FormalDocument { get; set; }

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
    /// Level of qualification according to the Dutch National Qualification Framework and the European Qualifications Framework, see [this overview](https://nlqf.nl/images/downloads/English2018/Schematic_overview_NLQF_2020.pdf) for more information.
    /// </summary>
    /// <value>Level of qualification according to the Dutch National Qualification Framework and the European Qualifications Framework, see [this overview](https://nlqf.nl/images/downloads/English2018/Schematic_overview_NLQF_2020.pdf) for more information.</value>
    [JsonProperty(PropertyName = "levelOfQualification")]
    public LevelOfQualification? LevelOfQualification { get; set; }

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

    [JsonIgnore]
    public string? StudyLoadUnit { get; set; }

    [JsonIgnore]
    public int? StudyLoadValue { get; set; }

    /// <summary>
    /// Statements that describe the knowledge or skills students should acquire by the end of a particular course or program (ECTS-learningoutcome).
    /// </summary>
    /// <value>Statements that describe the knowledge or skills students should acquire by the end of a particular course or program (ECTS-learningoutcome).</value>
    [JsonProperty(PropertyName = "learningOutcomes")]
    [NotMapped]
    public List<List<LanguageTypedString>> LearningOutcomes { get; set; } = default!;

    /// <summary>
    /// URL of the program&#x27;s website
    /// </summary>
    /// <value>URL of the program&#x27;s website</value>

    [MaxLength(2048)]
    [JsonProperty(PropertyName = "link")]
    public Uri? Link { get; set; }

    /// <summary>
    /// The educationSpecification that is the parent of this educationSpecification if it exists. [&#x60;expandable&#x60;](#tag/education_specification_model)
    /// </summary>
    /// <value>The educationSpecification that is the parent of this educationSpecification if it exists. [&#x60;expandable&#x60;](#tag/education_specification_model)</value>
    [JsonProperty(PropertyName = "parent")]
    [NotMapped]
    [JsonConverter(typeof(OneOfConverter))]
    public OneOfEducationSpecification? OneOfParent
    {
        get
        {
            if (ParentId == null)
            {
                return null;
            }

            return new OneOfEducationSpecificationInstance(ParentId, Parent);
        }
    }

    [JsonIgnore]
    public Guid? ParentId { get; set; }

    [JsonIgnore]
    [NotMapped]
    public EducationSpecification? Parent { get; set; }

    /// <summary>
    /// The EducationSpecifications that have this EducationSpecification as their parent. [&#x60;expandable&#x60;](#tag/education_specification_model)
    /// </summary>
    /// <value>The EducationSpecifications that have this EducationSpecification as their parent. [&#x60;expandable&#x60;](#tag/education_specification_model)</value>
    [JsonProperty("children")]
    [NotMapped]
    [JsonConverter(typeof(ListOneOfConverter))]
    public List<OneOfEducationSpecification> ChildrenList
    {
        get
        {
            if (ChildrenIds == null || ChildrenIds.Count == 0)
            {
                return new List<OneOfEducationSpecification>();
            }

            var result = new List<OneOfEducationSpecification>();
            foreach (var ChildId in ChildrenIds)
            {
                result.Add(new OneOfEducationSpecificationInstance(ChildId, Children?.Find(x => x.EducationSpecificationId.Equals(ChildId))));
            }
            return result;
        }
    }

    [JsonIgnore]
    [NotMapped]
    public List<Guid> ChildrenIds { get; set; } = default!;

    [JsonIgnore]
    [NotMapped]
    public List<EducationSpecification> Children { get; set; } = default!;

    /// <summary>
    /// The organization that manages this group. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
    /// </summary>
    /// <value>The organization that manages this group. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

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
    public List<JObject> ConsumersList
    {
        get
        {
            if (Consumers != null && Consumers.Count != 0)
            {
                return ConsumerConverter.GetDynamicConsumers(Consumers);
            }

            return new List<JObject>();
        }
    }

    [JsonIgnore]
    public List<ConsumerBase> Consumers { get; set; } = default!;

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
