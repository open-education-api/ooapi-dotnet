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
    public class Courses : Pagination
    {
        /// <summary>
        /// Array of objects (Course) 
        /// </summary>
        /// <value>Array of objects (Course) </value>
        [Required]

        [DataMember(Name = "items")]
        public List<Course> Items { get; set; }


    }
}
