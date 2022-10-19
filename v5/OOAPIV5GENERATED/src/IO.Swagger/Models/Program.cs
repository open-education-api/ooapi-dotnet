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
    public partial class Program : IEquatable<Program>
    {
        /// <summary>
        /// Gets or Sets PrimaryCode
        /// </summary>
        [Required]

        [DataMember(Name = "primaryCode")]
        public PrimaryCode PrimaryCode { get; set; }



        /// <summary>
        /// The type of this program - program: opleiding - minor: minor - honours: honours - specialization: specialisatie - track: track 
        /// </summary>
        /// <value>The type of this program - program: opleiding - minor: minor - honours: honours - specialization: specialisatie - track: track </value>
        [Required]

        [DataMember(Name = "programType")]
        public ProgramTypeEnum? ProgramType { get; set; }

        /// <summary>
        /// The name of this program
        /// </summary>
        /// <value>The name of this program</value>
        [Required]

        [DataMember(Name = "name")]
        public List<LanguageValueItem> Name { get; set; }

        /// <summary>
        /// The abbreviation of this program
        /// </summary>
        /// <value>The abbreviation of this program</value>
        [Required]

        [MaxLength(256)]
        [DataMember(Name = "abbreviation")]
        public string Abbreviation { get; set; }

        /// <summary>
        /// The description of this program
        /// </summary>
        /// <value>The description of this program</value>
        [Required]

        [DataMember(Name = "description")]
        public List<LanguageValueItem> Description { get; set; }

        /// <summary>
        /// The (primary) teaching language in which this program is given, should be a three-letter language code as specified by ISO 639-2.
        /// </summary>
        /// <value>The (primary) teaching language in which this program is given, should be a three-letter language code as specified by ISO 639-2.</value>
        [Required]
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



        /// <summary>
        /// The mode of delivery of the component (ECTS-mode of delivery) - distance-learning: afstandsleren - on campus: op de campus - online: online - hybrid: hybride - situated: op locatie 
        /// </summary>
        /// <value>The mode of delivery of the component (ECTS-mode of delivery) - distance-learning: afstandsleren - on campus: op de campus - online: online - hybrid: hybride - situated: op locatie </value>

        [DataMember(Name = "modeOfDelivery")]
        public List<ModeOfDeliveryEnum> ModeOfDelivery { get; set; }

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
        public List<LanguageValueItem> Enrollment { get; set; }

        /// <summary>
        /// An overview of the literature and other resources that is used in this course (ECTS-recommended reading and other sources)
        /// </summary>
        /// <value>An overview of the literature and other resources that is used in this course (ECTS-recommended reading and other sources)</value>

        [DataMember(Name = "resources")]
        public List<string> Resources { get; set; }

        /// <summary>
        /// List of learning outcomes at program level. It is advisable to limit the number of learning outcomes to approximately 20. It is also advisable to make sure that the program learning outcomes in the course catalogue correspond with those on the Diploma Supplement.
        /// </summary>
        /// <value>List of learning outcomes at program level. It is advisable to limit the number of learning outcomes to approximately 20. It is also advisable to make sure that the program learning outcomes in the course catalogue correspond with those on the Diploma Supplement.</value>

        [DataMember(Name = "learningOutcomes")]
        public List<List<Object>> LearningOutcomes { get; set; }

        /// <summary>
        /// A description of the way exams for this course are taken (ECTS-assessment method and criteria).
        /// </summary>
        /// <value>A description of the way exams for this course are taken (ECTS-assessment method and criteria).</value>

        [DataMember(Name = "assessment")]
        public List<LanguageValueItem> Assessment { get; set; }

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
        public Object EducationSpecification { get; set; }

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
        public Object Parent { get; set; }

        /// <summary>
        /// Programs which are a part of this program (e.g specializations). This object is [&#x60;expandable&#x60;](#tag/program_model)
        /// </summary>
        /// <value>Programs which are a part of this program (e.g specializations). This object is [&#x60;expandable&#x60;](#tag/program_model)</value>

        [DataMember(Name = "children")]
        public List<Object> Children { get; set; }

        /// <summary>
        /// The person(s) responsible for this program. This object is [&#x60;expandable&#x60;](#tag/person_model)
        /// </summary>
        /// <value>The person(s) responsible for this program. This object is [&#x60;expandable&#x60;](#tag/person_model)</value>

        [DataMember(Name = "coordinators")]
        public List<Object> Coordinators { get; set; }

        /// <summary>
        /// The organization providing this program. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
        /// </summary>
        /// <value>The organization providing this program. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

        [DataMember(Name = "organization")]
        public Object Organization { get; set; }

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

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Program {\n");
            sb.Append("  PrimaryCode: ").Append(PrimaryCode).Append("\n");
            sb.Append("  ProgramType: ").Append(ProgramType).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Abbreviation: ").Append(Abbreviation).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  TeachingLanguage: ").Append(TeachingLanguage).Append("\n");
            sb.Append("  StudyLoad: ").Append(StudyLoad).Append("\n");
            sb.Append("  QualificationAwarded: ").Append(QualificationAwarded).Append("\n");
            sb.Append("  ModeOfStudy: ").Append(ModeOfStudy).Append("\n");
            sb.Append("  ModeOfDelivery: ").Append(ModeOfDelivery).Append("\n");
            sb.Append("  Duration: ").Append(Duration).Append("\n");
            sb.Append("  FirstStartDate: ").Append(FirstStartDate).Append("\n");
            sb.Append("  LevelOfQualification: ").Append(LevelOfQualification).Append("\n");
            sb.Append("  Level: ").Append(Level).Append("\n");
            sb.Append("  Sector: ").Append(Sector).Append("\n");
            sb.Append("  FieldsOfStudy: ").Append(FieldsOfStudy).Append("\n");
            sb.Append("  Enrollment: ").Append(Enrollment).Append("\n");
            sb.Append("  Resources: ").Append(Resources).Append("\n");
            sb.Append("  LearningOutcomes: ").Append(LearningOutcomes).Append("\n");
            sb.Append("  Assessment: ").Append(Assessment).Append("\n");
            sb.Append("  AdmissionRequirements: ").Append(AdmissionRequirements).Append("\n");
            sb.Append("  QualificationRequirements: ").Append(QualificationRequirements).Append("\n");
            sb.Append("  Link: ").Append(Link).Append("\n");
            sb.Append("  EducationSpecification: ").Append(EducationSpecification).Append("\n");
            sb.Append("  OtherCodes: ").Append(OtherCodes).Append("\n");
            sb.Append("  Addresses: ").Append(Addresses).Append("\n");
            sb.Append("  Parent: ").Append(Parent).Append("\n");
            sb.Append("  Children: ").Append(Children).Append("\n");
            sb.Append("  Coordinators: ").Append(Coordinators).Append("\n");
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
            return obj.GetType() == GetType() && Equals((Program)obj);
        }

        /// <summary>
        /// Returns true if Program instances are equal
        /// </summary>
        /// <param name="other">Instance of Program to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Program other)
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
                    ProgramType == other.ProgramType ||
                    ProgramType != null &&
                    ProgramType.Equals(other.ProgramType)
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
                    TeachingLanguage == other.TeachingLanguage ||
                    TeachingLanguage != null &&
                    TeachingLanguage.Equals(other.TeachingLanguage)
                ) &&
                (
                    StudyLoad == other.StudyLoad ||
                    StudyLoad != null &&
                    StudyLoad.Equals(other.StudyLoad)
                ) &&
                (
                    QualificationAwarded == other.QualificationAwarded ||
                    QualificationAwarded != null &&
                    QualificationAwarded.Equals(other.QualificationAwarded)
                ) &&
                (
                    ModeOfStudy == other.ModeOfStudy ||
                    ModeOfStudy != null &&
                    ModeOfStudy.Equals(other.ModeOfStudy)
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
                    LevelOfQualification == other.LevelOfQualification ||
                    LevelOfQualification != null &&
                    LevelOfQualification.Equals(other.LevelOfQualification)
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
                    FieldsOfStudy == other.FieldsOfStudy ||
                    FieldsOfStudy != null &&
                    FieldsOfStudy.Equals(other.FieldsOfStudy)
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
                    LearningOutcomes == other.LearningOutcomes ||
                    LearningOutcomes != null &&
                    LearningOutcomes.SequenceEqual(other.LearningOutcomes)
                ) &&
                (
                    Assessment == other.Assessment ||
                    Assessment != null &&
                    Assessment.SequenceEqual(other.Assessment)
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
                    OtherCodes == other.OtherCodes ||
                    OtherCodes != null &&
                    OtherCodes.SequenceEqual(other.OtherCodes)
                ) &&
                (
                    Addresses == other.Addresses ||
                    Addresses != null &&
                    Addresses.SequenceEqual(other.Addresses)
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
                if (ProgramType != null)
                    hashCode = hashCode * 59 + ProgramType.GetHashCode();
                if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                if (Abbreviation != null)
                    hashCode = hashCode * 59 + Abbreviation.GetHashCode();
                if (Description != null)
                    hashCode = hashCode * 59 + Description.GetHashCode();
                if (TeachingLanguage != null)
                    hashCode = hashCode * 59 + TeachingLanguage.GetHashCode();
                if (StudyLoad != null)
                    hashCode = hashCode * 59 + StudyLoad.GetHashCode();
                if (QualificationAwarded != null)
                    hashCode = hashCode * 59 + QualificationAwarded.GetHashCode();
                if (ModeOfStudy != null)
                    hashCode = hashCode * 59 + ModeOfStudy.GetHashCode();
                if (ModeOfDelivery != null)
                    hashCode = hashCode * 59 + ModeOfDelivery.GetHashCode();
                if (Duration != null)
                    hashCode = hashCode * 59 + Duration.GetHashCode();
                if (FirstStartDate != null)
                    hashCode = hashCode * 59 + FirstStartDate.GetHashCode();
                if (LevelOfQualification != null)
                    hashCode = hashCode * 59 + LevelOfQualification.GetHashCode();
                if (Level != null)
                    hashCode = hashCode * 59 + Level.GetHashCode();
                if (Sector != null)
                    hashCode = hashCode * 59 + Sector.GetHashCode();
                if (FieldsOfStudy != null)
                    hashCode = hashCode * 59 + FieldsOfStudy.GetHashCode();
                if (Enrollment != null)
                    hashCode = hashCode * 59 + Enrollment.GetHashCode();
                if (Resources != null)
                    hashCode = hashCode * 59 + Resources.GetHashCode();
                if (LearningOutcomes != null)
                    hashCode = hashCode * 59 + LearningOutcomes.GetHashCode();
                if (Assessment != null)
                    hashCode = hashCode * 59 + Assessment.GetHashCode();
                if (AdmissionRequirements != null)
                    hashCode = hashCode * 59 + AdmissionRequirements.GetHashCode();
                if (QualificationRequirements != null)
                    hashCode = hashCode * 59 + QualificationRequirements.GetHashCode();
                if (Link != null)
                    hashCode = hashCode * 59 + Link.GetHashCode();
                if (EducationSpecification != null)
                    hashCode = hashCode * 59 + EducationSpecification.GetHashCode();
                if (OtherCodes != null)
                    hashCode = hashCode * 59 + OtherCodes.GetHashCode();
                if (Addresses != null)
                    hashCode = hashCode * 59 + Addresses.GetHashCode();
                if (Parent != null)
                    hashCode = hashCode * 59 + Parent.GetHashCode();
                if (Children != null)
                    hashCode = hashCode * 59 + Children.GetHashCode();
                if (Coordinators != null)
                    hashCode = hashCode * 59 + Coordinators.GetHashCode();
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

        public static bool operator ==(Program left, Program right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Program left, Program right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
