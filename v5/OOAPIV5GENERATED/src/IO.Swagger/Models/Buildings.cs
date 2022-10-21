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
    public class Buildings : Pagination
    {
        /// <summary>
        /// Array of objects (Building) 
        /// </summary>
        /// <value>Array of objects (Building) </value>
        [Required]

        [DataMember(Name = "items")]
        public List<Building> Items { get; set; }


    }
}
