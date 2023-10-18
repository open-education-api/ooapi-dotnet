using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;


public interface INewsFeedsService
{

    /// <param name="newsfeedId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    NewsFeed? Get(Guid newsfeedId, out ErrorResponse? errorResponse);


    /// <param name="dataRequestParameters"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Pagination<NewsFeed>? GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse? errorResponse);
}