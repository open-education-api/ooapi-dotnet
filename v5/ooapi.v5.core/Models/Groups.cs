using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Groups : Pagination<Group>
    {
        /// <summary>
        /// Array of objects (Group) 
        /// </summary>
        /// <value>Array of objects (Group) </value>
        [JsonRequired]

        [JsonProperty(PropertyName = "items")]
        public List<Group> Items
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
