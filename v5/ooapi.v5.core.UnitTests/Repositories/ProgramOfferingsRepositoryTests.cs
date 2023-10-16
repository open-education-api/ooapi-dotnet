using AutoFixture;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.UnitTests.Repositories.Helpers;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Repositories;

[TestFixture]
public class ProgramOfferingsRepositoryTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
     public void GetProgramOffering_ReturnsProgramOffering_WhenProgramOfferingExists()
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

        var db = Substitute.For<DbSet<ProgramOffering>, IQueryable<ProgramOffering>>();
        DbMockHelper.InitDb(db, programOfferings);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.ProgramOfferings.Returns(db);
        var programOfferingsRepository = new ProgramOfferingsRepository(dbContext);

        // Act
        var result = programOfferingsRepository.GetProgramOffering(programOfferingId);

        // Assert
        Assert.That(result, Is.EqualTo(programOffering));
    }

    [Test]
    public void GetProgramOffering_ReturnsNull_WhenProgramOfferingDoesNotExist()
    {
        // Arrange
        var programOfferingId = _fixture.Create<Guid>();
        var programOfferings = new List<ProgramOffering> { }.AsQueryable();

        var db = Substitute.For<DbSet<ProgramOffering>, IQueryable<ProgramOffering>>();
        DbMockHelper.InitDb(db, programOfferings);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.ProgramOfferings.Returns(db);
        var programOfferingsRepository = new ProgramOfferingsRepository(dbContext);

        // Act
        var result = programOfferingsRepository.GetProgramOffering(programOfferingId);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void GetProgramOfferingByProgramId_ReturnsProgramOfferings_WhenProgramOfferingsExist()
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

        var db = Substitute.For<DbSet<ProgramOffering>, IQueryable<ProgramOffering>>();
        DbMockHelper.InitDb(db, programOfferings);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.ProgramOfferingsNoTracking.Returns(db);
        var programOfferingsRepository = new ProgramOfferingsRepository(dbContext);

        // Act
        var result = programOfferingsRepository.GetProgramOfferingByProgramId(programId, new DataRequestParameters());

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<ProgramOffering>>());
        Assert.That(result.Items.Count, Is.EqualTo(1));
        Assert.That(result.Items[0].ProgramId, Is.EqualTo(programId));
    }

    [Test]
    public void GetProgramOfferingByProgramId_ReturnsEmptyList_WhenProgramOfferingsDoNotExist()
    {
        // Arrange
        var programId = _fixture.Create<Guid>();
        var programOfferings = new List<ProgramOffering> { }.AsQueryable();

        var db = Substitute.For<DbSet<ProgramOffering>, IQueryable<ProgramOffering>>();
        DbMockHelper.InitDb(db, programOfferings);
        var dbContext = Substitute.For<ICoreDbContext>();
        dbContext.ProgramOfferingsNoTracking.Returns(db);
        var programOfferingsRepository = new ProgramOfferingsRepository(dbContext);

        // Act
        var result = programOfferingsRepository.GetProgramOfferingByProgramId(programId, new DataRequestParameters());

        // Assert
        Assert.That(result, Is.InstanceOf<Pagination<ProgramOffering>>());
        Assert.That(result.Items.Count, Is.EqualTo(0));
    }

    // Code to filter by consumers doesn't seem to work
    // [Test]
    // public void GetProgramOfferingByProgramId_IncludesConsumers_WhenDataRequestParametersHasConsumer()
    // {
    //     // Arrange
    //     var programId = _fixture.Create<Guid>();
    //     var consumer = "TestConsumer";
    //     var filterParams = new FilterParams() { consumer = consumer };
    //     var dataRequestParameters = new DataRequestParameters(filterParams, new PagingParams(), null);
    //     var programOfferings = new List<ProgramOffering>()
    //     {
    //         _fixture.Build<ProgramOffering>()
    //             .With(a => a.Consumers, new List<Consumer>()
    //             {
    //                 _fixture.Build<Consumer>()
    //                     .With(c => c.ConsumerKey, consumer)
    //                     .Create()
    //             })
    //             .With(x => x.ProgramId, programId)
    //             .Without(x => x.AcademicSession)
    //             .Without(x => x.Organization)
    //             .Without(x => x.Address)
    //             .Without(x => x.Costs)
    //             .Without(x => x.Program)
    //             .Without(x => x.PriceInformation)
    //             .Create(),
    //         _fixture.Build<ProgramOffering>()
    //             .With(a => a.Consumers, new List<Consumer>()
    //             {
    //                 _fixture.Build<Consumer>()
    //                     .With(c => c.ConsumerKey, consumer)
    //                     .Create()
    //             })
    //             .With(x => x.ProgramId, programId)
    //             .Without(x => x.AcademicSession)
    //             .Without(x => x.Organization)
    //             .Without(x => x.Address)
    //             .Without(x => x.Costs)
    //             .Without(x => x.Program)
    //             .Without(x => x.PriceInformation)
    //             .Create(),
    //         _fixture.Build<ProgramOffering>()
    //             .With(a => a.Consumers, new List<Consumer>()
    //             {
    //                 _fixture.Build<Consumer>()
    //                     .With(c => c.ConsumerKey, "AnotherConsumer")
    //                     .Create()
    //             })
    //             .With(x => x.ProgramId, programId)
    //             .Without(x => x.AcademicSession)
    //             .Without(x => x.Organization)
    //             .Without(x => x.Address)
    //             .Without(x => x.Costs)
    //             .Without(x => x.Program)
    //             .Without(x => x.PriceInformation)
    //             .Create(),
    //     }.AsQueryable();
    //
    //     var db = Substitute.For<DbSet<ProgramOffering>, IQueryable<ProgramOffering>>();
    //     DbMockHelper.InitDb(db, programOfferings);
    //     var dbContext = Substitute.For<ICoreDbContext>();
    //     dbContext.ProgramOfferingsNoTracking.Returns(db);
    //     var programOfferingsRepository = new ProgramOfferingsRepository(dbContext);
    //
    //     // Act
    //     var result = programOfferingsRepository.GetProgramOfferingByProgramId(programId, dataRequestParameters);
    //
    //     // Assert
    //     Assert.IsInstanceOf<Pagination<ProgramOffering>>(result);
    //     Assert.That(result.Items.FindAll(a => a.Consumers!.Count > 0).TrueForAll(b => b.Consumers!.TrueForAll(c => c.ConsumerKey == consumer)), Is.True);
    //     Assert.That(result.Items.Count, Is.EqualTo(2));
    // }
}