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
    public void Get_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IBuildingsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new BuildingsService(dbContext, repository, userRequestContext);
        var buildingId = _fixture.Create<Guid>();

        var expected = new Building();
        repository.GetBuilding(buildingId).Returns(expected);

        // Act
        var result = sut.Get(buildingId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        repository.Received(1).GetBuilding(buildingId);
    }


    [Test]
    public void GetAll_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IBuildingsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new BuildingsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();

        var expected = new Pagination<Building>();
        repository.GetAllOrderedBy(dataRequestParameters).Returns(expected);

        // Act
        var result = sut.GetAll(dataRequestParameters);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        repository.Received(1).GetAllOrderedBy(dataRequestParameters);
    }
}