using AutoFixture;
using NSubstitute;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Services;

[TestFixture]
public class NewsItemsServiceTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void Get_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<INewsItemsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new NewsItemsService(dbContext, repository, userRequestContext);
        var newsItemId = _fixture.Create<Guid>();

        var expected = new NewsItem();
        repository.GetNewsItem(newsItemId).Returns(expected);

        // Act
        var result = sut.Get(newsItemId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        repository.Received(1).GetNewsItem(newsItemId);
    }

    [Test]
    public void GetNewsItemsByNewsFeedId_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<INewsItemsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new NewsItemsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var newsFeedId = _fixture.Create<Guid>();

        var newsItems = new List<NewsItem>();
        repository.GetNewsItemsByNewsFeedId(newsFeedId).Returns(newsItems);

        // Act
        var result = sut.GetNewsItemsByNewsFeedId(dataRequestParameters, newsFeedId);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<NewsItem>>());
        repository.Received(1).GetNewsItemsByNewsFeedId(newsFeedId);
    }
}