using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;

/// <summary>
/// Geolocation of the entrance of this address (WGS84 coordinate reference system)
/// </summary>
[DataContract]
[ExcludeFromCodeCoverage(Justification = "Get/Set")]
public class Geolocation
{
    /// <summary>
    /// Gets or Sets Latitude
    /// </summary>
    [JsonRequired]
    [JsonProperty(PropertyName = "latitude")]
    [Column(TypeName = "decimal(8, 6)")]
    [Precision(8, 6)]
    public decimal Latitude { get; set; }

    /// <summary>
    /// Gets or Sets Longitude
    /// </summary>
    [JsonRequired]
    [JsonProperty(PropertyName = "longitude")]
    [Column(TypeName = "decimal(8, 6)")]
    [Precision(8, 6)]
    public decimal Longitude { get; set; }
}