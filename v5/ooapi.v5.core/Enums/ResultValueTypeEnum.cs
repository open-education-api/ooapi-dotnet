namespace ooapi.v5.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;

/// <summary>
/// The result value type for this offering
/// </summary>
/// <value>The result value type for this offering</value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum ResultValueTypeEnum
{
    /// <summary>
    /// Enum PassOrFailEnum for pass-or-fail
    /// </summary>
    [EnumMember(Value = "pass-or-fail")]
    pass_or_fail = 0,
    /// <summary>
    /// Enum USLetterEnum for US letter
    /// </summary>
    [EnumMember(Value = "US letter")]
    US_letter = 1,
    /// <summary>
    /// Enum UKLetterEnum for UK letter
    /// </summary>
    [EnumMember(Value = "UK letter")]
    UK_letter = 2,
    /// <summary>
    /// Enum _0100Enum for 0-100
    /// </summary>
    [EnumMember(Value = "0-100")]
    _0_100 = 3,
    /// <summary>
    /// Enum _110Enum for 1-10
    /// </summary>
    [EnumMember(Value = "1-10")]
    _1_10 = 4
}