using Microsoft.AspNetCore.Mvc;
using ooapi.v5.Attributes;
using ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;


namespace ooapi.v5.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
public class ServiceMetadataController : BaseController
{
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
        //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(200, default(InlineResponse200));

        //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(400, default(InlineResponse400));

        //TODO: Uncomment the next line to return response 401 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(401, default(InlineResponse400));

        //TODO: Uncomment the next line to return response 403 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(403, default(InlineResponse400));

        //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(404, default(InlineResponse400));

        //TODO: Uncomment the next line to return response 405 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(405, default(InlineResponse400));

        //TODO: Uncomment the next line to return response 429 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(429, default(InlineResponse400));

        //TODO: Uncomment the next line to return response 500 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(500, default(InlineResponse400));

        return null;
    }
}
