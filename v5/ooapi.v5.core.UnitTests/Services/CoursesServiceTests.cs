using AutoFixture;
using NSubstitute;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Services;

[TestFixture]
public class CoursesServiceTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void GetAll_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<ICoursesRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new CoursesService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();

        var expected = new Pagination<Course>();
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
        var repository = Substitute.For<ICoursesRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new CoursesService(dbContext, repository, userRequestContext);
        var courseId = _fixture.Create<Guid>();

        var expected = new Course();
        repository.GetCourseAsync(courseId).Returns(expected);

        // Act
        var result = sut.GetAsync(courseId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        repository.Received(1).GetCourseAsync(courseId);
    }


    [Test]
    public void GetCoursesByEducationSpecificationId_CallsRepositoryAndReturnsPagination()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<ICoursesRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new CoursesService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var educationSpecificationId = _fixture.Create<Guid>();

        var expected = new Pagination<Course>();
        repository.GetCoursesByEducationSpecificationIdAsync(educationSpecificationId, dataRequestParameters).Returns(expected);

        // Act
        var result = sut.GetCoursesByEducationSpecificationIdAsync(dataRequestParameters, educationSpecificationId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        repository.Received(1).GetCoursesByEducationSpecificationIdAsync(educationSpecificationId, dataRequestParameters);
    }

    [Test]
    public void GetCoursesByOrganizationId_CallsRepositoryAndReturnsPagination()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<ICoursesRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new CoursesService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var organizationId = _fixture.Create<Guid>();

        var courses = new List<Course>();
        repository.GetCoursesByOrganizationIdAsync(organizationId).Returns(courses);

        // Act
        var result = sut.GetCoursesByOrganizationIdAsync(dataRequestParameters, organizationId);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Course>>());
        repository.Received(1).GetCoursesByOrganizationIdAsync(organizationId);
    }

    [Test]
    public void GetCoursesByProgramId_CallsRepositoryAndReturnsPagination()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<ICoursesRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new CoursesService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();
        var programId = _fixture.Create<Guid>();

        var courses = new List<Course>();
        repository.GetCoursesByProgramIdAsync(programId).Returns(courses);

        // Act
        var result = sut.GetCoursesByProgramIdAsync(dataRequestParameters, programId);

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Course>>());
        repository.Received(1).GetCoursesByProgramIdAsync(programId);
    }
}