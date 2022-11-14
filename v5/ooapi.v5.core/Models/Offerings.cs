using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Offerings : Pagination<OneOfOfferingNoIdentifier>
    {
        /// <summary>
        /// Array of objects (Offering) 
        /// </summary>
        /// <value>Array of objects (Offering) </value>
        [JsonRequired]

        [JsonProperty(PropertyName = "items")]
        public List<OneOfOfferingNoIdentifier> Items
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
