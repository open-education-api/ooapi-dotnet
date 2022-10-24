namespace ooapi.v5.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;

/// <summary>
/// The unit in which the studyload is specfied - contacttime: CONTACTUUR amount of time spent in classes - ects: ECTS_PUNT European Credit Transfer System - sbu: SBU studentloadhours - sp: STUDIEPUNT studentpoints - hour: UUR hours 
/// </summary>
/// <value>The unit in which the studyload is specfied - contacttime: CONTACTUUR amount of time spent in classes - ects: ECTS_PUNT European Credit Transfer System - sbu: SBU studentloadhours - sp: STUDIEPUNT studentpoints - hour: UUR hours </value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum StudyLoadUnitEnum
{
    /// <summary>
    /// Enum ContacttimeEnum for contacttime
    /// </summary>
    [EnumMember(Value = "contacttime")]
    ContacttimeEnum = 0,
    /// <summary>
    /// Enum EctsEnum for ects
    /// </summary>
    [EnumMember(Value = "ects")]
    EctsEnum = 1,
    /// <summary>
    /// Enum SbuEnum for sbu
    /// </summary>
    [EnumMember(Value = "sbu")]
    SbuEnum = 2,
    /// <summary>
    /// Enum SpEnum for sp
    /// </summary>
    [EnumMember(Value = "sp")]
    SpEnum = 3,
    /// <summary>
    /// Enum HourEnum for hour
    /// </summary>
    [EnumMember(Value = "hour")]
    HourEnum = 4
}