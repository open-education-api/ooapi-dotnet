using AutoFixture;
using MockQueryable.NSubstitute;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class NewsFeedsRepositoryTests
{
    private readonly Fixture _fixture = new Fixture();

    [Test]
     public async Task GetNewsFeed_WhenNewsFeedExists_ReturnsNewsFeed()
    {
        // Arrange
        var newsFeedId = _fixture.Create<Guid>();
        var newsFeed = _fixture.Build<NewsFeed>()
            .With(x => x.NewsFeedId, newsFeedId)
            .Without(x => x.NewsItems)
            .Create();
        var newsFeeds = new List<NewsFeed> { newsFeed }.AsQueryable();

        var db = newsFeeds.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.NewsFeeds.Returns(db);
        var newsFeedsRepository = new NewsFeedsRepository(dbContext);

        // Act
        var result = await newsFeedsRepository.GetNewsFeedAsync(newsFeedId);

        // Assert
        Assert.That(result, Is.EqualTo(newsFeed));
    }

    [Test]
    public async Task GetNewsFeed_WhenNewsFeedDoesNotFound_ReturnsNull()
    {
        // Arrange
        var newsFeedId = _fixture.Create<Guid>();
        var newsFeed = _fixture.Build<NewsFeed>()
            .With(x => x.NewsFeedId, newsFeedId)
            .Without(x => x.NewsItems)
            .Create();
        var newsFeeds = new List<NewsFeed> { newsFeed }.AsQueryable();

        var db = newsFeeds.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.NewsFeeds.Returns(db);
        var newsFeedsRepository = new NewsFeedsRepository(dbContext);

        // Act
        var result = await newsFeedsRepository.GetNewsFeedAsync(_fixture.Create<Guid>());

        // Assert
        Assert.That(result, Is.Null);
    }
}