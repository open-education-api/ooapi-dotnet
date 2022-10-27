using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class ComponentResult : Result
    {
        /// <summary>
        /// The weight to 100 as total for this offering in the course
        /// </summary>
        /// <value>The weight to 100 as total for this offering in the course</value>
        [JsonRequired]

        [Range(0, 100)]
        [JsonProperty(PropertyName = "weight")]
        public int? Weight { get; set; }

    }
}
