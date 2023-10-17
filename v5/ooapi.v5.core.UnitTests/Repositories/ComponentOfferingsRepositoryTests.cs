using AutoFixture;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.UnitTests.Repositories.Helpers;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class ComponentOfferingsRepositoryTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
     public void GetComponentOffering_ReturnsComponentOffering_WhenComponentOfferingExists()
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
    public void GetComponentOffering_ReturnsNull_WhenComponentOfferingDoesNotExist()
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
    public void GetComponentOfferingByProgramId_ReturnsComponentOfferings_WhenComponentOfferingsExist()
    {
        // Arrange
        var componentId = _fixture.Create<Guid>();
        var componentOffering = _fixture.Build<ComponentOffering>()
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
        var result = componentOfferingsRepository.GetComponentOfferingByProgramId(componentId, new DataRequestParameters());

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<ComponentOffering>>());
        Assert.That(result.Items.Count, Is.EqualTo(1));
        Assert.That(result.Items[0].Component!.ComponentId, Is.EqualTo(componentId));
    }

    [Test]
    public void GetComponentOfferingByProgramId_ReturnsEmptyList_WhenComponentOfferingsDoNotExist()
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
        var result = componentOfferingsRepository.GetComponentOfferingByProgramId(componentId, new DataRequestParameters());

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<ComponentOffering>>());
        Assert.That(result.Items.Count, Is.EqualTo(0));
    }
}