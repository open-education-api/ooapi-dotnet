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
    public class Organizations : Pagination
    {
        /// <summary>
        /// Array of objects (Organization) 
        /// </summary>
        /// <value>Array of objects (Organization) </value>
        [JsonRequired]

        [JsonProperty(PropertyName = "items")]
        public List<Organization> Items { get; set; }


    }
}
