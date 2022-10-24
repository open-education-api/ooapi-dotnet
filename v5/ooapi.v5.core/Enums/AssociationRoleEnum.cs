namespace ooapi.v5.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;


/// <summary>
/// The role of this person associated with the offering   - student: student   - lecturer: docent   - teaching assistant: studentassistent   - coordinator: coÃ¶rdinator   - guest: gast 
/// </summary>
/// <value>The role of this person associated with the offering   - student: student   - lecturer: docent   - teaching assistant: studentassistent   - coordinator: coÃ¶rdinator   - guest: gast </value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum AssociationRoleEnum
{
    /// <summary>
    /// Enum StudentEnum for student
    /// </summary>
    [EnumMember(Value = "student")]
    StudentEnum = 0,
    /// <summary>
    /// Enum LecturerEnum for lecturer
    /// </summary>
    [EnumMember(Value = "lecturer")]
    LecturerEnum = 1,
    /// <summary>
    /// Enum TeachingAssistantEnum for teaching assistant
    /// </summary>
    [EnumMember(Value = "teaching assistant")]
    TeachingAssistantEnum = 2,
    /// <summary>
    /// Enum CoordinatorEnum for coordinator
    /// </summary>
    [EnumMember(Value = "coordinator")]
    CoordinatorEnum = 3,
    /// <summary>
    /// Enum GuestEnum for guest
    /// </summary>
    [EnumMember(Value = "guest")]
    GuestEnum = 4
}