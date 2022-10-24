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
    public class CourseOfferings : Pagination
    {
        /// <summary>
        /// Array of objects (CourseOffering) 
        /// </summary>
        /// <value>Array of objects (CourseOffering) </value>
        [Required]

        [DataMember(Name = "items")]
        public List<CourseOffering> Items { get; set; }


    }
}
