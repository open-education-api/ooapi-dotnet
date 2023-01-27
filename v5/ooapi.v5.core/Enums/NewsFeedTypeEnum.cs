namespace ooapi.v5.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;

/// <summary>
/// The type of the object this news feed relates to
/// </summary>
/// <value>The type of the object this news feed relates to</value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum NewsFeedTypeEnum
{
    /// <summary>
    /// Enum OrganizationEnum for organization
    /// </summary>
    [EnumMember(Value = "organization")]
    organization = 0,
    /// <summary>
    /// Enum ProgramEnum for program
    /// </summary>
    [EnumMember(Value = "program")]
    program = 1,
    /// <summary>
    /// Enum CourseEnum for course
    /// </summary>
    [EnumMember(Value = "course")]
    course = 2,
    /// <summary>
    /// Enum ComponentEnum for component
    /// </summary>
    [EnumMember(Value = "component")]
    component = 3,
    /// <summary>
    /// Enum PersonEnum for person
    /// </summary>
    [EnumMember(Value = "person")]
    person = 4,
    /// <summary>
    /// Enum BuildingEnum for building
    /// </summary>
    [EnumMember(Value = "building")]
    building = 5,
    /// <summary>
    /// Enum RoomEnum for room
    /// </summary>
    [EnumMember(Value = "room")]
    room = 6
}