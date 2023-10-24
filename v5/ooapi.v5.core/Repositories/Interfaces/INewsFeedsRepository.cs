using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface INewsFeedsRepository
{
    NewsFeed? GetNewsFeed(Guid newsfeedId);
    Task<Pagination<NewsFeed>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, IQueryable<NewsFeed>? set = null, CancellationToken cancellationToken = default);
}