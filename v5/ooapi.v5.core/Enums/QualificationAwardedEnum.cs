namespace ooapi.v5.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;

/// <summary>
/// Type of qualificaton that can be obtained on finishing the program
/// </summary>
/// <value>Type of qualificaton that can be obtained on finishing the program</value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum QualificationAwardedEnum
{
    /// <summary>
    /// Enum ADEnum for AD
    /// </summary>
    [EnumMember(Value = "AD")]
    ADEnum = 0,
    /// <summary>
    /// Enum BAEnum for BA
    /// </summary>
    [EnumMember(Value = "BA")]
    BAEnum = 1,
    /// <summary>
    /// Enum BScEnum for BSc
    /// </summary>
    [EnumMember(Value = "BSc")]
    BScEnum = 2,
    /// <summary>
    /// Enum LLBEnum for LLB
    /// </summary>
    [EnumMember(Value = "LLB")]
    LLBEnum = 3,
    /// <summary>
    /// Enum MAEnum for MA
    /// </summary>
    [EnumMember(Value = "MA")]
    MAEnum = 4,
    /// <summary>
    /// Enum MScEnum for MSc
    /// </summary>
    [EnumMember(Value = "MSc")]
    MScEnum = 5,
    /// <summary>
    /// Enum LLMEnum for LLM
    /// </summary>
    [EnumMember(Value = "LLM")]
    LLMEnum = 6,
    /// <summary>
    /// Enum PhdEnum for Phd
    /// </summary>
    [EnumMember(Value = "Phd")]
    PhdEnum = 7,
    /// <summary>
    /// Enum NoneEnum for None
    /// </summary>
    [EnumMember(Value = "None")]
    NoneEnum = 8
}