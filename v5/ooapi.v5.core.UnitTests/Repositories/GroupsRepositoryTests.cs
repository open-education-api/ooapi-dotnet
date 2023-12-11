using AutoFixture;
using MockQueryable.NSubstitute;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class GroupsRepositoryTests
{
    private readonly Fixture _fixture = new Fixture();

    [Test]
     public async Task GetGroup_ReturnsGroup_WhenGroupExists()
    {
        // Arrange
        var groupId = _fixture.Create<Guid>();
        var group = _fixture.Build<Group>()
            .With(x => x.GroupId, groupId)
            .Without(x => x.Organization)
            .Without(x => x.Persons)
            .Create();
        var groups = new List<Group> { group }.AsQueryable();

        var db = groups.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Groups.Returns(db);
        var groupsRepository = new GroupsRepository(dbContext);

        // Act
        var result = await groupsRepository.GetGroupAsync(groupId);

        // Assert
        Assert.That(result, Is.EqualTo(group));
    }

    [Test]
    public async Task GetGroup_ReturnsNull_WhenGroupDoesNotFound()
    {
        // Arrange
        var groupId = _fixture.Create<Guid>();
        var group = _fixture.Build<Group>()
            .With(x => x.GroupId, groupId)
            .Without(x => x.Organization)
            .Without(x => x.Persons)
            .Create();
        var groups = new List<Group> { group }.AsQueryable();

        var db = groups.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Groups.Returns(db);
        var groupsRepository = new GroupsRepository(dbContext);

        // Act
        var result = await groupsRepository.GetGroupAsync(_fixture.Create<Guid>());

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetGroupByOrganizationId_WhenGroupsExist_ReturnsGroups()
    {
        // Arrange
        var organizationId = _fixture.Create<Guid>();
        var group = _fixture.Build<Group>()
            .With(x => x.OrganizationId, organizationId)
            .Without(x => x.Organization)
            .Without(x => x.Persons)
            .Create();
        var groups = new List<Group> { group }.AsQueryable();

        var db = groups.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Groups.Returns(db);
        var groupsRepository = new GroupsRepository(dbContext);

        var dataRequestParameters = new DataRequestParameters();

        // Act
        var result = await groupsRepository.GetGroupsByOrganizationIdAsync(organizationId, dataRequestParameters);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Group>>());
        Assert.That(result.Items, Has.Count.EqualTo(1));
        Assert.That(result.Items[0].OrganizationId, Is.EqualTo(organizationId));
    }

    [Test]
    public async Task GetGroupByOrganizationId_WhenGroupsNotFound_ReturnsEmptyList()
    {
        // Arrange
        var organizationId = _fixture.Create<Guid>();
        var group = _fixture.Build<Group>()
            .With(x => x.OrganizationId, organizationId)
            .Without(x => x.Organization)
            .Without(x => x.Persons)
            .Create();
        var groups = new List<Group> { group }.AsQueryable();

        var db = groups.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Groups.Returns(db);
        var groupsRepository = new GroupsRepository(dbContext);

        var dataRequestParameters = new DataRequestParameters();

        // Act
        var result = await groupsRepository.GetGroupsByOrganizationIdAsync(_fixture.Create<Guid>(), dataRequestParameters);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Group>>());
        Assert.That(result.Items, Is.Empty);
    }
}