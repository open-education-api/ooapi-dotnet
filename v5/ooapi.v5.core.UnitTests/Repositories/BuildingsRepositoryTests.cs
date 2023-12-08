using AutoFixture;
using MockQueryable.NSubstitute;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;
using Attribute = ooapi.v5.Models.Attribute;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class BuildingsRepositoryTests
{
    private readonly Fixture _fixture = new Fixture();

    [Test]
    public async Task GetBuilding_WhenBuildingExists_ReturnsBuilding()
    {
        // Arrange
        var buildingId = _fixture.Create<Guid>();
        var building = _fixture.Build<Building>()
            .With(x => x.BuildingId, buildingId)
            .Without(x => x.Address)
            .CreateMany(1)
            .AsQueryable();

        var db = building.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Buildings.Returns(db);
        var buildingsRepository = new BuildingsRepository(dbContext);

        // Act
        var result = await buildingsRepository.GetBuildingAsync(buildingId);

        // Assert
        Assert.That(building.First(), Is.EqualTo(result));
    }

    [Test]
    public async Task GetBuilding_WhenBuildingDoesNotExist_ReturnsNull()
    {
        // Arrange
        var buildingId = _fixture.Create<Guid>();
        var buildings = new List<Building> { }.AsQueryable();

        var db = buildings.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Buildings.Returns(db);
        var buildingsRepository = new BuildingsRepository(dbContext);

        // Act
        var result = await buildingsRepository.GetBuildingAsync(buildingId);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetAllOrderedBy_WithDataRequestParameters_ReturnsSet()
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

        var db = buildings.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Set<Building>().Returns(db);
        var repository = new BuildingsRepository(dbContext);

        // Act
        var result = await repository.GetAllOrderedByAsync(new DataRequestParameters());

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Building>>());
        Assert.That(result.Items, Has.Count.EqualTo(3));
    }
}