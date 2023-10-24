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
        var repository = Substitute.For<IProgramsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new ProgramsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var programId = _fixture.Create<Guid>();

        var expected = new Program();
        repository.GetProgram(programId, dataRequestParameters).Returns(expected);

        // Act
        var result = sut.Get(programId, dataRequestParameters);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        repository.Received(1).GetProgram(programId, dataRequestParameters);
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
        repository.GetProgramsByEducationSpecificationId(educationSpecificationId, dataRequestParameters).Returns(programs);

        // Act
        var result = sut.GetProgramsByEducationSpecificationId(dataRequestParameters, educationSpecificationId);

        // Assert
        Assert.That(result, Is.EqualTo(programs));
        repository.Received(1).GetProgramsByEducationSpecificationId(educationSpecificationId, dataRequestParameters);
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
        repository.GetProgramsByOrganizationId(organizationId).Returns(programs);

        // Act
        var result = sut.GetProgramsByOrganizationId(dataRequestParameters, organizationId);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Program>>());
        repository.Received(1).GetProgramsByOrganizationId(organizationId);
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
        repository.GetProgramsByProgramId(programId).Returns(programs);

        // Act
        var result = sut.GetProgramsByProgramId(dataRequestParameters, programId);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Program>>());
        repository.Received(1).GetProgramsByProgramId(programId);
    }
}