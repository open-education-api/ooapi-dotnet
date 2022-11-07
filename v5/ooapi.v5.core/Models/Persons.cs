using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Persons : Pagination<Person>
    {
        /// <summary>
        /// Array of objects (Person) 
        /// </summary>
        /// <value>Array of objects (Person) </value>
        [JsonRequired]

        [JsonProperty(PropertyName = "items")]
        public List<Person> Items
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
