using AutoFixture;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.UnitTests.Repositories.Helpers;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class NewsFeedsRepositoryTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
     public void GetNewsFeed_WhenNewsFeedExists_ReturnsNewsFeed()
    {
        // Arrange
        var newsFeedId = _fixture.Create<Guid>();
        var newsFeed = _fixture.Build<NewsFeed>()
            .With(x => x.NewsFeedId, newsFeedId)
            .Without(x => x.NewsItems)
            .Create();
        var newsFeeds = new List<NewsFeed> { newsFeed }.AsQueryable();

        var db = Substitute.For<DbSet<NewsFeed>, IQueryable<NewsFeed>>();
        DbMockHelper.InitDb(db, newsFeeds);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.NewsFeeds.Returns(db);
        var newsFeedsRepository = new NewsFeedsRepository(dbContext);

        // Act
        var result = newsFeedsRepository.GetNewsFeed(newsFeedId);

        // Assert
        Assert.That(result, Is.EqualTo(newsFeed));
    }

    [Test]
    public void GetNewsFeed_WhenNewsFeedDoesNotFound_ReturnsNull()
    {
        // Arrange
        var newsFeedId = _fixture.Create<Guid>();
        var newsFeed = _fixture.Build<NewsFeed>()
            .With(x => x.NewsFeedId, newsFeedId)
            .Without(x => x.NewsItems)
            .Create();
        var newsFeeds = new List<NewsFeed> { newsFeed }.AsQueryable();

        var db = Substitute.For<DbSet<NewsFeed>, IQueryable<NewsFeed>>();
        DbMockHelper.InitDb(db, newsFeeds);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.NewsFeeds.Returns(db);
        var newsFeedsRepository = new NewsFeedsRepository(dbContext);

        // Act
        var result = newsFeedsRepository.GetNewsFeed(_fixture.Create<Guid>());

        // Assert
        Assert.That(result, Is.Null);
    }
}