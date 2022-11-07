namespace ooapi.v5.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;

/// <summary>
/// The type of this group - learning group: A collection of participants carrying out common learning activities - class: A collection of participants carrying out jointly scheduled educational activities - team: A collection of members of a team, either students, employees or mixed. 
/// </summary>
/// <value>The type of this group - learning group: A collection of participants carrying out common learning activities - class: A collection of participants carrying out jointly scheduled educational activities - team: A collection of members of a team, either students, employees or mixed. </value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum GroupTypeEnum
{
    /// <summary>
    /// Enum LearningGroupEnum for learning group
    /// </summary>
    [EnumMember(Value = "learning group")]
    LearningGroupEnum = 0,
    /// <summary>
    /// Enum ClassEnum for class
    /// </summary>
    [EnumMember(Value = "class")]
    ClassEnum = 1,
    /// <summary>
    /// Enum TeamEnum for team
    /// </summary>
    [EnumMember(Value = "team")]
    TeamEnum = 2
}