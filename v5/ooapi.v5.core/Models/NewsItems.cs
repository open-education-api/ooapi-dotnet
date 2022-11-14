using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class NewsItems : Pagination<NewsItem>
    {
        /// <summary>
        /// Array of objects (NewsItem) 
        /// </summary>
        /// <value>Array of objects (NewsItem) </value>
        [JsonRequired]

        [JsonProperty(PropertyName = "items")]
        public List<NewsItem> Items
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
