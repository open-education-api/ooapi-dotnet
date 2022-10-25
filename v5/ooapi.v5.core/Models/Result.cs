using Newtonsoft.Json;
using ooapi.v5.Enums;
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
        [Required]

        [DataMember(Name = "state")]
        public ResultStateEnum State { get; set; }


        /// <summary>
        /// The state of this result
        /// </summary>
        /// <value>The state of this result</value>

        [DataMember(Name = "pass")]
        public PassEnum? Pass { get; set; }

        /// <summary>
        /// The comment on this result
        /// </summary>
        /// <value>The comment on this result</value>

        [DataMember(Name = "comment")]
        public string Comment { get; set; }

        /// <summary>
        /// The score of this program/course/component association (based on resultValueType in offering)
        /// </summary>
        /// <value>The score of this program/course/component association (based on resultValueType in offering)</value>

        [DataMember(Name = "score")]
        public string Score { get; set; }

        /// <summary>
        /// The date this result has been published, RFC3339 (full-date)
        /// </summary>
        /// <value>The date this result has been published, RFC3339 (full-date)</value>
        [Required]

        [DataMember(Name = "resultDate")]
        public DateTime ResultDate { get; set; }


    }
}
