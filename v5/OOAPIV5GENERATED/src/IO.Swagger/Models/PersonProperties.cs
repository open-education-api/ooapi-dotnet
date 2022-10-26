using IO.Swagger.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace IO.Swagger.Models
{
    /// <summary>
    /// A person that has a relationship with this institution
    /// </summary>
    [DataContract]
    public partial class PersonProperties
    {
        /// <summary>
        /// Gets or Sets PrimaryCode
        /// </summary>
        [JsonRequired]

        [DataMember(Name = "primaryCode")]
        public PrimaryCode PrimaryCode { get; set; }

        /// <summary>
        /// The first name of this person
        /// </summary>
        /// <value>The first name of this person</value>
        [JsonRequired]

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
        [JsonRequired]

        [MaxLength(256)]
        [DataMember(Name = "surname")]
        public string Surname { get; set; }

        /// <summary>
        /// The name of this person which will be displayed
        /// </summary>
        /// <value>The name of this person which will be displayed</value>
        [JsonRequired]

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
        [JsonRequired]

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
        [JsonRequired]

        [DataMember(Name = "affiliations")]
        public List<AffiliationsEnum> Affiliations { get; set; }

        /// <summary>
        /// The primary e-mailaddress of this person
        /// </summary>
        /// <value>The primary e-mailaddress of this person</value>
        [JsonRequired]

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
    }
}
