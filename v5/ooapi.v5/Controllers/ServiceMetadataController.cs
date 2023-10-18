using Microsoft.AspNetCore.Mvc;
using ooapi.v5.Attributes;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;


namespace ooapi.v5.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
public class ServiceMetadataController : BaseController
{
    private readonly IServiceMetadataService _serviceMetadataService;

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
        var result = _serviceMetadataService.Get();
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
}
