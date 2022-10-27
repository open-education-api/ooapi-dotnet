using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ooapi.v5.Attributes;
using ooapi.v5.Models;
using ooapi.v5.Models.Params;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace ooapi.v5.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
public class OfferingsController : BaseController
{
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
    public virtual IActionResult OfferingsOfferingIdAssociationsGet([FromRoute][Required] Guid offeringId, [FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] string associationType, [FromQuery] string role, [FromQuery] string state, [FromQuery] string resultState, [FromQuery] string sort)
    {
        //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(200, default(InlineResponse20022));

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

        return null;
    }

    /// <summary>
    /// GET /offerings/{offeringId}
    /// </summary>
    /// <remarks>Get a single offering.</remarks>
    /// <param name="offeringId">Offering ID</param>
    /// <param name="expand">Optional properties to expand, separated by a comma</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("offerings/{offeringId}")]
    [ValidateModelState]
    [SwaggerOperation("OfferingsOfferingIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Offering), description: "OK")]
    public virtual IActionResult OfferingsOfferingIdGet([FromRoute][Required] Guid offeringId, [FromQuery] List<string> expand)
    {
        //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(200, default(InlineResponse20021));

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
    public virtual IActionResult OfferingsOfferingIdGroupsGet([FromRoute][Required] Guid offeringId, [FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] string groupType, [FromQuery] string sort)
    {
        //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(200, default(InlineResponse2004));

        //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(400, default(InlineResponse400));

        //TODO: Uncomment the next line to return response 401 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(401, default(InlineResponse400));

        //TODO: Uncomment the next line to return response 403 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(403, default(InlineResponse400));

        //TODO: Uncomment the next line to return response 405 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(405, default(InlineResponse400));

        //TODO: Uncomment the next line to return response 429 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(429, default(InlineResponse400));

        //TODO: Uncomment the next line to return response 500 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(500, default(InlineResponse400));
        return null;
    }
}
