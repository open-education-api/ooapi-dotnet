namespace IO.Swagger.Enums;

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
    ProgramEnum = 0,
    /// <summary>
    /// Enum PrivateProgramEnum for privateProgram
    /// </summary>
    [EnumMember(Value = "privateProgram")]
    PrivateProgramEnum = 1,
    /// <summary>
    /// Enum ClusterEnum for cluster
    /// </summary>
    [EnumMember(Value = "cluster")]
    ClusterEnum = 2,
    /// <summary>
    /// Enum CourseEnum for course
    /// </summary>
    [EnumMember(Value = "course")]
    CourseEnum = 3
}