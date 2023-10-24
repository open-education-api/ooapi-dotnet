using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface INewsItemsRepository
{
    NewsItem? GetNewsItem(Guid newsitemId);
    List<NewsItem> GetNewsItemsByNewsFeedId(Guid newsfeedId);
    Task<Pagination<NewsItem>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, IQueryable<NewsItem>? set = null, CancellationToken cancellationToken = default);
}