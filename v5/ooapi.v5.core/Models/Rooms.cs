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
    public class Rooms : Pagination
    {
        /// <summary>
        /// Array of objects (Room) 
        /// </summary>
        /// <value>Array of objects (Room) </value>
        [JsonRequired]

        [JsonProperty(PropertyName = "items")]
        public List<Room> Items { get; set; }


    }
}
