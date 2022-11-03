using ooapi.v5.Enums.Params;

namespace ooapi.v5.core.Utility
{
    public class DataRequestParameters
    {
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public string Filter { get; set; } = "";
        public Dictionary<string, object> Filters { get; set; }
        public Dictionary<string, object> FiltersExt { get; set; }
        public string Sort { get; set; }
        public string SearchTerm { get; set; }
        public string Expand { get; set; }
        public int Skip => (PageNumber - 1) * PageSize;

        public void Validate()
        {
            if (PageSize <= 0)
            {
                PageNumber = 1;
                PageSize = 10;
            }

            if (PageNumber <= 0)
            {
                PageNumber = 1;
            }
        }

        public DataRequestParameters()
        {
            Filters = new Dictionary<string, object>();
            FiltersExt = new Dictionary<string, object>();
        }

        public void SetPageSize(PageSizeEnum pageSize)
        {
            PageSize = (int)pageSize;
        }
    }
}
