using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;

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
            {
                return null;
            }

            try
            {
                var result = new StudyLoadDescriptor();
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
            if (value is not null)
            {
                StudyLoadUnit = value.StudyLoadUnit;
                StudyLoadValue = value.Value;
            }
        }
    }

    [JsonIgnore]
    public string? StudyLoadUnit { get; set; }

    [JsonIgnore]
    public int? StudyLoadValue { get; set; }
}
