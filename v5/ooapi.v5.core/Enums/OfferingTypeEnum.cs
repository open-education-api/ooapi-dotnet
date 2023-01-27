﻿namespace ooapi.v5.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;

/// <summary>
/// The type of this offering
/// </summary>
/// <value>The type of this offering</value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum OfferingTypeEnum
{
    /// <summary>
    /// Enum ProgramEnum for program
    /// </summary>
    [EnumMember(Value = "program")]
    program = 0,
    /// <summary>
    /// Enum CourseEnum for course
    /// </summary>
    [EnumMember(Value = "course")]
    course = 1,
    /// <summary>
    /// Enum ComponentEnum for component
    /// </summary>
    [EnumMember(Value = "component")]
    component = 2
}