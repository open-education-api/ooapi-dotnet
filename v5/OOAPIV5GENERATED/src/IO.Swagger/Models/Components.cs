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
    public class Components : Pagination
    {
        /// <summary>
        /// Array of objects (Component) 
        /// </summary>
        /// <value>Array of objects (Component) </value>
        [Required]

        [DataMember(Name = "items")]
        public List<Component> Items { get; set; }


    }
}
