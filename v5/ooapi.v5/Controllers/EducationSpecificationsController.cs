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
    public class EducationSpecificationsController : BaseController
    {
        /// <summary>
        /// GET /education-specifications/{educationSpecificationId}/courses
        /// </summary>
        /// <remarks>Get an ordered list of all courses given through this EducationSpecification.</remarks>
        /// <param name="educationSpecificationId">Education Specification ID</param>
        /// <param name="filterParams"></param>
        /// <param name="pagingParams"></param>
        /// <param name="teachingLanguage">Filter by teachingLanguage, which is a string describing the main teaching language, should be a three-letter language code as specified by ISO 639-2.</param>
        /// <param name="level">Filter by level</param>
        /// <param name="modeOfDelivery">Filter by modeOfDelivery</param>
        /// <param name="sort">
        ///Default: ["courseId"]<br/>
        ///Items Enum: "courseId" "name" "-courseId" "-name"<br/>
        ///Example: sort=courseId,-name<br/>
        /// Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("education-specifications/{educationSpecificationId}/courses")]
        [ValidateModelState]
        [SwaggerOperation("EducationSpecificationsEducationSpecificationIdCoursesGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(Courses), description: "OK")]
        public virtual IActionResult EducationSpecificationsEducationSpecificationIdCoursesGet([FromRoute][Required] Guid? educationSpecificationId, [FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] string teachingLanguage, [FromQuery] string level, [FromQuery] List<string> modeOfDelivery, [FromQuery] string sort)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(InlineResponse2008));

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
        /// GET /education-specifications/{educationSpecificationId}/education-specifications
        /// </summary>
        /// <remarks>Get an ordered list of all education-specifications given through this educationspecification.</remarks>
        /// <param name="educationSpecificationId">Education Specification ID</param>
        /// <param name="filterParams"></param>
        /// <param name="pagingParams"></param>
        /// <param name="teachingLanguage">Filter by teachingLanguage, which is a string describing the main teaching language, should be a three-letter language code as specified by ISO 639-2.</param>
        /// <param name="sort">
        ///Default: ["educationSpecificationId"]<br/>
        ///Items Enum: "educationSpecificationId" "name" "educationSpecificationType" "-educationSpecificationId" "-name" "-educationSpecificationType"<br/>
        ///Example: sort=educationSpecificationId,-name<br/>
        /// Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("education-specifications/{educationSpecificationId}/education-specifications")]
        [ValidateModelState]
        [SwaggerOperation("EducationSpecificationsEducationSpecificationIdEducationSpecificationsGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(EducationSpecifications), description: "OK")]
        public virtual IActionResult EducationSpecificationsEducationSpecificationIdEducationSpecificationsGet([FromRoute][Required] Guid? educationSpecificationId, [FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] string teachingLanguage, [FromQuery] string sort)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(InlineResponse2008));

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
        /// GET /education-specifications/{educationSpecificationId}
        /// </summary>
        /// <remarks>Get a single education specification.</remarks>
        /// <param name="educationSpecificationId">Education specification ID</param>
        /// <param name="returnTimelineOverrides">Controls whether the attribute &#x60;timelineOverrides&#x60; is returned or not. The default is &#x60;false&#x60;, so this has to explicitly set to &#x60;true&#x60; if a client needs the timeline overrides. See [GET /education-specifications/{educationSpecificationId}](#tag/education-specifications/paths/~1education-specifications~1{educationSpecificationId}/get) for an example.</param>
        /// <param name="expand">Optional properties to include, separated by a comma</param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("education-specifications/{educationSpecificationId}")]
        [ValidateModelState]
        [SwaggerOperation("EducationSpecificationsEducationSpecificationIdGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(EducationSpecification), description: "OK")]
        public virtual IActionResult EducationSpecificationsEducationSpecificationIdGet([FromRoute][Required] Guid? educationSpecificationId, [FromQuery] bool? returnTimelineOverrides, [FromQuery] List<string> expand)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(InlineResponse20033));

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
        /// GET /education-specifications/{educationSpecificationId}/programs
        /// </summary>
        /// <remarks>Get an ordered list of all programs for a given education specification, ordered by name.</remarks>
        /// <param name="educationSpecificationId">Education Specification ID</param>
        /// <param name="filterParams"></param>
        /// <param name="pagingParams"></param>
        /// <param name="teachingLanguage">Filter by teachingLanguage, which is a string describing the main teaching language, should be a three-letter language code as specified by ISO 639-2.</param>
        /// <param name="programType">Filter by program type</param>
        /// <param name="qualificationAwarded">Filter by qualificationAwarded</param>
        /// <param name="levelOfQualification">Filter by levelOfQualification</param>
        /// <param name="sector">Filter by sector</param>
        /// <param name="fieldsOfStudy">Filter by fieldsOfStudy</param>
        /// <param name="crohoCreboCode">Filter by croho-creboCode</param>
        /// <param name="sort">
        ///Default: ["name"]<br/>
        ///Items Enum: "programId" "name" "-programId" "-name"<br/>
        ///Example: sort=programId,-name<br/>
        /// Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("education-specifications/{educationSpecificationId}/programs")]
        [ValidateModelState]
        [SwaggerOperation("EducationSpecificationsEducationSpecificationIdProgramsGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(Programs), description: "OK")]
        public virtual IActionResult EducationSpecificationsEducationSpecificationIdProgramsGet([FromRoute][Required] Guid? educationSpecificationId, [FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] string teachingLanguage, [FromQuery] string programType, [FromQuery] string qualificationAwarded, [FromQuery] string levelOfQualification, [FromQuery] string sector, [FromQuery] string fieldsOfStudy, [FromQuery] string crohoCreboCode, [FromQuery] string sort)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(InlineResponse2007));

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
        /// GET /education-specifications
        /// </summary>
        /// <remarks>Get a list of all education specifications, ordered by name (ascending).</remarks>
        /// <param name="filterParams"></param>
        /// <param name="pagingParams"></param>
        /// <param name="educationSpecificationType">Filter by type of education specification</param>
        /// <param name="primaryCode">Filter by primary code of education specification</param>
        /// <param name="sort">
        ///Default: [["name"]]<br/>
        ///Items Enum: "educationSpecificationType" "name" "primaryCode" "-educationSpecificationType" "-name" "-primaryCode"<br/>
        ///Example: sort=educationSpecificationType,-primaryCode<br/>
        /// Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("education-specifications")]
        [ValidateModelState]
        [SwaggerOperation("EducationSpecificationsGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(EducationSpecifications), description: "OK")]
        public virtual IActionResult EducationSpecificationsGet([FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] string educationSpecificationType, [FromQuery] string primaryCode, [FromQuery] string sort)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(InlineResponse20012));

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