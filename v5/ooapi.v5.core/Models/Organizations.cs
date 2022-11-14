using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Organizations : Pagination<Organization>
    {
        /// <summary>
        /// Array of objects (Organization) 
        /// </summary>
        /// <value>Array of objects (Organization) </value>
        [JsonRequired]

        [JsonProperty(PropertyName = "items")]
        public List<Organization> Items
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
