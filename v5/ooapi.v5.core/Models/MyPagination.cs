using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ooapi.v5.core.Utility;

namespace ooapi.v5.Models
{
    /// <summary>
    /// pagination
    /// </summary>
    public class MyPagination<T> : ModelBase
    {
        public MyPagination()
        {

        }

        public MyPagination(IQueryable<T> collection, DataRequestParameters dataRequestParameters = null)
        {
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
                var totalItems = collection.Count();
                SetPaginationMetadata(totalItems, totalItems == 0 ? 1 : totalItems, 1);
                SetItems(collection.ToList());
                SetExtendedAttributes();
            }
        }

        private void SetItems(List<T> list)
        {
            _currentPageSize = list.Count();
            Items = list;
        }

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
        public bool HasPreviousPage { get; set; } = false;

        /// <summary>
        /// Whether there is a previous page
        /// </summary>
        /// <value>Whether there is a previous page</value>
        [JsonRequired]

        [JsonProperty(PropertyName = "hasNextPage")]
        public bool HasNextPage { get; set; } = false;

        /// <summary>
        /// Total number of pages
        /// </summary>
        /// <value>Total number of pages</value>

        [JsonProperty(PropertyName = "totalPages")]
        public int TotalPages { get; set; } = 0;

        private int _totalItems { get; set; } = 0;

        private int _currentPageSize { get; set; } = 0;

        /// <summary>
        /// This method fetches data and returns a list of <typeparamref name="T"/>.
        /// </summary>
        /// <remarks>
        /// This example shows how to specify the <see cref="List{T}"/>
        /// type as a cref attribute.
        /// In generic classes and methods, you'll often want to reference the
        /// generic type, or the type parameter.
        /// The parameter and return value are both of an arbitrary type,
        /// <typeparamref name="T"/>
        /// </remarks>
        [JsonRequired]
        [JsonProperty(PropertyName = "items", NullValueHandling = NullValueHandling.Ignore)]
        public List<T>? Items { get; set; }

        private void SetPaginationMetadata(int totalItems, int? pageSize, int pageNumber)
        {
            _totalItems = totalItems;
            PageSize = pageSize ?? totalItems;
            PageNumber = (totalItems > 0) ? pageNumber : 0;
        }

        public void SetExtendedAttributes()
        {
            JObject extendedObject = new JObject(
                new JProperty("totalItems", _totalItems),
                new JProperty("currentPageSize", _currentPageSize)

                );
            Extension = JsonConvert.SerializeObject(extendedObject);
        }
    }
}
