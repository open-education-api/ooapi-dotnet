using AutoFixture;
using FluentAssertions;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Models;

[TestFixture]
public class ComponentOfferingTests
{
    private readonly IFixture _fixture = new Fixture();
    
    [Test]
    public void GetOneOfComponent_ReturnsOneOfComponentInstance()
    {
        // Arrange
        var componentId = _fixture.Create<Guid>();
        var component = _fixture.Build<Component>()
            .With(x => x.ComponentId, componentId)
            .OmitAutoProperties()
            .Create();
        var componentOffering = _fixture.Build<ComponentOffering>()
            .With(x => x.ComponentId, componentId)
            .With(x => x.Component, component)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfComponentInstance?)componentOffering.OneOfComponent;

        // Asserts
        result.Should().NotBeNull();
        result!.Id.Should().Be(componentId);
        result.Component.Should().Be(component);
    }

    [Test]
    public void GetOneOfComponent_WhenComponentIdIsNull_ReturnsNull()
    {
        // Arrange
        Guid? componentId = null;
        var componentOffering = _fixture.Build<ComponentOffering>()
            .With(x => x.ComponentId, componentId)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfComponentInstance?)componentOffering.OneOfComponent;

        // Assert
        result.Should().BeNull();
    }

    [Test]
    public void GetOneOfCourse_ReturnsOneOfCourseInstance()
    {
        // Arrange
        var courseId = _fixture.Create<Guid>();
        var course = _fixture.Build<Course>()
            .With(x => x.CourseId, courseId)
            .OmitAutoProperties()
            .Create();
        var componentOffering = _fixture.Build<ComponentOffering>()
            .With(x => x.CourseId, courseId)
            .With(x => x.Course, course)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfCourseInstance?)componentOffering.OneOfCourse;

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
        var componentOffering = _fixture.Build<ComponentOffering>()
            .With(x => x.CourseId, courseId)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfCourseInstance?)componentOffering.OneOfCourse;

        // Assert
        result.Should().BeNull();
    }
}