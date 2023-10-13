using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ooapi.v5.Controllers;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.Models;

namespace ooapi.v5.UnitTests.Controllers;

public class OfferingsControllerTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void OfferingsOfferingIdGet_SuccessWithCourseOffering_ReturnsCourseOffering()
    {
        //arrange
        var sut = CreateSut(out var offeringsService);
        var offeringId = _fixture.Create<Guid>();
        var courseOffering = new CourseOffering();
        var expand = _fixture.Create<List<string>>();

        var response = new OneOfOfferingInstance(offeringId, courseOffering);

        offeringsService.Get(offeringId, out var errorResponse).Returns(response);

        //act
        var result = sut.OfferingsOfferingIdGet(offeringId, expand) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        errorResponse.Should().BeNull();

        var offering = result.Value as CourseOffering;
        offering.Should().NotBeNull();
        offering.Should().Be(response.CourseOffering);
    }

    [Test]
    public void OfferingsOfferingIdGet_SuccessWithComponentOffering_ReturnsComponentOffering()
    {
        //arrange
        var sut = CreateSut(out var offeringsService);
        var offeringId = _fixture.Create<Guid>();
        var componentOffering = new ComponentOffering();
        var expand = _fixture.Create<List<string>>();

        var response = new OneOfOfferingInstance(offeringId, componentOffering);

        offeringsService.Get(offeringId, out var errorResponse).Returns(response);

        //act
        var result = sut.OfferingsOfferingIdGet(offeringId, expand) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        errorResponse.Should().BeNull();

        var offering = result.Value as ComponentOffering;
        offering.Should().NotBeNull();
        offering.Should().Be(response.ComponentOffering);
    }

    [Test]
    public void OfferingsOfferingIdGet_SuccessWithProgramOffering_ReturnsProgramOffering()
    {
        //arrange
        var sut = CreateSut(out var offeringsService);
        var offeringId = _fixture.Create<Guid>();
        var programOffering = new ProgramOffering();
        var expand = _fixture.Create<List<string>>();

        var response = new OneOfOfferingInstance(offeringId, programOffering);

        offeringsService.Get(offeringId, out var errorResponse).Returns(response);

        //act
        var result = sut.OfferingsOfferingIdGet(offeringId, expand) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        errorResponse.Should().BeNull();

        var offering = result.Value as ProgramOffering;
        offering.Should().NotBeNull();
        offering.Should().Be(response.ProgramOffering);
    }

    private static OfferingsController CreateSut(out IOfferingsService offeringsService)
    {
        offeringsService = Substitute.For<IOfferingsService>();
        return new OfferingsController(offeringsService);
    }
}