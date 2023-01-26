using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public StudyLoadDescriptor? StudyLoad
        {
            get
            {
                if (string.IsNullOrEmpty(StudyLoadUnit) || StudyLoadValue == 0)
                    return null;
                try
                {

                    StudyLoadDescriptor result = new StudyLoadDescriptor();
                    result.StudyLoadUnit = StudyLoadUnit;
                    result.Value = StudyLoadValue;
                    return result;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            set
            {
                StudyLoadUnit = value.StudyLoadUnit;
                StudyLoadValue = value.Value;
            }
        }

        [JsonIgnore]
        public string? StudyLoadUnit { get; set; }

        [JsonIgnore]
        public int StudyLoadValue { get; set; }


    }
}
