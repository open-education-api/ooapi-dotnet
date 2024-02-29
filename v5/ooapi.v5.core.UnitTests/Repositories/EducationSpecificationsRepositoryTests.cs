using AutoFixture;
using MockQueryable.NSubstitute;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class EducationSpecificationsRepositoryTests
{
    private readonly Fixture _fixture = new Fixture();

    [Test]
    public async Task GetAllOrderedBy_WithDataRequestParameters_ReturnsSet()
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

        var db = educationSpecifications.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.EducationSpecificationsNoTracking.Returns(db);
        var repository = new EducationSpecificationsRepository(dbContext);

        // Act
        var result = await repository.GetAllOrderedByAsync(new DataRequestParameters());

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<EducationSpecification>>());
        Assert.That(result.Items, Has.Count.EqualTo(3));
    }

    [Test]
    public async Task GetEducationSpecificationsByEducationSpecificationId_WhenEducationSpecificationsExist_ReturnsEducationSpecifications()
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

        var db = educationSpecifications.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.EducationSpecifications.Returns(db);
        var educationSpecificationsRepository = new EducationSpecificationsRepository(dbContext);

        var dataRequestParameters = new DataRequestParameters();

        // Act
        var result = await educationSpecificationsRepository.GetEducationSpecificationsByEducationSpecificationIdAsync(educationSpecificationId, dataRequestParameters);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<EducationSpecification>>());
        Assert.That(result.Items, Has.Count.EqualTo(1));
        Assert.That(result.Items[0].ParentId, Is.EqualTo(educationSpecificationId));
    }

    [Test]
    public async Task GetEducationSpecificationsByEducationSpecificationId_WhenEducationSpecificationsNotFound_ReturnsEmptyList()
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

        var db = educationSpecifications.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.EducationSpecifications.Returns(db);
        var educationSpecificationsRepository = new EducationSpecificationsRepository(dbContext);

        var dataRequestParameters = new DataRequestParameters();

        // Act
        var result = await educationSpecificationsRepository.GetEducationSpecificationsByEducationSpecificationIdAsync(_fixture.Create<Guid>(), dataRequestParameters);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<EducationSpecification>>());
        Assert.That(result.Items, Is.Empty);
    }

    [Test]
    public async Task GetEducationSpecificationsByOrganizationId_WhenEducationSpecificationsExist_ReturnsEducationSpecifications()
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

        var db = educationSpecifications.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.EducationSpecifications.Returns(db);
        var educationSpecificationsRepository = new EducationSpecificationsRepository(dbContext);

        var dataRequestParameters = new DataRequestParameters();

        // Act
        var result = await educationSpecificationsRepository.GetEducationSpecificationsByOrganizationIdAsync(organizationId, dataRequestParameters);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<EducationSpecification>>());
        Assert.That(result.Items, Has.Count.EqualTo(1));
        Assert.That(result.Items[0].OrganizationId, Is.EqualTo(organizationId));
    }

    [Test]
    public async Task GetEducationSpecificationsByOrganizationId_WhenEducationSpecificationsNotFound_ReturnsEmptyList()
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

        var db = educationSpecifications.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.EducationSpecifications.Returns(db);
        var educationSpecificationsRepository = new EducationSpecificationsRepository(dbContext);

        var dataRequestParameters = new DataRequestParameters();

        // Act
        var result = await educationSpecificationsRepository.GetEducationSpecificationsByOrganizationIdAsync(_fixture.Create<Guid>(), dataRequestParameters);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<EducationSpecification>>());
        Assert.That(result.Items, Is.Empty);
    }

    [Test]
    public async Task GetEducationSpecification_WhenEducationSpecificationExist_ReturnsEducationSpecification()
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

        var db = educationSpecifications.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.EducationSpecificationsNoTracking.Returns(db);
        var educationSpecificationsRepository = new EducationSpecificationsRepository(dbContext);

        // Act
        var result = await educationSpecificationsRepository.GetEducationSpecificationAsync(educationSpecificationId, new DataRequestParameters());

        // Assert
        Assert.That(result, Is.InstanceOf<EducationSpecification>());
        Assert.That(result.EducationSpecificationId, Is.EqualTo(educationSpecificationId));
    }

    [Test]
    public async Task GetEducationSpecification_WhenExpandParentRequestedViaRequestParameters_ReturnsEducationSpecificationAndParent()
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

        var db = educationSpecifications.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.EducationSpecificationsNoTracking.Returns(db);
        var educationSpecificationsRepository = new EducationSpecificationsRepository(dbContext);

        var requestParameters = new DataRequestParameters() { Expand = { "Parent" } };

        // Act
        var result = await educationSpecificationsRepository.GetEducationSpecificationAsync(educationSpecificationId, requestParameters);

        // Assert
        Assert.That(result, Is.InstanceOf<EducationSpecification>());
        Assert.That(result.Parent, Is.InstanceOf<EducationSpecification>());
        Assert.Multiple(() =>
        {
            Assert.That(result.Parent!.EducationSpecificationId, Is.EqualTo(parentEducationSpecificationId));
            Assert.That(result.Parent.ChildrenIds, Does.Contain(educationSpecificationId));
            Assert.That(result.EducationSpecificationId, Is.EqualTo(educationSpecificationId));
        });
    }

    [Test]
    public async Task GetEducationSpecification_WhenExpandChildrenRequestedViaRequestParameters_ReturnsEducationSpecificationAndChildren()
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

        var db = educationSpecifications.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.EducationSpecificationsNoTracking.Returns(db);
        var educationSpecificationsRepository = new EducationSpecificationsRepository(dbContext);

        var requestParameters = new DataRequestParameters() { Expand = { "Children" } };

        // Act
        var result = await educationSpecificationsRepository.GetEducationSpecificationAsync(educationSpecificationId, requestParameters);

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
    public async Task GetEducationSpecification_WhenExpandOrganizationRequestedViaRequestParameters_ReturnsEducationSpecificationAndOrganization()
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

        var db = educationSpecifications.BuildMockDbSet();
        var orgDb = organizations.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.EducationSpecificationsNoTracking.Returns(db);
        dbContext.OrganizationsNoTracking.Returns(orgDb);
        var educationSpecificationsRepository = new EducationSpecificationsRepository(dbContext);

        var requestParameters = new DataRequestParameters() { Expand = { "Organization" } };

        // Act
        var result = await educationSpecificationsRepository.GetEducationSpecificationAsync(educationSpecificationId, requestParameters);

        // Assert
        Assert.That(result, Is.InstanceOf<EducationSpecification>());
        Assert.Multiple(() =>
        {
            Assert.That(result.EducationSpecificationId, Is.EqualTo(educationSpecificationId));
            Assert.That(result.Organization!.OrganizationId, Is.EqualTo(organization.OrganizationId));
        });
    }
}