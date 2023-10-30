using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ooapi.v5.Attributes;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;
using ooapi.v5.Models.Params;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace ooapi.v5.Controllers;

/// <summary>
/// API calls for buildings
/// </summary>
[ApiController]
public class BuildingsController : BaseController
{
    private readonly IBuildingsService _buildingsService;
    private readonly IRoomsService _roomsService;

    /// <summary>
    /// Resolves the required services
    /// </summary>
    /// <param name="buildingsService"></param>
    /// <param name="roomsService"></param>
    public BuildingsController(IBuildingsService buildingsService, IRoomsService roomsService)
    {
        _buildingsService = buildingsService;
        _roomsService = roomsService;
    }

    /// <summary>
    /// GET /buildings/{buildingId}
    /// </summary>
    /// <remarks>Get a single building.</remarks>
    /// <param name="buildingId">Building ID</param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("buildings/{buildingId}")]
    [ValidateModelState]
    [SwaggerOperation("BuildingsBuildingIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Building), description: "OK")]
    public virtual async Task<IActionResult> BuildingsBuildingIdGetAsync([FromRoute][Required] Guid buildingId, CancellationToken cancellationToken = default)
    {
        var result = await _buildingsService.GetAsync(buildingId, cancellationToken);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);

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

    ///  <summary>
    ///  GET /buildings/{buildingId}/rooms
    ///  </summary>
    ///  <remarks>Get a list of all rooms in a building.</remarks>
    ///  <param name="buildingId">The id of the building to find rooms for</param>
    ///  <param name="filterParams"></param>
    ///  <param name="pagingParams"></param>
    ///  <param name="roomType">Filter by room type</param>
    ///  <param name="sort">
    /// Default: ["name"]<br/>
    /// Items Enum: "roomId" "name" "totalSeats" "availableSeats" "-roomId" "-name" "-totalSeats" "-availableSeats"<br/>
    /// Example: sort=name,-availableSeats<br/>
    /// Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    ///  <param name="cancellationToken"></param>
    ///  <response code="200">OK</response>
    [HttpGet]
    [Route("buildings/{buildingId}/rooms")]
    [ValidateModelState]
    [SwaggerOperation("BuildingsBuildingIdRoomsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Rooms), description: "OK")]
    public virtual async Task<IActionResult> BuildingsBuildingIdRoomsGetAsync(
        [FromRoute][Required] Guid buildingId,
        [FromQuery] FilterParams filterParams,
        [FromQuery] PagingParams pagingParams,
        [FromQuery] string? roomType,
        [FromQuery] string? sort = "name",
        CancellationToken cancellationToken = default)
    {
        var parameters = new DataRequestParameters(filterParams, pagingParams, sort);
        var result = await _roomsService.GetRoomsByBuildingIdAsync(parameters, buildingId, cancellationToken);
        return Ok(result);
    }

    ///  <summary>
    ///  GET /buildings
    ///  </summary>
    ///  <remarks>Get a list of all buildings, ordered by name (ascending).</remarks>
    ///  <param name="primaryCodeParam"></param>
    ///  <param name="filterParams"></param>
    ///  <param name="pagingParams"></param>
    ///  <param name="sort">
    /// Default: ["name"]<br/>
    /// Items Enum: "buildingId" "name" "-buildingId" "-name"<br/>
    /// Example: sort=name,-buildingId<br/>
    /// Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    ///  <param name="cancellationToken"></param>
    ///  <response code="200">OK</response>
    [HttpGet]
    [Route("buildings")]
    [ValidateModelState]
    [SwaggerOperation("BuildingsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Buildings), description: "OK")]
    public virtual async Task<IActionResult> BuildingsGetAsync(
        [FromQuery] PrimaryCodeParam? primaryCodeParam,
        [FromQuery] FilterParams? filterParams,
        [FromQuery] PagingParams? pagingParams,
        [FromQuery] string? sort = "name",
        CancellationToken cancellationToken = default)
    {
        var parameters = new DataRequestParameters(primaryCodeParam, filterParams, pagingParams, sort);
        var result = await _buildingsService.GetAllAsync(parameters, cancellationToken);
        return Ok(result);
    }
}