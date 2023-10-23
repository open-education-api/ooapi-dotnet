using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace ooapi.v5.Models;

[ExcludeFromCodeCoverage(Justification = "Get/Set")]
public class NewsFeeds : Pagination<NewsFeed>
{
    /// <summary>
    /// Array of objects (NewsFeed) 
    /// </summary>
    /// <value>Array of objects (NewsFeed) </value>
    [JsonRequired]
    [JsonProperty(PropertyName = "items")]
    public override List<NewsFeed> Items
    {
        get
        {
            return _items;
        }
    }
}