using AutoFixture;
using NSubstitute;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Services;

[TestFixture]
public class ProgramOfferingsServiceTests
{
    private readonly Fixture _fixture = new Fixture();

    [Test]
    public async Task GetAll_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IProgramOfferingsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new ProgramOfferingsService(dbContext, repository, userRequestContext);
        var dataRequestParameters = new DataRequestParameters();

        var expected = Substitute.For<Pagination<ProgramOffering>>();
        repository.GetAllOrderedByAsync(dataRequestParameters).Returns(expected);

        // Act
        var result = await sut.GetAllAsync(dataRequestParameters);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        await repository.Received(1).GetAllOrderedByAsync(dataRequestParameters);
    }
    
    [Test]
    public async Task Get_CallsRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var repository = Substitute.For<IProgramOfferingsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new ProgramOfferingsService(dbContext, repository, userRequestContext);
        var programOfferingId = _fixture.Create<Guid>();

        var expected = new ProgramOffering();
        repository.GetProgramOfferingAsync(programOfferingId).Returns(expected);

        // Act
        var result = await sut.GetAsync(programOfferingId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        await repository.Received(1).GetProgramOfferingAsync(programOfferingId);
    }
}