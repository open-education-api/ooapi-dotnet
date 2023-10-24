using AutoFixture;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.UnitTests.Repositories.Helpers;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class ComponentOfferingsRepositoryTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
     public void GetComponentOffering_WhenComponentOfferingExists_ReturnsComponentOffering()
    {
        // Arrange
        var courseOfferingId = _fixture.Create<Guid>();
        var componentOffering = _fixture.Build<ComponentOffering>()
            .With(x => x.OfferingId, courseOfferingId)
            .Without(x => x.Room)
            .Without(x => x.Component)
            .Without(x => x.Course)
            .Without(x => x.AcademicSession)
            .Without(x => x.Organization)
            .Without(x => x.Address)
            .Without(x => x.Costs)
            .Create();
        var componentOfferings = new List<ComponentOffering> { componentOffering }.AsQueryable();

        var db = Substitute.For<DbSet<ComponentOffering>, IQueryable<ComponentOffering>>();
        DbMockHelper.InitDb(db, componentOfferings);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.ComponentOfferings.Returns(db);
        var componentOfferingsRepository = new ComponentOfferingsRepository(dbContext);

        // Act
        var result = componentOfferingsRepository.GetComponentOffering(courseOfferingId);

        // Assert
        Assert.That(result, Is.EqualTo(componentOffering));
    }

    [Test]
    public void GetComponentOffering_WhenComponentOfferingDoesNotExist_ReturnsNull()
    {
        // Arrange
        var courseOfferingId = _fixture.Create<Guid>();
        var componentOfferings = new List<ComponentOffering> { }.AsQueryable();

        var db = Substitute.For<DbSet<ComponentOffering>, IQueryable<ComponentOffering>>();
        DbMockHelper.InitDb(db, componentOfferings);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.ComponentOfferings.Returns(db);
        var componentOfferingsRepository = new ComponentOfferingsRepository(dbContext);

        // Act
        var result = componentOfferingsRepository.GetComponentOffering(courseOfferingId);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void GetComponentOfferingByComponentId_WhenComponentOfferingsExist_ReturnsComponentOfferings()
    {
        // Arrange
        var componentId = _fixture.Create<Guid>();
        var componentOffering = _fixture.Build<ComponentOffering>()
            .With(x => x.ComponentId, componentId)
            .With(x => x.Component, new Component() { ComponentId = componentId })
            .Without(x => x.Room)
            .Without(x => x.Course)
            .Without(x => x.AcademicSession)
            .Without(x => x.Organization)
            .Without(x => x.Address)
            .Without(x => x.Costs)
            .Create();
        var componentOfferings = new List<ComponentOffering> { componentOffering }.AsQueryable();

        var db = Substitute.For<DbSet<ComponentOffering>, IQueryable<ComponentOffering>>();
        DbMockHelper.InitDb(db, componentOfferings);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.ComponentOfferingsNoTracking.Returns(db);
        var componentOfferingsRepository = new ComponentOfferingsRepository(dbContext);

        // Act
        var result = componentOfferingsRepository.GetComponentOfferingByComponentIdAsync(componentId, new DataRequestParameters());

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<ComponentOffering>>());
        Assert.That(result.Items, Has.Count.EqualTo(1));
        Assert.That(result.Items[0].Component!.ComponentId, Is.EqualTo(componentId));
    }

    [Test]
    public void GetComponentOfferingByComponentId_WhenComponentOfferingsDoNotExist_ReturnsEmptyList()
    {
        // Arrange
        var componentId = _fixture.Create<Guid>();
        var componentOfferings = new List<ComponentOffering> { }.AsQueryable();

        var db = Substitute.For<DbSet<ComponentOffering>, IQueryable<ComponentOffering>>();
        DbMockHelper.InitDb(db, componentOfferings);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.ComponentOfferingsNoTracking.Returns(db);
        var componentOfferingsRepository = new ComponentOfferingsRepository(dbContext);

        // Act
        var result = componentOfferingsRepository.GetComponentOfferingByComponentIdAsync(componentId, new DataRequestParameters());

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<ComponentOffering>>());
        Assert.That(result.Items, Has.Count.EqualTo(0));
    }
}