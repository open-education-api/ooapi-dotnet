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
    public async Task Get_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<INewsItemsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new NewsItemsService(dbContext, repository, userRequestContext);
        var newsItemId = _fixture.Create<Guid>();

        var expected = new NewsItem();
        repository.GetNewsItemAsync(newsItemId).Returns(expected);

        // Act
        var result = await sut.GetAsync(newsItemId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        await repository.Received(1).GetNewsItemAsync(newsItemId);
    }

    [Test]
    public async Task GetNewsItemsByNewsFeedId_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<INewsItemsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new NewsItemsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var newsFeedId = _fixture.Create<Guid>();

        var expected = Substitute.For<Pagination<NewsItem>>();
        repository.GetNewsItemsByNewsFeedIdAsync(newsFeedId, dataRequestParameters).Returns(expected);

        // Act
        var result = await sut.GetNewsItemsByNewsFeedIdAsync(dataRequestParameters, newsFeedId);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<NewsItem>>());
        await repository.Received(1).GetNewsItemsByNewsFeedIdAsync(newsFeedId, dataRequestParameters);
    }
}