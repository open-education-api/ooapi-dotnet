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
    public class Offerings : Pagination
    {
        /// <summary>
        /// Array of objects (Offering) 
        /// </summary>
        /// <value>Array of objects (Offering) </value>
        [JsonRequired]

        [DataMember(Name = "items")]
        public List<OneOfOfferingNoIdentifier> Items { get; set; }


    }
}
