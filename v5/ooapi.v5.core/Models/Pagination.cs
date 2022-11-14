using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ooapi.v5.core.Utility;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Pagination<T> : ModelBase
    {
        public Pagination()
        {

        }

        public Pagination(IQueryable<T> collection, DataRequestParameters dataRequestParameters = null)
        {
            TotalItems = collection.Count();
            PageSize = dataRequestParameters.PageSize;
            PageNumber = dataRequestParameters.PageNumber;

            if (dataRequestParameters != null)
            {
                dataRequestParameters.Validate();
                if (!string.IsNullOrEmpty(dataRequestParameters.Filter))
                {
                    collection = new FilterToLinq<T>(dataRequestParameters.Filter).Parse(collection);
                }
                int skip = dataRequestParameters.Skip;
                SetItems(collection.Skip(skip).Take(PageSize).ToList());
            }
            else
            {
                SetItems(collection.ToList());
            }

            SetExtendedAttributes();

        }

        private void SetItems(List<T> list)
        {
            CurrentPageSize = list.Count();
            //CurrentPage = (TotalItems > 0) ? PageNumber : 0;

            int remainder = (TotalItems % PageSize) == 0 ? 0 : 1;
            TotalPages = (int)(Math.Floor((double)(TotalItems / PageSize)) + remainder);

            HasPreviousPage = PageNumber > 1;
            HasNextPage = PageNumber < TotalPages;

            _items = list;
        }


        //public int CurrentPage { get;set; }
        //{
        //    get
        //    {
        //        return _currentPage;
        //    }
        //    set
        //    {
        //        if (value > TotalPages)
        //        {
        //            throw new Exception($"{nameof(CurrentPage)} ({value}) is > {nameof(TotalPages)} ({TotalPages})");
        //        }
        //        _currentPage = value;
        //    }
        //}

        /// <summary>
        /// The number of items per page
        /// </summary>
        /// <value>The number of items per page</value>
        [JsonRequired]

        [JsonProperty(PropertyName = "pageSize")]
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// The current page number
        /// </summary>
        /// <value>The current page number</value>
        [JsonRequired]

        [JsonProperty(PropertyName = "pageNumber")]
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Whether there is a previous page
        /// </summary>
        /// <value>Whether there is a previous page</value>
        [JsonRequired]

        [JsonProperty(PropertyName = "hasPreviousPage")]
        public bool HasPreviousPage { get; set; }
        //public bool HasPreviousPage => CurrentPage > 1;

        /// <summary>
        /// Whether there is a previous page
        /// </summary>
        /// <value>Whether there is a previous page</value>
        [JsonRequired]

        [JsonProperty(PropertyName = "hasNextPage")]
        public bool HasNextPage { get; set; }
        //public bool HasNextPage => CurrentPage < TotalPages;

        /// <summary>
        /// Total number of pages
        /// </summary>
        /// <value>Total number of pages</value>

        [JsonProperty(PropertyName = "totalPages")]
        public int TotalPages { get; set; }
        //{
        //    get
        //    {
        //        return _totalPages;
        //    }
        //    set
        //    {
        //        int remainder = (_totalItems % PageSize) == 0 ? 0 : 1;
        //        _totalPages = (int)(Math.Floor((double)(_totalItems / PageSize)) + remainder);
        //        //return (int)(Math.Floor((double)(_totalItems / PageSize)) + remainder);
        //    }
        //}
        ////public int TotalPages { get; set; } = 0;

        //private int _totalPages { get; set; } = 0;

        [JsonIgnore]
        public int TotalItems { get; set; } = 0;

        [JsonIgnore]
        public int CurrentPageSize { get; set; } = 0;

        [JsonIgnore]
        protected List<T>? _items { get; set; }

        //private void SetPaginationMetadata(int totalItems, int? pageSize, int pageNumber)
        //{
        //    _totalItems = totalItems;
        //    PageSize = pageSize ?? totalItems;
        //    PageNumber = (totalItems > 0) ? pageNumber : 0;
        //}

        [JsonProperty(PropertyName = "items")]
        public virtual List<T> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
            }
        }

        public void SetExtendedAttributes()
        {
            JObject extendedObject = new JObject(
                new JProperty("totalItems", TotalItems),
                new JProperty("currentPageSize", CurrentPageSize)

                );
            Extension = JsonConvert.SerializeObject(extendedObject);
        }

        protected void AddItems(List<T> items, IEnumerable<T> paginationItems)
        {
            if (items == null)
                items = new List<T>();
            items.AddRange(paginationItems);
        }

    }
}
