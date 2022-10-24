using ooapi.v5.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class Course : IEquatable<Course>
    {
        /// <summary>
        /// Gets or Sets PrimaryCode
        /// </summary>
        [Required]

        [DataMember(Name = "primaryCode")]
        public PrimaryCode PrimaryCode { get; set; }

        /// <summary>
        /// The name of this course (ECTS-title)
        /// </summary>
        /// <value>The name of this course (ECTS-title)</value>
        [Required]

        [DataMember(Name = "name")]
        public List<LanguageValueItem> Name { get; set; }

        /// <summary>
        /// The abbreviation or internal code used to identify this course (ECTS-code)
        /// </summary>
        /// <value>The abbreviation or internal code used to identify this course (ECTS-code)</value>
        [Required]

        [MaxLength(256)]
        [DataMember(Name = "abbreviation")]
        public string Abbreviation { get; set; }

        /// <summary>
        /// Gets or Sets StudyLoad
        /// </summary>

        [DataMember(Name = "studyLoad")]
        public ProgramResultStudyLoad StudyLoad { get; set; }


        /// <summary>
        /// The mode of delivery of the component (ECTS-mode of delivery) - distance-learning: afstandsleren - on campus: op de campus - online: online - hybrid: hybride - situated: op locatie 
        /// </summary>
        /// <value>The mode of delivery of the component (ECTS-mode of delivery) - distance-learning: afstandsleren - on campus: op de campus - online: online - hybrid: hybride - situated: op locatie </value>

        [DataMember(Name = "modeOfDelivery")]
        public List<ModeOfDeliveryEnum> ModeOfDelivery { get; set; }

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
        [Required]

        [DataMember(Name = "description")]
        public List<LanguageValueItem> Description { get; set; }

        /// <summary>
        /// The (primary) teaching language in which this course is given, should be a three-letter language code as specified by ISO 639-2.
        /// </summary>
        /// <value>The (primary) teaching language in which this course is given, should be a three-letter language code as specified by ISO 639-2.</value>
        [Required]
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
        public List<List<Object>> LearningOutcomes { get; set; }

        /// <summary>
        /// This information may be given at an institutional level and/or at the level of individual programmes. Make sure that it is clear whether the information applies to fee-paying students (national and/or international) or to exchange students.
        /// </summary>
        /// <value>This information may be given at an institutional level and/or at the level of individual programmes. Make sure that it is clear whether the information applies to fee-paying students (national and/or international) or to exchange students.</value>

        [DataMember(Name = "admissionRequirements")]
        public List<LanguageValueItem> AdmissionRequirements { get; set; }

        /// <summary>
        /// Normally, students will receive a diploma when they have completed the (official) study program and have obtained the required number of credits. If there are any other specific requirements that students need to have fulfilled, mention them here.
        /// </summary>
        /// <value>Normally, students will receive a diploma when they have completed the (official) study program and have obtained the required number of credits. If there are any other specific requirements that students need to have fulfilled, mention them here.</value>

        [DataMember(Name = "qualificationRequirements")]
        public List<LanguageValueItem> QualificationRequirements { get; set; }

        /// <summary>
        /// The level of this course (ECTS-year of study if applicable) - secondary vocational education: mbo - secondary vocational education 1: mbo 1, corresponds to levelOfQualification 1 - secondary vocational education 2: mbo 2, corresponds to levelOfQualification 2 - secondary vocational education 3: mbo 3, corresponds to levelOfQualification 3 - secondary vocational education 4: mbo 4, corresponds to levelOfQualification 4 - associate degree: associate degree, corresponds to levelOfQualification 5 - bachelor: bachelor, corresponds to levelOfQualification 6 - master: master, corresponds to levelOfQualification 7 - doctoral: doctoraal, corresponds to levelOfQualification 8 - undefined: onbepaald - undivided: ongedeeld - nt2-1: NT2 niveau 1 - nt2-2: NT2 niveau 2 
        /// </summary>
        /// <value>The level of this course (ECTS-year of study if applicable) - secondary vocational education: mbo - secondary vocational education 1: mbo 1, corresponds to levelOfQualification 1 - secondary vocational education 2: mbo 2, corresponds to levelOfQualification 2 - secondary vocational education 3: mbo 3, corresponds to levelOfQualification 3 - secondary vocational education 4: mbo 4, corresponds to levelOfQualification 4 - associate degree: associate degree, corresponds to levelOfQualification 5 - bachelor: bachelor, corresponds to levelOfQualification 6 - master: master, corresponds to levelOfQualification 7 - doctoral: doctoraal, corresponds to levelOfQualification 8 - undefined: onbepaald - undivided: ongedeeld - nt2-1: NT2 niveau 1 - nt2-2: NT2 niveau 2 </value>
        [Required]

        [DataMember(Name = "level")]
        public LevelEnum? Level { get; set; }

        /// <summary>
        /// The extra information that is provided for enrollment
        /// </summary>
        /// <value>The extra information that is provided for enrollment</value>

        [DataMember(Name = "enrollment")]
        public List<LanguageValueItem> Enrollment { get; set; }

        /// <summary>
        /// An overview of the literature and other resources that is used in this course (ECTS-recommended reading and other sources)
        /// </summary>
        /// <value>An overview of the literature and other resources that is used in this course (ECTS-recommended reading and other sources)</value>

        [DataMember(Name = "resources")]
        public List<string> Resources { get; set; }

        /// <summary>
        /// A description of the way exams for this course are taken (ECTS-assessment method and criteria).
        /// </summary>
        /// <value>A description of the way exams for this course are taken (ECTS-assessment method and criteria).</value>

        [DataMember(Name = "assessment")]
        public List<LanguageValueItem> Assessment { get; set; }

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
        public Object EducationSpecification { get; set; }

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
        /// Object for additional non-standard attributes
        /// </summary>
        /// <value>Object for additional non-standard attributes</value>

        [DataMember(Name = "ext")]
        public Object Ext { get; set; }

        /// <summary>
        /// The program of which this course is a part of. This object is [&#x60;expandable&#x60;](#tag/program_model)
        /// </summary>
        /// <value>The program of which this course is a part of. This object is [&#x60;expandable&#x60;](#tag/program_model)</value>

        [DataMember(Name = "programs")]
        public List<Object> Programs { get; set; }

        /// <summary>
        /// The person(s) responsible for this course. This object is [&#x60;expandable&#x60;](#tag/person_model)
        /// </summary>
        /// <value>The person(s) responsible for this course. This object is [&#x60;expandable&#x60;](#tag/person_model)</value>

        [DataMember(Name = "coordinators")]
        public List<Object> Coordinators { get; set; }

        /// <summary>
        /// The organization that manages this group. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
        /// </summary>
        /// <value>The organization that manages this group. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

        [DataMember(Name = "organization")]
        public Object Organization { get; set; }

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

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Course {\n");
            sb.Append("  PrimaryCode: ").Append(PrimaryCode).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Abbreviation: ").Append(Abbreviation).Append("\n");
            sb.Append("  StudyLoad: ").Append(StudyLoad).Append("\n");
            sb.Append("  ModeOfDelivery: ").Append(ModeOfDelivery).Append("\n");
            sb.Append("  Duration: ").Append(Duration).Append("\n");
            sb.Append("  FirstStartDate: ").Append(FirstStartDate).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  TeachingLanguage: ").Append(TeachingLanguage).Append("\n");
            sb.Append("  FieldsOfStudy: ").Append(FieldsOfStudy).Append("\n");
            sb.Append("  LearningOutcomes: ").Append(LearningOutcomes).Append("\n");
            sb.Append("  AdmissionRequirements: ").Append(AdmissionRequirements).Append("\n");
            sb.Append("  QualificationRequirements: ").Append(QualificationRequirements).Append("\n");
            sb.Append("  Level: ").Append(Level).Append("\n");
            sb.Append("  Enrollment: ").Append(Enrollment).Append("\n");
            sb.Append("  Resources: ").Append(Resources).Append("\n");
            sb.Append("  Assessment: ").Append(Assessment).Append("\n");
            sb.Append("  Link: ").Append(Link).Append("\n");
            sb.Append("  EducationSpecification: ").Append(EducationSpecification).Append("\n");
            sb.Append("  Addresses: ").Append(Addresses).Append("\n");
            sb.Append("  OtherCodes: ").Append(OtherCodes).Append("\n");
            sb.Append("  Consumers: ").Append(Consumers).Append("\n");
            sb.Append("  Ext: ").Append(Ext).Append("\n");
            sb.Append("  Programs: ").Append(Programs).Append("\n");
            sb.Append("  Coordinators: ").Append(Coordinators).Append("\n");
            sb.Append("  Organization: ").Append(Organization).Append("\n");
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
            return obj.GetType() == GetType() && Equals((Course)obj);
        }

        /// <summary>
        /// Returns true if Course instances are equal
        /// </summary>
        /// <param name="other">Instance of Course to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Course other)
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
                    StudyLoad == other.StudyLoad ||
                    StudyLoad != null &&
                    StudyLoad.Equals(other.StudyLoad)
                ) &&
                (
                    ModeOfDelivery == other.ModeOfDelivery ||
                    ModeOfDelivery != null &&
                    ModeOfDelivery.SequenceEqual(other.ModeOfDelivery)
                ) &&
                (
                    Duration == other.Duration ||
                    Duration != null &&
                    Duration.Equals(other.Duration)
                ) &&
                (
                    FirstStartDate == other.FirstStartDate ||
                    FirstStartDate != null &&
                    FirstStartDate.Equals(other.FirstStartDate)
                ) &&
                (
                    Description == other.Description ||
                    Description != null &&
                    Description.SequenceEqual(other.Description)
                ) &&
                (
                    TeachingLanguage == other.TeachingLanguage ||
                    TeachingLanguage != null &&
                    TeachingLanguage.Equals(other.TeachingLanguage)
                ) &&
                (
                    FieldsOfStudy == other.FieldsOfStudy ||
                    FieldsOfStudy != null &&
                    FieldsOfStudy.Equals(other.FieldsOfStudy)
                ) &&
                (
                    LearningOutcomes == other.LearningOutcomes ||
                    LearningOutcomes != null &&
                    LearningOutcomes.SequenceEqual(other.LearningOutcomes)
                ) &&
                (
                    AdmissionRequirements == other.AdmissionRequirements ||
                    AdmissionRequirements != null &&
                    AdmissionRequirements.SequenceEqual(other.AdmissionRequirements)
                ) &&
                (
                    QualificationRequirements == other.QualificationRequirements ||
                    QualificationRequirements != null &&
                    QualificationRequirements.SequenceEqual(other.QualificationRequirements)
                ) &&
                (
                    Level == other.Level ||
                    Level != null &&
                    Level.Equals(other.Level)
                ) &&
                (
                    Enrollment == other.Enrollment ||
                    Enrollment != null &&
                    Enrollment.SequenceEqual(other.Enrollment)
                ) &&
                (
                    Resources == other.Resources ||
                    Resources != null &&
                    Resources.SequenceEqual(other.Resources)
                ) &&
                (
                    Assessment == other.Assessment ||
                    Assessment != null &&
                    Assessment.SequenceEqual(other.Assessment)
                ) &&
                (
                    Link == other.Link ||
                    Link != null &&
                    Link.Equals(other.Link)
                ) &&
                (
                    EducationSpecification == other.EducationSpecification ||
                    EducationSpecification != null &&
                    EducationSpecification.Equals(other.EducationSpecification)
                ) &&
                (
                    Addresses == other.Addresses ||
                    Addresses != null &&
                    Addresses.SequenceEqual(other.Addresses)
                ) &&
                (
                    OtherCodes == other.OtherCodes ||
                    OtherCodes != null &&
                    OtherCodes.SequenceEqual(other.OtherCodes)
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
                    Programs == other.Programs ||
                    Programs != null &&
                    Programs.SequenceEqual(other.Programs)
                ) &&
                (
                    Coordinators == other.Coordinators ||
                    Coordinators != null &&
                    Coordinators.SequenceEqual(other.Coordinators)
                ) &&
                (
                    Organization == other.Organization ||
                    Organization != null &&
                    Organization.Equals(other.Organization)
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
                if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                if (Abbreviation != null)
                    hashCode = hashCode * 59 + Abbreviation.GetHashCode();
                if (StudyLoad != null)
                    hashCode = hashCode * 59 + StudyLoad.GetHashCode();
                if (ModeOfDelivery != null)
                    hashCode = hashCode * 59 + ModeOfDelivery.GetHashCode();
                if (Duration != null)
                    hashCode = hashCode * 59 + Duration.GetHashCode();
                if (FirstStartDate != null)
                    hashCode = hashCode * 59 + FirstStartDate.GetHashCode();
                if (Description != null)
                    hashCode = hashCode * 59 + Description.GetHashCode();
                if (TeachingLanguage != null)
                    hashCode = hashCode * 59 + TeachingLanguage.GetHashCode();
                if (FieldsOfStudy != null)
                    hashCode = hashCode * 59 + FieldsOfStudy.GetHashCode();
                if (LearningOutcomes != null)
                    hashCode = hashCode * 59 + LearningOutcomes.GetHashCode();
                if (AdmissionRequirements != null)
                    hashCode = hashCode * 59 + AdmissionRequirements.GetHashCode();
                if (QualificationRequirements != null)
                    hashCode = hashCode * 59 + QualificationRequirements.GetHashCode();
                if (Level != null)
                    hashCode = hashCode * 59 + Level.GetHashCode();
                if (Enrollment != null)
                    hashCode = hashCode * 59 + Enrollment.GetHashCode();
                if (Resources != null)
                    hashCode = hashCode * 59 + Resources.GetHashCode();
                if (Assessment != null)
                    hashCode = hashCode * 59 + Assessment.GetHashCode();
                if (Link != null)
                    hashCode = hashCode * 59 + Link.GetHashCode();
                if (EducationSpecification != null)
                    hashCode = hashCode * 59 + EducationSpecification.GetHashCode();
                if (Addresses != null)
                    hashCode = hashCode * 59 + Addresses.GetHashCode();
                if (OtherCodes != null)
                    hashCode = hashCode * 59 + OtherCodes.GetHashCode();
                if (Consumers != null)
                    hashCode = hashCode * 59 + Consumers.GetHashCode();
                if (Ext != null)
                    hashCode = hashCode * 59 + Ext.GetHashCode();
                if (Programs != null)
                    hashCode = hashCode * 59 + Programs.GetHashCode();
                if (Coordinators != null)
                    hashCode = hashCode * 59 + Coordinators.GetHashCode();
                if (Organization != null)
                    hashCode = hashCode * 59 + Organization.GetHashCode();
                if (ValidFrom != null)
                    hashCode = hashCode * 59 + ValidFrom.GetHashCode();
                if (ValidTo != null)
                    hashCode = hashCode * 59 + ValidTo.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(Course left, Course right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Course left, Course right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
