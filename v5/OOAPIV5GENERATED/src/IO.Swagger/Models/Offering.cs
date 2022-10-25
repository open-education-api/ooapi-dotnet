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
    public partial class Offering : IEquatable<Offering>
    {
        /// <summary>
        /// Gets or Sets PrimaryCode
        /// </summary>
        [JsonRequired]

        [DataMember(Name = "primaryCode")]
        public PrimaryCode PrimaryCode { get; set; }



        /// <summary>
        /// The type of this offering
        /// </summary>
        /// <value>The type of this offering</value>
        [JsonRequired]

        [DataMember(Name = "offeringType")]
        public OfferingTypeEnum? OfferingType { get; set; }

        /// <summary>
        /// Gets or Sets AcademicSession
        /// </summary>

        [DataMember(Name = "academicSession")]
        public OneOfAcademicSession AcademicSession { get; set; }

        /// <summary>
        /// The name of this offering
        /// </summary>
        /// <value>The name of this offering</value>
        [JsonRequired]

        [DataMember(Name = "name")]
        public List<LanguageValueItem> Name { get; set; }

        /// <summary>
        /// The abbreviation or internal code used to identify this offering
        /// </summary>
        /// <value>The abbreviation or internal code used to identify this offering</value>

        [MaxLength(256)]
        [DataMember(Name = "abbreviation")]
        public string Abbreviation { get; set; }

        /// <summary>
        /// The description of this offering.
        /// </summary>
        /// <value>The description of this offering.</value>
        [JsonRequired]

        [DataMember(Name = "description")]
        public List<LanguageValueItem> Description { get; set; }

        /// <summary>
        /// The (primary) teaching language in which this offering is given, should be a three-letter language code as specified by ISO 639-2.
        /// </summary>
        /// <value>The (primary) teaching language in which this offering is given, should be a three-letter language code as specified by ISO 639-2.</value>
        [JsonRequired]
        [RegularExpression("/^[a-z]{3}$/")]
        [StringLength(3, MinimumLength = 3)]
        [DataMember(Name = "teachingLanguage")]
        public string TeachingLanguage { get; set; }


        /// <summary>
        /// The mode of delivery of the component (ECTS-mode of delivery) - distance-learning: afstandsleren - on campus: op de campus - online: online - hybrid: hybride - situated: op locatie 
        /// </summary>
        /// <value>The mode of delivery of the component (ECTS-mode of delivery) - distance-learning: afstandsleren - on campus: op de campus - online: online - hybrid: hybride - situated: op locatie </value>

        [DataMember(Name = "modeOfDelivery")]
        public List<ModeOfDeliveryEnum> ModeOfDelivery { get; set; }

        /// <summary>
        /// The maximum number of students allowed to enroll for this offering
        /// </summary>
        /// <value>The maximum number of students allowed to enroll for this offering</value>

        [DataMember(Name = "maxNumberStudents")]
        public decimal? MaxNumberStudents { get; set; }

        /// <summary>
        /// The number of students that have already enrolled for this offering
        /// </summary>
        /// <value>The number of students that have already enrolled for this offering</value>

        [DataMember(Name = "enrolledNumberStudents")]
        public decimal? EnrolledNumberStudents { get; set; }

        /// <summary>
        /// The number of students that have a pending enrollment request for this offering
        /// </summary>
        /// <value>The number of students that have a pending enrollment request for this offering</value>

        [DataMember(Name = "pendingNumberStudents")]
        public decimal? PendingNumberStudents { get; set; }

        /// <summary>
        /// The minimum number of students needed for this offering to proceed
        /// </summary>
        /// <value>The minimum number of students needed for this offering to proceed</value>

        [DataMember(Name = "minNumberStudents")]
        public decimal? MinNumberStudents { get; set; }

        /// <summary>
        /// resultExpected, previously knwon as isLineItem is used so the specific instance of the object is identified as being an element that CAN contain “grade” information. Offerings do not always have to result in a grade or an other type of result.  If there is a result expected from a programOffering/courseOffering/componentOffering the is resultExpected field should parse true 
        /// </summary>
        /// <value>resultExpected, previously knwon as isLineItem is used so the specific instance of the object is identified as being an element that CAN contain “grade” information. Offerings do not always have to result in a grade or an other type of result.  If there is a result expected from a programOffering/courseOffering/componentOffering the is resultExpected field should parse true </value>
        [JsonRequired]

        [DataMember(Name = "resultExpected")]
        public bool? ResultExpected { get; set; }



        /// <summary>
        /// The result value type for this offering
        /// </summary>
        /// <value>The result value type for this offering</value>

        [DataMember(Name = "resultValueType")]
        public ResultValueTypeEnum? ResultValueType { get; set; }

        /// <summary>
        /// URL of this offering&#x27;s webpage.
        /// </summary>
        /// <value>URL of this offering&#x27;s webpage.</value>

        [MaxLength(2048)]
        [DataMember(Name = "link")]
        public string Link { get; set; }

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
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Offering {\n");
            sb.Append("  PrimaryCode: ").Append(PrimaryCode).Append("\n");
            sb.Append("  OfferingType: ").Append(OfferingType).Append("\n");
            sb.Append("  AcademicSession: ").Append(AcademicSession).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Abbreviation: ").Append(Abbreviation).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  TeachingLanguage: ").Append(TeachingLanguage).Append("\n");
            sb.Append("  ModeOfDelivery: ").Append(ModeOfDelivery).Append("\n");
            sb.Append("  MaxNumberStudents: ").Append(MaxNumberStudents).Append("\n");
            sb.Append("  EnrolledNumberStudents: ").Append(EnrolledNumberStudents).Append("\n");
            sb.Append("  PendingNumberStudents: ").Append(PendingNumberStudents).Append("\n");
            sb.Append("  MinNumberStudents: ").Append(MinNumberStudents).Append("\n");
            sb.Append("  ResultExpected: ").Append(ResultExpected).Append("\n");
            sb.Append("  ResultValueType: ").Append(ResultValueType).Append("\n");
            sb.Append("  Link: ").Append(Link).Append("\n");
            sb.Append("  OtherCodes: ").Append(OtherCodes).Append("\n");
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
            return obj.GetType() == GetType() && Equals((Offering)obj);
        }

        /// <summary>
        /// Returns true if Offering instances are equal
        /// </summary>
        /// <param name="other">Instance of Offering to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Offering other)
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
                    OfferingType == other.OfferingType ||
                    OfferingType != null &&
                    OfferingType.Equals(other.OfferingType)
                ) &&
                (
                    AcademicSession == other.AcademicSession ||
                    AcademicSession != null &&
                    AcademicSession.Equals(other.AcademicSession)
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
                    ModeOfDelivery == other.ModeOfDelivery ||
                    ModeOfDelivery != null &&
                    ModeOfDelivery.SequenceEqual(other.ModeOfDelivery)
                ) &&
                (
                    MaxNumberStudents == other.MaxNumberStudents ||
                    MaxNumberStudents != null &&
                    MaxNumberStudents.Equals(other.MaxNumberStudents)
                ) &&
                (
                    EnrolledNumberStudents == other.EnrolledNumberStudents ||
                    EnrolledNumberStudents != null &&
                    EnrolledNumberStudents.Equals(other.EnrolledNumberStudents)
                ) &&
                (
                    PendingNumberStudents == other.PendingNumberStudents ||
                    PendingNumberStudents != null &&
                    PendingNumberStudents.Equals(other.PendingNumberStudents)
                ) &&
                (
                    MinNumberStudents == other.MinNumberStudents ||
                    MinNumberStudents != null &&
                    MinNumberStudents.Equals(other.MinNumberStudents)
                ) &&
                (
                    ResultExpected == other.ResultExpected ||
                    ResultExpected != null &&
                    ResultExpected.Equals(other.ResultExpected)
                ) &&
                (
                    ResultValueType == other.ResultValueType ||
                    ResultValueType != null &&
                    ResultValueType.Equals(other.ResultValueType)
                ) &&
                (
                    Link == other.Link ||
                    Link != null &&
                    Link.Equals(other.Link)
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
                if (OfferingType != null)
                    hashCode = hashCode * 59 + OfferingType.GetHashCode();
                if (AcademicSession != null)
                    hashCode = hashCode * 59 + AcademicSession.GetHashCode();
                if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                if (Abbreviation != null)
                    hashCode = hashCode * 59 + Abbreviation.GetHashCode();
                if (Description != null)
                    hashCode = hashCode * 59 + Description.GetHashCode();
                if (TeachingLanguage != null)
                    hashCode = hashCode * 59 + TeachingLanguage.GetHashCode();
                if (ModeOfDelivery != null)
                    hashCode = hashCode * 59 + ModeOfDelivery.GetHashCode();
                if (MaxNumberStudents != null)
                    hashCode = hashCode * 59 + MaxNumberStudents.GetHashCode();
                if (EnrolledNumberStudents != null)
                    hashCode = hashCode * 59 + EnrolledNumberStudents.GetHashCode();
                if (PendingNumberStudents != null)
                    hashCode = hashCode * 59 + PendingNumberStudents.GetHashCode();
                if (MinNumberStudents != null)
                    hashCode = hashCode * 59 + MinNumberStudents.GetHashCode();
                if (ResultExpected != null)
                    hashCode = hashCode * 59 + ResultExpected.GetHashCode();
                if (ResultValueType != null)
                    hashCode = hashCode * 59 + ResultValueType.GetHashCode();
                if (Link != null)
                    hashCode = hashCode * 59 + Link.GetHashCode();
                if (OtherCodes != null)
                    hashCode = hashCode * 59 + OtherCodes.GetHashCode();
                if (Consumers != null)
                    hashCode = hashCode * 59 + Consumers.GetHashCode();
                if (Ext != null)
                    hashCode = hashCode * 59 + Ext.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(Offering left, Offering right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Offering left, Offering right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
