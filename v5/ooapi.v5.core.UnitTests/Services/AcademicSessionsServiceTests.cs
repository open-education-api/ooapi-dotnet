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
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void GetAll_WhenEmptyAcademicSessionTypeGiven_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IAcademicSessionsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new AcademicSessionsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = _fixture.Create<DataRequestParameters>();
        string? academicSessionType = null;

        var expected = new Pagination<AcademicSession>();
        repository.GetAllOrderedByAsync(dataRequestParameters, academicSessionType).Returns(expected);

        // Act
        var result = sut.GetAllAsync(dataRequestParameters, academicSessionType);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        repository.Received(1).GetAllOrderedByAsync(dataRequestParameters, academicSessionType);
    }

    [Test]
    public void GetAll_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IAcademicSessionsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new AcademicSessionsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = _fixture.Create<DataRequestParameters>();
        var academicSessionType = _fixture.Create<string>();

        var expected = new Pagination<AcademicSession>();
        repository.GetAllOrderedByAsync(dataRequestParameters, academicSessionType).Returns(expected);

        // Act
        var result = sut.GetAllAsync(dataRequestParameters, academicSessionType);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        repository.Received(1).GetAllOrderedByAsync(dataRequestParameters, academicSessionType);
    }

    [Test]
    public void Get_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IAcademicSessionsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new AcademicSessionsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = _fixture.Create<DataRequestParameters>();
        var academicSessionId = _fixture.Create<Guid>();

        var expected = new AcademicSession();
        repository.GetAcademicSession(academicSessionId, dataRequestParameters).Returns(expected);

        // Act
        var result = sut.GetAsync(academicSessionId, dataRequestParameters);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        repository.Received(1).GetAcademicSession(academicSessionId, dataRequestParameters);
    }
}