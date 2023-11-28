using AutoFixture;
using FluentAssertions;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Models;
using Attribute = ooapi.v5.Models.Attribute;

namespace ooapi.v5.core.UnitTests.Models;

[TestFixture]
public class EducationSpecificationTests
{
    private readonly IFixture _fixture = new Fixture();
    
    [TestCase("TestType", "TestCode", true)]
    [TestCase("TestType", null, false)]
    [TestCase(null, "TestCode", false)]
    [TestCase(null, null, false)]
    public void GetPrimaryCodeIdentifier_ReturnsIdentifierEntryOrNull(string? primaryCodeType, string? primaryCode, bool resultIsNotNull)
    {
        // Arrange
        var educationSpecification = _fixture.Build<EducationSpecification>()
            .With(x => x.PrimaryCodeType, primaryCodeType)
            .With(x => x.PrimaryCode, primaryCode)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = educationSpecification.PrimaryCodeIdentifier;

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
        var educationSpecification = _fixture.Build<EducationSpecification>()
            .OmitAutoProperties()
            .Create();

        var primaryCodeIdentifier = new IdentifierEntry() { CodeType = "TestType", Code = "TestCode" };

        // Act
        educationSpecification.PrimaryCodeIdentifier = primaryCodeIdentifier;

        // Assert
        educationSpecification.PrimaryCodeType.Should().Be(primaryCodeIdentifier.CodeType);
        educationSpecification.PrimaryCode.Should().Be(primaryCodeIdentifier.Code);
    }

    [Test]
    public void SetPrimaryCodeIdentifier_WhenIdentifierEntryIsNull_DoesNotSetPrimaryCodeAndPrimaryCodeType()
    {
        // Arrange
        var codeType = "TestType";
        var code = "TestCode";
        var educationSpecification = _fixture.Build<EducationSpecification>()
            .With(x => x.PrimaryCodeType, codeType)
            .With(x => x.PrimaryCode, code)
            .OmitAutoProperties()
            .Create();

        IdentifierEntry? primaryCodeIdentifier = null;

        // Act
        educationSpecification.PrimaryCodeIdentifier = primaryCodeIdentifier;

        // Assert
        educationSpecification.PrimaryCodeType.Should().Be(codeType);
        educationSpecification.PrimaryCode.Should().Be(code);
    }
    
    //[Test]
    //public void GetName_WhenAttributesExist_ReturnsListLanguageTypedString()
    //{
    //    // Arrange
    //    var educationSpecification = _fixture.Build<EducationSpecification>()
    //        .With(x => x.Attributes, new List<Attribute>() { new() { PropertyName = "name", Language = "en", Value = "TestName" } })
    //        .OmitAutoProperties()
    //        .Create();

    //    // Act
    //    var result = educationSpecification.name;

    //    // Assert
    //    result.Should().NotBeNull();
    //    result.Should().BeOfType<List<LanguageTypedString>>();
    //    result.Should().HaveCount(1);
    //    result[0].Language.Should().Be("en");
    //    result[0].Value.Should().Be("TestName");
    //}

    //[Test]
    //public void GetName_WhenAttributesAreEmpty_ReturnsEmptyListLanguageTypedString()
    //{
    //    // Arrange
    //    var educationSpecification = _fixture.Build<EducationSpecification>()
    //        .With(x => x.Attributes, new List<Attribute>() { })
    //        .OmitAutoProperties()
    //        .Create();

    //    // Act
    //    var result = educationSpecification.name;

    //    // Assert
    //    result.Should().NotBeNull();
    //    result.Should().BeOfType<List<LanguageTypedString>>();
    //    result.Should().HaveCount(0);
    //}
    
    //[Test]
    //public void GetDescription_WhenAttributesExist_ReturnsListLanguageTypedString()
    //{
    //    // Arrange
    //    var educationSpecification = _fixture.Build<EducationSpecification>()
    //        .With(x => x.Attributes, new List<Attribute>() { new() { PropertyName = "description", Language = "en", Value = "TestName" } })
    //        .OmitAutoProperties()
    //        .Create();

    //    // Act
    //    var result = educationSpecification.description;

    //    // Assert
    //    result.Should().NotBeNull();
    //    result.Should().BeOfType<List<LanguageTypedString>>();
    //    result.Should().HaveCount(1);
    //    result[0].Language.Should().Be("en");
    //    result[0].Value.Should().Be("TestName");
    //}

    //[Test]
    //public void GetDescription_WhenAttributesAreEmpty_ReturnsEmptyListLanguageTypedString()
    //{
    //    // Arrange
    //    var educationSpecification = _fixture.Build<EducationSpecification>()
    //        .With(x => x.Attributes, new List<Attribute>() { })
    //        .OmitAutoProperties()
    //        .Create();

    //    // Act
    //    var result = educationSpecification.description;

    //    // Assert
    //    result.Should().NotBeNull();
    //    result.Should().BeOfType<List<LanguageTypedString>>();
    //    result.Should().HaveCount(0);
    //}
    
    [TestCase("TestUnit", 100, true)]
    [TestCase("", 100, false)]
    [TestCase("TestUnit", 0, false)]
    public void GetStudyLoad_ReturnsStudyLoadDescriptor(string? studyLoadUnit, int studyLoadValue, bool isNotNull)
    {
        // Arrange
        var educationSpecification = _fixture.Build<EducationSpecification>()
            .With(x => x.StudyLoadUnit, studyLoadUnit)
            .With(x => x.StudyLoadValue, studyLoadValue)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = educationSpecification.StudyLoad;

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
        var educationSpecification = _fixture.Build<EducationSpecification>()
            .OmitAutoProperties()
            .Create();

        // Act
        educationSpecification.StudyLoad = studyLoadDescriptor;

        // Assert
        educationSpecification.StudyLoadUnit.Should().Be(studyLoadUnit);
        educationSpecification.StudyLoadValue.Should().Be(studyLoadValue);
    }
    
    [Test]
    public void GetOneOfParent_ReturnsOneOfEducationSpecificationInstance()
    {
        // Arrange
        var parentId = _fixture.Create<Guid>();
        var parent = _fixture.Build<EducationSpecification>()
            .With(x => x.EducationSpecificationId, parentId)
            .OmitAutoProperties()
            .Create();
        var educationSpecification = _fixture.Build<EducationSpecification>()
            .With(x => x.ParentId, parentId)
            .With(x => x.Parent, parent)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfEducationSpecificationInstance?)educationSpecification.OneOfParent;

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(parentId);
        result.EducationSpecification.Should().Be(parent);
    }

    [Test]
    public void GetOneOfParent_WhenParentIdIsNull_ReturnsNull()
    {
        // Arrange
        Guid? parentId = null;
        var educationSpecification = _fixture.Build<EducationSpecification>()
            .With(x => x.ParentId, parentId)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfEducationSpecificationInstance?)educationSpecification.OneOfParent;

        // Assert
        result.Should().BeNull();
    }
    
    [Test]
    public void GetChildrenList_ReturnsListOneOfEducationSpecification()
    {
        // Arrange
        var childrenIds = _fixture.CreateMany<Guid>(1).ToList();
        var children = _fixture.Build<EducationSpecification>()
            .With(x => x.EducationSpecificationId, childrenIds[0])
            .OmitAutoProperties()
            .CreateMany(1)
            .ToList();
        var educationSpecification = _fixture.Build<EducationSpecification>()
            .With(x => x.ChildrenIds, childrenIds)
            .With(x => x.Children, children)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = educationSpecification.ChildrenList;

        // Assert
        result.Should().NotBeNull();
        ((OneOfEducationSpecificationInstance)result[0]).EducationSpecification.Should().Be(children[0]);
    }

    [Test]
    public void GetChildrenList_WhenChildrenIdsEmpty_ReturnsEmptyListOneOfEducationSpecification()
    {
        // Arrange
        var childrenIds = new List<Guid>();
        var children = _fixture.Build<EducationSpecification>()
            .OmitAutoProperties()
            .CreateMany(1)
            .ToList();
        var educationSpecification = _fixture.Build<EducationSpecification>()
            .With(x => x.ChildrenIds, childrenIds)
            .With(x => x.Children, children)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = educationSpecification.ChildrenList;

        // Assert
        result.Should().BeEmpty();
    }
    
    [Test]
    public void GetOneOfOrganization_ReturnsOneOfOrganizationInstance()
    {
        // Arrange
        var organizationId = _fixture.Create<Guid>();
        var organization = _fixture.Build<Organization>()
            .With(x => x.OrganizationId, organizationId)
            .OmitAutoProperties()
            .Create();
        var educationSpecification = _fixture.Build<EducationSpecification>()
            .With(x => x.OrganizationId, organizationId)
            .With(x => x.Organization, organization)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfOrganizationInstance?)educationSpecification.OneOfOrganization;

        // Asserts
        result.Should().NotBeNull();
        result!.Id.Should().Be(organizationId);
        result.Organization.Should().Be(organization);
    }

    [Test]
    public void GetOneOfOrganization_WhenOrganizationIdIsNull_ReturnsNull()
    {
        // Arrange
        Guid? organizationId = null;
        var educationSpecification = _fixture.Build<EducationSpecification>()
            .With(x => x.OrganizationId, organizationId)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfOrganizationInstance?)educationSpecification.OneOfOrganization;

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
        var educationSpecification = _fixture.Build<EducationSpecification>()
            .With(x => x.Consumers, consumers)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = educationSpecification.ConsumersList;

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
    }

    [Test]
    public void GetConsumersList_WhenConsumersEmpty_ReturnsEmptyListJObject()
    {
        // Arrange
        var educationSpecification = _fixture.Build<EducationSpecification>()
            .With(x => x.Consumers, new List<Consumer>())
            .OmitAutoProperties()
            .Create();

        // Act
        var result = educationSpecification.ConsumersList;

        // Assert
        result.Should().BeEmpty();
    }
}