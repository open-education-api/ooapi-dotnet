﻿namespace IO.Swagger.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;

/// <summary>
/// The type of this program - program: opleiding - minor: minor - honours: honours - specialization: specialisatie - track: track 
/// </summary>
/// <value>The type of this program - program: opleiding - minor: minor - honours: honours - specialization: specialisatie - track: track </value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum ProgramTypeEnum
{
    /// <summary>
    /// Enum ProgramEnum for program
    /// </summary>
    [EnumMember(Value = "program")]
    ProgramEnum = 0,
    /// <summary>
    /// Enum MinorEnum for minor
    /// </summary>
    [EnumMember(Value = "minor")]
    MinorEnum = 1,
    /// <summary>
    /// Enum HonoursEnum for honours
    /// </summary>
    [EnumMember(Value = "honours")]
    HonoursEnum = 2,
    /// <summary>
    /// Enum SpecializationEnum for specialization
    /// </summary>
    [EnumMember(Value = "specialization")]
    SpecializationEnum = 3,
    /// <summary>
    /// Enum TrackEnum for track
    /// </summary>
    [EnumMember(Value = "track")]
    TrackEnum = 4
}