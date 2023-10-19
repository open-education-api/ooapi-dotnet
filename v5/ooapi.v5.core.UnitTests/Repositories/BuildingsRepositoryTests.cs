using AutoFixture;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.UnitTests.Repositories.Helpers;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;
using Attribute = ooapi.v5.Models.Attribute;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class BuildingsRepositoryTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void GetBuilding_WhenBuildingExists_ReturnsBuilding()
    {
        // Arrange
        var buildingId = _fixture.Create<Guid>();
        var building = _fixture.Build<Building>()
            .With(x => x.BuildingId, buildingId)
            .Without(x => x.Address)
            .Create();

        var db = Substitute.For<DbSet<Building>, IQueryable<Building>>();
        DbMockHelper.InitDb(db, new List<Building> { building }.AsQueryable());
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Buildings.Returns(db);
        var buildingsRepository = new BuildingsRepository(dbContext);

        // Act
        var result = buildingsRepository.GetBuilding(buildingId);

        // Assert
        Assert.That(building, Is.EqualTo(result));
    }

    [Test]
    public void GetBuilding_WhenBuildingDoesNotExist_ReturnsNull()
    {
        // Arrange
        var buildingId = _fixture.Create<Guid>();
        var buildings = new List<Building> { }.AsQueryable();

        var db = Substitute.For<DbSet<Building>, IQueryable<Building>>();
        DbMockHelper.InitDb(db, buildings);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Buildings.Returns(db);
        var buildingsRepository = new BuildingsRepository(dbContext);

        // Act
        var result = buildingsRepository.GetBuilding(buildingId);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void GetAllOrderedBy_WithDataRequestParameters_ReturnsSet()
    {
        // Arrange
        var buildings = new List<Building>()
        {
            _fixture.Build<Building>()
                .With(x => x.Address, new Address())
                .With(x => x.Attributes, _fixture.Build<Attribute>().CreateMany(3).ToList())
                .Create(),
            _fixture.Build<Building>()
                .With(x => x.Address, new Address())
                .With(x => x.Attributes, _fixture.Build<Attribute>().CreateMany(3).ToList())
                .Create(),
            _fixture.Build<Building>()
                .With(x => x.Address, new Address())
                .With(x => x.Attributes, _fixture.Build<Attribute>().CreateMany(3).ToList())
                .Create()
        }.AsQueryable();
        var db = Substitute.For<DbSet<Building>, IQueryable<Building>>();
        DbMockHelper.InitDb(db, buildings);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Set<Building>().Returns(db);
        var repository = new BuildingsRepository(dbContext);

        // Act
        var result = repository.GetAllOrderedBy(new DataRequestParameters());

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Building>>());
        Assert.That(result.Items, Has.Count.EqualTo(3));
    }
}