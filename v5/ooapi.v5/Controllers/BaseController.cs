using Microsoft.AspNetCore.Mvc;
using ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace ooapi.v5.Controllers;

[ApiController]
[SwaggerResponse(statusCode: 400, type: typeof(ErrorResponse), description: "Bad request")]
[SwaggerResponse(statusCode: 401, type: typeof(ErrorResponse), description: "Unauthorized")]
[SwaggerResponse(statusCode: 403, type: typeof(ErrorResponse), description: "Forbidden")]
[SwaggerResponse(statusCode: 404, type: typeof(ErrorResponse), description: "Not Found")]
[SwaggerResponse(statusCode: 405, type: typeof(ErrorResponse), description: "Method not allowed")]
[SwaggerResponse(statusCode: 429, type: typeof(ErrorResponse), description: "Too many requests")]
[SwaggerResponse(statusCode: 500, type: typeof(ErrorResponse), description: "Internal Server Error")]
[Route("/")]
public class BaseController : ControllerBase
{
}