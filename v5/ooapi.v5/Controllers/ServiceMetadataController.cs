using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ooapi.v5.Attributes;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Services;
using ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;


namespace ooapi.v5.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
public class ServiceMetadataController : BaseController
{
    public ServiceMetadataController(IConfiguration configuration, CoreDBContext dbContext) : base(configuration, dbContext)
    {
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
        var service = new ServiceMetadataService(DBContext, UserRequestContext);
        var result = service.Get(out ErrorResponse errorResponse);
        if (result == null)
        {
            return BadRequest(errorResponse);
        }
        return Ok(result);
    }
}
