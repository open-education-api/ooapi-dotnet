using AutoFixture;
using NSubstitute;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Services;

[TestFixture]
public class BuildingsServiceTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public async Task Get_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IBuildingsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new BuildingsService(dbContext, repository, userRequestContext);
        var buildingId = _fixture.Create<Guid>();

        var expected = new Building();
        repository.GetBuildingAsync(buildingId).Returns(expected);

        // Act
        var result = await sut.GetAsync(buildingId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        await repository.Received(1).GetBuildingAsync(buildingId);
    }


    [Test]
    public async Task GetAll_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IBuildingsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new BuildingsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();

        var expected = Substitute.For<Pagination<Building>>();
        repository.GetAllOrderedByAsync(dataRequestParameters).Returns(expected);

        // Act
        var result = await sut.GetAllAsync(dataRequestParameters);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        await repository.Received(1).GetAllOrderedByAsync(dataRequestParameters);
    }
}