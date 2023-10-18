using Microsoft.AspNetCore.Mvc;
using ooapi.v5.Attributes;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace ooapi.v5.Controllers;


[ApiController]
public class ServiceMetadataController : BaseController
{
    private readonly IServiceMetadataService _serviceMetadataService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="serviceMetadataService"></param>
    public ServiceMetadataController(IServiceMetadataService serviceMetadataService)
    {
        _serviceMetadataService = serviceMetadataService;
    }

    /// <summary>
    /// GET /
    /// </summary>
    /// <remarks>Get metadata for the service.</remarks>
    /// <response code="200">OK</response>
    [HttpGet]
    [ValidateModelState]
    [SwaggerOperation("RootGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Service), description: "OK")]
    public virtual IActionResult RootGet()
    {
        var result = _serviceMetadataService.Get(out var errorResponse);
        if (result == null)
        {
            return BadRequest(errorResponse);
        }
        return Ok(result);
    }
}
