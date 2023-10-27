using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ooapi.v5.Attributes;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Enums;
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
/// API calls for organizations
/// </summary>
[ApiController]
public class OrganizationsController : BaseController
{
    private readonly IOrganizationsService _organizationsService;
    private readonly IComponentsService _componentsService;
    private readonly ICoursesService _coursesService;
    private readonly IEducationSpecificationsService _educationSpecificationsService;
    private readonly IGroupsService _groupsService;
    private readonly IProgramsService _programsService;

    /// <summary>
    /// Resolves the required services
    /// </summary>
    /// <param name="organizationsService"></param>
    /// <param name="componentsService"></param>
    /// <param name="coursesService"></param>
    /// <param name="educationSpecificationsService"></param>
    /// <param name="groupsService"></param>
    /// <param name="programsService"></param>
    public OrganizationsController(IOrganizationsService organizationsService, IComponentsService componentsService, ICoursesService coursesService, IEducationSpecificationsService educationSpecificationsService, IGroupsService groupsService, IProgramsService programsService)
    {
        _organizationsService = organizationsService;
        _componentsService = componentsService;
        _coursesService = coursesService;
        _educationSpecificationsService = educationSpecificationsService;
        _groupsService = groupsService;
        _programsService = programsService;
    }

    ///  <summary>
    ///  GET /organizations
    ///  </summary>
    ///  <remarks>Get an ordered list of all organizations, ordered by name.</remarks>
    ///  <param name="primaryCodeParam"></param>
    ///  <param name="filterParams"></param>
    ///  <param name="pagingParams"></param>
    ///  <param name="organizationType">Filter by organization type</param>
    ///  <param name="sort">
    /// Default: ["name"]<br/>
    /// Items Enum: "organizationId" "name" "-organizationId" "-name"<br/>
    /// Example: sort=name,-organizationId<br/>
    ///  Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    ///  <param name="cancellationToken"></param>
    ///  <response code="200">OK</response>
    [HttpGet]
    [Route("organizations")]
    [ValidateModelState]
    [SwaggerOperation("OrganizationsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Organizations), description: "OK")]
    public virtual async Task<IActionResult> OrganizationsGetAsync(
        [FromQuery] PrimaryCodeParam? primaryCodeParam,
        [FromQuery] FilterParams? filterParams,
        [FromQuery] PagingParams? pagingParams,
        [FromQuery] OrganizationType? organizationType,
        [FromQuery] string? sort = "name",
        CancellationToken cancellationToken = default)
    {
        var parameters = new DataRequestParameters(filterParams, pagingParams, sort);
        if (!string.IsNullOrWhiteSpace(primaryCodeParam?.primaryCode))
        {
            parameters.Filters.Add("primaryCode", primaryCodeParam.primaryCode);
        }
        if (!string.IsNullOrWhiteSpace(organizationType.ToString()))
        {
            parameters.Filters.Add("OrganizationType", organizationType.ToString()!);
        }
        var result = await _organizationsService.GetAllAsync(parameters, cancellationToken);
        return Ok(result);
    }

    ///  <summary>
    ///  GET /organizations/{organizationId}/components
    ///  </summary>
    ///  <remarks>Get an ordered list of all components for a given organization, ordered by name.</remarks>
    ///  <param name="organizationId">Organization ID</param>
    ///  <param name="filterParams"></param>
    ///  <param name="pagingParams"></param>
    ///  <param name="teachingLanguage">Filter by teachingLanguage, which is a string describing the main teaching language, should be a three-letter language code as specified by ISO 639-2.</param>
    ///  <param name="componentType">Filter by component type</param>
    ///  <param name="sort">
    /// Default: ["name"]<br/>
    /// Items Enum: "componentId" "name" "-componentId" "-name"<br/>
    /// Example: sort=componentId,-name<br/>
    ///  Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    ///  <param name="cancellationToken"></param>
    ///  <response code="200">OK</response>
    [HttpGet]
    [Route("organizations/{organizationId}/components")]
    [ValidateModelState]
    [SwaggerOperation("OrganizationsOrganizationIdComponentsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Components), description: "OK")]
    public virtual async Task<IActionResult> OrganizationsOrganizationIdComponentsGetAsync(
        [FromRoute][Required] Guid organizationId,
        [FromQuery] FilterParams filterParams,
        [FromQuery] PagingParams pagingParams,
        [FromQuery] string? teachingLanguage,
        [FromQuery] string? componentType,
        [FromQuery] string? sort = "name",
        CancellationToken cancellationToken = default)
    {
        var parameters = new DataRequestParameters(filterParams, pagingParams, sort);
        var result = await _componentsService.GetComponentsByOrganizationIdAsync(parameters, organizationId, cancellationToken);
        return Ok(result);
    }

    ///  <summary>
    ///  GET /organizations/{organizationId}/courses
    ///  </summary>
    ///  <remarks>Get an ordered list of all courses for a given organization, ordered by name.</remarks>
    ///  <param name="organizationId">Organization ID</param>
    ///  <param name="filterParams"></param>
    ///  <param name="pagingParams"></param>
    ///  <param name="teachingLanguage">Filter by teachingLanguage, which is a string describing the main teaching language, should be a three-letter language code as specified by ISO 639-2.</param>
    ///  <param name="level">Filter by level</param>
    ///  <param name="modeOfDelivery">Filter by modeOfDelivery</param>
    ///  <param name="sort">
    /// Default: ["name"]<br/>
    /// Items Enum: "courseId" "name" "-courseId" "-name"<br/>
    /// Example: sort=name,-courseId<br/>
    ///  Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    ///  <param name="cancellationToken"></param>
    ///  <response code="200">OK</response>
    [HttpGet]
    [Route("organizations/{organizationId}/courses")]
    [ValidateModelState]
    [SwaggerOperation("OrganizationsOrganizationIdCoursesGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Courses), description: "OK")]
    public virtual async Task<IActionResult> OrganizationsOrganizationIdCoursesGetAsync(
        [FromRoute][Required] Guid organizationId,
        [FromQuery] FilterParams filterParams,
        [FromQuery] PagingParams pagingParams,
        [FromQuery] string? teachingLanguage,
        [FromQuery] string? level,
        [FromQuery] List<string>? modeOfDelivery,
        [FromQuery] string? sort = "name",
        CancellationToken cancellationToken = default)
    {
        var parameters = new DataRequestParameters(filterParams, pagingParams, sort);
        var result = await _coursesService.GetCoursesByOrganizationIdAsync(parameters, organizationId, cancellationToken);
        return Ok(result);
    }

    ///  <summary>
    ///  GET /organizations/{organizationId}/education-specifications
    ///  </summary>
    ///  <remarks>Get an ordered list of all EducationSpecifications for a given organization, ordered by name.</remarks>
    ///  <param name="organizationId">Organization ID</param>
    ///  <param name="filterParams"></param>
    ///  <param name="pagingParams"></param>
    ///  <param name="educationSpecificationType">Filter by type of education specification</param>
    ///  <param name="sort">
    /// Default: ["name"]<br/>
    /// Items Enum: "educationSpecificationType" "name" "primarycode" "-educationSpecificationType" "-name" "-primarycode"<br/>
    /// Example: sort=name,-educationSpecificationType<br/>
    ///  Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    ///  <param name="cancellationToken"></param>
    ///  <response code="200">OK</response>
    [HttpGet]
    [Route("organizations/{organizationId}/education-specifications")]
    [ValidateModelState]
    [SwaggerOperation("OrganizationsOrganizationIdEducationSpecificationsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(EducationSpecifications), description: "OK")]
    public virtual async Task<IActionResult> OrganizationsOrganizationIdEducationSpecificationsGetAsync(
        [FromRoute][Required] Guid organizationId,
        [FromQuery] FilterParams filterParams,
        [FromQuery] PagingParams pagingParams,
        [FromQuery] string? educationSpecificationType,
        [FromQuery] string? sort = "name",
        CancellationToken cancellationToken = default)
    {
        var parameters = new DataRequestParameters(filterParams, pagingParams, sort);
        var result = await _educationSpecificationsService.GetEducationSpecificationsByOrganizationIdAsync(parameters, organizationId, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// GET /organizations/{organizationId}
    /// </summary>
    /// <remarks>Get a single organization.</remarks>
    /// <param name="organizationId">Organization ID</param>
    /// <param name="expand">Items Enum: "parent" "children" <br/>Optional properties to expand, separated by a comma</param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("organizations/{organizationId}")]
    [ValidateModelState]
    [SwaggerOperation("OrganizationsOrganizationIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Organization), description: "OK")]
    public virtual async Task<IActionResult> OrganizationsOrganizationIdGetAsync(
        [FromRoute][Required] Guid organizationId,
        [FromQuery] List<string> expand,
        CancellationToken cancellationToken = default)
    {
        var parameters = new DataRequestParameters
        {
            Expand = expand
        };
        var result = await _organizationsService.GetAsync(organizationId, parameters, cancellationToken);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    ///  <summary>
    ///  GET /organizations/{organizationId}/groups
    ///  </summary>
    ///  <remarks>Get an ordered list of all groups for a given organization, ordered by name.</remarks>
    ///  <param name="organizationId">Organization ID</param>
    ///  <param name="filterParams"></param>
    ///  <param name="pagingParams"></param>
    ///  <param name="groupType">Filter by group type</param>
    ///  <param name="sort">
    /// Default: ["name"]<br/>
    /// Items Enum: "groupId" "name" "startDate" "-groupId" "-name" "-startDate"<br/>
    /// Example: sort=groupId,-name<br/>
    ///  Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    ///  <param name="cancellationToken"></param>
    ///  <response code="200">OK</response>
    [HttpGet]
    [Route("organizations/{organizationId}/groups")]
    [ValidateModelState]
    [SwaggerOperation("OrganizationsOrganizationIdGroupsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Groups), description: "OK")]
    public virtual async Task<IActionResult> OrganizationsOrganizationIdGroupsGetAsync(
        [FromRoute][Required] Guid organizationId,
        [FromQuery] FilterParams filterParams,
        [FromQuery] PagingParams pagingParams,
        [FromQuery] string? groupType,
        [FromQuery] string? sort = "name",
        CancellationToken cancellationToken = default)
    {
        var parameters = new DataRequestParameters(filterParams, pagingParams, sort);
        var result = await _groupsService.GetGroupsByOrganizationIdAsync(parameters, organizationId, cancellationToken);
        return Ok(result);
    }

    ///  <summary>
    ///  GET /organizations/{organizationId}/offerings
    ///  </summary>
    ///  <remarks>Get a list of all offerings for a given organization</remarks>
    ///  <param name="organizationId">Organization ID</param>
    ///  <param name="filterParams"></param>
    ///  <param name="pagingParams"></param>
    ///  <param name="teachingLanguage">Filter by teachingLanguage, which is a string describing the main teaching language, should be a three-letter language code as specified by ISO 639-2.</param>
    ///  <param name="offeringType">Filter by offering type</param>
    ///  <param name="resultExpected">Filter by resultExpected</param>
    ///  <param name="since">Filter all offerings by providing a minimum start date for the corresponding academic session, RFC3339 (full-date). By default only future offerings are shown (equal to &#x60;?since&#x3D;&lt;today&gt;&#x60;).</param>
    ///  <param name="until">Filter all offerings by providing a maximum end date for the corresponding academic session, RFC3339 (full-date).</param>
    ///  <param name="sort">
    /// Default: ["startDate"]<br/>
    /// Items Enum: "offeringId" "name" "startDate" "endDate" "-offeringId" "-name" "-startDate" "-endDate"<br/>
    /// Example: sort=name,-startDate<br/>
    ///  Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    ///  <param name="cancellationToken"></param>
    ///  <response code="200">OK</response>
    [HttpGet]
    [Route("organizations/{organizationId}/offerings")]
    [ValidateModelState]
    [SwaggerOperation("OrganizationsOrganizationIdOfferingsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Offerings), description: "OK")]
    public virtual async Task<IActionResult> OrganizationsOrganizationIdOfferingsGetAsync(
        [FromRoute][Required] Guid organizationId,
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
        return await Task.FromResult(BadRequest(new ErrorResponse(400, "Not implemented yet.")));
    }

    ///  <summary>
    ///  GET /organizations/{organizationId}/programs
    ///  </summary>
    ///  <remarks>Get an ordered list of all programs for a given organization, ordered by name.</remarks>
    ///  <param name="organizationId">Organization ID</param>
    ///  <param name="filterParams"></param>
    ///  <param name="pagingParams"></param>
    ///  <param name="teachingLanguage">Filter by teachingLanguage, which is a string describing the main teaching language, should be a three-letter language code as specified by ISO 639-2.</param>
    ///  <param name="programType">Filter by program type</param>
    ///  <param name="qualificationAwarded">Filter by qualificationAwarded</param>
    ///  <param name="levelOfQualification">Filter by levelOfQualification</param>
    ///  <param name="sector">Filter by sector</param>
    ///  <param name="fieldsOfStudy">Filter by fieldsOfStudy</param>
    ///  <param name="sort">
    /// Default: ["name"]<br/>
    /// Items Enum: "programId" "name" "-programId" "-name"<br/>
    /// Example: sort=name,-programId<br/>
    ///  Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    ///  <param name="cancellationToken"></param>
    ///  <response code="200">OK</response>
    [HttpGet]
    [Route("organizations/{organizationId}/programs")]
    [ValidateModelState]
    [SwaggerOperation("OrganizationsOrganizationIdProgramsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Programs), description: "OK")]
    public virtual async Task<IActionResult> OrganizationsOrganizationIdProgramsGetAsync(
        [FromRoute][Required] Guid organizationId,
        [FromQuery] FilterParams filterParams,
        [FromQuery] PagingParams pagingParams,
        [FromQuery] string? teachingLanguage,
        [FromQuery] string? programType,
        [FromQuery] string? qualificationAwarded,
        [FromQuery] string? levelOfQualification,
        [FromQuery] string? sector,
        [FromQuery] string? fieldsOfStudy,
        [FromQuery] string? sort = "name",
        CancellationToken cancellationToken = default)
    {
        var parameters = new DataRequestParameters(filterParams, pagingParams, sort);
        var result = await _programsService.GetProgramsByOrganizationIdAsync(parameters, organizationId, cancellationToken);
        return Ok(result);
    }
}