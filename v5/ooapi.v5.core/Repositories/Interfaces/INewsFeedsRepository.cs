using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface INewsFeedsRepository
{
    NewsFeed? GetNewsFeed(Guid newsfeedId);
    Pagination<NewsFeed> GetAllOrderedBy(DataRequestParameters dataRequestParameters, IQueryable<NewsFeed>? set = null);
}