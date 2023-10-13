using Microsoft.AspNetCore.Mvc;
using ooapi.v5.Controllers;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.Models;

namespace ooapi.v5.UnitTests.Controllers;

public class ComponentsControllerTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void ComponentsComponentIdGet_Success_ReturnsComponent()
    {
        //arrange
        var sut = CreateSut(out var associationsService);
        var componentId = _fixture.Create<Guid>();
        var expand = _fixture.Create<List<string>>();

        var response = new Component();

        associationsService.Get(componentId, out var errorResponse).Returns(response);

        //act
        var result = sut.ComponentsComponentIdGet(componentId, expand) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        errorResponse.Should().BeNull();

        var component = result.Value as Component;
        component.Should().NotBeNull();
        component.Should().Be(response);
    }

    private static ComponentsController CreateSut(out IComponentsService componentsService)
    {
        componentsService = Substitute.For<IComponentsService>();
        return new ComponentsController(componentsService);
    }
}