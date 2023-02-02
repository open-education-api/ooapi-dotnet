namespace ooapi.v5.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;

/// <summary>
/// The type of education specification   - program: HOOPLEIDING   - privateProgram: PARTICULIEREOPLEIDING   - programCluster: HOONDERWIJSEENHEDENCLUSTER   - course: HOONDERWIJSEENHEID 
/// </summary>
/// <value>The type of education specification   - program: HOOPLEIDING   - privateProgram: PARTICULIEREOPLEIDING   - programCluster: HOONDERWIJSEENHEDENCLUSTER   - course: HOONDERWIJSEENHEID </value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum EducationSpecificationTypeEnum
{
    /// <summary>
    /// Enum ProgramEnum for program
    /// </summary>
    [EnumMember(Value = "program")]
    program = 0,
    /// <summary>
    /// Enum PrivateProgramEnum for privateProgram
    /// </summary>
    [EnumMember(Value = "privateProgram")]
    privateProgram = 1,
    /// <summary>
    /// Enum ClusterEnum for cluster
    /// </summary>
    [EnumMember(Value = "cluster")]
    cluster = 2,
    /// <summary>
    /// Enum CourseEnum for course
    /// </summary>
    [EnumMember(Value = "course")]
    course = 3
}