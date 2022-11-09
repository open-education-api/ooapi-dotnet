namespace ooapi.v5.Controllers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using ooapi.v5.core.Models;
using ooapi.v5.core.Repositories;
using ooapi.v5.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;

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
[Route("v5.0")]
public class BaseController : ControllerBase
{
    public UserRequestContext UserRequestContext { get; internal set; } = new UserRequestContext();

    public BaseController(IConfiguration configuration, CoreDBContext dbContext, IHttpContextAccessor httpContextAccessor)
    {


        var curContext = httpContextAccessor.HttpContext;
        var curHeaders = curContext.Request.Headers;
        //            var reqContext = Request;

        if (curHeaders.TryGetValue("userId", out StringValues headerUserId))
        {
            if (headerUserId.Count > 0)
                UserRequestContext.UserID = headerUserId[0].ToString();
        }
        if (curHeaders.TryGetValue("isStudent", out StringValues headerIsStudent))
        {
            if (headerIsStudent.Count > 0)
                UserRequestContext.IsStudent = Convert.ToBoolean(headerIsStudent[0].ToString());
        }
        if (curHeaders.TryGetValue("IsEmployee", out StringValues headerIsEmployee))
        {
            if (headerIsEmployee.Count > 0)
                UserRequestContext.IsEmployee = Convert.ToBoolean(headerIsEmployee[0].ToString());
        }
        if (curHeaders.TryGetValue("Bivv", out StringValues headerBivv))
        {
            if (headerBivv.Count > 0)
                UserRequestContext.Bivv = String.IsNullOrEmpty(headerBivv[0].ToString()) ? headerBivv[0].ToString() : "laag";
        }
        UserRequestContext.IsLocal = (curContext.Request.Host.Host.ToLower().Equals("localhost"));


    }

}