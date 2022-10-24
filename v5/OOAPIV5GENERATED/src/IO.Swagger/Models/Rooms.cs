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
    public class Rooms : Pagination
    {
        /// <summary>
        /// Array of objects (Room) 
        /// </summary>
        /// <value>Array of objects (Room) </value>
        [JsonRequired]

        [DataMember(Name = "items")]
        public List<Room> Items { get; set; }


    }
}
