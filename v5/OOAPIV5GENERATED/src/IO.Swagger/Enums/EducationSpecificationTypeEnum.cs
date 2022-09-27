using System.Runtime.Serialization;

namespace IO.Swagger.Enums
{

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
}