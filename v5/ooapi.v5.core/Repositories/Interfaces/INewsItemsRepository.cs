using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface INewsItemsRepository
{
    Task<NewsItem?> GetNewsItemAsync(Guid newsitemId, CancellationToken cancellationToken = default);
    Task<Pagination<NewsItem>> GetNewsItemsByNewsFeedIdAsync(Guid newsfeedId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
    Task<Pagination<NewsItem>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, IQueryable<NewsItem>? set = null, CancellationToken cancellationToken = default);
}