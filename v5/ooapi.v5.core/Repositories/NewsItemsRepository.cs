using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class NewsItemsRepository : BaseRepository<NewsItem>
{
    public NewsItemsRepository(CoreDBContext dbContext) : base(dbContext)
    {
    }

    public NewsItem? GetNewsItem(Guid newsitemId)
    {
        return dbContext.NewsItems.FirstOrDefault(x => x.NewsItemId.Equals(newsitemId));
    }

    public List<NewsItem> GetNewsItemsByNewsFeedId(Guid newsfeedId)
    {
        return new List<NewsItem>();
        // TODO 
    }
}
