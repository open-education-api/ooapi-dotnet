using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class NewsFeeds : Pagination<NewsFeed>
    {
        /// <summary>
        /// Array of objects (NewsFeed) 
        /// </summary>
        /// <value>Array of objects (NewsFeed) </value>
        [JsonRequired]

        [JsonProperty(PropertyName = "items")]
        public List<NewsFeed> Items
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
