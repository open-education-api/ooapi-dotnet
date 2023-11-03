using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ooapi.v5.Attributes;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace ooapi.v5.Controllers;

/// <summary>
/// API calls for associations
/// </summary>
[ApiController]
public class AssociationsController : BaseController
{
    private readonly IAssociationsService _associationsService;

    /// <summary>
    /// Resolves the required services
    /// </summary>
    /// <param name="associationsService"></param>
    public AssociationsController(IAssociationsService associationsService)
    {
        _associationsService = associationsService;
    }

    /// <summary>
    /// GET /associations/{associationId}
    /// </summary>
    /// <remarks>Get a single association.</remarks>
    /// <param name="associationId">Association ID</param>
    /// <param name="expand">Items Enum: "person" "offering" <br/> Optional properties to expand, separated by a comma</param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("associations/{associationId}")]
    [ValidateModelState]
    [SwaggerOperation("AssociationsAssociationIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Association), description: "OK")]
    public virtual async Task<IActionResult> AssociationsAssociationIdGetAsync(
        [FromRoute][Required] Guid associationId,
        [FromQuery] List<string> expand,
        CancellationToken cancellationToken = default)
    {
        var result = await _associationsService.GetAsync(associationId, cancellationToken);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
}