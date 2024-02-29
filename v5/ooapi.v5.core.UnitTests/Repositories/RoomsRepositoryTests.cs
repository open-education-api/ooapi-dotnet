using AutoFixture;
using MockQueryable.NSubstitute;
using NSubstitute;
using NUnit.Framework.Legacy;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class RoomsRepositoryTests
{
    private readonly Fixture _fixture = new Fixture();

    [Test]
    public async Task GetRoom_WhenRoomExists_ReturnsRoom()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var roomsRepository = new RoomsRepository(dbContext);
        var roomId = _fixture.Create<Guid>();
        var room = _fixture.Build<Room>()
            .With(x => x.RoomId, roomId)
            .Without(x => x.name)
            .Without(x => x.description)
            .Without(x => x.Building)
            .CreateMany(1)
            .AsQueryable();

        var db = room.BuildMockDbSet();
        dbContext.Rooms.Returns(db);

        // Act
        var result = await roomsRepository.GetRoomAsync(roomId);

        // Assert
        Assert.That(room.First(), Is.EqualTo(result));
    }

    [Test]
    public async Task GetRoom_WhenRoomDoesNotExist_ReturnsNull()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var roomsRepository = new RoomsRepository(dbContext);
        var roomId = _fixture.Create<Guid>();
        var rooms = _fixture.Build<Room>()
            .With(x => x.RoomId, roomId)
            .Without(x => x.name)
            .Without(x => x.description)
            .Without(x => x.Building)
            .CreateMany(5)
            .AsQueryable();

        var db = rooms.BuildMockDbSet();
        dbContext.Rooms.Returns(db);

        // Act
        var result = await roomsRepository.GetRoomAsync(_fixture.Create<Guid>());

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetRoomsByPersonId_WhenRoomsExist_ReturnsRooms()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var roomsRepository = new RoomsRepository(dbContext);
        var buildingId = _fixture.Create<Guid>();
        var rooms = _fixture.Build<Room>()
            .With(x => x.BuildingId, buildingId)
            .Without(x => x.name)
            .Without(x => x.description)
            .Without(x => x.Building)
            .CreateMany(5)
            .AsQueryable();

        var db = rooms.BuildMockDbSet();
        dbContext.Rooms.Returns(db);

        var dataRequestParameters = new DataRequestParameters();

        // Act
        var result = await roomsRepository.GetRoomsByBuildingIdAsync(buildingId, dataRequestParameters);

        // Assert
        Assert.That(result.Items, Is.EqualTo(rooms).AsCollection);
    }

    [Test]
    public async Task GetRoomsByPersonId_WhenRoomsDoNotExist_ReturnsEmptyList()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var roomsRepository = new RoomsRepository(dbContext);
        var buildingId = _fixture.Create<Guid>();
        var rooms = _fixture.Build<Room>()
            .With(x => x.BuildingId, buildingId)
            .Without(x => x.name)
            .Without(x => x.description)
            .Without(x => x.Building)
            .CreateMany(5)
            .AsQueryable();

        var db = rooms.BuildMockDbSet();
        dbContext.Rooms.Returns(db);

        var dataRequestParameters = new DataRequestParameters();

        // Act
        var result = await roomsRepository.GetRoomsByBuildingIdAsync(_fixture.Create<Guid>(), dataRequestParameters);

        // Assert
        Assert.That(result.Items, Is.Empty);
    }
}