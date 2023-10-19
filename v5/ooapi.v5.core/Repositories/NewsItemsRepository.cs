using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;


public class NewsItemsRepository : BaseRepository<NewsItem>
{
    public NewsItemsRepository(ICoreDbContext dbContext) : base(dbContext)
    {
    }


    /// <param name="newsitemId"></param>
    /// <returns></returns>
    public NewsItem? GetNewsItem(Guid newsitemId)
    {
        return dbContext.NewsItems.FirstOrDefault(x => x.NewsItemId.Equals(newsitemId));
    }


    /// <param name="newsfeedId"></param>
    /// <returns></returns>
    public List<NewsItem> GetNewsItemsByNewsFeedId(Guid newsfeedId)
    {
        throw new NotImplementedException();
    }
}