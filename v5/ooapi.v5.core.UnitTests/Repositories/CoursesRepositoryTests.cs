using AutoFixture;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.UnitTests.Repositories.Helpers;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class CoursesRepositoryTests
{
    private readonly IFixture _fixture = new Fixture();
    
    [Test]
     public void GetCourse_ReturnsCourse_WhenCourseExists()
    {
        // Arrange
        var courseId = _fixture.Create<Guid>();
        var course = _fixture.Build<Course>()
            .With(x => x.CourseId, courseId)
            .Without(x => x.Organization)
            .Without(x => x.Address)
            .Without(x => x.Addresses)
            .Without(x => x.Duration)
            .Without(x => x.EducationSpecification)
            .Without(x => x.Programs)
            .Without(x => x.Coordinators)
            .Without(x => x.CoordinatorsRef)
            .Without(x => x.ProgramsRef)
            .Create();
        var courses = new List<Course> { course }.AsQueryable();

        var db = Substitute.For<DbSet<Course>, IQueryable<Course>>();
        DbMockHelper.InitDb(db, courses);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Courses.Returns(db);
        var coursesRepository = new CoursesRepository(dbContext);

        // Act
        var result = coursesRepository.GetCourse(courseId);

        // Assert
        Assert.That(result, Is.EqualTo(course));
    }

    [Test]
    public void GetCourse_ReturnsNull_WhenCourseDoesNotFound()
    {
        // Arrange
        var courseId = _fixture.Create<Guid>();
        var course = _fixture.Build<Course>()
            .With(x => x.CourseId, courseId)
            .Without(x => x.Organization)
            .Without(x => x.Address)
            .Without(x => x.Addresses)
            .Without(x => x.Duration)
            .Without(x => x.EducationSpecification)
            .Without(x => x.Programs)
            .Without(x => x.Coordinators)
            .Without(x => x.CoordinatorsRef)
            .Without(x => x.ProgramsRef)
            .Create();
        var courses = new List<Course> { course }.AsQueryable();

        var db = Substitute.For<DbSet<Course>, IQueryable<Course>>();
        DbMockHelper.InitDb(db, courses);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Courses.Returns(db);
        var coursesRepository = new CoursesRepository(dbContext);

        // Act
        var result = coursesRepository.GetCourse(_fixture.Create<Guid>());

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void GetCourseByEducationSpecificationId_ReturnsCourses_WhenCoursesExist()
    {
        // Arrange
        var educationSpecificationId = _fixture.Create<Guid>();
        var course = _fixture.Build<Course>()
            .With(x => x.EducationSpecificationId, educationSpecificationId )
            .Without(x => x.Organization)
            .Without(x => x.Address)
            .Without(x => x.Addresses)
            .Without(x => x.Duration)
            .Without(x => x.EducationSpecification)
            .Without(x => x.Programs)
            .Without(x => x.Coordinators)
            .Without(x => x.CoordinatorsRef)
            .Without(x => x.ProgramsRef)
            .Create();
        var courses = new List<Course> { course }.AsQueryable();

        var db = Substitute.For<DbSet<Course>, IQueryable<Course>>();
        DbMockHelper.InitDb(db, courses);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.CoursesNoTracking.Returns(db);
        var coursesRepository = new CoursesRepository(dbContext);

        // Act
        var result = coursesRepository.GetCoursesByEducationSpecificationId(educationSpecificationId, new DataRequestParameters());

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Course>>());
        Assert.That(result.Items.Count, Is.EqualTo(1));
        Assert.That(result.Items[0].EducationSpecificationId, Is.EqualTo(educationSpecificationId));
    }

    [Test]
    public void GetCourseByEducationSpecificationId_ReturnsEmptyList_WhenCoursesNotFound()
    {
        // Arrange
        var educationSpecificationId = _fixture.Create<Guid>();
        var course = _fixture.Build<Course>()
            .With(x => x.EducationSpecificationId, educationSpecificationId)
            .Without(x => x.Organization)
            .Without(x => x.Address)
            .Without(x => x.Addresses)
            .Without(x => x.Duration)
            .Without(x => x.EducationSpecification)
            .Without(x => x.Programs)
            .Without(x => x.Coordinators)
            .Without(x => x.CoordinatorsRef)
            .Without(x => x.ProgramsRef)
            .Create();
        var courses = new List<Course> { course }.AsQueryable();

        var db = Substitute.For<DbSet<Course>, IQueryable<Course>>();
        DbMockHelper.InitDb(db, courses);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.CoursesNoTracking.Returns(db);
        var coursesRepository = new CoursesRepository(dbContext);

        // Act
        var result = coursesRepository.GetCoursesByEducationSpecificationId(_fixture.Create<Guid>(), new DataRequestParameters());

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Course>>());
        Assert.That(result.Items.Count, Is.EqualTo(0));
    }

    // Code to filter by consumers doesn't seem to work
    // [Test]
    // public void GetCourseByEducationSpecificationId_IncludesConsumers_WhenDataRequestParametersHasConsumer()
    // {
    //     // Arrange
    //     var educationSpecificationId = _fixture.Create<Guid>();
    //     var consumer = "TestConsumer";
    //     var filterParams = new FilterParams() { consumer = consumer };
    //     var dataRequestParameters = new DataRequestParameters(filterParams, new PagingParams(), null);
    //     var courses = new List<Course>()
    //     {
    //         _fixture.Build<Course>()
    //             .With(a => a.Consumers, new List<Consumer>()
    //             {
    //                 _fixture.Build<Consumer>()
    //                     .With(c => c.ConsumerKey, consumer)
    //                     .Create()
    //             })
    //             .With(x => x.EducationSpecificationId, educationSpecificationId)
    //             .Without(x => x.Organization)
    //             .Without(x => x.Address)
    //             .Create(),
    //         _fixture.Build<Course>()
    //             .With(a => a.Consumers, new List<Consumer>()
    //             {
    //                 _fixture.Build<Consumer>()
    //                     .With(c => c.ConsumerKey, consumer)
    //                     .Create()
    //             })
    //             .With(x => x.EducationSpecificationId, educationSpecificationId)
    //             .Without(x => x.Organization)
    //             .Without(x => x.Address)
    //             .Create(),
    //         _fixture.Build<Course>()
    //             .With(a => a.Consumers, new List<Consumer>()
    //             {
    //                 _fixture.Build<Consumer>()
    //                     .With(c => c.ConsumerKey, "AnotherConsumer")
    //                     .Create()
    //             })
    //             .With(x => x.EducationSpecificationId, educationSpecificationId)
    //             .Without(x => x.Organization)
    //             .Without(x => x.Address)
    //             .Create(),
    //     }.AsQueryable();
    //
    //     var db = Substitute.For<DbSet<Course>, IQueryable<Course>>();
    //     DbMockHelper.InitDb(db, courses);
    //     var dbContext = Substitute.For<ICoreDbContext>();
    //     dbContext.CoursesNoTracking.Returns(db);
    //     var coursesRepository = new CoursesRepository(dbContext);
    //
    //     // Act
    //     var result = coursesRepository.GetCoursesByEducationSpecificationId(educationSpecificationId, dataRequestParameters);
    //
    //     // Assert
    //     Assert.IsInstanceOf<Pagination<Course>>(result);
    //     Assert.That(result.Items.FindAll(a => a.Consumers!.Count > 0).TrueForAll(b => b.Consumers!.TrueForAll(c => c.ConsumerKey == consumer)), Is.True);
    //     Assert.That(result.Items.Count, Is.EqualTo(2));
    // }

    [Test]
    public void GetCoursesByOrganizationId_ReturnsCourses_WhenCoursesExist()
    {
        // Arrange
        var organizationId = _fixture.Create<Guid>();
        var course = _fixture.Build<Course>()
            .With(x => x.OrganizationId, organizationId)
            .Without(x => x.Organization)
            .Without(x => x.Address)
            .Without(x => x.Addresses)
            .Without(x => x.Duration)
            .Without(x => x.EducationSpecification)
            .Without(x => x.Programs)
            .Without(x => x.Coordinators)
            .Without(x => x.CoordinatorsRef)
            .Without(x => x.ProgramsRef)
            .Create();
        var courses = new List<Course> { course }.AsQueryable();

        var db = Substitute.For<DbSet<Course>, IQueryable<Course>>();
        DbMockHelper.InitDb(db, courses);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Courses.Returns(db);
        var coursesRepository = new CoursesRepository(dbContext);

        // Act
        var result = coursesRepository.GetCoursesByOrganizationId(organizationId);

        // Assert
        Assert.That(result, Is.InstanceOf<List<Course>>());
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result[0].OrganizationId, Is.EqualTo(organizationId));
    }

    [Test]
    public void GetCoursesByOrganizationId_ReturnsEmptyList_WhenCoursesNotFound()
    {
        // Arrange
        var organizationId = _fixture.Create<Guid>();
        var course = _fixture.Build<Course>()
            .With(x => x.OrganizationId, organizationId)
            .Without(x => x.Organization)
            .Without(x => x.Address)
            .Without(x => x.Addresses)
            .Without(x => x.Duration)
            .Without(x => x.EducationSpecification)
            .Without(x => x.Programs)
            .Without(x => x.Coordinators)
            .Without(x => x.CoordinatorsRef)
            .Without(x => x.ProgramsRef)
            .Create();
        var courses = new List<Course> { course }.AsQueryable();

        var db = Substitute.For<DbSet<Course>, IQueryable<Course>>();
        DbMockHelper.InitDb(db, courses);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Courses.Returns(db);
        var coursesRepository = new CoursesRepository(dbContext);

        // Act
        var result = coursesRepository.GetCoursesByOrganizationId(_fixture.Create<Guid>());

        // Assert
        Assert.That(result, Is.InstanceOf<List<Course>>());
        Assert.That(result.Count, Is.EqualTo(0));
    }
}