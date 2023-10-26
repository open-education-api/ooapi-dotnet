using AutoFixture;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
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
        var result = groupsRepository.GetGroupAsync(groupId);

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
        var result = groupsRepository.GetGroupAsync(_fixture.Create<Guid>());

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void GetGroupByOrganizationId_WhenGroupsExist_ReturnsGroups()
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
        var result = groupsRepository.GetGroupsByOrganizationIdAsync(organizationId);

        // Assert
        Assert.That(result, Is.InstanceOf<List<Group>>());
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].OrganizationId, Is.EqualTo(organizationId));
    }

    [Test]
    public void GetGroupByOrganizationId_WhenGroupsNotFound_ReturnsEmptyList()
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
        var result = groupsRepository.GetGroupsByOrganizationIdAsync(_fixture.Create<Guid>());

        // Assert
        Assert.That(result, Is.InstanceOf<List<Group>>());
        Assert.That(result, Is.Empty);
    }
}