using AutoFixture;
using FluentAssertions;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Models;

[TestFixture]
public class AssociationTests
{
    private readonly Fixture _fixture = new Fixture();

    [Test]
    public void GetConsumersList_ReturnsListJObject()
    {
        // Arrange
        var consumers = _fixture.Build<Consumer>()
            .CreateMany(1)
            .ToList();
        var association = _fixture.Build<Association>()
            .With(x => x.Consumers, consumers)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = association.ConsumersList;

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
    }

    [Test]
    public void GetConsumersList_WhenConsumersEmpty_ReturnsEmptyListJObject()
    {
        // Arrange
        var association = _fixture.Build<Association>()
            .With(x => x.Consumers, new List<Consumer>())
            .OmitAutoProperties()
            .Create();

        // Act
        var result = association.ConsumersList;

        // Assert
        result.Should().BeEmpty();
    }

    [Test]
    public void GetOneOfResult_WhenProgramResultExists_ReturnsOneOfResult()
    {
        // Arrange
        var resultId = _fixture.Create<Guid>();
        var programResult = _fixture.Build<ProgramResult>()
            .OmitAutoProperties()
            .Create();
        var association = _fixture.Build<Association>()
            .With(x => x.ResultId, resultId)
            .With(x => x.ProgramResult, programResult)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfResultInstance?)association.OneOfResult;

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(resultId);
        result.ProgramResult.Should().Be(programResult);
    }

    [Test]
    public void GetOneOfResult_WhenCourseResultExists_ReturnsOneOfResult()
    {
        // Arrange
        var resultId = _fixture.Create<Guid>();
        var courseResult = _fixture.Build<CourseResult>()
            .OmitAutoProperties()
            .Create();
        var association = _fixture.Build<Association>()
            .With(x => x.ResultId, resultId)
            .With(x => x.CourseResult, courseResult)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfResultInstance?)association.OneOfResult;

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(resultId);
        result.CourseResult.Should().Be(courseResult);
    }

    [Test]
    public void GetOneOfResult_WhenComponentResultExists_ReturnsOneOfResult()
    {
        // Arrange
        var resultId = _fixture.Create<Guid>();
        var componentResult = _fixture.Build<ComponentResult>()
            .OmitAutoProperties()
            .Create();
        var association = _fixture.Build<Association>()
            .With(x => x.ResultId, resultId)
            .With(x => x.ComponentResult, componentResult)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfResultInstance?)association.OneOfResult;

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(resultId);
        result.ComponentResult.Should().Be(componentResult);
    }

    [Test]
    public void GetOneOfResult_WhenResultIdIsNull_ReturnsNull()
    {
        // Arrange
        Guid? resultId = null;
        var association = _fixture.Build<Association>()
            .With(x => x.ResultId, resultId)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfResultInstance?)association.OneOfResult;

        // Assert
        result.Should().BeNull();
    }

    [Test]
    public void GetOneOfPerson_ReturnsOneOfPerson()
    {
        // Arrange
        var personId = _fixture.Create<Guid>();
        var person = _fixture.Build<Person>()
            .OmitAutoProperties()
            .Create();
        var association = _fixture.Build<Association>()
            .With(x => x.PersonId, personId)
            .With(x => x.Person, person)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfPersonInstance?)association.OneOfPerson;

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(personId);
        result.Person.Should().Be(person);
    }

    [Test]
    public void GetOneOfPerson_WhenPersonIdIsNull_ReturnsNull()
    {
        // Arrange
        var association = _fixture.Build<Association>()
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfPersonInstance?)association.OneOfPerson;

        // Assert
        result.Should().BeNull();
    }

    [Test]
    public void GetOneOfOffering_WhenProgramOfferingExists_ReturnsOneOfResult()
    {
        // Arrange
        var offeringId = _fixture.Create<Guid>();
        var programOffering = _fixture.Build<ProgramOffering>()
            .OmitAutoProperties()
            .Create();
        var association = _fixture.Build<Association>()
            .With(x => x.OfferingId, offeringId)
            .With(x => x.ProgramOffering, programOffering)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfOfferingInstance?)association.OneOfOffering;

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(offeringId);
        result.ProgramOffering.Should().Be(programOffering);
    }

    [Test]
    public void GetOneOfOffering_WhenCourseOfferingExists_ReturnsOneOfResult()
    {
        // Arrange
        var offeringId = _fixture.Create<Guid>();
        var courseOffering = _fixture.Build<CourseOffering>()
            .OmitAutoProperties()
            .Create();
        var association = _fixture.Build<Association>()
            .With(x => x.OfferingId, offeringId)
            .With(x => x.CourseOffering, courseOffering)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfOfferingInstance?)association.OneOfOffering;

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(offeringId);
        result.CourseOffering.Should().Be(courseOffering);
    }

    [Test]
    public void GetOneOfOffering_WhenComponentOfferingExists_ReturnsOneOfResult()
    {
        // Arrange
        var offeringId = _fixture.Create<Guid>();
        var componentOffering = _fixture.Build<ComponentOffering>()
            .OmitAutoProperties()
            .Create();
        var association = _fixture.Build<Association>()
            .With(x => x.OfferingId, offeringId)
            .With(x => x.ComponentOffering, componentOffering)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfOfferingInstance?)association.OneOfOffering;

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(offeringId);
        result.ComponentOffering.Should().Be(componentOffering);
    }

    [Test]
    public void GetOneOfOffering_WhenOfferingIdIsNull_ReturnsNull()
    {
        // Arrange
        Guid? offeringId = null;
        var association = _fixture.Build<Association>()
            .With(x => x.OfferingId, offeringId)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfResultInstance?)association.OneOfResult;

        // Assert
        result.Should().BeNull();
    }
}