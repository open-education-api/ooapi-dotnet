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
    public class ProgramResult : Result
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
                if (string.IsNullOrEmpty(StudyLoadUnit) || StudyLoadValue == null)
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
        public decimal? StudyLoadValue { get; set; }


    }
}
