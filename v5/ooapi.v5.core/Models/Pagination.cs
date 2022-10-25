using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
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
        [Required]

        [DataMember(Name = "pageSize")]
        public int? PageSize { get; set; }

        /// <summary>
        /// The current page number
        /// </summary>
        /// <value>The current page number</value>
        [Required]

        [DataMember(Name = "pageNumber")]
        public int? PageNumber { get; set; }

        /// <summary>
        /// Whether there is a previous page
        /// </summary>
        /// <value>Whether there is a previous page</value>
        [Required]

        [DataMember(Name = "hasPreviousPage")]
        public bool? HasPreviousPage { get; set; }

        /// <summary>
        /// Whether there is a previous page
        /// </summary>
        /// <value>Whether there is a previous page</value>
        [Required]

        [DataMember(Name = "hasNextPage")]
        public bool? HasNextPage { get; set; }

        /// <summary>
        /// Total number of pages
        /// </summary>
        /// <value>Total number of pages</value>

        [DataMember(Name = "totalPages")]
        public int? TotalPages { get; set; }


    }
}
