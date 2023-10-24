using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

internal class NewsItemsService : ServiceBase, INewsItemsService
{
    private readonly INewsItemsRepository _repository;

    public NewsItemsService(ICoreDbContext dbContext, INewsItemsRepository repository, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = repository;
    }

    public NewsItem? Get(Guid newsitemId)
    {
            return _repository.GetNewsItem(newsitemId);
    }

    public Pagination<NewsItem> GetNewsItemsByNewsFeedId(DataRequestParameters dataRequestParameters, Guid newsfeedId)
    {
        var result = _repository.GetNewsItemsByNewsFeedId(newsfeedId);
        return new Pagination<NewsItem>(result.AsQueryable(), dataRequestParameters);
    }
}