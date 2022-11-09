using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ooapi.v5.Attributes;
using ooapi.v5.core.Repositories;
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
public class OrganizationsController : BaseController
{
    public OrganizationsController(IConfiguration configuration, CoreDBContext dbContext, IHttpContextAccessor httpContextAccessor) : base(configuration, dbContext, httpContextAccessor)
    {
    }

    /// <summary>
    /// GET /organizations
    /// </summary>
    /// <remarks>Get an ordered list of all organizations, ordered by name.</remarks>
    /// <param name="primaryCodeParam"></param>
    /// <param name="filterParams"></param>
    /// <param name="pagingParams"></param>
    /// <param name="organizationType">Filter by organization type</param>
    /// <param name="sort">
    ///Default: ["name"]<br/>
    ///Items Enum: "organizationId" "name" "-organizationId" "-name"<br/>
    ///Example: sort=name,-organizationId<br/>
    /// Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("organizations")]
    [ValidateModelState]
    [SwaggerOperation("OrganizationsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Organizations), description: "OK")]
    public virtual IActionResult OrganizationsGet([FromQuery] PrimaryCodeParam primaryCodeParam, [FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] string organizationType, [FromQuery] string sort)
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
    /// <param name="sort">
    ///Default: ["name"]<br/>
    ///Items Enum: "componentId" "name" "-componentId" "-name"<br/>
    ///Example: sort=componentId,-name<br/>
    /// Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("organizations/{organizationId}/components")]
    [ValidateModelState]
    [SwaggerOperation("OrganizationsOrganizationIdComponentsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Components), description: "OK")]
    public virtual IActionResult OrganizationsOrganizationIdComponentsGet([FromRoute][Required] Guid organizationId, [FromQuery] int? pageSize, [FromQuery] int? pageNumber, [FromQuery] string consumer, [FromQuery] string q, [FromQuery] string teachingLanguage, [FromQuery] string componentType, [FromQuery] string sort)
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
    /// <param name="sort">
    ///Default: ["name"]<br/>
    ///Items Enum: "courseId" "name" "-courseId" "-name"<br/>
    ///Example: sort=name,-courseId<br/>
    /// Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("organizations/{organizationId}/courses")]
    [ValidateModelState]
    [SwaggerOperation("OrganizationsOrganizationIdCoursesGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Courses), description: "OK")]
    public virtual IActionResult OrganizationsOrganizationIdCoursesGet([FromRoute][Required] Guid organizationId, [FromQuery] int? pageSize, [FromQuery] int? pageNumber, [FromQuery] string consumer, [FromQuery] string q, [FromQuery] string teachingLanguage, [FromQuery] string level, [FromQuery] List<string> modeOfDelivery, [FromQuery] string sort)
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
    /// <param name="sort">
    ///Default: ["name"]<br/>
    ///Items Enum: "educationSpecificationType" "name" "primarycode" "-educationSpecificationType" "-name" "-primarycode"<br/>
    ///Example: sort=name,-educationSpecificationType<br/>
    /// Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("organizations/{organizationId}/education-specifications")]
    [ValidateModelState]
    [SwaggerOperation("OrganizationsOrganizationIdEducationSpecificationsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(EducationSpecifications), description: "OK")]
    public virtual IActionResult OrganizationsOrganizationIdEducationSpecificationsGet([FromRoute][Required] Guid organizationId, [FromQuery] int? pageSize, [FromQuery] int? pageNumber, [FromQuery] string consumer, [FromQuery] string q, [FromQuery] string educationSpecificationType, [FromQuery] string sort)
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
    [HttpGet]
    [Route("organizations/{organizationId}")]
    [ValidateModelState]
    [SwaggerOperation("OrganizationsOrganizationIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Organization), description: "OK")]
    public virtual IActionResult OrganizationsOrganizationIdGet([FromRoute][Required] Guid organizationId, [FromQuery] List<string> expand)
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
    /// <param name="sort">
    ///Default: ["name"]<br/>
    ///Items Enum: "groupId" "name" "startDate" "-groupId" "-name" "-startDate"<br/>
    ///Example: sort=groupId,-name<br/>
    /// Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("organizations/{organizationId}/groups")]
    [ValidateModelState]
    [SwaggerOperation("OrganizationsOrganizationIdGroupsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Groups), description: "OK")]
    public virtual IActionResult OrganizationsOrganizationIdGroupsGet([FromRoute][Required] Guid organizationId, [FromQuery] int? pageSize, [FromQuery] int? pageNumber, [FromQuery] string consumer, [FromQuery] string q, [FromQuery] string groupType, [FromQuery] string sort)
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
    /// <param name="sort">
    ///Default: ["startDate"]<br/>
    ///Items Enum: "offeringId" "name" "startDate" "endDate" "-offeringId" "-name" "-startDate" "-endDate"<br/>
    ///Example: sort=name,-startDate<br/>
    /// Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("organizations/{organizationId}/offerings")]
    [ValidateModelState]
    [SwaggerOperation("OrganizationsOrganizationIdOfferingsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Offerings), description: "OK")]
    public virtual IActionResult OrganizationsOrganizationIdOfferingsGet([FromRoute][Required] Guid organizationId, [FromQuery] int? pageSize, [FromQuery] int? pageNumber, [FromQuery] string consumer, [FromQuery] string q, [FromQuery] string teachingLanguage, [FromQuery] string offeringType, [FromQuery] bool? resultExpected, [FromQuery] DateTime? since, [FromQuery] DateTime? until, [FromQuery] string sort)
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
    /// <param name="filterParams"></param>
    /// <param name="pagingParams"></param>
    /// <param name="teachingLanguage">Filter by teachingLanguage, which is a string describing the main teaching language, should be a three-letter language code as specified by ISO 639-2.</param>
    /// <param name="programType">Filter by program type</param>
    /// <param name="qualificationAwarded">Filter by qualificationAwarded</param>
    /// <param name="levelOfQualification">Filter by levelOfQualification</param>
    /// <param name="sector">Filter by sector</param>
    /// <param name="fieldsOfStudy">Filter by fieldsOfStudy</param>
    /// <param name="sort">
    ///Default: ["name"]<br/>
    ///Items Enum: "programId" "name" "-programId" "-name"<br/>
    ///Example: sort=name,-programId<br/>
    /// Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("organizations/{organizationId}/programs")]
    [ValidateModelState]
    [SwaggerOperation("OrganizationsOrganizationIdProgramsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Programs), description: "OK")]
    public virtual IActionResult OrganizationsOrganizationIdProgramsGet([FromRoute][Required] Guid organizationId, [FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] string teachingLanguage, [FromQuery] string programType, [FromQuery] string qualificationAwarded, [FromQuery] string levelOfQualification, [FromQuery] string sector, [FromQuery] string fieldsOfStudy, [FromQuery] string sort)
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
