using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// Geolocation of the entrance of this room (WGS84 coordinate reference system)
    /// </summary>
    [DataContract]
    public class RoomGeolocation
    {


        /// <summary>
        /// Unique id of this roomGeolocation
        /// </summary>
        /// <value>Unique id of this roomGeolocation</value>
        [JsonIgnore]
        [JsonProperty("roomGeolocationId")]
        public Guid RoomGeolocationId { get; set; }

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


    }
}
