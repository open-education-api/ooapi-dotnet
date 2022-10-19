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
    public class OrganizationsController : BaseController
    {
        /// <summary>
        /// GET /organizations
        /// </summary>
        /// <remarks>Get an ordered list of all organizations, ordered by name.</remarks>
        /// <param name="primaryCode">The primaryCode of the requested item. This is often the source identifier as defined by the institution.</param>
        /// <param name="pageSize">The number of items per page</param>
        /// <param name="pageNumber">The page number to get. Page numbers start at 1.</param>
        /// <param name="consumer">Request entities meant for a specific consumer. This query parameter is independent from the &#x60;consumers&#x60; attribute. See the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</param>
        /// <param name="q">Filter by items having a name, abbreviation or description containing the given search term (exact partial match, case insensitive)</param>
        /// <param name="organizationType">Filter by organization type</param>
        /// <param name="sort">Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="405">Method not allowed</response>
        /// <response code="429">Too many requests</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("organizations")]
        [ValidateModelState]
        [SwaggerOperation("OrganizationsGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Organization>), description: "OK")]
        public virtual IActionResult OrganizationsGet([FromQuery] string primaryCode, [FromQuery] int? pageSize, [FromQuery] int? pageNumber, [FromQuery] string consumer, [FromQuery] string q, [FromQuery] string organizationType, [FromQuery] List<string> sort)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(InlineResponse2005));

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
        /// GET /organizations/{organizationId}/components
        /// </summary>
        /// <remarks>Get an ordered list of all components for a given organization, ordered by name.</remarks>
        /// <param name="organizationId">Organization ID</param>
        /// <param name="pageSize">The number of items per page</param>
        /// <param name="pageNumber">The page number to get. Page numbers start at 1.</param>
        /// <param name="consumer">Request entities meant for a specific consumer. This query parameter is independent from the &#x60;consumers&#x60; attribute. See the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</param>
        /// <param name="q">Filter by items having a name, abbreviation or description containing the given search term (exact partial match, case insensitive)</param>
        /// <param name="teachingLanguage">Filter by teachingLanguage, which is a string describing the main teaching language, should be a three-letter language code as specified by ISO 639-2.</param>
        /// <param name="componentType">Filter by component type</param>
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
        [Route("organizations/{organizationId}/components")]
        [ValidateModelState]
        [SwaggerOperation("OrganizationsOrganizationIdComponentsGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Component>), description: "OK")]
        public virtual IActionResult OrganizationsOrganizationIdComponentsGet([FromRoute][Required] Guid? organizationId, [FromQuery] int? pageSize, [FromQuery] int? pageNumber, [FromQuery] string consumer, [FromQuery] string q, [FromQuery] string teachingLanguage, [FromQuery] string componentType, [FromQuery] List<string> sort)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(InlineResponse2009));

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
        /// GET /organizations/{organizationId}/courses
        /// </summary>
        /// <remarks>Get an ordered list of all courses for a given organization, ordered by name.</remarks>
        /// <param name="organizationId">Organization ID</param>
        /// <param name="pageSize">The number of items per page</param>
        /// <param name="pageNumber">The page number to get. Page numbers start at 1.</param>
        /// <param name="consumer">Request entities meant for a specific consumer. This query parameter is independent from the &#x60;consumers&#x60; attribute. See the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</param>
        /// <param name="q">Filter by items having a name, abbreviation or description containing the given search term (exact partial match, case insensitive)</param>
        /// <param name="teachingLanguage">Filter by teachingLanguage, which is a string describing the main teaching language, should be a three-letter language code as specified by ISO 639-2.</param>
        /// <param name="level">Filter by level</param>
        /// <param name="modeOfDelivery">Filter by modeOfDelivery</param>
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
        [Route("organizations/{organizationId}/courses")]
        [ValidateModelState]
        [SwaggerOperation("OrganizationsOrganizationIdCoursesGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Course>), description: "OK")]
        public virtual IActionResult OrganizationsOrganizationIdCoursesGet([FromRoute][Required] Guid? organizationId, [FromQuery] int? pageSize, [FromQuery] int? pageNumber, [FromQuery] string consumer, [FromQuery] string q, [FromQuery] string teachingLanguage, [FromQuery] string level, [FromQuery] List<string> modeOfDelivery, [FromQuery] List<string> sort)
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
        /// GET /organizations/{organizationId}/education-specifications
        /// </summary>
        /// <remarks>Get an ordered list of all EducationSpecifications for a given organization, ordered by name.</remarks>
        /// <param name="organizationId">Organization ID</param>
        /// <param name="pageSize">The number of items per page</param>
        /// <param name="pageNumber">The page number to get. Page numbers start at 1.</param>
        /// <param name="consumer">Request entities meant for a specific consumer. This query parameter is independent from the &#x60;consumers&#x60; attribute. See the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</param>
        /// <param name="q">Filter by items having a name, abbreviation or description containing the given search term (exact partial match, case insensitive)</param>
        /// <param name="educationSpecificationType">Filter by type of education specification</param>
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
        [Route("organizations/{organizationId}/education-specifications")]
        [ValidateModelState]
        [SwaggerOperation("OrganizationsOrganizationIdEducationSpecificationsGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<EducationSpecification>), description: "OK")]
        public virtual IActionResult OrganizationsOrganizationIdEducationSpecificationsGet([FromRoute][Required] Guid? organizationId, [FromQuery] int? pageSize, [FromQuery] int? pageNumber, [FromQuery] string consumer, [FromQuery] string q, [FromQuery] string educationSpecificationType, [FromQuery] List<string> sort)
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

        /// <summary>
        /// GET /organizations/{organizationId}
        /// </summary>
        /// <remarks>Get a single organization.</remarks>
        /// <param name="organizationId">Organization ID</param>
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
        [Route("organizations/{organizationId}")]
        [ValidateModelState]
        [SwaggerOperation("OrganizationsOrganizationIdGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(Organization), description: "OK")]
        public virtual IActionResult OrganizationsOrganizationIdGet([FromRoute][Required] Guid? organizationId, [FromQuery] List<string> expand)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(InlineResponse2006));

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
        /// GET /organizations/{organizationId}/groups
        /// </summary>
        /// <remarks>Get an ordered list of all groups for a given organization, ordered by name.</remarks>
        /// <param name="organizationId">Organization ID</param>
        /// <param name="pageSize">The number of items per page</param>
        /// <param name="pageNumber">The page number to get. Page numbers start at 1.</param>
        /// <param name="consumer">Request entities meant for a specific consumer. This query parameter is independent from the &#x60;consumers&#x60; attribute. See the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</param>
        /// <param name="q">Filter by items having a name, abbreviation or description containing the given search term (exact partial match, case insensitive)</param>
        /// <param name="groupType">Filter by group type</param>
        /// <param name="sort">Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="405">Method not allowed</response>
        /// <response code="429">Too many requests</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("organizations/{organizationId}/groups")]
        [ValidateModelState]
        [SwaggerOperation("OrganizationsOrganizationIdGroupsGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Group>), description: "OK")]
        public virtual IActionResult OrganizationsOrganizationIdGroupsGet([FromRoute][Required] Guid? organizationId, [FromQuery] int? pageSize, [FromQuery] int? pageNumber, [FromQuery] string consumer, [FromQuery] string q, [FromQuery] string groupType, [FromQuery] List<string> sort)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(InlineResponse20011));

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
        /// GET /organizations/{organizationId}/offerings
        /// </summary>
        /// <remarks>Get a list of all offerings for a given organization</remarks>
        /// <param name="organizationId">Organization ID</param>
        /// <param name="pageSize">The number of items per page</param>
        /// <param name="pageNumber">The page number to get. Page numbers start at 1.</param>
        /// <param name="consumer">Request entities meant for a specific consumer. This query parameter is independent from the &#x60;consumers&#x60; attribute. See the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</param>
        /// <param name="q">Filter by items having a name, abbreviation or description containing the given search term (exact partial match, case insensitive)</param>
        /// <param name="teachingLanguage">Filter by teachingLanguage, which is a string describing the main teaching language, should be a three-letter language code as specified by ISO 639-2.</param>
        /// <param name="offeringType">Filter by offering type</param>
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
        [Route("organizations/{organizationId}/offerings")]
        [ValidateModelState]
        [SwaggerOperation("OrganizationsOrganizationIdOfferingsGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Offering>), description: "OK")]
        public virtual IActionResult OrganizationsOrganizationIdOfferingsGet([FromRoute][Required] Guid? organizationId, [FromQuery] int? pageSize, [FromQuery] int? pageNumber, [FromQuery] string consumer, [FromQuery] string q, [FromQuery] string teachingLanguage, [FromQuery] string offeringType, [FromQuery] bool? resultExpected, [FromQuery] DateTime? since, [FromQuery] DateTime? until, [FromQuery] List<string> sort)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(InlineResponse20010));

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
        /// GET /organizations/{organizationId}/programs
        /// </summary>
        /// <remarks>Get an ordered list of all programs for a given organization, ordered by name.</remarks>
        /// <param name="organizationId">Organization ID</param>
        /// <param name="pageSize">The number of items per page</param>
        /// <param name="pageNumber">The page number to get. Page numbers start at 1.</param>
        /// <param name="consumer">Request entities meant for a specific consumer. This query parameter is independent from the &#x60;consumers&#x60; attribute. See the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</param>
        /// <param name="q">Filter by items having a name, abbreviation or description containing the given search term (exact partial match, case insensitive)</param>
        /// <param name="teachingLanguage">Filter by teachingLanguage, which is a string describing the main teaching language, should be a three-letter language code as specified by ISO 639-2.</param>
        /// <param name="programType">Filter by program type</param>
        /// <param name="qualificationAwarded">Filter by qualificationAwarded</param>
        /// <param name="levelOfQualification">Filter by levelOfQualification</param>
        /// <param name="sector">Filter by sector</param>
        /// <param name="fieldsOfStudy">Filter by fieldsOfStudy</param>
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
        [Route("organizations/{organizationId}/programs")]
        [ValidateModelState]
        [SwaggerOperation("OrganizationsOrganizationIdProgramsGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Program>), description: "OK")]
        public virtual IActionResult OrganizationsOrganizationIdProgramsGet([FromRoute][Required] Guid? organizationId, [FromQuery] int? pageSize, [FromQuery] int? pageNumber, [FromQuery] string consumer, [FromQuery] string q, [FromQuery] string teachingLanguage, [FromQuery] string programType, [FromQuery] string qualificationAwarded, [FromQuery] string levelOfQualification, [FromQuery] string sector, [FromQuery] string fieldsOfStudy, [FromQuery] List<string> sort)
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
    }
}