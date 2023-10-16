using AutoFixture;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.UnitTests.Repositories.Helpers;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class NewsItemsRepositoryTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
     public void GetNewsItem_ReturnsNewsItem_WhenNewsItemExists()
    {
        // Arrange
        var newsItemId = _fixture.Create<Guid>();
        var newsItem = _fixture.Build<NewsItem>()
            .With(x => x.NewsItemId, newsItemId)
            .Without(x => x.NewsFeeds)
            .Without(x => x.OneOfNewsFeeds)
            .Create();
        var newsItems = new List<NewsItem> { newsItem }.AsQueryable();

        var db = Substitute.For<DbSet<NewsItem>, IQueryable<NewsItem>>();
        DbMockHelper.InitDb(db, newsItems);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.NewsItems.Returns(db);
        var newsItemsRepository = new NewsItemsRepository(dbContext);

        // Act
        var result = newsItemsRepository.GetNewsItem(newsItemId);

        // Assert
        Assert.That(result, Is.EqualTo(newsItem));
    }

    [Test]
    public void GetNewsItem_ReturnsNull_WhenNewsItemDoesNotFound()
    {
        // Arrange
        var newsItemId = _fixture.Create<Guid>();
        var newsItem = _fixture.Build<NewsItem>()
            .With(x => x.NewsItemId, newsItemId)
            .Without(x => x.NewsFeeds)
            .Without(x => x.OneOfNewsFeeds)
            .Create();
        var newsItems = new List<NewsItem> { newsItem }.AsQueryable();

        var db = Substitute.For<DbSet<NewsItem>, IQueryable<NewsItem>>();
        DbMockHelper.InitDb(db, newsItems);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.NewsItems.Returns(db);
        var newsItemsRepository = new NewsItemsRepository(dbContext);

        // Act
        var result = newsItemsRepository.GetNewsItem(_fixture.Create<Guid>());

        // Assert
        Assert.That(result, Is.Null);
    }
}