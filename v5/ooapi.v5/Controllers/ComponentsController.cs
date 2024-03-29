using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ooapi.v5.Attributes;
using ooapi.v5.core.Services.Interfaces;
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
/// API calls for components
/// </summary>
[ApiController]
public class ComponentsController : BaseController
{
    private readonly IComponentsService _componentsService;

    /// <summary>
    /// Resolves the required services
    /// </summary>
    /// <param name="componentsService"></param>
    public ComponentsController(IComponentsService componentsService)
    {
        _componentsService = componentsService;
    }

    /// <summary>
    /// GET /components/{componentId}
    /// </summary>
    /// <remarks>Get a single component.</remarks>
    /// <param name="componentId">component ID</param>
    /// <param name="expand">Items Enum: "course" "organization"  <br/>Optional properties to expand, separated by a comma</param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("components/{componentId}")]
    [ValidateModelState]
    [SwaggerOperation("ComponentsComponentIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Component), description: "OK")]
    public virtual async Task<IActionResult> ComponentsComponentIdGetAsync(
        [FromRoute][Required] Guid componentId,
        [FromQuery] List<string> expand,
        CancellationToken cancellationToken = default)
    {
        var result = await _componentsService.GetAsync(componentId, cancellationToken);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    ///  <summary>
    ///  GET /components/{componentId}/offerings
    ///  </summary>
    ///  <remarks>Get a list of all offerings for this component, ordered chronologically.</remarks>
    ///  <param name="componentId">Component ID</param>
    ///  <param name="filterParams"></param>
    ///  <param name="pagingParams"></param>
    ///  <param name="teachingLanguage">Filter by teachingLanguage, which is a string describing the main teaching language, should be a three-letter language code as specified by ISO 639-2.</param>
    ///  <param name="resultExpected">Filter by resultExpected</param>
    ///  <param name="since">Filter all offerings by providing a minimum start date for the corresponding academic session, RFC3339 (full-date). By default only future offerings are shown (equal to &#x60;?since&#x3D;&lt;today&gt;&#x60;).</param>
    ///  <param name="until">Filter all offerings by providing a maximum end date for the corresponding academic session, RFC3339 (full-date).</param>
    ///  <param name="sort"><br/>
    /// Default: ["startDateTime"]<br/>
    /// Items Enum: "offeringId" "name" "startDateTime" "endDateTime" "-offeringId" "-name" "-startDateTime" "-endDateTime"<br/>
    /// Example: sort=offeringId,-startDateTime<br/>
    ///  Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    ///  <param name="cancellationToken"></param>
    ///  <response code="200">OK</response>
    [HttpGet]
    [Route("components/{componentId}/offerings")]
    [ValidateModelState]
    [SwaggerOperation("ComponentsComponentIdOfferingsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(ComponentOfferings), description: "OK")]
    public virtual async Task<IActionResult> ComponentsComponentIdOfferingsGetAsync(
        [FromRoute][Required] Guid componentId,
        [FromQuery] FilterParams filterParams,
        [FromQuery] PagingParams pagingParams,
        [FromQuery] string? teachingLanguage,
        [FromQuery] bool? resultExpected,
        [FromQuery] DateTime? since,
        [FromQuery] DateTime? until,
        [FromQuery] string? sort = "startDateTime",
        CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(BadRequest(new ErrorResponse(400, "Not implemented yet.")));
    }
}