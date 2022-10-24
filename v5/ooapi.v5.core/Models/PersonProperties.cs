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
    /// A person that has a relationship with this institution
    /// </summary>
    [DataContract]
    public partial class PersonProperties : IEquatable<PersonProperties>
    {
        /// <summary>
        /// Gets or Sets PrimaryCode
        /// </summary>
        [Required]

        [DataMember(Name = "primaryCode")]
        public PrimaryCode PrimaryCode { get; set; }

        /// <summary>
        /// The first name of this person
        /// </summary>
        /// <value>The first name of this person</value>
        [Required]

        [MaxLength(256)]
        [DataMember(Name = "givenName")]
        public string GivenName { get; set; }

        /// <summary>
        /// The prefix of the family name of this person
        /// </summary>
        /// <value>The prefix of the family name of this person</value>

        [DataMember(Name = "surnamePrefix")]
        public string SurnamePrefix { get; set; }

        /// <summary>
        /// The family name of this person
        /// </summary>
        /// <value>The family name of this person</value>
        [Required]

        [MaxLength(256)]
        [DataMember(Name = "surname")]
        public string Surname { get; set; }

        /// <summary>
        /// The name of this person which will be displayed
        /// </summary>
        /// <value>The name of this person which will be displayed</value>
        [Required]

        [MaxLength(256)]
        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// The initials of this person
        /// </summary>
        /// <value>The initials of this person</value>

        [DataMember(Name = "initials")]
        public string Initials { get; set; }

        /// <summary>
        /// Whether this person has an active enrollment.
        /// </summary>
        /// <value>Whether this person has an active enrollment.</value>
        [Required]

        [DataMember(Name = "activeEnrollment")]
        public bool? ActiveEnrollment { get; set; }

        /// <summary>
        /// The date of birth of this person, RFC3339 (full-date)
        /// </summary>
        /// <value>The date of birth of this person, RFC3339 (full-date)</value>

        [DataMember(Name = "dateOfBirth")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// The city of birth of this person
        /// </summary>
        /// <value>The city of birth of this person</value>

        [DataMember(Name = "cityOfBirth")]
        public string CityOfBirth { get; set; }

        /// <summary>
        /// The country of birth of this person the country code according to [iso-3166-1-alpha-2](https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2)
        /// </summary>
        /// <value>The country of birth of this person the country code according to [iso-3166-1-alpha-2](https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2)</value>

        [DataMember(Name = "countryOfBirth")]
        public string CountryOfBirth { get; set; }

        /// <summary>
        /// The nationality of this person the nationality according to https://gist.github.com/zspine/2365808
        /// </summary>
        /// <value>The nationality of this person the nationality according to https://gist.github.com/zspine/2365808</value>

        [DataMember(Name = "nationality")]
        public string Nationality { get; set; }

        /// <summary>
        /// The date of nationality of this person, RFC3339 (full-date)
        /// </summary>
        /// <value>The date of nationality of this person, RFC3339 (full-date)</value>

        [DataMember(Name = "dateOfNationality")]
        public DateTime? DateOfNationality { get; set; }



        /// <summary>
        /// The affiliations of this person, the relations a person has with the organization providing this endpoint - student: student - employee: medewerker - guest: gast 
        /// </summary>
        /// <value>The affiliations of this person, the relations a person has with the organization providing this endpoint - student: student - employee: medewerker - guest: gast </value>
        [Required]

        [DataMember(Name = "affiliations")]
        public List<AffiliationsEnum> Affiliations { get; set; }

        /// <summary>
        /// The primary e-mailaddress of this person
        /// </summary>
        /// <value>The primary e-mailaddress of this person</value>
        [Required]

        [DataMember(Name = "mail")]
        public string Mail { get; set; }

        /// <summary>
        /// The secondary e-mailaddress of this person
        /// </summary>
        /// <value>The secondary e-mailaddress of this person</value>

        [DataMember(Name = "secondaryMail")]
        public string SecondaryMail { get; set; }

        /// <summary>
        /// The telephone number of this person
        /// </summary>
        /// <value>The telephone number of this person</value>

        [MaxLength(256)]
        [DataMember(Name = "telephoneNumber")]
        public string TelephoneNumber { get; set; }

        /// <summary>
        /// The mobile number of this person
        /// </summary>
        /// <value>The mobile number of this person</value>

        [MaxLength(256)]
        [DataMember(Name = "mobileNumber")]
        public string MobileNumber { get; set; }

        /// <summary>
        /// The url of the informal picture of this person
        /// </summary>
        /// <value>The url of the informal picture of this person</value>

        [MaxLength(2048)]
        [DataMember(Name = "photoSocial")]
        public string PhotoSocial { get; set; }

        /// <summary>
        /// The url of the official picture of this person
        /// </summary>
        /// <value>The url of the official picture of this person</value>

        [MaxLength(2048)]
        [DataMember(Name = "photoOfficial")]
        public string PhotoOfficial { get; set; }


        /// <summary>
        /// The gender of this person
        /// </summary>
        /// <value>The gender of this person</value>

        [DataMember(Name = "gender")]
        public GenderEnum? Gender { get; set; }

        /// <summary>
        /// A title prefix to be used for this person
        /// </summary>
        /// <value>A title prefix to be used for this person</value>

        [DataMember(Name = "titlePrefix")]
        public string TitlePrefix { get; set; }

        /// <summary>
        /// A title suffix to be used for this person
        /// </summary>
        /// <value>A title suffix to be used for this person</value>

        [DataMember(Name = "titleSuffix")]
        public string TitleSuffix { get; set; }

        /// <summary>
        /// The name of the office where this person is located
        /// </summary>
        /// <value>The name of the office where this person is located</value>

        [DataMember(Name = "office")]
        public string Office { get; set; }

        /// <summary>
        /// Gets or Sets Address
        /// </summary>

        [DataMember(Name = "address")]
        public Address Address { get; set; }

        /// <summary>
        /// Full name of In Case of Emergency contact
        /// </summary>
        /// <value>Full name of In Case of Emergency contact</value>

        [MaxLength(256)]
        [DataMember(Name = "ICEName")]
        public string ICEName { get; set; }

        /// <summary>
        /// Phone number of In Case of Emergency contact
        /// </summary>
        /// <value>Phone number of In Case of Emergency contact</value>

        [MaxLength(256)]
        [DataMember(Name = "ICEPhoneNumber")]
        public string ICEPhoneNumber { get; set; }



        /// <summary>
        /// Type of relation between person and In Case of Emergency contact
        /// </summary>
        /// <value>Type of relation between person and In Case of Emergency contact</value>

        [DataMember(Name = "ICERelation")]
        public ICERelationEnum? ICERelation { get; set; }

        /// <summary>
        /// The language(s) of choice for this person, RFC3066
        /// </summary>
        /// <value>The language(s) of choice for this person, RFC3066</value>

        [DataMember(Name = "languageOfChoice")]
        public List<string> LanguageOfChoice { get; set; }

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
            sb.Append("class PersonProperties {\n");
            sb.Append("  PrimaryCode: ").Append(PrimaryCode).Append("\n");
            sb.Append("  GivenName: ").Append(GivenName).Append("\n");
            sb.Append("  SurnamePrefix: ").Append(SurnamePrefix).Append("\n");
            sb.Append("  Surname: ").Append(Surname).Append("\n");
            sb.Append("  DisplayName: ").Append(DisplayName).Append("\n");
            sb.Append("  Initials: ").Append(Initials).Append("\n");
            sb.Append("  ActiveEnrollment: ").Append(ActiveEnrollment).Append("\n");
            sb.Append("  DateOfBirth: ").Append(DateOfBirth).Append("\n");
            sb.Append("  CityOfBirth: ").Append(CityOfBirth).Append("\n");
            sb.Append("  CountryOfBirth: ").Append(CountryOfBirth).Append("\n");
            sb.Append("  Nationality: ").Append(Nationality).Append("\n");
            sb.Append("  DateOfNationality: ").Append(DateOfNationality).Append("\n");
            sb.Append("  Affiliations: ").Append(Affiliations).Append("\n");
            sb.Append("  Mail: ").Append(Mail).Append("\n");
            sb.Append("  SecondaryMail: ").Append(SecondaryMail).Append("\n");
            sb.Append("  TelephoneNumber: ").Append(TelephoneNumber).Append("\n");
            sb.Append("  MobileNumber: ").Append(MobileNumber).Append("\n");
            sb.Append("  PhotoSocial: ").Append(PhotoSocial).Append("\n");
            sb.Append("  PhotoOfficial: ").Append(PhotoOfficial).Append("\n");
            sb.Append("  Gender: ").Append(Gender).Append("\n");
            sb.Append("  TitlePrefix: ").Append(TitlePrefix).Append("\n");
            sb.Append("  TitleSuffix: ").Append(TitleSuffix).Append("\n");
            sb.Append("  Office: ").Append(Office).Append("\n");
            sb.Append("  Address: ").Append(Address).Append("\n");
            sb.Append("  ICEName: ").Append(ICEName).Append("\n");
            sb.Append("  ICEPhoneNumber: ").Append(ICEPhoneNumber).Append("\n");
            sb.Append("  ICERelation: ").Append(ICERelation).Append("\n");
            sb.Append("  LanguageOfChoice: ").Append(LanguageOfChoice).Append("\n");
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
            return obj.GetType() == GetType() && Equals((PersonProperties)obj);
        }

        /// <summary>
        /// Returns true if PersonProperties instances are equal
        /// </summary>
        /// <param name="other">Instance of PersonProperties to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PersonProperties other)
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
                    GivenName == other.GivenName ||
                    GivenName != null &&
                    GivenName.Equals(other.GivenName)
                ) &&
                (
                    SurnamePrefix == other.SurnamePrefix ||
                    SurnamePrefix != null &&
                    SurnamePrefix.Equals(other.SurnamePrefix)
                ) &&
                (
                    Surname == other.Surname ||
                    Surname != null &&
                    Surname.Equals(other.Surname)
                ) &&
                (
                    DisplayName == other.DisplayName ||
                    DisplayName != null &&
                    DisplayName.Equals(other.DisplayName)
                ) &&
                (
                    Initials == other.Initials ||
                    Initials != null &&
                    Initials.Equals(other.Initials)
                ) &&
                (
                    ActiveEnrollment == other.ActiveEnrollment ||
                    ActiveEnrollment != null &&
                    ActiveEnrollment.Equals(other.ActiveEnrollment)
                ) &&
                (
                    DateOfBirth == other.DateOfBirth ||
                    DateOfBirth != null &&
                    DateOfBirth.Equals(other.DateOfBirth)
                ) &&
                (
                    CityOfBirth == other.CityOfBirth ||
                    CityOfBirth != null &&
                    CityOfBirth.Equals(other.CityOfBirth)
                ) &&
                (
                    CountryOfBirth == other.CountryOfBirth ||
                    CountryOfBirth != null &&
                    CountryOfBirth.Equals(other.CountryOfBirth)
                ) &&
                (
                    Nationality == other.Nationality ||
                    Nationality != null &&
                    Nationality.Equals(other.Nationality)
                ) &&
                (
                    DateOfNationality == other.DateOfNationality ||
                    DateOfNationality != null &&
                    DateOfNationality.Equals(other.DateOfNationality)
                ) &&
                (
                    Affiliations == other.Affiliations ||
                    Affiliations != null &&
                    Affiliations.SequenceEqual(other.Affiliations)
                ) &&
                (
                    Mail == other.Mail ||
                    Mail != null &&
                    Mail.Equals(other.Mail)
                ) &&
                (
                    SecondaryMail == other.SecondaryMail ||
                    SecondaryMail != null &&
                    SecondaryMail.Equals(other.SecondaryMail)
                ) &&
                (
                    TelephoneNumber == other.TelephoneNumber ||
                    TelephoneNumber != null &&
                    TelephoneNumber.Equals(other.TelephoneNumber)
                ) &&
                (
                    MobileNumber == other.MobileNumber ||
                    MobileNumber != null &&
                    MobileNumber.Equals(other.MobileNumber)
                ) &&
                (
                    PhotoSocial == other.PhotoSocial ||
                    PhotoSocial != null &&
                    PhotoSocial.Equals(other.PhotoSocial)
                ) &&
                (
                    PhotoOfficial == other.PhotoOfficial ||
                    PhotoOfficial != null &&
                    PhotoOfficial.Equals(other.PhotoOfficial)
                ) &&
                (
                    Gender == other.Gender ||
                    Gender != null &&
                    Gender.Equals(other.Gender)
                ) &&
                (
                    TitlePrefix == other.TitlePrefix ||
                    TitlePrefix != null &&
                    TitlePrefix.Equals(other.TitlePrefix)
                ) &&
                (
                    TitleSuffix == other.TitleSuffix ||
                    TitleSuffix != null &&
                    TitleSuffix.Equals(other.TitleSuffix)
                ) &&
                (
                    Office == other.Office ||
                    Office != null &&
                    Office.Equals(other.Office)
                ) &&
                (
                    Address == other.Address ||
                    Address != null &&
                    Address.Equals(other.Address)
                ) &&
                (
                    ICEName == other.ICEName ||
                    ICEName != null &&
                    ICEName.Equals(other.ICEName)
                ) &&
                (
                    ICEPhoneNumber == other.ICEPhoneNumber ||
                    ICEPhoneNumber != null &&
                    ICEPhoneNumber.Equals(other.ICEPhoneNumber)
                ) &&
                (
                    ICERelation == other.ICERelation ||
                    ICERelation != null &&
                    ICERelation.Equals(other.ICERelation)
                ) &&
                (
                    LanguageOfChoice == other.LanguageOfChoice ||
                    LanguageOfChoice != null &&
                    LanguageOfChoice.SequenceEqual(other.LanguageOfChoice)
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
                if (GivenName != null)
                    hashCode = hashCode * 59 + GivenName.GetHashCode();
                if (SurnamePrefix != null)
                    hashCode = hashCode * 59 + SurnamePrefix.GetHashCode();
                if (Surname != null)
                    hashCode = hashCode * 59 + Surname.GetHashCode();
                if (DisplayName != null)
                    hashCode = hashCode * 59 + DisplayName.GetHashCode();
                if (Initials != null)
                    hashCode = hashCode * 59 + Initials.GetHashCode();
                if (ActiveEnrollment != null)
                    hashCode = hashCode * 59 + ActiveEnrollment.GetHashCode();
                if (DateOfBirth != null)
                    hashCode = hashCode * 59 + DateOfBirth.GetHashCode();
                if (CityOfBirth != null)
                    hashCode = hashCode * 59 + CityOfBirth.GetHashCode();
                if (CountryOfBirth != null)
                    hashCode = hashCode * 59 + CountryOfBirth.GetHashCode();
                if (Nationality != null)
                    hashCode = hashCode * 59 + Nationality.GetHashCode();
                if (DateOfNationality != null)
                    hashCode = hashCode * 59 + DateOfNationality.GetHashCode();
                if (Affiliations != null)
                    hashCode = hashCode * 59 + Affiliations.GetHashCode();
                if (Mail != null)
                    hashCode = hashCode * 59 + Mail.GetHashCode();
                if (SecondaryMail != null)
                    hashCode = hashCode * 59 + SecondaryMail.GetHashCode();
                if (TelephoneNumber != null)
                    hashCode = hashCode * 59 + TelephoneNumber.GetHashCode();
                if (MobileNumber != null)
                    hashCode = hashCode * 59 + MobileNumber.GetHashCode();
                if (PhotoSocial != null)
                    hashCode = hashCode * 59 + PhotoSocial.GetHashCode();
                if (PhotoOfficial != null)
                    hashCode = hashCode * 59 + PhotoOfficial.GetHashCode();
                if (Gender != null)
                    hashCode = hashCode * 59 + Gender.GetHashCode();
                if (TitlePrefix != null)
                    hashCode = hashCode * 59 + TitlePrefix.GetHashCode();
                if (TitleSuffix != null)
                    hashCode = hashCode * 59 + TitleSuffix.GetHashCode();
                if (Office != null)
                    hashCode = hashCode * 59 + Office.GetHashCode();
                if (Address != null)
                    hashCode = hashCode * 59 + Address.GetHashCode();
                if (ICEName != null)
                    hashCode = hashCode * 59 + ICEName.GetHashCode();
                if (ICEPhoneNumber != null)
                    hashCode = hashCode * 59 + ICEPhoneNumber.GetHashCode();
                if (ICERelation != null)
                    hashCode = hashCode * 59 + ICERelation.GetHashCode();
                if (LanguageOfChoice != null)
                    hashCode = hashCode * 59 + LanguageOfChoice.GetHashCode();
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

        public static bool operator ==(PersonProperties left, PersonProperties right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PersonProperties left, PersonProperties right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
