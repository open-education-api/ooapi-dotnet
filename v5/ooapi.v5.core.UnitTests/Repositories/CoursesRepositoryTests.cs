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
     public void GetCourse_WhenCourseExists_ReturnsCourse()
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
    public void GetCourse_WhenCourseDoesNotFound_ReturnsNull()
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
    public void GetCourseByEducationSpecificationId_WhenCoursesExist_ReturnsCourses()
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
    public void GetCourseByEducationSpecificationId_WhenCoursesNotFound_ReturnsEmptyList()
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

    [Test]
    public void GetCoursesByOrganizationId_WhenCoursesExist_ReturnsCourses()
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
    public void GetCoursesByOrganizationId_WhenCoursesNotFound_ReturnsEmptyList()
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