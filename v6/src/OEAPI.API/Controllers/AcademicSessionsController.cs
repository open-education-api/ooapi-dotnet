using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Infrastructure.Data.Context;
using OEAPI.Infrastructure.Data.Entities;
using OEAPI.Infrastructure.Data.Mapping;
using OEAPI.Infrastructure.Query;
using OEAPI.Infrastructure.Query.Consumers;
using OEAPI.Infrastructure.Query.Extensions;

namespace OEAPI.API.Controllers;

/// <summary>
///     Controller for academic session endpoints.
/// </summary>
/// <remarks>
///     Initializes a new instance of the AcademicSessionsController class.
/// </remarks>
/// <param name="dbContext">The database context.</param>
/// <param name="logger">The logger.</param>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
[Tags("Academic Sessions")]
public class AcademicSessionsController(OEAPIDbContext dbContext, ILogger<AcademicSessionsController> logger) : GenericEntityController<AcademicSessionEntity, AcademicSession>(dbContext, logger, "AcademicSession", "academic-sessions")
{
    private readonly OEAPIDbContext _dbContext = dbContext;

    /// <summary>Retrieves a list of all academic sessions.</summary>
    /// <param name="primaryCode">Filter by primary code.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response for Scalar/OpenAPI doc generation -
    // TApiModel can't be referenced in an attribute on the generic base class (CS0416). Behaviour is
    // unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(PagedResult<AcademicSession>), StatusCodes.Status200OK)]
    public override Task<IActionResult> GetAll(
        string? primaryCode,
        int? pageNumber,
        int? pageSize,
        string? consumer,
        string? fields,
        CancellationToken cancellationToken = default)
    {
        return base.GetAll(primaryCode, pageNumber, pageSize, consumer, fields, cancellationToken);
    }

    /// <summary>Retrieves a single academic session by ID.</summary>
    /// <param name="academicSessionId">The academic session ID.</param>
    /// <param name="expand">Comma-separated list of relations to expand, e.g. <c>parent,children</c>.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response - see the GetAll override above.
    // Behaviour is unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(AcademicSession), StatusCodes.Status200OK)]
    [HttpGet("{academicSessionId}")]
    public override Task<IActionResult> GetById(
        string academicSessionId,
        string? expand,
        string? fields,
        string? consumer,
        CancellationToken cancellationToken = default)
    {
        return base.GetById(academicSessionId, expand, fields, consumer, cancellationToken);
    }

    /// <summary>
    ///     Applies the <c>academicSessionType</c>, <c>parent</c>, and <c>year</c> filters to a
    ///     <c>GetAll</c> query.
    /// </summary>
    protected override IQueryable<AcademicSessionEntity> ApplyEntityFilters(IQueryable<AcademicSessionEntity> query)
    {
        string? academicSessionTypeValue = GetQueryValue("academicSessionType");
        if (!string.IsNullOrEmpty(academicSessionTypeValue))
            query = query.Where(a =>
                a.AcademicSessionType != null &&
                a.AcademicSessionType.ToLower() == academicSessionTypeValue.ToLower());

        string? parent = GetQueryValue("parent");
        if (!string.IsNullOrEmpty(parent))
            query = query.Where(a => a.Parent != null && a.Parent.AcademicSessionId == parent);

        string? year = GetQueryValue("year");
        if (!string.IsNullOrEmpty(year)) query = query.Where(a => a.Year != null && a.Year.AcademicSessionId == year);

        return query;
    }

    /// <summary>
    ///     Normalizes a <c>since</c>/<c>until</c> query value's formatting to match this codebase's
    ///     own stored-data convention (fixed-width, no fractional seconds, colon-separated offset)
    ///     before a string comparison against a <c>StartDateTime</c>/<c>EndDateTime</c> column - fixes
    ///     the precision-mismatch class of the raw-string-comparison bug (e.g. a client re-serializing
    ///     a parsed <see cref="DateTimeOffset" /> with fractional-second precision the stored data
    ///     doesn't have). Falls back to the raw value unchanged if it doesn't parse. Known limitation,
    ///     accepted: doesn't resolve differing UTC offsets between the query value and stored data -
    ///     see design.md Decision 3 of the <c>fix-since-until-academic-session-target</c> change.
    /// </summary>
    private static string? NormalizeDateTimeFilterValue(string? value)
    {
        return DateTimeOffset.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None,
            out DateTimeOffset parsed)
            ? parsed.ToString("yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture)
            : value;
    }

    /// <summary>
    ///     Resolves the effective <c>since</c> value for a filter comparison: the client's own value,
    ///     normalized, or today (UTC) when the client omits <c>since</c> entirely - every
    ///     offering-collection endpoint's own parameter description says "By default only future
    ///     offerings are shown (equal to <c>?since=&lt;today&gt;</c>)", confirmed to apply only to
    ///     <c>since</c> (never <c>until</c>) - see design.md of the
    ///     <c>add-since-default-today-filter</c> change.
    /// </summary>
    private static string GetEffectiveSince(string? since)
    {
        return string.IsNullOrEmpty(since)
            ? DateTimeOffset.UtcNow.Date.ToString("yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture)
            : NormalizeDateTimeFilterValue(since)!;
    }

    /// <summary>
    ///     Eagerly loads the single-valued Parent/Year navigations (and the Children collection, so
    ///     <c>childIds</c> can be populated without an extra query) - see
    ///     <see cref="GenericEntityController{TEntity,TApiModel}.ApplyIncludes" /> for why.
    /// </summary>
    protected override IQueryable<AcademicSessionEntity> ApplyIncludes(IQueryable<AcademicSessionEntity> query)
    {
        return query.IncludeHierarchy();
    }

    // ========================================================================
    // Required abstract method implementations
    // ========================================================================

    /// <summary>
    ///     Maps an AcademicSessionEntity to an AcademicSession API model.
    /// </summary>
    /// <param name="entity">The entity to map.</param>
    /// <param name="consumer">The requested consumer, for consumer-specific data.</param>
    /// <returns>The API model.</returns>
    protected override AcademicSession MapToApiModel(AcademicSessionEntity entity, string? consumer)
    {
        return entity.ToApiModel(consumer);
    }

    /// <summary>
    ///     Maps an AcademicSession API model to an AcademicSessionEntity. Not currently called by any
    ///     live code path (see the note on writes in <see cref="GenericEntityController{TEntity,TApiModel}" />).
    ///     Note: resolving <c>model.ParentId</c>/<c>model.YearId</c> (external string IDs) to the
    ///     entity's internal <see cref="AcademicSessionEntity.ParentEntityId" />/
    ///     <see cref="AcademicSessionEntity.YearEntityId" />
    ///     Guid FKs requires an async DB lookup that this synchronous method can't perform - whichever
    ///     explicit write action ends up calling this needs to resolve and set those FKs itself after
    ///     calling this method.
    /// </summary>
    /// <param name="model">The API model to map.</param>
    /// <returns>The entity.</returns>
    protected override AcademicSessionEntity MapToEntity(AcademicSession model)
    {
        return model.ToEntity();
    }

    /// <summary>
    ///     Updates an AcademicSessionEntity from an AcademicSession API model. See the note on
    ///     <see cref="MapToEntity" /> regarding Parent/Year FK resolution.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="model">The API model with new data.</param>
    protected override void UpdateEntityFromApiModel(AcademicSessionEntity entity, AcademicSession model)
    {
        entity.UpdateFrom(model);
    }

    /// <summary>
    ///     Gets the unique identifier for an academic session entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>The identifier as string.</returns>
    protected override string GetEntityId(AcademicSessionEntity entity)
    {
        return entity.AcademicSessionId;
    }

    // ========================================================================
    // Custom methods for AcademicSessionsController
    // ========================================================================

    /// <summary>
    ///     Applies default ordering for academic sessions: by start date descending, then by name.
    /// </summary>
    /// <param name="query">The query to order.</param>
    /// <returns>The ordered query.</returns>
    protected override IQueryable<AcademicSessionEntity> ApplyDefaultOrdering(IQueryable<AcademicSessionEntity> query)
    {
        return query.OrderByDescending(a => a.StartDateTime).ThenBy(a => a.NameJson);
    }

    /// <summary>
    ///     Applies expand functionality for academic sessions.
    /// </summary>
    /// <param name="apiModel">The API model to expand.</param>
    /// <param name="expand">The expand parameter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the expanded API model.</returns>
    protected override async Task<AcademicSession> ApplyExpandAsync(
        AcademicSession apiModel,
        string expand,
        CancellationToken cancellationToken = default)
    {
        string[] expands = expand.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        foreach (string expandOption in expands)
            switch (expandOption.ToLower())
            {
                case "parent":
                    if (!string.IsNullOrEmpty(apiModel.ParentId?.Value))
                    {
                        AcademicSessionEntity? parentEntity = await Entities
                            .FirstOrDefaultAsync(a => a.AcademicSessionId == apiModel.ParentId.Value, cancellationToken)
                            .ConfigureAwait(false);

                        if (parentEntity != null)
                            // Expand sub-objects don't currently thread the requested consumer
                            // through - see the remarks on ApplyExpandAsync in GenericEntityController.
                            apiModel.Parent = MapToApiModel(parentEntity, null);
                    }

                    break;

                case "children":
                    if (apiModel.ChildIds != null && apiModel.ChildIds.Length > 0)
                    {
                        List<AcademicSessionEntity> childEntities = await Entities
                            .Where(a => a.ParentEntityId != null &&
                                        a.Parent!.AcademicSessionId == apiModel.AcademicSessionId)
                            .ToListAsync(cancellationToken)
                            .ConfigureAwait(false);

                        apiModel.Children = [.. childEntities.Select(e => MapToApiModel(e, null))];
                    }

                    break;

                case "year":
                    if (!string.IsNullOrEmpty(apiModel.YearId?.Value))
                    {
                        AcademicSessionEntity? yearEntity = await Entities
                            .FirstOrDefaultAsync(a => a.AcademicSessionId == apiModel.YearId.Value, cancellationToken)
                            .ConfigureAwait(false);

                        if (yearEntity != null) apiModel.Year = MapToApiModel(yearEntity, null);
                    }

                    break;
            }

        return apiModel;
    }

    // ========================================================================
    // Nested endpoints for academic-sessions/{academicSessionId}/course-offerings
    // ========================================================================

    /// <summary>
    ///     Retrieves all course offerings for a specific academic session.
    ///     GET /api/academic-sessions/{academicSessionId}/course-offerings
    /// </summary>
    /// <remarks>
    ///     Get a list of all course offerings during this academic session.
    /// </remarks>
    /// <param name="academicSessionId">The academic session ID.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="q">Search term.</param>
    /// <param name="teachingLanguage">Filter by teaching language.</param>
    /// <param name="state">Filter by offering state.</param>
    /// <param name="resultExpected">Filter by resultExpected.</param>
    /// <param name="since">Filter by minimum start moment (RFC3339 full-date).</param>
    /// <param name="until">Filter by maximum end moment (RFC3339 full-date).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{academicSessionId}/course-offerings")]
    [ProducesResponseType(typeof(PagedResult<CourseOffering>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCourseOfferingsByAcademicSessionId(
        string academicSessionId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? consumer,
        [FromQuery(Name = "fields")] string? fields,
        [FromQuery] string? q,
        [FromQuery] string? teachingLanguage,
        [FromQuery] string? state,
        [FromQuery] bool? resultExpected,
        [FromQuery] string? since,
        [FromQuery] string? until,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // First, verify the academic session exists
            AcademicSessionEntity? academicSessionEntity = await _dbContext.AcademicSessions
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AcademicSessionId == academicSessionId, cancellationToken)
                .ConfigureAwait(false);

            if (academicSessionEntity == null)
                return Problem(
                    detail: $"Academic session with ID '{academicSessionId}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            // Get course offerings for this academic session using the proper foreign key
            IQueryable<CourseOfferingEntity> query = _dbContext.CourseOfferings
                .AsNoTracking()
                .Include(c => c.Course)
                .Include(c => c.Organisation)
                .Include(c => c.AcademicSession)
                .Include(c => c.Groups)
                .Include(c => c.ProgrammeOfferings)
                .Where(c => c.AcademicSessionEntityId != null && c.AcademicSessionEntityId == academicSessionEntity.Id);

            // Apply the spec's filter_query DSL
            query = ApplyFilterQuery(query);

            // Apply search if requested
            if (!string.IsNullOrEmpty(q))
                query = query.WhereContains(q, nameof(CourseOfferingEntity.NameJson),
                    nameof(CourseOfferingEntity.PrimaryCode));

            // Apply consumer filtering
            query = ConsumerKeyFilter.Apply(query, consumer);

            // Apply teachingLanguage filter - TeachingLanguagesJson is a plain JSON-array-of-strings
            // column, so a substring Contains (portable LIKE across both providers) is the only query
            // this codebase has infrastructure for; see design.md Decision 4.
            if (!string.IsNullOrEmpty(teachingLanguage))
                query = query.Where(c =>
                    c.TeachingLanguagesJson != null && c.TeachingLanguagesJson.Contains(teachingLanguage));

            // Apply state filter (the spec's offeringState parameter - "state" is its actual wire name)
            if (!string.IsNullOrEmpty(state)) query = query.Where(c => c.State != null && c.State == state);

            // Apply resultExpected filter
            if (resultExpected.HasValue) query = query.Where(c => c.ResultExpected == resultExpected.Value);

            // Apply since/until filters (spec: filters by the corresponding academic session's own
            // dates, not the offering's own - every row here is already scoped to this one session,
            // so compare its own StartDateTime/EndDateTime once rather than per row).
            // since always applies - defaults to today when the client omits it.
            string effectiveSince = GetEffectiveSince(since);
            query = query.Where(_ =>
                !string.IsNullOrEmpty(academicSessionEntity.StartDateTime) &&
                academicSessionEntity.StartDateTime.CompareTo(effectiveSince) >= 0);

            if (!string.IsNullOrEmpty(until))
            {
                string? normalizedUntil = NormalizeDateTimeFilterValue(until);
                query = query.Where(_ =>
                    !string.IsNullOrEmpty(academicSessionEntity.EndDateTime) &&
                    academicSessionEntity.EndDateTime.CompareTo(normalizedUntil) <= 0);
            }

            // Apply pagination
            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);

            int totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            // Apply default ordering
            query = ApplyCourseOfferingDefaultOrdering(query);

            List<CourseOfferingEntity> items = await query
                .Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            List<CourseOffering> apiModels = [.. items.Select(item => item.ToApiModel(consumer))];

            // Create paged result
            PagedResult<CourseOffering> result = new(
                apiModels, totalCount, validatedPage, validatedPageSize);

            return PagedResponseWithFieldSelection(result, fields);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    ///     Retrieves all learning component offerings for a specific academic session.
    ///     GET /api/academic-sessions/{academicSessionId}/learning-component-offerings
    /// </summary>
    /// <param name="academicSessionId">The academic session ID.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="q">Search term.</param>
    /// <param name="teachingLanguage">Filter by teaching language.</param>
    /// <param name="state">Filter by offering state.</param>
    /// <param name="resultExpected">Filter by resultExpected.</param>
    /// <param name="since">Filter by minimum start moment (RFC3339 full-date).</param>
    /// <param name="until">Filter by maximum end moment (RFC3339 full-date).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{academicSessionId}/learning-component-offerings")]
    [ProducesResponseType(typeof(PagedResult<LearningComponentOffering>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLearningComponentOfferingsByAcademicSessionId(
        string academicSessionId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? consumer,
        [FromQuery(Name = "fields")] string? fields,
        [FromQuery] string? q,
        [FromQuery] string? teachingLanguage,
        [FromQuery] string? state,
        [FromQuery] bool? resultExpected,
        [FromQuery] string? since,
        [FromQuery] string? until,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // First, verify the academic session exists
            AcademicSessionEntity? academicSessionEntity = await _dbContext.AcademicSessions
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AcademicSessionId == academicSessionId, cancellationToken)
                .ConfigureAwait(false);

            if (academicSessionEntity == null)
                return Problem(
                    detail: $"Academic session with ID '{academicSessionId}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            // Get learning component offerings for this academic session
            IQueryable<LearningComponentOfferingEntity> query = _dbContext.LearningComponentOfferings
                .AsNoTracking()
                .Include(lc => lc.LearningComponent)
                .Include(lc => lc.Organisation)
                .Include(lc => lc.AcademicSession)
                .Include(lc => lc.Rooms)
                .Include(lc => lc.CourseOfferings)
                .Include(lc => lc.Groups)
                .Where(lc =>
                    lc.AcademicSessionEntityId != null && lc.AcademicSessionEntityId == academicSessionEntity.Id);

            // Apply the spec's filter_query DSL
            query = ApplyFilterQuery(query);

            // Apply search if requested
            if (!string.IsNullOrEmpty(q))
                query = query.WhereContains(q, nameof(LearningComponentOfferingEntity.NameJson),
                    nameof(LearningComponentOfferingEntity.PrimaryCode));

            // Apply teachingLanguage filter - TeachingLanguagesJson is a plain JSON-array-of-strings
            // column, so a substring Contains (portable LIKE across both providers) is the only query
            // this codebase has infrastructure for; see design.md Decision 4.
            if (!string.IsNullOrEmpty(teachingLanguage))
                query = query.Where(lc =>
                    lc.TeachingLanguagesJson != null && lc.TeachingLanguagesJson.Contains(teachingLanguage));

            // Apply state filter (the spec's offeringState parameter - "state" is its actual wire name)
            if (!string.IsNullOrEmpty(state)) query = query.Where(lc => lc.State != null && lc.State == state);

            // Apply resultExpected filter
            if (resultExpected.HasValue) query = query.Where(lc => lc.ResultExpected == resultExpected.Value);

            // Apply since/until filters (spec: filters by the corresponding academic session's own
            // dates, not the offering's own - every row here is already scoped to this one session,
            // so compare its own StartDateTime/EndDateTime once rather than per row).
            // since always applies - defaults to today when the client omits it.
            string effectiveSince = GetEffectiveSince(since);
            query = query.Where(_ =>
                !string.IsNullOrEmpty(academicSessionEntity.StartDateTime) &&
                academicSessionEntity.StartDateTime.CompareTo(effectiveSince) >= 0);

            if (!string.IsNullOrEmpty(until))
            {
                string? normalizedUntil = NormalizeDateTimeFilterValue(until);
                query = query.Where(_ =>
                    !string.IsNullOrEmpty(academicSessionEntity.EndDateTime) &&
                    academicSessionEntity.EndDateTime.CompareTo(normalizedUntil) <= 0);
            }

            // Apply pagination
            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);

            int totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            // Apply default ordering
            query = ApplyLearningComponentOfferingDefaultOrdering(query);

            List<LearningComponentOfferingEntity> items = await query
                .Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            List<LearningComponentOffering> apiModels = [.. items.Select(item => item.ToApiModel(consumer))];

            // Create paged result
            PagedResult<LearningComponentOffering> result = new(
                apiModels, totalCount, validatedPage, validatedPageSize);

            return PagedResponseWithFieldSelection(result, fields);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    ///     Retrieves all programme offerings for a specific academic session.
    ///     GET /api/academic-sessions/{academicSessionId}/programme-offerings
    /// </summary>
    /// <param name="academicSessionId">The academic session ID.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="q">Search term.</param>
    /// <param name="teachingLanguage">Filter by teaching language.</param>
    /// <param name="state">Filter by offering state.</param>
    /// <param name="resultExpected">Filter by resultExpected.</param>
    /// <param name="since">Filter by minimum start moment (RFC3339 full-date).</param>
    /// <param name="until">Filter by maximum end moment (RFC3339 full-date).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{academicSessionId}/programme-offerings")]
    [ProducesResponseType(typeof(PagedResult<ProgrammeOffering>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProgrammeOfferingsByAcademicSessionId(
        string academicSessionId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? consumer,
        [FromQuery(Name = "fields")] string? fields,
        [FromQuery] string? q,
        [FromQuery] string? teachingLanguage,
        [FromQuery] string? state,
        [FromQuery] bool? resultExpected,
        [FromQuery] string? since,
        [FromQuery] string? until,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // First, verify the academic session exists
            AcademicSessionEntity? academicSessionEntity = await _dbContext.AcademicSessions
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AcademicSessionId == academicSessionId, cancellationToken)
                .ConfigureAwait(false);

            if (academicSessionEntity == null)
                return Problem(
                    detail: $"Academic session with ID '{academicSessionId}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            // Get programme offerings for this academic session
            IQueryable<ProgrammeOfferingEntity> query = _dbContext.ProgrammeOfferings
                .AsNoTracking()
                .Include(po => po.Programme)
                .Include(po => po.Organisation)
                .Include(po => po.AcademicSession)
                .Include(po => po.Groups)
                .Where(po =>
                    po.AcademicSessionEntityId != null && po.AcademicSessionEntityId == academicSessionEntity.Id);

            // Apply the spec's filter_query DSL
            query = ApplyFilterQuery(query);

            // Apply search if requested
            if (!string.IsNullOrEmpty(q))
                query = query.WhereContains(q, nameof(ProgrammeOfferingEntity.NameJson),
                    nameof(ProgrammeOfferingEntity.PrimaryCode));

            // Apply teachingLanguage filter - TeachingLanguagesJson is a plain JSON-array-of-strings
            // column, so a substring Contains (portable LIKE across both providers) is the only query
            // this codebase has infrastructure for; see design.md Decision 4.
            if (!string.IsNullOrEmpty(teachingLanguage))
                query = query.Where(po =>
                    po.TeachingLanguagesJson != null && po.TeachingLanguagesJson.Contains(teachingLanguage));

            // Apply state filter (the spec's offeringState parameter - "state" is its actual wire name)
            if (!string.IsNullOrEmpty(state)) query = query.Where(po => po.State != null && po.State == state);

            // Apply resultExpected filter
            if (resultExpected.HasValue) query = query.Where(po => po.ResultExpected == resultExpected.Value);

            // Apply since/until filters (spec: filters by the corresponding academic session's own
            // dates, not the offering's own - every row here is already scoped to this one session,
            // so compare its own StartDateTime/EndDateTime once rather than per row).
            // since always applies - defaults to today when the client omits it.
            string effectiveSince = GetEffectiveSince(since);
            query = query.Where(_ =>
                !string.IsNullOrEmpty(academicSessionEntity.StartDateTime) &&
                academicSessionEntity.StartDateTime.CompareTo(effectiveSince) >= 0);

            if (!string.IsNullOrEmpty(until))
            {
                string? normalizedUntil = NormalizeDateTimeFilterValue(until);
                query = query.Where(_ =>
                    !string.IsNullOrEmpty(academicSessionEntity.EndDateTime) &&
                    academicSessionEntity.EndDateTime.CompareTo(normalizedUntil) <= 0);
            }

            // Apply pagination
            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);

            int totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            // Apply default ordering
            query = ApplyProgrammeOfferingDefaultOrdering(query);

            List<ProgrammeOfferingEntity> items = await query
                .Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            List<ProgrammeOffering> apiModels = [.. items.Select(item => item.ToApiModel(consumer))];

            // Create paged result
            PagedResult<ProgrammeOffering> result = new(
                apiModels, totalCount, validatedPage, validatedPageSize);

            return PagedResponseWithFieldSelection(result, fields);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    ///     Retrieves all test component offerings for a specific academic session.
    ///     GET /api/academic-sessions/{academicSessionId}/test-component-offerings
    /// </summary>
    /// <param name="academicSessionId">The academic session ID.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="q">Search term.</param>
    /// <param name="teachingLanguage">Filter by teaching language.</param>
    /// <param name="state">Filter by offering state.</param>
    /// <param name="resultExpected">Filter by resultExpected.</param>
    /// <param name="since">Filter by minimum start moment (RFC3339 full-date).</param>
    /// <param name="until">Filter by maximum end moment (RFC3339 full-date).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{academicSessionId}/test-component-offerings")]
    [ProducesResponseType(typeof(PagedResult<TestComponentOffering>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTestComponentOfferingsByAcademicSessionId(
        string academicSessionId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? consumer,
        [FromQuery(Name = "fields")] string? fields,
        [FromQuery] string? q,
        [FromQuery] string? teachingLanguage,
        [FromQuery] string? state,
        [FromQuery] bool? resultExpected,
        [FromQuery] string? since,
        [FromQuery] string? until,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // First, verify the academic session exists
            AcademicSessionEntity? academicSessionEntity = await _dbContext.AcademicSessions
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AcademicSessionId == academicSessionId, cancellationToken)
                .ConfigureAwait(false);

            if (academicSessionEntity == null)
                return Problem(
                    detail: $"Academic session with ID '{academicSessionId}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            // Get test component offerings for this academic session
            IQueryable<TestComponentOfferingEntity> query = _dbContext.TestComponentOfferings
                .AsNoTracking()
                .Include(t => t.TestComponent)
                .Include(t => t.Organisation)
                .Include(t => t.AcademicSession)
                .Include(t => t.Rooms)
                .Include(t => t.CourseOfferings)
                .Include(t => t.Groups)
                .Where(t => t.AcademicSessionEntityId != null && t.AcademicSessionEntityId == academicSessionEntity.Id);

            // Apply the spec's filter_query DSL
            query = ApplyFilterQuery(query);

            // Apply search if requested - was searching the internal GUID-shaped IdValue instead of
            // name/code (same bug class already fixed elsewhere in fix-response-field-and-parameter-
            // bugs), found via this change's own live-verify once a real matchable fixture made this
            // endpoint's baseline reachable for the first time.
            if (!string.IsNullOrEmpty(q))
                query = query.WhereContains(q, nameof(TestComponentOfferingEntity.NameJson),
                    nameof(TestComponentOfferingEntity.PrimaryCode));

            // Apply teachingLanguage filter - TeachingLanguagesJson is a plain JSON-array-of-strings
            // column, so a substring Contains (portable LIKE across both providers) is the only query
            // this codebase has infrastructure for; see design.md Decision 4.
            if (!string.IsNullOrEmpty(teachingLanguage))
                query = query.Where(t =>
                    t.TeachingLanguagesJson != null && t.TeachingLanguagesJson.Contains(teachingLanguage));

            // Apply state filter (the spec's offeringState parameter - "state" is its actual wire name)
            if (!string.IsNullOrEmpty(state)) query = query.Where(t => t.State != null && t.State == state);

            // Apply resultExpected filter
            if (resultExpected.HasValue) query = query.Where(t => t.ResultExpected == resultExpected.Value);

            // Apply since/until filters (spec: filters by the corresponding academic session's own
            // dates, not the offering's own - every row here is already scoped to this one session,
            // so compare its own StartDateTime/EndDateTime once rather than per row).
            // since always applies - defaults to today when the client omits it.
            string effectiveSince = GetEffectiveSince(since);
            query = query.Where(_ =>
                !string.IsNullOrEmpty(academicSessionEntity.StartDateTime) &&
                academicSessionEntity.StartDateTime.CompareTo(effectiveSince) >= 0);

            if (!string.IsNullOrEmpty(until))
            {
                string? normalizedUntil = NormalizeDateTimeFilterValue(until);
                query = query.Where(_ =>
                    !string.IsNullOrEmpty(academicSessionEntity.EndDateTime) &&
                    academicSessionEntity.EndDateTime.CompareTo(normalizedUntil) <= 0);
            }

            // Apply pagination
            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);

            int totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            // Apply default ordering
            query = ApplyTestComponentOfferingDefaultOrdering(query);

            List<TestComponentOfferingEntity> items = await query
                .Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            List<TestComponentOffering> apiModels = [.. items.Select(item => item.ToApiModel(consumer))];

            // Create paged result
            PagedResult<TestComponentOffering> result = new(
                apiModels, totalCount, validatedPage, validatedPageSize);

            return PagedResponseWithFieldSelection(result, fields);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    ///     Applies default ordering for course offerings: by start date, then by primary code.
    /// </summary>
    /// <param name="query">The query to order.</param>
    /// <returns>The ordered query.</returns>
    private IQueryable<CourseOfferingEntity> ApplyCourseOfferingDefaultOrdering(IQueryable<CourseOfferingEntity> query)
    {
        return query.OrderBy(c => c.StartDateTime).ThenBy(c => c.PrimaryCode);
    }

    /// <summary>
    ///     Applies default ordering for learning component offerings: by start date, then by ID.
    /// </summary>
    /// <param name="query">The query to order.</param>
    /// <returns>The ordered query.</returns>
    private IQueryable<LearningComponentOfferingEntity> ApplyLearningComponentOfferingDefaultOrdering(
        IQueryable<LearningComponentOfferingEntity> query)
    {
        return query.OrderBy(lc => lc.StartDateTime).ThenBy(lc => lc.LearningComponentOfferingIdValue);
    }

    /// <summary>
    ///     Applies default ordering for programme offerings: by start date, then by ID.
    /// </summary>
    /// <param name="query">The query to order.</param>
    /// <returns>The ordered query.</returns>
    private IQueryable<ProgrammeOfferingEntity> ApplyProgrammeOfferingDefaultOrdering(
        IQueryable<ProgrammeOfferingEntity> query)
    {
        return query.OrderBy(p => p.StartDateTime).ThenBy(p => p.ProgrammeOfferingIdValue);
    }

    /// <summary>
    ///     Applies default ordering for test component offerings: by start date, then by ID.
    /// </summary>
    /// <param name="query">The query to order.</param>
    /// <returns>The ordered query.</returns>
    private IQueryable<TestComponentOfferingEntity> ApplyTestComponentOfferingDefaultOrdering(
        IQueryable<TestComponentOfferingEntity> query)
    {
        return query.OrderBy(tc => tc.StartDateTime).ThenBy(tc => tc.TestComponentOfferingIdValue);
    }
}
