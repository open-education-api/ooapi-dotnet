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
    public class Groups : Pagination
    {
        /// <summary>
        /// Array of objects (Group) 
        /// </summary>
        /// <value>Array of objects (Group) </value>
        [Required]

        [DataMember(Name = "items")]
        public List<Group> Items { get; set; }


    }
}