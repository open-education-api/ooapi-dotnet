using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Associations : Pagination
    {
        /// <summary>
        /// Array of objects (Association) 
        /// </summary>
        /// <value>Array of objects (Association) </value>
        [Required]

        [DataMember(Name = "items")]
        public List<Association> Items { get; set; }


    }
}
