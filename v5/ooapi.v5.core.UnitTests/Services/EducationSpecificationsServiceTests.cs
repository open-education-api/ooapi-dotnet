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
    private readonly Fixture _fixture = new Fixture();

    [Test]
    public async Task GetAll_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IEducationSpecificationsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new EducationSpecificationsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();

        var expected = Substitute.For<Pagination<EducationSpecification>>();
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
        var repository = Substitute.For<IEducationSpecificationsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new EducationSpecificationsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var educationSpecificationId = _fixture.Create<Guid>();

        var expected = new EducationSpecification();
        repository.GetEducationSpecificationAsync(educationSpecificationId, dataRequestParameters).Returns(expected);

        // Act
        var result = await sut.GetAsync(educationSpecificationId, dataRequestParameters);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        await repository.Received(1).GetEducationSpecificationAsync(educationSpecificationId, dataRequestParameters);
    }


    [Test]
    public async Task GetEducationSpecificationsByEducationSpecificationId_CallsRepositoryAndReturnsPagination()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IEducationSpecificationsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new EducationSpecificationsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var educationSpecificationId = _fixture.Create<Guid>();

        var expected = Substitute.For<Pagination<EducationSpecification>>();
        repository.GetEducationSpecificationsByEducationSpecificationIdAsync(educationSpecificationId, dataRequestParameters).Returns(expected);

        // Act
        var result = await sut.GetEducationSpecificationsByEducationSpecificationIdAsync(dataRequestParameters, educationSpecificationId);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<EducationSpecification>>());
        await repository.Received(1).GetEducationSpecificationsByEducationSpecificationIdAsync(educationSpecificationId, dataRequestParameters);
    }

    [Test]
    public async Task GetEducationSpecificationsByOrganizationId_CallsRepositoryAndReturnsPagination()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IEducationSpecificationsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new EducationSpecificationsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var organizationId = _fixture.Create<Guid>();

        var expected = Substitute.For<Pagination<EducationSpecification>>();
        repository.GetEducationSpecificationsByOrganizationIdAsync(organizationId, dataRequestParameters).Returns(expected);

        // Act
        var result = await sut.GetEducationSpecificationsByOrganizationIdAsync(dataRequestParameters, organizationId);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<EducationSpecification>>());
        await repository.Received(1).GetEducationSpecificationsByOrganizationIdAsync(organizationId, dataRequestParameters);
    }
}