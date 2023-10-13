using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ooapi.v5.Controllers;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.Models;

namespace ooapi.v5.UnitTests.Controllers;

public class AssociationsControllerTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void AssociationsAssociationIdGet_Success_ReturnsAssociation()
    {
        //arrange
        var sut = CreateSut(out var associationsService);
        var associationId = _fixture.Create<Guid>();
        var expand = _fixture.Create<List<string>>();

        var response = new Association();

        associationsService.Get(associationId, out var errorResponse).Returns(response);

        //act
        var result = sut.AssociationsAssociationIdGet(associationId, expand) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        errorResponse.Should().BeNull();

        var association = result.Value as Association;
        association.Should().NotBeNull();
        association.Should().Be(response);
    }

    private static AssociationsController CreateSut(out IAssociationsService associationsService)
    {
        associationsService = Substitute.For<IAssociationsService>();
        return new AssociationsController(associationsService);
    }
}