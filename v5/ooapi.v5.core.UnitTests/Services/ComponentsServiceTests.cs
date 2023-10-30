using AutoFixture;
using NSubstitute;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Services;

[TestFixture]
public class ComponentsServiceTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public async Task Get_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IComponentsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new ComponentsService(dbContext, repository, userRequestContext);
        var componentId = _fixture.Create<Guid>();

        var expected = new Component();
        repository.GetComponentAsync(componentId).Returns(expected);

        // Act
        var result = await sut.GetAsync(componentId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        await repository.Received(1).GetComponentAsync(componentId);
    }


    [Test]
    public async Task GetComponentsByCourseId_CallsRepositoryAndReturnsPagination()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IComponentsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new ComponentsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var courseId = _fixture.Create<Guid>();

        var expected = Substitute.For<Pagination<Component>>();
        repository.GetComponentsByCourseIdAsync(courseId, dataRequestParameters).Returns(expected);

        // Act
        var result = await sut.GetComponentsByCourseIdAsync(dataRequestParameters, courseId);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Component>>());
        await repository.Received(1).GetComponentsByCourseIdAsync(courseId, dataRequestParameters);
    }

    [Test]
    public async Task GetComponentsByOrganizationId_CallsRepositoryAndReturnsPagination()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IComponentsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new ComponentsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var organizationId = _fixture.Create<Guid>();

        var expected = Substitute.For<Pagination<Component>>();
        repository.GetComponentsByOrganizationIdAsync(organizationId, dataRequestParameters).Returns(expected);

        // Act
        var result = await sut.GetComponentsByOrganizationIdAsync(dataRequestParameters, organizationId);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Component>>());
        await repository.Received(1).GetComponentsByOrganizationIdAsync(organizationId, dataRequestParameters);
    }
}