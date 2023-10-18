using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ooapi.v5.core.Utility;

namespace ooapi.v5.Models;

/// <summary>
/// 
/// </summary>
public class Pagination<T> : ModelBase
{
    /// <summary>
    /// 
    /// </summary>
    public Pagination()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="dataRequestParameters"></param>
    public Pagination(IQueryable<T> collection, DataRequestParameters dataRequestParameters)
    {
        TotalItems = collection.Count();
        PageSize = dataRequestParameters.PageSize;
        PageNumber = dataRequestParameters.PageNumber;

        dataRequestParameters.Validate();
        if (!string.IsNullOrEmpty(dataRequestParameters.Filter))
        {
            collection = new FilterToLinq<T>(dataRequestParameters.Filter).Parse(collection);
        }
        var skip = dataRequestParameters.Skip;
        SetItems(collection.Skip(skip).Take(PageSize).ToList());

        SetExtendedAttributes();
    }

    private void SetItems(List<T> list)
    {
        CurrentPageSize = list.Count;

        var remainder = (TotalItems % PageSize) == 0 ? 0 : 1;
        TotalPages = (int)(Math.Floor((double)(TotalItems / PageSize)) + remainder);

        HasPreviousPage = PageNumber > 1;
        HasNextPage = PageNumber < TotalPages;

        _items = list;
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
    public bool HasPreviousPage { get; set; }

    /// <summary>
    /// Whether there is a previous page
    /// </summary>
    /// <value>Whether there is a previous page</value>
    [JsonRequired]
    [JsonProperty(PropertyName = "hasNextPage")]
    public bool HasNextPage { get; set; }

    /// <summary>
    /// Total number of pages
    /// </summary>
    /// <value>Total number of pages</value>
    [JsonProperty(PropertyName = "totalPages")]
    public int TotalPages { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public int TotalItems { get; set; } = 0;

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public int CurrentPageSize { get; set; } = 0;

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    protected List<T> _items { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
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

    /// <summary>
    /// 
    /// </summary>
    public void SetExtendedAttributes()
    {
        var extendedObject = new JObject(
            new JProperty("totalItems", TotalItems),
            new JProperty("currentPageSize", CurrentPageSize)
            );
        Extension = JsonConvert.SerializeObject(extendedObject);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="items"></param>
    /// <param name="paginationItems"></param>
    protected void AddItems(List<T> items, IEnumerable<T> paginationItems)
    {
        items ??= new List<T>();
        items.AddRange(paginationItems);
    }
}
