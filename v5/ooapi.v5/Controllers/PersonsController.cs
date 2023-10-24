using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ooapi.v5.Attributes;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;
using ooapi.v5.Models.Params;
using ooapi.v5.Security;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ooapi.v5.Controllers;

/// <summary>
/// API calls for persons
/// </summary>
[ApiController]
public class PersonsController : BaseController
{
    private readonly IPersonsService _personsService;
    private readonly IAssociationsService _associationsService;
    private readonly IGroupsService _groupsService;

    /// <summary>
    /// Resolves the required services
    /// </summary>
    /// <param name="personsService"></param>
    /// <param name="associationsService"></param>
    /// <param name="groupsService"></param>
    public PersonsController(IPersonsService personsService, IAssociationsService associationsService, IGroupsService groupsService)
    {
        _personsService = personsService;
        _associationsService = associationsService;
        _groupsService = groupsService;
    }

    /// <summary>
    /// GET /persons
    /// </summary>
    /// <remarks>Get an ordered list of all persons.</remarks>
    /// <param name="primaryCodeParam"></param>
    /// <param name="filterParams"></param>
    /// <param name="pagingParams"></param>
    /// <param name="affiliations">Filter by affiliations</param>
    /// <param name="sort">
    ///Default: ["personId"]<br/>
    ///Items Enum: "personId" "givenName" "surName" "displayName" "-personId" "-givenName" "-surName" "-displayName"<br/>
    ///Example: sort=surName,-personId<br/>
    /// Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("persons")]
    [ValidateModelState]
    [SwaggerOperation("PersonsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Persons), description: "OK")]
    public virtual IActionResult PersonsGet([FromQuery] PrimaryCodeParam? primaryCodeParam, [FromQuery] FilterParams? filterParams, [FromQuery] PagingParams? pagingParams, [FromQuery] List<string>? affiliations, [FromQuery] string? sort = "personId")
    {
        var parameters = new DataRequestParameters(primaryCodeParam, filterParams, pagingParams, sort);
        var result = _personsService.GetAll(parameters);
        return Ok(result);
    }

    /// <summary>
    /// GET /persons/me
    /// </summary>
    /// <remarks>Returns the person object for the currently authenticated user.</remarks>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("persons/me")]
    [Authorize(AuthenticationSchemes = BearerAuthenticationHandler.SchemeName)]
    [ValidateModelState]
    [SwaggerOperation("PersonsMeGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Person), description: "OK")]
    public virtual IActionResult PersonsMeGet()
    {
        return BadRequest(new ErrorResponse(400, "Not implemented yet."));
    }

    /// <summary>
    /// GET /persons/{personId}/associations
    /// </summary>
    /// <remarks>Get a list of all associations for an individual person.</remarks>
    /// <param name="personId">Person ID</param>
    /// <param name="filterParams"></param>
    /// <param name="pagingParams"></param>
    /// <param name="associationType">Filter by association type</param>
    /// <param name="role">Filter by role</param>
    /// <param name="state">Filter by state</param>
    /// <param name="resultState">Filter by result state</param>
    /// <param name="sort">
    ///Default: ["associationId"]<br/>
    ///Items Enum: "associationId" "-associationId"<br/>
    ///Example: sort=associationId<br/>
    /// Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("persons/{personId}/associations")]
    [ValidateModelState]
    [SwaggerOperation("PersonsPersonIdAssociationsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Associations), description: "OK")]
    public virtual IActionResult PersonsPersonIdAssociationsGet([FromRoute][Required] Guid personId, [FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] string? associationType, [FromQuery] string? role, [FromQuery] string? state, [FromQuery] string? resultState, [FromQuery] string? sort = "associationId")
    {
        var parameters = new DataRequestParameters(filterParams, pagingParams, sort);
        var result = _associationsService.GetAssociationsByPersonId(parameters, personId);
        return Ok(result);
    }

    /// <summary>
    /// GET /persons/{personId}
    /// </summary>
    /// <remarks>Get a single person.</remarks>
    /// <param name="personId">User ID</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("persons/{personId}")]
    [ValidateModelState]
    [SwaggerOperation("PersonsPersonIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Person), description: "OK")]
    public virtual IActionResult PersonsPersonIdGet([FromRoute][Required] Guid personId)
    {
        var result = _personsService.Get(personId);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    /// <summary>
    /// GET /persons/{personId}/groups
    /// </summary>
    /// <remarks>Get an ordered list of all groups for a given person, ordered by name.</remarks>
    /// <param name="personId">Person ID</param>
    /// <param name="filterParams"></param>
    /// <param name="pagingParams"></param>
    /// <param name="groupType">Filter by group type</param>
    /// <param name="sort">
    ///Default: ["name"]<br/>
    ///Items Enum: "groupId" "name" "startDate" "-groupId" "-name" "-startDate"<br/>
    ///Example: sort=name,-groupId<br/>
    /// Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("persons/{personId}/groups")]
    [ValidateModelState]
    [SwaggerOperation("PersonsPersonIdGroupsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Groups), description: "OK")]
    public virtual IActionResult PersonsPersonIdGroupsGet([FromRoute][Required] Guid personId, [FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] string? groupType, [FromQuery] string? sort = "name")
    {
        var parameters = new DataRequestParameters(filterParams, pagingParams, sort);
        var result = _groupsService.GetGroupsByPersonId(parameters, personId);
        return Ok(result);
    }
}
