using Microsoft.AspNetCore.Mvc;
using ooapi.v5.Controllers;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;
using ooapi.v5.Models.Params;

namespace ooapi.v5.UnitTests.Controllers;

public class BuildingsControllerTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public async Task BuildingsBuildingIdGet_Success_ReturnsBuilding()
    {
        //arrange
        var sut = CreateSut(out var buildingsService, out var _);
        var buildingId = _fixture.Create<Guid>();

        var response = new Building();

        buildingsService.GetAsync(buildingId, Arg.Any<CancellationToken>()).Returns(response);

        //act
        var result = await sut.BuildingsBuildingIdGetAsync(buildingId) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        var building = result.Value as Building;
        building.Should().NotBeNull();
        building.Should().Be(response);
    }

    [Test]
    public async Task BuildingsBuildingIdRoomsGet_Success_ReturnsRooms()
    {
        //arrange
        var sut = CreateSut(out var _, out var roomsService);
        var buildingId = _fixture.Create<Guid>();
        var filterParams = _fixture.Create<FilterParams>();
        var pagingParams = _fixture.Create<PagingParams>();
        var roomType = _fixture.Create<string?>();
        var sort = _fixture.Create<string?>();

        var response = new Pagination<Room>();

        DataRequestParameters? dataRequestParameters = null;

        roomsService.GetRoomsByBuildingIdAsync(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), buildingId, Arg.Any<CancellationToken>()).Returns(response);

        //act
        var result = await sut.BuildingsBuildingIdRoomsGetAsync(buildingId, filterParams, pagingParams, roomType, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        var rooms = result.Value as Pagination<Room>;
        rooms.Should().NotBeNull();
        rooms.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.SearchTerm.Should().Be(filterParams.q);
        dataRequestParameters.Consumer.Should().Be(filterParams.consumer);
        dataRequestParameters.PageNumber.Should().Be(pagingParams.PageNumber);
    }

    [Test]
    public async Task BuildingsGet_ByPrimaryCode_ReturnsBuildings()
    {
        //arrange
        var sut = CreateSut(out var buildingsService, out var _);
        var primaryCodeParam = _fixture.Create<PrimaryCodeParam>();
        FilterParams? filterParams = null;
        PagingParams? pagingParams = null;
        var sort = _fixture.Create<string?>();

        var response = new Pagination<Building>();

        DataRequestParameters? dataRequestParameters = null;

        buildingsService.GetAllAsync(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), Arg.Any<CancellationToken>()).Returns(response);

        //act
        var result = await sut.BuildingsGetAsync(primaryCodeParam, filterParams, pagingParams, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        var buildings = result.Value as Pagination<Building>;
        buildings.Should().NotBeNull();
        buildings.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.PrimaryCodeSearch.Should().Be(primaryCodeParam.primaryCode);
    }

    [Test]
    public async Task BuildingsGet_ByFilterParams_ReturnsAcademicBuildings()
    {
        //arrange
        var sut = CreateSut(out var buildingsService, out var _);
        PrimaryCodeParam? primaryCodeParam = null;
        var filterParams = _fixture.Create<FilterParams>();
        var pagingParams = _fixture.Create<PagingParams>();
        var sort = _fixture.Create<string?>();

        var response = new Pagination<Building>();

        DataRequestParameters? dataRequestParameters = null;

        buildingsService.GetAllAsync(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), Arg.Any<CancellationToken>()).Returns(response);

        //act
        var result = await sut.BuildingsGetAsync(primaryCodeParam, filterParams, pagingParams, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        var buildings = result.Value as Pagination<Building>;
        buildings.Should().NotBeNull();
        buildings.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.SearchTerm.Should().Be(filterParams.q);
        dataRequestParameters.Consumer.Should().Be(filterParams.consumer);
        dataRequestParameters.PageNumber.Should().Be(pagingParams.PageNumber);
    }

    private static BuildingsController CreateSut(out IBuildingsService buildingsService, out IRoomsService roomsService)
    {
        buildingsService = Substitute.For<IBuildingsService>();
        roomsService = Substitute.For<IRoomsService>();
        return new BuildingsController(buildingsService, roomsService);
    }
}