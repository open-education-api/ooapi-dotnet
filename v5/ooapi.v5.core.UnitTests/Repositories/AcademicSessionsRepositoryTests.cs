using AutoFixture;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using ooapi.v5.core.Utility;
using ooapi.v5.Enums;
using ooapi.v5.Models;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.UnitTests.Repositories.Helpers;
using ooapi.v5.Models.Params;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class AcademicSessionsRepositoryTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void GetAcademicSession_WithValidId_ReturnsAcademicSession()
    {
        // Arrange
        var set = CreateAcademicSessions();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.AcademicSessionsNoTracking.Returns(set);
        var repository = new AcademicSessionsRepository(dbContext);

        // Act
        var result = repository.GetAcademicSession(set.First().AcademicSessionId, new DataRequestParameters());

        // Assert
        Assert.IsInstanceOf<AcademicSession>(result);
        Assert.That(result.AcademicSessionId, Is.EqualTo(set.First().AcademicSessionId));
    }

    [Test]
    public void GetAcademicSession_WithInvalidId_ReturnsNull()
    {
        // Arrange
        var set = CreateAcademicSessions();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.AcademicSessionsNoTracking.Returns(set);
        var repository = new AcademicSessionsRepository(dbContext);

        // Act
        var result = repository.GetAcademicSession(Guid.NewGuid(), new DataRequestParameters());

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public void GetAcademicSession_WithValidPrimaryCode_ReturnsAcademicSession()
    {
        // Arrange
        var set = CreateAcademicSessions();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.AcademicSessionsNoTracking.Returns(set);
        var repository = new AcademicSessionsRepository(dbContext);

        // Act
        var result = repository.GetAcademicSession(set.First().PrimaryCode!, new DataRequestParameters());

        // Assert
        Assert.IsInstanceOf<AcademicSession>(result);
        Assert.That(result.PrimaryCode, Is.EqualTo(set.First().PrimaryCode));
    }

    [Test]
    public void GetAcademicSession_WithInvalidPrimaryCode_ReturnsNull()
    {
        // Arrange
        var set = CreateAcademicSessions();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.AcademicSessionsNoTracking.Returns(set);
        var repository = new AcademicSessionsRepository(dbContext);

        // Act
        var result = repository.GetAcademicSession("custom_primary_code_for_test", new DataRequestParameters());

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public void GetAllOrderedBy_WithAcademicSessionType_ReturnsFilteredSet()
    {
        // Arrange
        var academicSessionType = AcademicSessionTypeEnum.semester;
        var set = new List<AcademicSession>()
        {
            // Children, Parent and Year need to be empty they otherwise introduce a circular reference
            // ProgramOfferings, CourseOfferings and ComponentOfferings aren't needed
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
                .With(a => a.AcademicSessionType, AcademicSessionTypeEnum.quarter)
                .Without(a => a.Children)
                .Without(a => a.Parent)
                .Without(a => a.Year)
                .Without(a => a.ProgramOfferings)
                .Without(a => a.CourseOfferings)
                .Without(a => a.ComponentOfferings)
                .Create()
        }.AsQueryable();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.AcademicSessionsNoTracking.Returns(set);
        var repository = new AcademicSessionsRepository(dbContext);

        // Act
        var result = repository.GetAllOrderedBy(new DataRequestParameters(), academicSessionType.ToString());

        // Assert
        Assert.IsInstanceOf<Pagination<AcademicSession>>(result);
        Assert.That(result.Items.TrueForAll(a => a.AcademicSessionType == academicSessionType));
        Assert.That(result.Items.Count, Is.EqualTo(2));
    }

    // Code to filter by consumers doesn't seem to work
    // [Test]
    // public void GetAllOrderedBy_WithConsumer_ReturnsFilteredSet()
    // {
    //     // Arrange
    //     var consumer = "TestConsumer";
    //     var filterParams = new FilterParams() { consumer = consumer };
    //     var dataRequestParameters = new DataRequestParameters(filterParams, new PagingParams(), null);
    //     var academicSessions = new List<AcademicSession>()
    //     {
    //         _fixture.Build<AcademicSession>()
    //             .With(a => a.Consumers, new List<Consumer>()
    //             {
    //                 _fixture.Build<Consumer>()
    //                     .With(c => c.ConsumerKey, consumer)
    //                     .Create()
    //             })
    //             .Without(a => a.Children)
    //             .Without(a => a.Parent)
    //             .Without(a => a.Year)
    //             .Without(a => a.ProgramOfferings)
    //             .Without(a => a.CourseOfferings)
    //             .Without(a => a.ComponentOfferings)
    //             .Create(),
    //         _fixture.Build<AcademicSession>()
    //             .With(a => a.Consumers, new List<Consumer>()
    //             {
    //                 _fixture.Build<Consumer>()
    //                     .With(c => c.ConsumerKey, consumer)
    //                     .Create()
    //             })
    //             .Without(a => a.Children)
    //             .Without(a => a.Parent)
    //             .Without(a => a.Year)
    //             .Without(a => a.ProgramOfferings)
    //             .Without(a => a.CourseOfferings)
    //             .Without(a => a.ComponentOfferings)
    //             .Create(),
    //         _fixture.Build<AcademicSession>()
    //             .With(a => a.Consumers, new List<Consumer>()
    //             {
    //                 _fixture.Build<Consumer>()
    //                     .With(c => c.ConsumerKey, "AnotherConsumer")
    //                     .Create()
    //             })
    //             .Without(a => a.Children)
    //             .Without(a => a.Parent)
    //             .Without(a => a.Year)
    //             .Without(a => a.ProgramOfferings)
    //             .Without(a => a.CourseOfferings)
    //             .Without(a => a.ComponentOfferings)
    //             .Create()
    //     }.AsQueryable();
    //
    //     var db = Substitute.For<DbSet<AcademicSession>, IQueryable<AcademicSession>>();
    //     DbMockHelper.InitDb(db, academicSessions);
    //     var dbContext = Substitute.For<ICoreDbContext>();
    //     dbContext.AcademicSessionsNoTracking.Returns(db);
    //     var academicSessionsRepository = new AcademicSessionsRepository(dbContext);
    //
    //     // Act
    //     var result = academicSessionsRepository.GetAllOrderedBy(dataRequestParameters);
    //
    //     // Assert
    //     Assert.IsInstanceOf<Pagination<AcademicSession>>(result);
    //     Assert.That(result.Items.FindAll(a => a.Consumers!.Count > 0).TrueForAll(b => b.Consumers!.TrueForAll(c => c.ConsumerKey == consumer)), Is.True);
    //     Assert.That(result.Items.Count, Is.EqualTo(2));
    // }

    private IQueryable<AcademicSession> CreateAcademicSessions()
    {
        return new List<AcademicSession>()
        {
            // Children, Parent and Year need to be empty they otherwise introduce a circular reference
            // ProgramOfferings, CourseOfferings and ComponentOfferings aren't needed
            _fixture.Build<AcademicSession>()
                .Without(a => a.Children)
                .Without(a => a.Parent)
                .Without(a => a.Year)
                .Without(a => a.ProgramOfferings)
                .Without(a => a.CourseOfferings)
                .Without(a => a.ComponentOfferings)
                .Create(),
            _fixture.Build<AcademicSession>()
                .Without(a => a.Children)
                .Without(a => a.Parent)
                .Without(a => a.Year)
                .Without(a => a.ProgramOfferings)
                .Without(a => a.CourseOfferings)
                .Without(a => a.ComponentOfferings)
                .Create()
        }.AsQueryable();
    }
}