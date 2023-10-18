using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;

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
            if (string.IsNullOrEmpty(StudyLoadUnit) || StudyLoadValue == 0)
            {
                return null;
            }

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
            if (value is not null)
            {
                StudyLoadUnit = value.StudyLoadUnit;
                StudyLoadValue = value.Value;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public string? StudyLoadUnit { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public int StudyLoadValue { get; set; }
}
