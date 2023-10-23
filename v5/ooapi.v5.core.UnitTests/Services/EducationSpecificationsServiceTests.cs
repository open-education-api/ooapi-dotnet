using AutoFixture;
using NSubstitute;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Services;

[TestFixture]
public class EducationSpecificationsServiceTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void GetAll_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IEducationSpecificationsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new EducationSpecificationsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();

        var expected = new Pagination<EducationSpecification>();
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
        var repository = Substitute.For<IEducationSpecificationsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new EducationSpecificationsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var educationSpecificationId = _fixture.Create<Guid>();

        var expected = new EducationSpecification();
        repository.GetEducationSpecification(educationSpecificationId, dataRequestParameters).Returns(expected);

        // Act
        var result = sut.Get(educationSpecificationId, dataRequestParameters);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        repository.Received(1).GetEducationSpecification(educationSpecificationId, dataRequestParameters);
    }


    [Test]
    public void GetEducationSpecificationsByEducationSpecificationId_CallsRepositoryAndReturnsPagination()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IEducationSpecificationsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new EducationSpecificationsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var educationSpecificationId = _fixture.Create<Guid>();

        var educationSpecifications = new List<EducationSpecification>();
        repository.GetEducationSpecificationsByEducationSpecificationId(educationSpecificationId).Returns(educationSpecifications);

        // Act
        var result = sut.GetEducationSpecificationsByEducationSpecificationId(dataRequestParameters, educationSpecificationId);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<EducationSpecification>>());
        repository.Received(1).GetEducationSpecificationsByEducationSpecificationId(educationSpecificationId);
    }

    [Test]
    public void GetEducationSpecificationsByOrganizationId_CallsRepositoryAndReturnsPagination()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IEducationSpecificationsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new EducationSpecificationsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var organizationId = _fixture.Create<Guid>();

        var educationSpecifications = new List<EducationSpecification>();
        repository.GetEducationSpecificationsByOrganizationId(organizationId).Returns(educationSpecifications);

        // Act
        var result = sut.GetEducationSpecificationsByOrganizationId(dataRequestParameters, organizationId);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<EducationSpecification>>());
        repository.Received(1).GetEducationSpecificationsByOrganizationId(organizationId);
    }
}