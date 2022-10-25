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
    /// A component is a part of a course
    /// </summary>
    [DataContract]
    public partial class Component : IEquatable<Component>
    {
        /// <summary>
        /// Unique id of this component
        /// </summary>
        /// <value>Unique id of this component</value>
        [JsonRequired]

        [DataMember(Name = "componentId")]
        public Guid ComponentId { get; set; }

        /// <summary>
        /// Gets or Sets PrimaryCode
        /// </summary>
        [JsonRequired]

        [DataMember(Name = "primaryCode")]
        public PrimaryCode PrimaryCode { get; set; }


        /// <summary>
        /// The component type - test: tentamen - lecture: college - practical: practicum - tutorial: werkcollege - consultation: consultatie - project: project - workshop: workshop - excursion: excursie - independent study: zelfstudie - external: extern - skills training: vaardighedentraining 
        /// </summary>
        /// <value>The component type - test: tentamen - lecture: college - practical: practicum - tutorial: werkcollege - consultation: consultatie - project: project - workshop: workshop - excursion: excursie - independent study: zelfstudie - external: extern - skills training: vaardighedentraining </value>
        [JsonRequired]

        [DataMember(Name = "componentType")]
        public ComponentTypeEnum? ComponentType { get; set; }

        /// <summary>
        /// The name of this component
        /// </summary>
        /// <value>The name of this component</value>
        [JsonRequired]

        [DataMember(Name = "name")]
        public List<LanguageValueItem> Name { get; set; }

        /// <summary>
        /// The abbreviation of this component
        /// </summary>
        /// <value>The abbreviation of this component</value>
        [JsonRequired]

        [MaxLength(256)]
        [DataMember(Name = "abbreviation")]
        public string Abbreviation { get; set; }

        /// <summary>
        /// The mode of delivery of the component (ECTS-mode of delivery) - distance-learning: afstandsleren - on campus: op de campus - online: online - hybrid: hybride - situated: op locatie 
        /// </summary>
        /// <value>The mode of delivery of the component (ECTS-mode of delivery) - distance-learning: afstandsleren - on campus: op de campus - online: online - hybrid: hybride - situated: op locatie </value>

        [DataMember(Name = "modeOfDelivery")]
        public List<ModeOfDeliveryEnum> ModeOfDelivery { get; set; }

        /// <summary>
        /// The duration of this component. The duration format is from the ISO 8601 ABNF as given in Appendix A of RFC 3339.
        /// </summary>
        /// <value>The duration of this component. The duration format is from the ISO 8601 ABNF as given in Appendix A of RFC 3339.</value>
        [RegularExpression("/^(-?)P(?=\\d|T\\d)(?:(\\d+)Y)?(?:(\\d+)M)?(?:(\\d+)([DW]))?(?:T(?:(\\d+)H)?(?:(\\d+)M)?(?:(\\d+(?:\\.\\d+)?)S)?)?$/")]
        [DataMember(Name = "duration")]
        public string Duration { get; set; }

        /// <summary>
        /// The description of this component.
        /// </summary>
        /// <value>The description of this component.</value>

        [DataMember(Name = "description")]
        public List<LanguageValueItem> Description { get; set; }

        /// <summary>
        /// The (primary) teaching language in which this component is given, should be a three-letter language code as specified by ISO 639-2.
        /// </summary>
        /// <value>The (primary) teaching language in which this component is given, should be a three-letter language code as specified by ISO 639-2.</value>
        [JsonRequired]
        [RegularExpression("/^[a-z]{3}$/")]
        [StringLength(3, MinimumLength = 3)]
        [DataMember(Name = "teachingLanguage")]
        public string TeachingLanguage { get; set; }

        /// <summary>
        /// Statements that describe the knowledge or skills students should acquire by the end of a particular course (ECTS-learningoutcome).
        /// </summary>
        /// <value>Statements that describe the knowledge or skills students should acquire by the end of a particular course (ECTS-learningoutcome).</value>

        [DataMember(Name = "learningOutcomes")]
        public List<List<Object>> LearningOutcomes { get; set; }

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
        /// Addresses for this component
        /// </summary>
        /// <value>Addresses for this component</value>

        [DataMember(Name = "addresses")]
        public List<Address> Addresses { get; set; }

        /// <summary>
        /// An array of additional human readable codes/identifiers for the entity being described.
        /// </summary>
        /// <value>An array of additional human readable codes/identifiers for the entity being described.</value>

        [DataMember(Name = "otherCodes")]
        public List<OtherCodes> OtherCodes { get; set; }

        /// <summary>
        /// The course of which this component is a part. [&#x60;expandable&#x60;](#tag/course_model) By default only the &#x60;courseId&#x60; (a string) is returned. If the client requested an expansion of &#x60;course&#x60; the full course object should be returned. 
        /// </summary>
        /// <value>The course of which this component is a part. [&#x60;expandable&#x60;](#tag/course_model) By default only the &#x60;courseId&#x60; (a string) is returned. If the client requested an expansion of &#x60;course&#x60; the full course object should be returned. </value>

        [DataMember(Name = "course")]
        public OneOfCourse Course { get; set; }

        /// <summary>
        /// The organization which provides this component. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
        /// </summary>
        /// <value>The organization which provides this component. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

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
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Component {\n");
            sb.Append("  ComponentId: ").Append(ComponentId).Append("\n");
            sb.Append("  PrimaryCode: ").Append(PrimaryCode).Append("\n");
            sb.Append("  ComponentType: ").Append(ComponentType).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Abbreviation: ").Append(Abbreviation).Append("\n");
            sb.Append("  ModeOfDelivery: ").Append(ModeOfDelivery).Append("\n");
            sb.Append("  Duration: ").Append(Duration).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  TeachingLanguage: ").Append(TeachingLanguage).Append("\n");
            sb.Append("  LearningOutcomes: ").Append(LearningOutcomes).Append("\n");
            sb.Append("  Enrollment: ").Append(Enrollment).Append("\n");
            sb.Append("  Resources: ").Append(Resources).Append("\n");
            sb.Append("  Assessment: ").Append(Assessment).Append("\n");
            sb.Append("  Addresses: ").Append(Addresses).Append("\n");
            sb.Append("  OtherCodes: ").Append(OtherCodes).Append("\n");
            sb.Append("  Course: ").Append(Course).Append("\n");
            sb.Append("  Organization: ").Append(Organization).Append("\n");
            sb.Append("  Consumers: ").Append(Consumers).Append("\n");
            sb.Append("  Ext: ").Append(Ext).Append("\n");
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
            return obj.GetType() == GetType() && Equals((Component)obj);
        }

        /// <summary>
        /// Returns true if Component instances are equal
        /// </summary>
        /// <param name="other">Instance of Component to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Component other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    ComponentId == other.ComponentId ||
                    ComponentId != null &&
                    ComponentId.Equals(other.ComponentId)
                ) &&
                (
                    PrimaryCode == other.PrimaryCode ||
                    PrimaryCode != null &&
                    PrimaryCode.Equals(other.PrimaryCode)
                ) &&
                (
                    ComponentType == other.ComponentType ||
                    ComponentType != null &&
                    ComponentType.Equals(other.ComponentType)
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
                    LearningOutcomes == other.LearningOutcomes ||
                    LearningOutcomes != null &&
                    LearningOutcomes.SequenceEqual(other.LearningOutcomes)
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
                    Course == other.Course ||
                    Course != null &&
                    Course.Equals(other.Course)
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
                if (ComponentId != null)
                    hashCode = hashCode * 59 + ComponentId.GetHashCode();
                if (PrimaryCode != null)
                    hashCode = hashCode * 59 + PrimaryCode.GetHashCode();
                if (ComponentType != null)
                    hashCode = hashCode * 59 + ComponentType.GetHashCode();
                if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                if (Abbreviation != null)
                    hashCode = hashCode * 59 + Abbreviation.GetHashCode();
                if (ModeOfDelivery != null)
                    hashCode = hashCode * 59 + ModeOfDelivery.GetHashCode();
                if (Duration != null)
                    hashCode = hashCode * 59 + Duration.GetHashCode();
                if (Description != null)
                    hashCode = hashCode * 59 + Description.GetHashCode();
                if (TeachingLanguage != null)
                    hashCode = hashCode * 59 + TeachingLanguage.GetHashCode();
                if (LearningOutcomes != null)
                    hashCode = hashCode * 59 + LearningOutcomes.GetHashCode();
                if (Enrollment != null)
                    hashCode = hashCode * 59 + Enrollment.GetHashCode();
                if (Resources != null)
                    hashCode = hashCode * 59 + Resources.GetHashCode();
                if (Assessment != null)
                    hashCode = hashCode * 59 + Assessment.GetHashCode();
                if (Addresses != null)
                    hashCode = hashCode * 59 + Addresses.GetHashCode();
                if (OtherCodes != null)
                    hashCode = hashCode * 59 + OtherCodes.GetHashCode();
                if (Course != null)
                    hashCode = hashCode * 59 + Course.GetHashCode();
                if (Organization != null)
                    hashCode = hashCode * 59 + Organization.GetHashCode();
                if (Consumers != null)
                    hashCode = hashCode * 59 + Consumers.GetHashCode();
                if (Ext != null)
                    hashCode = hashCode * 59 + Ext.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(Component left, Component right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Component left, Component right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
