using Newtonsoft.Json;
using ooapi.v5.Enums;
using ooapi.v5.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// A result as part of an association
    /// </summary>
    [DataContract]
    public partial class Result : ModelBase
    {

        /// <summary>
        /// The state of this result
        /// </summary>
        /// <value>The state of this result</value>
        [JsonRequired]

        [JsonProperty(PropertyName = "state")]
        public ResultStateEnum State { get; set; }


        /// <summary>
        /// The state of this result
        /// </summary>
        /// <value>The state of this result</value>

        [JsonProperty(PropertyName = "pass")]
        public PassEnum? Pass { get; set; }

        /// <summary>
        /// The comment on this result
        /// </summary>
        /// <value>The comment on this result</value>

        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }

        /// <summary>
        /// The score of this program/course/component association (based on resultValueType in offering)
        /// </summary>
        /// <value>The score of this program/course/component association (based on resultValueType in offering)</value>

        [JsonProperty(PropertyName = "score")]
        public string Score { get; set; }

        /// <summary>
        /// The date this result has been published, RFC3339 (full-date)
        /// </summary>
        /// <value>The date this result has been published, RFC3339 (full-date)</value>
        [JsonRequired]

        [JsonProperty(PropertyName = "resultDate")]
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
        public DateTime ResultDate { get; set; }


    }
}
