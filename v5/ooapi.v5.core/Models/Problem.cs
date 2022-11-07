using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// A system message including the error code and an explanation
    /// </summary>
    [DataContract]
    public partial class Problem
    {
        /// <summary>
        /// The HTTP status code
        /// </summary>
        /// <value>The HTTP status code</value>
        [JsonRequired]

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        /// <summary>
        /// A short, human-readable summary of the problem type
        /// </summary>
        /// <value>A short, human-readable summary of the problem type</value>
        [JsonRequired]

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// A human-readable explanation specific to this occurrence of the problem
        /// </summary>
        /// <value>A human-readable explanation specific to this occurrence of the problem</value>

        [JsonProperty(PropertyName = "detail")]
        public string Detail { get; set; }


    }
}
