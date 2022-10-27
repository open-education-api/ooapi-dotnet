using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class CourseResult : Result
    {
        /// <summary>
        /// Gets or Sets StudyLoad
        /// </summary>
        [JsonRequired]

        [DataMember(Name = "studyLoad")]
        public ProgramResultStudyLoad StudyLoad { get; set; }


    }
}
