using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Enums;

/// <summary>
/// The type of this news item - calamity: calamiteit - general: algemeen - schedule-change: roosterwijziging - announcement: aankondiging 
/// </summary>
/// <value>The type of this news item - calamity: calamiteit - general: algemeen - schedule-change: roosterwijziging - announcement: aankondiging </value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum NewsItemType
{
    /// <summary>
    /// Enum CalamityEnum for calamity
    /// </summary>
    [EnumMember(Value = "calamity")]
    calamity = 0,
    /// <summary>
    /// Enum GeneralEnum for general
    /// </summary>
    [EnumMember(Value = "general")]
    general = 1,
    /// <summary>
    /// Enum ScheduleChangeEnum for schedule-change
    /// </summary>
    [EnumMember(Value = "schedule-change")]
    schedule_change = 2,
    /// <summary>
    /// Enum AnnouncementEnum for announcement
    /// </summary>
    [EnumMember(Value = "announcement")]
    announcement = 3
}