using AutoFixture;
using NSubstitute;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Services;

[TestFixture]
public class RoomsServiceTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void GetAll_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IRoomsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new RoomsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();

        var expected = new Pagination<Room>();
        repository.GetAllOrderedByAsync(dataRequestParameters).Returns(expected);

        // Act
        var result = sut.GetAllAsync(dataRequestParameters);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        repository.Received(1).GetAllOrderedByAsync(dataRequestParameters);
    }

    [Test]
    public void Get_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IRoomsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new RoomsService(dbContext, repository, userRequestContext);
        var roomId = _fixture.Create<Guid>();

        var expected = new Room();
        repository.GetRoomAsync(roomId).Returns(expected);

        // Act
        var result = sut.GetAsync(roomId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        repository.Received(1).GetRoomAsync(roomId);
    }

    [Test]
    public void GetRoomsByBuildingId_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IRoomsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new RoomsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var buildingId = _fixture.Create<Guid>();

        var rooms = new List<Room>();
        repository.GetRoomsByBuildingIdAsync(buildingId).Returns(rooms);

        // Act
        var result = sut.GetRoomsByBuildingIdAsync(dataRequestParameters, buildingId);

        // Assert
        Assert.That(result, Is.TypeOf<Pagination<Room>>());
        repository.Received(1).GetRoomsByBuildingIdAsync(buildingId);
    }
}