using AutoFixture;
using MockQueryable.NSubstitute;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class OrganizationsRepositoryTests
{
    private readonly Fixture _fixture = new Fixture();

    [Test]
    public async Task GetAllOrderedBy_WithDataRequestParameters_ReturnsSet()
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

        var db = organizations.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.OrganizationsNoTracking.Returns(db);
        var organizationsRepository = new OrganizationsRepository(dbContext);

        // Act
        var result = await organizationsRepository.GetAllOrderedByAsync(new DataRequestParameters());

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Organization>>());
        Assert.That(result.Items, Has.Count.EqualTo(3));
    }

    [Test]
    public async Task GetOrganization_WhenOrganizationExist_ReturnsOrganization()
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

        var db = organizations.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.OrganizationsNoTracking.Returns(db);
        var organizationsRepository = new OrganizationsRepository(dbContext);

        // Act
        var result = await organizationsRepository.GetOrganizationAsync(organizationId, new DataRequestParameters());

        // Assert
        Assert.That(result, Is.InstanceOf<Organization>());
        Assert.That(result.OrganizationId, Is.EqualTo(organizationId));
    }

    [Test]
    public async Task GetOrganization_WhenParentRequestedViaRequestParams_ReturnsOrganizationWithParent()
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

        var db = organizations.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.OrganizationsNoTracking.Returns(db);
        var organizationsRepository = new OrganizationsRepository(dbContext);

        var requestParams = new DataRequestParameters() { Expand = { "Parent" } };

        // Act
        var result = await organizationsRepository.GetOrganizationAsync(organizationId, requestParams);

        // Assert
        Assert.That(result, Is.InstanceOf<Organization>());
        Assert.Multiple(() =>
        {
            Assert.That(result.OrganizationId, Is.EqualTo(organizationId));
            Assert.That(result.Parent!.OrganizationId, Is.EqualTo(parentOrganizationId));
            Assert.That(result.Parent.ChildrenIds, Does.Contain(organizationId));
        });
    }

    [Test]
    public async Task GetOrganization_WhenChildrenRequestedViaRequestParams_ReturnsOrganizationWithChildren()
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

        var db = organizations.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.OrganizationsNoTracking.Returns(db);
        var organizationsRepository = new OrganizationsRepository(dbContext);

        var requestParams = new DataRequestParameters() { Expand = { "Children" } };

        // Act
        var result = await organizationsRepository.GetOrganizationAsync(organizationId, requestParams);

        // Assert
        Assert.That(result, Is.InstanceOf<Organization>());
        Assert.Multiple(() =>
        {
            Assert.That(result.OrganizationId, Is.EqualTo(organizationId));
            Assert.That(result.Children, Is.InstanceOf<List<Organization>>());
            Assert.That(result.ChildrenIds, Does.Contain(firstChildOrganizationId));
            Assert.That(result.Children, Does.Contain(firstChildOrganization));
            Assert.That(result.ChildrenIds, Does.Contain(secondChildOrganizationId));
            Assert.That(result.Children, Does.Contain(secondChildOrganization));
        });
    }
}