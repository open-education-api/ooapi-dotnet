using Newtonsoft.Json;
using ooapi.v5.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class Person : ModelBase
    {

        /// <summary>
        /// Unique id of this person
        /// </summary>
        /// <value>Unique id of this person</value>
        [JsonRequired]
        [JsonProperty("personId")]
        public Guid PersonId { get; set; }

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
        /// The first name of this person
        /// </summary>
        /// <value>The first name of this person</value>
        [JsonRequired]

        [MaxLength(256)]
        [JsonProperty(PropertyName = "givenName")]
        public string GivenName { get; set; }

        /// <summary>
        /// The prefix of the family name of this person
        /// </summary>
        /// <value>The prefix of the family name of this person</value>

        [JsonProperty(PropertyName = "surnamePrefix")]
        public string SurnamePrefix { get; set; }

        /// <summary>
        /// The family name of this person
        /// </summary>
        /// <value>The family name of this person</value>
        [JsonRequired]

        [MaxLength(256)]
        [JsonProperty(PropertyName = "surname")]
        public string Surname { get; set; }

        /// <summary>
        /// The name of this person which will be displayed
        /// </summary>
        /// <value>The name of this person which will be displayed</value>
        [JsonRequired]

        [MaxLength(256)]
        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// The initials of this person
        /// </summary>
        /// <value>The initials of this person</value>

        [JsonProperty(PropertyName = "initials")]
        public string Initials { get; set; }

        /// <summary>
        /// Whether this person has an active enrollment.
        /// </summary>
        /// <value>Whether this person has an active enrollment.</value>
        [JsonRequired]

        [JsonProperty(PropertyName = "activeEnrollment")]
        public bool? ActiveEnrollment { get; set; }

        /// <summary>
        /// The date of birth of this person, RFC3339 (full-date)
        /// </summary>
        /// <value>The date of birth of this person, RFC3339 (full-date)</value>

        [JsonProperty(PropertyName = "dateOfBirth")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// The city of birth of this person
        /// </summary>
        /// <value>The city of birth of this person</value>

        [JsonProperty(PropertyName = "cityOfBirth")]
        public string CityOfBirth { get; set; }

        /// <summary>
        /// The country of birth of this person the country code according to [iso-3166-1-alpha-2](https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2)
        /// </summary>
        /// <value>The country of birth of this person the country code according to [iso-3166-1-alpha-2](https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2)</value>

        [JsonProperty(PropertyName = "countryOfBirth")]
        public string CountryOfBirth { get; set; }

        /// <summary>
        /// The nationality of this person the nationality according to https://gist.github.com/zspine/2365808
        /// </summary>
        /// <value>The nationality of this person the nationality according to https://gist.github.com/zspine/2365808</value>

        [JsonProperty(PropertyName = "nationality")]
        public string Nationality { get; set; }

        /// <summary>
        /// The date of nationality of this person, RFC3339 (full-date)
        /// </summary>
        /// <value>The date of nationality of this person, RFC3339 (full-date)</value>

        [JsonProperty(PropertyName = "dateOfNationality")]
        public DateTime? DateOfNationality { get; set; }

        [JsonIgnore]
        public string Affiliations { get; set; }


        /// <summary>
        /// The affiliations of this person, the relations a person has with the organization providing this endpoint - student: student - employee: medewerker - guest: gast 
        /// </summary>
        /// <value>The affiliations of this person, the relations a person has with the organization providing this endpoint - student: student - employee: medewerker - guest: gast </value>
        [JsonRequired]
        [JsonProperty(PropertyName = "affiliations")]
        [NotMapped]
        public List<AffiliationsEnum> Affs
        {
            get
            {
                if (!string.IsNullOrEmpty(Affiliations))
                {
                    List<AffiliationsEnum> list = new List<AffiliationsEnum>();
                    var affiliations = Affiliations.Split(',');
                    foreach (var affiliation in affiliations)
                    {
                        switch (affiliation)
                        {
                            case "student":
                                {
                                    list.Add(AffiliationsEnum.StudentEnum);
                                    break;
                                }
                            case "employee":
                                {
                                    list.Add(AffiliationsEnum.EmployeeEnum);
                                    break;
                                }
                            case "guest":
                                {
                                    list.Add(AffiliationsEnum.GuestEnum);
                                    break;
                                }
                            default:
                                break;
                        }
                    }
                    return list;
                }
                return new List<AffiliationsEnum>();
            }
        }






        /// <summary>
        /// The primary e-mailaddress of this person
        /// </summary>
        /// <value>The primary e-mailaddress of this person</value>
        [JsonRequired]

        [JsonProperty(PropertyName = "mail")]
        public string Mail { get; set; }

        /// <summary>
        /// The secondary e-mailaddress of this person
        /// </summary>
        /// <value>The secondary e-mailaddress of this person</value>

        [JsonProperty(PropertyName = "secondaryMail")]
        public string SecondaryMail { get; set; }

        /// <summary>
        /// The telephone number of this person
        /// </summary>
        /// <value>The telephone number of this person</value>

        [MaxLength(256)]
        [JsonProperty(PropertyName = "telephoneNumber")]
        public string TelephoneNumber { get; set; }

        /// <summary>
        /// The mobile number of this person
        /// </summary>
        /// <value>The mobile number of this person</value>

        [MaxLength(256)]
        [JsonProperty(PropertyName = "mobileNumber")]
        public string MobileNumber { get; set; }

        /// <summary>
        /// The url of the informal picture of this person
        /// </summary>
        /// <value>The url of the informal picture of this person</value>

        [MaxLength(2048)]
        [JsonProperty(PropertyName = "photoSocial")]
        public string PhotoSocial { get; set; }

        /// <summary>
        /// The url of the official picture of this person
        /// </summary>
        /// <value>The url of the official picture of this person</value>

        [MaxLength(2048)]
        [JsonProperty(PropertyName = "photoOfficial")]
        public string PhotoOfficial { get; set; }



        /// <summary>
        /// The gender of this person
        /// </summary>
        /// <value>The gender of this person</value>

        [JsonProperty(PropertyName = "gender")]
        public GenderEnum? Gender { get; set; }

        /// <summary>
        /// A title prefix to be used for this person
        /// </summary>
        /// <value>A title prefix to be used for this person</value>

        [JsonProperty(PropertyName = "titlePrefix")]
        public string TitlePrefix { get; set; }

        /// <summary>
        /// A title suffix to be used for this person
        /// </summary>
        /// <value>A title suffix to be used for this person</value>

        [JsonProperty(PropertyName = "titleSuffix")]
        public string TitleSuffix { get; set; }

        /// <summary>
        /// The name of the office where this person is located
        /// </summary>
        /// <value>The name of the office where this person is located</value>

        [JsonProperty(PropertyName = "office")]
        public string Office { get; set; }

        /// <summary>
        /// Gets or Sets Address
        /// </summary>

        [JsonProperty(PropertyName = "address")]
        public Address Address { get; set; }

        /// <summary>
        /// Full name of In Case of Emergency contact
        /// </summary>
        /// <value>Full name of In Case of Emergency contact</value>

        [MaxLength(256)]
        [JsonProperty(PropertyName = "ICEName")]
        public string ICEName { get; set; }

        /// <summary>
        /// Phone number of In Case of Emergency contact
        /// </summary>
        /// <value>Phone number of In Case of Emergency contact</value>

        [MaxLength(256)]
        [JsonProperty(PropertyName = "ICEPhoneNumber")]
        public string ICEPhoneNumber { get; set; }



        /// <summary>
        /// Type of relation between person and In Case of Emergency contact
        /// </summary>
        /// <value>Type of relation between person and In Case of Emergency contact</value>

        [JsonProperty(PropertyName = "ICERelation")]
        public ICERelationEnum? ICERelation { get; set; }


        [JsonIgnore]
        public List<LanguageOfChoice> LanguageOfChoice { get; set; }

        /// <summary>
        /// The language(s) of choice for this person, RFC3066
        /// </summary>
        /// <value>The language(s) of choice for this person, RFC3066</value>

        [JsonProperty(PropertyName = "languageOfChoice")]
        [NotMapped]
        public List<string> languageOfChoice { get; set; }

        /// <summary>
        /// An array of additional human readable codes/identifiers for the entity being described.
        /// </summary>
        /// <value>An array of additional human readable codes/identifiers for the entity being described.</value>

        [JsonProperty(PropertyName = "otherCodes")]
        public List<OtherCodes> OtherCodes { get; set; }

        /// <summary>
        /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
        /// </summary>
        /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>

        [JsonProperty(PropertyName = "consumers")]
        public List<Consumer> Consumers { get; set; }

        [JsonIgnore]
        public virtual ICollection<Group> Groups { get; set; }

    }
}
