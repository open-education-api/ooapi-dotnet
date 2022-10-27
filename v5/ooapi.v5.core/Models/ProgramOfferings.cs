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
    public class ProgramOfferings : Pagination
    {
        /// <summary>
        /// Array of objects (ProgramOffering) 
        /// </summary>
        /// <value>Array of objects (ProgramOffering) </value>
        [JsonRequired]

        [DataMember(Name = "items")]
        public List<ProgramOffering> Items { get; set; }


    }
}
