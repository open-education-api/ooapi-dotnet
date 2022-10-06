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
    public class ComponentsApiController : ControllerBase
    {
        /// <summary>
        /// GET /components/{componentId}
        /// </summary>
        /// <remarks>Get a single component.</remarks>
        /// <param name="componentId">component ID</param>
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
        [Route("/v5.0/components/{componentId}")]
        [ValidateModelState]
        [SwaggerOperation("ComponentsComponentIdGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(Component), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400), description: "Bad request")]
        [SwaggerResponse(statusCode: 401, type: typeof(InlineResponse400), description: "Unauthorized")]
        [SwaggerResponse(statusCode: 403, type: typeof(InlineResponse400), description: "Forbidden")]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400), description: "Not Found")]
        [SwaggerResponse(statusCode: 405, type: typeof(InlineResponse400), description: "Method not allowed")]
        [SwaggerResponse(statusCode: 429, type: typeof(InlineResponse400), description: "Too many requests")]
        [SwaggerResponse(statusCode: 500, type: typeof(InlineResponse400), description: "Internal Server Error")]
        public virtual IActionResult ComponentsComponentIdGet([FromRoute][Required] Guid? componentId, [FromQuery] List<string> expand)
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
        /// <param name="pageSize">The number of items per page</param>
        /// <param name="pageNumber">The page number to get. Page numbers start at 1.</param>
        /// <param name="consumer">Request entities meant for a specific consumer. This query parameter is independent from the &#x60;consumers&#x60; attribute. See the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</param>
        /// <param name="q">Filter by items having a name, abbreviation or description containing the given search term (exact partial match, case insensitive)</param>
        /// <param name="teachingLanguage">Filter by teachingLanguage, which is a string describing the main teaching language, should be a three-letter language code as specified by ISO 639-2.</param>
        /// <param name="resultExpected">Filter by resultExpected</param>
        /// <param name="since">Filter all offerings by providing a minimum start date for the corresponding academic session, RFC3339 (full-date). By default only future offerings are shown (equal to &#x60;?since&#x3D;&lt;today&gt;&#x60;).</param>
        /// <param name="until">Filter all offerings by providing a maximum end date for the corresponding academic session, RFC3339 (full-date).</param>
        /// <param name="sort">Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method not allowed</response>
        /// <response code="429">Too many requests</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("/v5.0/components/{componentId}/offerings")]
        [ValidateModelState]
        [SwaggerOperation("ComponentsComponentIdOfferingsGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<ComponentOffering>), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400), description: "Bad request")]
        [SwaggerResponse(statusCode: 401, type: typeof(InlineResponse400), description: "Unauthorized")]
        [SwaggerResponse(statusCode: 403, type: typeof(InlineResponse400), description: "Forbidden")]
        [SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400), description: "Not Found")]
        [SwaggerResponse(statusCode: 405, type: typeof(InlineResponse400), description: "Method not allowed")]
        [SwaggerResponse(statusCode: 429, type: typeof(InlineResponse400), description: "Too many requests")]
        [SwaggerResponse(statusCode: 500, type: typeof(InlineResponse400), description: "Internal Server Error")]
        public virtual IActionResult ComponentsComponentIdOfferingsGet([FromRoute][Required] Guid? componentId, [FromQuery] int? pageSize, [FromQuery] int? pageNumber, [FromQuery] string consumer, [FromQuery] string q, [FromQuery] string teachingLanguage, [FromQuery] bool? resultExpected, [FromQuery] DateTime? since, [FromQuery] DateTime? until, [FromQuery] List<string> sort)
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
}
