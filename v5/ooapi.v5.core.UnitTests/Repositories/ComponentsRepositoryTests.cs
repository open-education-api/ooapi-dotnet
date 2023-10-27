using AutoFixture;
using MockQueryable.NSubstitute;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class ComponentsRepositoryTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public async Task GetComponent_WhenComponentExists_ReturnsComponent()
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
        var components = new List<Component> { component }.AsQueryable();

        var db = components.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Components.Returns(db);
        var componentsRepository = new ComponentsRepository(dbContext);

        // Act
        var result = await componentsRepository.GetComponentAsync(componentId);

        // Assert
        Assert.That(result, Is.EqualTo(component));
    }

    [Test]
    public async Task GetComponent_WhenComponentNotFound_ReturnsNull()
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
        var components = new List<Component> { component }.AsQueryable();

        var db = components.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Components.Returns(db);
        var componentsRepository = new ComponentsRepository(dbContext);

        // Act
        var result = await componentsRepository.GetComponentAsync(_fixture.Create<Guid>());

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetComponentsByCourseId_WhenComponentWithCourseIdExists_ReturnsComponent()
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
        var components = new List<Component> { component }.AsQueryable();

        var db = components.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Components.Returns(db);
        var componentsRepository = new ComponentsRepository(dbContext);

        var dataRequestParameters = new DataRequestParameters();

        // Act
        var result = await componentsRepository.GetComponentsByCourseIdAsync(courseId, dataRequestParameters);

        // Assert
        Assert.That(result.Items, Does.Contain(component));
    }

    [Test]
    public async Task GetComponentsByCourseId_WhenComponentWithCourseNotFound_ReturnsEmptyList()
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
        var components = new List<Component> { component }.AsQueryable();

        var db = components.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Components.Returns(db);
        var componentsRepository = new ComponentsRepository(dbContext);

        var dataRequestParameters = new DataRequestParameters();

        // Act
        var result = await componentsRepository.GetComponentsByCourseIdAsync(_fixture.Create<Guid>(), dataRequestParameters);

        // Assert
        Assert.That(result.Items, Does.Not.Contain(component));
    }

    [Test]
    public async Task GetComponentsByOrganizationId_WhenComponentWithOrganizationIdExists_ReturnsComponent()
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
        var components = new List<Component> { component }.AsQueryable();

        var db = components.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Components.Returns(db);
        var componentsRepository = new ComponentsRepository(dbContext);

        var dataRequestParameters = new DataRequestParameters();

        // Act
        var result = await componentsRepository.GetComponentsByOrganizationIdAsync(organizationId, dataRequestParameters);

        // Assert
        Assert.That(result.Items, Does.Contain(component));
    }

    [Test]
    public async Task GetComponentsByOrganizationId_WhenComponentWithOrganizationIdNotfound_ReturnsEmptyList()
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
        var components = new List<Component> { component }.AsQueryable();

        var db = components.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Components.Returns(db);
        var componentsRepository = new ComponentsRepository(dbContext);

        var dataRequestParameters = new DataRequestParameters();

        // Act
        var result = await componentsRepository.GetComponentsByOrganizationIdAsync(_fixture.Create<Guid>(), dataRequestParameters);

        // Assert
        Assert.That(result.Items, Does.Not.Contain(component));
    }
}