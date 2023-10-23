using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Enums;

/// <summary>
/// Type of qualificaton that can be obtained on finishing the program
/// </summary>
/// <value>Type of qualificaton that can be obtained on finishing the program</value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum QualificationAwarded
{
    /// <summary>
    /// Enum ADEnum for AD
    /// </summary>
    [EnumMember(Value = "AD")]
    AD = 0,
    /// <summary>
    /// Enum BAEnum for BA
    /// </summary>
    [EnumMember(Value = "BA")]
    BA = 1,
    /// <summary>
    /// Enum BScEnum for BSc
    /// </summary>
    [EnumMember(Value = "BSc")]
    BSc = 2,
    /// <summary>
    /// Enum LLBEnum for LLB
    /// </summary>
    [EnumMember(Value = "LLB")]
    LLB = 3,
    /// <summary>
    /// Enum MAEnum for MA
    /// </summary>
    [EnumMember(Value = "MA")]
    MA = 4,
    /// <summary>
    /// Enum MScEnum for MSc
    /// </summary>
    [EnumMember(Value = "MSc")]
    MSc = 5,
    /// <summary>
    /// Enum LLMEnum for LLM
    /// </summary>
    [EnumMember(Value = "LLM")]
    LLM = 6,
    /// <summary>
    /// Enum PhdEnum for Phd
    /// </summary>
    [EnumMember(Value = "Phd")]
    Phd = 7,
    /// <summary>
    /// Enum NoneEnum for None
    /// </summary>
    [EnumMember(Value = "None")]
    None = 8
}