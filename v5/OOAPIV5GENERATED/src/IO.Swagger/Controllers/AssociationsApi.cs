/*
 * Open Education API
 *
 * OpenAPI (fka Swagger) specification for the Open Education API.  <figure>  <a target=\"_blank\" href=\"OOAPIv5_model.png\">   <img src=\"OOAPIv5_model.png\" alt=\"OOAPI information model that feeds OOAPI specification\" width=\"70%\" class=\"img-responsive\">   </a>   <figcaption> OOAPI information model that feeds OOAPI specification (click to enlage)</figcaption> </figure>  The model provides an overview of how the objects on which the API is specified are related. The overarching concept educations is not found in the in the end points of the API. The smaller concepts of programOffering, courseOffering and conceptOffering are all found in the offering endpoint. The different types of association can all be found in the association endpoint.  The original file for this model can be found <a target=\"_blank\" href=\"OOAPIv5_model_v0.4.drawio\">here</a>  The program relations object is not found as a separate endpoint but relations between programs can be found within the program endpoint by expanding that endpoint.  Information about earlier meetings and presentations can be found <a target=\"_blank\" href=\"https://github.com/open-education-api/notulen\">here</a>  Information on the EDU-API model that was also used for this api is shown <a target=\"_blank\" href=\"eduapi.png\">here</a> 
 *
 * OpenAPI spec version: 5.0.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using IO.Swagger.Attributes;
using IO.Swagger.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IO.Swagger.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class AssociationsApiController : ControllerBase
    {
        /// <summary>
        /// GET /associations/{associationId}
        /// </summary>
        /// <remarks>Get a single association.</remarks>
        /// <param name="associationId">Association ID</param>
        /// <param name="expand">Optional properties to expand, separated by a comma</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method not allowed</response>
        /// <response code="429">Too many requests</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("/v5/associations/{associationId}")]
        [ValidateModelState]
        [SwaggerOperation("AssociationsAssociationIdGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(Association), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400), description: "Bad request")]
        [SwaggerResponse(statusCode: 401, type: typeof(InlineResponse400), description: "Unauthorized")]
        [SwaggerResponse(statusCode: 403, type: typeof(InlineResponse400), description: "Forbidden")]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400), description: "Not Found")]
        [SwaggerResponse(statusCode: 405, type: typeof(InlineResponse400), description: "Method not allowed")]
        [SwaggerResponse(statusCode: 429, type: typeof(InlineResponse400), description: "Too many requests")]
        [SwaggerResponse(statusCode: 500, type: typeof(InlineResponse400), description: "Internal Server Error")]
        public virtual IActionResult AssociationsAssociationIdGet([FromRoute][Required] Guid? associationId, [FromQuery] List<string> expand)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(InlineResponse20023));

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

        ///// <summary>
        ///// PATCH /associations/{associationId}
        ///// </summary>
        ///// <remarks>Update the status or result of an enrollment. Other elements of the association object COULD  also be PATCHED. But are not likely and have therefor not been included in this endpoint. Implementation of the PATCH activity is based on use PATCH with JSON Merge Patch standard,  a specialized media type &#x60;application/merge-patch+json&#x60; for partial resource representation  to update parts of resource objects. </remarks>
        ///// <param name="body"></param>
        ///// <param name="associationId">The id of the association to update</param>
        ///// <response code="200">OK</response>
        ///// <response code="400">Bad request</response>
        //[HttpPatch]
        //[Route("/v5/associations/{associationId}")]
        //[ValidateModelState]
        //[SwaggerOperation("AssociationsAssociationIdPatch")]
        //[SwaggerResponse(statusCode: 200, type: typeof(Association), description: "OK")]
        //[SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400), description: "Bad request")]
        //public virtual IActionResult AssociationsAssociationIdPatch([FromBody] AssociationsAssociationIdBody body, [FromRoute][Required] Guid? associationId)
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
        //[Route("/v5/associations/external/me")]
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
}
