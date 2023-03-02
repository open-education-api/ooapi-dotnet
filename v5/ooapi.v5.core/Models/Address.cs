using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ooapi.v5.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// The full street address
    /// </summary>
    [DataContract]
    public partial class Address : ModelBase
    {

        /// <summary>
        /// Unique id of this address
        /// </summary>
        /// <value>Unique id of this address</value>
        [JsonRequired]

        [JsonProperty(PropertyName = "addressId")]
        public Guid AddressId { get; set; }


        /// <summary>
        /// Address type - postal: post - visit: bezoek - deliveries: bezorg - billing: factuur - teaching: the address where education takes place 
        /// </summary>
        /// <value>Address type - postal: post - visit: bezoek - deliveries: bezorg - billing: factuur - teaching: the address where education takes place </value>
        [JsonRequired]
        [JsonProperty(PropertyName = "addressType")]
        public AddressTypeEnum? AddressType { get; set; }

        /// <summary>
        /// The street name
        /// </summary>
        /// <value>The street name</value>

        [JsonProperty(PropertyName = "street")]
        public string Street { get; set; }

        /// <summary>
        /// The street number
        /// </summary>
        /// <value>The street number</value>

        [JsonProperty(PropertyName = "streetNumber")]
        public string StreetNumber { get; set; }

        /// <summary>
        /// Further details like building name, suite, apartment number, etc.
        /// </summary>
        /// <value>Further details like building name, suite, apartment number, etc.</value>
        [JsonProperty(PropertyName = "additional")]
        [NotMapped]
        public List<LanguageTypedString>? addition
        {
            get
            {
                return Helpers.LanguageTypedStringJsonConverter.GetLanguageTypesStringList(Additional);
            }
            set
            {
                if (value != null)
                    Additional = JsonConvert.SerializeObject(value);
            }
        }


        [JsonIgnore]
        public string? Additional { get; set; }

        /// <summary>
        /// Postal code
        /// </summary>
        /// <value>Postal code</value>

        [JsonProperty(PropertyName = "postalCode")]
        public string PostalCode { get; set; }

        /// <summary>
        /// name of the city / locality
        /// </summary>
        /// <value>name of the city / locality</value>

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        /// <summary>
        /// the country code according to [iso-3166-1-alpha-2](https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2)
        /// </summary>
        /// <value>the country code according to [iso-3166-1-alpha-2](https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2)</value>

        [JsonProperty(PropertyName = "countryCode")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Geolocation of the entrance of this address (WGS84 coordinate reference system)
        /// </summary>
        /// <value>Geolocation of the entrance of this address (WGS84 coordinate reference system)</value>

        [JsonProperty(PropertyName = "geolocation")]
        [NotMapped]
        public Geolocation? Geolocation
        {
            get
            {
                if (Latitude != null && Longitude != null)
                {
                    return new Geolocation() { Latitude = (decimal)Latitude, Longitude = (decimal)Longitude };
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    Latitude = value.Latitude;
                    Longitude = value.Longitude;
                }
            }
        }


        [JsonIgnore]
        [Column(TypeName = "decimal(8, 6)")]
        [Precision(8, 6)]
        public decimal? Latitude { get; set; }


        [JsonIgnore]
        [Column(TypeName = "decimal(8, 6)")]
        [Precision(8, 6)]
        public decimal? Longitude { get; set; }


        [JsonIgnore]
        public virtual ICollection<Organization> Organizations { get; set; }

        [JsonIgnore]
        public virtual ICollection<Program> Programs { get; set; }

        [JsonIgnore]
        public virtual ICollection<Course> Courses { get; set; }

        [JsonIgnore]
        public virtual ICollection<Component> Components { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProgramOffering> ProgramOfferings { get; set; }

        [JsonIgnore]
        public virtual ICollection<CourseOffering> CourseOfferings { get; set; }

        [JsonIgnore]
        public virtual ICollection<ComponentOffering> ComponentOfferings { get; set; }




    }
}
