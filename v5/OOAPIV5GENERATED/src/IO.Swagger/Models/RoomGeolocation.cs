using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace IO.Swagger.Models
{
    /// <summary>
    /// Geolocation of the entrance of this room (WGS84 coordinate reference system)
    /// </summary>
    [DataContract]
    public partial class RoomGeolocation : IEquatable<RoomGeolocation>
    {
        /// <summary>
        /// Gets or Sets Latitude
        /// </summary>
        [JsonRequired]

        [DataMember(Name = "latitude")]
        public double? Latitude { get; set; }

        /// <summary>
        /// Gets or Sets Longitude
        /// </summary>
        [JsonRequired]

        [DataMember(Name = "longitude")]
        public double? Longitude { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RoomGeolocation {\n");
            sb.Append("  Latitude: ").Append(Latitude).Append("\n");
            sb.Append("  Longitude: ").Append(Longitude).Append("\n");
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
            return obj.GetType() == GetType() && Equals((RoomGeolocation)obj);
        }

        /// <summary>
        /// Returns true if RoomGeolocation instances are equal
        /// </summary>
        /// <param name="other">Instance of RoomGeolocation to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(RoomGeolocation other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Latitude == other.Latitude ||
                    Latitude != null &&
                    Latitude.Equals(other.Latitude)
                ) &&
                (
                    Longitude == other.Longitude ||
                    Longitude != null &&
                    Longitude.Equals(other.Longitude)
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
                if (Latitude != null)
                    hashCode = hashCode * 59 + Latitude.GetHashCode();
                if (Longitude != null)
                    hashCode = hashCode * 59 + Longitude.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(RoomGeolocation left, RoomGeolocation right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(RoomGeolocation left, RoomGeolocation right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
