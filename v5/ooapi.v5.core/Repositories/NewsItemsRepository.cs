using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class NewsItemsRepository : BaseRepository<NewsItem>, INewsItemsRepository
{
    public NewsItemsRepository(ICoreDbContext dbContext) : base(dbContext)
    {
    }

    public NewsItem? GetNewsItem(Guid newsitemId)
    {
        return dbContext.NewsItems.FirstOrDefault(x => x.NewsItemId.Equals(newsitemId));
    }

    public List<NewsItem> GetNewsItemsByNewsFeedId(Guid newsfeedId)
    {
        throw new NotImplementedException();
    }
}