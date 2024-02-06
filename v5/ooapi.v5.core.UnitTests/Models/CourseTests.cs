using AutoFixture;
using FluentAssertions;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Models;
using Attribute = ooapi.v5.Models.Attribute;

namespace ooapi.v5.core.UnitTests.Models;

[TestFixture]
public class CourseTests
{
    private readonly Fixture _fixture = new Fixture();

    [TestCase("TestType", "TestCode", true)]
    [TestCase("TestType", null, false)]
    [TestCase(null, "TestCode", false)]
    [TestCase(null, null, false)]
    public void GetPrimaryCodeIdentifier_ReturnsIdentifierEntryOrNull(string? primaryCodeType, string? primaryCode, bool resultIsNotNull)
    {
        // Arrange
        var course = _fixture.Build<Course>()
            .With(x => x.PrimaryCodeType, primaryCodeType)
            .With(x => x.PrimaryCode, primaryCode)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = course.PrimaryCodeIdentifier;

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
        var course = _fixture.Build<Course>()
            .OmitAutoProperties()
            .Create();

        var primaryCodeIdentifier = new IdentifierEntry() { CodeType = "TestType", Code = "TestCode" };

        // Act
        course.PrimaryCodeIdentifier = primaryCodeIdentifier;

        // Assert
        course.PrimaryCodeType.Should().Be(primaryCodeIdentifier.CodeType);
        course.PrimaryCode.Should().Be(primaryCodeIdentifier.Code);
    }

    [Test]
    public void SetPrimaryCodeIdentifier_WhenIdentifierEntryIsNull_DoesNotSetPrimaryCodeAndPrimaryCodeType()
    {
        // Arrange
        var codeType = "TestType";
        var code = "TestCode";
        var course = _fixture.Build<Course>()
            .With(x => x.PrimaryCodeType, codeType)
            .With(x => x.PrimaryCode, code)
            .OmitAutoProperties()
            .Create();

        IdentifierEntry? primaryCodeIdentifier = null;

        // Act
        course.PrimaryCodeIdentifier = primaryCodeIdentifier;

        // Assert
        course.PrimaryCodeType.Should().Be(codeType);
        course.PrimaryCode.Should().Be(code);
    }
    
    [Test]
    public void GetName_WhenAttributesExist_ReturnsListLanguageTypedString()
    {
        // Arrange
        var course = _fixture.Build<Course>()
            .With(x => x.Attributes, new List<Attribute>() { new() { PropertyName = "name", Language = "en", Value = "TestName" } })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = course.name;

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
        var course = _fixture.Build<Course>()
            .With(x => x.Attributes, new List<Attribute>() { })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = course.name;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(0);
    }

    [TestCase("TestUnit", 100, true)]
    [TestCase("", 100, false)]
    [TestCase("TestUnit", 0, false)]
    public void GetStudyLoad_ReturnsStudyLoadDescriptor(string? studyLoadUnit, int studyLoadValue, bool isNotNull)
    {
        // Arrange
        var course = _fixture.Build<Course>()
            .With(x => x.StudyLoadUnit, studyLoadUnit)
            .With(x => x.StudyLoadValue, studyLoadValue)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = course.StudyLoad;

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
        var course = _fixture.Build<Course>()
            .OmitAutoProperties()
            .Create();

        // Act
        course.StudyLoad = studyLoadDescriptor;

        // Assert
        course.StudyLoadUnit.Should().Be(studyLoadUnit);
        course.StudyLoadValue.Should().Be(studyLoadValue);
    }
    
    [TestCase("distance-learning", Enums.ModeOfDelivery.distance_learning)]
    [TestCase("on campus", Enums.ModeOfDelivery.on_campus)]
    [TestCase("online", Enums.ModeOfDelivery.online)]
    [TestCase("hybrid", Enums.ModeOfDelivery.hybrid)]
    [TestCase("situated", Enums.ModeOfDelivery.situated)]
    [TestCase("", null)]
    public void GetModeOfDel_ReturnsListModeOfDelivery(string modeOfDelivery, Enums.ModeOfDelivery? expectedModeOfDelivery)
    {
        // Arrange
        var course = _fixture.Build<Course>()
            .With(x => x.ModeOfDelivery, modeOfDelivery)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = course.ModeOfDel;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<Enums.ModeOfDelivery>>();
        if (expectedModeOfDelivery != null)
        {
            result.Should().HaveCount(1);
            result[0].Should().Be(expectedModeOfDelivery);
        }
        else
        {
            result.Should().HaveCount(0);
        }
    }
    
    [Test]
    public void GetDescription_WhenAttributesExist_ReturnsListLanguageTypedString()
    {
        // Arrange
        var course = _fixture.Build<Course>()
            .With(x => x.Attributes, new List<Attribute>() { new() { PropertyName = "description", Language = "en", Value = "TestName" } })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = course.description;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(1);
        result[0].Language.Should().Be("en");
        result[0].Value.Should().Be("TestName");
    }

    [Test]
    public void GetDescription_WhenAttributesAreEmpty_ReturnsEmptyListLanguageTypedString()
    {
        // Arrange
        var course = _fixture.Build<Course>()
            .With(x => x.Attributes, new List<Attribute>() { })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = course.description;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(0);
    }

    [Test]
    public void GetAdmissionRequirements_WhenAttributesExist_ReturnsListLanguageTypedString()
    {
        // Arrange
        var course = _fixture.Build<Course>()
            .With(x => x.Attributes, new List<Attribute>() { new() { PropertyName = "admissionRequirement", Language = "en", Value = "TestName" } })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = course.admissionRequirements;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(1);
        result[0].Language.Should().Be("en");
        result[0].Value.Should().Be("TestName");
    }

    [Test]
    public void GetAdmissionRequirements_WhenAttributesAreEmpty_ReturnsEmptyListLanguageTypedString()
    {
        // Arrange
        var course = _fixture.Build<Course>()
            .With(x => x.Attributes, new List<Attribute>() { })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = course.admissionRequirements;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(0);
    }

    [Test]
    public void GetQualificationRequirements_WhenAttributesExist_ReturnsListLanguageTypedString()
    {
        // Arrange
        var course = _fixture.Build<Course>()
            .With(x => x.Attributes, new List<Attribute>() { new() { PropertyName = "qualificationRequirement", Language = "en", Value = "TestName" } })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = course.qualificationRequirements;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(1);
        result[0].Language.Should().Be("en");
        result[0].Value.Should().Be("TestName");
    }

    [Test]
    public void GetQualificationRequirements_WhenAttributesAreEmpty_ReturnsEmptyListLanguageTypedString()
    {
        // Arrange
        var course = _fixture.Build<Course>()
            .With(x => x.Attributes, new List<Attribute>() { })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = course.qualificationRequirements;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(0);
    }

    [Test]
    public void GetEnrollment_WhenAttributesExist_ReturnsListLanguageTypedString()
    {
        // Arrange
        var course = _fixture.Build<Course>()
            .With(x => x.Attributes, new List<Attribute>() { new() { PropertyName = "enrollment", Language = "en", Value = "TestName" } })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = course.enrollment;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(1);
        result[0].Language.Should().Be("en");
        result[0].Value.Should().Be("TestName");
    }

    [Test]
    public void GetEnrollment_WhenAttributesAreEmpty_ReturnsEmptyListLanguageTypedString()
    {
        // Arrange
        var course = _fixture.Build<Course>()
            .With(x => x.Attributes, new List<Attribute>() { })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = course.enrollment;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(0);
    }

    [Test]
    public void GetAssessment_WhenAttributesExist_ReturnsListLanguageTypedString()
    {
        // Arrange
        var course = _fixture.Build<Course>()
            .With(x => x.Attributes, new List<Attribute>() { new() { PropertyName = "assessment", Language = "en", Value = "TestName" } })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = course.assessment;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(1);
        result[0].Language.Should().Be("en");
        result[0].Value.Should().Be("TestName");
    }

    [Test]
    public void GetAssessment_WhenAttributesAreEmpty_ReturnsEmptyListLanguageTypedString()
    {
        // Arrange
        var course = _fixture.Build<Course>()
            .With(x => x.Attributes, new List<Attribute>() { })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = course.assessment;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(0);
    }

    [Test]
    public void GetOneOfEducationSpecification_ReturnsOneOfEducationSpecificationInstance()
    {
        // Arrange
        var educationSpecificationId = _fixture.Create<Guid>();
        var educationSpecification = _fixture.Build<EducationSpecification>()
            .With(x => x.EducationSpecificationId, educationSpecificationId)
            .OmitAutoProperties()
            .Create();
        var course = _fixture.Build<Course>()
            .With(x => x.EducationSpecificationId, educationSpecificationId)
            .With(x => x.EducationSpecification, educationSpecification)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfEducationSpecificationInstance?)course.OneOfEducationSpecification;

        // Asserts
        result.Should().NotBeNull();
        result!.Id.Should().Be(educationSpecificationId);
        result.EducationSpecification.Should().Be(educationSpecification);
    }

    [Test]
    public void GetOneOfEducationSpecification_WhenEducationSpecificationIdIsNull_ReturnsNull()
    {
        // Arrange
        Guid? educationSpecificationId = null;
        var course = _fixture.Build<Course>()
            .With(x => x.EducationSpecificationId, educationSpecificationId)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfEducationSpecificationInstance?)course.OneOfEducationSpecification;

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
        var course = _fixture.Build<Course>()
            .With(x => x.Consumers, consumers)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = course.ConsumersList;

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
    }

    [Test]
    public void GetConsumersList_WhenConsumersEmpty_ReturnsEmptyListJObject()
    {
        // Arrange
        var course = _fixture.Build<Course>()
            .With(x => x.Consumers, new List<Consumer>())
            .OmitAutoProperties()
            .Create();

        // Act
        var result = course.ConsumersList;

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
        var course = _fixture.Build<Course>()
            .With(x => x.OrganizationId, organizationId)
            .With(x => x.Organization, organization)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfOrganizationInstance?)course.OneOfOrganization;

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
        var course = _fixture.Build<Course>()
            .With(x => x.OrganizationId, organizationId)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfOrganizationInstance?)course.OneOfOrganization;

        // Assert
        result.Should().BeNull();
    }
}