using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace IO.Swagger.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Offerings : Pagination
    {
        /// <summary>
        /// Array of objects (Offering) 
        /// </summary>
        /// <value>Array of objects (Offering) </value>
        [Required]

        [DataMember(Name = "items")]
        public List<OneOfOfferingNoIdentifier> Items { get; set; }


    }
}
