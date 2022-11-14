using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Programs : Pagination<Program>
    {
        /// <summary>
        /// Array of objects (Program) 
        /// </summary>
        /// <value>Array of objects (Program) </value>
        [JsonRequired]

        [JsonProperty(PropertyName = "items")]
        public List<Program> Items
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
