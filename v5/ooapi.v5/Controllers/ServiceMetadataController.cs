using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ooapi.v5.Attributes;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace ooapi.v5.Controllers;

/// <summary>
/// API calls for service metadata
/// </summary>
[ApiController]
public class ServiceMetadataController : BaseController
{
    private readonly IServiceMetadataService _serviceMetadataService;

    /// <summary>
    /// Resolves the required services
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
    /// <param name="cancellationToken"></param>
    /// <response code="200">OK</response>
    [HttpGet]
    [ValidateModelState]
    [SwaggerOperation("RootGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Service), description: "OK")]
    public virtual async Task<IActionResult> RootGetAsync(
        CancellationToken cancellationToken = default)
    {
        var result = await _serviceMetadataService.GetAsync(cancellationToken);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
}