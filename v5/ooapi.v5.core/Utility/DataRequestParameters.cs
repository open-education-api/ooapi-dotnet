﻿using ooapi.v5.Enums.Params;
using ooapi.v5.Models.Params;

namespace ooapi.v5.core.Utility;

public class DataRequestParameters
{
    public int PageSize { get; set; } = 10;

    public int PageNumber { get; set; } = 1;

    public string Filter { get; set; } = "";

    public Dictionary<string, object> Filters { get; set; } = new Dictionary<string, object>();

    public Dictionary<string, object> FiltersExt { get; set; } = new Dictionary<string, object>();

    public string? Sort { get; set; }

    public string? SearchTerm { get; set; }

    public string? Consumer { get; set; }

    public string? PrimaryCodeSearch { get; set; }

    public List<string> Expand { get; set; } = new List<string>();

    public int Skip => (PageNumber - 1) * PageSize;

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
    public DataRequestParameters() : this(null, null, null)
    {

    }

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

    public void SetPageSize(PageSize pageSize)
    {
        if (pageSize == 0)
            PageSize = (int)Enums.Params.PageSize._10;

        else
            PageSize = (int)pageSize;
    }
}
