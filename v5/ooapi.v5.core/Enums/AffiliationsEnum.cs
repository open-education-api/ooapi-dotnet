namespace ooapi.v5.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;


/// <summary>
/// Gets or Sets Affiliations
/// </summary>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum AffiliationsEnum
{
    /// <summary>
    /// Enum StudentEnum for student
    /// </summary>
    [EnumMember(Value = "student")]
    student = 0,
    /// <summary>
    /// Enum EmployeeEnum for employee
    /// </summary>
    [EnumMember(Value = "employee")]
    employee = 1,
    /// <summary>
    /// Enum GuestEnum for guest
    /// </summary>
    [EnumMember(Value = "guest")]
    guest = 2
}