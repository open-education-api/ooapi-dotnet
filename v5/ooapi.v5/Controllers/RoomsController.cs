using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ooapi.v5.Attributes;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Services;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;
using ooapi.v5.Models.Params;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ooapi.v5.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
public class RoomsController : BaseController
{
    public RoomsController(IConfiguration configuration, CoreDBContext dbContext) : base(configuration, dbContext)
    {
    }

    /// <summary>
    /// GET /rooms
    /// </summary>
    /// <remarks>Get a list of all rooms, ordered by name (ascending).</remarks>
    /// <param name="primaryCodeParam"></param>
    /// <param name="filterParams"></param>
    /// <param name="pagingParams"></param>
    /// <param name="roomType">Filter by room type</param>
    /// <param name="sort">
    ///Default: ["name"]<br/>
    ///Items Enum: "roomId" "name" "totalSeats" "availableSeats" "-roomId" "-name" "-totalSeats" "-availableSeats"<br/>
    ///Example: sort=name,-availableSeats<br/>
    /// Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("rooms")]
    [ValidateModelState]
    [SwaggerOperation("RoomsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Rooms), description: "OK")]
    public virtual IActionResult RoomsGet([FromQuery] PrimaryCodeParam primaryCodeParam, [FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] string roomType, [FromQuery] string sort)
    {
        DataRequestParameters parameters = new DataRequestParameters(primaryCodeParam, filterParams, pagingParams, sort);
        var service = new RoomsService(DBContext, UserRequestContext);
        var result = service.GetAll(parameters, out ErrorResponse errorResponse);
        if (result == null)
        {
            return BadRequest(errorResponse);
        }
        return Ok(result);
    }

    /// <summary>
    /// GET /rooms/{roomId}
    /// </summary>
    /// <remarks>Get a single room.</remarks>
    /// <param name="roomId">Room ID</param>
    /// <param name="expand">Optional properties to expand, separated by a comma</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("rooms/{roomId}")]
    [ValidateModelState]
    [SwaggerOperation("RoomsRoomIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Room), description: "OK")]
    public virtual IActionResult RoomsRoomIdGet([FromRoute][Required] Guid roomId, [FromQuery] List<string> expand)
    {
        var service = new RoomsService(DBContext, UserRequestContext);
        var result = service.Get(roomId, out ErrorResponse errorResponse);
        if (result == null)
        {
            return BadRequest(errorResponse);
        }
        return Ok(result);
    }
}
