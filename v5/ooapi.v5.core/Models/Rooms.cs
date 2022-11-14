using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Rooms : Pagination<Room>
    {
        /// <summary>
        /// Array of objects (Room) 
        /// </summary>
        /// <value>Array of objects (Room) </value>
        [JsonRequired]

        [JsonProperty(PropertyName = "items")]
        public List<Room> Items
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
