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
    public class Persons : Pagination
    {
        /// <summary>
        /// Array of objects (Person) 
        /// </summary>
        /// <value>Array of objects (Person) </value>
        [JsonRequired]

        [DataMember(Name = "items")]
        public List<Person> Items { get; set; }


    }
}
