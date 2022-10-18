namespace IO.Swagger.Enums;

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
    ProgramEnum = 0,
    /// <summary>
    /// Enum CourseEnum for course
    /// </summary>
    [EnumMember(Value = "course")]
    CourseEnum = 1,
    /// <summary>
    /// Enum ComponentEnum for component
    /// </summary>
    [EnumMember(Value = "component")]
    ComponentEnum = 2
}