using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    [DataContract]
    public class ModelBase
    {

        [JsonIgnore]
        public string Extension { get; set; }

        [JsonProperty(PropertyName = "ext")]
        [NotMapped]
        /// <summary>
        /// Object for additional non-standard attributes
        /// </summary>
        public JObject? Ext
        {
            get
            {
                if (string.IsNullOrEmpty(Extension))
                    return null;
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
                Extension = value.ToString();
            }
        }

    }
}
