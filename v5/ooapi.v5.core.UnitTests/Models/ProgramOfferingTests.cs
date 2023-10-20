using AutoFixture;
using FluentAssertions;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Models;

public sealed class ProgramOfferingTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void GetOneOfProgram_ReturnsOneOfProgram()
    {
        // Arrange
        var programId = _fixture.Create<Guid>();
        var program = _fixture.Build<Program>()
            .With(x => x.ProgramId, programId)
            .OmitAutoProperties()
            .Create();
        var offering = _fixture.Build<ProgramOffering>()
            .With(x => x.ProgramId, programId)
            .With(x => x.Program, program)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = offering.OneOfProgram;

        // Assert
        result.Should().NotBeNull();
        ((OneOfProgramInstance?)result)?.Program?.ProgramId.Should().Be(programId);
        ((OneOfProgramInstance?)result)?.Id.Should().Be(programId);
    }

    [Test]
    public void GetOneOfProgram_WhenProgramIdIsNull_ReturnsNull()
    {
        // Arrange
        Guid? programId = null;
        var program = _fixture.Build<Program>()
            .With(x => x.ProgramId, programId)
            .OmitAutoProperties()
            .Create();
        var offering = _fixture.Build<ProgramOffering>()
            .With(x => x.ProgramId, programId)
            .With(x => x.Program, program)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = offering.OneOfProgram;

        // Assert
        result.Should().BeNull();
    }
}