using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
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

        [JsonProperty(PropertyName = "items")]
        public List<Group> Items { get; set; }


    }
}
