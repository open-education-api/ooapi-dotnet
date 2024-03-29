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
/// API calls for groups
/// </summary>
[ApiController]
public class GroupsController : BaseController
{
    private readonly IGroupsService _groupsService;
    private readonly IPersonsService _personsService;

    /// <summary>
    /// Resolves the required services
    /// </summary>
    /// <param name="groupsService"></param>
    /// <param name="personsService"></param>
    public GroupsController(IGroupsService groupsService, IPersonsService personsService)
    {
        _groupsService = groupsService;
        _personsService = personsService;
    }

    ///  <summary>
    ///  GET /groups
    ///  </summary>
    ///  <remarks>Get a list of all groups, ordered by name (ascending).</remarks>
    ///  <param name="primaryCodeParam"></param>
    ///  <param name="filterParams"></param>
    ///  <param name="pagingParams"></param>
    ///  <param name="groupType">Filter by group type</param>
    ///  <param name="sort">
    /// Default: ["name"]<br/>
    /// Items Enum: "groupId" "name" "startDate" "-groupId" "-name" "-startDate"<br/>
    /// Example: sort=groupId,-startDate<br/>
    ///  Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    ///  <param name="cancellationToken"></param>
    ///  <response code="200">OK</response>
    [HttpGet]
    [Route("groups")]
    [ValidateModelState]
    [SwaggerOperation("GroupsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Groups), description: "OK")]
    public virtual async Task<IActionResult> GroupsGetAsync(
        [FromQuery] PrimaryCodeParam? primaryCodeParam,
        [FromQuery] FilterParams? filterParams,
        [FromQuery] PagingParams? pagingParams,
        [FromQuery] string? groupType,
        [FromQuery] string? sort = "name",
        CancellationToken cancellationToken = default)
    {
        var parameters = new DataRequestParameters(primaryCodeParam, filterParams, pagingParams, sort);
        var result = await _groupsService.GetAllAsync(parameters, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// GET /groups/{groupId}
    /// </summary>
    /// <remarks>Get a single group.</remarks>
    /// <param name="groupId">Group ID</param>
    /// <param name="expand">Items Value: "organization" <br/>Optional properties to expand, separated by a comma</param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("groups/{groupId}")]
    [ValidateModelState]
    [SwaggerOperation("GroupsGroupIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Group), description: "OK")]
    public virtual async Task<IActionResult> GroupsGroupIdGetAsync(
        [FromRoute][Required] Guid groupId,
        [FromQuery] List<string> expand,
        CancellationToken cancellationToken = default)
    {
        var result = await _groupsService.GetAsync(groupId, cancellationToken);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    ///  <summary>
    ///  GET /groups/{groupId}/persons
    ///  </summary>
    ///  <remarks>Get an ordered list of all persons that are member of a given group, ordered by personId.</remarks>
    ///  <param name="groupId">Group ID</param>
    ///  <param name="filterParams"></param>
    ///  <param name="pagingParams"></param>
    ///  <param name="affiliations">Filter by affiliations</param>
    ///  <param name="sort">
    /// Default: ["personId"]<br/>
    /// Items Enum: "personId" "givenName" "surName" "displayName" "-personId" "-givenName" "-surName" "-displayName"<br/>
    /// Example: sort=personId,-givenName<br/>
    ///  Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    ///  <param name="cancellationToken"></param>
    ///  <response code="200">OK</response>
    [HttpGet]
    [Route("groups/{groupId}/persons")]
    [ValidateModelState]
    [SwaggerOperation("GroupsGroupIdPersonsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Persons), description: "OK")]
    public virtual async Task<IActionResult> GroupsGroupIdPersonsGetAsync(
        [FromRoute][Required] Guid groupId,
        [FromQuery] FilterParams filterParams,
        [FromQuery] PagingParams pagingParams,
        [FromQuery] List<string>? affiliations,
        [FromQuery] string? sort = "personId",
        CancellationToken cancellationToken = default)
    {
        var parameters = new DataRequestParameters(filterParams, pagingParams, sort);
        var result = await _personsService.GetPersonsByGroupIdAsync(parameters, groupId, cancellationToken);
        return Ok(result);
    }
}