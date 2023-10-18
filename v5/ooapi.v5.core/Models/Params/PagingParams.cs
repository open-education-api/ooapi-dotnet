using Newtonsoft.Json;
using ooapi.v5.Enums.Params;
using System.ComponentModel;

namespace ooapi.v5.Models.Params;


public class PagingParams
{
    /// <summary>
    /// The number of items per page <br/>
    /// Default: 10
    /// </summary>
    [DefaultValue(PageSize._10)]
    [JsonProperty("pageSize")]
    public PageSize PageSize { get; set; }

    /// <summary>
    /// The page number to get. Page numbers start at 1. <br/>
    /// Example: pageNumber=1
    /// </summary>
    [DefaultValue(1)]
    [JsonProperty("pageNumber")]
    public int PageNumber { get; set; } = 1;
}
