using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class NewsFeedsRepository : BaseRepository<NewsFeed>, INewsFeedsRepository
{
    public NewsFeedsRepository(ICoreDbContext dbContext) : base(dbContext)
    {
    }

    public NewsFeed? GetNewsFeed(Guid newsfeedId)
    {
        return dbContext.NewsFeeds.FirstOrDefault(x => x.NewsFeedId.Equals(newsfeedId));
    }
}