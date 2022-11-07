using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IO.Swagger.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Associations : Pagination
    {
        /// <summary>
        /// Array of objects (Association) 
        /// </summary>
        /// <value>Array of objects (Association) </value>
        [JsonRequired]

        [DataMember(Name = "items")]
        public List<Association> Items { get; set; }


    }
}
