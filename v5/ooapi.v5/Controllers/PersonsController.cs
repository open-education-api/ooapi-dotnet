/*
 * Open Education API
 *
 * OpenAPI (fka Swagger) specification for the Open Education API.  <figure>  <a target=\"_blank\" href=\"OOAPIv5_model.png\">   <img src=\"OOAPIv5_model.png\" alt=\"OOAPI information model that feeds OOAPI specification\" width=\"70%\" class=\"img-responsive\">   </a>   <figcaption> OOAPI information model that feeds OOAPI specification (click to enlage)</figcaption> </figure>  The model provides an overview of how the objects on which the API is specified are related. The overarching concept educations is not found in the in the end points of the API. The smaller concepts of programOffering, courseOffering and conceptOffering are all found in the offering endpoint. The different types of association can all be found in the association endpoint.  The original file for this model can be found <a target=\"_blank\" href=\"OOAPIv5_model_v0.4.drawio\">here</a>  The program relations object is not found as a separate endpoint but relations between programs can be found within the program endpoint by expanding that endpoint.  Information about earlier meetings and presentations can be found <a target=\"_blank\" href=\"https://github.com/open-education-api/notulen\">here</a>  Information on the EDU-API model that was also used for this api is shown <a target=\"_blank\" href=\"eduapi.png\">here</a> 
 *
 * OpenAPI spec version: 5.0.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using ooapi.v5.Attributes;
using ooapi.v5.Models;
using ooapi.v5.Models.Params;
using ooapi.v5.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ooapi.v5.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class PersonsController : BaseController
    {
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
        public virtual IActionResult PersonsGet([FromQuery] PrimaryCodeParam primaryCodeParam, [FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] List<string> affiliations, [FromQuery] string sort)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(InlineResponse2001));

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
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(InlineResponse2002));

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
        public virtual IActionResult PersonsPersonIdAssociationsGet([FromRoute][Required] Guid? personId, [FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] string associationType, [FromQuery] string role, [FromQuery] string state, [FromQuery] string resultState, [FromQuery] string sort)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(InlineResponse2003));

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
        public virtual IActionResult PersonsPersonIdGet([FromRoute][Required] Guid? personId)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(InlineResponse2002));

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
        public virtual IActionResult PersonsPersonIdGroupsGet([FromRoute][Required] Guid? personId, [FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] string groupType, [FromQuery] string sort)
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

        ///// <summary>
        ///// POST /persons
        ///// </summary>
        ///// <remarks>POST a single person.</remarks>
        ///// <param name="body"></param>
        ///// <response code="201">CREATED</response>
        ///// <response code="400">Bad request</response>
        //[HttpPost]
        //[Route("persons")]
        //[ValidateModelState]
        //[SwaggerOperation("PersonsPost")]
        //[SwaggerResponse(statusCode: 201, type: typeof(List<Person>), description: "CREATED")]
        //[SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400), description: "Bad request")]
        //public virtual IActionResult PersonsPost([FromBody] PersonsBody body)
        //{
        //    //TODO: Uncomment the next line to return response 201 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        //    // return StatusCode(201, default(InlineResponse201));

        //    //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        //    // return StatusCode(400, default(InlineResponse400));
        //    string exampleJson = null;
        //    exampleJson = "\"\"";

        //    var example = exampleJson != null
        //    ? JsonConvert.DeserializeObject<InlineResponse201>(exampleJson)
        //    : default(InlineResponse201);            //TODO: Change the data returned
        //    return new ObjectResult(example);
        //}
    }
}