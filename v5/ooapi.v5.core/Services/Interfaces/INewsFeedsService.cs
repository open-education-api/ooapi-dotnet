using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

public interface INewsFeedsService
{
    Task<NewsFeed?> GetAsync(Guid newsfeedId, CancellationToken cancellationToken = default);
    Task<Pagination<NewsFeed>> GetAllAsync(DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
}