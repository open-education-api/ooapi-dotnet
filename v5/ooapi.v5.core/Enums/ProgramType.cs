using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Enums;

/// <summary>
/// The type of this program - program: opleiding - minor: minor - honours: honours - specialization: specialisatie - track: track 
/// </summary>
/// <value>The type of this program - program: opleiding - minor: minor - honours: honours - specialization: specialisatie - track: track </value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum ProgramType
{
    /// <summary>
    /// Enum ProgramEnum for program
    /// </summary>
    [EnumMember(Value = "program")]
    program = 0,
    /// <summary>
    /// Enum MinorEnum for minor
    /// </summary>
    [EnumMember(Value = "minor")]
    minor = 1,
    /// <summary>
    /// Enum HonoursEnum for honours
    /// </summary>
    [EnumMember(Value = "honours")]
    honours = 2,
    /// <summary>
    /// Enum SpecializationEnum for specialization
    /// </summary>
    [EnumMember(Value = "specialization")]
    specialization = 3,
    /// <summary>
    /// Enum TrackEnum for track
    /// </summary>
    [EnumMember(Value = "track")]
    track = 4
}