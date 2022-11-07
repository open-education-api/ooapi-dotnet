using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Associations : Pagination<Association>
    {
        /// <summary>
        /// Array of objects (Association) 
        /// </summary>
        /// <value>Array of objects (Association) </value>
        [JsonRequired]
        [JsonProperty(PropertyName = "items")]
        public List<Association> Items
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
