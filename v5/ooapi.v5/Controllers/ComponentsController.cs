using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ooapi.v5.Attributes;
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
public class ComponentsController : BaseController
{
    /// <summary>
    /// GET /components/{componentId}
    /// </summary>
    /// <remarks>Get a single component.</remarks>
    /// <param name="componentId">component ID</param>
    /// <param name="expand">Optional properties to expand, separated by a comma</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("components/{componentId}")]
    [ValidateModelState]
    [SwaggerOperation("ComponentsComponentIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Component), description: "OK")]
    public virtual IActionResult ComponentsComponentIdGet([FromRoute][Required] Guid componentId, [FromQuery] List<string> expand)
    {
        //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(200, default(InlineResponse20019));

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
    /// GET /components/{componentId}/offerings
    /// </summary>
    /// <remarks>Get a list of all offerings for this component, ordered chronologically.</remarks>
    /// <param name="componentId">Component ID</param>
    /// <param name="filterParams"></param>
    /// <param name="pagingParams"></param>
    /// <param name="teachingLanguage">Filter by teachingLanguage, which is a string describing the main teaching language, should be a three-letter language code as specified by ISO 639-2.</param>
    /// <param name="resultExpected">Filter by resultExpected</param>
    /// <param name="since">Filter all offerings by providing a minimum start date for the corresponding academic session, RFC3339 (full-date). By default only future offerings are shown (equal to &#x60;?since&#x3D;&lt;today&gt;&#x60;).</param>
    /// <param name="until">Filter all offerings by providing a maximum end date for the corresponding academic session, RFC3339 (full-date).</param>
    /// <param name="sort"><br/>
    ///Default: ["startDateTime"]<br/>
    ///Items Enum: "offeringId" "name" "startDateTime" "endDateTime" "-offeringId" "-name" "-startDateTime" "-endDateTime"<br/>
    ///Example: sort=offeringId,-startDateTime<br/>
    /// Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("components/{componentId}/offerings")]
    [ValidateModelState]
    [SwaggerOperation("ComponentsComponentIdOfferingsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(ComponentOfferings), description: "OK")]
    public virtual IActionResult ComponentsComponentIdOfferingsGet([FromRoute][Required] Guid componentId, [FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] string teachingLanguage, [FromQuery] bool? resultExpected, [FromQuery] DateTime? since, [FromQuery] DateTime? until, [FromQuery] string sort)
    {
        //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(200, default(InlineResponse20020));

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
}
