using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ooapi.v5.Attributes;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ooapi.v5.Controllers;


[ApiController]
public class AssociationsController : BaseController
{
    private readonly IAssociationsService _associationsService;


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
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("associations/{associationId}")]
    [ValidateModelState]
    [SwaggerOperation("AssociationsAssociationIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Association), description: "OK")]
    public virtual IActionResult AssociationsAssociationIdGet([FromRoute][Required] Guid associationId, [FromQuery] List<string> expand)
    {
        var result = _associationsService.Get(associationId, out var errorResponse);
        if (result == null)
        {
            return BadRequest(errorResponse);
        }
        return Ok(result);
    }
}
