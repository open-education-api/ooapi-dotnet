namespace ooapi.v5.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;

/// <summary>
/// The type of this news item - calamity: calamiteit - general: algemeen - schedule-change: roosterwijziging - announcement: aankondiging 
/// </summary>
/// <value>The type of this news item - calamity: calamiteit - general: algemeen - schedule-change: roosterwijziging - announcement: aankondiging </value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum NewsItemTypeEnum
{
    /// <summary>
    /// Enum CalamityEnum for calamity
    /// </summary>
    [EnumMember(Value = "calamity")]
    CalamityEnum = 0,
    /// <summary>
    /// Enum GeneralEnum for general
    /// </summary>
    [EnumMember(Value = "general")]
    GeneralEnum = 1,
    /// <summary>
    /// Enum ScheduleChangeEnum for schedule-change
    /// </summary>
    [EnumMember(Value = "schedule-change")]
    ScheduleChangeEnum = 2,
    /// <summary>
    /// Enum AnnouncementEnum for announcement
    /// </summary>
    [EnumMember(Value = "announcement")]
    AnnouncementEnum = 3
}