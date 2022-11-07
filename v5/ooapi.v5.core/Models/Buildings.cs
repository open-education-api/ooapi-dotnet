using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Buildings : Pagination<Building>
    {
        /// <summary>
        /// Array of objects (Building) 
        /// </summary>
        /// <value>Array of objects (Building) </value>
        [JsonRequired]

        [JsonProperty(PropertyName = "items")]
        public List<Building> Items
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
