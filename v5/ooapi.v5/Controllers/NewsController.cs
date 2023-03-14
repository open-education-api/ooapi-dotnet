using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ooapi.v5.Attributes;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Services;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;
using ooapi.v5.Models.Params;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ooapi.v5.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
public class NewsController : BaseController
{

    public NewsController(IConfiguration configuration, CoreDBContext dbContext) : base(configuration, dbContext)
    {
    }

    /// <summary>
    /// GET /news-feeds
    /// </summary>
    /// <remarks>Get a list of all news feeds, ordered by title.</remarks>
    /// <param name="filterParams"></param>
    /// <param name="pagingParams"></param>
    /// <param name="newsFeedType">Filter by news type</param>
    /// <param name="sort">
    ///Default: ["name"]<br/>
    ///Items Enum: "newsFeedId" "name" "-newsFeedId" "-name"<br/>
    ///Example: sort=name,-newsFeedId<br/>
    /// Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("news-feeds")]
    [ValidateModelState]
    [SwaggerOperation("NewsFeedsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(NewsFeeds), description: "OK")]
    public virtual IActionResult NewsFeedsGet([FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] string? newsFeedType, [FromQuery] string? sort = "name")
    {
        DataRequestParameters parameters = new DataRequestParameters(null, filterParams, pagingParams, sort);
        var service = new NewsFeedsService(DBContext, UserRequestContext);
        var result = service.GetAll(parameters, out ErrorResponse errorResponse);
        if (result == null)
        {
            return BadRequest(errorResponse);
        }
        return Ok(result);
    }

    /// <summary>
    /// GET /news-feeds/{newsFeedId}
    /// </summary>
    /// <remarks>Get a single news feed.</remarks>
    /// <param name="newsFeedId">News feed ID</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("news-feeds/{newsFeedId}")]
    [ValidateModelState]
    [SwaggerOperation("NewsFeedsNewsFeedIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(NewsFeed), description: "OK")]
    public virtual IActionResult NewsFeedsNewsFeedIdGet([FromRoute][Required] Guid newsFeedId)
    {
        var service = new NewsFeedsService(DBContext, UserRequestContext);
        var result = service.Get(newsFeedId, out ErrorResponse errorResponse);
        if (result == null)
        {
            return BadRequest(errorResponse);
        }
        return Ok(result);
    }

    /// <summary>
    /// GET /news-feeds/{newsFeedId}/news-items
    /// </summary>
    /// <remarks>Get an ordered list of all news items.</remarks>
    /// <param name="newsFeedId">News feed ID</param>
    /// <param name="filterParams"></param>
    /// <param name="pagingParams"></param>
    /// <param name="author">Filter by author</param>
    /// <param name="sort">
    ///Default: ["newsItemId"]<br/>
    ///Items Enum: "newsItemId" "name" "validFrom" "validUntil" "lastModified" "-newsItemId" "-name" "-validFrom" "-validUntil" "-lastModified"<br/>
    ///Example: sort=validFrom,-name<br/>
    /// Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign &#x60;-&#x60; allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("news-feeds/{newsFeedId}/news-items")]
    [ValidateModelState]
    [SwaggerOperation("NewsFeedsNewsFeedIdNewsItemsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(NewsItems), description: "OK")]
    public virtual IActionResult NewsFeedsNewsFeedIdNewsItemsGet([FromRoute][Required] Guid newsFeedId, [FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams, [FromQuery] string? author, [FromQuery] string? sort = "newsItemId")
    {
        DataRequestParameters parameters = new DataRequestParameters(filterParams, pagingParams, sort);
        var service = new NewsItemsService(DBContext, UserRequestContext);
        var result = service.GetNewsItemsByNewsFeedId(parameters, newsFeedId, out ErrorResponse errorResponse);
        if (result == null)
        {
            return BadRequest(errorResponse);
        }
        return Ok(result);
    }

    /// <summary>
    /// GET /news-items/{newsItemId}
    /// </summary>
    /// <remarks>Get a single news item.</remarks>
    /// <param name="newsItemId">News item ID</param>
    /// <param name="expand">Items Value: "newsFeeds" <br/>Optional properties to expand, separated by a comma</param>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("news-items/{newsItemId}")]
    [ValidateModelState]
    [SwaggerOperation("NewsItemsNewsItemIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(NewsItem), description: "OK")]
    public virtual IActionResult NewsItemsNewsItemIdGet([FromRoute][Required] Guid newsItemId, [FromQuery] List<string> expand)
    {
        var service = new NewsItemsService(DBContext, UserRequestContext);
        var result = service.Get(newsItemId, out ErrorResponse errorResponse);
        if (result == null)
        {
            return BadRequest(errorResponse);
        }
        return Ok(result);
    }
}
