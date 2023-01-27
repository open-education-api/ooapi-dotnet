namespace ooapi.v5.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;

/// <summary>
/// The type of this organization. Each OOAPI endpoint should have a single organization with type `root`, describing the root organization. - root: the root of this organization, representing the Educational Institution itself - institute: instituut - department: departement - faculty: faculteit - branch: vestiging - academy: academie - school: school 
/// </summary>
/// <value>The type of this organization. Each OOAPI endpoint should have a single organization with type `root`, describing the root organization. - root: the root of this organization, representing the Educational Institution itself - institute: instituut - department: departement - faculty: faculteit - branch: vestiging - academy: academie - school: school </value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum OrganizationTypeEnum
{
    /// <summary>
    /// Enum RootEnum for root
    /// </summary>
    [EnumMember(Value = "root")]
    root = 0,
    /// <summary>
    /// Enum InstituteEnum for institute
    /// </summary>
    [EnumMember(Value = "institute")]
    institute = 1,
    /// <summary>
    /// Enum DepartmentEnum for department
    /// </summary>
    [EnumMember(Value = "department")]
    department = 2,
    /// <summary>
    /// Enum FacultyEnum for faculty
    /// </summary>
    [EnumMember(Value = "faculty")]
    faculty = 3,
    /// <summary>
    /// Enum BranchEnum for branch
    /// </summary>
    [EnumMember(Value = "branch")]
    branch = 4,
    /// <summary>
    /// Enum AcademyEnum for academy
    /// </summary>
    [EnumMember(Value = "academy")]
    academy = 5,
    /// <summary>
    /// Enum SchoolEnum for school
    /// </summary>
    [EnumMember(Value = "school")]
    school = 6
}
