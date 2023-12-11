using AutoFixture;
using FluentAssertions;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Models;

[TestFixture]
public class CourseOfferingTests
{
    private readonly Fixture _fixture = new Fixture();
    
    [Test]
    public void GetOneOfCourse_ReturnsOneOfCourseInstance()
    {
        // Arrange
        var courseId = _fixture.Create<Guid>();
        var course = _fixture.Build<Course>()
            .With(x => x.CourseId, courseId)
            .OmitAutoProperties()
            .Create();
        var courseOffering = _fixture.Build<CourseOffering>()
            .With(x => x.CourseId, courseId)
            .With(x => x.Course, course)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfCourseInstance?)courseOffering.OneOfCourse;

        // Asserts
        result.Should().NotBeNull();
        result!.Id.Should().Be(courseId);
        result.Course.Should().Be(course);
    }

    [Test]
    public void GetOneOfCourse_WhenCourseIdIsNull_ReturnsNull()
    {
        // Arrange
        Guid? courseId = null;
        var courseOffering = _fixture.Build<CourseOffering>()
            .With(x => x.CourseId, courseId)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfCourseInstance?)courseOffering.OneOfCourse;

        // Assert
        result.Should().BeNull();
    }

    [Test]
    public void GetOneOfProgramOffering_ReturnsOneOfProgramOfferingInstance()
    {
        // Arrange
        var programOfferingId = _fixture.Create<Guid>();
        var programOffering = _fixture.Build<ProgramOffering>()
            .With(x => x.ProgramId, programOfferingId)
            .OmitAutoProperties()
            .Create();
        var courseOffering = _fixture.Build<CourseOffering>()
            .With(x => x.ProgramOfferingId, programOfferingId)
            .With(x => x.ProgramOffering, programOffering)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfProgramOfferingInstance?)courseOffering.OneOfProgramOffering;

        // Asserts
        result.Should().NotBeNull();
        result!.Id.Should().Be(programOfferingId);
        result.ProgramOffering.Should().Be(programOffering);
    }

    [Test]
    public void GetOneOfProgramOffering_WhenProgramOfferingIdIsNull_ReturnsNull()
    {
        // Arrange
        Guid? programOfferingId = null;
        var courseOffering = _fixture.Build<CourseOffering>()
            .With(x => x.ProgramOfferingId, programOfferingId)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfProgramOfferingInstance?)courseOffering.OneOfProgramOffering;

        // Assert
        result.Should().BeNull();
    }
}