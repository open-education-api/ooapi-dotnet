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
using System.ComponentModel.DataAnnotations;

namespace ooapi.v5.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
public class BuildingsController : BaseController
{
    public BuildingsController(IConfiguration configuration, CoreDBContext dbContext) : base(configuration, dbContext)
    {
    }

    /// <summary>
    /// GET /buildings/{buildingId}
    /// </summary>
    /// <remarks>Get a single building.</remarks>
    /// <param name="buildingId">Building ID</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("buildings/{buildingId}")]
    [ValidateModelState]
    [SwaggerOperation("BuildingsBuildingIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Building), description: "OK")]
    public virtual IActionResult BuildingsBuildingIdGet([FromRoute][Required] Guid buildingId)
    {
        var service = new BuildingsService(DBContext, UserRequestContext);
        var result = service.Get(buildingId, out ErrorResponse errorResponse);
        if (result == null)
        {
            return BadRequest(errorResponse);
        }
        return Ok(result);

        //var service = new BuildingsService(DBContext, userRequestContext);
        //var result = service.Get(buildingId);

        //if (result.PaginationMetadata.CurrentPageSize.Equals(0))
        //{
        //    return NotFound(new ServiceErrorResult<Building>(404, "Not found.", result));
        //}

        //return null;  //Ok(result);

        //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(200, default(InlineResponse20026));

        //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(400, default(InlineResponse400));

        //TODO: Uncomment the next line to return response 401 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(401, default(InlineResponse400));

        //TODO: Uncomment the next line to return response 403 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(403, default(InlineResponse400));

        //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(404, default(InlineResponse400));

        //TODO: Uncomment the next line to return response 405 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(405, default(InlineResponse400));

        //TODO: Uncomment the next line to return response 429 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(429, default(InlineResponse400));

        //TODO: Uncomment the next line to return response 500 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(500, default(InlineResponse400));
    }

    /// <summary>
    /// GET /buildings/{buildingId}/rooms
    /// </summary>
    /// <remarks>Get a list of all rooms in a building.</remarks>
    /// <param name="buildingId">The id of the building to find rooms for</param>
    /// <param name="filterParams"></param>
    /// <param name="pagingParams"></param>
    /// <param name="roomType">Filter by room type</param>
    /// <param name="sort">
    ///Default: ["name"]<br/>
    ///Items Enum: "roomId" "name" "totalSeats" "availableSeats" "-roomId" "-name" "-totalSeats" "-availableSeats"<br/>
    ///Example: sort=name,-availableSeats<br/>
    ///Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("buildings/{buildingId}/rooms")]
    [ValidateModelState]
    [SwaggerOperation("BuildingsBuildingIdRoomsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Rooms), description: "OK")]
    public virtual IActionResult BuildingsBuildingIdRoomsGet([FromRoute][Required] Guid buildingId, [FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] string? roomType, [FromQuery] string? sort = "name")
    {
        DataRequestParameters parameters = new DataRequestParameters(filterParams, pagingParams, sort);
        var service = new RoomsService(DBContext, UserRequestContext);
        var result = service.GetRoomsByBuildingId(parameters, buildingId, out ErrorResponse errorResponse);
        if (result == null)
        {
            return BadRequest(errorResponse);
        }
        return Ok(result);
    }

    /// <summary>
    /// GET /buildings
    /// </summary>
    /// <remarks>Get a list of all buildings, ordered by name (ascending).</remarks>
    /// <param name="primaryCodeParam"></param>
    /// <param name="filterParams"></param>
    /// <param name="pagingParams"></param>
    /// <param name="sort"> 
    ///Default: ["name"]<br/>
    ///Items Enum: "buildingId" "name" "-buildingId" "-name"<br/>
    ///Example: sort=name,-buildingId<br/>
    ///Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("buildings")]
    [ValidateModelState]
    [SwaggerOperation("BuildingsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Buildings), description: "OK")]
    public virtual IActionResult BuildingsGet([FromQuery] PrimaryCodeParam primaryCodeParam, [FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] string? sort = "name")
    {
        DataRequestParameters parameters = new DataRequestParameters(primaryCodeParam, filterParams, pagingParams, sort);
        var service = new BuildingsService(DBContext, UserRequestContext);
        var result = service.GetAll(parameters, out ErrorResponse errorResponse);
        if (result == null)
        {
            return BadRequest(errorResponse);
        }
        return Ok(result);
    }
}
