using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces
{
    public interface INewsFeedsService
    {
        NewsFeed Get(Guid newsfeedId, out ErrorResponse errorResponse);
        Pagination<NewsFeed> GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse errorResponse);
    }
}