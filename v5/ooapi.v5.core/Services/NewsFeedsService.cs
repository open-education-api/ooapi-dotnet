using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

internal class NewsFeedsService : ServiceBase, INewsFeedsService
{
    private readonly INewsFeedsRepository _repository;

    public NewsFeedsService(ICoreDbContext dbContext, INewsFeedsRepository repository, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = repository;
    }

    public async Task<Pagination<NewsFeed>> GetAll(DataRequestParameters dataRequestParameters)
    {
        return await _repository.GetAllOrderedByAsync(dataRequestParameters);
    }

    public NewsFeed? Get(Guid newsfeedId)
    {
        return _repository.GetNewsFeed(newsfeedId);
    }
}