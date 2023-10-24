using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ooapi.v5.Attributes;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;
using ooapi.v5.Models.Params;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace ooapi.v5.Controllers;

/// <summary>
/// API calls for academic sessions
/// </summary>
[ApiController]
public class AcademicSessionsController : BaseController
{
    private readonly IAcademicSessionsService _academicSessionsService;

    /// <summary>
    /// Resolves the required services
    /// </summary>
    /// <param name="academicSessionsService"></param>
    public AcademicSessionsController(IAcademicSessionsService academicSessionsService)
    {
        _academicSessionsService = academicSessionsService;
    }

    /// <summary>
    /// GET /academic-sessions
    /// </summary>
    /// <remarks>Get a list of all academic sessions, ordered chronologically.</remarks>
    /// <param name="primaryCodeParam"></param>
    /// <param name="filterParams"></param>
    /// <param name="pagingParams"></param>
    /// <param name="academicSessionType">
    /// - academic year: academic year <br/>
    /// - semester: semester, typically there are two semesters per academic year <br/>
    /// - trimester: trimester, typically there are three semesters per academic year <br/>
    /// - quarter: quarter, typically there are four quarters per academic year <br/>
    /// - testing period: a period in which tests take place <br/>
    /// - period: any other period in an academic year  <br/>
    /// </param>
    /// <param name="parent">Filter by parent (academicSessionId)</param>
    /// <param name="year">Filter by year (academicSessionId)</param>
    /// <param name="sort">	
    /// Default: ["startDate"] <br/>
    /// Items Enum: "academicSessionId" "name" "startDate" "-academicSessionId" "-name" "-startDate" <br/>
    /// Example: sort=startDate,-academicSessionId <br/>
    /// Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign - allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("academic-sessions")]
    [ValidateModelState]
    [SwaggerOperation("AcademicSessionsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(AcademicSessions), description: "OK")]
    public virtual async Task<IActionResult> AcademicSessionsGetAsync(
        [FromQuery] PrimaryCodeParam? primaryCodeParam,
        [FromQuery] FilterParams? filterParams,
        [FromQuery] PagingParams? pagingParams,
        [FromQuery] string? academicSessionType,
        [FromQuery] Guid? parent,
        [FromQuery] Guid? year,
        [FromQuery] string? sort = "startDate",
        CancellationToken cancellationToken = default)
    {
        var parameters = new DataRequestParameters(primaryCodeParam, filterParams, pagingParams, sort);
        if (parent != null) parameters.Filters.Add("ParentId", parent);
        if (year != null) parameters.Filters.Add("YearId", year);
        var result = await _academicSessionsService.GetAllAsync(parameters, academicSessionType, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// GET /academic-sessions/{academicSessionId}
    /// </summary>
    /// <remarks>Get a single academic session.</remarks>
    /// <param name="academicSessionId">Academic session ID</param>
    /// <param name="expand">Items Enum: "parent" "children" "year" <br/> Optional properties to expand, separated by a comma </param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("academic-sessions/{academicSessionId}")]
    [ValidateModelState]
    [SwaggerOperation("AcademicSessionsAcademicSessionIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(AcademicSession), description: "OK")]
    public virtual async Task<IActionResult> AcademicSessionsAcademicSessionIdGetAsync(
        [FromRoute][Required] Guid academicSessionId,
        [FromQuery] List<string> expand,
        CancellationToken cancellationToken = default)
    {
        var parameters = new DataRequestParameters
        {
            Expand = expand
        };
        var result = await _academicSessionsService.GetAsync(academicSessionId, parameters, cancellationToken);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    ///  <summary>
    ///  GET /academic-sessions/{academicSessionId}/offerings
    ///  </summary>
    ///  <remarks>Get a list of all offerings during this academic session</remarks>
    ///  <param name="academicSessionId">Academic session ID</param>
    ///  <param name="filterParams"></param>
    ///  <param name="pagingParams"></param>
    ///  <param name="teachingLanguage"> = 3 characters ^[a-z]{3}$<br/>
    ///  Filter by teachingLanguage, which is a string describing the main teaching language, should be a three-letter language code as specified by ISO 639-2.<br/>
    ///  Example: teachingLanguage=nld</param>
    ///  <param name="offeringType">Filter by offering type</param>
    ///  <param name="resultExpected">Filter by resultExpected</param>
    ///  <param name="since">Filter all offerings by providing a minimum start date for the corresponding academic session, RFC3339 (full-date). By default only future offerings are shown (equal to &#x60;?since&#x3D;&lt;today&gt;&#x60;).</param>
    ///  <param name="until">Filter all offerings by providing a maximum end date for the corresponding academic session, RFC3339 (full-date).</param>
    ///  <param name="sort">>
    /// Default: ["startDate"]<br/>
    /// Items Enum: "offeringId" "name" "startDate" "endDate" "-offeringId" "-name" "-startDate" "-endDate"<br/>
    /// Example: sort=startDate,-nameSort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    ///  <param name="cancellationToken"></param>
    ///  <response code="200">OK</response>
    [HttpGet]
    [Route("academic-sessions/{academicSessionId}/offerings")]
    [ValidateModelState]
    [SwaggerOperation("AcademicSessionsAcademicSessionIdOfferingsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Offerings), description: "OK")]
    public virtual async Task<IActionResult> AcademicSessionsAcademicSessionIdOfferingsGetAsync(
        [FromRoute][Required] Guid academicSessionId,
        [FromQuery] FilterParams filterParams,
        [FromQuery] PagingParams pagingParams,
        [FromQuery] string? teachingLanguage,
        [FromQuery] string? offeringType,
        [FromQuery] bool? resultExpected,
        [FromQuery] DateTime? since,
        [FromQuery] DateTime? until,
        [FromQuery] string? sort = "startDate",
        CancellationToken cancellationToken = default)
    {
        return BadRequest(new ErrorResponse(400, "Not implemented yet."));
    }
}