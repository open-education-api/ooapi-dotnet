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
    public async Task GetAll_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IProgramsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new ProgramsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();

        var expected = Substitute.For<Pagination<Program>>();
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
        var repository = Substitute.For<IProgramsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new ProgramsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var programId = _fixture.Create<Guid>();

        var expected = new Program();
        repository.GetProgramAsync(programId, dataRequestParameters).Returns(expected);

        // Act
        var result = await sut.GetAsync(programId, dataRequestParameters);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        await repository.Received(1).GetProgramAsync(programId, dataRequestParameters);
    }

    [Test]
    public async Task GetProgramsByEducationSpecificationId_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IProgramsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new ProgramsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var educationSpecificationId = _fixture.Create<Guid>();

        var expected = Substitute.For<Pagination<Program>>();
        repository.GetProgramsByEducationSpecificationIdAsync(educationSpecificationId, dataRequestParameters).Returns(expected);

        // Act
        var result = await sut.GetProgramsByEducationSpecificationIdAsync(dataRequestParameters, educationSpecificationId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        await repository.Received(1).GetProgramsByEducationSpecificationIdAsync(educationSpecificationId, dataRequestParameters);
    }

    [Test]
    public async Task GetProgramsByOrganizationId_CallsRepositoryAndReturnsPagination()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IProgramsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new ProgramsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var organizationId = _fixture.Create<Guid>();

        var expected = Substitute.For<Pagination<Program>>();
        repository.GetProgramsByOrganizationIdAsync(organizationId, dataRequestParameters).Returns(expected);

        // Act
        var result = await sut.GetProgramsByOrganizationIdAsync(dataRequestParameters, organizationId);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Program>>());
        await repository.Received(1).GetProgramsByOrganizationIdAsync(organizationId, dataRequestParameters);
    }

    [Test]
    public async Task GetProgramsByProgramId_CallsRepositoryAndReturnsPagination()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IProgramsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new ProgramsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var programId = _fixture.Create<Guid>();

        var expected = Substitute.For<Pagination<Program>>();
        repository.GetProgramsByProgramIdAsync(programId, dataRequestParameters).Returns(expected);

        // Act
        var result = await sut.GetProgramsByProgramIdAsync(dataRequestParameters, programId);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Program>>());
        await repository.Received(1).GetProgramsByProgramIdAsync(programId, dataRequestParameters);
    }
}