using IO.Swagger.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace IO.Swagger.Models
{
    /// <summary>
    /// The full street address
    /// </summary>
    [DataContract]
    public partial class Address
    {

        /// <summary>
        /// Address type - postal: post - visit: bezoek - deliveries: bezorg - billing: factuur - teaching: the address where education takes place 
        /// </summary>
        /// <value>Address type - postal: post - visit: bezoek - deliveries: bezorg - billing: factuur - teaching: the address where education takes place </value>
        [JsonRequired]

        [DataMember(Name = "addressType")]
        public AddressTypeEnum? AddressType { get; set; }

        /// <summary>
        /// The street name
        /// </summary>
        /// <value>The street name</value>

        [DataMember(Name = "street")]
        public string Street { get; set; }

        /// <summary>
        /// The street number
        /// </summary>
        /// <value>The street number</value>

        [DataMember(Name = "streetNumber")]
        public string StreetNumber { get; set; }

        /// <summary>
        /// Further details like building name, suite, apartment number, etc.
        /// </summary>
        /// <value>Further details like building name, suite, apartment number, etc.</value>

        [DataMember(Name = "additional")]
        public List<LanguageValueItem> Additional { get; set; }

        /// <summary>
        /// Postal code
        /// </summary>
        /// <value>Postal code</value>

        [DataMember(Name = "postalCode")]
        public string PostalCode { get; set; }

        /// <summary>
        /// name of the city / locality
        /// </summary>
        /// <value>name of the city / locality</value>

        [DataMember(Name = "city")]
        public string City { get; set; }

        /// <summary>
        /// the country code according to [iso-3166-1-alpha-2](https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2)
        /// </summary>
        /// <value>the country code according to [iso-3166-1-alpha-2](https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2)</value>

        [DataMember(Name = "countryCode")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or Sets Geolocation
        /// </summary>

        [DataMember(Name = "geolocation")]
        public Geolocation Geolocation { get; set; }

        /// <summary>
        /// Object for additional non-standard attributes
        /// </summary>
        /// <value>Object for additional non-standard attributes</value>

        [DataMember(Name = "ext")]
        public Object Ext { get; set; }


    }
}
