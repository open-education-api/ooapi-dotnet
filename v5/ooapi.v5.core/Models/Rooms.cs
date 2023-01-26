using Newtonsoft.Json;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Rooms : Pagination<Room>
    {
        /// <summary>
        /// Array of objects (Room) 
        /// </summary>
        /// <value>Array of objects (Room) </value>
        [JsonRequired]
        [JsonProperty(PropertyName = "items")]
        public override List<Room> Items
        {
            get
            {
                return _items;
            }
        }
    }
}
