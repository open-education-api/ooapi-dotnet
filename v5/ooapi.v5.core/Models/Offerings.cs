using Newtonsoft.Json;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Offerings : Pagination<OneOfOfferingNoIdentifier>
    {
        /// <summary>
        /// Array of objects (Offering) 
        /// </summary>
        /// <value>Array of objects (Offering) </value>
        [JsonRequired]
        [JsonProperty(PropertyName = "items")]
        public override List<OneOfOfferingNoIdentifier> Items
        {
            get
            {
                return _items;
            }
        }
    }
}
