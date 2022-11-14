using Newtonsoft.Json;
using ooapi.v5.Enums.Params;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ooapi.v5.Models.Params
{
    public class PagingParams
    {

        /// <summary>
        /// The number of items per page <br/>
        /// Default: 10
        /// </summary>
        [DefaultValue(PageSizeEnum.Ten)]
        [JsonProperty("pageSize")]
        public PageSizeEnum PageSize { get; set; }

        /// <summary>
        /// The page number to get. Page numbers start at 1. <br/>
        /// Example: pageNumber=1
        /// </summary>
        [DefaultValue(1)]
        [JsonProperty("pageNumber")]
        public int PageNumber { get; set; } = 1;


    }
}
