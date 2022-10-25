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
    public class Programs : Pagination
    {
        /// <summary>
        /// Array of objects (Program) 
        /// </summary>
        /// <value>Array of objects (Program) </value>
        [Required]

        [DataMember(Name = "items")]
        public List<Program> Items { get; set; }


    }
}