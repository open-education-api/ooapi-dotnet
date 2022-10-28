using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class CourseOfferings : Pagination
    {
        /// <summary>
        /// Array of objects (CourseOffering) 
        /// </summary>
        /// <value>Array of objects (CourseOffering) </value>
        [JsonRequired]

        [JsonProperty(PropertyName = "items")]
        public List<CourseOffering> Items { get; set; }


    }
}
