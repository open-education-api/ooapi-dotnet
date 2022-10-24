namespace ooapi.v5.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;

/// <summary>
/// The gender of this person
/// </summary>
/// <value>The gender of this person</value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum GenderEnum
{
    /// <summary>
    /// Enum MEnum for M
    /// </summary>
    [EnumMember(Value = "M")]
    MEnum = 0,
    /// <summary>
    /// Enum FEnum for F
    /// </summary>
    [EnumMember(Value = "F")]
    FEnum = 1,
    /// <summary>
    /// Enum UEnum for U
    /// </summary>
    [EnumMember(Value = "U")]
    UEnum = 2,
    /// <summary>
    /// Enum XEnum for X
    /// </summary>
    [EnumMember(Value = "X")]
    XEnum = 3
}