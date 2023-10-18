using ooapi.v5.Enums.Params;
using ooapi.v5.Models.Params;

namespace ooapi.v5.core.Utility;

/// <summary>
/// 
/// </summary>
public class DataRequestParameters
{
    /// <summary>
    /// 
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// 
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// 
    /// </summary>
    public string Filter { get; set; } = "";

    /// <summary>
    /// 
    /// </summary>
    public Dictionary<string, object> Filters { get; set; } = new Dictionary<string, object>();

    /// <summary>
    /// 
    /// </summary>
    public Dictionary<string, object> FiltersExt { get; set; } = new Dictionary<string, object>();

    /// <summary>
    /// 
    /// </summary>
    public string? Sort { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? SearchTerm { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Consumer { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? PrimaryCodeSearch { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public List<string> Expand { get; set; } = new List<string>();

    /// <summary>
    /// 
    /// </summary>
    public int Skip => (PageNumber - 1) * PageSize;

    /// <summary>
    /// 
    /// </summary>
    public void Validate()
    {
        if (PageSize <= 0)
        {
            PageSize = 10;
        }

        if (PageNumber <= 0)
        {
            PageNumber = 1;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public DataRequestParameters() : this(null, null, null)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filterParams"></param>
    /// <param name="curPagingParams"></param>
    /// <param name="sort"></param>
    public DataRequestParameters(FilterParams? filterParams, PagingParams? curPagingParams, string? sort)
    {
        Sort = sort;
        if (filterParams != null)
        {
            SearchTerm = filterParams.q;
            Consumer = filterParams.consumer;
        }
        if (curPagingParams != null)
        {
            PageNumber = curPagingParams.PageNumber;
            SetPageSize(curPagingParams.PageSize);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="primaryCodeParam"></param>
    /// <param name="filterParams"></param>
    /// <param name="curPagingParams"></param>
    /// <param name="sort"></param>
    public DataRequestParameters(PrimaryCodeParam? primaryCodeParam, FilterParams? filterParams, PagingParams? curPagingParams, string? sort)
    {
        if (primaryCodeParam != null && primaryCodeParam.primaryCode != null)
        {
            PrimaryCodeSearch = primaryCodeParam.primaryCode;
        }
        else
        {
            Sort = sort;
            if (filterParams != null)
            {
                SearchTerm = filterParams.q;
                Consumer = filterParams.consumer;
            }
            if (curPagingParams != null)
            {
                PageNumber = curPagingParams.PageNumber;
                SetPageSize(curPagingParams.PageSize);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pageSize"></param>
    public void SetPageSize(PageSize pageSize)
    {
        if (pageSize == 0)
            PageSize = (int)Enums.Params.PageSize._10;

        else
            PageSize = (int)pageSize;
    }
}
