using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;

/// <summary>
/// 
/// </summary>
[DataContract]
public class ModelBase
{
    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public string? Extension { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty(PropertyName = "ext")]
    [NotMapped]
    public dynamic? Ext
    {
        get
        {
            if (string.IsNullOrEmpty(Extension))
            {
                return null;
            }

            try
            {
                return JsonConvert.DeserializeObject<JObject>(Extension);
            }
            catch (Exception)
            {
                return null;
            }
        }
        set
        {
            Extension = value?.ToString();
        }
    }
}

