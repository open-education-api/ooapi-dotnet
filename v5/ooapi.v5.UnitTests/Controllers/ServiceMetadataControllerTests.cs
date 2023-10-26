using Microsoft.AspNetCore.Mvc;
using ooapi.v5.Controllers;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.Models;

namespace ooapi.v5.UnitTests.Controllers;

public class ServiceMetadataControllerTests
{
    [Test]
    public async Task RootGet_Success_ReturnsService()
    {
        //arrange
        var sut = CreateSut(out var serviceMetadataService);

        var response = new Service();

        serviceMetadataService.GetAsync(Arg.Any<CancellationToken>()).Returns(response);

        //act
        var result = await sut.RootGetAsync() as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        var service = result.Value as Service;
        service.Should().NotBeNull();
        service.Should().Be(response);
    }

    private static ServiceMetadataController CreateSut(out IServiceMetadataService serviceMetadataService)
    {
        serviceMetadataService = Substitute.For<IServiceMetadataService>();
        return new ServiceMetadataController(serviceMetadataService);
    }
}