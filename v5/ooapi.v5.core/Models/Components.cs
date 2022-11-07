using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Components : Pagination<Component>
    {
        /// <summary>
        /// Array of objects (Component) 
        /// </summary>
        /// <value>Array of objects (Component) </value>
        [JsonRequired]

        [JsonProperty(PropertyName = "items")]
        public List<Component> Items
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
