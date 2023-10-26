using AutoFixture;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.UnitTests.Repositories.Helpers;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class ComponentsRepositoryTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void GetComponent_WhenComponentExists_ReturnsComponent()
    {
        // Arrange
        var componentId = _fixture.Create<Guid>();
        var component = _fixture.Build<Component>()
            .With(x => x.ComponentId, componentId)
            .Without(x => x.Duration)
            .Without(x => x.LearningOutcomes)
            .Without(x => x.Addresses)
            .Without(x => x.Address)
            .Without(x => x.Course)
            .Without(x => x.Organization)
            .Create();
        var componentOfferings = new List<Component> { component }.AsQueryable();

        var db = Substitute.For<DbSet<Component>, IQueryable<Component>>();
        DbMockHelper.InitDb(db, componentOfferings);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Components.Returns(db);
        var componentsRepository = new ComponentsRepository(dbContext);

        // Act
        var result = componentsRepository.GetComponentAsync(componentId);

        // Assert
        Assert.That(result, Is.EqualTo(component));
    }

    [Test]
    public void GetComponent_WhenComponentNotFound_ReturnsNull()
    {
        // Arrange
        var componentId = _fixture.Create<Guid>();
        var component = _fixture.Build<Component>()
            .With(x => x.ComponentId, componentId)
            .Without(x => x.Duration)
            .Without(x => x.LearningOutcomes)
            .Without(x => x.Addresses)
            .Without(x => x.Address)
            .Without(x => x.Course)
            .Without(x => x.Organization)
            .Create();
        var componentOfferings = new List<Component> { component }.AsQueryable();

        var db = Substitute.For<DbSet<Component>, IQueryable<Component>>();
        DbMockHelper.InitDb(db, componentOfferings);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Components.Returns(db);
        var componentsRepository = new ComponentsRepository(dbContext);

        // Act
        var result = componentsRepository.GetComponentAsync(_fixture.Create<Guid>());

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void GetComponentsByCourseId_WhenComponentWithCourseIdExists_ReturnsComponent()
    {
        // Arrange
        var courseId = _fixture.Create<Guid>();
        var component = _fixture.Build<Component>()
            .With(x => x.CourseId, courseId)
            .Without(x => x.Duration)
            .Without(x => x.LearningOutcomes)
            .Without(x => x.Addresses)
            .Without(x => x.Address)
            .Without(x => x.Course)
            .Without(x => x.Organization)
            .Create();
        var componentOfferings = new List<Component> { component }.AsQueryable();

        var db = Substitute.For<DbSet<Component>, IQueryable<Component>>();
        DbMockHelper.InitDb(db, componentOfferings);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Components.Returns(db);
        var componentsRepository = new ComponentsRepository(dbContext);

        // Act
        var result = componentsRepository.GetComponentsByCourseIdAsync(courseId);

        // Assert
        Assert.That(result, Does.Contain(component));
    }

    [Test]
    public void GetComponentsByCourseId_WhenComponentWithCourseNotFound_ReturnsEmptyList()
    {
        // Arrange
        var courseId = _fixture.Create<Guid>();
        var component = _fixture.Build<Component>()
            .With(x => x.CourseId, courseId)
            .Without(x => x.Duration)
            .Without(x => x.LearningOutcomes)
            .Without(x => x.Addresses)
            .Without(x => x.Address)
            .Without(x => x.Course)
            .Without(x => x.Organization)
            .Create();
        var componentOfferings = new List<Component> { component }.AsQueryable();

        var db = Substitute.For<DbSet<Component>, IQueryable<Component>>();
        DbMockHelper.InitDb(db, componentOfferings);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Components.Returns(db);
        var componentsRepository = new ComponentsRepository(dbContext);

        // Act
        var result = componentsRepository.GetComponentsByCourseIdAsync(_fixture.Create<Guid>());

        // Assert
        Assert.That(result, Does.Not.Contain(component));
    }

    [Test]
    public void GetComponentsByOrganizationId_WhenComponentWithOrganizationIdExists_ReturnsComponent()
    {
        // Arrange
        var organizationId = _fixture.Create<Guid>();
        var component = _fixture.Build<Component>()
            .With(x => x.OrganizationId, organizationId)
            .Without(x => x.Duration)
            .Without(x => x.LearningOutcomes)
            .Without(x => x.Addresses)
            .Without(x => x.Address)
            .Without(x => x.Course)
            .Without(x => x.Organization)
            .Create();
        var componentOfferings = new List<Component> { component }.AsQueryable();

        var db = Substitute.For<DbSet<Component>, IQueryable<Component>>();
        DbMockHelper.InitDb(db, componentOfferings);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Components.Returns(db);
        var componentsRepository = new ComponentsRepository(dbContext);

        // Act
        var result = componentsRepository.GetComponentsByOrganizationIdAsync(organizationId);

        // Assert
        Assert.That(result, Does.Contain(component));
    }

    [Test]
    public void GetComponentsByOrganizationId_WhenComponentWithOrganizationIdNotfound_ReturnsEmptyList()
    {
        // Arrange
        var organizationId = _fixture.Create<Guid>();
        var component = _fixture.Build<Component>()
            .With(x => x.OrganizationId, organizationId)
            .Without(x => x.Duration)
            .Without(x => x.LearningOutcomes)
            .Without(x => x.Addresses)
            .Without(x => x.Address)
            .Without(x => x.Course)
            .Without(x => x.Organization)
            .Create();
        var componentOfferings = new List<Component> { component }.AsQueryable();

        var db = Substitute.For<DbSet<Component>, IQueryable<Component>>();
        DbMockHelper.InitDb(db, componentOfferings);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Components.Returns(db);
        var componentsRepository = new ComponentsRepository(dbContext);

        // Act
        var result = componentsRepository.GetComponentsByOrganizationIdAsync(_fixture.Create<Guid>());

        // Assert
        Assert.That(result, Does.Not.Contain(component));
    }
}