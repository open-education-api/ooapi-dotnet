using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

public interface INewsItemsService
{
    NewsItem? Get(Guid newsitemId);
    Pagination<NewsItem> GetNewsItemsByNewsFeedId(DataRequestParameters dataRequestParameters, Guid newsfeedId);
}