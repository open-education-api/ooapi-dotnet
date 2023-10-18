using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;


public class NewsFeedsRepository : BaseRepository<NewsFeed>
{

    /// <param name="dbContext"></param>
    public NewsFeedsRepository(CoreDBContext dbContext) : base(dbContext)
    {
    }


    /// <param name="newsfeedId"></param>
    /// <returns></returns>
    public NewsFeed? GetNewsFeed(Guid newsfeedId)
    {
        return dbContext.NewsFeeds.FirstOrDefault(x => x.NewsFeedId.Equals(newsfeedId));
    }
}
