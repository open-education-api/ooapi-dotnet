using AutoFixture;
using FluentAssertions;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Models;

[TestFixture]
public class CourseResultTests
{
    private readonly Fixture _fixture = new Fixture();

    [TestCase("TestUnit", 100, true)]
    [TestCase("", 100, false)]
    [TestCase("TestUnit", 0, false)]
    public void GetStudyLoad_ReturnsStudyLoadDescriptor(string? studyLoadUnit, int studyLoadValue, bool isNotNull)
    {
        // Arrange
        var courseResult = _fixture.Build<CourseResult>()
            .With(x => x.StudyLoadUnit, studyLoadUnit)
            .With(x => x.StudyLoadValue, studyLoadValue)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = courseResult.StudyLoad;

        // Assert
        if (isNotNull)
        {
            result!.StudyLoadUnit.Should().Be(studyLoadUnit);
            result.Value.Should().Be(studyLoadValue);
        }
        else
        {
            result.Should().BeNull();
        }
    }

    [Test]
    public void SetStudyLoad_SetsStudyLoadUnitAndStudyLoadValue()
    {
        // Arrange
        var studyLoadUnit = "TestUnit";
        var studyLoadValue = 100;
        var studyLoadDescriptor = new StudyLoadDescriptor()
        {
            StudyLoadUnit = studyLoadUnit,
            Value = studyLoadValue
        };
        var courseResult = _fixture.Build<CourseResult>()
            .OmitAutoProperties()
            .Create();

        // Act
        courseResult.StudyLoad = studyLoadDescriptor;

        // Assert
        courseResult.StudyLoadUnit.Should().Be(studyLoadUnit);
        courseResult.StudyLoadValue.Should().Be(studyLoadValue);
    }
}