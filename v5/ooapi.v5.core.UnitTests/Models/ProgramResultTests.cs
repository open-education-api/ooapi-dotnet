using AutoFixture;
using FluentAssertions;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Models;

public sealed class ProgramResultTests
{
    private readonly Fixture _fixture = new Fixture();

    [TestCase(null, 0)]
    [TestCase(null, 1)]
    [TestCase("", -1)]
    [TestCase("test", 0)]
    public void GetStudyLoad_GivenEmptyStudyLoadUnitAndStudyLoad_ReturnsNull(string? studyLoadUnit, int studyLoad)
    {
        // arrange
        var model = new ProgramResult {StudyLoadUnit = studyLoadUnit, StudyLoadValue = studyLoad};

        // act
        var result = model.StudyLoad;

        // assert
        result.Should().BeNull();
    }

    [Test]
    public void SetStudyLoad_GivenStudyLoadUnitAndStudyLoad_ReturnsStudyLoadDescriptor()
    {
        // arrange
        var studyLoadDescriptor = new StudyLoadDescriptor { StudyLoadUnit = _fixture.Create<string>(), Value = _fixture.Create<int>() };

        // act
        var model = new ProgramResult { StudyLoad = studyLoadDescriptor };
        var result = model.StudyLoad;

        // assert
        result.Should().NotBeNull();
        result!.StudyLoadUnit.Should().Be(model.StudyLoadUnit);
        result.Value.Should().Be(model.StudyLoadValue);
    }
}