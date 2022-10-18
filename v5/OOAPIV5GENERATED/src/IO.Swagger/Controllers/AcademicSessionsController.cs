
using IO.Swagger.Attributes;
using IO.Swagger.Enums.Params;
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
    public class AcademicSessionsController : BaseController
    {
        /// <summary>
        /// GET /academic-sessions/{academicSessionId}
        /// </summary>
        /// <remarks>Get a single academic session.</remarks>
        /// <param name="academicSessionId">Academic session ID</param>
        /// <param name="expand">Optional properties to expand, separated by a comma <br/>Items Enum: "parent" "children" "year" </param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("academic-sessions/{academicSessionId}")]
        [ValidateModelState]
        [SwaggerOperation("AcademicSessionsAcademicSessionIdGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(AcademicSession), description: "OK")]
        public virtual IActionResult AcademicSessionsAcademicSessionIdGet([FromRoute][Required] Guid? academicSessionId, [FromQuery] string expand)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(InlineResponse20014));

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
        /// GET /academic-sessions/{academicSessionId}/offerings
        /// </summary>
        /// <remarks>Get a list of all offerings during this academic session</remarks>
        /// <param name="academicSessionId">Academic session ID</param>
        /// <param name="filterModel"></param>
        /// <param name="pagingModel"></param>
        /// <param name="teachingLanguage">Filter by teachingLanguage, which is a string describing the main teaching language, should be a three-letter language code as specified by ISO 639-2.</param>
        /// <param name="offeringType">Filter by offering type</param>
        /// <param name="resultExpected">Filter by resultExpected</param>
        /// <param name="since">Filter all offerings by providing a minimum start date for the corresponding academic session, RFC3339 (full-date). By default only future offerings are shown (equal to &#x60;?since&#x3D;&lt;today&gt;&#x60;).</param>
        /// <param name="until">Filter all offerings by providing a maximum end date for the corresponding academic session, RFC3339 (full-date).</param>
        /// <param name="sort">Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("academic-sessions/{academicSessionId}/offerings")]
        [ValidateModelState]
        [SwaggerOperation("AcademicSessionsAcademicSessionIdOfferingsGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Offering>), description: "OK")]
        public virtual IActionResult AcademicSessionsAcademicSessionIdOfferingsGet([FromRoute][Required] Guid? academicSessionId, [FromQuery] FilterModel filterModel, [FromQuery] PagingModel pagingModel, [FromQuery] int? pageSize, [FromQuery] int? pageNumber, [FromQuery] string consumer, [FromQuery] string q, [FromQuery] string teachingLanguage, [FromQuery] string offeringType, [FromQuery] bool? resultExpected, [FromQuery] DateTime? since, [FromQuery] DateTime? until, [FromQuery] List<string> sort)
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
        /// GET /academic-sessions
        /// </summary>
        /// <remarks>Get a list of all academic sessions, ordered chronologically.</remarks>
        /// <param name="primaryCodeModel"></param>
        /// <param name="filterModel"></param>
        /// <param name="pagingModel"></param>
        /// <param name="academicSessionType">Filter by academic session type <br/> Example: academicSessionType=semester</param>
        /// <param name="parent">Filter by parent (academicSessionId)</param>
        /// <param name="year">Filter by year (academicSessionId)</param>
        /// <param name="sort">	
        /// Array of strings <br/>
        /// Default: ["startDate"] <br/>
        /// Items Enum: "academicSessionId" "name" "startDate" "-academicSessionId" "-name" "-startDate" <br/>
        /// Example: sort=startDate,-academicSessionId <br/>
        /// Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign - allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("academic-sessions")]
        [ValidateModelState]
        [SwaggerOperation("AcademicSessionsGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<AcademicSession>), description: "OK")]
        public virtual IActionResult AcademicSessionsGet([FromQuery] PrimaryCodeModel primaryCodeModel, [FromQuery] FilterModel filterModel, [FromQuery] PagingModel pagingModel, [FromQuery] string academicSessionType, [FromQuery] Guid? parent, [FromQuery] Guid? year, [FromQuery] string sort)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(InlineResponse20013));

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


        /////// <summary>
        /////// GET /academic-sessions
        /////// </summary>
        /////// <remarks>Get a list of all academic sessions, ordered chronologically.</remarks>
        /////// <param name="primaryCode">The primaryCode of the requested item. This is often the source identifier as defined by the institution.</param>
        /////// <param name="pageSize">The number of items per page.  Default: 10 Enum: 10 20 50 100 250 </param>
        /////// <param name="pageNumber">The page number to get. Page numbers start at 1.</param>
        /////// <param name="consumer">Request entities meant for a specific consumer. This query parameter is independent from the &#x60;consumers&#x60; attribute. See the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</param>
        /////// <param name="academicSessionType">Filter by academic session type</param>
        /////// <param name="parent">Filter by parent (academicSessionId)</param>
        /////// <param name="year">Filter by year (academicSessionId)</param>
        /////// <param name="sort">Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
        /////// <response code="200">OK</response>
        ////[HttpGet]
        ////[Route("academic-sessions")]
        ////[ValidateModelState]
        ////[SwaggerOperation("AcademicSessionsGet")]
        ////[SwaggerResponse(statusCode: 200, type: typeof(List<AcademicSession>), description: "OK")]
        ////public virtual IActionResult AcademicSessionsGet([FromQuery] string primaryCode, [FromQuery] PageSizeEnum pageSize = PageSizeEnum.Ten, [FromQuery] int? pageNumber = 1, [FromQuery] string consumer = "", [FromQuery] string academicSessionType = "", [FromQuery] Guid? parent = null, [FromQuery] Guid? year = null, [FromQuery] List<string> sort = null)
        ////{
        ////    //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        ////    // return StatusCode(200, default(InlineResponse20013));

        ////    //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        ////    // return StatusCode(400, default(InlineResponse400));

        ////    //TODO: Uncomment the next line to return response 401 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        ////    // return StatusCode(401, default(InlineResponse400));

        ////    //TODO: Uncomment the next line to return response 403 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        ////    // return StatusCode(403, default(InlineResponse400));

        ////    //TODO: Uncomment the next line to return response 405 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        ////    // return StatusCode(405, default(InlineResponse400));

        ////    //TODO: Uncomment the next line to return response 429 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        ////    // return StatusCode(429, default(InlineResponse400));

        ////    //TODO: Uncomment the next line to return response 500 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        ////    // return StatusCode(500, default(InlineResponse400));

        ////    return null;
        ////}


    }
}
