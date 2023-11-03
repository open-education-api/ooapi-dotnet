using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

public interface INewsItemsService
{
    Task<NewsItem?> GetAsync(Guid newsitemId, CancellationToken cancellationToken = default);
    Task<Pagination<NewsItem>> GetNewsItemsByNewsFeedIdAsync(DataRequestParameters dataRequestParameters, Guid newsfeedId, CancellationToken cancellationToken = default);
}