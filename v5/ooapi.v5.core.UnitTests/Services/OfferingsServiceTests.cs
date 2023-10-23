using AutoFixture;
using NSubstitute;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Services;

[TestFixture]
public class OfferingsServiceTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void Get_WhenExistingProgramOffering_CallsOnlyProgramOfferingRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var programOfferingsRepository = Substitute.For<IProgramOfferingsRepository>();
        var courseOfferingsRepository = Substitute.For<ICourseOfferingsRepository>();
        var componentOfferingsRepository = Substitute.For<IComponentOfferingsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new OfferingsService(
            dbContext,
            userRequestContext,
            programOfferingsRepository,
            courseOfferingsRepository,
            componentOfferingsRepository);
        var offeringId = _fixture.Create<Guid>();

        var programOffering = _fixture.Build<ProgramOffering>()
            .With(x => x.OfferingId, offeringId)
            .OmitAutoProperties()
            .Create();
        programOfferingsRepository.GetProgramOffering(offeringId).Returns(programOffering);

        // Act
        var result = sut.Get(offeringId);

        // Assert
        Assert.That(result, Is.InstanceOf<OneOfOfferingInstance>());
        Assert.That(result!.ProgramOffering, Is.EqualTo(programOffering));
        programOfferingsRepository.Received(1).GetProgramOffering(offeringId);
        componentOfferingsRepository.DidNotReceiveWithAnyArgs().GetComponentOffering(offeringId);
        courseOfferingsRepository.DidNotReceiveWithAnyArgs().GetCourseOffering(offeringId);
    }

    [Test]
    public void Get_WhenExistingComponentOfferingAndNoProgramOffering_CallsComponentOfferingRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var programOfferingsRepository = Substitute.For<IProgramOfferingsRepository>();
        var courseOfferingsRepository = Substitute.For<ICourseOfferingsRepository>();
        var componentOfferingsRepository = Substitute.For<IComponentOfferingsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new OfferingsService(
            dbContext,
            userRequestContext,
            programOfferingsRepository,
            courseOfferingsRepository,
            componentOfferingsRepository);
        var offeringId = _fixture.Create<Guid>();

        var componentOffering = _fixture.Build<ComponentOffering>()
            .With(x => x.OfferingId, offeringId)
            .OmitAutoProperties()
            .Create();
        componentOfferingsRepository.GetComponentOffering(offeringId).Returns(componentOffering);

        // Act
        var result = sut.Get(offeringId);

        // Assert
        Assert.That(result, Is.InstanceOf<OneOfOfferingInstance>());
        Assert.That(result!.ComponentOffering, Is.EqualTo(componentOffering));
        programOfferingsRepository.Received(1).GetProgramOffering(offeringId);
        componentOfferingsRepository.Received(1).GetComponentOffering(offeringId);
        courseOfferingsRepository.DidNotReceiveWithAnyArgs().GetCourseOffering(offeringId);
    }

    [Test]
    public void Get_WhenExistingCourseOfferingAndNoComponentOfferingAndNoProgramOffering_CallsCourseOfferingRepository()
    {
        // Arrange
        var dbContext = Substitute.For<ICoreDbContext>();
        var programOfferingsRepository = Substitute.For<IProgramOfferingsRepository>();
        var courseOfferingsRepository = Substitute.For<ICourseOfferingsRepository>();
        var componentOfferingsRepository = Substitute.For<IComponentOfferingsRepository>();
        var userRequestContext = Substitute.For<IUserRequestContext>();
        var sut = new OfferingsService(
            dbContext,
            userRequestContext,
            programOfferingsRepository,
            courseOfferingsRepository,
            componentOfferingsRepository);
        var offeringId = _fixture.Create<Guid>();

        var courseOffering = _fixture.Build<CourseOffering>()
            .With(x => x.OfferingId, offeringId)
            .OmitAutoProperties()
            .Create();
        courseOfferingsRepository.GetCourseOffering(offeringId).Returns(courseOffering);

        // Act
        var result = sut.Get(offeringId);

        // Assert
        Assert.That(result, Is.InstanceOf<OneOfOfferingInstance>());
        Assert.That(result!.CourseOffering, Is.EqualTo(courseOffering));
        programOfferingsRepository.Received(1).GetProgramOffering(offeringId);
        componentOfferingsRepository.Received(1).GetComponentOffering(offeringId);
        courseOfferingsRepository.Received(1).GetCourseOffering(offeringId);

    }
}