using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ooapi.v5.Controllers;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;
using ooapi.v5.Models.Params;

namespace ooapi.v5.UnitTests.Controllers;

public class RoomsControllerTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void RoomsGet_ByPrimaryCode_ReturnsRooms()
    {
        //arrange
        var sut = CreateSut(out var roomsService);
        var primaryCodeParam = _fixture.Create<PrimaryCodeParam>();
        FilterParams? filterParams = null;
        PagingParams? pagingParams = null;
        var roomType = _fixture.Create<string?>();
        var sort = _fixture.Create<string?>();

        var response = new Pagination<Room>();

        DataRequestParameters? dataRequestParameters = null;

        roomsService.GetAll(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), out var errorResponse).Returns(response);

        //act
        var result = sut.RoomsGet(primaryCodeParam, filterParams, pagingParams, roomType, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        errorResponse.Should().BeNull();

        var rooms = result.Value as Pagination<Room>;
        rooms.Should().NotBeNull();
        rooms.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.PrimaryCodeSearch.Should().Be(primaryCodeParam.primaryCode);
    }

    [Test]
    public void RoomsGet_ByFilterParams_ReturnsRooms()
    {
        //arrange
        var sut = CreateSut(out var roomsService);
        PrimaryCodeParam? primaryCodeParam = null;
        var filterParams = _fixture.Create<FilterParams>();
        var pagingParams = _fixture.Create<PagingParams>();
        var roomType = _fixture.Create<string?>();
        var sort = _fixture.Create<string?>();

        var response = new Pagination<Room>();

        DataRequestParameters? dataRequestParameters = null;

        roomsService.GetAll(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), out var errorResponse).Returns(response);

        //act
        var result = sut.RoomsGet(primaryCodeParam, filterParams, pagingParams, roomType, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        errorResponse.Should().BeNull();

        var rooms = result.Value as Pagination<Room>;
        rooms.Should().NotBeNull();
        rooms.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.SearchTerm.Should().Be(filterParams.q);
        dataRequestParameters.Consumer.Should().Be(filterParams.consumer);
        dataRequestParameters.PageNumber.Should().Be(pagingParams.PageNumber);
    }

    [Test]
    public void RoomsRoomIdGet_Success_ReturnsRoom()
    {
        //arrange
        var sut = CreateSut(out var associationsService);
        var roomId = _fixture.Create<Guid>();
        var expand = _fixture.Create<List<string>>();

        var response = new Room();

        associationsService.Get(roomId, out var errorResponse).Returns(response);

        //act
        var result = sut.RoomsRoomIdGet(roomId, expand) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        errorResponse.Should().BeNull();

        var room = result.Value as Room;
        room.Should().NotBeNull();
        room.Should().Be(response);
    }

    private static RoomsController CreateSut(out IRoomsService roomsService)
    {
        roomsService = Substitute.For<IRoomsService>();
        return new RoomsController(roomsService);
    }
}