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
                if (string.IsNullOrEmpty(StudyLoadDescriptor))
                    return null;
                try
                {
                    JObject jObject = JsonConvert.DeserializeObject<JObject>(StudyLoadDescriptor);
                    StudyLoadDescriptor result = new StudyLoadDescriptor();
                    result.StudyLoadUnit = jObject.Value<string>("studyLoadUnit");
                    result.Value = jObject.Value<decimal>("value");
                    return result;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            set
            {
                StudyLoadDescriptor = value.ToString();
            }
        }

        [JsonIgnore]
        public string? StudyLoadDescriptor { get; set; }

    }
}
