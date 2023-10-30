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
    public async Task GetAll_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IGroupsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new GroupsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();

        var expected = Substitute.For<Pagination<Group>>();
        repository.GetAllOrderedByAsync(dataRequestParameters).Returns(expected);

        // Act
        var result = await sut.GetAllAsync(dataRequestParameters);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        await repository.Received(1).GetAllOrderedByAsync(dataRequestParameters);
    }

    [Test]
    public async Task Get_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IGroupsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new GroupsService(dbContext, repository, userRequestContext);
        var groupId = _fixture.Create<Guid>();

        var expected = new Group();
        repository.GetGroupAsync(groupId).Returns(expected);

        // Act
        var result = await sut.GetAsync(groupId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        await repository.Received(1).GetGroupAsync(groupId);
    }

    [Test]
    public async Task GetGroupsByOrganizationId_CallsRepositoryAndReturnsPagination()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IGroupsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new GroupsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var organizationId = _fixture.Create<Guid>();

        var expected = Substitute.For<Pagination<Group>>();
        repository.GetGroupsByOrganizationIdAsync(organizationId, dataRequestParameters).Returns(expected);

        // Act
        var result = await sut.GetGroupsByOrganizationIdAsync(dataRequestParameters, organizationId);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Group>>());
        await repository.Received(1).GetGroupsByOrganizationIdAsync(organizationId, dataRequestParameters);
    }

    [Test]
    public async Task GetGroupsByPersonId_CallsRepositoryAndReturnsPagination()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IGroupsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new GroupsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var groupId = _fixture.Create<Guid>();

        var expected = Substitute.For<Pagination<Group>>();
        repository.GetGroupsByPersonIdAsync(groupId, dataRequestParameters).Returns(expected);

        // Act
        var result = await sut.GetGroupsByPersonIdAsync(dataRequestParameters, groupId);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Group>>());
        await repository.Received(1).GetGroupsByPersonIdAsync(groupId, dataRequestParameters);
    }
}