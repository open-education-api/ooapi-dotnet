using AutoFixture;
using MockQueryable.NSubstitute;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class NewsItemsRepositoryTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
     public async Task GetNewsItem_WhenNewsItemExists_ReturnsNewsItem()
    {
        // Arrange
        var newsItemId = _fixture.Create<Guid>();
        var newsItem = _fixture.Build<NewsItem>()
            .With(x => x.NewsItemId, newsItemId)
            .Without(x => x.NewsFeeds)
            .Without(x => x.OneOfNewsFeeds)
            .Create();
        var newsItems = new List<NewsItem> { newsItem }.AsQueryable();

        var db = newsItems.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.NewsItems.Returns(db);
        var newsItemsRepository = new NewsItemsRepository(dbContext);

        // Act
        var result = await newsItemsRepository.GetNewsItemAsync(newsItemId);

        // Assert
        Assert.That(result, Is.EqualTo(newsItem));
    }

    [Test]
    public async Task GetNewsItem_WhenNewsItemDoesNotFound_ReturnsNull()
    {
        // Arrange
        var newsItemId = _fixture.Create<Guid>();
        var newsItem = _fixture.Build<NewsItem>()
            .With(x => x.NewsItemId, newsItemId)
            .Without(x => x.NewsFeeds)
            .Without(x => x.OneOfNewsFeeds)
            .Create();
        var newsItems = new List<NewsItem> { newsItem }.AsQueryable();

        var db = newsItems.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.NewsItems.Returns(db);
        var newsItemsRepository = new NewsItemsRepository(dbContext);

        // Act
        var result = await newsItemsRepository.GetNewsItemAsync(_fixture.Create<Guid>());

        // Assert
        Assert.That(result, Is.Null);
    }
}