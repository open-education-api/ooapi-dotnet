namespace ooapi.v5.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;

/// <summary>
/// Level of qualification according to the Dutch National Qualification Framework and the European Qualifications Framework, see [this overview](https://nlqf.nl/images/downloads/English2018/Schematic_overview_NLQF_2020.pdf) for more information.
/// </summary>
/// <value>Level of qualification according to the Dutch National Qualification Framework and the European Qualifications Framework, see [this overview](https://nlqf.nl/images/downloads/English2018/Schematic_overview_NLQF_2020.pdf) for more information.</value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum LevelOfQualificationEnum
{
    /// <summary>
    /// Enum NUMBER_1 for 1
    /// </summary>
    [EnumMember(Value = "1")]
    NUMBER_1 = 0,
    /// <summary>
    /// Enum NUMBER_2 for 2
    /// </summary>
    [EnumMember(Value = "2")]
    NUMBER_2 = 1,
    /// <summary>
    /// Enum NUMBER_3 for 3
    /// </summary>
    [EnumMember(Value = "3")]
    NUMBER_3 = 2,
    /// <summary>
    /// Enum NUMBER_4 for 4
    /// </summary>
    [EnumMember(Value = "4")]
    NUMBER_4 = 3,
    /// <summary>
    /// Enum _4Enum for 4+
    /// </summary>
    [EnumMember(Value = "4+")]
    _4Enum = 4,
    /// <summary>
    /// Enum NUMBER_5 for 5
    /// </summary>
    [EnumMember(Value = "5")]
    NUMBER_5 = 5,
    /// <summary>
    /// Enum NUMBER_6 for 6
    /// </summary>
    [EnumMember(Value = "6")]
    NUMBER_6 = 6,
    /// <summary>
    /// Enum NUMBER_7 for 7
    /// </summary>
    [EnumMember(Value = "7")]
    NUMBER_7 = 7,
    /// <summary>
    /// Enum NUMBER_8 for 8
    /// </summary>
    [EnumMember(Value = "8")]
    NUMBER_8 = 8
}