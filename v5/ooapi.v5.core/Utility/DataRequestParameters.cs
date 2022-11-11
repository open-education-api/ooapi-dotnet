using ooapi.v5.Enums.Params;
using ooapi.v5.Models.Params;

namespace ooapi.v5.core.Utility
{
    public class DataRequestParameters
    {
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public string Filter { get; set; } = "";
        public Dictionary<string, object> Filters { get; set; } = new Dictionary<string, object>();
        public Dictionary<string, object> FiltersExt { get; set; } = new Dictionary<string, object>();
        public string Sort { get; set; }
        public string SearchTerm { get; set; }
        public string Expand { get; set; }
        public int Skip => (PageNumber - 1) * PageSize;

        public void Validate()
        {
            if (PageSize <= 0)
            {
                //PageNumber = 1;
                PageSize = 10;
            }

            if (PageNumber <= 0)
            {
                PageNumber = 1;
            }
        }

        public DataRequestParameters(PrimaryCodeParam? primaryCodeParam = null, FilterParams? filterParams = null, PagingParams? curPagingParams = null, string sort = null)
        {
            if (primaryCodeParam != null && primaryCodeParam.primaryCode != null)
            {
                SearchTerm = $"primaryCode = {primaryCodeParam.primaryCode}";
            }
            else
            {
                Sort = sort;
                if (filterParams != null)
                {
                    SearchTerm = filterParams.q;
                }
                if (curPagingParams != null)
                {
                    PageNumber = curPagingParams.pageNumber;
                    SetPageSize(curPagingParams.pageSize);
                }
            }
        }

        public void SetPageSize(PageSizeEnum pageSize)
        {
            if (pageSize == 0)
                PageSize = (int)PageSizeEnum.Ten;

            else
                PageSize = (int)pageSize;
        }
    }
}
