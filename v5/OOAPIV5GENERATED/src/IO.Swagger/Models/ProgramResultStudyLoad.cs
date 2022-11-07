using IO.Swagger.Enums;
using System.Runtime.Serialization;

namespace IO.Swagger.Models
{
    /// <summary>
    /// The amount of effort to complete this education in the specified unit.
    /// </summary>
    [DataContract]
    public partial class ProgramResultStudyLoad
    {


        /// <summary>
        /// The unit in which the studyload is specfied - contacttime: CONTACTUUR amount of time spent in classes - ects: ECTS_PUNT European Credit Transfer System - sbu: SBU studentloadhours - sp: STUDIEPUNT studentpoints - hour: UUR hours 
        /// </summary>
        /// <value>The unit in which the studyload is specfied - contacttime: CONTACTUUR amount of time spent in classes - ects: ECTS_PUNT European Credit Transfer System - sbu: SBU studentloadhours - sp: STUDIEPUNT studentpoints - hour: UUR hours </value>

        [DataMember(Name = "studyLoadUnit")]
        public StudyLoadUnitEnum? StudyLoadUnit { get; set; }

        /// <summary>
        /// The amount of load depicted in numbers
        /// </summary>
        /// <value>The amount of load depicted in numbers</value>

        [DataMember(Name = "value")]
        public decimal? Value { get; set; }
    }
}
