using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Enums;
using ooapi.v5.Models;
using Attribute = ooapi.v5.Models.Attribute;

namespace ooapi.v5.core.UnitTests.Models;

public sealed class ProgramTests
{
    private readonly Fixture _fixture = new Fixture();

    [TestCase("TestType", "TestCode", true)]
    [TestCase("TestType", null, false)]
    [TestCase(null, "TestCode", false)]
    [TestCase(null, null, false)]
    public void GetPrimaryCodeIdentifier_ReturnsIdentifierEntryOrNull(string? primaryCodeType, string? primaryCode, bool resultIsNotNull)
    {
        // Arrange
        var model = _fixture.Build<Program>()
            .With(x => x.PrimaryCodeType, primaryCodeType)
            .With(x => x.PrimaryCode, primaryCode)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = model.PrimaryCodeIdentifier;

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
        var model = _fixture.Build<Program>()
            .OmitAutoProperties()
            .Create();

        var primaryCodeIdentifier = new IdentifierEntry() { CodeType = "TestType", Code = "TestCode" };

        // Act
        model.PrimaryCodeIdentifier = primaryCodeIdentifier;

        // Assert
        model.PrimaryCodeType.Should().Be(primaryCodeIdentifier.CodeType);
        model.PrimaryCode.Should().Be(primaryCodeIdentifier.Code);
    }

    [Test]
    public void SetPrimaryCodeIdentifier_WhenIdentifierEntryIsNull_DoesNotSetPrimaryCodeAndPrimaryCodeType()
    {
        // Arrange
        var codeType = "TestType";
        var code = "TestCode";
        var model = _fixture.Build<Program>()
            .With(x => x.PrimaryCodeType, codeType)
            .With(x => x.PrimaryCode, code)
            .OmitAutoProperties()
            .Create();

        IdentifierEntry? primaryCodeIdentifier = null;

        // Act
        model.PrimaryCodeIdentifier = primaryCodeIdentifier;

        // Assert
        model.PrimaryCodeType.Should().Be(codeType);
        model.PrimaryCode.Should().Be(code);
    }

    [Test]
    public void GetName_WhenAttributesExist_ReturnsListLanguageTypedString()
    {
        // Arrange
        var program = _fixture.Build<Program>()
            .With(x => x.Attributes, new List<LanguageTypedProperty>() { new() { PropertyName = "name", Language = "en", Value = "TestName" } })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = program.name;

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
        var program = _fixture.Build<Program>()
            .With(x => x.Attributes, new List<LanguageTypedProperty>())
            .OmitAutoProperties()
            .Create();

        // Act
        var result = program.name;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(0);
    }

    [Test]
    public void GetDescription_WhenAttributesExist_ReturnsListLanguageTypedString()
    {
        // Arrange
        var program = _fixture.Build<Program>()
            .With(x => x.Attributes, new List<LanguageTypedProperty>() { new() { PropertyName = "description", Language = "en", Value = "TestName" } })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = program.description;

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
        var program = _fixture.Build<Program>()
            .With(x => x.Attributes, new List<LanguageTypedProperty>())
            .OmitAutoProperties()
            .Create();

        // Act
        var result = program.description;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(0);
    }

    [TestCase(null, 0)]
    [TestCase(null, 1)]
    [TestCase("", -1)]
    [TestCase("test", 0)]
    public void GetStudyLoad_GivenEmptyStudyLoadUnitAndStudyLoad_ReturnsNull(string? studyLoadUnit, int studyLoad)
    {
        // arrange
        var model = new Program {StudyLoadUnit = studyLoadUnit, StudyLoadValue = studyLoad};

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
        var program = new Program { StudyLoad = studyLoadDescriptor };
        var result = program.StudyLoad;

        // assert
        result.Should().NotBeNull();
        result!.StudyLoadUnit.Should().Be(program.StudyLoadUnit);
        result.Value.Should().Be(program.StudyLoadValue);
    }

    [TestCase("distance-learning", ModeOfDelivery.distance_learning)]
    [TestCase("on campus", ModeOfDelivery.on_campus)]
    [TestCase("online", ModeOfDelivery.online)]
    [TestCase("hybrid", ModeOfDelivery.hybrid)]
    [TestCase("situated", ModeOfDelivery.situated)]
    [TestCase("", null)]
    public void GetModeOfDel_ReturnsListModeOfDelivery(string modeOfDelivery, ModeOfDelivery? expectedModeOfDelivery)
    {
        // Arrange
        var program = _fixture.Build<Program>()
            .With(x => x.ModeOfDelivery, modeOfDelivery)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = program.ModeOfDel;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<ModeOfDelivery>>();
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
    public void GetEnrollment_WhenAttributesExist_ReturnsListLanguageTypedString()
    {
        // Arrange
        var program = _fixture.Build<Program>()
            .With(x => x.Attributes, new List<LanguageTypedProperty>() { new() { PropertyName = "enrollment", Language = "en", Value = "TestName" } })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = program.enrollment;

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
        var program = _fixture.Build<Program>()
            .With(x => x.Attributes, new List<LanguageTypedProperty>())
            .OmitAutoProperties()
            .Create();

        // Act
        var result = program.enrollment;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(0);
    }

    [Test]
    public void GetAssessment_WhenAttributesExist_ReturnsListLanguageTypedString()
    {
        // Arrange
        var program = _fixture.Build<Program>()
            .With(x => x.Attributes, new List<LanguageTypedProperty>() { new() { PropertyName = "assessment", Language = "en", Value = "TestName" } })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = program.assessment;

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
        var program = _fixture.Build<Program>()
            .With(x => x.Attributes, new List<LanguageTypedProperty>())
            .OmitAutoProperties()
            .Create();

        // Act
        var result = program.assessment;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(0);
    }

    [Test]
    public void GetAdmissionRequirements_WhenAttributesExist_ReturnsListLanguageTypedString()
    {
        // Arrange
        var program = _fixture.Build<Program>()
            .With(x => x.Attributes, new List<LanguageTypedProperty>() { new() { PropertyName = "admissionRequirements", Language = "en", Value = "TestName" } })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = program.admissionRequirements;

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
        var program = _fixture.Build<Program>()
            .With(x => x.Attributes, new List<LanguageTypedProperty>())
            .OmitAutoProperties()
            .Create();

        // Act
        var result = program.admissionRequirements;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(0);
    }

    [Test]
    public void GetQualificationRequirements_WhenAttributesExist_ReturnsListLanguageTypedString()
    {
        // Arrange
        var program = _fixture.Build<Program>()
            .With(x => x.Attributes, new List<LanguageTypedProperty >() { new() { PropertyName = "qualificationRequirements", Language = "en", Value = "TestName" } })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = program.qualificationRequirements;

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
        var program = _fixture.Build<Program>()
            .With(x => x.Attributes, new List<LanguageTypedProperty>())
            .OmitAutoProperties()
            .Create();

        // Act
        var result = program.qualificationRequirements;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(0);
    }

    [Test]
    public void GetOneOfEducationSpecification_ReturnsOneOfEducationSpecification()
    {
        // Arrange
        var id = _fixture.Create<Guid>();
        var oneOfModel = _fixture.Build<EducationSpecification>()
            .With(x => x.EducationSpecificationId, id)
            .OmitAutoProperties()
            .Create();
        var model = _fixture.Build<Program>()
            .With(x => x.EducationSpecificationId, id)
            .With(x => x.EducationSpecification, oneOfModel)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = model.OneOfEducationSpecification;

        // Assert
        result.Should().NotBeNull();
        ((OneOfEducationSpecificationInstance?)result)?.EducationSpecification?.EducationSpecificationId.Should().Be(id);
        ((OneOfEducationSpecificationInstance?)result)?.Id.Should().Be(id);
    }

    [Test]
    public void GetOneOfEducationSpecification_WhenIdIsNull_ReturnsNull()
    {
        // Arrange
        Guid? id = null;
        var oneOfModel = _fixture.Build<EducationSpecification>()
            .With(x => x.EducationSpecificationId, id)
            .OmitAutoProperties()
            .Create();
        var model = _fixture.Build<Program>()
            .With(x => x.EducationSpecificationId, id)
            .With(x => x.EducationSpecification, oneOfModel)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = model.OneOfEducationSpecification;

        // Assert
        result.Should().BeNull();
    }

    [Test]
    public void GetOneOfParent_ReturnsOneOfParent()
    {
        // Arrange
        var id = _fixture.Create<Guid>();
        var oneOfModel = _fixture.Build<Program>()
            .With(x => x.ParentId, id)
            .OmitAutoProperties()
            .Create();
        var model = _fixture.Build<Program>()
            .With(x => x.ParentId, id)
            .With(x => x.Parent, oneOfModel)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = model.OneOfParent;

        // Assert
        result.Should().NotBeNull();
        ((OneOfProgramInstance?)result)?.Program?.ParentId.Should().Be(id);
        ((OneOfProgramInstance?)result)?.Id.Should().Be(id);
    }

    [Test]
    public void GetOneOfParent_WhenIdIsNull_ReturnsNull()
    {
        // Arrange
        Guid? id = null;
        var oneOfModel = _fixture.Build<Program>()
            .With(x => x.EducationSpecificationId, id)
            .OmitAutoProperties()
            .Create();
        var model = _fixture.Build<Program>()
            .With(x => x.ParentId, id)
            .With(x => x.Parent, oneOfModel)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = model.OneOfParent;

        // Assert
        result.Should().BeNull();
    }

    [Test]
    public void GetChildrenList_ReturnsListOneOfProgram()
    {
        // Arrange
        var childrenIds = _fixture.CreateMany<Guid>(1).ToList();
        var children = _fixture.Build<Program>()
            .With(x => x.ProgramId, childrenIds[0])
            .OmitAutoProperties()
            .CreateMany(1)
            .ToList();
        var academicSession = _fixture.Build<Program>()
            .With(x => x.ChildrenIds, childrenIds)
            .With(x => x.Children, children)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = academicSession.ChildrenList;

        // Assert
        result.Should().NotBeNull();
        ((OneOfProgramInstance)result[0]).Program.Should().Be(children[0]);
    }

    [Test]
    public void GetChildrenList_WhenChildrenIdsEmpty_ReturnsEmptyListOneOfProgram()
    {
        // Arrange
        var childrenIds = new List<Guid>();
        var children = _fixture.Build<Program>()
            .OmitAutoProperties()
            .CreateMany(1)
            .ToList();
        var academicSession = _fixture.Build<Program>()
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
    public void GetOneOfOrganization_ReturnsOneOfOrganization()
    {
        // Arrange
        var id = _fixture.Create<Guid>();
        var oneOfModel = _fixture.Build<Organization>()
            .With(x => x.OrganizationId, id)
            .OmitAutoProperties()
            .Create();
        var model = _fixture.Build<Program>()
            .With(x => x.OrganizationId, id)
            .With(x => x.Organization, oneOfModel)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = model.OneOfOrganization;

        // Assert
        result.Should().NotBeNull();
        ((OneOfOrganizationInstance?)result)?.Organization?.OrganizationId.Should().Be(id);
        ((OneOfOrganizationInstance?)result)?.Id.Should().Be(id);
    }

    [Test]
    public void GetOneOfOrganization_WhenIdIsNull_ReturnsNull()
    {
        // Arrange
        Guid? id = null;
        var oneOfModel = _fixture.Build<Organization>()
            .With(x => x.OrganizationId, id)
            .OmitAutoProperties()
            .Create();
        var model = _fixture.Build<Program>()
            .With(x => x.OrganizationId, id)
            .With(x => x.Organization, oneOfModel)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = model.OneOfOrganization;

        // Assert
        result.Should().BeNull();
    }

    [Test]
    public void ConsumersList_ReturnsListOfJObjects()
    {
        // arrange
        var consumers = _fixture.Build<ConsumerBase>().With(x => x.PropertyType, ConsumerPropertyType.String).CreateMany().ToList();
        var program = _fixture.Build<Program>().With(x => x.Consumers, consumers).OmitAutoProperties().Create();
        program.Consumers.Should().NotBeEmpty();

        // act
        var result = program.ConsumersList;

        // assert
        result.Should().NotBeNullOrEmpty().And.HaveCount(program.Consumers.Count);
        result.Should().ContainItemsAssignableTo<JObject>();
    }

    [Test]
    public void ConsumersList_NoConsumers_ReturnsEmptyList()
    {
        // arrange
        var program = _fixture.Build<Program>().With(x => x.Consumers, new List<ConsumerBase>()).OmitAutoProperties().Create();
        program.Consumers.Should().BeEmpty();

        // act
        var result = program.ConsumersList;

        // assert
        result.Should().BeEmpty();
    }
}