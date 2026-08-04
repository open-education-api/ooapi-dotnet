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
///     Controller for programme endpoints.
/// </summary>
/// <remarks>
///     Initializes a new instance of the ProgrammesController class.
/// </remarks>
/// <param name="dbContext">The database context.</param>
/// <param name="logger">The logger.</param>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
[Tags("Programmes")]
public class ProgrammesController(OEAPIDbContext dbContext, ILogger<ProgrammesController> logger) : GenericEntityController<ProgrammeEntity, Programme>(dbContext, logger, "Programme", "programmes")
{
    private readonly OEAPIDbContext _dbContext = dbContext;

    /// <summary>Retrieves a list of all programmes.</summary>
    /// <param name="primaryCode">Filter by primary code.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response for Scalar/OpenAPI doc generation -
    // TApiModel can't be referenced in an attribute on the generic base class (CS0416). Behaviour is
    // unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(PagedResult<Programme>), StatusCodes.Status200OK)]
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

    /// <summary>Retrieves a single programme by ID.</summary>
    /// <param name="programmeId">The programme ID.</param>
    /// <param name="expand">Comma-separated list of relations to expand, e.g. <c>organisation,parent</c>.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response - see the GetAll override above.
    // Behaviour is unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(Programme), StatusCodes.Status200OK)]
    [HttpGet("{programmeId}")]
    public override Task<IActionResult> GetById(
        string programmeId,
        string? expand,
        string? fields,
        string? consumer,
        CancellationToken cancellationToken = default)
    {
        return base.GetById(programmeId, expand, fields, consumer, cancellationToken);
    }

    /// <summary>
    ///     Applies the <c>qualification</c>, <c>q</c>, <c>teachingLanguage</c>, <c>programmeType</c>,
    ///     <c>levelOfQualification</c>, and <c>fieldsOfStudy</c> filters to a <c>GetAll</c> query.
    /// </summary>
    protected override IQueryable<ProgrammeEntity> ApplyEntityFilters(IQueryable<ProgrammeEntity> query)
    {
        string? qualification = GetQueryValue("qualificationAwarded");
        if (!string.IsNullOrEmpty(qualification))
            query = query.Where(p =>
                p.QualificationAwarded != null && p.QualificationAwarded.ToLower() == qualification.ToLower());

        string? q = GetQueryValue("q");
        if (!string.IsNullOrEmpty(q))
            query = query.WhereContains(q, nameof(ProgrammeEntity.NameJson), nameof(ProgrammeEntity.PrimaryCode));

        // TeachingLanguagesJson is a plain JSON-array-of-strings column, so a substring Contains
        // (portable LIKE across both providers) is the only query this codebase has infrastructure
        // for; see design.md Decision 4.
        string? teachingLanguage = GetQueryValue("teachingLanguage");
        if (!string.IsNullOrEmpty(teachingLanguage))
            query = query.Where(p =>
                p.TeachingLanguagesJson != null && p.TeachingLanguagesJson.Contains(teachingLanguage));

        string? programmeType = GetQueryValue("programmeType");
        if (!string.IsNullOrEmpty(programmeType))
            query = query.Where(p => p.ProgrammeType != null && p.ProgrammeType.ToLower() == programmeType.ToLower());

        string? levelOfQualification = GetQueryValue("levelOfQualification");
        if (!string.IsNullOrEmpty(levelOfQualification))
            query = query.Where(p =>
                p.LevelOfQualification != null && p.LevelOfQualification.ToLower() == levelOfQualification.ToLower());

        string? fieldsOfStudy = GetQueryValue("fieldsOfStudy");
        if (!string.IsNullOrEmpty(fieldsOfStudy))
            query = query.Where(p => p.FieldsOfStudy != null && p.FieldsOfStudy.ToLower() == fieldsOfStudy.ToLower());

        return query;
    }

    /// <summary>
    ///     Eagerly loads Parent/Children/Organisation/Coordinators/Instructors so
    ///     <c>MapToApiModel</c> can read them directly - see
    ///     <see cref="GenericEntityController{TEntity,TApiModel}.ApplyIncludes" /> for why. Previously
    ///     missing entirely, so <c>parentId</c>/<c>childIds</c>/<c>organisationId</c>/
    ///     <c>coordinatorIds</c>/<c>instructorIds</c> were always <c>null</c> on every response
    ///     regardless of the underlying data. Parent/Children also need their own Organisation
    ///     navigation loaded - <c>ProgrammeMappingExtensions.ToApiModel</c> reads
    ///     <c>entity.Organisation?.OrganisationId</c> for the <c>organisationId</c> field, so without
    ///     this a mapped Parent/Child's <c>organisationId</c> silently came out as an empty string
    ///     rather than the real value.
    /// </summary>
    protected override IQueryable<ProgrammeEntity> ApplyIncludes(IQueryable<ProgrammeEntity> query)
    {
        return query
            .Include(p => p.Parent).ThenInclude(parent => parent!.Organisation)
            .Include(p => p.Children).ThenInclude(c => c.Organisation)
            .Include(p => p.Organisation)
            .Include(p => p.Coordinators)
            .Include(p => p.Instructors)
            .Include(p => p.LearningOutcomes);
    }

    // Programmes has no spec-defined write operations (no POST/PUT/PATCH/DELETE for
    // /programmes in oeapi.yaml), so - per the P2 decision to strip generic CRUD down to
    // spec-only writes - this controller no longer provides Create/Update/Delete at all.

    // ========================================================================
    // Required abstract method implementations
    // ========================================================================

    /// <summary>
    ///     Maps a ProgrammeEntity to a Programme API model.
    /// </summary>
    /// <param name="entity">The entity to map.</param>
    /// <param name="consumer">The requested consumer, for consumer-specific data.</param>
    /// <returns>The API model.</returns>
    protected override Programme MapToApiModel(ProgrammeEntity entity, string? consumer)
    {
        return entity.ToApiModel(consumer);
    }

    /// <summary>
    ///     Maps a Programme API model to a ProgrammeEntity.
    /// </summary>
    /// <param name="model">The API model to map.</param>
    /// <returns>The entity.</returns>
    protected override ProgrammeEntity MapToEntity(Programme model)
    {
        return model.ToEntity();
    }

    /// <summary>
    ///     Updates a ProgrammeEntity from a Programme API model.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="model">The API model with new data.</param>
    protected override void UpdateEntityFromApiModel(ProgrammeEntity entity, Programme model)
    {
        entity.UpdateFrom(model);
    }

    /// <summary>
    ///     Gets the unique identifier for a programme entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>The identifier as string.</returns>
    protected override string GetEntityId(ProgrammeEntity entity)
    {
        return entity.ProgrammeId;
    }

    // ========================================================================
    // Override methods for custom behaviour
    // ========================================================================

    /// <summary>
    ///     Applies default ordering for programmes: by primary code, then by name.
    /// </summary>
    /// <param name="query">The query to order.</param>
    /// <returns>The ordered query.</returns>
    protected override IQueryable<ProgrammeEntity> ApplyDefaultOrdering(IQueryable<ProgrammeEntity> query)
    {
        return query.OrderBy(p => p.PrimaryCode).ThenBy(p => p.NameJson);
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

    // ========================================================================
    // Nested endpoints for programmes/{programmeId}/programme-offerings
    // ========================================================================

    /// <summary>
    ///     Retrieves all programme offerings for a specific programme.
    ///     GET /api/programmes/{programmeId}/programme-offerings
    /// </summary>
    /// <param name="programmeId">The programme ID.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="q">Search term.</param>
    /// <param name="since">Filter by minimum start moment (RFC3339 full-date).</param>
    /// <param name="until">Filter by maximum end moment (RFC3339 full-date).</param>
    /// <param name="resultExpected">Filter by resultExpected.</param>
    /// <param name="teachingLanguage">Filter by teaching language.</param>
    /// <param name="state">Filter by offering state.</param>
    /// <param name="modeOfStudy">Filter by mode of study.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{programmeId}/programme-offerings")]
    [ProducesResponseType(typeof(PagedResult<ProgrammeOffering>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProgrammeOfferingsByProgrammeId(
        string programmeId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? consumer,
        [FromQuery(Name = "fields")] string? fields,
        [FromQuery] string? q,
        [FromQuery] string? since,
        [FromQuery] string? until,
        [FromQuery] bool? resultExpected,
        [FromQuery] string? teachingLanguage,
        [FromQuery] string? state,
        [FromQuery] string? modeOfStudy,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // First, verify the programme exists
            ProgrammeEntity? programmeEntity = await _dbContext.Programmes
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProgrammeId == programmeId, cancellationToken)
                .ConfigureAwait(false);

            if (programmeEntity == null)
                return Problem(
                    detail: $"Programme with ID '{programmeId}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            // Get programme offerings for this programme
            IQueryable<ProgrammeOfferingEntity> query = _dbContext.ProgrammeOfferings
                .AsNoTracking()
                .Include(p => p.Programme)
                .Include(p => p.Organisation)
                .Include(p => p.AcademicSession)
                .Include(p => p.Groups)
                .Where(p => p.ProgrammeEntityId != null && p.ProgrammeEntityId == programmeEntity.Id);

            // Apply generic filtering
            query = ApplyFilterQuery(query);

            // Apply search if requested
            if (!string.IsNullOrEmpty(q))
                query = query.WhereContains(q, nameof(ProgrammeOfferingEntity.NameJson),
                    nameof(ProgrammeOfferingEntity.PrimaryCode));

            // Apply consumer filtering
            query = ConsumerKeyFilter.Apply(query, consumer);

            // Spec: since/until filter by the corresponding academic session's own dates, not the
            // offering's own.
            string effectiveSince = GetEffectiveSince(since);
            query = query.Where(p =>
                p.AcademicSession != null &&
                !string.IsNullOrEmpty(p.AcademicSession.StartDateTime) &&
                p.AcademicSession.StartDateTime.CompareTo(effectiveSince) >= 0);

            if (!string.IsNullOrEmpty(until))
            {
                string? normalizedUntil = NormalizeDateTimeFilterValue(until);
                query = query.Where(p =>
                    p.AcademicSession != null &&
                    !string.IsNullOrEmpty(p.AcademicSession.EndDateTime) &&
                    p.AcademicSession.EndDateTime.CompareTo(normalizedUntil) <= 0);
            }

            // Apply resultExpected filter
            if (resultExpected.HasValue) query = query.Where(p => p.ResultExpected == resultExpected.Value);

            // Apply teachingLanguage filter - TeachingLanguagesJson is a plain JSON-array-of-strings
            // column, so a substring Contains (portable LIKE across both providers) is the only query
            // this codebase has infrastructure for; see design.md Decision 4.
            if (!string.IsNullOrEmpty(teachingLanguage))
                query = query.Where(p =>
                    p.TeachingLanguagesJson != null && p.TeachingLanguagesJson.Contains(teachingLanguage));

            // Apply state filter (the spec's offeringState parameter - "state" is its actual wire name)
            if (!string.IsNullOrEmpty(state)) query = query.Where(p => p.State != null && p.State == state);

            // Apply modeOfStudy filter - ProgrammeOfferingEntity has no ModeOfStudy column of its own
            // (adding one is tier 1b entity/migration work, out of scope here), so this filters via the
            // already-Included Programme navigation's ModeOfStudy instead - the offering's own mode of
            // study is inherited from its parent programme.
            if (!string.IsNullOrEmpty(modeOfStudy))
                query = query.Where(p => p.Programme != null && p.Programme.ModeOfStudy == modeOfStudy);

            // Apply pagination
            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);

            int totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            // Apply default ordering: by start date, then by primary code
            query = query.OrderBy(p => p.StartDateTime).ThenBy(p => p.ProgrammeOfferingIdValue);

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
    ///     Retrieves all courses that are part of a specific programme.
    ///     GET /programmes/{programmeId}/courses
    /// </summary>
    /// <param name="programmeId">The programme ID.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="q">Search term.</param>
    /// <param name="teachingLanguage">Filter by teaching language.</param>
    /// <param name="level">Filter by level.</param>
    /// <param name="modeOfDelivery">Filter by mode of delivery.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{programmeId}/courses")]
    [ProducesResponseType(typeof(PagedResult<Course>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCoursesByProgrammeId(
        string programmeId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? consumer,
        [FromQuery] string? fields,
        [FromQuery] string? q,
        [FromQuery] string? teachingLanguage,
        [FromQuery] string? level,
        [FromQuery] string? modeOfDelivery,
        CancellationToken cancellationToken = default)
    {
        try
        {
            ProgrammeEntity? programmeEntity = await _dbContext.Programmes
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProgrammeId == programmeId, cancellationToken)
                .ConfigureAwait(false);

            if (programmeEntity == null)
                return Problem(
                    detail: $"Programme with ID '{programmeId}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            IQueryable<CourseEntity> query = _dbContext.Courses
                .AsNoTracking()
                .Include(c => c.Organisation)
                .Include(c => c.CourseCoordinatorEntities)
                .Include(c => c.CourseInstructorEntities)
                .Include(c => c.ProgrammeEntities)
                .Include(c => c.LearningOutcomes)
                .Where(c => c.ProgrammeEntities.Any(p => p.Id == programmeEntity.Id));

            query = ApplyFilterQuery(query);

            if (!string.IsNullOrEmpty(q))
                query = query.WhereContains(q, nameof(CourseEntity.NameJson), nameof(CourseEntity.PrimaryCode));

            // Apply teachingLanguage filter - TeachingLanguagesJson is a plain JSON-array-of-strings
            // column, so a substring Contains (portable LIKE across both providers) is the only query
            // this codebase has infrastructure for; see design.md Decision 4.
            if (!string.IsNullOrEmpty(teachingLanguage))
                query = query.Where(c =>
                    c.TeachingLanguagesJson != null && c.TeachingLanguagesJson.Contains(teachingLanguage));

            // Apply level filter (plain column, direct equality)
            if (!string.IsNullOrEmpty(level)) query = query.Where(c => c.Level != null && c.Level == level);

            // Apply modeOfDelivery filter - same JSON-array-substring approach as teachingLanguage
            if (!string.IsNullOrEmpty(modeOfDelivery))
                query = query.Where(c =>
                    c.ModesOfDeliveryJson != null && c.ModesOfDeliveryJson.Contains(modeOfDelivery));

            query = ConsumerKeyFilter.Apply(query, consumer);

            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);
            int totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            query = query.OrderBy(c => c.PrimaryCode).ThenBy(c => c.NameJson);
            List<CourseEntity> items = await query
                .Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            List<Course> apiModels = [.. items.Select(item => item.ToApiModel(consumer))];
            PagedResult<Course> result = new(apiModels, totalCount, validatedPage, validatedPageSize);
            return PagedResponseWithFieldSelection(result, fields);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    ///     Retrieves all sub-programmes of a specific programme. GET /programmes/{programmeId}/programmes
    /// </summary>
    /// <param name="programmeId">The programme ID.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="q">Search term.</param>
    /// <param name="teachingLanguage">Filter by teaching language.</param>
    /// <param name="programmeType">Filter by programme type.</param>
    /// <param name="qualificationAwarded">Filter by qualification awarded.</param>
    /// <param name="levelOfQualification">Filter by level of qualification.</param>
    /// <param name="fieldsOfStudy">Filter by fields of study.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{programmeId}/programmes")]
    [ProducesResponseType(typeof(PagedResult<Programme>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSubProgrammesByProgrammeId(
        string programmeId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? consumer,
        [FromQuery] string? fields,
        [FromQuery] string? q,
        [FromQuery] string? teachingLanguage,
        [FromQuery] string? programmeType,
        [FromQuery] string? qualificationAwarded,
        [FromQuery] string? levelOfQualification,
        [FromQuery] string? fieldsOfStudy,
        CancellationToken cancellationToken = default)
    {
        try
        {
            ProgrammeEntity? programmeEntity = await _dbContext.Programmes
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProgrammeId == programmeId, cancellationToken)
                .ConfigureAwait(false);

            if (programmeEntity == null)
                return Problem(
                    detail: $"Programme with ID '{programmeId}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            // Same Includes as the primary ApplyIncludes, applied explicitly here since this query
            // doesn't go through Entities (it's scoped to this programme's children, not the whole set).
            IQueryable<ProgrammeEntity> query = ApplyIncludes(_dbContext.Programmes.AsNoTracking())
                .Where(p => p.ParentEntityId == programmeEntity.Id);

            query = ApplyFilterQuery(query);

            if (!string.IsNullOrEmpty(q))
                query = query.WhereContains(q, nameof(ProgrammeEntity.NameJson),
                    nameof(ProgrammeEntity.PrimaryCode));

            // Apply teachingLanguage filter - TeachingLanguagesJson is a plain JSON-array-of-strings
            // column, so a substring Contains (portable LIKE across both providers) is the only query
            // this codebase has infrastructure for; see design.md Decision 4.
            if (!string.IsNullOrEmpty(teachingLanguage))
                query = query.Where(p =>
                    p.TeachingLanguagesJson != null && p.TeachingLanguagesJson.Contains(teachingLanguage));

            // Apply programmeType filter (plain column, direct equality)
            if (!string.IsNullOrEmpty(programmeType))
                query = query.Where(p => p.ProgrammeType != null && p.ProgrammeType == programmeType);

            // Apply qualificationAwarded filter (plain column, direct equality)
            if (!string.IsNullOrEmpty(qualificationAwarded))
                query = query.Where(p => p.QualificationAwarded != null && p.QualificationAwarded == qualificationAwarded);

            // Apply levelOfQualification filter (plain column, direct equality)
            if (!string.IsNullOrEmpty(levelOfQualification))
                query = query.Where(p => p.LevelOfQualification != null && p.LevelOfQualification == levelOfQualification);

            // Apply fieldsOfStudy filter (plain column, direct equality)
            if (!string.IsNullOrEmpty(fieldsOfStudy))
                query = query.Where(p => p.FieldsOfStudy != null && p.FieldsOfStudy == fieldsOfStudy);

            query = ConsumerKeyFilter.Apply(query, consumer);

            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);
            int totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            query = query.OrderBy(p => p.PrimaryCode).ThenBy(p => p.NameJson);
            List<ProgrammeEntity> items = await query
                .Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            List<Programme> apiModels = [.. items.Select(item => MapToApiModel(item, consumer))];
            PagedResult<Programme> result = new(apiModels, totalCount, validatedPage, validatedPageSize);
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
    ///     Applies expand to a Programme API model (includes related entities).
    /// </summary>
    /// <param name="apiModel">The Programme API model to expand.</param>
    /// <param name="expand">The expand parameter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the expanded Programme API model.</returns>
    protected override async Task<Programme> ApplyExpandAsync(
        Programme apiModel,
        string expand,
        CancellationToken cancellationToken = default)
    {
        string[] expands = expand.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        // Routed through Entities (not a raw _dbContext.Programmes query) so ApplyIncludes' eager
        // loads apply here too - without this, every case below saw a programmeEntity whose
        // Children/Coordinators/Instructors navigations were always unloaded, silently no-op'ing
        // those expand options regardless of what was requested.
        ProgrammeEntity? programmeEntity = await Entities
            .FirstOrDefaultAsync(p => p.ProgrammeId == apiModel.ProgrammeIdValue, cancellationToken)
            .ConfigureAwait(false);

        foreach (string expandOption in expands)
            switch (expandOption.ToLower())
            {
                case "organisation":
                    if (programmeEntity?.OrganisationEntityId.HasValue == true && apiModel.Organisation == null)
                    {
                        Guid organisationEntityId = programmeEntity.OrganisationEntityId.Value;
                        OrganisationEntity? organisationEntity = await _dbContext.Organisations
                            .AsNoTracking()
                            .IncludeHierarchy()
                            .FirstOrDefaultAsync(o => o.Id == organisationEntityId,
                                cancellationToken)
                            .ConfigureAwait(false);

                        if (organisationEntity != null) apiModel.Organisation = organisationEntity.ToApiModel(null);
                    }

                    break;

                case "parent":
                    if (programmeEntity?.Parent != null && apiModel.Parent == null)
                        apiModel.Parent = MapToApiModel(programmeEntity.Parent, null);

                    break;

                case "children":
                    if (programmeEntity != null && apiModel.Children == null)
                    {
                        List<ProgrammeEntity> childEntities = [.. programmeEntity.Children];
                        if (childEntities.Count > 0)
                            apiModel.Children = [.. childEntities.Select(e => MapToApiModel(e, null))];
                    }

                    break;

                case "coordinators":
                    if (programmeEntity != null && apiModel.Coordinators == null)
                    {
                        List<PersonEntity> coordinatorEntities = [.. programmeEntity.Coordinators];
                        if (coordinatorEntities.Count > 0)
                            apiModel.Coordinators = [.. coordinatorEntities.Select(e => e.ToApiModel(null))];
                    }

                    break;

                case "instructors":
                    if (programmeEntity != null && apiModel.Instructors == null)
                    {
                        List<PersonEntity> instructorEntities = [.. programmeEntity.Instructors];
                        if (instructorEntities.Count > 0)
                            apiModel.Instructors = [.. instructorEntities.Select(e => e.ToApiModel(null))];
                    }

                    break;

                case "learning_outcomes":
                    if (programmeEntity != null && apiModel.LearningOutcomes == null)
                    {
                        List<LearningOutcomeEntity> learningOutcomeEntities = [.. programmeEntity.LearningOutcomes];
                        if (learningOutcomeEntities.Count > 0)
                            apiModel.LearningOutcomes = [.. learningOutcomeEntities.Select(e => e.ToApiModel(null))];
                    }

                    break;
            }

        return apiModel;
    }

    /// <summary>
    ///     Populates <see cref="Programme.TimelineOverrides" /> when the client requests
    ///     <c>returnTimelineOverrides=true</c>. The caller's <c>expand=</c> composes into every override
    ///     entry the same way it applies to the parent programme.
    /// </summary>
    protected override async Task<Programme> ApplyTimelineOverridesAsync(
        Programme apiModel,
        ProgrammeEntity entity,
        string? expand,
        string? consumer,
        CancellationToken cancellationToken = default)
    {
        HashSet<string> expandOptions = string.IsNullOrEmpty(expand)
            ? []
            : [.. expand.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)];

        List<TimelineOverrideProgrammeEntity> overrides = await _dbContext.ProgrammeTimelineOverrides
            .AsNoTracking()
            .Where(o => o.ProgrammeEntityId == entity.Id)
            .Include(o => o.Organisation)
            .Include(o => o.Parent)
            .Include(o => o.Children)
            .Include(o => o.Coordinators)
            .Include(o => o.Instructors)
            .Include(o => o.OtherCodes)
            .OrderBy(o => o.ValidFrom)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        if (overrides.Count > 0)
            apiModel.TimelineOverrides = [.. overrides.Select(o => o.ToApiModel(consumer, expandOptions))];

        return apiModel;
    }
}
