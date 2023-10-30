using AutoFixture;
using NSubstitute;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Services;

[TestFixture]
public class NewsFeedsServiceTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public async Task GetAll_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<INewsFeedsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new NewsFeedsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();

        var expected = Substitute.For<Pagination<NewsFeed>>();
        repository.GetAllOrderedByAsync(dataRequestParameters).Returns(expected);

        // Act
        var result = await sut.GetAllAsync(dataRequestParameters);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        await repository.Received(1).GetAllOrderedByAsync(dataRequestParameters);
    }

    [Test]
    public async Task Get_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<INewsFeedsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new NewsFeedsService(dbContext, repository, userRequestContext);
        var newsFeedId = _fixture.Create<Guid>();

        var expected = new NewsFeed();
        repository.GetNewsFeedAsync(newsFeedId).Returns(expected);

        // Act
        var result = await sut.GetAsync(newsFeedId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        await repository.Received(1).GetNewsFeedAsync(newsFeedId);
    }
}