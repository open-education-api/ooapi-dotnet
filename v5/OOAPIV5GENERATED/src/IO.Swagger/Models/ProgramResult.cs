using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace IO.Swagger.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class ProgramResult : Result
    {
        /// <summary>
        /// Gets or Sets StudyLoad
        /// </summary>
        [JsonRequired]

        [DataMember(Name = "studyLoad")]
        public ProgramResultStudyLoad StudyLoad { get; set; }


    }
}
