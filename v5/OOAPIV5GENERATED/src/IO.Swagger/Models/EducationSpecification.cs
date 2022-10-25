using IO.Swagger.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace IO.Swagger.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class EducationSpecification : IEquatable<EducationSpecification>
    {
        /// <summary>
        /// Gets or Sets PrimaryCode
        /// </summary>
        [JsonRequired]

        [DataMember(Name = "primaryCode")]
        public PrimaryCode PrimaryCode { get; set; }

        /// <summary>
        /// An array of additional human readable codes/identifiers for the entity being described.
        /// </summary>
        /// <value>An array of additional human readable codes/identifiers for the entity being described.</value>

        [DataMember(Name = "otherCodes")]
        public List<OtherCodes> OtherCodes { get; set; }

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

        [DataMember(Name = "educationSpecificationType")]
        public EducationSpecificationTypeEnum? EducationSpecificationType { get; set; }

        /// <summary>
        /// The name of this education specification
        /// </summary>
        /// <value>The name of this education specification</value>
        [JsonRequired]

        [DataMember(Name = "name")]
        public List<LanguageValueItem> Name { get; set; }

        /// <summary>
        /// The abbreviation of this program
        /// </summary>
        /// <value>The abbreviation of this program</value>

        [MaxLength(256)]
        [DataMember(Name = "abbreviation")]
        public string Abbreviation { get; set; }

        /// <summary>
        /// The description of this program. [The limited implementation of Git Hub Markdown syntax](#tag/formatting-and-displaying-results-from-API) MAY be used for rich text representation.
        /// </summary>
        /// <value>The description of this program. [The limited implementation of Git Hub Markdown syntax](#tag/formatting-and-displaying-results-from-API) MAY be used for rich text representation.</value>

        [DataMember(Name = "description")]
        public List<LanguageValueItem> Description { get; set; }



        /// <summary>
        /// The type of formal document obtained after completion of this education   - diploma: DIPLOMA   - certificate: CERTIFICAAT   - no official document: GEEN OFFICIEEL DOCUMENT   - testimonial: GETUIGSCHRIFT   - school advice: SCHOOLADVIES 
        /// </summary>
        /// <value>The type of formal document obtained after completion of this education   - diploma: DIPLOMA   - certificate: CERTIFICAAT   - no official document: GEEN OFFICIEEL DOCUMENT   - testimonial: GETUIGSCHRIFT   - school advice: SCHOOLADVIES </value>

        [DataMember(Name = "formalDocument")]
        public FormalDocumentEnum? FormalDocument { get; set; }

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
        /// Level of qualification according to the Dutch National Qualification Framework and the European Qualifications Framework, see [this overview](https://nlqf.nl/images/downloads/English2018/Schematic_overview_NLQF_2020.pdf) for more information.
        /// </summary>
        /// <value>Level of qualification according to the Dutch National Qualification Framework and the European Qualifications Framework, see [this overview](https://nlqf.nl/images/downloads/English2018/Schematic_overview_NLQF_2020.pdf) for more information.</value>

        [DataMember(Name = "levelOfQualification")]
        public LevelOfQualificationEnum? LevelOfQualification { get; set; }

        /// <summary>
        /// Field(s) of study (e.g. ISCED-F) (http://uis.unesco.org/sites/default/files/documents/isced-fields-of-education-and-training-2013-en.pdf.
        /// </summary>
        /// <value>Field(s) of study (e.g. ISCED-F) (http://uis.unesco.org/sites/default/files/documents/isced-fields-of-education-and-training-2013-en.pdf.</value>

        [MaxLength(4)]
        [DataMember(Name = "fieldsOfStudy")]
        public string FieldsOfStudy { get; set; }

        /// <summary>
        /// Gets or Sets StudyLoad
        /// </summary>

        [DataMember(Name = "studyLoad")]
        public ProgramResultStudyLoad StudyLoad { get; set; }

        /// <summary>
        /// Statements that describe the knowledge or skills students should acquire by the end of a particular course or program (ECTS-learningoutcome).
        /// </summary>
        /// <value>Statements that describe the knowledge or skills students should acquire by the end of a particular course or program (ECTS-learningoutcome).</value>

        [DataMember(Name = "learningOutcomes")]
        public List<List<Object>> LearningOutcomes { get; set; }

        /// <summary>
        /// URL of the program&#x27;s website
        /// </summary>
        /// <value>URL of the program&#x27;s website</value>

        [MaxLength(2048)]
        [DataMember(Name = "link")]
        public string Link { get; set; }

        /// <summary>
        /// The educationSpecification that is the parent of this educationSpecification if it exists. [&#x60;expandable&#x60;](#tag/education_specification_model)
        /// </summary>
        /// <value>The educationSpecification that is the parent of this educationSpecification if it exists. [&#x60;expandable&#x60;](#tag/education_specification_model)</value>

        [DataMember(Name = "parent")]
        public OneOfEducationSpecification Parent { get; set; }

        /// <summary>
        /// The EducationSpecifications that have this EducationSpecification as their parent. [&#x60;expandable&#x60;](#tag/education_specification_model)
        /// </summary>
        /// <value>The EducationSpecifications that have this EducationSpecification as their parent. [&#x60;expandable&#x60;](#tag/education_specification_model)</value>

        [DataMember(Name = "children")]
        public List<OneOfEducationSpecification> Children { get; set; }

        /// <summary>
        /// The organization that manages this group. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
        /// </summary>
        /// <value>The organization that manages this group. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

        [DataMember(Name = "organization")]
        public OneOfOrganization Organization { get; set; }

        /// <summary>
        /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
        /// </summary>
        /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>

        [DataMember(Name = "consumers")]
        public List<Consumer> Consumers { get; set; }

        /// <summary>
        /// Object for additional non-standard attributes
        /// </summary>
        /// <value>Object for additional non-standard attributes</value>

        [DataMember(Name = "ext")]
        public Object Ext { get; set; }

        /// <summary>
        /// The first day this EducationSpecification is valid (inclusive).
        /// </summary>
        /// <value>The first day this EducationSpecification is valid (inclusive).</value>

        [DataMember(Name = "validFrom")]
        public DateTime? ValidFrom { get; set; }

        /// <summary>
        /// The day this EducationSpecification ceases to be valid (e.g. exclusive).
        /// </summary>
        /// <value>The day this EducationSpecification ceases to be valid (e.g. exclusive).</value>

        [DataMember(Name = "validTo")]
        public DateTime? ValidTo { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class EducationSpecification {\n");
            sb.Append("  PrimaryCode: ").Append(PrimaryCode).Append("\n");
            sb.Append("  OtherCodes: ").Append(OtherCodes).Append("\n");
            sb.Append("  EducationSpecificationType: ").Append(EducationSpecificationType).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Abbreviation: ").Append(Abbreviation).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  FormalDocument: ").Append(FormalDocument).Append("\n");
            sb.Append("  Level: ").Append(Level).Append("\n");
            sb.Append("  Sector: ").Append(Sector).Append("\n");
            sb.Append("  LevelOfQualification: ").Append(LevelOfQualification).Append("\n");
            sb.Append("  FieldsOfStudy: ").Append(FieldsOfStudy).Append("\n");
            sb.Append("  StudyLoad: ").Append(StudyLoad).Append("\n");
            sb.Append("  LearningOutcomes: ").Append(LearningOutcomes).Append("\n");
            sb.Append("  Link: ").Append(Link).Append("\n");
            sb.Append("  Parent: ").Append(Parent).Append("\n");
            sb.Append("  Children: ").Append(Children).Append("\n");
            sb.Append("  Organization: ").Append(Organization).Append("\n");
            sb.Append("  Consumers: ").Append(Consumers).Append("\n");
            sb.Append("  Ext: ").Append(Ext).Append("\n");
            sb.Append("  ValidFrom: ").Append(ValidFrom).Append("\n");
            sb.Append("  ValidTo: ").Append(ValidTo).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((EducationSpecification)obj);
        }

        /// <summary>
        /// Returns true if EducationSpecification instances are equal
        /// </summary>
        /// <param name="other">Instance of EducationSpecification to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(EducationSpecification other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    PrimaryCode == other.PrimaryCode ||
                    PrimaryCode != null &&
                    PrimaryCode.Equals(other.PrimaryCode)
                ) &&
                (
                    OtherCodes == other.OtherCodes ||
                    OtherCodes != null &&
                    OtherCodes.SequenceEqual(other.OtherCodes)
                ) &&
                (
                    EducationSpecificationType == other.EducationSpecificationType ||
                    EducationSpecificationType != null &&
                    EducationSpecificationType.Equals(other.EducationSpecificationType)
                ) &&
                (
                    Name == other.Name ||
                    Name != null &&
                    Name.SequenceEqual(other.Name)
                ) &&
                (
                    Abbreviation == other.Abbreviation ||
                    Abbreviation != null &&
                    Abbreviation.Equals(other.Abbreviation)
                ) &&
                (
                    Description == other.Description ||
                    Description != null &&
                    Description.SequenceEqual(other.Description)
                ) &&
                (
                    FormalDocument == other.FormalDocument ||
                    FormalDocument != null &&
                    FormalDocument.Equals(other.FormalDocument)
                ) &&
                (
                    Level == other.Level ||
                    Level != null &&
                    Level.Equals(other.Level)
                ) &&
                (
                    Sector == other.Sector ||
                    Sector != null &&
                    Sector.Equals(other.Sector)
                ) &&
                (
                    LevelOfQualification == other.LevelOfQualification ||
                    LevelOfQualification != null &&
                    LevelOfQualification.Equals(other.LevelOfQualification)
                ) &&
                (
                    FieldsOfStudy == other.FieldsOfStudy ||
                    FieldsOfStudy != null &&
                    FieldsOfStudy.Equals(other.FieldsOfStudy)
                ) &&
                (
                    StudyLoad == other.StudyLoad ||
                    StudyLoad != null &&
                    StudyLoad.Equals(other.StudyLoad)
                ) &&
                (
                    LearningOutcomes == other.LearningOutcomes ||
                    LearningOutcomes != null &&
                    LearningOutcomes.SequenceEqual(other.LearningOutcomes)
                ) &&
                (
                    Link == other.Link ||
                    Link != null &&
                    Link.Equals(other.Link)
                ) &&
                (
                    Parent == other.Parent ||
                    Parent != null &&
                    Parent.Equals(other.Parent)
                ) &&
                (
                    Children == other.Children ||
                    Children != null &&
                    Children.SequenceEqual(other.Children)
                ) &&
                (
                    Organization == other.Organization ||
                    Organization != null &&
                    Organization.Equals(other.Organization)
                ) &&
                (
                    Consumers == other.Consumers ||
                    Consumers != null &&
                    Consumers.SequenceEqual(other.Consumers)
                ) &&
                (
                    Ext == other.Ext ||
                    Ext != null &&
                    Ext.Equals(other.Ext)
                ) &&
                (
                    ValidFrom == other.ValidFrom ||
                    ValidFrom != null &&
                    ValidFrom.Equals(other.ValidFrom)
                ) &&
                (
                    ValidTo == other.ValidTo ||
                    ValidTo != null &&
                    ValidTo.Equals(other.ValidTo)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                if (PrimaryCode != null)
                    hashCode = hashCode * 59 + PrimaryCode.GetHashCode();
                if (OtherCodes != null)
                    hashCode = hashCode * 59 + OtherCodes.GetHashCode();
                if (EducationSpecificationType != null)
                    hashCode = hashCode * 59 + EducationSpecificationType.GetHashCode();
                if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                if (Abbreviation != null)
                    hashCode = hashCode * 59 + Abbreviation.GetHashCode();
                if (Description != null)
                    hashCode = hashCode * 59 + Description.GetHashCode();
                if (FormalDocument != null)
                    hashCode = hashCode * 59 + FormalDocument.GetHashCode();
                if (Level != null)
                    hashCode = hashCode * 59 + Level.GetHashCode();
                if (Sector != null)
                    hashCode = hashCode * 59 + Sector.GetHashCode();
                if (LevelOfQualification != null)
                    hashCode = hashCode * 59 + LevelOfQualification.GetHashCode();
                if (FieldsOfStudy != null)
                    hashCode = hashCode * 59 + FieldsOfStudy.GetHashCode();
                if (StudyLoad != null)
                    hashCode = hashCode * 59 + StudyLoad.GetHashCode();
                if (LearningOutcomes != null)
                    hashCode = hashCode * 59 + LearningOutcomes.GetHashCode();
                if (Link != null)
                    hashCode = hashCode * 59 + Link.GetHashCode();
                if (Parent != null)
                    hashCode = hashCode * 59 + Parent.GetHashCode();
                if (Children != null)
                    hashCode = hashCode * 59 + Children.GetHashCode();
                if (Organization != null)
                    hashCode = hashCode * 59 + Organization.GetHashCode();
                if (Consumers != null)
                    hashCode = hashCode * 59 + Consumers.GetHashCode();
                if (Ext != null)
                    hashCode = hashCode * 59 + Ext.GetHashCode();
                if (ValidFrom != null)
                    hashCode = hashCode * 59 + ValidFrom.GetHashCode();
                if (ValidTo != null)
                    hashCode = hashCode * 59 + ValidTo.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(EducationSpecification left, EducationSpecification right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EducationSpecification left, EducationSpecification right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
