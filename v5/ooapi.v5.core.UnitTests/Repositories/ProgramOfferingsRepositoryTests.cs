using AutoFixture;
using MockQueryable.NSubstitute;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class ProgramOfferingsRepositoryTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
     public async Task GetProgramOffering_WhenProgramOfferingExists_ReturnsProgramOffering()
    {
        // Arrange
        var programOfferingId = _fixture.Create<Guid>();
        var programOffering = _fixture.Build<ProgramOffering>()
            .With(x => x.OfferingId, programOfferingId)
            .Without(x => x.AcademicSession)
            .Without(x => x.Organization)
            .Without(x => x.Address)
            .Without(x => x.Costs)
            .Without(x => x.Program)
            .Without(x => x.PriceInformation)
            .Create();
        var programOfferings = new List<ProgramOffering> { programOffering }.AsQueryable();

        var db = programOfferings.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.ProgramOfferings.Returns(db);
        var programOfferingsRepository = new ProgramOfferingsRepository(dbContext);

        // Act
        var result = await programOfferingsRepository.GetProgramOfferingAsync(programOfferingId);

        // Assert
        Assert.That(result, Is.EqualTo(programOffering));
    }

    [Test]
    public async Task GetProgramOffering_WhenProgramOfferingDoesNotExist_ReturnsNull()
    {
        // Arrange
        var programOfferingId = _fixture.Create<Guid>();
        var programOfferings = new List<ProgramOffering> { }.AsQueryable();

        var db = programOfferings.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.ProgramOfferings.Returns(db);
        var programOfferingsRepository = new ProgramOfferingsRepository(dbContext);

        // Act
        var result = await programOfferingsRepository.GetProgramOfferingAsync(programOfferingId);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetProgramOfferingByProgramId_WhenProgramOfferingsExist_ReturnsProgramOfferings()
    {
        // Arrange
        var programId = _fixture.Create<Guid>();
        var programOffering = _fixture.Build<ProgramOffering>()
            .With(x => x.ProgramId, programId)
            .Without(x => x.AcademicSession)
            .Without(x => x.Organization)
            .Without(x => x.Address)
            .Without(x => x.Costs)
            .Without(x => x.Program)
            .Without(x => x.PriceInformation)
            .Create();
        var programOfferings = new List<ProgramOffering> { programOffering }.AsQueryable();

        var db = programOfferings.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.ProgramOfferingsNoTracking.Returns(db);
        var programOfferingsRepository = new ProgramOfferingsRepository(dbContext);

        // Act
        var result = await programOfferingsRepository.GetProgramOfferingByProgramIdAsync(programId, new DataRequestParameters());

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<ProgramOffering>>());
        Assert.That(result.Items, Has.Count.EqualTo(1));
        Assert.That(result.Items[0].ProgramId, Is.EqualTo(programId));
    }

    [Test]
    public async Task GetProgramOfferingByProgramId_WhenProgramOfferingsDoNotExist_ReturnsEmptyList()
    {
        // Arrange
        var programId = _fixture.Create<Guid>();
        var programOfferings = new List<ProgramOffering> { }.AsQueryable();

        var db = programOfferings.BuildMockDbSet();
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.ProgramOfferingsNoTracking.Returns(db);
        var programOfferingsRepository = new ProgramOfferingsRepository(dbContext);

        // Act
        var result = await programOfferingsRepository.GetProgramOfferingByProgramIdAsync(programId, new DataRequestParameters());

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<ProgramOffering>>());
        Assert.That(result.Items, Is.Empty);
    }
}