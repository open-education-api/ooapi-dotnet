using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ooapi.v5.core.Models;
using ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace ooapi.v5.Controllers;

/// <summary>
/// 
/// </summary>
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
    /// <summary>
    /// 
    /// </summary>
    public UserRequestContext UserRequestContext
    {
        get
        {
            var result = new UserRequestContext();
            var curHeaders = HttpContext.Request.Headers;

            if (curHeaders.TryGetValue("userId", out var headerUserId) && headerUserId.Count > 0)
            {
                result.UserID = headerUserId[0].ToString();
            }

            if (curHeaders.TryGetValue("isStudent", out var headerIsStudent) && headerIsStudent.Count > 0)
            {
                result.IsStudent = Convert.ToBoolean(headerIsStudent[0].ToString());
            }

            if (curHeaders.TryGetValue("IsEmployee", out var headerIsEmployee) && headerIsEmployee.Count > 0)
            {
                result.IsEmployee = Convert.ToBoolean(headerIsEmployee[0].ToString());
            }

            if (curHeaders.TryGetValue("Bivv", out var headerBivv) && headerBivv.Count > 0)
            {
                result.Bivv = string.IsNullOrEmpty(headerBivv[0].ToString()) ? headerBivv[0].ToString() : "laag";
            }

            result.IsLocal = (HttpContext.Request.Host.Host.ToLower().Equals("localhost"));
            return result;
        }
    }
}