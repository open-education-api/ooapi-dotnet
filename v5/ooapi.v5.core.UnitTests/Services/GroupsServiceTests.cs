using AutoFixture;
using NSubstitute;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Services;

[TestFixture]
public class GroupsServiceTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void GetAll_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IGroupsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new GroupsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();

        var expected = new Pagination<Group>();
        repository.GetAllOrderedBy(dataRequestParameters).Returns(expected);

        // Act
        var result = sut.GetAll(dataRequestParameters);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        repository.Received(1).GetAllOrderedBy(dataRequestParameters);
    }

    [Test]
    public void Get_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IGroupsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new GroupsService(dbContext, repository, userRequestContext);
        var groupId = _fixture.Create<Guid>();

        var expected = new Group();
        repository.GetGroup(groupId).Returns(expected);

        // Act
        var result = sut.Get(groupId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        repository.Received(1).GetGroup(groupId);
    }

    [Test]
    public void GetGroupsByOrganizationId_CallsRepositoryAndReturnsPagination()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IGroupsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new GroupsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var organizationId = _fixture.Create<Guid>();

        var groups = new List<Group>();
        repository.GetGroupsByOrganizationId(organizationId).Returns(groups);

        // Act
        var result = sut.GetGroupsByOrganizationId(dataRequestParameters, organizationId);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Group>>());
        repository.Received(1).GetGroupsByOrganizationId(organizationId);
    }

    [Test]
    public void GetGroupsByPersonId_CallsRepositoryAndReturnsPagination()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IGroupsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new GroupsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var groupId = _fixture.Create<Guid>();

        var groups = new List<Group>();
        repository.GetGroupsByPersonId(groupId).Returns(groups);

        // Act
        var result = sut.GetGroupsByPersonId(dataRequestParameters, groupId);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Group>>());
        repository.Received(1).GetGroupsByPersonId(groupId);
    }
}