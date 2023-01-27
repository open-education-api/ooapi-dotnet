using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ooapi.v5.Attributes;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Services;
using ooapi.v5.core.Utility;
using ooapi.v5.Enums;
using ooapi.v5.Filters;
using ooapi.v5.Models;
using ooapi.v5.Models.Params;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ooapi.v5.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
[ValidateModelStateAttribute]
public class AcademicSessionsController : BaseController
{

    public AcademicSessionsController(IConfiguration configuration, CoreDBContext dbContext) : base(configuration, dbContext)
    {
    }

    /// <summary>
    /// GET /academic-sessions
    /// </summary>
    /// <remarks>Get a list of all academic sessions, ordered chronologically.</remarks>
    /// <param name="primaryCodeParam"></param>
    /// <param name="filterParams"></param>
    /// <param name="pagingParams"></param>
    /// <param name="academicSessionType">Filter by academic session type <br/> Example: academicSessionType=semester</param>
    /// <param name="parent">Filter by parent (academicSessionId)</param>
    /// <param name="year">Filter by year (academicSessionId)</param>
    /// <param name="sort">	
    /// Default: ["startDate"] <br/>
    /// Items Enum: "academicSessionId" "name" "startDate" "-academicSessionId" "-name" "-startDate" <br/>
    /// Example: sort=startDate,-academicSessionId <br/>
    /// Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign - allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("academic-sessions")]
    [ValidateModelState]
    [SwaggerOperation("AcademicSessionsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(AcademicSessions), description: "OK")]
    public virtual IActionResult AcademicSessionsGet([FromQuery] PrimaryCodeParam primaryCodeParam, [FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] AcademicSessionTypeEnum? academicSessionType, [FromQuery] Guid? parent, [FromQuery] Guid? year, [FromQuery] string sort = "startDate")
    {
        DataRequestParameters parameters = new DataRequestParameters(primaryCodeParam, filterParams, pagingParams, sort);
        var service = new AcademicSessionsService(DBContext, UserRequestContext);
        var result = service.GetAll(parameters, out ErrorResponse errorResponse, academicSessionType);
        if (result == null)
        {
            return BadRequest(errorResponse);
        }
        return Ok(result);
    }

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
    public virtual IActionResult AcademicSessionsAcademicSessionIdGet([FromRoute][Required] Guid academicSessionId, [FromQuery] string expand)
    {
        var service = new AcademicSessionsService(DBContext, UserRequestContext);
        var result = service.Get(academicSessionId, out ErrorResponse errorResponse);
        if (result == null)
        {
            return BadRequest(errorResponse);
        }
        return Ok(result);
    }

    /// <summary>
    /// GET /academic-sessions/{academicSessionId}/offerings
    /// </summary>
    /// <remarks>Get a list of all offerings during this academic session</remarks>
    /// <param name="academicSessionId">Academic session ID</param>
    /// <param name="filterParams"></param>
    /// <param name="pagingParams"></param>
    /// <param name="teachingLanguage"> = 3 characters ^[a-z]{3}$<br/>
    /// Filter by teachingLanguage, which is a string describing the main teaching language, should be a three-letter language code as specified by ISO 639-2.<br/>
    /// Example: teachingLanguage=nld</param>
    /// <param name="offeringType">Filter by offering type</param>
    /// <param name="resultExpected">Filter by resultExpected</param>
    /// <param name="since">Filter all offerings by providing a minimum start date for the corresponding academic session, RFC3339 (full-date). By default only future offerings are shown (equal to &#x60;?since&#x3D;&lt;today&gt;&#x60;).</param>
    /// <param name="until">Filter all offerings by providing a maximum end date for the corresponding academic session, RFC3339 (full-date).</param>
    /// <param name="sort">>
    ///Default: ["startDate"]<br/>
    ///Items Enum: "offeringId" "name" "startDate" "endDate" "-offeringId" "-name" "-startDate" "-endDate"<br/>
    ///Example: sort=startDate,-nameSort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("academic-sessions/{academicSessionId}/offerings")]
    [ValidateModelState]
    [SwaggerOperation("AcademicSessionsAcademicSessionIdOfferingsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Offerings), description: "OK")]
    public virtual IActionResult AcademicSessionsAcademicSessionIdOfferingsGet([FromRoute][Required] Guid academicSessionId, [FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] string? teachingLanguage, [FromQuery] string? offeringType, [FromQuery] bool? resultExpected, [FromQuery] DateTime? since, [FromQuery] DateTime? until, [FromQuery] string sort = "startDate")
    {
        DataRequestParameters parameters = new DataRequestParameters(filterParams, pagingParams, sort);
        //var service = new OfferingsService(DBContext, UserRequestContext);
        //var result = service.GetAll(parameters, out ErrorResponse errorResponse);
        //if (result == null)
        //{
        //    return BadRequest(errorResponse);
        //}
        return null;
    }
}
