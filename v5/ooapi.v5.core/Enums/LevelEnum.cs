namespace ooapi.v5.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;

/// <summary>
/// The level of this course (ECTS-year of study if applicable) - secondary vocational education: mbo - secondary vocational education 1: mbo 1, corresponds to levelOfQualification 1 - secondary vocational education 2: mbo 2, corresponds to levelOfQualification 2 - secondary vocational education 3: mbo 3, corresponds to levelOfQualification 3 - secondary vocational education 4: mbo 4, corresponds to levelOfQualification 4 - associate degree: associate degree, corresponds to levelOfQualification 5 - bachelor: bachelor, corresponds to levelOfQualification 6 - master: master, corresponds to levelOfQualification 7 - doctoral: doctoraal, corresponds to levelOfQualification 8 - undefined: onbepaald - undivided: ongedeeld - nt2-1: NT2 niveau 1 - nt2-2: NT2 niveau 2 
/// </summary>
/// <value>The level of this course (ECTS-year of study if applicable) - secondary vocational education: mbo - secondary vocational education 1: mbo 1, corresponds to levelOfQualification 1 - secondary vocational education 2: mbo 2, corresponds to levelOfQualification 2 - secondary vocational education 3: mbo 3, corresponds to levelOfQualification 3 - secondary vocational education 4: mbo 4, corresponds to levelOfQualification 4 - associate degree: associate degree, corresponds to levelOfQualification 5 - bachelor: bachelor, corresponds to levelOfQualification 6 - master: master, corresponds to levelOfQualification 7 - doctoral: doctoraal, corresponds to levelOfQualification 8 - undefined: onbepaald - undivided: ongedeeld - nt2-1: NT2 niveau 1 - nt2-2: NT2 niveau 2 </value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum LevelEnum
{
    /// <summary>
    /// Enum SecondaryVocationalEducationEnum for secondary vocational education
    /// </summary>
    [EnumMember(Value = "secondary vocational education")]
    secondary_vocational_education = 0,
    /// <summary>
    /// Enum SecondaryVocationalEducation1Enum for secondary vocational education 1
    /// </summary>
    [EnumMember(Value = "secondary vocational education 1")]
    secondary_vocational_education_1 = 1,
    /// <summary>
    /// Enum SecondaryVocationalEducation2Enum for secondary vocational education 2
    /// </summary>
    [EnumMember(Value = "secondary vocational education 2")]
    secondary_vocational_education_2= 2,
    /// <summary>
    /// Enum SecondaryVocationalEducation3Enum for secondary vocational education 3
    /// </summary>
    [EnumMember(Value = "secondary vocational education 3")]
    secondary_vocational_education_3 = 3,
    /// <summary>
    /// Enum SecondaryVocationalEducation4Enum for secondary vocational education 4
    /// </summary>
    [EnumMember(Value = "secondary vocational education 4")]
    secondary_vocational_education_4 = 4,
    /// <summary>
    /// Enum AssociateDegreeEnum for associate degree
    /// </summary>
    [EnumMember(Value = "associate degree")]
    associate_degree = 5,
    /// <summary>
    /// Enum BachelorEnum for bachelor
    /// </summary>
    [EnumMember(Value = "bachelor")]
    bachelor = 6,
    /// <summary>
    /// Enum MasterEnum for master
    /// </summary>
    [EnumMember(Value = "master")]
    master = 7,
    /// <summary>
    /// Enum DoctoralEnum for doctoral
    /// </summary>
    [EnumMember(Value = "doctoral")]
    doctoral = 8,
    /// <summary>
    /// Enum UndefinedEnum for undefined
    /// </summary>
    [EnumMember(Value = "undefined")]
    undefined = 9,
    /// <summary>
    /// Enum UndividedEnum for undivided
    /// </summary>
    [EnumMember(Value = "undivided")]
    undivided = 10,
    /// <summary>
    /// Enum Nt21Enum for nt2-1
    /// </summary>
    [EnumMember(Value = "nt2-1")]
    nt2_1 = 11,
    /// <summary>
    /// Enum Nt22Enum for nt2-2
    /// </summary>
    [EnumMember(Value = "nt2-2")]
    nt2_2 = 12
}