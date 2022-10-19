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
    /// An object describing a building and the properties of a building.
    /// </summary>
    [DataContract]
    public partial class Building : IEquatable<Building>
    {
        /// <summary>
        /// Unique id of this building
        /// </summary>
        /// <value>Unique id of this building</value>
        [Required]

        [DataMember(Name = "buildingId")]
        public Guid? BuildingId { get; set; }

        /// <summary>
        /// Gets or Sets PrimaryCode
        /// </summary>
        [Required]

        [DataMember(Name = "primaryCode")]
        public PrimaryCode PrimaryCode { get; set; }

        /// <summary>
        /// The abbreviation of the name of this building
        /// </summary>
        /// <value>The abbreviation of the name of this building</value>

        [MaxLength(256)]
        [DataMember(Name = "abbreviation")]
        public string Abbreviation { get; set; }

        /// <summary>
        /// The name of this building
        /// </summary>
        /// <value>The name of this building</value>
        [Required]

        [DataMember(Name = "name")]
        public List<LanguageValueItem> Name { get; set; }

        /// <summary>
        /// The description of this building.
        /// </summary>
        /// <value>The description of this building.</value>

        [DataMember(Name = "description")]
        public List<LanguageValueItem> Description { get; set; }

        /// <summary>
        /// Gets or Sets Address
        /// </summary>
        [Required]

        [DataMember(Name = "address")]
        public Address Address { get; set; }

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
            sb.Append("class Building {\n");
            sb.Append("  BuildingId: ").Append(BuildingId).Append("\n");
            sb.Append("  PrimaryCode: ").Append(PrimaryCode).Append("\n");
            sb.Append("  Abbreviation: ").Append(Abbreviation).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  Address: ").Append(Address).Append("\n");
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
            return obj.GetType() == GetType() && Equals((Building)obj);
        }

        /// <summary>
        /// Returns true if Building instances are equal
        /// </summary>
        /// <param name="other">Instance of Building to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Building other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    BuildingId == other.BuildingId ||
                    BuildingId != null &&
                    BuildingId.Equals(other.BuildingId)
                ) &&
                (
                    PrimaryCode == other.PrimaryCode ||
                    PrimaryCode != null &&
                    PrimaryCode.Equals(other.PrimaryCode)
                ) &&
                (
                    Abbreviation == other.Abbreviation ||
                    Abbreviation != null &&
                    Abbreviation.Equals(other.Abbreviation)
                ) &&
                (
                    Name == other.Name ||
                    Name != null &&
                    Name.SequenceEqual(other.Name)
                ) &&
                (
                    Description == other.Description ||
                    Description != null &&
                    Description.SequenceEqual(other.Description)
                ) &&
                (
                    Address == other.Address ||
                    Address != null &&
                    Address.Equals(other.Address)
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
                if (BuildingId != null)
                    hashCode = hashCode * 59 + BuildingId.GetHashCode();
                if (PrimaryCode != null)
                    hashCode = hashCode * 59 + PrimaryCode.GetHashCode();
                if (Abbreviation != null)
                    hashCode = hashCode * 59 + Abbreviation.GetHashCode();
                if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                if (Description != null)
                    hashCode = hashCode * 59 + Description.GetHashCode();
                if (Address != null)
                    hashCode = hashCode * 59 + Address.GetHashCode();
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

        public static bool operator ==(Building left, Building right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Building left, Building right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
