using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ooapi.v5.Attributes;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Services;
using ooapi.v5.core.Services.Interfaces;
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
public class OfferingsController : BaseController
{
    private readonly IOfferingsService _offeringsService;

    public OfferingsController(IOfferingsService offeringsService)
    {
        _offeringsService = offeringsService;
    }

    /// <summary>
    /// GET /offerings/{offeringId}/associations
    /// </summary>
    /// <remarks>Get a list of all offering associations.</remarks>
    /// <param name="offeringId">Offering ID</param>
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
    [Route("offerings/{offeringId}/associations")]
    [ValidateModelState]
    [SwaggerOperation("OfferingsOfferingIdAssociationsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Associations), description: "OK")]
    public virtual IActionResult OfferingsOfferingIdAssociationsGet([FromRoute][Required] Guid offeringId, [FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] string? associationType, [FromQuery] string? role, [FromQuery] string? state, [FromQuery] string? resultState, [FromQuery] string? sort = "associationId")
    {
        return BadRequest(new ErrorResponse(400, "Not implemented yet."));
    }

    /// <summary>
    /// GET /offerings/{offeringId}
    /// </summary>
    /// <remarks>Get a single offering.</remarks>
    /// <param name="offeringId">Offering ID</param>
    /// <param name="expand">Items Enum: "program" "programOffering" "course" "courseOffering" "component" "organization" "academicSession" <br/>Optional properties to expand, separated by a comma</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("offerings/{offeringId}")]
    [ValidateModelState]
    [SwaggerOperation("OfferingsOfferingIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(OneOfOfferingNoIdentifier), description: "OK")]
    public virtual IActionResult OfferingsOfferingIdGet([FromRoute][Required] Guid offeringId, [FromQuery] List<string> expand)
    {
        DataRequestParameters parameters = new DataRequestParameters();
        parameters.Expand = expand;

        OneOfOfferingInstance result = _offeringsService.Get(offeringId, out ErrorResponse errorResponse);
        if (result == null)
        {
            return BadRequest(errorResponse);
        }
        if (result.CourseOffering != null)
            return Ok(result.CourseOffering);
        if (result.ComponentOffering != null)
            return Ok(result.ComponentOffering);
        if (result.ProgramOffering != null)
            return Ok(result.ProgramOffering);

        return NotFound(errorResponse);



        return null;
    }

    /// <summary>
    /// GET /offerings/{offeringId}/groups
    /// </summary>
    /// <remarks>Get an ordered list of all groups related to an offering, ordered by name.</remarks>
    /// <param name="offeringId">Offering ID</param>
    /// <param name="filterParams"></param>
    /// <param name="pagingParams"></param>
    /// <param name="groupType">Filter by group type</param>
    /// <param name="sort">
    ///Default: ["name"]<br/>
    ///Items Enum: "groupId" "name" "startDate" "-groupId" "-name" "-startDate"<br/>
    ///Example: sort=name,-startDate<br/>
    /// Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("offerings/{offeringId}/groups")]
    [ValidateModelState]
    [SwaggerOperation("OfferingsOfferingIdGroupsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Groups), description: "OK")]
    public virtual IActionResult OfferingsOfferingIdGroupsGet([FromRoute][Required] Guid offeringId, [FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] string? groupType, [FromQuery] string? sort = "name")
    {
        return BadRequest(new ErrorResponse(400,"Not implemented yet."));
    }
}
