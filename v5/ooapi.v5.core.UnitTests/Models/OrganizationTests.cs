using AutoFixture;
using FluentAssertions;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Models;
using Attribute = ooapi.v5.Models.Attribute;

namespace ooapi.v5.core.UnitTests.Models;

[TestFixture]
public class OrganizationTests
{
    private readonly IFixture _fixture = new Fixture();
    
    [TestCase("TestType", "TestCode", true)]
    [TestCase("TestType", null, false)]
    [TestCase(null, "TestCode", false)]
    [TestCase(null, null, false)]
    public void GetPrimaryCodeIdentifier_ReturnsIdentifierEntryOrNull(string? primaryCodeType, string? primaryCode, bool resultIsNotNull)
    {
        // Arrange
        var organization = _fixture.Build<Organization>()
            .With(x => x.PrimaryCodeType, primaryCodeType)
            .With(x => x.PrimaryCode, primaryCode)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = organization.PrimaryCodeIdentifier;

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
        var organization = _fixture.Build<Organization>()
            .OmitAutoProperties()
            .Create();

        var primaryCodeIdentifier = new IdentifierEntry() { CodeType = "TestType", Code = "TestCode" };

        // Act
        organization.PrimaryCodeIdentifier = primaryCodeIdentifier;

        // Assert
        organization.PrimaryCodeType.Should().Be(primaryCodeIdentifier.CodeType);
        organization.PrimaryCode.Should().Be(primaryCodeIdentifier.Code);
    }

    [Test]
    public void SetPrimaryCodeIdentifier_WhenIdentifierEntryIsNull_DoesNotSetPrimaryCodeAndPrimaryCodeType()
    {
        // Arrange
        var codeType = "TestType";
        var code = "TestCode";
        var organization = _fixture.Build<Organization>()
            .With(x => x.PrimaryCodeType, codeType)
            .With(x => x.PrimaryCode, code)
            .OmitAutoProperties()
            .Create();

        IdentifierEntry? primaryCodeIdentifier = null;

        // Act
        organization.PrimaryCodeIdentifier = primaryCodeIdentifier;

        // Assert
        organization.PrimaryCodeType.Should().Be(codeType);
        organization.PrimaryCode.Should().Be(code);
    }
    
    [Test]
    public void GetName_WhenAttributesExist_ReturnsListLanguageTypedString()
    {
        // Arrange
        var organization = _fixture.Build<Organization>()
            .With(x => x.Attributes, new List<Attribute>() { new() { PropertyName = "name", Language = "en", Value = "TestName" } })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = organization.name;

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
        var organization = _fixture.Build<Organization>()
            .With(x => x.Attributes, new List<Attribute>() { })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = organization.name;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(0);
    }

    [Test]
    public void GetDescription_WhenAttributesExist_ReturnsListLanguageTypedString()
    {
        // Arrange
        var organization = _fixture.Build<Organization>()
            .With(x => x.Attributes, new List<Attribute>() { new() { PropertyName = "description", Language = "en", Value = "TestName" } })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = organization.description;

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
        var organization = _fixture.Build<Organization>()
            .With(x => x.Attributes, new List<Attribute>() { })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = organization.description;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(0);
    }
    
    [Test]
    public void GetOneOfOrganization_ReturnsOneOfOrganizationInstance()
    {
        // Arrange
        var parentId = _fixture.Create<Guid>();
        var parent = _fixture.Build<Organization>()
            .With(x => x.OrganizationId, parentId)
            .OmitAutoProperties()
            .Create();
        var organization = _fixture.Build<Organization>()
            .With(x => x.ParentId, parentId)
            .With(x => x.Parent, parent)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfOrganizationInstance?)organization.OneOfOrganization;

        // Asserts
        result.Should().NotBeNull();
        result!.Id.Should().Be(parentId);
        result.Organization.Should().Be(parent);
    }

    [Test]
    public void GetOneOfOrganization_WhenOrganizationIdIsNull_ReturnsNull()
    {
        // Arrange
        Guid? parentId = null;
        var organization = _fixture.Build<Organization>()
            .With(x => x.ParentId, parentId)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = (OneOfOrganizationInstance?)organization.OneOfOrganization;

        // Assert
        result.Should().BeNull();
    }
    
    [Test]
    public void GetChildrenList_ReturnsListOneOfOrganization()
    {
        // Arrange
        var childrenIds = _fixture.CreateMany<Guid>(1).ToList();
        var children = _fixture.Build<Organization>()
            .With(x => x.OrganizationId, childrenIds[0])
            .OmitAutoProperties()
            .CreateMany(1)
            .ToList();
        var organization = _fixture.Build<Organization>()
            .With(x => x.ChildrenIds, childrenIds)
            .With(x => x.Children, children)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = organization.ChildrenList;

        // Assert
        result.Should().NotBeNull();
        ((OneOfOrganizationInstance)result[0]).Organization.Should().Be(children[0]);
    }

    [Test]
    public void GetChildrenList_WhenChildrenIdsEmpty_ReturnsEmptyListOneOfOrganization()
    {
        // Arrange
        var childrenIds = new List<Guid>();
        var children = _fixture.Build<Organization>()
            .OmitAutoProperties()
            .CreateMany(1)
            .ToList();
        var organization = _fixture.Build<Organization>()
            .With(x => x.ChildrenIds, childrenIds)
            .With(x => x.Children, children)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = organization.ChildrenList;

        // Assert
        result.Should().BeEmpty();
    }
    
    [Test]
    public void GetConsumersList_ReturnsListJObject()
    {
        // Arrange
        var consumers = _fixture.Build<Consumer>()
            .CreateMany(1)
            .ToList();
        var organization = _fixture.Build<Organization>()
            .With(x => x.Consumers, consumers)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = organization.ConsumersList;

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
    }

    [Test]
    public void GetConsumersList_WhenConsumersEmpty_ReturnsEmptyListJObject()
    {
        // Arrange
        var organization = _fixture.Build<Organization>()
            .With(x => x.Consumers, new List<Consumer>())
            .OmitAutoProperties()
            .Create();

        // Act
        var result = organization.ConsumersList;

        // Assert
        result.Should().BeEmpty();
    }
}