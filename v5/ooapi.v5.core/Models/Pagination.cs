using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class Pagination : ModelBase
    {
        /// <summary>
        /// The number of items per page
        /// </summary>
        /// <value>The number of items per page</value>
        [JsonRequired]

        [JsonProperty(PropertyName = "pageSize")]
        public int? PageSize { get; set; }

        /// <summary>
        /// The current page number
        /// </summary>
        /// <value>The current page number</value>
        [JsonRequired]

        [JsonProperty(PropertyName = "pageNumber")]
        public int? PageNumber { get; set; }

        /// <summary>
        /// Whether there is a previous page
        /// </summary>
        /// <value>Whether there is a previous page</value>
        [JsonRequired]

        [JsonProperty(PropertyName = "hasPreviousPage")]
        public bool? HasPreviousPage { get; set; }

        /// <summary>
        /// Whether there is a previous page
        /// </summary>
        /// <value>Whether there is a previous page</value>
        [JsonRequired]

        [JsonProperty(PropertyName = "hasNextPage")]
        public bool? HasNextPage { get; set; }

        /// <summary>
        /// Total number of pages
        /// </summary>
        /// <value>Total number of pages</value>

        [JsonProperty(PropertyName = "totalPages")]
        public int? TotalPages { get; set; }

        //[JsonRequired]
        //[JsonProperty(PropertyName = "myitems", NullValueHandling = NullValueHandling.Ignore)]
        //public List<T>? MyItems { get; set; }
    }
}
