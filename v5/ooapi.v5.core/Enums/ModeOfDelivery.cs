namespace ooapi.v5.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;

/// <summary>
/// The mode of delivery of the component (ECTS-mode of delivery) - distance-learning: afstandsleren - on campus: op de campus - online: online - hybrid: hybride - situated: op locatie 
/// </summary>
/// <value>The mode of delivery of the component (ECTS-mode of delivery) - distance-learning: afstandsleren - on campus: op de campus - online: online - hybrid: hybride - situated: op locatie </value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum ModeOfDelivery
{
    /// <summary>
    /// Enum DistanceLearningEnum for distance-learning
    /// </summary>
    [EnumMember(Value = "distance learning")]
    distance_learning = 0,
    /// <summary>
    /// Enum OnCampusEnum for on campus
    /// </summary>
    [EnumMember(Value = "on campus")]
    on_campus = 1,
    /// <summary>
    /// Enum OnlineEnum for online
    /// </summary>
    [EnumMember(Value = "online")]
    online = 2,
    /// <summary>
    /// Enum HybridEnum for hybrid
    /// </summary>
    [EnumMember(Value = "hybrid")]
    hybrid = 3,
    /// <summary>
    /// Enum SituatedEnum for situated
    /// </summary>
    [EnumMember(Value = "situated")]
    situated = 4
}