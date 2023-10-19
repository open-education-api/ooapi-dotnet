using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class NewsFeedsRepository : BaseRepository<NewsFeed>
{
    public NewsFeedsRepository(CoreDBContext dbContext) : base(dbContext)
    {
    }

    public NewsFeed? GetNewsFeed(Guid newsfeedId)
    {
        return dbContext.NewsFeeds.FirstOrDefault(x => x.NewsFeedId.Equals(newsfeedId));
    }
}
