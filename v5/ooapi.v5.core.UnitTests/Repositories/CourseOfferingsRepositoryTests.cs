using AutoFixture;
using MockQueryable.NSubstitute;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class CourseOfferingsRepositoryTests
{
    private readonly Fixture _fixture = new Fixture();

    [Test]
     public async Task GetCourseOffering_WhenCourseOfferingExists_ReturnsCourseOffering()
    {
        // Arrange
        var courseOfferingId = _fixture.Create<Guid>();
        var courseOffering = _fixture.Build<CourseOffering>()
            .With(x => x.OfferingId, courseOfferingId)
            .Without(x => x.Course)
            .Without(x => x.AcademicSession)
            .Without(x => x.Organization)
            .Without(x => x.Address)
            .Without(x => x.Costs)
            .Without(x => x.ProgramOffering)
            .Without(x => x.PriceInformation)
            .Create();
        var courseOfferings = new List<CourseOffering> { courseOffering }.AsQueryable();

        var db = courseOfferings.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.CourseOfferings.Returns(db);
        var courseOfferingsRepository = new CourseOfferingsRepository(dbContext);

        // Act
        var result = await courseOfferingsRepository.GetCourseOfferingAsync(courseOfferingId);

        // Assert
        Assert.That(result, Is.EqualTo(courseOffering));
    }

    [Test]
    public async Task GetCourseOffering_WhenCourseOfferingDoesNotFound_ReturnsNull()
    {
        // Arrange
        var courseOfferingId = _fixture.Create<Guid>();
        var courseOffering = _fixture.Build<CourseOffering>()
            .With(x => x.OfferingId, courseOfferingId)
            .Without(x => x.Course)
            .Without(x => x.AcademicSession)
            .Without(x => x.Organization)
            .Without(x => x.Address)
            .Without(x => x.Costs)
            .Without(x => x.ProgramOffering)
            .Without(x => x.PriceInformation)
            .Create();
        var courseOfferings = new List<CourseOffering> { courseOffering }.AsQueryable();

        var db = courseOfferings.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.CourseOfferings.Returns(db);
        var courseOfferingsRepository = new CourseOfferingsRepository(dbContext);

        // Act
        var result = await courseOfferingsRepository.GetCourseOfferingAsync(_fixture.Create<Guid>());

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetCourseOfferingByCourseId_WhenCourseOfferingsExist_ReturnsCourseOfferings()
    {
        // Arrange
        var courseId = _fixture.Create<Guid>();
        var courseOffering = _fixture.Build<CourseOffering>()
            .With(x => x.CourseId, courseId)
            .With(x => x.Course, new Course() { CourseId = courseId })
            .Without(x => x.Course)
            .Without(x => x.AcademicSession)
            .Without(x => x.Organization)
            .Without(x => x.Address)
            .Without(x => x.Costs)
            .Without(x => x.ProgramOffering)
            .Without(x => x.PriceInformation)
            .Create();
        var courseOfferings = new List<CourseOffering> { courseOffering }.AsQueryable();

        var db = courseOfferings.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.CourseOfferingsNoTracking.Returns(db);
        var courseOfferingsRepository = new CourseOfferingsRepository(dbContext);

        // Act
        var result = await courseOfferingsRepository.GetCourseOfferingByCourseIdAsync(courseId, new DataRequestParameters());

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<CourseOffering>>());
        Assert.That(result.Items, Has.Count.EqualTo(1));
        Assert.That(result.Items[0].Course!.CourseId, Is.EqualTo(courseId));
    }

    [Test]
    public async Task GetCourseOfferingByCourseId_WhenCourseOfferingsNotFound_ReturnsEmptyList()
    {
        // Arrange
        var courseId = _fixture.Create<Guid>();
        var courseOffering = _fixture.Build<CourseOffering>()
            .With(x => x.CourseId, courseId)
            .With(x => x.Course, new Course() { CourseId = courseId })
            .Without(x => x.Course)
            .Without(x => x.AcademicSession)
            .Without(x => x.Organization)
            .Without(x => x.Address)
            .Without(x => x.Costs)
            .Without(x => x.ProgramOffering)
            .Without(x => x.PriceInformation)
            .Create();
        var courseOfferings = new List<CourseOffering> { courseOffering }.AsQueryable();

        var db = courseOfferings.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.CourseOfferingsNoTracking.Returns(db);
        var courseOfferingsRepository = new CourseOfferingsRepository(dbContext);

        // Act
        var result = await courseOfferingsRepository.GetCourseOfferingByCourseIdAsync(_fixture.Create<Guid>(), new DataRequestParameters());

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<CourseOffering>>());
        Assert.That(result.Items, Is.Empty);
    }
}