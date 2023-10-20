using AutoFixture;
using FluentAssertions;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Models;
using Attribute = ooapi.v5.Models.Attribute;

namespace ooapi.v5.core.UnitTests.Models;

[TestFixture]
public class ComponentTests
{
    private readonly IFixture _fixture = new Fixture();

[TestCase("TestType", "TestCode", true)]
    [TestCase("TestType", null, false)]
    [TestCase(null, "TestCode", false)]
    [TestCase(null, null, false)]
    public void GetPrimaryCodeIdentifier_ReturnsIdentifierEntryOrNull(string? primaryCodeType, string? primaryCode, bool resultIsNotNull)
    {
        // Arrange
        var component = _fixture.Build<Component>()
            .With(x => x.PrimaryCodeType, primaryCodeType)
            .With(x => x.PrimaryCode, primaryCode)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = component.PrimaryCodeIdentifier;

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
        var component = _fixture.Build<Component>()
            .OmitAutoProperties()
            .Create();

        var primaryCodeIdentifier = new IdentifierEntry() { CodeType = "TestType", Code = "TestCode" };

        // Act
        component.PrimaryCodeIdentifier = primaryCodeIdentifier;

        // Assert
        component.PrimaryCodeType.Should().Be(primaryCodeIdentifier.CodeType);
        component.PrimaryCode.Should().Be(primaryCodeIdentifier.Code);
    }

    [Test]
    public void SetPrimaryCodeIdentifier_WhenIdentifierEntryIsNull_DoesNotSetPrimaryCodeAndPrimaryCodeType()
    {
        // Arrange
        var codeType = "TestType";
        var code = "TestCode";
        var component = _fixture.Build<Component>()
            .With(x => x.PrimaryCodeType, codeType)
            .With(x => x.PrimaryCode, code)
            .OmitAutoProperties()
            .Create();

        IdentifierEntry? primaryCodeIdentifier = null;

        // Act
        component.PrimaryCodeIdentifier = primaryCodeIdentifier;

        // Assert
        component.PrimaryCodeType.Should().Be(codeType);
        component.PrimaryCode.Should().Be(code);
    }

    [Test]
    public void GetName_WhenAttributesExist_ReturnsListLanguageTypedString()
    {
        // Arrange
        var component = _fixture.Build<Component>()
            .With(x => x.Attributes, new List<Attribute>() { new() { PropertyName = "name", Language = "en", Value = "TestName" } })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = component.name;

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
        var component = _fixture.Build<Component>()
            .With(x => x.Attributes, new List<Attribute>() { })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = component.name;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(0);
    }

    [Test]
    public void GetDescription_WhenAttributesExist_ReturnsListLanguageTypedString()
    {
        // Arrange
        var component = _fixture.Build<Component>()
            .With(x => x.Attributes, new List<Attribute>() { new() { PropertyName = "description", Language = "en", Value = "TestDescription" } })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = component.Description;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(1);
        result[0].Language.Should().Be("en");
        result[0].Value.Should().Be("TestDescription");
    }

    [Test]
    public void GetDescription_WhenAttributesAreEmpty_ReturnsEmptyListLanguageTypedString()
    {
        // Arrange
        var component = _fixture.Build<Component>()
            .With(x => x.Attributes, new List<Attribute>() { })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = component.Description;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(0);
    }

    [Test]
    public void GetAssessment_WhenAttributesExist_ReturnsListLanguageTypedString()
    {
        // Arrange
        var component = _fixture.Build<Component>()
            .With(x => x.Attributes, new List<Attribute>() { new() { PropertyName = "assessment", Language = "en", Value = "TestAssessment" } })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = component.assessment;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(1);
        result[0].Language.Should().Be("en");
        result[0].Value.Should().Be("TestAssessment");
    }

    [Test]
    public void GetAssessment_WhenAttributesAreEmpty_ReturnsEmptyListLanguageTypedString()
    {
        // Arrange
        var component = _fixture.Build<Component>()
            .With(x => x.Attributes, new List<Attribute>() { })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = component.assessment;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(0);
    }

    [Test]
    public void GetEnrollment_WhenAttributesExist_ReturnsListLanguageTypedString()
    {
        // Arrange
        var component = _fixture.Build<Component>()
            .With(x => x.Attributes, new List<Attribute>() { new() { PropertyName = "enrollment", Language = "en", Value = "TestEnrollment" } })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = component.enrollment;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(1);
        result[0].Language.Should().Be("en");
        result[0].Value.Should().Be("TestEnrollment");
    }

    [Test]
    public void GetEnrollment_WhenAttributesAreEmpty_ReturnsEmptyListLanguageTypedString()
    {
        // Arrange
        var component = _fixture.Build<Component>()
            .With(x => x.Attributes, new List<Attribute>() { })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = component.enrollment;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(0);
    }
    
    [Test]
    public void GetConsumersList_ReturnsListJObject()
    {
        // Arrange
        var consumers = _fixture.Build<Consumer>()
            .CreateMany(1)
            .ToList();
        var component = _fixture.Build<Component>()
            .With(x => x.Consumers, consumers)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = component.ConsumersList;

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
    }

    [Test]
    public void GetConsumersList_WhenConsumersEmpty_ReturnsEmptyListJObject()
    {
        // Arrange
        var component = _fixture.Build<Component>()
            .With(x => x.Consumers, new List<Consumer>())
            .OmitAutoProperties()
            .Create();

        // Act
        var result = component.ConsumersList;

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
        var component = _fixture.Build<Component>()
            .With(x => x.OrganizationId, organizationId)
            .With(x => x.Organization, organization)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfOrganizationInstance?)component.OneOfOrganization;

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
        var component = _fixture.Build<Component>()
            .With(x => x.OrganizationId, organizationId)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfOrganizationInstance?)component.OneOfOrganization;

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
        var component = _fixture.Build<Component>()
            .With(x => x.CourseId, courseId)
            .With(x => x.Course, course)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfCourseInstance?)component.OneOfCourse;

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
        var component = _fixture.Build<Component>()
            .With(x => x.CourseId, courseId)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfCourseInstance?)component.OneOfCourse;

        // Assert
        result.Should().BeNull();
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
        var component = _fixture.Build<Component>()
            .With(x => x.ModeOfDelivery, modeOfDelivery)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = component.ModeOfDel;

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
}