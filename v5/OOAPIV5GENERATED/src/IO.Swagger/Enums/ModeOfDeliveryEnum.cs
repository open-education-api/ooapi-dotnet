using System.Runtime.Serialization;

namespace IO.Swagger.Enums
{

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
}