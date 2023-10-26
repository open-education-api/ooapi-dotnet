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

    public async Task<NewsItem?> GetAsync(Guid newsitemId, CancellationToken cancellationToken = default)
    {
            return await _repository.GetNewsItemAsync(newsitemId, cancellationToken);
    }

    public async Task<Pagination<NewsItem>> GetNewsItemsByNewsFeedIdAsync(DataRequestParameters dataRequestParameters, Guid newsfeedId, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetNewsItemsByNewsFeedIdAsync(newsfeedId, cancellationToken);

        var pagination = new Pagination<NewsItem>();
        await pagination.LoadData(result.AsQueryable(), dataRequestParameters);
        return pagination;
    }
}