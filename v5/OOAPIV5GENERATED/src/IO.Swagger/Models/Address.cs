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
    /// The full street address
    /// </summary>
    [DataContract]
    public partial class Address : IEquatable<Address>
    {

        /// <summary>
        /// Address type - postal: post - visit: bezoek - deliveries: bezorg - billing: factuur - teaching: the address where education takes place 
        /// </summary>
        /// <value>Address type - postal: post - visit: bezoek - deliveries: bezorg - billing: factuur - teaching: the address where education takes place </value>
        [Required]

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
        public AddressGeolocation Geolocation { get; set; }

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
            sb.Append("class Address {\n");
            sb.Append("  AddressType: ").Append(AddressType).Append("\n");
            sb.Append("  Street: ").Append(Street).Append("\n");
            sb.Append("  StreetNumber: ").Append(StreetNumber).Append("\n");
            sb.Append("  Additional: ").Append(Additional).Append("\n");
            sb.Append("  PostalCode: ").Append(PostalCode).Append("\n");
            sb.Append("  City: ").Append(City).Append("\n");
            sb.Append("  CountryCode: ").Append(CountryCode).Append("\n");
            sb.Append("  Geolocation: ").Append(Geolocation).Append("\n");
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
            return obj.GetType() == GetType() && Equals((Address)obj);
        }

        /// <summary>
        /// Returns true if Address instances are equal
        /// </summary>
        /// <param name="other">Instance of Address to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Address other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    AddressType == other.AddressType ||
                    AddressType != null &&
                    AddressType.Equals(other.AddressType)
                ) &&
                (
                    Street == other.Street ||
                    Street != null &&
                    Street.Equals(other.Street)
                ) &&
                (
                    StreetNumber == other.StreetNumber ||
                    StreetNumber != null &&
                    StreetNumber.Equals(other.StreetNumber)
                ) &&
                (
                    Additional == other.Additional ||
                    Additional != null &&
                    Additional.SequenceEqual(other.Additional)
                ) &&
                (
                    PostalCode == other.PostalCode ||
                    PostalCode != null &&
                    PostalCode.Equals(other.PostalCode)
                ) &&
                (
                    City == other.City ||
                    City != null &&
                    City.Equals(other.City)
                ) &&
                (
                    CountryCode == other.CountryCode ||
                    CountryCode != null &&
                    CountryCode.Equals(other.CountryCode)
                ) &&
                (
                    Geolocation == other.Geolocation ||
                    Geolocation != null &&
                    Geolocation.Equals(other.Geolocation)
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
                if (AddressType != null)
                    hashCode = hashCode * 59 + AddressType.GetHashCode();
                if (Street != null)
                    hashCode = hashCode * 59 + Street.GetHashCode();
                if (StreetNumber != null)
                    hashCode = hashCode * 59 + StreetNumber.GetHashCode();
                if (Additional != null)
                    hashCode = hashCode * 59 + Additional.GetHashCode();
                if (PostalCode != null)
                    hashCode = hashCode * 59 + PostalCode.GetHashCode();
                if (City != null)
                    hashCode = hashCode * 59 + City.GetHashCode();
                if (CountryCode != null)
                    hashCode = hashCode * 59 + CountryCode.GetHashCode();
                if (Geolocation != null)
                    hashCode = hashCode * 59 + Geolocation.GetHashCode();
                if (Ext != null)
                    hashCode = hashCode * 59 + Ext.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(Address left, Address right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Address left, Address right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
