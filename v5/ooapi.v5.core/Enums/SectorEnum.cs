namespace ooapi.v5.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;

/// <summary>
/// The sector for this program - secondary vocational education: middelbaar beroepsonderwijs - higher professional education: hoger beroepsonderwijs - university education: universitair onderwijs 
/// </summary>
/// <value>The sector for this program - secondary vocational education: middelbaar beroepsonderwijs - higher professional education: hoger beroepsonderwijs - university education: universitair onderwijs </value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum SectorEnum
{
    /// <summary>
    /// Enum SecondaryVocationalEducationEnum for secondary vocational education
    /// </summary>
    [EnumMember(Value = "secondary vocational education")]
    SecondaryVocationalEducationEnum = 0,
    /// <summary>
    /// Enum HigherProfessionalEducationEnum for higher professional education
    /// </summary>
    [EnumMember(Value = "higher professional education")]
    HigherProfessionalEducationEnum = 1,
    /// <summary>
    /// Enum UniversityEducationEnum for university education
    /// </summary>
    [EnumMember(Value = "university education")]
    UniversityEducationEnum = 2
}