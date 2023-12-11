using AutoFixture;
using FluentAssertions;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Models;
using Attribute = ooapi.v5.Models.Attribute;

namespace ooapi.v5.core.UnitTests.Models;

[TestFixture]
public class AcademicSessionTests
{
    private readonly Fixture _fixture = new Fixture();

    [TestCase("TestType", "TestCode", true)]
    [TestCase("TestType", null, false)]
    [TestCase(null, "TestCode", false)]
    [TestCase(null, null, false)]
    public void GetPrimaryCodeIdentifier_ReturnsIdentifierEntryOrNull(string? primaryCodeType, string? primaryCode, bool resultIsNotNull)
    {
        // Arrange
        var academicSession = _fixture.Build<AcademicSession>()
            .With(x => x.PrimaryCodeType, primaryCodeType)
            .With(x => x.PrimaryCode, primaryCode)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = academicSession.PrimaryCodeIdentifier;

        // Assert
        if (resultIsNotNull)
        {
            result.Should().BeOfType<IdentifierEntry>();
            result!.CodeType.Should().Be(primaryCodeType);
            result.Code.Should().Be(primaryCode);
        }
        else
        {
            result.Should().BeNull();
        }
    }

    [Test]
    public void SetPrimaryCodeIdentifier_SetsPrimaryCodeAndPrimaryCodeType()
    {
        // Arrange
        var academicSession = _fixture.Build<AcademicSession>()
            .OmitAutoProperties()
            .Create();

        var primaryCodeIdentifier = new IdentifierEntry() { CodeType = "TestType", Code = "TestCode" };

        // Act
        academicSession.PrimaryCodeIdentifier = primaryCodeIdentifier;

        // Assert
        academicSession.PrimaryCodeType.Should().Be(primaryCodeIdentifier.CodeType);
        academicSession.PrimaryCode.Should().Be(primaryCodeIdentifier.Code);
    }

    [Test]
    public void SetPrimaryCodeIdentifier_WhenIdentifierEntryIsNull_DoesNotSetPrimaryCodeAndPrimaryCodeType()
    {
        // Arrange
        var codeType = "TestType";
        var code = "TestCode";
        var academicSession = _fixture.Build<AcademicSession>()
            .With(x => x.PrimaryCodeType, codeType)
            .With(x => x.PrimaryCode, code)
            .OmitAutoProperties()
            .Create();

        IdentifierEntry? primaryCodeIdentifier = null;

        // Act
        academicSession.PrimaryCodeIdentifier = primaryCodeIdentifier;

        // Assert
        academicSession.PrimaryCodeType.Should().Be(codeType);
        academicSession.PrimaryCode.Should().Be(code);
    }

    [Test]
    public void GetName_WhenAttributesExist_ReturnsListLanguageTypedString()
    {
        // Arrange
        var academicSession = _fixture.Build<AcademicSession>()
            .With(x => x.Attributes, new List<Attribute>() { new() { PropertyName = "name", Language = "en", Value = "TestName" } })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = academicSession.Name;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(1);
        result[0].Language.Should().Be("en");
        result[0].Value.Should().Be("TestName");
    }

    [Test]
    public void GetName_WhenAttributesAreEmpty_ReturnsEmptyListLanguageTypedString()
    {
        // Arrange
        var academicSession = _fixture.Build<AcademicSession>()
            .With(x => x.Attributes, new List<Attribute>() { })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = academicSession.Name;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(0);
    }

    [Test]
    public void GetOneOfParent_ReturnsOneOfAcademicSessionInstance()
    {
        // Arrange
        var parentId = _fixture.Create<Guid>();
        var parent = _fixture.Build<AcademicSession>()
            .With(x => x.AcademicSessionId, parentId)
            .OmitAutoProperties()
            .Create();
        var academicSession = _fixture.Build<AcademicSession>()
            .With(x => x.ParentId, parentId)
            .With(x => x.Parent, parent)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfAcademicSessionInstance?)academicSession.OneOfParent;

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(parentId);
        result.AcademicSession.Should().Be(parent);
    }

    [Test]
    public void GetOneOfParent_WhenParentIdIsNull_ReturnsNull()
    {
        // Arrange
        Guid? parentId = null;
        var academicSession = _fixture.Build<AcademicSession>()
            .With(x => x.ParentId, parentId)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfAcademicSessionInstance?)academicSession.OneOfParent;

        // Assert
        result.Should().BeNull();
    }

    [Test]
    public void GetChildrenList_ReturnsListOneOfAcademicSession()
    {
        // Arrange
        var childrenIds = _fixture.CreateMany<Guid>(1).ToList();
        var children = _fixture.Build<AcademicSession>()
            .With(x => x.AcademicSessionId, childrenIds[0])
            .OmitAutoProperties()
            .CreateMany(1)
            .ToList();
        var academicSession = _fixture.Build<AcademicSession>()
            .With(x => x.ChildrenIds, childrenIds)
            .With(x => x.Children, children)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = academicSession.ChildrenList;

        // Assert
        result.Should().NotBeNull();
        ((OneOfAcademicSessionInstance)result[0]).AcademicSession.Should().Be(children[0]);
    }

    [Test]
    public void GetChildrenList_WhenChildrenIdsEmpty_ReturnsEmptyListOneOfAcademicSession()
    {
        // Arrange
        var childrenIds = new List<Guid>();
        var children = _fixture.Build<AcademicSession>()
            .OmitAutoProperties()
            .CreateMany(1)
            .ToList();
        var academicSession = _fixture.Build<AcademicSession>()
            .With(x => x.ChildrenIds, childrenIds)
            .With(x => x.Children, children)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = academicSession.ChildrenList;

        // Assert
        result.Should().BeEmpty();
    }

    [Test]
    public void GetOneOfYear_ReturnsOneOfAcademicSession()
    {
        // Arrange
        var yearId = _fixture.Create<Guid>();
        var year = _fixture.Build<AcademicSession>()
            .With(x => x.AcademicSessionId, yearId)
            .OmitAutoProperties()
            .Create();
        var academicSession = _fixture.Build<AcademicSession>()
            .With(x => x.YearId, yearId)
            .With(x => x.Year, year)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = academicSession.OneOfYear;

        // Assert
        result.Should().NotBeNull();
        ((OneOfAcademicSessionInstance?)result)?.AcademicSession?.AcademicSessionId.Should().Be(yearId);
        ((OneOfAcademicSessionInstance?)result)?.Id.Should().Be(yearId);
    }

    [Test]
    public void GetOneOfYear_WhenYearIdIsNull_ReturnsNull()
    {
        // Arrange
        Guid? yearId = null;
        var year = _fixture.Build<AcademicSession>()
            .With(x => x.AcademicSessionId, yearId)
            .OmitAutoProperties()
            .Create();
        var academicSession = _fixture.Build<AcademicSession>()
            .With(x => x.YearId, yearId)
            .With(x => x.Year, year)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = academicSession.OneOfYear;

        // Assert
        result.Should().BeNull();
    }

    [Test]
    public void GetOneOfYear_WhenYearHasSameId_ReturnsNull()
    {
        // Arrange
        Guid? yearId = _fixture.Create<Guid>();
        var year = _fixture.Build<AcademicSession>()
            .With(x => x.AcademicSessionId, yearId)
            .OmitAutoProperties()
            .Create();
        var academicSession = _fixture.Build<AcademicSession>()
            .With(x => x.AcademicSessionId, yearId)
            .With(x => x.YearId, yearId)
            .With(x => x.Year, year)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = academicSession.OneOfYear;

        // Assert
        result.Should().BeNull();
    }

    [Test]
    public void GetConsumersList_ReturnsListJObject()
    {
        // Arrange
        var consumers = _fixture.Build<Consumer>()
            .CreateMany(1)
            .ToList();
        var academicSession = _fixture.Build<AcademicSession>()
            .With(x => x.Consumers, consumers)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = academicSession.ConsumersList;

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
    }

    [Test]
    public void GetConsumersList_WhenConsumersEmpty_ReturnsEmptyListJObject()
    {
        // Arrange
        var academicSession = _fixture.Build<AcademicSession>()
            .With(x => x.Consumers, new List<Consumer>())
            .OmitAutoProperties()
            .Create();

        // Act
        var result = academicSession.ConsumersList;

        // Assert
        result.Should().BeEmpty();
    }
}