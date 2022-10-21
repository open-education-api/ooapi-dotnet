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
    public class EducationSpecifications : Pagination
    {
        /// <summary>
        /// Array of objects (EducationSpecification) 
        /// </summary>
        /// <value>Array of objects (EducationSpecification) </value>
        [Required]

        [DataMember(Name = "items")]
        public List<EducationSpecification> Items { get; set; }


    }
}
