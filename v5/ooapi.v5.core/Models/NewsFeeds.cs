using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class NewsFeeds : Pagination
    {
        /// <summary>
        /// Array of objects (NewsFeed) 
        /// </summary>
        /// <value>Array of objects (NewsFeed) </value>
        [JsonRequired]

        [DataMember(Name = "items")]
        public List<NewsFeed> Items { get; set; }


    }
}
