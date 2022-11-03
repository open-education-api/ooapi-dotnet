using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class CourseOfferings : Pagination<CourseOffering>
    {
        /// <summary>
        /// Array of objects (CourseOffering) 
        /// </summary>
        /// <value>Array of objects (CourseOffering) </value>
        [JsonRequired]

        [JsonProperty(PropertyName = "items")]
        public List<CourseOffering> Items
        {
            get
            {
                return PaginationItems;
            }
            set
            {
                PaginationItems = value;
            }
        }
    }
}
