using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ooapi.v5.Attributes;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;
using ooapi.v5.Models.Params;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace ooapi.v5.Controllers;

/// <summary>
/// API calls for rooms
/// </summary>
[ApiController]
public class RoomsController : BaseController
{
    private readonly IRoomsService _roomsService;

    /// <summary>
    /// Resolves the required services
    /// </summary>
    /// <param name="roomsService"></param>
    public RoomsController(IRoomsService roomsService)
    {
        _roomsService = roomsService;
    }

    ///  <summary>
    ///  GET /rooms
    ///  </summary>
    ///  <remarks>Get a list of all rooms, ordered by name (ascending).</remarks>
    ///  <param name="primaryCodeParam"></param>
    ///  <param name="filterParams"></param>
    ///  <param name="pagingParams"></param>
    ///  <param name="roomType">Filter by room type</param>
    ///  <param name="sort">
    /// Default: ["name"]<br/>
    /// Items Enum: "roomId" "name" "totalSeats" "availableSeats" "-roomId" "-name" "-totalSeats" "-availableSeats"<br/>
    /// Example: sort=name,-availableSeats<br/>
    ///  Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    ///  <param name="cancellationToken"></param>
    ///  <response code="200">OK</response>
    [HttpGet]
    [Route("rooms")]
    [ValidateModelState]
    [SwaggerOperation("RoomsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Rooms), description: "OK")]
    public virtual async Task<IActionResult> RoomsGetAsync(
        [FromQuery] PrimaryCodeParam? primaryCodeParam,
        [FromQuery] FilterParams? filterParams,
        [FromQuery] PagingParams? pagingParams,
        [FromQuery] string? roomType,
        [FromQuery] string? sort = "name",
        CancellationToken cancellationToken = default)
    {
        var parameters = new DataRequestParameters(primaryCodeParam, filterParams, pagingParams, sort);
        var result = await _roomsService.GetAllAsync(parameters, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// GET /rooms/{roomId}
    /// </summary>
    /// <remarks>Get a single room.</remarks>
    /// <param name="roomId">Room ID</param>
    /// <param name="expand">Items Value: "building"  <br/>Optional properties to expand, separated by a comma</param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("rooms/{roomId}")]
    [ValidateModelState]
    [SwaggerOperation("RoomsRoomIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Room), description: "OK")]
    public virtual async Task<IActionResult> RoomsRoomIdGetAsync(
        [FromRoute][Required] Guid roomId,
        [FromQuery] List<string> expand,
        CancellationToken cancellationToken = default)
    {
        var result = await _roomsService.GetAsync(roomId, cancellationToken);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
}