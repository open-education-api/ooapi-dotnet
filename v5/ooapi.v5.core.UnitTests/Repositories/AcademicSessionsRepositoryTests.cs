using AutoFixture;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using ooapi.v5.core.Utility;
using ooapi.v5.Enums;
using ooapi.v5.Models;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.UnitTests.Repositories.Helpers;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class AcademicSessionsRepositoryTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void GetAcademicSession_WithValidId_ReturnsAcademicSession()
    {
        // Arrange
        var academicSessions = CreateDefaultAcademicSessions();

        var db = Substitute.For<DbSet<AcademicSession>, IQueryable<AcademicSession>>();
        DbMockHelper.InitDb(db, academicSessions);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.AcademicSessionsNoTracking.Returns(db);
        var academicSessionsRepository = new AcademicSessionsRepository(dbContext);

        // Act
        var result = academicSessionsRepository.GetAcademicSession(academicSessions.First().AcademicSessionId, new DataRequestParameters());

        // Assert
        Assert.That(result, Is.InstanceOf<AcademicSession>());
        Assert.That(result.AcademicSessionId, Is.EqualTo(academicSessions.First().AcademicSessionId));
    }

    [Test]
    public void GetAcademicSession_WithInvalidId_ReturnsNull()
    {
        // Arrange
        var academicSessions = CreateDefaultAcademicSessions();

        var db = Substitute.For<DbSet<AcademicSession>, IQueryable<AcademicSession>>();
        DbMockHelper.InitDb(db, academicSessions);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.AcademicSessionsNoTracking.Returns(db);
        var academicSessionsRepository = new AcademicSessionsRepository(dbContext);

        // Act
        var result = academicSessionsRepository.GetAcademicSession(Guid.NewGuid(), new DataRequestParameters());

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void GetAcademicSession_WithValidPrimaryCode_ReturnsAcademicSession()
    {
        // Arrange
        var academicSessions = CreateDefaultAcademicSessions();

        var db = Substitute.For<DbSet<AcademicSession>, IQueryable<AcademicSession>>();
        DbMockHelper.InitDb(db, academicSessions);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.AcademicSessionsNoTracking.Returns(db);
        var academicSessionsRepository = new AcademicSessionsRepository(dbContext);

        // Act
        var result = academicSessionsRepository.GetAcademicSession(academicSessions.First().PrimaryCode!, new DataRequestParameters());

        // Assert
        Assert.That(result, Is.InstanceOf<AcademicSession>());
        Assert.That(result.PrimaryCode, Is.EqualTo(academicSessions.First().PrimaryCode));
    }

    [Test]
    public void GetAcademicSession_WithInvalidPrimaryCode_ReturnsNull()
    {
        // Arrange
        var academicSessions = CreateDefaultAcademicSessions();

        var db = Substitute.For<DbSet<AcademicSession>, IQueryable<AcademicSession>>();
        DbMockHelper.InitDb(db, academicSessions);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.AcademicSessionsNoTracking.Returns(db);
        var academicSessionsRepository = new AcademicSessionsRepository(dbContext);

        // Act
        var result = academicSessionsRepository.GetAcademicSession("custom_primary_code_for_test", new DataRequestParameters());

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void GetAllOrderedBy_WithAcademicSessionType_ReturnsFilteredSet()
    {
        // Arrange
        var academicSessionType = AcademicSessionType.semester;
        var academicSessions = new List<AcademicSession>()
        {
            _fixture.Build<AcademicSession>()
                .With(a => a.AcademicSessionType, academicSessionType)
                .Without(a => a.Children)
                .Without(a => a.Parent)
                .Without(a => a.Year)
                .Without(a => a.ProgramOfferings)
                .Without(a => a.CourseOfferings)
                .Without(a => a.ComponentOfferings)
                .Create(),
            _fixture.Build<AcademicSession>()
                .With(a => a.AcademicSessionType, academicSessionType)
                .Without(a => a.Children)
                .Without(a => a.Parent)
                .Without(a => a.Year)
                .Without(a => a.ProgramOfferings)
                .Without(a => a.CourseOfferings)
                .Without(a => a.ComponentOfferings)
                .Create(),
            _fixture.Build<AcademicSession>()
                .With(a => a.AcademicSessionType, AcademicSessionType.quarter)
                .Without(a => a.Children)
                .Without(a => a.Parent)
                .Without(a => a.Year)
                .Without(a => a.ProgramOfferings)
                .Without(a => a.CourseOfferings)
                .Without(a => a.ComponentOfferings)
                .Create()
        }.AsQueryable();

        var db = Substitute.For<DbSet<AcademicSession>, IQueryable<AcademicSession>>();
        DbMockHelper.InitDb(db, academicSessions);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.AcademicSessionsNoTracking.Returns(db);
        var academicSessionsRepository = new AcademicSessionsRepository(dbContext);

        // Act
        var result = academicSessionsRepository.GetAllOrderedBy(new DataRequestParameters(), academicSessionType.ToString());

        // Assert
        Assert.IsInstanceOf<Pagination<AcademicSession>>(result);
        Assert.That(result.Items.TrueForAll(a => a.AcademicSessionType == academicSessionType));
        Assert.That(result.Items.Count, Is.EqualTo(2));
    }

    private IQueryable<AcademicSession> CreateDefaultAcademicSessions()
    {
        return _fixture.Build<AcademicSession>()
            .Without(a => a.Children)
            .Without(a => a.Parent)
            .Without(a => a.Year)
            .Without(a => a.ProgramOfferings)
            .Without(a => a.CourseOfferings)
            .Without(a => a.ComponentOfferings)
            .CreateMany(2)
            .AsQueryable();
    }
}