namespace IO.Swagger.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;

/// <summary>
/// The mode of delivery of the component (ECTS-mode of delivery) - distance-learning: afstandsleren - on campus: op de campus - online: online - hybrid: hybride - situated: op locatie 
/// </summary>
/// <value>The mode of delivery of the component (ECTS-mode of delivery) - distance-learning: afstandsleren - on campus: op de campus - online: online - hybrid: hybride - situated: op locatie </value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum ModeOfDeliveryEnum
{
    /// <summary>
    /// Enum DistanceLearningEnum for distance-learning
    /// </summary>
    [EnumMember(Value = "distance-learning")]
    DistanceLearningEnum = 0,
    /// <summary>
    /// Enum OnCampusEnum for on campus
    /// </summary>
    [EnumMember(Value = "on campus")]
    OnCampusEnum = 1,
    /// <summary>
    /// Enum OnlineEnum for online
    /// </summary>
    [EnumMember(Value = "online")]
    OnlineEnum = 2,
    /// <summary>
    /// Enum HybridEnum for hybrid
    /// </summary>
    [EnumMember(Value = "hybrid")]
    HybridEnum = 3,
    /// <summary>
    /// Enum SituatedEnum for situated
    /// </summary>
    [EnumMember(Value = "situated")]
    SituatedEnum = 4
}