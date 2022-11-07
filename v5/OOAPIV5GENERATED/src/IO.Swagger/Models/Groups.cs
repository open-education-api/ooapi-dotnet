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
    public class Groups : Pagination
    {
        /// <summary>
        /// Array of objects (Group) 
        /// </summary>
        /// <value>Array of objects (Group) </value>
        [JsonRequired]

        [DataMember(Name = "items")]
        public List<Group> Items { get; set; }


    }
}
