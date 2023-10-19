using AutoFixture;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.UnitTests.Repositories.Helpers;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class EducationSpecificationsRepositoryTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void GetAllOrderedBy_WithDataRequestParameters_ReturnsSet()
    {
        // Arrange
        var educationSpecifications = new List<EducationSpecification>()
        {
            _fixture.Build<EducationSpecification>()
                .Without(x => x.LearningOutcomes)
                .Without(x => x.Parent)
                .Without(x => x.Children)
                .Without(x => x.Organization)
                .Create(),
            _fixture.Build<EducationSpecification>()
                .Without(x => x.LearningOutcomes)
                .Without(x => x.Parent)
                .Without(x => x.Children)
                .Without(x => x.Organization)
                .Create(),
            _fixture.Build<EducationSpecification>()
                .Without(x => x.LearningOutcomes)
                .Without(x => x.Parent)
                .Without(x => x.Children)
                .Without(x => x.Organization)
                .Create()
        }.AsQueryable();
        var db = Substitute.For<DbSet<EducationSpecification>, IQueryable<EducationSpecification>>();
        DbMockHelper.InitDb(db, educationSpecifications);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.EducationSpecificationsNoTracking.Returns(db);
        var repository = new EducationSpecificationsRepository(dbContext);

        // Act
        var result = repository.GetAllOrderedBy(new DataRequestParameters());

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<EducationSpecification>>());
        Assert.That(result.Items, Has.Count.EqualTo(3));
    }

    [Test]
    public void GetEducationSpecificationsByEducationSpecificationId_WhenEducationSpecificationsExist_ReturnsEducationSpecifications()
    {
        // Arrange
        var educationSpecificationId = _fixture.Create<Guid>();
        var educationSpecification = _fixture.Build<EducationSpecification>()
            .With(x => x.ParentId, educationSpecificationId)
            .Without(x => x.LearningOutcomes)
            .Without(x => x.Parent)
            .Without(x => x.Children)
            .Without(x => x.Organization)
            .Create();
        var educationSpecifications = new List<EducationSpecification> { educationSpecification }.AsQueryable();

        var db = Substitute.For<DbSet<EducationSpecification>, IQueryable<EducationSpecification>>();
        DbMockHelper.InitDb(db, educationSpecifications);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.EducationSpecifications.Returns(db);
        var educationSpecificationsRepository = new EducationSpecificationsRepository(dbContext);

        // Act
        var result = educationSpecificationsRepository.GetEducationSpecificationsByEducationSpecificationId(educationSpecificationId);

        // Assert
        Assert.That(result, Is.InstanceOf<List<EducationSpecification>>());
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].ParentId, Is.EqualTo(educationSpecificationId));
    }

    [Test]
    public void GetEducationSpecificationsByEducationSpecificationId_WhenEducationSpecificationsNotFound_ReturnsEmptyList()
    {
        // Arrange
        var educationSpecificationId = _fixture.Create<Guid>();
        var educationSpecification = _fixture.Build<EducationSpecification>()
            .With(x => x.ParentId, educationSpecificationId)
            .Without(x => x.LearningOutcomes)
            .Without(x => x.Parent)
            .Without(x => x.Children)
            .Without(x => x.Organization)
            .Create();
        var educationSpecifications = new List<EducationSpecification> { educationSpecification }.AsQueryable();

        var db = Substitute.For<DbSet<EducationSpecification>, IQueryable<EducationSpecification>>();
        DbMockHelper.InitDb(db, educationSpecifications);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.EducationSpecifications.Returns(db);
        var educationSpecificationsRepository = new EducationSpecificationsRepository(dbContext);

        // Act
        var result = educationSpecificationsRepository.GetEducationSpecificationsByEducationSpecificationId(_fixture.Create<Guid>());

        // Assert
        Assert.That(result, Is.InstanceOf<List<EducationSpecification>>());
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void GetEducationSpecificationsByOrganizationId_WhenEducationSpecificationsExist_ReturnsEducationSpecifications()
    {
        // Arrange
        var organizationId = _fixture.Create<Guid>();
        var educationSpecification = _fixture.Build<EducationSpecification>()
            .With(x => x.OrganizationId, organizationId)
            .Without(x => x.LearningOutcomes)
            .Without(x => x.Parent)
            .Without(x => x.Children)
            .Without(x => x.Organization)
            .Create();
        var educationSpecifications = new List<EducationSpecification> { educationSpecification }.AsQueryable();

        var db = Substitute.For<DbSet<EducationSpecification>, IQueryable<EducationSpecification>>();
        DbMockHelper.InitDb(db, educationSpecifications);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.EducationSpecifications.Returns(db);
        var educationSpecificationsRepository = new EducationSpecificationsRepository(dbContext);

        // Act
        var result = educationSpecificationsRepository.GetEducationSpecificationsByOrganizationId(organizationId);

        // Assert
        Assert.That(result, Is.InstanceOf<List<EducationSpecification>>());
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].OrganizationId, Is.EqualTo(organizationId));
    }

    [Test]
    public void GetEducationSpecificationsByOrganizationId_WhenEducationSpecificationsNotFound_ReturnsEmptyList()
    {
        // Arrange
        var organizationId = _fixture.Create<Guid>();
        var educationSpecification = _fixture.Build<EducationSpecification>()
            .With(x => x.OrganizationId, organizationId)
            .Without(x => x.LearningOutcomes)
            .Without(x => x.Parent)
            .Without(x => x.Children)
            .Without(x => x.Organization)
            .Create();
        var educationSpecifications = new List<EducationSpecification> { educationSpecification }.AsQueryable();

        var db = Substitute.For<DbSet<EducationSpecification>, IQueryable<EducationSpecification>>();
        DbMockHelper.InitDb(db, educationSpecifications);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.EducationSpecifications.Returns(db);
        var educationSpecificationsRepository = new EducationSpecificationsRepository(dbContext);

        // Act
        var result = educationSpecificationsRepository.GetEducationSpecificationsByOrganizationId(_fixture.Create<Guid>());

        // Assert
        Assert.That(result, Is.InstanceOf<List<EducationSpecification>>());
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void GetEducationSpecification_WhenEducationSpecificationExist_ReturnsEducationSpecification()
    {
        // Arrange
        var educationSpecificationId = _fixture.Create<Guid>();
        var educationSpecification = _fixture.Build<EducationSpecification>()
            .With(x => x.EducationSpecificationId, educationSpecificationId)
            .Without(x => x.LearningOutcomes)
            .Without(x => x.Parent)
            .Without(x => x.Children)
            .Without(x => x.Organization)
            .Create();
        var educationSpecifications = new List<EducationSpecification> { educationSpecification }.AsQueryable();

        var db = Substitute.For<DbSet<EducationSpecification>, IQueryable<EducationSpecification>>();
        DbMockHelper.InitDb(db, educationSpecifications);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.EducationSpecificationsNoTracking.Returns(db);
        var educationSpecificationsRepository = new EducationSpecificationsRepository(dbContext);

        // Act
        var result = educationSpecificationsRepository.GetEducationSpecification(educationSpecificationId, new DataRequestParameters());

        // Assert
        Assert.That(result, Is.InstanceOf<EducationSpecification>());
        Assert.That(result.EducationSpecificationId, Is.EqualTo(educationSpecificationId));
    }

    [Test]
    public void GetEducationSpecification_WhenExpandParentRequestedViaRequestParameters_ReturnsEducationSpecificationAndParent()
    {
        // Arrange
        var educationSpecificationId = _fixture.Create<Guid>();
        var parentEducationSpecificationId = _fixture.Create<Guid>();
        var educationSpecification = _fixture.Build<EducationSpecification>()
            .With(x => x.EducationSpecificationId, educationSpecificationId)
            .With(x => x.ParentId, parentEducationSpecificationId)
            .Without(x => x.LearningOutcomes)
            .Without(x => x.Parent)
            .Without(x => x.Children)
            .Without(x => x.Organization)
            .Create();
        var parentEducationSpecification = _fixture.Build<EducationSpecification>()
            .With(x => x.EducationSpecificationId, parentEducationSpecificationId)
            .Without(x => x.LearningOutcomes)
            .Without(x => x.Parent)
            .Without(x => x.Children)
            .Without(x => x.Organization)
            .Create();
        var educationSpecifications = new List<EducationSpecification> { educationSpecification, parentEducationSpecification }.AsQueryable();

        var db = Substitute.For<DbSet<EducationSpecification>, IQueryable<EducationSpecification>>();
        DbMockHelper.InitDb(db, educationSpecifications);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.EducationSpecificationsNoTracking.Returns(db);
        var educationSpecificationsRepository = new EducationSpecificationsRepository(dbContext);

        var requestParameters = new DataRequestParameters() { Expand = { "Parent" } };

        // Act
        var result = educationSpecificationsRepository.GetEducationSpecification(educationSpecificationId, requestParameters);

        // Assert
        Assert.That(result, Is.InstanceOf<EducationSpecification>());
        Assert.That(result.Parent, Is.InstanceOf<EducationSpecification>());
        Assert.Multiple(() =>
        {
            Assert.That(result.Parent.EducationSpecificationId, Is.EqualTo(parentEducationSpecificationId));
            Assert.That(result.Parent.ChildrenIds, Does.Contain(educationSpecificationId));
            Assert.That(result.EducationSpecificationId, Is.EqualTo(educationSpecificationId));
        });
    }

    [Test]
    public void GetEducationSpecification_WhenExpandChildrenRequestedViaRequestParameters_ReturnsEducationSpecificationAndChildren()
    {
        // Arrange
        var educationSpecificationId = _fixture.Create<Guid>();
        var firstChildEducationSpecificationId = _fixture.Create<Guid>();
        var secondChildEducationSpecificationId = _fixture.Create<Guid>();
        var educationSpecification = _fixture.Build<EducationSpecification>()
            .With(x => x.EducationSpecificationId, educationSpecificationId)
            .Without(x => x.LearningOutcomes)
            .Without(x => x.Parent)
            .Without(x => x.Children)
            .Without(x => x.Organization)
            .Create();
        var firstChildEducationSpecification = _fixture.Build<EducationSpecification>()
            .With(x => x.EducationSpecificationId, firstChildEducationSpecificationId)
            .With(x => x.ParentId, educationSpecificationId)
            .Without(x => x.LearningOutcomes)
            .Without(x => x.Parent)
            .Without(x => x.Children)
            .Without(x => x.Organization)
            .Create();
        var secondChildEducationSpecification = _fixture.Build<EducationSpecification>()
            .With(x => x.EducationSpecificationId, secondChildEducationSpecificationId)
            .With(x => x.ParentId, educationSpecificationId)
            .Without(x => x.LearningOutcomes)
            .Without(x => x.Parent)
            .Without(x => x.Children)
            .Without(x => x.Organization)
            .Create();
        var educationSpecifications = new List<EducationSpecification> { educationSpecification, firstChildEducationSpecification, secondChildEducationSpecification }.AsQueryable();

        var db = Substitute.For<DbSet<EducationSpecification>, IQueryable<EducationSpecification>>();
        DbMockHelper.InitDb(db, educationSpecifications);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.EducationSpecificationsNoTracking.Returns(db);
        var educationSpecificationsRepository = new EducationSpecificationsRepository(dbContext);

        var requestParameters = new DataRequestParameters() { Expand = { "Children" } };

        // Act
        var result = educationSpecificationsRepository.GetEducationSpecification(educationSpecificationId, requestParameters);

        // Assert
        Assert.That(result, Is.InstanceOf<EducationSpecification>());
        Assert.Multiple(() =>
        {
            Assert.That(result.EducationSpecificationId, Is.EqualTo(educationSpecificationId));
            Assert.That(result.Children, Is.InstanceOf<List<EducationSpecification>>());
            Assert.That(result.ChildrenIds, Does.Contain(firstChildEducationSpecificationId));
            Assert.That(result.Children, Does.Contain(firstChildEducationSpecification));
            Assert.That(result.ChildrenIds, Does.Contain(secondChildEducationSpecificationId));
            Assert.That(result.Children, Does.Contain(secondChildEducationSpecification));
        });
    }

    [Test]
    public void GetEducationSpecification_WhenExpandOrganizationRequestedViaRequestParameters_ReturnsEducationSpecificationAndOrganization()
    {
        // Arrange
        var educationSpecificationId = _fixture.Create<Guid>();
        var organizationId = _fixture.Create<Guid>();
        var educationSpecification = _fixture.Build<EducationSpecification>()
            .With(x => x.EducationSpecificationId, educationSpecificationId)
            .With(x => x.OrganizationId, organizationId)
            .Without(x => x.LearningOutcomes)
            .Without(x => x.Parent)
            .Without(x => x.Children)
            .Without(x => x.Organization)
            .Create();
        var educationSpecifications = new List<EducationSpecification> { educationSpecification }.AsQueryable();

        var organization = _fixture.Build<Organization>()
            .With(x => x.OrganizationId, organizationId)
            .Without(x => x.Addresses)
            .Without(x => x.Address)
            .Without(x => x.Parent)
            .Without(x => x.Children)
            .Create();
        var organizations = new List<Organization>() { organization }.AsQueryable();

        var db = Substitute.For<DbSet<EducationSpecification>, IQueryable<EducationSpecification>>();
        DbMockHelper.InitDb(db, educationSpecifications);
        var orgDb = Substitute.For<DbSet<Organization>, IQueryable<Organization>>();
        DbMockHelper.InitDb(orgDb, organizations);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.EducationSpecificationsNoTracking.Returns(db);
        dbContext.OrganizationsNoTracking.Returns(orgDb);
        var educationSpecificationsRepository = new EducationSpecificationsRepository(dbContext);

        var requestParameters = new DataRequestParameters() { Expand = { "Organization" } };

        // Act
        var result = educationSpecificationsRepository.GetEducationSpecification(educationSpecificationId, requestParameters);

        // Assert
        Assert.That(result, Is.InstanceOf<EducationSpecification>());
        Assert.Multiple(() =>
        {
            Assert.That(result.EducationSpecificationId, Is.EqualTo(educationSpecificationId));
            Assert.That(result.Organization!.OrganizationId, Is.EqualTo(organization.OrganizationId));
        });
    }
}