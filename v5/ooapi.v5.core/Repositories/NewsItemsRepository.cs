using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

/// <summary>
/// 
/// </summary>
public class NewsItemsRepository : BaseRepository<NewsItem>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbContext"></param>
    public NewsItemsRepository(CoreDBContext dbContext) : base(dbContext)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="newsitemId"></param>
    /// <returns></returns>
    public NewsItem? GetNewsItem(Guid newsitemId)
    {
        return dbContext.NewsItems.FirstOrDefault(x => x.NewsItemId.Equals(newsitemId));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="newsfeedId"></param>
    /// <returns></returns>
    public List<NewsItem> GetNewsItemsByNewsFeedId(Guid newsfeedId)
    {
        return new List<NewsItem>();
        // TODO 
    }
}
