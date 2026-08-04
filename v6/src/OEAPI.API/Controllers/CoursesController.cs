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
///     Controller for course endpoints.
/// </summary>
/// <remarks>
///     Initializes a new instance of the CoursesController class.
/// </remarks>
/// <param name="dbContext">The database context.</param>
/// <param name="logger">The logger.</param>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
[Tags("Courses")]
public class CoursesController(OEAPIDbContext dbContext, ILogger<CoursesController> logger) : GenericEntityController<CourseEntity, Course>(dbContext, logger, "Course", "courses")
{
    private readonly OEAPIDbContext _dbContext = dbContext;

    /// <summary>Retrieves a list of all courses.</summary>
    /// <param name="primaryCode">Filter by primary code.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response for Scalar/OpenAPI doc generation -
    // TApiModel can't be referenced in an attribute on the generic base class (CS0416). Behaviour is
    // unchanged; forwards straight to the base implementation. A plain comment (not <remarks>) so this
    // implementation note doesn't leak into the public OpenAPI operation description.
    [ProducesResponseType(typeof(PagedResult<Course>), StatusCodes.Status200OK)]
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

    /// <summary>Retrieves a single course by ID.</summary>
    /// <param name="courseId">The course ID.</param>
    /// <param name="expand">Comma-separated list of relations to expand, e.g. <c>organisation,coordinators</c>.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response - see the GetAll override above.
    // Behaviour is unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(Course), StatusCodes.Status200OK)]
    [HttpGet("{courseId}")]
    public override Task<IActionResult> GetById(
        string courseId,
        string? expand,
        string? fields,
        string? consumer,
        CancellationToken cancellationToken = default)
    {
        return base.GetById(courseId, expand, fields, consumer, cancellationToken);
    }

    /// <summary>
    ///     Finds a course by unique identifier (CourseId).
    /// </summary>
    /// <param name="idValue">The course ID value.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the course entity or null.</returns>
    protected override async Task<CourseEntity?> FindByUniqueIdAsync(string idValue,
        CancellationToken cancellationToken = default)
    {
        return await Entities
            .FirstOrDefaultAsync(c => c.CourseId == idValue, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    ///     Eager-loads the relationships needed to always populate <c>organisationId</c>/
    ///     <c>coordinatorIds</c>/<c>instructorIds</c>/<c>programmeIds</c>/<c>learningOutcomeIds</c> -
    ///     per spec, these identifier fields must always be present regardless of <c>expand</c> (only
    ///     the full nested objects are expand-conditional). Without this, <see cref="MapToApiModel" />
    ///     would see empty/null navigation properties on every response that didn't explicitly request
    ///     the matching <c>expand</c> value.
    /// </summary>
    protected override IQueryable<CourseEntity> ApplyIncludes(IQueryable<CourseEntity> query)
    {
        return query
            .Include(c => c.Organisation).ThenInclude(o => o!.Parent)
            .Include(c => c.Organisation).ThenInclude(o => o!.Root)
            .Include(c => c.Organisation).ThenInclude(o => o!.Children)
            .Include(c => c.CourseCoordinatorEntities)
            .Include(c => c.CourseInstructorEntities)
            .Include(c => c.ProgrammeEntities).ThenInclude(p => p.Parent)
            .Include(c => c.ProgrammeEntities).ThenInclude(p => p.Children)
            .Include(c => c.ProgrammeEntities).ThenInclude(p => p.Organisation)
            .Include(c => c.ProgrammeEntities).ThenInclude(p => p.Coordinators)
            .Include(c => c.ProgrammeEntities).ThenInclude(p => p.Instructors)
            .Include(c => c.LearningOutcomes).ThenInclude(lo => lo.Organisation)
            .Include(c => c.LearningOutcomes).ThenInclude(lo => lo.Parents)
            .Include(c => c.LearningOutcomes).ThenInclude(lo => lo.Children);
    }

    // ========================================================================
    // Required abstract method implementations
    // ========================================================================

    /// <summary>
    ///     Maps a CourseEntity to a Course API model.
    /// </summary>
    /// <param name="entity">The entity to map.</param>
    /// <param name="consumer">The requested consumer, for consumer-specific data.</param>
    /// <returns>The API model.</returns>
    protected override Course MapToApiModel(CourseEntity entity, string? consumer)
    {
        return entity.ToApiModel(consumer);
    }

    /// <summary>
    ///     Maps a Course API model to a CourseEntity.
    /// </summary>
    /// <param name="model">The API model to map.</param>
    /// <returns>The entity.</returns>
    protected override CourseEntity MapToEntity(Course model)
    {
        return model.ToEntity();
    }

    /// <summary>
    ///     Updates a CourseEntity from a Course API model.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="model">The API model with new data.</param>
    protected override void UpdateEntityFromApiModel(CourseEntity entity, Course model)
    {
        entity.UpdateFrom(model);
    }

    /// <summary>
    ///     Gets the unique identifier for a course entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>The identifier as string.</returns>
    protected override string GetEntityId(CourseEntity entity)
    {
        return entity.CourseId;
    }

    // ========================================================================
    // Custom methods for CoursesController
    // ========================================================================

    /// <summary>
    ///     Applies default ordering for courses: by primary code, then by name.
    /// </summary>
    /// <param name="query">The query to order.</param>
    /// <returns>The ordered query.</returns>
    protected override IQueryable<CourseEntity> ApplyDefaultOrdering(IQueryable<CourseEntity> query)
    {
        return query.OrderBy(c => c.PrimaryCode).ThenBy(c => c.NameJson);
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
    ///     Applies the spec's <c>q</c> (search), <c>teachingLanguage</c>, <c>level</c> and
    ///     <c>modeOfDelivery</c> filters to a <c>GetAll</c> query - not formal parameters, since
    ///     <c>GetAll</c>'s signature is fixed by the generic base controller (see
    ///     <see cref="GenericEntityController{TEntity,TApiModel}.ApplyEntityFilters" />; mirrors
    ///     <c>PersonsController.ApplyEntityFilters</c>'s <c>q</c> pattern).
    /// </summary>
    protected override IQueryable<CourseEntity> ApplyEntityFilters(IQueryable<CourseEntity> query)
    {
        // Spec: q (search) - partial, case-insensitive match against name, abbreviation or
        // description. Explicitly lower-cased on both sides rather than using the shared
        // WhereContains (which relies on each provider's default LIKE collation - case-insensitive
        // on SQL Server but case-sensitive on PostgreSQL by default), since the spec mandates
        // case-insensitivity for this parameter.
        string? searchValue = GetQueryValue("q");
        if (!string.IsNullOrEmpty(searchValue))
        {
            string searchTerm = searchValue.ToLower();
            query = query.Where(c =>
                c.NameJson.ToLower().Contains(searchTerm) ||
                (c.Abbreviation != null && c.Abbreviation.ToLower().Contains(searchTerm)) ||
                (c.DescriptionJson != null && c.DescriptionJson.ToLower().Contains(searchTerm)));
        }

        // Spec: teachingLanguage - TeachingLanguagesJson is a plain JSON-array-of-strings column, so
        // a substring Contains (portable LIKE across both providers) is the only query this codebase
        // has infrastructure for; see design.md Decision 4.
        string? teachingLanguageValue = GetQueryValue("teachingLanguage");
        if (!string.IsNullOrEmpty(teachingLanguageValue))
            query = query.Where(c =>
                c.TeachingLanguagesJson != null && c.TeachingLanguagesJson.Contains(teachingLanguageValue));

        // Spec: level - direct equality against the course's Level column.
        string? levelValue = GetQueryValue("level");
        if (!string.IsNullOrEmpty(levelValue))
            query = query.Where(c => c.Level != null && c.Level == levelValue);

        // Spec: modeOfDelivery - same JSON-array-substring approach as teachingLanguage.
        string? modeOfDeliveryValue = GetQueryValue("modeOfDelivery");
        if (!string.IsNullOrEmpty(modeOfDeliveryValue))
            query = query.Where(c =>
                c.ModesOfDeliveryJson != null && c.ModesOfDeliveryJson.Contains(modeOfDeliveryValue));

        return query;
    }

    // ========================================================================
    // Nested endpoints for courses/{courseId}/course-offerings
    // ========================================================================

    /// <summary>
    ///     Retrieves all course offerings for a specific course.
    ///     GET /api/courses/{courseId}/course-offerings
    /// </summary>
    /// <remarks>
    ///     Get a list of all course offerings for this course, ordered chronologically.
    /// </remarks>
    /// <param name="courseId">The course ID.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="q">Search term.</param>
    /// <param name="teachingLanguage">Filter by teaching language.</param>
    /// <param name="state">Filter by offering state.</param>
    /// <param name="modeOfDelivery">Filter by mode of delivery.</param>
    /// <param name="resultExpected">Filter by resultExpected.</param>
    /// <param name="since">Filter by minimum start moment (RFC3339 full-date).</param>
    /// <param name="until">Filter by maximum end moment (RFC3339 full-date).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{courseId}/course-offerings")]
    [ProducesResponseType(typeof(PagedResult<CourseOffering>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCourseOfferingsByCourseId(
        string courseId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? consumer,
        [FromQuery] string? fields,
        [FromQuery] string? q,
        [FromQuery] string? teachingLanguage,
        [FromQuery] string? state,
        [FromQuery] string? modeOfDelivery,
        [FromQuery] bool? resultExpected,
        [FromQuery] string? since,
        [FromQuery] string? until,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // First, verify the course exists
            CourseEntity? courseEntity = await _dbContext.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CourseId == courseId, cancellationToken)
                .ConfigureAwait(false);

            if (courseEntity == null)
                return Problem(
                    detail: $"Course with ID '{courseId}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            // Get course offerings for this course
            IQueryable<CourseOfferingEntity> query = _dbContext.CourseOfferings
                .AsNoTracking()
                .Include(c => c.Course)
                .Include(c => c.Organisation)
                .Include(c => c.AcademicSession)
                .Include(c => c.Groups)
                .Include(c => c.ProgrammeOfferings)
                .Where(c => c.CourseEntityId != null && c.CourseEntityId == courseEntity.Id);

            // Apply the spec's filter_query DSL
            query = ApplyFilterQuery(query);

            // Apply search if requested
            if (!string.IsNullOrEmpty(q))
                query = query.WhereContains(q, nameof(CourseOfferingEntity.NameJson),
                    nameof(CourseOfferingEntity.PrimaryCode));

            // Apply teachingLanguage filter - TeachingLanguagesJson is a plain JSON-array-of-strings
            // column, so a substring Contains (portable LIKE across both providers) is the only query
            // this codebase has infrastructure for; see design.md Decision 4.
            if (!string.IsNullOrEmpty(teachingLanguage))
                query = query.Where(c =>
                    c.TeachingLanguagesJson != null && c.TeachingLanguagesJson.Contains(teachingLanguage));

            // Apply state filter (the spec's offeringState parameter - "state" is its actual wire name)
            if (!string.IsNullOrEmpty(state)) query = query.Where(c => c.State != null && c.State == state);

            // Apply modeOfDelivery filter - same JSON-array-substring approach as teachingLanguage
            if (!string.IsNullOrEmpty(modeOfDelivery))
                query = query.Where(c =>
                    c.ModesOfDeliveryJson != null && c.ModesOfDeliveryJson.Contains(modeOfDelivery));

            // Apply consumer filtering
            query = ConsumerKeyFilter.Apply(query, consumer);

            // Apply resultExpected filter
            if (resultExpected.HasValue) query = query.Where(c => c.ResultExpected == resultExpected.Value);

            // Spec: since/until filter by the corresponding academic session's own dates, not the
            // offering's own.
            string effectiveSince = GetEffectiveSince(since);
            query = query.Where(c =>
                c.AcademicSession != null &&
                !string.IsNullOrEmpty(c.AcademicSession.StartDateTime) &&
                c.AcademicSession.StartDateTime.CompareTo(effectiveSince) >= 0);

            if (!string.IsNullOrEmpty(until))
            {
                string? normalizedUntil = NormalizeDateTimeFilterValue(until);
                query = query.Where(c =>
                    c.AcademicSession != null &&
                    !string.IsNullOrEmpty(c.AcademicSession.EndDateTime) &&
                    c.AcademicSession.EndDateTime.CompareTo(normalizedUntil) <= 0);
            }

            // Apply pagination
            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);

            int totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            // Apply default ordering: by start date, then by primary code
            query = query.OrderBy(c => c.StartDateTime).ThenBy(c => c.PrimaryCode);

            List<CourseOfferingEntity> items = await query
                .Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            List<CourseOffering> apiModels = [.. items.Select(e => e.ToApiModel(consumer))];

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

    // ========================================================================
    // Nested endpoints for courses/{courseId}/learning-components
    // ========================================================================

    /// <summary>
    ///     Retrieves all learning components for a specific course.
    ///     GET /api/courses/{courseId}/learning-components
    /// </summary>
    /// <remarks>
    ///     Get an ordered list of all course learning components.
    /// </remarks>
    /// <param name="courseId">The course ID.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="q">Search term.</param>
    /// <param name="teachingLanguage">Filter by teaching language.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{courseId}/learning-components")]
    [ProducesResponseType(typeof(PagedResult<LearningComponent>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLearningComponentsByCourseId(
        string courseId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? consumer,
        [FromQuery] string? fields,
        [FromQuery] string? q,
        [FromQuery] string? teachingLanguage,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // First, verify the course exists
            CourseEntity? courseEntity = await _dbContext.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CourseId == courseId, cancellationToken)
                .ConfigureAwait(false);

            if (courseEntity == null)
                return Problem(
                    detail: $"Course with ID '{courseId}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            // Get learning components for this course
            IQueryable<LearningComponentEntity> query = _dbContext.LearningComponents
                .AsNoTracking()
                .Include(l => l.Course)
                .Include(l => l.Organisation)
                .Include(l => l.Parent)
                .Include(l => l.Children)
                .Include(l => l.LearningOutcomes)
                .Where(l => l.CourseEntityId != null && l.CourseEntityId == courseEntity.Id);

            // Apply the spec's filter_query DSL
            query = ApplyFilterQuery(query);

            // Apply search if requested
            if (!string.IsNullOrEmpty(q))
                query = query.WhereContains(q, nameof(LearningComponentEntity.NameJson),
                    nameof(LearningComponentEntity.PrimaryCode));

            // Apply teachingLanguage filter - TeachingLanguagesJson is a plain JSON-array-of-strings
            // column, so a substring Contains (portable LIKE across both providers) is the only query
            // this codebase has infrastructure for; see design.md Decision 4.
            if (!string.IsNullOrEmpty(teachingLanguage))
                query = query.Where(l =>
                    l.TeachingLanguagesJson != null && l.TeachingLanguagesJson.Contains(teachingLanguage));

            // Apply consumer filtering
            query = ConsumerKeyFilter.Apply(query, consumer);

            // Apply pagination
            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);

            int totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            // Apply default ordering: by primary code, then by name
            query = query.OrderBy(l => l.PrimaryCode).ThenBy(l => l.NameJson);

            List<LearningComponentEntity> items = await query
                .Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            List<LearningComponent> apiModels = [.. items.Select(item => item.ToApiModel(consumer))];

            // Create paged result
            PagedResult<LearningComponent> result = new(
                apiModels, totalCount, validatedPage, validatedPageSize);

            return PagedResponseWithFieldSelection(result, fields);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    ///     Retrieves all learning component offerings for a specific course.
    ///     GET /api/courses/{courseId}/learning-component-offerings
    /// </summary>
    /// <param name="courseId">The course ID.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="q">Search term.</param>
    /// <param name="teachingLanguage">Filter by teaching language.</param>
    /// <param name="state">Filter by offering state.</param>
    /// <param name="modeOfDelivery">Filter by mode of delivery.</param>
    /// <param name="resultExpected">Filter by resultExpected.</param>
    /// <param name="since">Filter by minimum start moment (RFC3339 full-date).</param>
    /// <param name="until">Filter by maximum end moment (RFC3339 full-date).</param>
    /// <param name="expand">Comma-separated list of relations to expand, e.g. <c>learningComponent,rooms</c>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{courseId}/learning-component-offerings")]
    [ProducesResponseType(typeof(PagedResult<LearningComponentOffering>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLearningComponentOfferingsByCourseId(
        string courseId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? consumer,
        [FromQuery] string? fields,
        [FromQuery] string? q,
        [FromQuery] string? teachingLanguage,
        [FromQuery] string? state,
        [FromQuery] string? modeOfDelivery,
        [FromQuery] bool? resultExpected,
        [FromQuery] string? since,
        [FromQuery] string? until,
        [FromQuery] string? expand,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // First, verify the course exists
            CourseEntity? courseEntity = await _dbContext.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CourseId == courseId, cancellationToken)
                .ConfigureAwait(false);

            if (courseEntity == null)
                return Problem(
                    detail: $"Course with ID '{courseId}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            // Get learning component offerings for this course through LearningComponents
            IQueryable<LearningComponentOfferingEntity> query = _dbContext.LearningComponentOfferings
                .AsNoTracking()
                .Include(lc => lc.LearningComponent)
                .Include(lc => lc.Organisation)
                .Include(lc => lc.AcademicSession)
                .Include(lc => lc.Rooms)
                .Include(lc => lc.CourseOfferings)
                .Include(lc => lc.Groups)
                .Where(lc => _dbContext.LearningComponents
                    .AsNoTracking()
                    .Where(lcEntity => lcEntity.CourseEntityId != null && lcEntity.CourseEntityId == courseEntity.Id)
                    .Select(lcEntity => lcEntity.Id)
                    .Contains(lc.LearningComponentEntityId ?? Guid.Empty));

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

            // Apply modeOfDelivery filter - same JSON-array-substring approach as teachingLanguage
            if (!string.IsNullOrEmpty(modeOfDelivery))
                query = query.Where(lc =>
                    lc.ModesOfDeliveryJson != null && lc.ModesOfDeliveryJson.Contains(modeOfDelivery));

            // Apply resultExpected filter
            if (resultExpected.HasValue) query = query.Where(lc => lc.ResultExpected == resultExpected.Value);

            // Spec: since/until filter by the corresponding academic session's own dates, not the
            // offering's own.
            string effectiveSince = GetEffectiveSince(since);
            query = query.Where(lc =>
                lc.AcademicSession != null &&
                !string.IsNullOrEmpty(lc.AcademicSession.StartDateTime) &&
                lc.AcademicSession.StartDateTime.CompareTo(effectiveSince) >= 0);

            if (!string.IsNullOrEmpty(until))
            {
                string? normalizedUntil = NormalizeDateTimeFilterValue(until);
                query = query.Where(lc =>
                    lc.AcademicSession != null &&
                    !string.IsNullOrEmpty(lc.AcademicSession.EndDateTime) &&
                    lc.AcademicSession.EndDateTime.CompareTo(normalizedUntil) <= 0);
            }

            // Apply consumer filtering
            query = ConsumerKeyFilter.Apply(query, consumer);

            // Apply pagination
            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);

            int totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            // Apply default ordering
            query = query.OrderBy(lc => lc.StartDateTime).ThenBy(lc => lc.LearningComponentOfferingIdValue);

            List<LearningComponentOfferingEntity> items = await query
                .Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            List<LearningComponentOffering> apiModels = [.. items.Select(item => item.ToApiModel(consumer))];

            // Apply expand, per item, after pagination - reuses
            // LearningComponentOfferingsController.ApplyExpandAsync's case bodies (see design.md
            // Decision 1: no shared hook exists for nested-endpoint expansion in this codebase, every
            // other nested endpoint that expands duplicates its canonical controller's logic the same way).
            if (!string.IsNullOrEmpty(expand))
                foreach (LearningComponentOffering apiModel in apiModels)
                    await ApplyLearningComponentOfferingExpandAsync(apiModel, expand, cancellationToken)
                        .ConfigureAwait(false);

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
    ///     Applies <c>expand</c> to a single <see cref="LearningComponentOffering" /> returned from
    ///     <see cref="GetLearningComponentOfferingsByCourseId" />. Duplicates
    ///     <c>LearningComponentOfferingsController.ApplyExpandAsync</c>'s case bodies verbatim - see
    ///     design.md Decision 1 (in the <c>fix-nested-endpoint-spec-param-gaps</c> change) for why this
    ///     isn't a shared/virtual hook instead.
    /// </summary>
    private async Task ApplyLearningComponentOfferingExpandAsync(
        LearningComponentOffering apiModel,
        string expand,
        CancellationToken cancellationToken)
    {
        string[] expands = expand.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        foreach (string expandOption in expands)
            switch (expandOption.ToLower())
            {
                case "learning_component":
                    if (!string.IsNullOrEmpty(apiModel.LearningComponentId?.Value))
                    {
                        LearningComponentEntity? learningComponentEntity = await _dbContext.LearningComponents
                            .AsNoTracking()
                            .Include(lc => lc.Course)
                            .Include(lc => lc.Organisation)
                            .Include(lc => lc.Parent)
                            .Include(lc => lc.Children)
                            .Include(lc => lc.LearningOutcomes)
                            .FirstOrDefaultAsync(lc => lc.ComponentId == apiModel.LearningComponentId.Value,
                                cancellationToken)
                            .ConfigureAwait(false);

                        if (learningComponentEntity != null)
                            apiModel.LearningComponent = learningComponentEntity.ToApiModel(null);
                    }

                    break;

                case "organisation":
                    if (!string.IsNullOrEmpty(apiModel.OrganisationId?.Value))
                    {
                        OrganisationEntity? organisationEntity = await _dbContext.Organisations
                            .AsNoTracking()
                            .IncludeHierarchy()
                            .FirstOrDefaultAsync(o => o.OrganisationId == apiModel.OrganisationId.Value,
                                cancellationToken)
                            .ConfigureAwait(false);

                        if (organisationEntity != null) apiModel.Organisation = organisationEntity.ToApiModel(null);
                    }

                    break;

                case "academic_session":
                    if (!string.IsNullOrEmpty(apiModel.AcademicSessionId?.Value))
                    {
                        AcademicSessionEntity? academicSessionEntity = await _dbContext.AcademicSessions
                            .AsNoTracking()
                            .IncludeHierarchy()
                            .FirstOrDefaultAsync(a => a.AcademicSessionId == apiModel.AcademicSessionId.Value,
                                cancellationToken)
                            .ConfigureAwait(false);

                        if (academicSessionEntity != null)
                            apiModel.AcademicSession = academicSessionEntity.ToApiModel(null);
                    }

                    break;

                case "rooms":
                    if (apiModel.RoomIds != null && apiModel.RoomIds.Length > 0)
                    {
                        List<RoomEntity> roomEntities = await _dbContext.Rooms
                            .AsNoTracking()
                            .Include(r => r.Building)
                            .Where(r => apiModel.RoomIds.Select(id => id.Value).Contains(r.RoomId))
                            .ToListAsync(cancellationToken)
                            .ConfigureAwait(false);

                        if (roomEntities.Count > 0)
                            apiModel.Rooms = [.. roomEntities.Select(r => r.ToApiModel(null))];
                    }

                    break;

                // Not spec-declared for this operation's own `expand` enum (only `learning_component`/
                // `organisation`/`rooms` are) - kept as extra, harmless leniency per this codebase's
                // established "leave working undeclared behaviour in place" precedent. No underscored
                // spec spelling exists to align this one to.
                case "courseofferings":
                    if (apiModel.CourseOfferingIds != null && apiModel.CourseOfferingIds.Length > 0)
                    {
                        List<CourseOfferingEntity> courseOfferingEntities = await _dbContext.CourseOfferings
                            .AsNoTracking()
                            .Include(co => co.Course)
                            .Include(co => co.Organisation)
                            .Include(co => co.AcademicSession)
                            .Include(co => co.Groups)
                            .Include(co => co.ProgrammeOfferings)
                            .Where(co =>
                                apiModel.CourseOfferingIds.Select(id => id.Value).Contains(co.CourseOfferingId))
                            .ToListAsync(cancellationToken)
                            .ConfigureAwait(false);

                        if (courseOfferingEntities.Count > 0)
                            apiModel.CourseOfferings = [.. courseOfferingEntities.Select(e => e.ToApiModel(null))];
                    }

                    break;
            }
    }

    // ========================================================================
    // Helper methods for nested endpoints
    // ========================================================================

    /// <summary>
    ///     Retrieves all test components for a specific course.
    ///     GET /api/courses/{courseId}/test-components
    /// </summary>
    /// <param name="courseId">The course ID.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="q">Search term.</param>
    /// <param name="teachingLanguage">Filter by teaching language.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{courseId}/test-components")]
    [ProducesResponseType(typeof(PagedResult<TestComponent>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTestComponentsByCourseId(
        string courseId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? consumer,
        [FromQuery] string? fields,
        [FromQuery] string? q,
        [FromQuery] string? teachingLanguage,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // First, verify the course exists
            CourseEntity? courseEntity = await _dbContext.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CourseId == courseId, cancellationToken)
                .ConfigureAwait(false);

            if (courseEntity == null)
                return Problem(
                    detail: $"Course with ID '{courseId}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            // Get test components for this course
            IQueryable<TestComponentEntity> query = _dbContext.TestComponents
                .AsNoTracking()
                .Include(t => t.Course)
                .Include(t => t.Organisation)
                .Include(t => t.Parent)
                .Include(t => t.Children)
                .Include(t => t.LearningOutcomes)
                .Where(t => t.CourseEntityId != null && t.CourseEntityId == courseEntity.Id);

            // Apply the spec's filter_query DSL
            query = ApplyFilterQuery(query);

            // Apply search if requested
            if (!string.IsNullOrEmpty(q))
                query = query.WhereContains(q, nameof(TestComponentEntity.NameJson),
                    nameof(TestComponentEntity.PrimaryCode));

            // Apply teachingLanguage filter - TeachingLanguagesJson is a plain JSON-array-of-strings
            // column, so a substring Contains (portable LIKE across both providers) is the only query
            // this codebase has infrastructure for; see design.md Decision 4.
            if (!string.IsNullOrEmpty(teachingLanguage))
                query = query.Where(t =>
                    t.TeachingLanguagesJson != null && t.TeachingLanguagesJson.Contains(teachingLanguage));

            // Apply consumer filtering
            query = ConsumerKeyFilter.Apply(query, consumer);

            // Apply pagination
            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);

            int totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            // Apply default ordering: by primary code, then by name
            query = query.OrderBy(t => t.PrimaryCode).ThenBy(t => t.NameJson);

            List<TestComponentEntity> items = await query
                .Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            List<TestComponent> apiModels = [.. items.Select(item => item.ToApiModel(consumer))];

            // Create paged result
            PagedResult<TestComponent> result = new(
                apiModels, totalCount, validatedPage, validatedPageSize);

            return PagedResponseWithFieldSelection(result, fields);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    ///     Retrieves all test component offerings for a specific course.
    ///     GET /api/courses/{courseId}/test-component-offerings
    /// </summary>
    /// <param name="courseId">The course ID.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="q">Search term.</param>
    /// <param name="teachingLanguage">Filter by teaching language.</param>
    /// <param name="state">Filter by offering state.</param>
    /// <param name="modeOfDelivery">Filter by mode of delivery.</param>
    /// <param name="resultExpected">Filter by resultExpected.</param>
    /// <param name="since">Filter by minimum start moment (RFC3339 full-date).</param>
    /// <param name="until">Filter by maximum end moment (RFC3339 full-date).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{courseId}/test-component-offerings")]
    [ProducesResponseType(typeof(PagedResult<TestComponentOffering>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTestComponentOfferingsByCourseId(
        string courseId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? consumer,
        [FromQuery] string? fields,
        [FromQuery] string? q,
        [FromQuery] string? teachingLanguage,
        [FromQuery] string? state,
        [FromQuery] string? modeOfDelivery,
        [FromQuery] bool? resultExpected,
        [FromQuery] string? since,
        [FromQuery] string? until,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // First, verify the course exists
            CourseEntity? courseEntity = await _dbContext.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CourseId == courseId, cancellationToken)
                .ConfigureAwait(false);

            if (courseEntity == null)
                return Problem(
                    detail: $"Course with ID '{courseId}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            // Get test component offerings for this course
            IQueryable<TestComponentOfferingEntity> query = _dbContext.TestComponentOfferings
                .AsNoTracking()
                .Include(t => t.TestComponent)
                .Include(t => t.Organisation)
                .Include(t => t.AcademicSession)
                .Include(t => t.Rooms)
                .Include(t => t.CourseOfferings)
                .Include(t => t.Groups)
                .Where(t => t.CourseEntityId != null && t.CourseEntityId == courseEntity.Id);

            // Apply the spec's filter_query DSL
            query = ApplyFilterQuery(query);

            // Apply search if requested
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

            // Apply modeOfDelivery filter - same JSON-array-substring approach as teachingLanguage
            if (!string.IsNullOrEmpty(modeOfDelivery))
                query = query.Where(t =>
                    t.ModesOfDeliveryJson != null && t.ModesOfDeliveryJson.Contains(modeOfDelivery));

            // Apply resultExpected filter
            if (resultExpected.HasValue) query = query.Where(t => t.ResultExpected == resultExpected.Value);

            // Spec: since/until filter by the corresponding academic session's own dates, not the
            // offering's own.
            string effectiveSince = GetEffectiveSince(since);
            query = query.Where(t =>
                t.AcademicSession != null &&
                !string.IsNullOrEmpty(t.AcademicSession.StartDateTime) &&
                t.AcademicSession.StartDateTime.CompareTo(effectiveSince) >= 0);

            if (!string.IsNullOrEmpty(until))
            {
                string? normalizedUntil = NormalizeDateTimeFilterValue(until);
                query = query.Where(t =>
                    t.AcademicSession != null &&
                    !string.IsNullOrEmpty(t.AcademicSession.EndDateTime) &&
                    t.AcademicSession.EndDateTime.CompareTo(normalizedUntil) <= 0);
            }

            // Apply consumer filtering
            query = ConsumerKeyFilter.Apply(query, consumer);

            // Apply pagination
            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);

            int totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            // Apply default ordering
            query = query.OrderBy(t => t.StartDateTime).ThenBy(t => t.TestComponentOfferingIdValue);

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

    // ========================================================================
    // Helper methods for nested endpoints
    // ========================================================================

    /// <summary>
    ///     Applies expand functionality to Course model.
    /// </summary>
    /// <param name="apiModel">The Course API model to expand.</param>
    /// <param name="expand">Comma-separated list of properties to expand.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The expanded Course API model.</returns>
    protected override async Task<Course> ApplyExpandAsync(
        Course apiModel,
        string expand,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(expand))
            return apiModel;

        string[] expandOptions =
            expand.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        // Routed through Entities (not a raw _dbContext.Courses query) so ApplyIncludes' eager
        // loads apply here too - without this, every Expand*Async method below saw an entity whose
        // Organisation/CourseCoordinatorEntities/CourseInstructorEntities/ProgrammeEntities/
        // LearningOutcomes navigations were always unloaded, silently no-op'ing every expand
        // option regardless of what was requested.
        CourseEntity? entity = await Entities
            .FirstOrDefaultAsync(c => c.CourseId == apiModel.CourseIdValue, cancellationToken)
            .ConfigureAwait(false);

        if (entity == null)
            return apiModel;

        foreach (string option in expandOptions)
            switch (option.ToLower())
            {
                case "programmes":
                    await ExpandProgrammesAsync(apiModel, entity).ConfigureAwait(false);
                    break;
                case "coordinators":
                    await ExpandCoordinatorsAsync(apiModel, entity).ConfigureAwait(false);
                    break;
                case "instructors":
                    await ExpandInstructorsAsync(apiModel, entity).ConfigureAwait(false);
                    break;
                case "organisation":
                    await ExpandOrganisationAsync(apiModel, entity).ConfigureAwait(false);
                    break;
                case "learning_outcomes":
                    await ExpandLearningOutcomesAsync(apiModel, entity).ConfigureAwait(false);
                    break;
                    // Ignore unknown expand options
            }

        return apiModel;
    }

    /// <summary>
    ///     Expands the programmes relationship for a course.
    /// </summary>
    private async Task ExpandProgrammesAsync(Course apiModel, CourseEntity entity)
    {
        // programmeIds is already set unconditionally in MapToApiModel - only the full objects are
        // expand-conditional.
        if (entity.ProgrammeEntities.Count > 0)
            apiModel.Programmes = [.. entity.ProgrammeEntities.Select(p => p.ToApiModel(null))];
    }

    /// <summary>
    ///     Expands the coordinators relationship for a course.
    /// </summary>
    private async Task ExpandCoordinatorsAsync(Course apiModel, CourseEntity entity)
    {
        // coordinatorIds is already set unconditionally in MapToApiModel - only the full objects
        // are expand-conditional.
        if (entity.CourseCoordinatorEntities.Count > 0)
            apiModel.Coordinators = [.. entity.CourseCoordinatorEntities.Select(p => p.ToApiModel(null))];
    }

    /// <summary>
    ///     Expands the instructors relationship for a course.
    /// </summary>
    private async Task ExpandInstructorsAsync(Course apiModel, CourseEntity entity)
    {
        // instructorIds is already set unconditionally in MapToApiModel - only the full objects
        // are expand-conditional.
        if (entity.CourseInstructorEntities.Count > 0)
            apiModel.Instructors = [.. entity.CourseInstructorEntities.Select(p => p.ToApiModel(null))];
    }

    /// <summary>
    ///     Expands the organisation relationship for a course.
    /// </summary>
    private async Task ExpandOrganisationAsync(Course apiModel, CourseEntity entity)
    {
        // organisationId is already set unconditionally in MapToApiModel - only the full object is
        // expand-conditional.
        if (entity.Organisation != null) apiModel.Organisation = entity.Organisation.ToApiModel(null);
    }

    /// <summary>
    ///     Expands the learning outcomes relationship for a course.
    /// </summary>
    private async Task ExpandLearningOutcomesAsync(Course apiModel, CourseEntity entity)
    {
        // learningOutcomeIds is already set unconditionally in MapToApiModel - only the full
        // objects are expand-conditional.
        if (entity.LearningOutcomes.Count > 0)
            apiModel.LearningOutcomes = [.. entity.LearningOutcomes.Select(lo => lo.ToApiModel(null))];
    }

    /// <summary>
    ///     Populates <see cref="Course.TimelineOverrides" /> when the client requests
    ///     <c>returnTimelineOverrides=true</c>. The caller's <c>expand=</c> composes into every override
    ///     entry the same way it applies to the parent course.
    /// </summary>
    protected override async Task<Course> ApplyTimelineOverridesAsync(
        Course apiModel,
        CourseEntity entity,
        string? expand,
        string? consumer,
        CancellationToken cancellationToken = default)
    {
        HashSet<string> expandOptions = string.IsNullOrEmpty(expand)
            ? []
            : [.. expand.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)];

        List<TimelineOverrideCourseEntity> overrides = await _dbContext.CourseTimelineOverrides
            .AsNoTracking()
            .Where(o => o.CourseEntityId == entity.Id)
            .Include(o => o.Organisation)
            .Include(o => o.Coordinators)
            .Include(o => o.Instructors)
            .Include(o => o.Programmes)
            .Include(o => o.LearningOutcomes)
            .Include(o => o.OtherCodes)
            .OrderBy(o => o.ValidFrom)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        if (overrides.Count > 0)
            apiModel.TimelineOverrides = [.. overrides.Select(o => o.ToApiModel(consumer, expandOptions))];

        return apiModel;
    }
}
