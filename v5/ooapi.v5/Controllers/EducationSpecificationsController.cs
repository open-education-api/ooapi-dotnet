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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ooapi.v5.Controllers;

/// <summary>
/// API calls for education specifications
/// </summary>
[ApiController]
public class EducationSpecificationsController : BaseController
{
    private readonly ICoursesService _coursesService;
    private readonly IEducationSpecificationsService _educationSpecificationsService;
    private readonly IProgramsService _programsService;

    /// <summary>
    /// Resolves the required services
    /// </summary>
    /// <param name="educationSpecificationsService"></param>
    /// <param name="coursesService"></param>
    /// <param name="programsService"></param>
    public EducationSpecificationsController(IEducationSpecificationsService educationSpecificationsService, ICoursesService coursesService, IProgramsService programsService)
    {
        _educationSpecificationsService = educationSpecificationsService;
        _coursesService = coursesService;
        _programsService = programsService;
    }

    ///  <summary>
    ///  GET /education-specifications/{educationSpecificationId}/courses
    ///  </summary>
    ///  <remarks>Get an ordered list of all courses given through this EducationSpecification.</remarks>
    ///  <param name="educationSpecificationId">Education Specification ID</param>
    ///  <param name="filterParams"></param>
    ///  <param name="pagingParams"></param>
    ///  <param name="teachingLanguage">Filter by teachingLanguage, which is a string describing the main teaching language, should be a three-letter language code as specified by ISO 639-2.</param>
    ///  <param name="level">Filter by level</param>
    ///  <param name="modeOfDelivery">Filter by modeOfDelivery</param>
    ///  <param name="sort">
    /// Default: ["courseId"]<br/>
    /// Items Enum: "courseId" "name" "-courseId" "-name"<br/>
    /// Example: sort=courseId,-name<br/>
    ///  Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    ///  <param name="cancellationToken"></param>
    ///  <response code="200">OK</response>
    [HttpGet]
    [Route("education-specifications/{educationSpecificationId}/courses")]
    [ValidateModelState]
    [SwaggerOperation("EducationSpecificationsEducationSpecificationIdCoursesGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Courses), description: "OK")]
    public virtual async Task<IActionResult> EducationSpecificationsEducationSpecificationIdCoursesGetAsync(
        [FromRoute][Required] Guid educationSpecificationId,
        [FromQuery] FilterParams filterParams,
        [FromQuery] PagingParams pagingParams,
        [FromQuery] string? teachingLanguage,
        [FromQuery] string? level,
        [FromQuery] List<string>? modeOfDelivery,
        [FromQuery] string? sort = "courseId",
        CancellationToken cancellationToken = default)
    {
        var parameters = new DataRequestParameters(filterParams, pagingParams, sort);
        if (!string.IsNullOrWhiteSpace(teachingLanguage))
        {
            parameters.Filters.Add("TeachingLanguage", teachingLanguage);
        }
        if (!string.IsNullOrWhiteSpace(level))
        {
            parameters.Filters.Add("Level", level);
        }
        if (modeOfDelivery != null && modeOfDelivery.Any())
        {
            parameters.Filters.Add("ModeOfDelivery", modeOfDelivery);
        }
        var result = await _coursesService.GetCoursesByEducationSpecificationIdAsync(parameters, educationSpecificationId, cancellationToken);
        return Ok(result);
    }

    ///  <summary>
    ///  GET /education-specifications/{educationSpecificationId}/education-specifications
    ///  </summary>
    ///  <remarks>Get an ordered list of all education-specifications given through this educationspecification.</remarks>
    ///  <param name="educationSpecificationId">Education Specification ID</param>
    ///  <param name="filterParams"></param>
    ///  <param name="pagingParams"></param>
    ///  <param name="sort">
    /// Default: ["educationSpecificationId"]<br/>
    /// Items Enum: "educationSpecificationId" "name" "educationSpecificationType" "-educationSpecificationId" "-name" "-educationSpecificationType"<br/>
    /// Example: sort=educationSpecificationId,-name<br/>
    ///  Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    ///  <param name="cancellationToken"></param>
    ///  <response code="200">OK</response>
    [HttpGet]
    [Route("education-specifications/{educationSpecificationId}/education-specifications")]
    [ValidateModelState]
    [SwaggerOperation("EducationSpecificationsEducationSpecificationIdEducationSpecificationsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(EducationSpecifications), description: "OK")]
    public virtual async Task<IActionResult> EducationSpecificationsEducationSpecificationIdEducationSpecificationsGetAsync(
        [FromRoute][Required] Guid educationSpecificationId,
        [FromQuery] FilterParams filterParams,
        [FromQuery] PagingParams pagingParams,
        [FromQuery] string? sort = "educationSpecificationId",
        CancellationToken cancellationToken = default)
    {
        var parameters = new DataRequestParameters(filterParams, pagingParams, sort);
        var result = await _educationSpecificationsService.GetEducationSpecificationsByEducationSpecificationIdAsync(parameters, educationSpecificationId, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// GET /education-specifications/{educationSpecificationId}
    /// </summary>
    /// <remarks>Get a single education specification.</remarks>
    /// <param name="educationSpecificationId">Education specification ID</param>
    /// <param name="returnTimelineOverrides">Controls whether the attribute &#x60;timelineOverrides&#x60; is returned or not. The default is &#x60;false&#x60;, so this has to explicitly set to &#x60;true&#x60; if a client needs the timeline overrides. See [GET /education-specifications/{educationSpecificationId}](#tag/education-specifications/paths/~1education-specifications~1{educationSpecificationId}/get) for an example.</param>
    /// <param name="expand">Items Enum: "parent" "children" "organization"  <br/>Optional properties to expand, separated by a comma</param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("education-specifications/{educationSpecificationId}")]
    [ValidateModelState]
    [SwaggerOperation("EducationSpecificationsEducationSpecificationIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(EducationSpecification), description: "OK")]
    public virtual async Task<IActionResult> EducationSpecificationsEducationSpecificationIdGetAsync(
        [FromRoute][Required] Guid educationSpecificationId,
        [FromQuery] bool? returnTimelineOverrides,
        [FromQuery] List<string> expand,
        CancellationToken cancellationToken = default)
    {
        var parameters = new DataRequestParameters
        {
            Expand = expand
        };
        var result = await _educationSpecificationsService.GetAsync(educationSpecificationId, parameters, cancellationToken);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    ///  <summary>
    ///  GET /education-specifications/{educationSpecificationId}/programs
    ///  </summary>
    ///  <remarks>Get an ordered list of all programs for a given education specification, ordered by name.</remarks>
    ///  <param name="educationSpecificationId">Education Specification ID</param>
    ///  <param name="filterParams"></param>
    ///  <param name="pagingParams"></param>
    ///  <param name="teachingLanguage">Filter by teachingLanguage, which is a string describing the main teaching language, should be a three-letter language code as specified by ISO 639-2.</param>
    ///  <param name="programType">Filter by program type</param>
    ///  <param name="qualificationAwarded">Filter by qualificationAwarded</param>
    ///  <param name="levelOfQualification">Filter by levelOfQualification</param>
    ///  <param name="sector">Filter by sector</param>
    ///  <param name="fieldsOfStudy">Filter by fieldsOfStudy</param>
    ///  <param name="crohoCreboCode">Filter by croho-creboCode</param>
    ///  <param name="sort">
    /// Default: ["name"]<br/>
    /// Items Enum: "programId" "name" "-programId" "-name"<br/>
    /// Example: sort=programId,-name<br/>
    ///  Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    ///  <param name="cancellationToken"></param>
    ///  <response code="200">OK</response>
    [HttpGet]
    [Route("education-specifications/{educationSpecificationId}/programs")]
    [ValidateModelState]
    [SwaggerOperation("EducationSpecificationsEducationSpecificationIdProgramsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Programs), description: "OK")]
    public virtual async Task<IActionResult> EducationSpecificationsEducationSpecificationIdProgramsGetAsync(
        [FromRoute][Required] Guid educationSpecificationId,
        [FromQuery] FilterParams filterParams,
        [FromQuery] PagingParams pagingParams,
        [FromQuery] string? teachingLanguage,
        [FromQuery] string? programType,
        [FromQuery] string? qualificationAwarded,
        [FromQuery] string? levelOfQualification,
        [FromQuery] string? sector,
        [FromQuery] string? fieldsOfStudy,
        [FromQuery] string? crohoCreboCode,
        [FromQuery] string? sort = "name",
        CancellationToken cancellationToken = default)
    {
        var parameters = new DataRequestParameters(filterParams, pagingParams, sort);
        if (!string.IsNullOrWhiteSpace(teachingLanguage))
        {
            parameters.Filters.Add("TeachingLanguage", teachingLanguage);
        }
        if (!string.IsNullOrWhiteSpace(programType))
        {
            parameters.Filters.Add("ProgramType", programType);
        }
        if (!string.IsNullOrWhiteSpace(qualificationAwarded))
        {
            parameters.Filters.Add("QualificationAwarded", qualificationAwarded);
        }
        if (!string.IsNullOrWhiteSpace(levelOfQualification))
        {
            parameters.Filters.Add("LevelOfQualification", levelOfQualification);
        }
        if (!string.IsNullOrWhiteSpace(sector))
        {
            parameters.Filters.Add("Sector", sector);
        }
        if (!string.IsNullOrWhiteSpace(fieldsOfStudy))
        {
            parameters.Filters.Add("FieldsOfStudy", fieldsOfStudy);
        }
        if (!string.IsNullOrWhiteSpace(crohoCreboCode))
        {
            parameters.Filters.Add("PrimaryCode", crohoCreboCode);
        }

        var result = await _programsService.GetProgramsByEducationSpecificationIdAsync(parameters, educationSpecificationId, cancellationToken);
        return Ok(result);
    }

    ///  <summary>
    ///  GET /education-specifications
    ///  </summary>
    ///  <remarks>Get a list of all education specifications, ordered by name (ascending).</remarks>
    ///  <param name="primaryCodeParam"></param>
    ///  <param name="filterParams"></param>
    ///  <param name="pagingParams"></param>
    ///  <param name="educationSpecificationType">Filter by type of education specification</param>
    ///  <param name="sort">
    /// Default: ["name"]<br/>
    /// Items Enum: "educationSpecificationType" "name" "primaryCode" "-educationSpecificationType" "-name" "-primaryCode"<br/>
    /// Example: sort=educationSpecificationType,-primaryCode<br/>
    ///  Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    ///  <param name="cancellationToken"></param>
    ///  <response code="200">OK</response>
    [HttpGet]
    [Route("education-specifications")]
    [ValidateModelState]
    [SwaggerOperation("EducationSpecificationsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(EducationSpecifications), description: "OK")]
    public virtual async Task<IActionResult> EducationSpecificationsGetAsync(
        [FromQuery] PrimaryCodeParam? primaryCodeParam,
        [FromQuery] FilterParams? filterParams,
        [FromQuery] PagingParams? pagingParams,
        [FromQuery] string? educationSpecificationType = "",
        [FromQuery] string? sort = "name",
        CancellationToken cancellationToken = default)
    {
        var parameters = new DataRequestParameters(filterParams, pagingParams, sort);
        if (!string.IsNullOrWhiteSpace(primaryCodeParam?.primaryCode))
        {
            parameters.Filters.Add("PrimaryCode", primaryCodeParam.primaryCode);
        }
        if (!string.IsNullOrWhiteSpace(educationSpecificationType))
        {
            parameters.Filters.Add("EducationSpecificationType", educationSpecificationType);
        }
        var result = await _educationSpecificationsService.GetAllAsync(parameters, cancellationToken);
        return Ok(result);
    }
}