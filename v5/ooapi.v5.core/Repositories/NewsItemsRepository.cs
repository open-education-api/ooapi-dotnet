using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class NewsItemsRepository : BaseRepository<NewsItem>, INewsItemsRepository
{
    public NewsItemsRepository(ICoreDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<NewsItem?> GetNewsItemAsync(Guid newsitemId, CancellationToken cancellationToken = default)
    {
        return await dbContext.NewsItems.FirstOrDefaultAsync(x => x.NewsItemId.Equals(newsitemId),cancellationToken);
    }

    public async Task<List<NewsItem>> GetNewsItemsByNewsFeedIdAsync(Guid newsfeedId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}