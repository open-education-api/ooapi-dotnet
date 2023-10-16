using AutoFixture;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.UnitTests.Repositories.Helpers;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class GroupsRepositoryTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
     public void GetGroup_ReturnsGroup_WhenGroupExists()
    {
        // Arrange
        var groupId = _fixture.Create<Guid>();
        var group = _fixture.Build<Group>()
            .With(x => x.GroupId, groupId)
            .Without(x => x.Organization)
            .Without(x => x.Persons)
            .Create();
        var groups = new List<Group> { group }.AsQueryable();

        var db = Substitute.For<DbSet<Group>, IQueryable<Group>>();
        DbMockHelper.InitDb(db, groups);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Groups.Returns(db);
        var groupsRepository = new GroupsRepository(dbContext);

        // Act
        var result = groupsRepository.GetGroup(groupId);

        // Assert
        Assert.That(result, Is.EqualTo(group));
    }

    [Test]
    public void GetGroup_ReturnsNull_WhenGroupDoesNotFound()
    {
        // Arrange
        var groupId = _fixture.Create<Guid>();
        var group = _fixture.Build<Group>()
            .With(x => x.GroupId, groupId)
            .Without(x => x.Organization)
            .Without(x => x.Persons)
            .Create();
        var groups = new List<Group> { group }.AsQueryable();

        var db = Substitute.For<DbSet<Group>, IQueryable<Group>>();
        DbMockHelper.InitDb(db, groups);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Groups.Returns(db);
        var groupsRepository = new GroupsRepository(dbContext);

        // Act
        var result = groupsRepository.GetGroup(_fixture.Create<Guid>());

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void GetGroupByOrganizationId_ReturnsGroups_WhenGroupsExist()
    {
        // Arrange
        var organizationId = _fixture.Create<Guid>();
        var group = _fixture.Build<Group>()
            .With(x => x.OrganizationId, organizationId)
            .Without(x => x.Organization)
            .Without(x => x.Persons)
            .Create();
        var groups = new List<Group> { group }.AsQueryable();

        var db = Substitute.For<DbSet<Group>, IQueryable<Group>>();
        DbMockHelper.InitDb(db, groups);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Groups.Returns(db);
        var groupsRepository = new GroupsRepository(dbContext);

        // Act
        var result = groupsRepository.GetGroupsByOrganizationId(organizationId);

        // Assert
        Assert.That(result, Is.InstanceOf<List<Group>>());
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result[0].OrganizationId, Is.EqualTo(organizationId));
    }

    [Test]
    public void GetGroupByOrganizationId_ReturnsEmptyList_WhenGroupsNotFound()
    {
        // Arrange
        var organizationId = _fixture.Create<Guid>();
        var group = _fixture.Build<Group>()
            .With(x => x.OrganizationId, organizationId)
            .Without(x => x.Organization)
            .Without(x => x.Persons)
            .Create();
        var groups = new List<Group> { group }.AsQueryable();

        var db = Substitute.For<DbSet<Group>, IQueryable<Group>>();
        DbMockHelper.InitDb(db, groups);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Groups.Returns(db);
        var groupsRepository = new GroupsRepository(dbContext);

        // Act
        var result = groupsRepository.GetGroupsByOrganizationId(_fixture.Create<Guid>());

        // Assert
        Assert.That(result, Is.InstanceOf<List<Group>>());
        Assert.That(result.Count, Is.EqualTo(0));
    }
}