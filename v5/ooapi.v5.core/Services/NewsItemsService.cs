using ooapi.v5.core.Repositories;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;


public class NewsItemsService : ServiceBase, INewsItemsService
{
    private readonly NewsItemsRepository _repository;

    public NewsItemsService(CoreDBContext dbContext, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = new NewsItemsRepository(dbContext);
    }


    /// <param name="newsitemId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    public NewsItem? Get(Guid newsitemId, out ErrorResponse? errorResponse)
    {
        try
        {
            var item = _repository.GetNewsItem(newsitemId);

            errorResponse = null;
            return item;
        }
        catch (Exception)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }


    /// <param name="dataRequestParameters"></param>
    /// <param name="newsfeedId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    public Pagination<NewsItem>? GetNewsItemsByNewsFeedId(DataRequestParameters dataRequestParameters, Guid newsfeedId, out ErrorResponse? errorResponse)
    {
        try
        {
            var result = _repository.GetNewsItemsByNewsFeedId(newsfeedId);
            var paginationResult = new Pagination<NewsItem>(result.AsQueryable(), dataRequestParameters);
            errorResponse = null;
            return paginationResult;
        }
        catch (Exception)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }
}
