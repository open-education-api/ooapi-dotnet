using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace IO.Swagger.Models
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

        [DataMember(Name = "status")]
        public string Status { get; set; }

        /// <summary>
        /// A short, human-readable summary of the problem type
        /// </summary>
        /// <value>A short, human-readable summary of the problem type</value>
        [JsonRequired]

        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// A human-readable explanation specific to this occurrence of the problem
        /// </summary>
        /// <value>A human-readable explanation specific to this occurrence of the problem</value>

        [DataMember(Name = "detail")]
        public string Detail { get; set; }
    }
}
