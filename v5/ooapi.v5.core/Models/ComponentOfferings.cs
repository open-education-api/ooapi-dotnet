using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class ComponentOfferings : Pagination<ComponentOffering>
    {
        /// <summary>
        /// Array of objects (ComponentOffering) 
        /// </summary>
        /// <value>Array of objects (ComponentOffering) </value>
        [JsonRequired]

        [JsonProperty(PropertyName = "items")]
        public List<ComponentOffering> Items
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
