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
    public class ComponentOfferings : Pagination
    {
        /// <summary>
        /// Array of objects (ComponentOffering) 
        /// </summary>
        /// <value>Array of objects (ComponentOffering) </value>
        [JsonRequired]

        [DataMember(Name = "items")]
        public List<ComponentOffering> Items { get; set; }


    }
}
