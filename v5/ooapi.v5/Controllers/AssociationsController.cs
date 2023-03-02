using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ooapi.v5.Attributes;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Services;
using ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ooapi.v5.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
public class AssociationsController : BaseController
{

    public AssociationsController(IConfiguration configuration, CoreDBContext dbContext) : base(configuration, dbContext)
    {
    }

    /// <summary>
    /// GET /associations/{associationId}
    /// </summary>
    /// <remarks>Get a single association.</remarks>
    /// <param name="associationId">Association ID</param>
    /// <param name="expand">Items Enum: "person" "offering" <br/> Optional properties to expand, separated by a comma</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("associations/{associationId}")]
    [ValidateModelState]
    [SwaggerOperation("AssociationsAssociationIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(List<Association>), description: "OK")]
    public virtual IActionResult AssociationsAssociationIdGet([FromRoute][Required] Guid associationId, [FromQuery] List<string> expand)
    {
        var service = new AssociationsService(DBContext, UserRequestContext);
        var result = service.Get(associationId, out ErrorResponse errorResponse);
        if (result == null)
        {
            return BadRequest(errorResponse);
        }
        return Ok(result);
    }

    ///// <summary>
    ///// PATCH /associations/{associationId}
    ///// </summary>
    ///// <remarks>Update the status or result of an enrollment. Other elements of the association object COULD  also be PATCHED. But are not likely and have therefor not been included in this endpoint. Implementation of the PATCH activity is based on use PATCH with JSON Merge Patch standard,  a specialized media type &#x60;application/merge-patch+json&#x60; for partial resource representation  to update parts of resource objects. </remarks>
    ///// <param name="body"></param>
    ///// <param name="associationId">The id of the association to update</param>
    ///// <response code="200">OK</response>
    ///// <response code="400">Bad request</response>
    //[HttpPatch]
    //[Route("associations/{associationId}")]
    //[ValidateModelState]
    //[SwaggerOperation("AssociationsAssociationIdPatch")]
    //[SwaggerResponse(statusCode: 200, type: typeof(Association), description: "OK")]
    //[SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400), description: "Bad request")]
    //public virtual IActionResult AssociationsAssociationIdPatch([FromBody] AssociationsAssociationIdBody body, [FromRoute][Required] Guid associationId)
    //{
    //    //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
    //    // return StatusCode(200, default(InlineResponse20024));

    //    //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
    //    // return StatusCode(400, default(InlineResponse400));
    //    string exampleJson = null;
    //    exampleJson = "\"\"";

    //    var example = exampleJson != null
    //    ? JsonConvert.DeserializeObject<InlineResponse20024>(exampleJson)
    //    : default(InlineResponse20024);            //TODO: Change the data returned
    //    return new ObjectResult(example);
    //}

    ///// <summary>
    ///// POST /associations/external/me
    ///// </summary>
    ///// <remarks>POST a single association enroll a person based on person information obtained from .wellknown endpoint, an offering,  and the organization/type&#x3D;root information form the organization that is issuing this association.  The offering can either be identified by an offeringId if known or the full offering details.  </remarks>
    ///// <param name="body"></param>
    ///// <response code="201">CREATED</response>
    ///// <response code="400">Bad request</response>
    //[HttpPost]
    //[Route("associations/external/me")]
    //[ValidateModelState]
    //[SwaggerOperation("AssociationsExternalMePost")]
    //[SwaggerResponse(statusCode: 201, type: typeof(InlineResponse20024), description: "CREATED")]
    //[SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400), description: "Bad request")]
    //public virtual IActionResult AssociationsExternalMePost([FromBody] ExternalMeBody body)
    //{
    //    //TODO: Uncomment the next line to return response 201 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
    //    // return StatusCode(201, default(InlineResponse20024));

    //    //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
    //    // return StatusCode(400, default(InlineResponse400));
    //    string exampleJson = null;
    //    exampleJson = "\"\"";

    //    var example = exampleJson != null
    //    ? JsonConvert.DeserializeObject<InlineResponse20024>(exampleJson)
    //    : default(InlineResponse20024);            //TODO: Change the data returned
    //    return new ObjectResult(example);
    //}
}
