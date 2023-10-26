using AutoFixture;
using NSubstitute;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Services;

[TestFixture]
public class ProgramsServiceTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void GetAll_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IProgramsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new ProgramsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();

        var expected = new Pagination<Program>();
        repository.GetAllOrderedByAsync(dataRequestParameters).Returns(expected);

        // Act
        var result = sut.GetAllAsync(dataRequestParameters);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        repository.Received(1).GetAllOrderedByAsync(dataRequestParameters);
    }

    [Test]
    public void Get_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IProgramsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new ProgramsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var programId = _fixture.Create<Guid>();

        var expected = new Program();
        repository.GetProgramAsync(programId, dataRequestParameters).Returns(expected);

        // Act
        var result = sut.GetAsync(programId, dataRequestParameters);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        repository.Received(1).GetProgramAsync(programId, dataRequestParameters);
    }

    [Test]
    public void GetProgramsByEducationSpecificationId_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IProgramsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new ProgramsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var educationSpecificationId = _fixture.Create<Guid>();

        var programs = new Pagination<Program>();
        repository.GetProgramsByEducationSpecificationIdAsync(educationSpecificationId, dataRequestParameters).Returns(programs);

        // Act
        var result = sut.GetProgramsByEducationSpecificationIdAsync(dataRequestParameters, educationSpecificationId);

        // Assert
        Assert.That(result, Is.EqualTo(programs));
        repository.Received(1).GetProgramsByEducationSpecificationIdAsync(educationSpecificationId, dataRequestParameters);
    }

    [Test]
    public void GetProgramsByOrganizationId_CallsRepositoryAndReturnsPagination()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IProgramsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new ProgramsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var organizationId = _fixture.Create<Guid>();

        var programs = new List<Program>();
        repository.GetProgramsByOrganizationIdAsync(organizationId).Returns(programs);

        // Act
        var result = sut.GetProgramsByOrganizationIdAsync(dataRequestParameters, organizationId);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Program>>());
        repository.Received(1).GetProgramsByOrganizationIdAsync(organizationId);
    }

    [Test]
    public void GetProgramsByProgramId_CallsRepositoryAndReturnsPagination()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IProgramsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new ProgramsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var programId = _fixture.Create<Guid>();

        var programs = new List<Program>();
        repository.GetProgramsByProgramIdAsync(programId).Returns(programs);

        // Act
        var result = sut.GetProgramsByProgramIdAsync(dataRequestParameters, programId);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Program>>());
        repository.Received(1).GetProgramsByProgramIdAsync(programId);
    }
}