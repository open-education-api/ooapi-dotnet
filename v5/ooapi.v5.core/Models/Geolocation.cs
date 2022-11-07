using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// Geolocation of the entrance of this address (WGS84 coordinate reference system)
    /// </summary>
    [DataContract]
    public class Geolocation
    {

        /// <summary>
        /// Unique id of this geolocation
        /// </summary>
        /// <value>Unique id of this geolocation</value>
        [JsonIgnore]
        [JsonProperty("geolocationId")]
        public Guid GeolocationId { get; set; }


        /// <summary>
        /// Gets or Sets Latitude
        /// </summary>
        [JsonRequired]

        [JsonProperty(PropertyName = "latitude")]
        public double? Latitude { get; set; }

        /// <summary>
        /// Gets or Sets Longitude
        /// </summary>
        [JsonRequired]

        [JsonProperty(PropertyName = "longitude")]
        public double? Longitude { get; set; }


    }
}
