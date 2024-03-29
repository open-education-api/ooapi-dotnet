using AutoFixture;
using NSubstitute;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Services;

[TestFixture]
public class AcademicSessionsServiceTests
{
    private readonly Fixture _fixture = new Fixture();

    [Test]
    public async Task GetAll_WhenEmptyAcademicSessionTypeGiven_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IAcademicSessionsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new AcademicSessionsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = _fixture.Create<DataRequestParameters>();
        string? academicSessionType = null;

        var expected = Substitute.For<Pagination<AcademicSession>>();
        repository.GetAllOrderedByAsync(dataRequestParameters, academicSessionType).Returns(expected);

        // Act
        var result = await sut.GetAllAsync(dataRequestParameters, academicSessionType);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        await repository.Received(1).GetAllOrderedByAsync(dataRequestParameters, academicSessionType);
    }

    [Test]
    public async Task GetAll_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IAcademicSessionsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new AcademicSessionsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = _fixture.Create<DataRequestParameters>();
        var academicSessionType = _fixture.Create<string>();

        var expected = Substitute.For<Pagination<AcademicSession>>();
        repository.GetAllOrderedByAsync(dataRequestParameters, academicSessionType).Returns(expected);

        // Act
        var result = await sut.GetAllAsync(dataRequestParameters, academicSessionType);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        await repository.Received(1).GetAllOrderedByAsync(dataRequestParameters, academicSessionType);
    }

    [Test]
    public async Task Get_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IAcademicSessionsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new AcademicSessionsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = _fixture.Create<DataRequestParameters>();
        var academicSessionId = _fixture.Create<Guid>();

        var expected = new AcademicSession();
        repository.GetAcademicSessionAsync(academicSessionId, dataRequestParameters).Returns(expected);

        // Act
        var result = await sut.GetAsync(academicSessionId, dataRequestParameters);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        await repository.Received(1).GetAcademicSessionAsync(academicSessionId, dataRequestParameters);
    }
}