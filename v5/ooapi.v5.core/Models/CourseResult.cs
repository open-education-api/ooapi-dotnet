using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
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

        [JsonProperty(PropertyName = "studyLoad")]
        [NotMapped]
        public StudyLoadDescriptor StudyLoad { get; set; }


        [JsonIgnore]
        public Guid StudyLoadDescriptorId { get; set; }


    }
}
