using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ooapi.v5.Attributes;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Services;
using ooapi.v5.core.Utility;
using ooapi.v5.Enums;
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
public class CoursesController : BaseController
{

    public CoursesController(IConfiguration configuration, CoreDBContext dbContext) : base(configuration, dbContext)
    {
    }

    /// <summary>
    /// GET /courses/{courseId}/components
    /// </summary>
    /// <remarks>Get an ordered list of all course components.</remarks>
    /// <param name="courseId">Course ID</param>
    /// <param name="filterParams"></param>
    /// <param name="pagingParams"></param>
    /// <param name="teachingLanguage">Filter by teachingLanguage, which is a string describing the main teaching language, should be a three-letter language code as specified by ISO 639-2.</param>
    /// <param name="componentType">Filter by component type</param>
    /// <param name="sort">
    ///Default: ["componentId"]<br/>
    ///Items Enum: "componentId" "name" "-componentId" "-name"<br/>
    ///Example: sort=name,-componentId<br/>
    /// Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("courses/{courseId}/components")]
    [ValidateModelState]
    [SwaggerOperation("CoursesCourseIdComponentsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Components), description: "OK")]
    public virtual IActionResult CoursesCourseIdComponentsGet([FromRoute][Required] Guid courseId, [FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] string teachingLanguage, [FromQuery] string componentType, [FromQuery] string sort)
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
    /// GET /courses/{courseId}
    /// </summary>
    /// <remarks>Get a single course.</remarks>
    /// <param name="courseId">Course ID</param>
    /// <param name="expand">Optional properties to include, separated by a comma</param>
    /// <param name="returnTimelineOverrides">Controls whether the attribute &#x60;timelineOverrides&#x60; is returned or not. The default is &#x60;false&#x60;, so this has to explicitly set to &#x60;true&#x60; if a client needs the timeline overrides. See [GET /education-specifications/{educationSpecificationId}](#tag/education-specifications/paths/~1education-specifications~1{educationSpecificationId}/get) for an example.</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("courses/{courseId}")]
    [ValidateModelState]
    [SwaggerOperation("CoursesCourseIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Course), description: "OK")]
    public virtual IActionResult CoursesCourseIdGet([FromRoute][Required] Guid courseId, [FromQuery] List<string> expand, [FromQuery] bool? returnTimelineOverrides)
    {
        var service = new CoursesService(DBContext, UserRequestContext);
        var result = service.Get(courseId, out ErrorResponse errorResponse);
        if (result == null)
        {
            return BadRequest(errorResponse);
        }
        return Ok(result);
    }

    /// <summary>
    /// GET /courses/{courseId}/offerings
    /// </summary>
    /// <remarks>Get a list of all offerings for this course, ordered chronologically.</remarks>
    /// <param name="courseId">Course ID</param>
    /// <param name="filterParams"></param>
    /// <param name="pagingParams"></param>
    /// <param name="teachingLanguage">Filter by teachingLanguage, which is a string describing the main teaching language, should be a three-letter language code as specified by ISO 639-2.</param>
    /// <param name="modeOfDelivery">Filter by modeOfDelivery</param>
    /// <param name="resultExpected">Filter by resultExpected</param>
    /// <param name="since">Filter all offerings by providing a minimum start date for the corresponding academic session, RFC3339 (full-date). By default only future offerings are shown (equal to &#x60;?since&#x3D;&lt;today&gt;&#x60;).</param>
    /// <param name="until">Filter all offerings by providing a maximum end date for the corresponding academic session, RFC3339 (full-date).</param>
    /// <param name="sort">
    ///Default: ["startDate"]<br/>
    ///Items Enum: "offeringId" "name" "startDate" "endDate" "-offeringId" "-name" "-startDate" "-endDate"<br/>
    ///Example: sort=offeringId,-endDate<br/>
    /// Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("courses/{courseId}/offerings")]
    [ValidateModelState]
    [SwaggerOperation("CoursesCourseIdOfferingsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(CourseOfferings), description: "OK")]
    public virtual IActionResult CoursesCourseIdOfferingsGet([FromRoute][Required] Guid courseId, [FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] string teachingLanguage, [FromQuery] List<string> modeOfDelivery, [FromQuery] bool? resultExpected, [FromQuery] DateTime? since, [FromQuery] DateTime? until, [FromQuery] string sort)
    {
        //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(200, default(InlineResponse20018));

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
    /// GET /courses
    /// </summary>
    /// <remarks>Get a list of all courses, ordered by name (ascending).</remarks>
    /// <param name="primaryCodeParam"></param>
    /// <param name="filterParams"></param>
    /// <param name="pagingParams"></param>
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
    [Route("courses")]
    [ValidateModelState]
    [SwaggerOperation("CoursesGet")]
    //[SwaggerResponse(statusCode: 200, type: typeof(MyPagination<Course>), description: "OK")]
    [SwaggerResponse(statusCode: 200, type: typeof(Courses), description: "OK")]
    public virtual IActionResult CoursesGet([FromQuery] PrimaryCodeParam primaryCodeParam, [FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] string teachingLanguage, [FromQuery] LevelEnum level, [FromQuery] ModeOfDeliveryEnum modeOfDelivery, [FromQuery] string sort)
    {
        DataRequestParameters parameters = new DataRequestParameters(primaryCodeParam, filterParams, pagingParams, sort);
        var service = new CoursesService(DBContext, UserRequestContext);
        var result = service.GetAll(parameters, out ErrorResponse errorResponse);
        if (result == null)
        {
            return BadRequest(errorResponse);
        }
        return Ok(result);
    }
}
