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
    /// An area within a building where education can take place
    /// </summary>
    [DataContract]
    public partial class Room : IEquatable<Room>
    {
        /// <summary>
        /// Unique id for this room
        /// </summary>
        /// <value>Unique id for this room</value>
        [JsonRequired]

        [DataMember(Name = "roomId")]
        public Guid RoomId { get; set; }

        /// <summary>
        /// Gets or Sets PrimaryCode
        /// </summary>
        [JsonRequired]

        [DataMember(Name = "primaryCode")]
        public PrimaryCode PrimaryCode { get; set; }


        /// <summary>
        /// The type of this room - general purpose: algemeen - lecture room: collegezaal - computer room: computerruimte - laboratory: laboratorium - office: kantoor - workspace: werkruimte - exam location: tentamenruimte - study room: studieruimte - examination room: onderzoekskamer - conference room: vergaderkamer 
        /// </summary>
        /// <value>The type of this room - general purpose: algemeen - lecture room: collegezaal - computer room: computerruimte - laboratory: laboratorium - office: kantoor - workspace: werkruimte - exam location: tentamenruimte - study room: studieruimte - examination room: onderzoekskamer - conference room: vergaderkamer </value>
        [JsonRequired]

        [DataMember(Name = "roomType")]
        public RoomTypeEnum? RoomType { get; set; }

        /// <summary>
        /// The abbreviation of the name of this room
        /// </summary>
        /// <value>The abbreviation of the name of this room</value>

        [MaxLength(256)]
        [DataMember(Name = "abbreviation")]
        public string Abbreviation { get; set; }

        /// <summary>
        /// The name of this room
        /// </summary>
        /// <value>The name of this room</value>
        [JsonRequired]

        [DataMember(Name = "name")]
        public List<LanguageValueItem> Name { get; set; }

        /// <summary>
        /// The description of this room. [The limited implementation of Git Hub Markdown syntax](#tag/formatting-and-displaying-results-from-API) MAY be used for rich text representation.
        /// </summary>
        /// <value>The description of this room. [The limited implementation of Git Hub Markdown syntax](#tag/formatting-and-displaying-results-from-API) MAY be used for rich text representation.</value>

        [DataMember(Name = "description")]
        public List<LanguageValueItem> Description { get; set; }

        /// <summary>
        /// The total number of seats located in the room
        /// </summary>
        /// <value>The total number of seats located in the room</value>

        [DataMember(Name = "totalSeats")]
        public int? TotalSeats { get; set; }

        /// <summary>
        /// The total number of available (&#x3D;non-reserved) seats in the room
        /// </summary>
        /// <value>The total number of available (&#x3D;non-reserved) seats in the room</value>

        [DataMember(Name = "availableSeats")]
        public int? AvailableSeats { get; set; }

        /// <summary>
        /// The floor on which this room is located
        /// </summary>
        /// <value>The floor on which this room is located</value>

        [DataMember(Name = "floor")]
        public string Floor { get; set; }

        /// <summary>
        /// The wing in which this room is located
        /// </summary>
        /// <value>The wing in which this room is located</value>

        [DataMember(Name = "wing")]
        public string Wing { get; set; }

        /// <summary>
        /// Gets or Sets Geolocation
        /// </summary>

        [DataMember(Name = "geolocation")]
        public RoomGeolocation Geolocation { get; set; }

        /// <summary>
        /// An array of additional human readable codes/identifiers for the entity being described.
        /// </summary>
        /// <value>An array of additional human readable codes/identifiers for the entity being described.</value>

        [DataMember(Name = "otherCodes")]
        public List<OtherCodes> OtherCodes { get; set; }

        /// <summary>
        /// The building in which the room is located. [&#x60;expandable&#x60;](#tag/building_model) By default only the &#x60;buildingId&#x60; (a string) is returned. If the client requested an expansion of &#x60;building&#x60; the full building object should be returned. 
        /// </summary>
        /// <value>The building in which the room is located. [&#x60;expandable&#x60;](#tag/building_model) By default only the &#x60;buildingId&#x60; (a string) is returned. If the client requested an expansion of &#x60;building&#x60; the full building object should be returned. </value>

        [DataMember(Name = "building")]
        public Guid Building { get; set; }

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
            sb.Append("class Room {\n");
            sb.Append("  RoomId: ").Append(RoomId).Append("\n");
            sb.Append("  PrimaryCode: ").Append(PrimaryCode).Append("\n");
            sb.Append("  RoomType: ").Append(RoomType).Append("\n");
            sb.Append("  Abbreviation: ").Append(Abbreviation).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  TotalSeats: ").Append(TotalSeats).Append("\n");
            sb.Append("  AvailableSeats: ").Append(AvailableSeats).Append("\n");
            sb.Append("  Floor: ").Append(Floor).Append("\n");
            sb.Append("  Wing: ").Append(Wing).Append("\n");
            sb.Append("  Geolocation: ").Append(Geolocation).Append("\n");
            sb.Append("  OtherCodes: ").Append(OtherCodes).Append("\n");
            sb.Append("  Building: ").Append(Building).Append("\n");
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
            return obj.GetType() == GetType() && Equals((Room)obj);
        }

        /// <summary>
        /// Returns true if Room instances are equal
        /// </summary>
        /// <param name="other">Instance of Room to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Room other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    RoomId == other.RoomId ||
                    RoomId != null &&
                    RoomId.Equals(other.RoomId)
                ) &&
                (
                    PrimaryCode == other.PrimaryCode ||
                    PrimaryCode != null &&
                    PrimaryCode.Equals(other.PrimaryCode)
                ) &&
                (
                    RoomType == other.RoomType ||
                    RoomType != null &&
                    RoomType.Equals(other.RoomType)
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
                    TotalSeats == other.TotalSeats ||
                    TotalSeats != null &&
                    TotalSeats.Equals(other.TotalSeats)
                ) &&
                (
                    AvailableSeats == other.AvailableSeats ||
                    AvailableSeats != null &&
                    AvailableSeats.Equals(other.AvailableSeats)
                ) &&
                (
                    Floor == other.Floor ||
                    Floor != null &&
                    Floor.Equals(other.Floor)
                ) &&
                (
                    Wing == other.Wing ||
                    Wing != null &&
                    Wing.Equals(other.Wing)
                ) &&
                (
                    Geolocation == other.Geolocation ||
                    Geolocation != null &&
                    Geolocation.Equals(other.Geolocation)
                ) &&
                (
                    OtherCodes == other.OtherCodes ||
                    OtherCodes != null &&
                    OtherCodes.SequenceEqual(other.OtherCodes)
                ) &&
                (
                    Building == other.Building ||
                    Building != null &&
                    Building.Equals(other.Building)
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
                if (RoomId != null)
                    hashCode = hashCode * 59 + RoomId.GetHashCode();
                if (PrimaryCode != null)
                    hashCode = hashCode * 59 + PrimaryCode.GetHashCode();
                if (RoomType != null)
                    hashCode = hashCode * 59 + RoomType.GetHashCode();
                if (Abbreviation != null)
                    hashCode = hashCode * 59 + Abbreviation.GetHashCode();
                if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                if (Description != null)
                    hashCode = hashCode * 59 + Description.GetHashCode();
                if (TotalSeats != null)
                    hashCode = hashCode * 59 + TotalSeats.GetHashCode();
                if (AvailableSeats != null)
                    hashCode = hashCode * 59 + AvailableSeats.GetHashCode();
                if (Floor != null)
                    hashCode = hashCode * 59 + Floor.GetHashCode();
                if (Wing != null)
                    hashCode = hashCode * 59 + Wing.GetHashCode();
                if (Geolocation != null)
                    hashCode = hashCode * 59 + Geolocation.GetHashCode();
                if (OtherCodes != null)
                    hashCode = hashCode * 59 + OtherCodes.GetHashCode();
                if (Building != null)
                    hashCode = hashCode * 59 + Building.GetHashCode();
                if (Consumers != null)
                    hashCode = hashCode * 59 + Consumers.GetHashCode();
                if (Ext != null)
                    hashCode = hashCode * 59 + Ext.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(Room left, Room right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Room left, Room right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
