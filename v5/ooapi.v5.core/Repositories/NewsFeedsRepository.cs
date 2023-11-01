using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class NewsFeedsRepository : BaseRepository<NewsFeed>, INewsFeedsRepository
{
    public NewsFeedsRepository(ICoreDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<NewsFeed?> GetNewsFeedAsync(Guid newsfeedId, CancellationToken cancellationToken = default)
    {
        return await dbContext.NewsFeeds.FirstOrDefaultAsync(x => x.NewsFeedId.Equals(newsfeedId), cancellationToken);
    }
}