using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ooapi.v5.Attributes;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Services;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;
using ooapi.v5.Models.Params;
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
    public class ProgramsController : BaseController
    {
        public ProgramsController(IConfiguration configuration, CoreDBContext dbContext) : base(configuration, dbContext)
        {
        }

        /// <summary>
        /// GET /programs
        /// </summary>
        /// <remarks>Get an ordered list of all programs, ordered by name.</remarks>
        /// <param name="primaryCodeParam"></param>
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
        ///Example: sort=name,programId<br/>
        /// Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("programs")]
        [ValidateModelState]
        [SwaggerOperation("ProgramsGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(Programs), description: "OK")]
        public virtual IActionResult ProgramsGet([FromQuery] PrimaryCodeParam primaryCodeParam, [FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] string? teachingLanguage, [FromQuery] string? programType, [FromQuery] string? qualificationAwarded, [FromQuery] string? levelOfQualification, [FromQuery] string? sector, [FromQuery] string? fieldsOfStudy, [FromQuery] string? sort = "name")
        {
            DataRequestParameters parameters = new DataRequestParameters(primaryCodeParam, filterParams, pagingParams, sort);
            var service = new ProgramsService(DBContext, UserRequestContext);
            var result = service.GetAll(parameters, out ErrorResponse errorResponse);
            if (result == null)
            {
                return BadRequest(errorResponse);
            }
            return Ok(result);
        }

        /// <summary>
        /// GET /programs/{programId}/courses
        /// </summary>
        /// <remarks>Get an ordered list of all courses given through this program.</remarks>
        /// <param name="programId">Program ID</param>
        /// <param name="filterParams"></param>
        /// <param name="pagingParams"></param>
        /// <param name="teachingLanguage">Filter by teachingLanguage, which is a string describing the main teaching language, should be a three-letter language code as specified by ISO 639-2.</param>
        /// <param name="level">Filter by level</param>
        /// <param name="modeOfDelivery">Filter by modeOfDelivery</param>
        /// <param name="sort">
        ///Default: ["courseId"]<br/>
        ///Items Enum: "courseId" "name" "-courseId" "-name"<br/>
        ///Example: sort=name,-courseId<br/>
        /// Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("programs/{programId}/courses")]
        [ValidateModelState]
        [SwaggerOperation("ProgramsProgramIdCoursesGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(Courses), description: "OK")]
        public virtual IActionResult ProgramsProgramIdCoursesGet([FromRoute][Required] Guid programId, [FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] string? teachingLanguage, [FromQuery] string? level, [FromQuery] List<string>? modeOfDelivery, [FromQuery] string? sort = "courseId")
        {
            DataRequestParameters parameters = new DataRequestParameters(filterParams, pagingParams, sort);
            var service = new CoursesService(DBContext, UserRequestContext);
            var result = service.GetCoursesByProgramId(parameters, programId, out ErrorResponse errorResponse);
            if (result == null)
            {
                return BadRequest(errorResponse);
            }
            return Ok(result);
        }

        /// <summary>
        /// GET /programs/{programId}
        /// </summary>
        /// <remarks>Get a single program.</remarks>
        /// <param name="programId">Program ID</param>
        /// <param name="expand">Items Enum: "parent" "children" "coordinators" "organization" "educationSpecification" <br/>Optional properties to expand, separated by a comma</param>
        /// <param name="returnTimelineOverrides">Controls whether the attribute &#x60;timelineOverrides&#x60; is returned or not. The default is &#x60;false&#x60;, so this has to explicitly set to &#x60;true&#x60; if a client needs the timeline overrides. See [GET /education-specifications/{educationSpecificationId}](#tag/education-specifications/paths/~1education-specifications~1{educationSpecificationId}/get) for an example.</param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("programs/{programId}")]
        [ValidateModelState]
        [SwaggerOperation("ProgramsProgramIdGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(Models.Program), description: "OK")]
        public virtual IActionResult ProgramsProgramIdGet([FromRoute][Required] Guid programId, [FromQuery] List<string> expand, [FromQuery] bool? returnTimelineOverrides)
        {
            DataRequestParameters parameters = new DataRequestParameters();
            parameters.Expand = expand;
            var service = new ProgramsService(DBContext, UserRequestContext);
            var result = service.Get(programId, parameters, out ErrorResponse errorResponse);
            if (result == null)
            {
                return BadRequest(errorResponse);
            }
            return Ok(result);
        }

        /// <summary>
        /// GET /programs/{programId}/offerings
        /// </summary>
        /// <remarks>Get a list of all offerings for this program, ordered chronologically.</remarks>
        /// <param name="programId">Program ID</param>
        /// <param name="filterParams"></param>
        /// <param name="pagingParams"></param>
        /// <param name="teachingLanguage">Filter by teachingLanguage, which is a string describing the main teaching language, should be a three-letter language code as specified by ISO 639-2.</param>
        /// <param name="modeOfStudy">Filter by modeOfStudy</param>
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
        [Route("programs/{programId}/offerings")]
        [ValidateModelState]
        [SwaggerOperation("ProgramsProgramIdOfferingsGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(ProgramOfferings), description: "OK")]
        public virtual IActionResult ProgramsProgramIdOfferingsGet([FromRoute][Required] Guid programId, [FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] string? teachingLanguage, [FromQuery] string? modeOfStudy, [FromQuery] bool? resultExpected, [FromQuery] DateTime? since, [FromQuery] DateTime? until, [FromQuery] string? sort = "startDate")
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(InlineResponse20016));

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
        /// GET /programs/{programId}/programs
        /// </summary>
        /// <remarks>Get an ordered list of nested programs, ordered by name.</remarks>
        /// <param name="programId">the id of the program to find nested programs for</param>
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
        [Route("programs/{programId}/programs")]
        [ValidateModelState]
        [SwaggerOperation("ProgramsProgramIdProgramsGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(Programs), description: "OK")]
        public virtual IActionResult ProgramsProgramIdProgramsGet([FromRoute][Required] Guid programId, [FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] string? teachingLanguage, [FromQuery] string? programType, [FromQuery] string? qualificationAwarded, [FromQuery] string? levelOfQualification, [FromQuery] string? sector, [FromQuery] string? fieldsOfStudy, [FromQuery] string? sort = "name")
        {
            DataRequestParameters parameters = new DataRequestParameters(filterParams, pagingParams, sort);
            var service = new ProgramsService(DBContext, UserRequestContext);
            var result = service.GetProgramsByProgramId(parameters, programId, out ErrorResponse errorResponse);
            if (result == null)
            {
                return BadRequest(errorResponse);
            }
            return Ok(result);
        }
    }
}
