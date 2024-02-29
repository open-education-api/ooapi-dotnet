using AutoFixture;
using MockQueryable.NSubstitute;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class CoursesRepositoryTests
{
    private readonly Fixture _fixture = new Fixture();
    
    [Test]
     public async Task GetCourse_WhenCourseExists_ReturnsCourse()
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

        var db = courses.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Courses.Returns(db);
        var coursesRepository = new CoursesRepository(dbContext);

        // Act
        var result = await coursesRepository.GetCourseAsync(courseId);

        // Assert
        Assert.That(result, Is.EqualTo(course));
    }

    [Test]
    public async Task GetCourse_WhenCourseDoesNotFound_ReturnsNull()
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

        var db = courses.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Courses.Returns(db);
        var coursesRepository = new CoursesRepository(dbContext);

        // Act
        var result = await coursesRepository.GetCourseAsync(_fixture.Create<Guid>());

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetCourseByEducationSpecificationId_WhenCoursesExist_ReturnsCourses()
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

        var db = courses.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.CoursesNoTracking.Returns(db);
        var coursesRepository = new CoursesRepository(dbContext);

        // Act
        var result = await coursesRepository.GetCoursesByEducationSpecificationIdAsync(educationSpecificationId, new DataRequestParameters());

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Course>>());
        Assert.That(result.Items, Has.Count.EqualTo(1));
        Assert.That(result.Items[0].EducationSpecificationId, Is.EqualTo(educationSpecificationId));
    }

    [Test]
    public async Task GetCourseByEducationSpecificationId_WhenCoursesNotFound_ReturnsEmptyList()
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

        var db = courses.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.CoursesNoTracking.Returns(db);
        var coursesRepository = new CoursesRepository(dbContext);

        // Act
        var result = await coursesRepository.GetCoursesByEducationSpecificationIdAsync(_fixture.Create<Guid>(), new DataRequestParameters());

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Course>>());
        Assert.That(result.Items, Is.Empty);
    }

    [Test]
    public async Task GetCoursesByOrganizationId_WhenCoursesExist_ReturnsCourses()
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

        var db = courses.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Courses.Returns(db);
        var coursesRepository = new CoursesRepository(dbContext);

        var dataRequestParameters = new DataRequestParameters();

        // Act
        var result = await coursesRepository.GetCoursesByOrganizationIdAsync(organizationId, dataRequestParameters);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Course>>());
        Assert.That(result.Items, Has.Count.EqualTo(1));
        Assert.That(result.Items[0].OrganizationId, Is.EqualTo(organizationId));
    }

    [Test]
    public async Task GetCoursesByOrganizationId_WhenCoursesNotFound_ReturnsEmptyList()
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

        var db = courses.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Courses.Returns(db);
        var coursesRepository = new CoursesRepository(dbContext);

        var dataRequestParameters = new DataRequestParameters();

        // Act
        var result = await coursesRepository.GetCoursesByOrganizationIdAsync(_fixture.Create<Guid>(), dataRequestParameters);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Course>>());
        Assert.That(result.Items, Is.Empty);
    }
}