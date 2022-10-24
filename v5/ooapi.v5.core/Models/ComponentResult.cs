using Newtonsoft.Json;
using System;
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
        [Required]

        [Range(0, 100)]
        [DataMember(Name = "weight")]
        public int? Weight { get; set; }

    }
}
