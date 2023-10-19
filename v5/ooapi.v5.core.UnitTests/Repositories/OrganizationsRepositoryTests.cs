using AutoFixture;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.UnitTests.Repositories.Helpers;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class OrganizationsRepositoryTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void GetAllOrderedBy_WithDataRequestParameters_ReturnsSet()
    {
        // Arrange
        var organizations = new List<Organization>()
        {
            _fixture.Build<Organization>()
                .Without(x => x.Addresses)
                .Without(x => x.Address)
                .Without(x => x.Parent)
                .Without(x => x.Children)
                .Create(),
            _fixture.Build<Organization>()
                .Without(x => x.Addresses)
                .Without(x => x.Address)
                .Without(x => x.Parent)
                .Without(x => x.Children)
                .Create(),
            _fixture.Build<Organization>()
                .Without(x => x.Addresses)
                .Without(x => x.Address)
                .Without(x => x.Parent)
                .Without(x => x.Children)
                .Create()
        }.AsQueryable();
        var db = Substitute.For<DbSet<Organization>, IQueryable<Organization>>();
        DbMockHelper.InitDb(db, organizations);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.OrganizationsNoTracking.Returns(db);
        var organizationsRepository = new OrganizationsRepository(dbContext);

        // Act
        var result = organizationsRepository.GetAllOrderedBy(new DataRequestParameters());

        // Assert
        Assert.IsInstanceOf<Pagination<Organization>>(result);
        Assert.That(result.Items.Count, Is.EqualTo(3));
    }

    [Test]
    public void GetOrganization_WhenOrganizationExist_ReturnsOrganization()
    {
        // Arrange
        var organizationId = _fixture.Create<Guid>();
        var organization = _fixture.Build<Organization>()
            .With(x => x.OrganizationId, organizationId)
            .Without(x => x.Addresses)
            .Without(x => x.Address)
            .Without(x => x.Parent)
            .Without(x => x.Children)
            .Create();
        var organizations = new List<Organization> { organization }.AsQueryable();

        var db = Substitute.For<DbSet<Organization>, IQueryable<Organization>>();
        DbMockHelper.InitDb(db, organizations);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.OrganizationsNoTracking.Returns(db);
        var organizationsRepository = new OrganizationsRepository(dbContext);

        // Act
        var result = organizationsRepository.GetOrganization(organizationId, new DataRequestParameters());

        // Assert
        Assert.That(result, Is.InstanceOf<Organization>());
        Assert.That(result.OrganizationId, Is.EqualTo(organizationId));
    }

    [Test]
    public void GetOrganization_WhenParentRequestedViaRequestParams_ReturnsOrganizationWithParent()
    {
        // Arrange
        var organizationId = _fixture.Create<Guid>();
        var parentOrganizationId = _fixture.Create<Guid>();
        var organization = _fixture.Build<Organization>()
            .With(x => x.OrganizationId, organizationId)
            .With(x => x.ParentId, parentOrganizationId)
            .Without(x => x.Addresses)
            .Without(x => x.Address)
            .Without(x => x.Parent)
            .Without(x => x.Children)
            .Create();
        var parentOrganization = _fixture.Build<Organization>()
            .With(x => x.OrganizationId, parentOrganizationId)
            .Without(x => x.Addresses)
            .Without(x => x.Address)
            .Without(x => x.Parent)
            .Without(x => x.Children)
            .Create();
        var organizations = new List<Organization> { organization, parentOrganization }.AsQueryable();

        var db = Substitute.For<DbSet<Organization>, IQueryable<Organization>>();
        DbMockHelper.InitDb(db, organizations);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.OrganizationsNoTracking.Returns(db);
        var organizationsRepository = new OrganizationsRepository(dbContext);

        var requestParams = new DataRequestParameters() { Expand = { "Parent" } };

        // Act
        var result = organizationsRepository.GetOrganization(organizationId, requestParams);

        // Assert
        Assert.That(result, Is.InstanceOf<Organization>());
        Assert.That(result.OrganizationId, Is.EqualTo(organizationId));
        Assert.That(result.Parent.OrganizationId, Is.EqualTo(parentOrganizationId));
        Assert.That(result.Parent.ChildrenIds.Contains(organizationId), Is.True);
    }

    [Test]
    public void GetOrganization_WhenChildrenRequestedViaRequestParams_ReturnsOrganizationWithChildren()
    {
        // Arrange
        var organizationId = _fixture.Create<Guid>();
        var firstChildOrganizationId = _fixture.Create<Guid>();
        var secondChildOrganizationId = _fixture.Create<Guid>();
        var organization = _fixture.Build<Organization>()
            .With(x => x.OrganizationId, organizationId)
            .Without(x => x.Addresses)
            .Without(x => x.Address)
            .Without(x => x.Parent)
            .Without(x => x.Children)
            .Create();
        var firstChildOrganization = _fixture.Build<Organization>()
            .With(x => x.OrganizationId, firstChildOrganizationId)
            .With(x => x.ParentId, organizationId)
            .Without(x => x.Addresses)
            .Without(x => x.Address)
            .Without(x => x.Parent)
            .Without(x => x.Children)
            .Create();
        var secondChildOrganization = _fixture.Build<Organization>()
            .With(x => x.OrganizationId, secondChildOrganizationId)
            .With(x => x.ParentId, organizationId)
            .Without(x => x.Addresses)
            .Without(x => x.Address)
            .Without(x => x.Parent)
            .Without(x => x.Children)
            .Create();
        var organizations = new List<Organization> { organization, firstChildOrganization, secondChildOrganization }.AsQueryable();

        var db = Substitute.For<DbSet<Organization>, IQueryable<Organization>>();
        DbMockHelper.InitDb(db, organizations);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.OrganizationsNoTracking.Returns(db);
        var organizationsRepository = new OrganizationsRepository(dbContext);

        var requestParams = new DataRequestParameters() { Expand = { "Children" } };

        // Act
        var result = organizationsRepository.GetOrganization(organizationId, requestParams);

        // Assert
        Assert.That(result, Is.InstanceOf<Organization>());
        Assert.That(result.OrganizationId, Is.EqualTo(organizationId));
        Assert.That(result.Children, Is.InstanceOf<List<Organization>>());
        Assert.That(result.ChildrenIds.Contains(firstChildOrganizationId), Is.True);
        Assert.That(result.Children.Contains(firstChildOrganization), Is.True);
        Assert.That(result.ChildrenIds.Contains(secondChildOrganizationId), Is.True);
        Assert.That(result.Children.Contains(secondChildOrganization), Is.True);
    }
}