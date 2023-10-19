using AutoFixture;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.UnitTests.Repositories.Helpers;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class ProgramsRepositoryTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void GetAllOrderedBy_WithDataRequestParameters_ReturnsSet()
    {
        // Arrange
        var programs = new List<Program>()
        {
            _fixture.Build<Program>()
                .Without(x => x.Duration)
                .Without(x => x.LearningOutcomes)
                .Without(x => x.EducationSpecification)
                .Without(x => x.Addresses)
                .Without(x => x.Address)
                .Without(x => x.Parent)
                .Without(x => x.Children)
                .Without(x => x.Coordinators)
                .Without(x => x.CoordinatorsRef)
                .Without(x => x.Organization)
                .Create(),
            _fixture.Build<Program>()
                .Without(x => x.Duration)
                .Without(x => x.LearningOutcomes)
                .Without(x => x.EducationSpecification)
                .Without(x => x.Addresses)
                .Without(x => x.Address)
                .Without(x => x.Parent)
                .Without(x => x.Children)
                .Without(x => x.Coordinators)
                .Without(x => x.CoordinatorsRef)
                .Without(x => x.Organization)
                .Create(),
            _fixture.Build<Program>()
                .Without(x => x.Duration)
                .Without(x => x.LearningOutcomes)
                .Without(x => x.EducationSpecification)
                .Without(x => x.Addresses)
                .Without(x => x.Address)
                .Without(x => x.Parent)
                .Without(x => x.Children)
                .Without(x => x.Coordinators)
                .Without(x => x.CoordinatorsRef)
                .Without(x => x.Organization)
                .Create()
        }.AsQueryable();
        var db = Substitute.For<DbSet<Program>, IQueryable<Program>>();
        DbMockHelper.InitDb(db, programs);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.ProgramsNoTracking.Returns(db);
        var programsRepository = new ProgramsRepository(dbContext);

        // Act
        var result = programsRepository.GetAllOrderedBy(new DataRequestParameters());

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Program>>());
        Assert.That(result.Items, Has.Count.EqualTo(3));
    }

        [Test]
    public void GetProgramsByEducationSpecificationId_WithEducationSpecificationId_ReturnsSetByEducationSpecificationId()
    {
        // Arrange
        var educationSpecificationId = _fixture.Create<Guid>();

        var programs = new List<Program>()
        {
            _fixture.Build<Program>()
                .With(x => x.EducationSpecificationId, educationSpecificationId)
                .Without(x => x.Duration)
                .Without(x => x.LearningOutcomes)
                .Without(x => x.EducationSpecification)
                .Without(x => x.Addresses)
                .Without(x => x.Address)
                .Without(x => x.Parent)
                .Without(x => x.Children)
                .Without(x => x.Coordinators)
                .Without(x => x.CoordinatorsRef)
                .Without(x => x.Organization)
                .Create(),
            _fixture.Build<Program>()
                .With(x => x.EducationSpecificationId, educationSpecificationId)
                .Without(x => x.Duration)
                .Without(x => x.LearningOutcomes)
                .Without(x => x.EducationSpecification)
                .Without(x => x.Addresses)
                .Without(x => x.Address)
                .Without(x => x.Parent)
                .Without(x => x.Children)
                .Without(x => x.Coordinators)
                .Without(x => x.CoordinatorsRef)
                .Without(x => x.Organization)
                .Create(),
            _fixture.Build<Program>()
                .With(x => x.EducationSpecificationId, _fixture.Create<Guid>())
                .Without(x => x.Duration)
                .Without(x => x.LearningOutcomes)
                .Without(x => x.EducationSpecification)
                .Without(x => x.Addresses)
                .Without(x => x.Address)
                .Without(x => x.Parent)
                .Without(x => x.Children)
                .Without(x => x.Coordinators)
                .Without(x => x.CoordinatorsRef)
                .Without(x => x.Organization)
                .Create()
        }.AsQueryable();
        var db = Substitute.For<DbSet<Program>, IQueryable<Program>>();
        DbMockHelper.InitDb(db, programs);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.ProgramsNoTracking.Returns(db);
        var programsRepository = new ProgramsRepository(dbContext);

        // Act
        var result = programsRepository.GetProgramsByEducationSpecificationId(educationSpecificationId, new DataRequestParameters());

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<Program>>());
        Assert.That(result.Items, Has.Count.EqualTo(2));
    }

    [Test]
    public void GetProgramsByOrganizationId_WithOrganizationId_ReturnsSetByOrganizationId()
    {
        // Arrange
        var organizationId = _fixture.Create<Guid>();

        var programs = new List<Program>()
        {
            _fixture.Build<Program>()
                .With(x => x.OrganizationId, organizationId)
                .Without(x => x.Duration)
                .Without(x => x.LearningOutcomes)
                .Without(x => x.EducationSpecification)
                .Without(x => x.Addresses)
                .Without(x => x.Address)
                .Without(x => x.Parent)
                .Without(x => x.Children)
                .Without(x => x.Coordinators)
                .Without(x => x.CoordinatorsRef)
                .Without(x => x.Organization)
                .Create(),
            _fixture.Build<Program>()
                .With(x => x.OrganizationId, organizationId)
                .Without(x => x.Duration)
                .Without(x => x.LearningOutcomes)
                .Without(x => x.EducationSpecification)
                .Without(x => x.Addresses)
                .Without(x => x.Address)
                .Without(x => x.Parent)
                .Without(x => x.Children)
                .Without(x => x.Coordinators)
                .Without(x => x.CoordinatorsRef)
                .Without(x => x.Organization)
                .Create(),
            _fixture.Build<Program>()
                .With(x => x.OrganizationId, _fixture.Create<Guid>())
                .Without(x => x.Duration)
                .Without(x => x.LearningOutcomes)
                .Without(x => x.EducationSpecification)
                .Without(x => x.Addresses)
                .Without(x => x.Address)
                .Without(x => x.Parent)
                .Without(x => x.Children)
                .Without(x => x.Coordinators)
                .Without(x => x.CoordinatorsRef)
                .Without(x => x.Organization)
                .Create()
        }.AsQueryable();
        var db = Substitute.For<DbSet<Program>, IQueryable<Program>>();
        DbMockHelper.InitDb(db, programs);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Programs.Returns(db);
        var programsRepository = new ProgramsRepository(dbContext);

        // Act
        var result = programsRepository.GetProgramsByOrganizationId(organizationId);

        // Assert
        Assert.That(result, Is.InstanceOf<List<Program>>());
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public void GetProgramsByProgramId_WithProgramId_ReturnsSetByProgramId()
    {
        // Arrange
        var programId = _fixture.Create<Guid>();

        var programs = new List<Program>()
        {
            _fixture.Build<Program>()
                .With(x => x.ParentId, programId)
                .Without(x => x.Duration)
                .Without(x => x.LearningOutcomes)
                .Without(x => x.EducationSpecification)
                .Without(x => x.Addresses)
                .Without(x => x.Address)
                .Without(x => x.Parent)
                .Without(x => x.Children)
                .Without(x => x.Coordinators)
                .Without(x => x.CoordinatorsRef)
                .Without(x => x.Organization)
                .Create(),
            _fixture.Build<Program>()
                .With(x => x.ParentId, programId)
                .Without(x => x.Duration)
                .Without(x => x.LearningOutcomes)
                .Without(x => x.EducationSpecification)
                .Without(x => x.Addresses)
                .Without(x => x.Address)
                .Without(x => x.Parent)
                .Without(x => x.Children)
                .Without(x => x.Coordinators)
                .Without(x => x.CoordinatorsRef)
                .Without(x => x.Organization)
                .Create(),
            _fixture.Build<Program>()
                .With(x => x.ParentId, _fixture.Create<Guid>())
                .Without(x => x.Duration)
                .Without(x => x.LearningOutcomes)
                .Without(x => x.EducationSpecification)
                .Without(x => x.Addresses)
                .Without(x => x.Address)
                .Without(x => x.Parent)
                .Without(x => x.Children)
                .Without(x => x.Coordinators)
                .Without(x => x.CoordinatorsRef)
                .Without(x => x.Organization)
                .Create()
        }.AsQueryable();
        var db = Substitute.For<DbSet<Program>, IQueryable<Program>>();
        DbMockHelper.InitDb(db, programs);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.Programs.Returns(db);
        var programsRepository = new ProgramsRepository(dbContext);

        // Act
        var result = programsRepository.GetProgramsByProgramId(programId);

        // Assert
        Assert.That(result, Is.InstanceOf<List<Program>>());
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public void GetProgram_WithProgramId_ReturnsProgram()
    {
        // Arrange
        var programId = _fixture.Create<Guid>();

        var programs = new List<Program>()
        {
            _fixture.Build<Program>()
                .With(x => x.ProgramId, programId)
                .Without(x => x.Duration)
                .Without(x => x.LearningOutcomes)
                .Without(x => x.EducationSpecification)
                .Without(x => x.Addresses)
                .Without(x => x.Address)
                .Without(x => x.Parent)
                .Without(x => x.Children)
                .Without(x => x.Coordinators)
                .Without(x => x.CoordinatorsRef)
                .Without(x => x.Organization)
                .Create(),
        }.AsQueryable();
        var db = Substitute.For<DbSet<Program>, IQueryable<Program>>();
        DbMockHelper.InitDb(db, programs);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.ProgramsNoTracking.Returns(db);
        var programsRepository = new ProgramsRepository(dbContext);

        // Act
        var result = programsRepository.GetProgram(programId, new DataRequestParameters());

        // Assert
        Assert.That(result, Is.InstanceOf<Program>());
        Assert.That(result!.ProgramId, Is.EqualTo(programId));
    }

    [Test]
    public void GetProgram_WhenExpandParentRequestedViaRequestParameters_ReturnsProgramAndParent()
    {
        // Arrange
        var programId = _fixture.Create<Guid>();
        var parentProgramId = _fixture.Create<Guid>();
        var program = _fixture.Build<Program>()
            .With(x => x.ProgramId, programId)
            .With(x => x.ParentId, parentProgramId)
            .Without(x => x.Duration)
            .Without(x => x.LearningOutcomes)
            .Without(x => x.EducationSpecification)
            .Without(x => x.Addresses)
            .Without(x => x.Address)
            .Without(x => x.Parent)
            .Without(x => x.Children)
            .Without(x => x.Coordinators)
            .Without(x => x.CoordinatorsRef)
            .Without(x => x.Organization)
            .Create();
        var parentProgram = _fixture.Build<Program>()
            .With(x => x.ProgramId, parentProgramId)
            .Without(x => x.Duration)
            .Without(x => x.LearningOutcomes)
            .Without(x => x.EducationSpecification)
            .Without(x => x.Addresses)
            .Without(x => x.Address)
            .Without(x => x.Parent)
            .Without(x => x.Children)
            .Without(x => x.Coordinators)
            .Without(x => x.CoordinatorsRef)
            .Without(x => x.Organization)
            .Create();
        var programs = new List<Program> { program, parentProgram }.AsQueryable();

        var db = Substitute.For<DbSet<Program>, IQueryable<Program>>();
        DbMockHelper.InitDb(db, programs);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.ProgramsNoTracking.Returns(db);
        var programsRepository = new ProgramsRepository(dbContext);

        var requestParameters = new DataRequestParameters() { Expand = { "Parent" } };

        // Act
        var result = programsRepository.GetProgram(programId, requestParameters);

        // Assert
        Assert.That(result, Is.InstanceOf<Program>());
        Assert.That(result.ProgramId, Is.EqualTo(programId));
        Assert.That(result.Parent, Is.InstanceOf<Program>());
        Assert.That(result.Parent.ProgramId, Is.EqualTo(parentProgramId));
        Assert.That(result.Parent.ChildrenIds.Contains(programId), Is.True);
    }

    [Test]
    public void GetProgram_WhenExpandChildrenRequestedViaRequestParameters_ReturnsProgramAndChildren()
    {
        // Arrange
        var programId = _fixture.Create<Guid>();
        var firstChildProgramId = _fixture.Create<Guid>();
        var secondChildProgramId = _fixture.Create<Guid>();
        var program = _fixture.Build<Program>()
            .With(x => x.ProgramId, programId)
            .Without(x => x.Duration)
            .Without(x => x.LearningOutcomes)
            .Without(x => x.EducationSpecification)
            .Without(x => x.Addresses)
            .Without(x => x.Address)
            .Without(x => x.Parent)
            .Without(x => x.Children)
            .Without(x => x.Coordinators)
            .Without(x => x.CoordinatorsRef)
            .Without(x => x.Organization)
            .Create();
        var firstChildProgram = _fixture.Build<Program>()
            .With(x => x.ProgramId, firstChildProgramId)
            .With(x => x.ParentId, programId)
            .Without(x => x.Duration)
            .Without(x => x.LearningOutcomes)
            .Without(x => x.EducationSpecification)
            .Without(x => x.Addresses)
            .Without(x => x.Address)
            .Without(x => x.Parent)
            .Without(x => x.Children)
            .Without(x => x.Coordinators)
            .Without(x => x.CoordinatorsRef)
            .Without(x => x.Organization)
            .Create();
        var secondChildProgram = _fixture.Build<Program>()
            .With(x => x.ProgramId, secondChildProgramId)
            .With(x => x.ParentId, programId)
            .Without(x => x.Duration)
            .Without(x => x.LearningOutcomes)
            .Without(x => x.EducationSpecification)
            .Without(x => x.Addresses)
            .Without(x => x.Address)
            .Without(x => x.Parent)
            .Without(x => x.Children)
            .Without(x => x.Coordinators)
            .Without(x => x.CoordinatorsRef)
            .Without(x => x.Organization)
            .Create();
        var programs = new List<Program> { program, firstChildProgram, secondChildProgram }.AsQueryable();

        var db = Substitute.For<DbSet<Program>, IQueryable<Program>>();
        DbMockHelper.InitDb(db, programs);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.ProgramsNoTracking.Returns(db);
        var programsRepository = new ProgramsRepository(dbContext);

        var requestParameters = new DataRequestParameters() { Expand = { "Children" } };

        // Act
        var result = programsRepository.GetProgram(programId, requestParameters);

        // Assert
        Assert.That(result, Is.InstanceOf<Program>());
        Assert.Multiple(() =>
        {
            Assert.That(result.ProgramId, Is.EqualTo(programId));
            Assert.That(result.Children, Is.InstanceOf<List<Program>>());
            Assert.That(result.ChildrenIds, Does.Contain(firstChildProgramId));
            Assert.That(result.Children, Does.Contain(firstChildProgram));
            Assert.That(result.ChildrenIds, Does.Contain(secondChildProgramId));
            Assert.That(result.Children, Does.Contain(secondChildProgram));
        });
    }

    [Test]
    public void GetProgram_WhenExpandOrganizationRequestedViaRequestParameters_ReturnsProgramAndOrganization()
    {
        // Arrange
        var programId = _fixture.Create<Guid>();
        var organizationId = _fixture.Create<Guid>();
        var program = _fixture.Build<Program>()
            .With(x => x.ProgramId, programId)
            .With(x => x.OrganizationId, organizationId)
            .Without(x => x.Duration)
            .Without(x => x.LearningOutcomes)
            .Without(x => x.EducationSpecification)
            .Without(x => x.Addresses)
            .Without(x => x.Address)
            .Without(x => x.Parent)
            .Without(x => x.Children)
            .Without(x => x.Coordinators)
            .Without(x => x.CoordinatorsRef)
            .Without(x => x.Organization)
            .Create();
        var programs = new List<Program> { program }.AsQueryable();

        var organization = _fixture.Build<Organization>()
            .With(x => x.OrganizationId, organizationId)
            .Without(x => x.Addresses)
            .Without(x => x.Address)
            .Without(x => x.Parent)
            .Without(x => x.Children)
            .Create();
        var organizations = new List<Organization>() { organization }.AsQueryable();

        var db = Substitute.For<DbSet<Program>, IQueryable<Program>>();
        DbMockHelper.InitDb(db, programs);
        var orgDb = Substitute.For<DbSet<Organization>, IQueryable<Organization>>();
        DbMockHelper.InitDb(orgDb, organizations);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.ProgramsNoTracking.Returns(db);
        dbContext.OrganizationsNoTracking.Returns(orgDb);
        var programsRepository = new ProgramsRepository(dbContext);

        var requestParameters = new DataRequestParameters() { Expand = { "Organization" } };

        // Act
        var result = programsRepository.GetProgram(programId, requestParameters);

        // Assert
        Assert.That(result, Is.InstanceOf<Program>());
        Assert.Multiple(() =>
        {
            Assert.That(result.ProgramId, Is.EqualTo(programId));
            Assert.That(result.Organization!.OrganizationId, Is.EqualTo(organization.OrganizationId));
        });
    }

    [Test]
    public void GetProgram_WhenExpandEducationSpecificationRequestedViaRequestParameters_ReturnsProgramAndEducationSpecification()
    {
        // Arrange
        var programId = _fixture.Create<Guid>();
        var educationSpecificationId = _fixture.Create<Guid>();
        var program = _fixture.Build<Program>()
            .With(x => x.ProgramId, programId)
            .With(x => x.EducationSpecificationId, educationSpecificationId)
            .Without(x => x.Duration)
            .Without(x => x.LearningOutcomes)
            .Without(x => x.EducationSpecification)
            .Without(x => x.Addresses)
            .Without(x => x.Address)
            .Without(x => x.Parent)
            .Without(x => x.Children)
            .Without(x => x.Coordinators)
            .Without(x => x.CoordinatorsRef)
            .Without(x => x.Organization)
            .Create();
        var programs = new List<Program> { program }.AsQueryable();

        var educationSpecification = _fixture.Build<EducationSpecification>()
            .With(x => x.EducationSpecificationId, educationSpecificationId)
            .Without(x => x.LearningOutcomes)
            .Without(x => x.Parent)
            .Without(x => x.Children)
            .Without(x => x.Organization)
            .Create();
        var educationSpecifications = new List<EducationSpecification>() { educationSpecification }.AsQueryable();

        var db = Substitute.For<DbSet<Program>, IQueryable<Program>>();
        DbMockHelper.InitDb(db, programs);
        var orgDb = Substitute.For<DbSet<EducationSpecification>, IQueryable<EducationSpecification>>();
        DbMockHelper.InitDb(orgDb, educationSpecifications);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.ProgramsNoTracking.Returns(db);
        dbContext.EducationSpecificationsNoTracking.Returns(orgDb);
        var programsRepository = new ProgramsRepository(dbContext);

        var requestParameters = new DataRequestParameters() { Expand = { "EducationSpecification" } };

        // Act
        var result = programsRepository.GetProgram(programId, requestParameters);

        // Assert
        Assert.That(result, Is.InstanceOf<Program>());
        Assert.Multiple(() =>
        {
            Assert.That(result.ProgramId, Is.EqualTo(programId));
            Assert.That(result.EducationSpecification!.EducationSpecificationId, Is.EqualTo(educationSpecificationId));
        });
    }
}