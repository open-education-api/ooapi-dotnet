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
    public class Components : Pagination
    {
        /// <summary>
        /// Array of objects (Component) 
        /// </summary>
        /// <value>Array of objects (Component) </value>
        [JsonRequired]

        [JsonProperty(PropertyName = "items")]
        public List<Component> Items { get; set; }


    }
}
