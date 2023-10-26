using AutoFixture;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.UnitTests.Repositories.Helpers;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class RoomsRepositoryTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void GetRoom_WhenRoomExists_ReturnsRoom()
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
            .Create();
        var db = Substitute.For<DbSet<Room>, IQueryable<Room>>();
        DbMockHelper.InitDb(db, new List<Room> { room }.AsQueryable());
        dbContext.Rooms.Returns(db);

        // Act
        var result = roomsRepository.GetRoomAsync(roomId);

        // Assert
        Assert.That(room, Is.EqualTo(result));
    }

    [Test]
    public void GetRoom_WhenRoomDoesNotExist_ReturnsNull()
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
            .CreateMany(5);
        var db = Substitute.For<DbSet<Room>, IQueryable<Room>>();
        DbMockHelper.InitDb(db, rooms.AsQueryable());
        dbContext.Rooms.Returns(db);

        // Act
        var result = roomsRepository.GetRoomAsync(_fixture.Create<Guid>());

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void GetRoomsByPersonId_WhenRoomsExist_ReturnsRooms()
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
            .CreateMany(5);
        var db = Substitute.For<DbSet<Room>, IQueryable<Room>>();
        DbMockHelper.InitDb(db, rooms.AsQueryable());
        dbContext.Rooms.Returns(db);

        // Act
        var result = roomsRepository.GetRoomsByBuildingIdAsync(buildingId);

        // Assert
        CollectionAssert.AreEqual(rooms, result);
    }

    [Test]
    public void GetRoomsByPersonId_WhenRoomsDoNotExist_ReturnsEmptyList()
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
            .CreateMany(5);
        var db = Substitute.For<DbSet<Room>, IQueryable<Room>>();
        DbMockHelper.InitDb(db, rooms.AsQueryable());
        dbContext.Rooms.Returns(db);

        // Act
        var result = roomsRepository.GetRoomsByBuildingIdAsync(_fixture.Create<Guid>());

        // Assert
        Assert.That(result, Is.Empty);
    }
}