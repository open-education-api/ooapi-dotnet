using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Courses : Pagination<Course>
    {
        /// <summary>
        /// Array of objects (Course) 
        /// </summary>
        /// <value>Array of objects (Course) </value>
        [JsonRequired]

        [JsonProperty(PropertyName = "items")]
        public List<Course> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
            }
        }
    }
}
