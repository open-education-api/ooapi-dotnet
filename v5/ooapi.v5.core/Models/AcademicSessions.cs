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
    public class AcademicSessions : Pagination
    {
        /// <summary>
        /// Array of objects (AcademicSession) 
        /// </summary>
        /// <value>Array of objects (AcademicSessions) </value>
        [Required]

        [DataMember(Name = "items")]
        public List<AcademicSession> Items { get; set; }


    }
}
