using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class ProgramOfferings : Pagination<ProgramOffering>
    {
        /// <summary>
        /// Array of objects (ProgramOffering) 
        /// </summary>
        /// <value>Array of objects (ProgramOffering) </value>
        [JsonRequired]

        [JsonProperty(PropertyName = "items")]
        public List<ProgramOffering> Items
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
