using ooapi.v5.core.Models;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

/// <summary>
/// 
/// </summary>
public class NewsFeedsService : ServiceBase, INewsFeedsService
{
    private readonly NewsFeedsRepository _repository;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="userRequestContext"></param>
    public NewsFeedsService(CoreDBContext dbContext, UserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = new NewsFeedsRepository(dbContext);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataRequestParameters"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    public Pagination<NewsFeed>? GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse? errorResponse)
    {
        try
        {
            var result = _repository.GetAllOrderedBy(dataRequestParameters);
            errorResponse = null;
            return result;
        }
        catch (Exception)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="newsfeedId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    public NewsFeed? Get(Guid newsfeedId, out ErrorResponse? errorResponse)
    {
        try
        {
            var item = _repository.GetNewsFeed(newsfeedId);

            errorResponse = null;
            return item;
        }
        catch (Exception)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }
}
