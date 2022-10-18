namespace IO.Swagger.Controllers;

using IO.Swagger.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

/// <summary>
/// 
/// </summary>
[ApiController]
[SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400), description: "Bad request")]
[SwaggerResponse(statusCode: 401, type: typeof(InlineResponse400), description: "Unauthorized")]
[SwaggerResponse(statusCode: 403, type: typeof(InlineResponse400), description: "Forbidden")]
[SwaggerResponse(statusCode: 404, type: typeof(InlineResponse400), description: "Not Found")]
[SwaggerResponse(statusCode: 405, type: typeof(InlineResponse400), description: "Method not allowed")]
[SwaggerResponse(statusCode: 429, type: typeof(InlineResponse400), description: "Too many requests")]
[SwaggerResponse(statusCode: 500, type: typeof(InlineResponse400), description: "Internal Server Error")]
[Route("v5.0")]
public class BaseController : ControllerBase
{

}