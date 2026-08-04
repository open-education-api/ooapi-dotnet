using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Npgsql;
using OEAPI.API.Serialization;
using OEAPI.Core.Exceptions;
using OEAPI.Infrastructure.Query;
using OEAPI.Infrastructure.Query.Fields;
using OEAPI.Infrastructure.Query.Filtering;

namespace OEAPI.API.Controllers;

/// <summary>
///     Base controller class with common functionality for OEAPI controllers.
/// </summary>
/// <remarks>
///     Initializes a new instance of the BaseApiController class.
/// </remarks>
/// <param name="logger">The logger.</param>
[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public abstract class BaseApiController(ILogger logger) : ControllerBase
{
    private readonly ILogger _logger = logger;

    /// <summary>
    ///     Handles exceptions and returns appropriate error responses.
    /// </summary>
    /// <param name="exception">The exception to handle.</param>
    /// <returns>The action result representing the error.</returns>
    protected IActionResult HandleException(Exception exception)
    {
        _logger.LogError(exception, "Unhandled exception");

        return exception switch
        {
            NotFoundException notFound => Problem(
                detail: notFound.Message,
                statusCode: notFound.StatusCode,
                title: "Not Found",
                type: notFound.ErrorCode),
            ValidationException validation => Problem(
                detail: validation.Message,
                statusCode: validation.StatusCode,
                title: "Validation Error",
                type: validation.ErrorCode,
                extensions: new Dictionary<string, object?> { ["errors"] = validation.Errors }),
            OEAPIException oeapi => Problem(
                oeapi.Message,
                statusCode: oeapi.StatusCode,
                type: oeapi.ErrorCode),
            DbUpdateException dbUpdate when IsUniqueConstraintViolation(dbUpdate) => Problem(
                detail:
                "The request conflicts with an existing resource (e.g. a duplicate primaryCode or other unique identifier).",
                statusCode: StatusCodes.Status409Conflict,
                title: "Conflict",
                type: "CONFLICT"),
            _ => Problem(
                detail: "An unexpected error occurred",
                statusCode: StatusCodes.Status500InternalServerError,
                title: "Internal Server Error",
                type: "INTERNAL_ERROR")
        };
    }

    /// <summary>
    ///     True if a <see cref="DbUpdateException" /> was caused by a unique-index/constraint
    ///     violation (e.g. two rows with the same <c>primaryCode</c>) on either supported provider,
    ///     rather than some other, genuinely unexpected database failure.
    /// </summary>
    private static bool IsUniqueConstraintViolation(DbUpdateException exception)
    {
        return exception.InnerException switch
        {
            SqlException sql => sql.Number is 2601 or 2627, // duplicate key row / unique constraint
            PostgresException postgres => postgres.SqlState == PostgresErrorCodes.UniqueViolation,
            _ => false
        };
    }

    /// <summary>
    ///     Creates a paged response.
    /// </summary>
    /// <typeparam name="T">The type of items in the response.</typeparam>
    /// <param name="result">The paged result.</param>
    /// <returns>The action result.</returns>
    protected IActionResult PagedResponse<T>(PagedResult<T> result)
    {
        return Ok(result);
    }

    /// <summary>
    ///     Applies the spec's <c>filter_query</c> DSL to a query for an entity type other than the
    ///     controller's own generic type parameter (i.e. a nested endpoint querying a different entity,
    ///     like <c>GET /academic-sessions/{id}/course-offerings</c> querying <c>CourseOfferingEntity</c>
    ///     from within <c>AcademicSessionsController</c>). Reads directly from
    ///     <see cref="Microsoft.AspNetCore.Http.HttpRequest.Query" /> rather than a formal parameter, for
    ///     the same reason <see cref="GenericEntityController{TEntity,TApiModel}.GetAll" /> does - the
    ///     spec's <c>filter_query[field][operation]=value</c>/<c>filter_query[__or][][field][operation]=value</c>
    ///     deepObject keys can't be bound to a single formal parameter.
    /// </summary>
    /// <typeparam name="TEntity">The entity type being queried.</typeparam>
    /// <param name="query">The query to filter.</param>
    /// <returns>The filtered query.</returns>
    protected IQueryable<TEntity> ApplyFilterQuery<TEntity>(IQueryable<TEntity> query) where TEntity : class
    {
        ParsedFilterQuery parsedFilter = FilterQueryParser.Parse(
            Request.Query.Select(kv => new KeyValuePair<string, string?>(kv.Key, kv.Value.ToString())));
        return FilterQueryTranslator.Apply(query, parsedFilter);
    }

    /// <summary>
    ///     Equivalent of <see cref="PagedResponse{T}" /> that also honours the spec's <c>fields</c>
    ///     selection (nested-parens syntax, e.g. <c>(id,title,programme(code))</c>) for nested-endpoint
    ///     actions that build their own <see cref="PagedResult{T}" /> outside of
    ///     <see cref="GenericEntityController{TEntity,TApiModel}.GetAll" />. See that method's field
    ///     selection handling for why pruning has to happen on the serialized JSON rather than the typed
    ///     model.
    /// </summary>
    /// <typeparam name="T">The type of items in the result.</typeparam>
    /// <param name="result">The paged result to return.</param>
    /// <param name="fields">The raw <c>fields</c> query parameter value, if any.</param>
    /// <returns>The action result.</returns>
    protected IActionResult PagedResponseWithFieldSelection<T>(PagedResult<T> result, string? fields)
    {
        FieldSelection? selection = FieldSelectionParser.Parse(fields);
        if (selection == null) return PagedResponse(result);

        JsonNode? node = JsonSerializer.SerializeToNode(result, OeapiJsonSerializerOptions.Instance);
        if (node is JsonObject resultObject && resultObject["items"] is JsonArray itemsArray)
            FieldPruner.Prune(itemsArray, selection, typeof(T).Name);

        return Ok(node);
    }

    /// <summary>
    ///     Gets the entity ID from route or query parameters.
    /// </summary>
    /// <param name="id">The ID parameter.</param>
    /// <returns>The parsed GUID or the original string.</returns>
    protected (Guid? GuidId, string StringId) ParseEntityId(string id)
    {
        return Guid.TryParse(id, out Guid guidId) ? (guidId, id) : (null, id);
    }

    /// <summary>
    ///     Reads a raw query-string value by key, without declaring it as a formal action parameter.
    ///     Used by <see cref="GenericEntityController{TEntity,TApiModel}.ApplyEntityFilters" /> overrides
    ///     to read entity-specific filter parameters (e.g. <c>organisationType</c>) - see that method's
    ///     remarks for why these aren't formal parameters on <c>GetAll</c> itself.
    /// </summary>
    /// <param name="key">The query-string key.</param>
    /// <returns>The value, or <see langword="null" /> if not present.</returns>
    protected string? GetQueryValue(string key)
    {
        return Request.Query.TryGetValue(key, out StringValues value) ? value.ToString() : null;
    }

    /// <summary>
    ///     Creates a created response with location header.
    /// </summary>
    /// <param name="actionName">The action name for the location URL.</param>
    /// <param name="id">The ID of the created resource.</param>
    /// <param name="value">The created resource.</param>
    /// <returns>The created response.</returns>
    protected CreatedAtActionResult CreatedResponse<T>(string actionName, string id, T value)
    {
        return CreatedAtAction(actionName, new { id }, value);
    }
}
