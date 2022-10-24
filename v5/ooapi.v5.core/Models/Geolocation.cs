using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// Geolocation of the entrance of this address (WGS84 coordinate reference system)
    /// </summary>
    [DataContract]
    public partial class Geolocation
    {
        /// <summary>
        /// Gets or Sets Latitude
        /// </summary>
        [Required]

        [DataMember(Name = "latitude")]
        public double? Latitude { get; set; }

        /// <summary>
        /// Gets or Sets Longitude
        /// </summary>
        [Required]

        [DataMember(Name = "longitude")]
        public double? Longitude { get; set; }


    }
}
