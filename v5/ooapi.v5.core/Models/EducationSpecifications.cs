using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class EducationSpecifications : Pagination
    {
        /// <summary>
        /// Array of objects (EducationSpecification) 
        /// </summary>
        /// <value>Array of objects (EducationSpecification) </value>
        [JsonRequired]

        [JsonProperty(PropertyName = "items")]
        public List<EducationSpecification> Items { get; set; }


    }
}
