using Newtonsoft.Json;

namespace ooapi.v5.Models;

public class NewsItems : Pagination<NewsItem>
{
    /// <summary>
    /// Array of objects (NewsItem) 
    /// </summary>
    /// <value>Array of objects (NewsItem) </value>
    [JsonRequired]
    [JsonProperty(PropertyName = "items")]
    public override List<NewsItem> Items
    {
        get
        {
            return _items;
        }
    }
}
