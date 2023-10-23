using AutoFixture;
using FluentAssertions;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Models;
using Attribute = ooapi.v5.Models.Attribute;

namespace ooapi.v5.core.UnitTests.Models;

[TestFixture]
public class OfferingTests
{
    private readonly IFixture _fixture = new Fixture();
    
    [TestCase("TestType", "TestCode", true)]
    [TestCase("TestType", null, false)]
    [TestCase(null, "TestCode", false)]
    [TestCase(null, null, false)]
    public void GetPrimaryCodeIdentifier_ReturnsIdentifierEntryOrNull(string? primaryCodeType, string? primaryCode, bool resultIsNotNull)
    {
        // Arrange
        var offering = _fixture.Build<Offering>()
            .With(x => x.PrimaryCodeType, primaryCodeType)
            .With(x => x.PrimaryCode, primaryCode)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = offering.PrimaryCodeIdentifier;

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
        var offering = _fixture.Build<Offering>()
            .OmitAutoProperties()
            .Create();

        var primaryCodeIdentifier = new IdentifierEntry() { CodeType = "TestType", Code = "TestCode" };

        // Act
        offering.PrimaryCodeIdentifier = primaryCodeIdentifier;

        // Assert
        offering.PrimaryCodeType.Should().Be(primaryCodeIdentifier.CodeType);
        offering.PrimaryCode.Should().Be(primaryCodeIdentifier.Code);
    }

    [Test]
    public void SetPrimaryCodeIdentifier_WhenIdentifierEntryIsNull_DoesNotSetPrimaryCodeAndPrimaryCodeType()
    {
        // Arrange
        var codeType = "TestType";
        var code = "TestCode";
        var offering = _fixture.Build<Offering>()
            .With(x => x.PrimaryCodeType, codeType)
            .With(x => x.PrimaryCode, code)
            .OmitAutoProperties()
            .Create();

        IdentifierEntry? primaryCodeIdentifier = null;

        // Act
        offering.PrimaryCodeIdentifier = primaryCodeIdentifier;

        // Assert
        offering.PrimaryCodeType.Should().Be(codeType);
        offering.PrimaryCode.Should().Be(code);
    }

    [Test]
    public void GetOneOfAcademicSession_ReturnsOneOfAcademicSessionInstance()
    {
        // Arrange
        var academicSessionId = _fixture.Create<Guid>();
        var academicSession = _fixture.Build<AcademicSession>()
            .With(x => x.AcademicSessionId, academicSessionId)
            .OmitAutoProperties()
            .Create();
        var offering = _fixture.Build<Offering>()
            .With(x => x.AcademicSessionId, academicSessionId)
            .With(x => x.AcademicSession, academicSession)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfAcademicSessionInstance?)offering.OneOfAcademicSession;

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(academicSessionId);
        result.AcademicSession.Should().Be(academicSession);
    }

    [Test]
    public void GetOneOfAcademicSession_WhenAcademicSessionIdIsNull_ReturnsNull()
    {
        // Arrange
        Guid? academicSesionId = null;
        var offering = _fixture.Build<Offering>()
            .With(x => x.AcademicSessionId, academicSesionId)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfAcademicSessionInstance?)offering.OneOfAcademicSession;

        // Assert
        result.Should().BeNull();
    }

    [Test]
    public void GetName_WhenAttributesExist_ReturnsListLanguageTypedString()
    {
        // Arrange
        var offering = _fixture.Build<Offering>()
            .With(x => x.Attributes, new List<Attribute>() { new() { PropertyName = "name", Language = "en", Value = "TestName" } })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = offering.name;

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
        var offering = _fixture.Build<Offering>()
            .With(x => x.Attributes, new List<Attribute>() { })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = offering.name;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(0);
    }
    
    [Test]
    public void GetDescription_WhenAttributesExist_ReturnsListLanguageTypedString()
    {
        // Arrange
        var offering = _fixture.Build<Offering>()
            .With(x => x.Attributes, new List<Attribute>() { new() { PropertyName = "description", Language = "en", Value = "TestName" } })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = offering.description;

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
        var offering = _fixture.Build<Offering>()
            .With(x => x.Attributes, new List<Attribute>() { })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = offering.description;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(0);
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
        var offering = _fixture.Build<Offering>()
            .With(x => x.ModeOfDelivery, modeOfDelivery)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = offering.ModeOfDel;

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
    public void GetOneOfOrganization_ReturnsOneOfOrganizationInstance()
    {
        // Arrange
        var organizationId = _fixture.Create<Guid>();
        var organization = _fixture.Build<Organization>()
            .With(x => x.OrganizationId, organizationId)
            .OmitAutoProperties()
            .Create();
        var offering = _fixture.Build<Offering>()
            .With(x => x.OrganizationId, organizationId)
            .With(x => x.Organization, organization)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfOrganizationInstance?)offering.OneOfOrganization;

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
        var offering = _fixture.Build<Offering>()
            .With(x => x.OrganizationId, organizationId)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfOrganizationInstance?)offering.OneOfOrganization;

        // Assert
        result.Should().BeNull();
    }
}