using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OEAPI.API.Configuration;
using OEAPI.API.Serialization;
using OEAPI.Infrastructure.Data.Context;
using OEAPI.Infrastructure.Query;
using OEAPI.Infrastructure.Query.Consumers;
using OEAPI.Infrastructure.Query.Fields;
using OEAPI.Infrastructure.Query.Filtering;

namespace OEAPI.API.Controllers;

/// <summary>
///     Base generic controller for the two operations every resource shares: <c>GetAll</c>/<c>GetById</c>.
///     Deliberately a **read-only** base rather than generic CRUD: the spec doesn't require every
///     resource to be writable, and institutions are expected to populate data via their own
///     integrations, not a generic public write API. Entity-specific write actions (<c>PUT</c>/
///     <c>POST</c>/<c>PATCH</c>) are implemented explicitly on the relevant controller instead, with the
///     exact request/response shape the spec defines for that operation - never inherited generically.
///     Entity-specific query parameters use the <see cref="ApplyEntityFilters" /> hook (and eager-loading
///     needs use <see cref="ApplyIncludes" />) rather than redeclaring <c>GetAll</c>/<c>GetById</c> with a
///     different signature - redeclaring instead of overriding silently registers two separate actions on
///     the same route, which is ambiguous at request time.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <typeparam name="TApiModel">The API model type.</typeparam>
[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public abstract class GenericEntityController<TEntity, TApiModel> : BaseApiController
    where TEntity : class
    where TApiModel : class
{
    private readonly DbSet<TEntity> _dbSet;

    /// <summary>
    ///     Initializes a new instance of the GenericEntityController class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="entityName">The entity name (singular).</param>
    /// <param name="routeName">The route name (plural, kebab-case).</param>
    protected GenericEntityController(
        OEAPIDbContext dbContext,
        ILogger logger,
        string entityName,
        string routeName)
        : base(logger)
    {
        OEAPIDbContext nonNullDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dbSet = nonNullDbContext.Set<TEntity>();
        EntityName = entityName ?? throw new ArgumentNullException(nameof(entityName));
        RouteName = routeName ?? throw new ArgumentNullException(nameof(routeName));
    }

    /// <summary>
    ///     Gets the DbSet for the entity with AsNoTracking applied, plus any eager-loaded navigation
    ///     properties declared via <see cref="ApplyIncludes" />.
    /// </summary>
    protected IQueryable<TEntity> Entities => ApplyIncludes(_dbSet.AsNoTracking());

    /// <summary>
    ///     Gets the entity name (singular).
    /// </summary>
    protected string EntityName { get; }

    /// <summary>
    ///     Gets the route name (plural, kebab-case).
    /// </summary>
    protected string RouteName { get; }

    /// <summary>
    ///     Retrieves a list of all entities.
    /// </summary>
    /// <param name="primaryCode">Filter by primary code.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">
    ///     The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>. Not
    ///     bound via a formal <c>filter_query</c>-shaped parameter here (see the <c>filter_query</c>
    ///     handling below for why) - <c>fields</c> itself binds fine since it's a plain string, unlike
    ///     <c>filter_query</c>'s bracket-notation keys.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <remarks>
    ///     <c>filter_query[field][operation]=value</c> and <c>filter_query[__or][][field][operation]=value</c>
    ///     are deepObject-style query keys that ASP.NET Core's model binding can't map onto a single
    ///     formal parameter, so they're deliberately not declared as parameters here - they're read
    ///     directly from <see cref="Microsoft.AspNetCore.Http.HttpRequest.Query" /> via
    ///     <see cref="FilterQueryParser" /> instead. See <see cref="FilterQueryTranslator" /> for how the
    ///     parsed clauses become LINQ predicates.
    ///
    ///     Returns <c>404</c> immediately, before any of the above, when this controller's own
    ///     <c>RouteName</c> is one of the 8 in <see cref="NonCanonicalTopLevelListRoutes" /> and the
    ///     deployment hasn't opted into <see cref="ServiceConfiguration.ExposeNonCanonicalListEndpoints" />.
    /// </remarks>
    [HttpGet]
    // 200 intentionally not declared here: TApiModel can't be referenced in an attribute on this
    // generic base class (CS0416). Every concrete controller overrides this action solely to
    // attach a concrete-typed [ProducesResponseType(typeof(PagedResult<TConcrete>), 200)] - see
    // any derived controller's GetAll override. Declaring an untyped 200 here too would create a
    // duplicate/ambiguous entry for the same status code once combined with the override's.
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status405MethodNotAllowed)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> GetAll(
        [FromQuery(Name = "primaryCode")] string? primaryCode,
        [FromQuery(Name = "pageNumber")] int? pageNumber,
        [FromQuery(Name = "pageSize")] int? pageSize,
        [FromQuery(Name = "consumer")] string? consumer,
        [FromQuery(Name = "fields")] string? fields,
        CancellationToken cancellationToken = default)
    {
        if (NonCanonicalTopLevelListRoutes.Names.Contains(RouteName) &&
            !HttpContext.RequestServices.GetRequiredService<ServiceConfiguration>().ExposeNonCanonicalListEndpoints)
            return Problem(
                detail: $"GET /{RouteName} has no canonical-spec counterpart and is disabled by this " +
                         "deployment's configuration (Service:ExposeNonCanonicalListEndpoints).",
                statusCode: StatusCodes.Status404NotFound,
                title: "Not Found");

        try
        {
            IQueryable<TEntity> query = Entities;

            // Apply primaryCode filter if supported
            if (!string.IsNullOrEmpty(primaryCode)) query = ApplyPrimaryCodeFilter(query, primaryCode);

            // Apply entity-specific filters (e.g. organisationType, gender) that don't
            // appear as formal parameters here - see ApplyEntityFilters for why.
            query = ApplyEntityFilters(query);

            // Apply the spec's filter_query DSL (read directly from the raw query string - see the
            // remarks on this method for why it can't be a formal [FromQuery] parameter).
            ParsedFilterQuery parsedFilter = FilterQueryParser.Parse(
                Request.Query.Select(kv => new KeyValuePair<string, string?>(kv.Key, kv.Value.ToString())));
            query = FilterQueryTranslator.Apply(query, parsedFilter);

            // Apply consumer filtering if supported
            query = ApplyConsumerFilter(query, consumer);

            // Apply pagination
            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);

            int totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            // Apply default ordering - should be overridden by specific implementations
            query = ApplyDefaultOrdering(query);

            List<TEntity> items = await query
                .Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            // Convert to API models
            List<TApiModel> apiModels = [.. items.Select(item => MapToApiModel(item, consumer))];

            // Create paged result
            PagedResult<TApiModel> result = new(
                apiModels, totalCount, validatedPage, validatedPageSize);

            // fields prunes the *serialized JSON*, not the typed models - the spec's nested-parens
            // syntax (e.g. programme(code)) can't be represented by selectively populating a
            // strongly-typed TApiModel, so this only applies to the response actually being written.
            FieldSelection? fieldSelection = FieldSelectionParser.Parse(fields);
            if (fieldSelection != null)
            {
                JsonNode? node = JsonSerializer.SerializeToNode(result, OeapiJsonSerializerOptions.Instance);
                if (node is JsonObject { } resultObject && resultObject["items"] is JsonArray itemsArray)
                    FieldPruner.Prune(itemsArray, fieldSelection, typeof(TApiModel).Name);

                return Ok(node);
            }

            return PagedResponse(result);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    ///     Computes a <c>Skip()</c> offset for a page/page-size pair, without the 32-bit overflow raw
    ///     <c>(validatedPage - 1) * validatedPageSize</c> int arithmetic is prone to for a large enough
    ///     <paramref name="validatedPage" /> - the overflowed value goes negative, which SQL Server
    ///     rejects as an invalid <c>OFFSET</c>, crashing the request with a <c>500</c> instead of just
    ///     returning an empty page. Every entity-specific nested-collection <c>GET</c> action (e.g.
    ///     <c>GET /academic-sessions/{id}/course-offerings</c>) has its own hand-rolled pagination
    ///     alongside this base class's own <see cref="GetAll" /> - this is the one shared, tested
    ///     implementation all of them call, so the overflow can only ever need fixing in one place.
    /// </summary>
    protected static int ComputePageOffset(int validatedPage, int validatedPageSize) =>
        (int)Math.Min((long)(validatedPage - 1) * validatedPageSize, int.MaxValue);

    /// <summary>
    ///     Retrieves a single entity by ID.
    /// </summary>
    /// <param name="id">The entity ID.</param>
    /// <param name="expand">Optional properties to expand, separated by a comma.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{id}")]
    // 200 intentionally not declared here - see the identical note on GetAll above.
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> GetById(
        string id,
        [FromQuery] string? expand,
        [FromQuery] string? fields,
        [FromQuery] string? consumer,
        CancellationToken cancellationToken = default)
    {
        try
        {
            (Guid? guidId, string stringId) = ParseEntityId(id);

            TEntity? entity;

            if (guidId.HasValue)
            {
                entity = await Entities
                    .FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == guidId.Value, cancellationToken)
                    .ConfigureAwait(false);

                // A GUID-shaped id is ambiguous: it could be the internal Id, or it could be a
                // GUID-shaped external unique identifier (e.g. a server-generated one, like
                // PersonsController.CreatePerson's `Guid.NewGuid().ToString()` PersonId, or a
                // client-chosen one via a PUT-to-create). Fall back to the external field lookup
                // before giving up, so those don't incorrectly 404.
                entity ??= await FindByUniqueIdAsync(stringId, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                // Try to find by unique identifier field
                entity = await FindByUniqueIdAsync(stringId, cancellationToken).ConfigureAwait(false);
            }

            if (entity == null)
                return Problem(
                    detail: $"{EntityName} with ID '{id}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            // Apply consumer filtering
            if (!string.IsNullOrEmpty(consumer) &&
                !await IsConsumerMatchAsync(entity, consumer, cancellationToken).ConfigureAwait(false))
                return Problem(
                    detail: $"{EntityName} with ID '{id}' not found for the specified consumer.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            // Convert to API model
            TApiModel apiModel = MapToApiModel(entity, consumer);

            // Apply expand if requested
            if (!string.IsNullOrEmpty(expand))
                apiModel = await ApplyExpandAsync(apiModel, expand, cancellationToken).ConfigureAwait(false);

            // Apply timelineOverrides if requested - read directly from the query string (like
            // ApplyEntityFilters' entity-specific filters) rather than a formal parameter, since only
            // Course/Programme support this and a formal parameter would force a mechanical edit to
            // every one of this base class's other overriders and incorrectly advertise the option on
            // every resource's generated OpenAPI doc.
            if (bool.TryParse(GetQueryValue("returnTimelineOverrides"), out bool returnTimelineOverrides) &&
                returnTimelineOverrides)
                apiModel = await ApplyTimelineOverridesAsync(apiModel, entity, expand, consumer, cancellationToken)
                    .ConfigureAwait(false);

            // fields prunes the *serialized JSON*, not the typed model - see the note on GetAll.
            FieldSelection? fieldSelection = FieldSelectionParser.Parse(fields);
            if (fieldSelection != null)
            {
                JsonNode? node = JsonSerializer.SerializeToNode(apiModel, OeapiJsonSerializerOptions.Instance);
                FieldPruner.Prune(node, fieldSelection, typeof(TApiModel).Name);
                return Ok(node);
            }

            return Ok(apiModel);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    // ========================================================================
    // Writes are intentionally NOT provided generically.
    //
    // The OEAPI spec is read-only for most entities; institutions populate the
    // database via their own middleware, not through this API. Only a specific
    // subset of entities have spec-defined POST/PUT/PATCH operations, and those
    // are implemented explicitly on the relevant controller (with the correct
    // spec request/response shape) rather than inherited generically here.
    // ========================================================================

    // ========================================================================
    // Abstract/Virtual methods that must be implemented by derived classes
    // ========================================================================

    /// <summary>
    ///     Maps an entity to an API model.
    /// </summary>
    /// <param name="entity">The entity to map.</param>
    /// <param name="consumer">
    ///     The requested <c>?consumer=</c> query value, if any - implementations that populate a
    ///     typed or generic <c>Consumer</c> field must gate it on this via
    ///     <see cref="OEAPI.Infrastructure.Query.Consumers.ConsumerJsonExtensions" /> rather than
    ///     returning stored consumer data unconditionally.
    /// </param>
    /// <returns>The API model.</returns>
    protected abstract TApiModel MapToApiModel(TEntity entity, string? consumer);

    /// <summary>
    ///     Maps an API model to an entity. Not called by this base class (writes are read-only by
    ///     default - see the note above <see cref="ApplyEntityFilters" />'s section); kept as a required
    ///     implementation because entities with a spec-defined write action need it for their explicit
    ///     POST/PUT/PATCH implementation.
    /// </summary>
    /// <param name="model">The API model to map.</param>
    /// <returns>The entity.</returns>
    protected abstract TEntity MapToEntity(TApiModel model);

    /// <summary>
    ///     Updates an entity from an API model. Not called by this base class; same rationale as
    ///     <see cref="MapToEntity" />.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="model">The API model with new data.</param>
    protected abstract void UpdateEntityFromApiModel(TEntity entity, TApiModel model);

    /// <summary>
    ///     Gets the unique identifier for an entity. Not called by this base class; same rationale as
    ///     <see cref="MapToEntity" />.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>The identifier as string.</returns>
    protected abstract string GetEntityId(TEntity entity);

    // ========================================================================
    // Virtual methods that can be overridden by derived classes
    // ========================================================================

    /// <summary>
    ///     Applies primary code filtering to the query.
    /// </summary>
    /// <param name="query">The query to filter.</param>
    /// <param name="primaryCode">The primary code to filter by.</param>
    /// <returns>The filtered query.</returns>
    protected virtual IQueryable<TEntity> ApplyPrimaryCodeFilter(
        IQueryable<TEntity> query,
        string primaryCode)
    {
        // Base implementation tries to filter by PrimaryCode property
        PropertyInfo? primaryCodeProperty = typeof(TEntity).GetProperty("PrimaryCode");
        if (primaryCodeProperty != null)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "x");
            MemberExpression propertyAccess = Expression.MakeMemberAccess(parameter, primaryCodeProperty);
            ConstantExpression constant = Expression.Constant(primaryCode, typeof(string));
            BinaryExpression equality = Expression.Equal(propertyAccess, constant);
            Expression<Func<TEntity, bool>> lambda = Expression.Lambda<Func<TEntity, bool>>(equality, parameter);

            return query.Where(lambda);
        }

        // If no PrimaryCode property, return query unchanged
        return query;
    }

    /// <summary>
    ///     Applies consumer filtering to the query - see <see cref="ConsumerKeyFilter" /> for the
    ///     shared, reflection-based semantics (kept generic here rather than per-controller so every
    ///     entity gets this for free).
    /// </summary>
    /// <param name="query">The query to filter.</param>
    /// <param name="consumer">The consumer key.</param>
    /// <returns>The filtered query.</returns>
    protected virtual IQueryable<TEntity> ApplyConsumerFilter(
        IQueryable<TEntity> query,
        string? consumer)
    {
        return ConsumerKeyFilter.Apply(query, consumer);
    }

    /// <summary>
    ///     Applies default ordering to the query.
    /// </summary>
    /// <param name="query">The query to order.</param>
    /// <returns>The ordered query.</returns>
    protected virtual IQueryable<TEntity> ApplyDefaultOrdering(IQueryable<TEntity> query)
    {
        // Default ordering by Id
        return query.OrderBy(e => EF.Property<Guid>(e, "Id"));
    }

    /// <summary>
    ///     Finds an entity by unique identifier (non-GUID).
    /// </summary>
    /// <param name="idValue">The unique identifier value.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity or null.</returns>
    protected virtual async Task<TEntity?> FindByUniqueIdAsync(
        string idValue,
        CancellationToken cancellationToken = default)
    {
        // Default implementation tries to find by a "UniqueId" or entity name + "Id" property
        if (string.IsNullOrEmpty(idValue))
            return null;

        try
        {
            // Try to find by Id property first. Note: idValue being GUID-shaped doesn't guarantee
            // it's the internal Id - it could equally be a GUID-shaped external identifier (e.g. a
            // server-generated one). So on a miss here, fall through to the string-identifier-property
            // lookup below instead of returning null immediately.
            PropertyInfo? idProperty = typeof(TEntity).GetProperty("Id");
            if (idProperty != null && idProperty.PropertyType == typeof(Guid) &&
                Guid.TryParse(idValue, out Guid guidValue))
            {
                // Must be `object[]` (not `Guid[]`), or overload resolution silently falls back to
                // the `params object[] keyValues` overload and packs both this array and
                // cancellationToken into it as two composite-key values instead of one.
                TEntity? byId = await _dbSet.FindAsync([guidValue], cancellationToken)
                    .ConfigureAwait(false);
                if (byId != null) return byId;
            }

            // Try to find by a string identifier property (e.g., CourseId, PersonId, etc.)
            string entityName = typeof(TEntity).Name.Replace("Entity", "");
            string idPropertyName = entityName + "Id";

            PropertyInfo? stringIdProperty = typeof(TEntity).GetProperty(idPropertyName);
            if (stringIdProperty != null && stringIdProperty.PropertyType == typeof(string))
            {
                ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "x");
                MemberExpression propertyAccess = Expression.MakeMemberAccess(parameter, stringIdProperty);
                ConstantExpression constant = Expression.Constant(idValue, typeof(string));
                BinaryExpression equality = Expression.Equal(propertyAccess, constant);
                Expression<Func<TEntity, bool>> lambda = Expression.Lambda<Func<TEntity, bool>>(equality, parameter);

                return await Entities
                    .FirstOrDefaultAsync(lambda, cancellationToken)
                    .ConfigureAwait(false);
            }
        }
        catch
        {
            // If any error occurs, return null
        }

        return null;
    }

    /// <summary>
    ///     Checks if an entity matches the specified consumer, for the single-item <c>GetById</c> path.
    ///     Must stay semantically identical to
    ///     <see cref="OEAPI.Infrastructure.Query.Consumers.ConsumerKeyFilter" />'s own list-level
    ///     filtering (a <c>null</c> <c>ConsumerKey</c> means "not tied to any particular consumer, fine
    ///     to return to anyone" - it always matches, regardless of the requested <paramref name="consumer" />)
    ///     - confirmed live that this had drifted out of sync: <c>GET /academic-sessions?consumer=rio</c>
    ///     correctly included a null-<c>ConsumerKey</c> row, but <c>GET /academic-sessions/{that-same-id}
    ///     ?consumer=rio</c> 404'd for it, because this method's own equality check had no null exemption.
    /// </summary>
    /// <param name="entity">The entity to check.</param>
    /// <param name="consumer">The consumer key.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result indicates whether the entity matches the
    ///     consumer.
    /// </returns>
    protected virtual Task<bool> IsConsumerMatchAsync(
        TEntity entity,
        string consumer,
        CancellationToken cancellationToken = default)
    {
        // Base implementation checks for ConsumerKey property
        PropertyInfo? consumerProperty = typeof(TEntity).GetProperty("ConsumerKey");
        if (consumerProperty != null)
        {
            string? consumerValue = consumerProperty.GetValue(entity) as string;
            bool isMatch = consumerValue == null ||
                           string.Equals(consumerValue, consumer, StringComparison.OrdinalIgnoreCase);
            return Task.FromResult(isMatch);
        }

        // If no ConsumerKey property, assume it matches (no consumer-specific filtering)
        return Task.FromResult(true);
    }

    /// <summary>
    ///     Applies entity-specific filters to a <c>GetAll</c> query (e.g. <c>organisationType</c>,
    ///     <c>gender</c>). These are read from <see cref="Microsoft.AspNetCore.Http.HttpRequest.Query" />
    ///     via <see cref="BaseApiController.GetQueryValue" /> rather than as formal action parameters.
    /// </summary>
    /// <remarks>
    ///     Do not redeclare <see cref="GetAll" /> or <see cref="GetById" /> in a derived controller to add
    ///     entity-specific query parameters - a redeclared method with a different signature hides the
    ///     base action instead of overriding it, and ASP.NET Core then registers both as separate actions
    ///     on the same route, which throws <c>AmbiguousMatchException</c> on every request. Override this
    ///     hook instead.
    /// </remarks>
    /// <param name="query">The query to filter.</param>
    /// <returns>The filtered query.</returns>
    protected virtual IQueryable<TEntity> ApplyEntityFilters(IQueryable<TEntity> query)
    {
        return query;
    }

    /// <summary>
    ///     Eagerly loads navigation properties needed to populate identifier fields on the API model
    ///     (e.g. a single-valued <c>Parent</c> navigation, so <c>MapToApiModel</c> can read
    ///     <c>entity.Parent?.SomeId</c> without triggering a separate query per row). Applies to both
    ///     <see cref="GetAll" /> (via <see cref="Entities" />) and the GUID-keyed lookup path of
    ///     <see cref="GetById" />. Controllers with their own <c>FindByUniqueIdAsync</c> override (the
    ///     string-keyed lookup path) should apply the same includes there directly.
    /// </summary>
    /// <remarks>
    ///     Keep this to single-valued (reference) navigations where practical - including a collection
    ///     navigation here loads it for every row of a list response, which is fine for a reference
    ///     implementation's correctness but is a real cost worth knowing about at scale.
    /// </remarks>
    /// <param name="query">The query to add includes to.</param>
    /// <returns>The query with any needed includes applied.</returns>
    protected virtual IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query)
    {
        return query;
    }

    /// <summary>
    ///     Applies expand to an API model (includes related entities).
    /// </summary>
    /// <param name="apiModel">The API model to expand.</param>
    /// <param name="expand">The expand parameter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the expanded API model.</returns>
    protected virtual Task<TApiModel> ApplyExpandAsync(
        TApiModel apiModel,
        string expand,
        CancellationToken cancellationToken = default)
    {
        // Base implementation does nothing - expand should be implemented by specific controllers
        return Task.FromResult(apiModel);
    }

    /// <summary>
    ///     Applies the spec's <c>returnTimelineOverrides=true</c> mechanism to an API model, populating
    ///     historical/future alternate snapshots. Only <c>Course</c>/<c>Programme</c> support this - every
    ///     other resource inherits this no-op default, so the query parameter is silently ignored rather
    ///     than erroring.
    /// </summary>
    /// <param name="apiModel">The API model to populate.</param>
    /// <param name="entity">The entity the API model was mapped from.</param>
    /// <param name="expand">
    ///     The caller's <c>expand=</c> value, if any - implementations compose it into each override
    ///     entry's own nested expansion, matching the resource's normal expand behaviour.
    /// </param>
    /// <param name="consumer">The requested <c>?consumer=</c> query value, if any.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the populated API model.</returns>
    protected virtual Task<TApiModel> ApplyTimelineOverridesAsync(
        TApiModel apiModel,
        TEntity entity,
        string? expand,
        string? consumer,
        CancellationToken cancellationToken = default)
    {
        // Base implementation does nothing - timelineOverrides should be implemented by specific controllers
        return Task.FromResult(apiModel);
    }
}
