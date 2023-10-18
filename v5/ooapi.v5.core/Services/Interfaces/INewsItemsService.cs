using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

/// <summary>
/// 
/// </summary>
public interface INewsItemsService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="newsitemId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    NewsItem? Get(Guid newsitemId, out ErrorResponse? errorResponse);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataRequestParameters"></param>
    /// <param name="newsfeedId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Pagination<NewsItem>? GetNewsItemsByNewsFeedId(DataRequestParameters dataRequestParameters, Guid newsfeedId, out ErrorResponse? errorResponse);
}