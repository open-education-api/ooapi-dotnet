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
    public void Get_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IComponentsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new ComponentsService(dbContext, repository, userRequestContext);
        var componentId = _fixture.Create<Guid>();

        var expected = new Component();
        repository.GetComponent(componentId).Returns(expected);

        // Act
        var result = sut.Get(componentId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        repository.Received(1).GetComponent(componentId);
    }


    [Test]
    public void GetComponentsByCourseId_CallsRepositoryAndReturnsPagination()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IComponentsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new ComponentsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var courseId = _fixture.Create<Guid>();

        var components = new List<Component>();
        repository.GetComponentsByCourseId(courseId).Returns(components);

        // Act
        var result = sut.GetComponentsByCourseId(dataRequestParameters, courseId);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Component>>());
        repository.Received(1).GetComponentsByCourseId(courseId);
    }

    [Test]
    public void GetComponentsByOrganizationId_CallsRepositoryAndReturnsPagination()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IComponentsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new ComponentsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var organizationId = _fixture.Create<Guid>();

        var components = new List<Component>();
        repository.GetComponentsByOrganizationId(organizationId).Returns(components);

        // Act
        var result = sut.GetComponentsByOrganizationId(dataRequestParameters, organizationId);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Component>>());
        repository.Received(1).GetComponentsByOrganizationId(organizationId);
    }
}