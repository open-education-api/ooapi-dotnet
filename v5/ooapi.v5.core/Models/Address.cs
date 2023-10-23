using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ooapi.v5.Attributes;
using ooapi.v5.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;

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
    public AddressType? AddressType { get; set; }

    /// <summary>
    /// The street name
    /// </summary>
    /// <value>The street name</value>
    [JsonProperty(PropertyName = "street")]
    public string Street { get; set; } = default!;

    /// <summary>
    /// The street number
    /// </summary>
    /// <value>The street number</value>
    [JsonProperty(PropertyName = "streetNumber")]
    public string StreetNumber { get; set; } = default!;

    /// <summary>
    /// Further details like building name, suite, apartment number, etc.
    /// </summary>
    /// <value>Further details like building name, suite, apartment number, etc.</value>
    [JsonRequired]
    [JsonProperty("addition")]
    [NotMapped]
    [SortAllowed]
    public List<LanguageTypedString> addition
    {
        get
        {
            var result = new List<LanguageTypedString>();
            if (Attributes != null && Attributes.Any())
            {
                result = Attributes.Where(x => x.PropertyName.Equals("additional")).Select(x => new LanguageTypedString() { Language = x.Language, Value = x.Value }).ToList();
            }
            return result;
        }
    }

    [JsonIgnore]
    [SortDefault]
    public List<Attribute> Attributes { get; set; } = default!;

    /// <summary>
    /// Postal code
    /// </summary>
    /// <value>Postal code</value>
    [JsonProperty(PropertyName = "postalCode")]
    public string PostalCode { get; set; } = default!;

    /// <summary>
    /// name of the city / locality
    /// </summary>
    /// <value>name of the city / locality</value>
    [JsonProperty(PropertyName = "city")]
    public string City { get; set; } = default!;

    /// <summary>
    /// the country code according to [iso-3166-1-alpha-2](https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2)
    /// </summary>
    /// <value>the country code according to [iso-3166-1-alpha-2](https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2)</value>
    [JsonProperty(PropertyName = "countryCode")]
    public string CountryCode { get; set; } = default!;

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

    /// <summary>
    /// The latitude
    /// </summary>
    [JsonIgnore]
    [Column(TypeName = "decimal(8, 6)")]
    [Precision(8, 6)]
    public decimal? Latitude { get; set; }

    /// <summary>
    /// The longitude
    /// </summary>
    [JsonIgnore]
    [Column(TypeName = "decimal(8, 6)")]
    [Precision(8, 6)]
    public decimal? Longitude { get; set; }

    /// <summary>
    /// Collection of organizations
    /// </summary>
    [JsonIgnore]
    public virtual ICollection<Organization> Organizations { get; set; } = default!;

    /// <summary>
    /// Collection of programs
    /// </summary>
    [JsonIgnore]
    public virtual ICollection<Program> Programs { get; set; } = default!;

    /// <summary>
    /// Collection of coures
    /// </summary>
    [JsonIgnore]
    public virtual ICollection<Course> Courses { get; set; } = default!;

    /// <summary>
    /// Collection of components
    /// </summary>
    [JsonIgnore]
    public virtual ICollection<Component> Components { get; set; } = default!;

    /// <summary>
    /// Collection of program offerings
    /// </summary>
    [JsonIgnore]
    public virtual ICollection<ProgramOffering> ProgramOfferings { get; set; } = default!;

    /// <summary>
    /// Collection of course offerings
    /// </summary>
    [JsonIgnore]
    public virtual ICollection<CourseOffering> CourseOfferings { get; set; } = default!;

    /// <summary>
    /// Collection of component offerings
    /// </summary>
    [JsonIgnore]
    public virtual ICollection<ComponentOffering> ComponentOfferings { get; set; } = default!;
}
