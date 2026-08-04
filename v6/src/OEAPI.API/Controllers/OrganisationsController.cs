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
///     Controller for organisation endpoints.
/// </summary>
/// <remarks>
///     Initializes a new instance of the OrganisationsController class.
/// </remarks>
/// <param name="dbContext">The database context.</param>
/// <param name="logger">The logger.</param>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
[Tags("Organisations")]
public class OrganisationsController(OEAPIDbContext dbContext, ILogger<OrganisationsController> logger) : GenericEntityController<OrganisationEntity, Organisation>(dbContext, logger, "Organisation", "organisations")
{
    private readonly OEAPIDbContext _dbContext = dbContext;

    /// <summary>Retrieves a list of all organisations.</summary>
    /// <param name="primaryCode">Filter by primary code.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response for Scalar/OpenAPI doc generation -
    // TApiModel can't be referenced in an attribute on the generic base class (CS0416). Behaviour is
    // unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(PagedResult<Organisation>), StatusCodes.Status200OK)]
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

    /// <summary>Retrieves a single organisation by ID.</summary>
    /// <param name="organisationId">The organisation ID.</param>
    /// <param name="expand">Comma-separated list of relations to expand, e.g. <c>parent,children</c>.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response - see the GetAll override above.
    // Behaviour is unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(Organisation), StatusCodes.Status200OK)]
    [HttpGet("{organisationId}")]
    public override Task<IActionResult> GetById(
        string organisationId,
        string? expand,
        string? fields,
        string? consumer,
        CancellationToken cancellationToken = default)
    {
        return base.GetById(organisationId, expand, fields, consumer, cancellationToken);
    }

    /// <summary>
    ///     Applies the <c>organisationType</c> filter to a <c>GetAll</c> query.
    /// </summary>
    protected override IQueryable<OrganisationEntity> ApplyEntityFilters(IQueryable<OrganisationEntity> query)
    {
        string? organisationTypeValue = GetQueryValue("organisationType");
        if (!string.IsNullOrEmpty(organisationTypeValue))
            query = query.Where(o =>
                o.OrganisationType != null && o.OrganisationType.ToLower() == organisationTypeValue.ToLower());

        // Spec: q (search) - substring match against name and primary code.
        string? searchValue = GetQueryValue("q");
        if (!string.IsNullOrEmpty(searchValue))
            query = query.WhereContains(searchValue, nameof(OrganisationEntity.NameJson),
                nameof(OrganisationEntity.PrimaryCode));

        return query;
    }

    /// <summary>
    ///     Eagerly loads Parent/Root/Children so <c>MapToApiModel</c> can read them directly - see
    ///     <see cref="GenericEntityController{TEntity,TApiModel}.ApplyIncludes" /> for why.
    /// </summary>
    protected override IQueryable<OrganisationEntity> ApplyIncludes(IQueryable<OrganisationEntity> query)
    {
        return query.IncludeHierarchy();
    }

    // ========================================================================
    // Required abstract method implementations
    // ========================================================================

    /// <summary>
    ///     Maps an OrganisationEntity to an Organisation API model.
    /// </summary>
    /// <param name="entity">The entity to map.</param>
    /// <param name="consumer">The requested consumer, for consumer-specific data.</param>
    /// <returns>The API model.</returns>
    protected override Organisation MapToApiModel(OrganisationEntity entity, string? consumer)
    {
        // Parent/Root/Children are eager-loaded via ApplyIncludes, so ParentId/RootId/ChildIds are
        // safe to read here - see OrganisationMappingExtensions.ToApiModel for the full mapping,
        // shared with every other controller that embeds a full Organisation via expand=organisation.
        return entity.ToApiModel(consumer);
    }

    /// <summary>
    ///     Maps an Organisation API model to an OrganisationEntity.
    /// </summary>
    /// <param name="model">The API model to map.</param>
    /// <returns>The entity.</returns>
    protected override OrganisationEntity MapToEntity(Organisation model)
    {
        return model.ToEntity();
    }

    /// <summary>
    ///     Updates an OrganisationEntity from an Organisation API model.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="model">The API model with new data.</param>
    protected override void UpdateEntityFromApiModel(OrganisationEntity entity, Organisation model)
    {
        entity.UpdateFrom(model);
    }

    /// <summary>
    ///     Gets the unique identifier for an organisation entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>The identifier as string.</returns>
    protected override string GetEntityId(OrganisationEntity entity)
    {
        return entity.OrganisationId;
    }

    /// <summary>
    ///     Replaces an organisation, or creates one at the given id if it doesn't exist yet.
    ///     PUT /organisations/{organisationId}. Upsert semantics per spec: <c>200</c> if replacing an
    ///     existing organisation, <c>201</c> if creating a new one at the given id.
    /// </summary>
    /// <param name="organisationId">The organisation ID.</param>
    /// <param name="model">The full organisation representation.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPut("{organisationId}")]
    // No typeof() here: this action returns Ok()/StatusCode(201) with no body, matching the
    // spec's own PUT /organisations/{organisationId} responses (200/201 declare no content).
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PutOrganisation(
        string organisationId,
        [FromBody] Organisation model,
        CancellationToken cancellationToken = default)
    {
        try
        {
            OrganisationEntity? entity = await _dbContext.Organisations
                .FirstOrDefaultAsync(o => o.OrganisationId == organisationId, cancellationToken)
                .ConfigureAwait(false);

            bool isNew;
            if (entity == null)
            {
                isNew = true;
                entity = MapToEntity(model);
                entity.OrganisationId = organisationId;
                _dbContext.Organisations.Add(entity);
            }
            else
            {
                isNew = false;
                UpdateEntityFromApiModel(entity, model);
            }

            if (!string.IsNullOrEmpty(model.ParentId?.Value))
            {
                OrganisationEntity? parentEntity = await _dbContext.Organisations
                    .AsNoTracking()
                    .FirstOrDefaultAsync(o => o.OrganisationId == model.ParentId.Value, cancellationToken)
                    .ConfigureAwait(false);
                if (parentEntity != null) entity.ParentEntityId = parentEntity.Id;
            }

            if (!string.IsNullOrEmpty(model.RootId?.Value))
            {
                OrganisationEntity? rootEntity = await _dbContext.Organisations
                    .AsNoTracking()
                    .FirstOrDefaultAsync(o => o.OrganisationId == model.RootId.Value, cancellationToken)
                    .ConfigureAwait(false);
                if (rootEntity != null) entity.RootEntityId = rootEntity.Id;
            }

            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return isNew ? StatusCode(StatusCodes.Status201Created) : Ok();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    // ========================================================================
    // Custom methods for OrganisationsController
    // ========================================================================

    /// <summary>
    ///     Applies default ordering for organisations: by primary code, then by name.
    /// </summary>
    /// <param name="query">The query to order.</param>
    /// <returns>The ordered query.</returns>
    protected override IQueryable<OrganisationEntity> ApplyDefaultOrdering(IQueryable<OrganisationEntity> query)
    {
        return query.OrderBy(o => o.PrimaryCode).ThenBy(o => o.NameJson);
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
    // Nested endpoints for organisations/{organisationId}/*
    // ========================================================================

    /// <summary>
    ///     Retrieves all courses for a specific organisation.
    ///     GET /api/organisations/{organisationId}/courses
    /// </summary>
    /// <remarks>
    ///     Get an ordered list of all courses for a given organisation, ordered by name.
    /// </remarks>
    /// <param name="organisationId">The organisation ID.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="q">Search term.</param>
    /// <param name="teachingLanguage">Filter by teaching language.</param>
    /// <param name="level">Filter by level.</param>
    /// <param name="modeOfDelivery">Filter by mode of delivery.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{organisationId}/courses")]
    [ProducesResponseType(typeof(PagedResult<Course>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status405MethodNotAllowed)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCoursesByOrganisationId(
        string organisationId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? consumer,
        [FromQuery(Name = "fields")] string? fields,
        [FromQuery] string? q,
        [FromQuery] string? teachingLanguage,
        [FromQuery] string? level,
        [FromQuery] string? modeOfDelivery,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // First, verify the organisation exists
            OrganisationEntity? organisationEntity = await _dbContext.Organisations
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.OrganisationId == organisationId, cancellationToken)
                .ConfigureAwait(false);

            if (organisationEntity == null)
                return Problem(
                    detail: "Organisation with ID not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            // Get courses for this organisation
            IQueryable<CourseEntity> query = _dbContext.Courses
                .AsNoTracking()
                .Include(c => c.Organisation)
                .Include(c => c.CourseCoordinatorEntities)
                .Include(c => c.CourseInstructorEntities)
                .Include(c => c.ProgrammeEntities)
                .Include(c => c.LearningOutcomes)
                .Where(c => c.OrganisationEntityId == organisationEntity.Id);

            // Apply generic filtering
            query = ApplyFilterQuery(query);

            // Apply search if requested
            if (!string.IsNullOrEmpty(q))
                query = query.WhereContains(q, nameof(CourseEntity.NameJson), nameof(CourseEntity.PrimaryCode));

            // Apply teachingLanguage filter - TeachingLanguagesJson is a plain JSON-array-of-strings
            // column, so a substring Contains (portable LIKE across both providers) is the only query
            // this codebase has infrastructure for.
            if (!string.IsNullOrEmpty(teachingLanguage))
                query = query.Where(c =>
                    c.TeachingLanguagesJson != null && c.TeachingLanguagesJson.Contains(teachingLanguage));

            // Apply level filter
            if (!string.IsNullOrEmpty(level)) query = query.Where(c => c.Level != null && c.Level == level);

            // Apply modeOfDelivery filter - same JSON-array-substring approach as teachingLanguage
            if (!string.IsNullOrEmpty(modeOfDelivery))
                query = query.Where(c =>
                    c.ModesOfDeliveryJson != null && c.ModesOfDeliveryJson.Contains(modeOfDelivery));

            // Apply consumer filtering
            query = ConsumerKeyFilter.Apply(query, consumer);

            // Apply pagination
            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);

            int totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            // Apply default ordering
            query = query.OrderBy(c => c.PrimaryCode).ThenBy(c => c.NameJson);

            List<CourseEntity> items = await query
                .Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            List<Course> apiModels = [.. items.Select(item => item.ToApiModel(consumer))];

            // Create paged result
            PagedResult<Course> result = new(
                apiModels, totalCount, validatedPage, validatedPageSize);

            return PagedResponseWithFieldSelection(result, fields);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    // Additional nested endpoints for organisations

    /// <summary>
    ///     Retrieves all programme offerings for a specific organisation.
    ///     GET /api/organisations/{organisationId}/programme-offerings
    /// </summary>
    /// <param name="organisationId">The organisation ID.</param>
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
    [HttpGet("{organisationId}/programme-offerings")]
    [ProducesResponseType(typeof(PagedResult<ProgrammeOffering>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProgrammeOfferingsByOrganisationId(
        string organisationId,
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
            OrganisationEntity? organisationEntity = await _dbContext.Organisations
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.OrganisationId == organisationId, cancellationToken)
                .ConfigureAwait(false);

            if (organisationEntity == null)
                return Problem(
                    detail: "Organisation with ID not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            IQueryable<ProgrammeOfferingEntity> query = _dbContext.ProgrammeOfferings
                .AsNoTracking()
                .Include(po => po.Programme)
                .Include(po => po.Organisation)
                .Include(po => po.AcademicSession)
                .Include(po => po.Groups)
                .Where(po => po.OrganisationEntityId == organisationEntity.Id);

            query = ApplyFilterQuery(query);

            // Apply search if requested
            if (!string.IsNullOrEmpty(q))
                query = query.WhereContains(q, nameof(ProgrammeOfferingEntity.NameJson),
                    nameof(ProgrammeOfferingEntity.PrimaryCode));

            // Apply teachingLanguage filter - TeachingLanguagesJson is a plain JSON-array-of-strings
            // column, so a substring Contains (portable LIKE across both providers) is the only query
            // this codebase has infrastructure for.
            if (!string.IsNullOrEmpty(teachingLanguage))
                query = query.Where(po =>
                    po.TeachingLanguagesJson != null && po.TeachingLanguagesJson.Contains(teachingLanguage));

            // Apply state filter (the spec's offeringState parameter - "state" is its actual wire name)
            if (!string.IsNullOrEmpty(state)) query = query.Where(po => po.State != null && po.State == state);

            // Apply resultExpected filter
            if (resultExpected.HasValue) query = query.Where(po => po.ResultExpected == resultExpected.Value);

            // Spec: since/until filter by the corresponding academic session's own dates, not the
            // offering's own.
            string effectiveSince = GetEffectiveSince(since);
            query = query.Where(po =>
                po.AcademicSession != null &&
                !string.IsNullOrEmpty(po.AcademicSession.StartDateTime) &&
                po.AcademicSession.StartDateTime.CompareTo(effectiveSince) >= 0);

            if (!string.IsNullOrEmpty(until))
            {
                string? normalizedUntil = NormalizeDateTimeFilterValue(until);
                query = query.Where(po =>
                    po.AcademicSession != null &&
                    !string.IsNullOrEmpty(po.AcademicSession.EndDateTime) &&
                    po.AcademicSession.EndDateTime.CompareTo(normalizedUntil) <= 0);
            }

            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);
            int totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            query = query.OrderBy(po => po.ProgrammeOfferingIdValue);
            List<ProgrammeOfferingEntity> items = await query.Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize).ToListAsync(cancellationToken).ConfigureAwait(false);

            List<ProgrammeOffering> apiModels = [.. items.Select(item => item.ToApiModel(consumer))];

            PagedResult<ProgrammeOffering> result = new(apiModels, totalCount, validatedPage, validatedPageSize);
            return PagedResponseWithFieldSelection(result, fields);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    ///     Retrieves all programmes for a specific organisation.
    ///     GET /api/organisations/{organisationId}/programmes
    /// </summary>
    /// <param name="organisationId">The organisation ID.</param>
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
    [HttpGet("{organisationId}/programmes")]
    [ProducesResponseType(typeof(PagedResult<Programme>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProgrammesByOrganisationId(
        string organisationId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? consumer,
        [FromQuery(Name = "fields")] string? fields,
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
            OrganisationEntity? organisationEntity = await _dbContext.Organisations
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.OrganisationId == organisationId, cancellationToken)
                .ConfigureAwait(false);

            if (organisationEntity == null)
                return Problem(
                    detail: "Organisation with ID not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            IQueryable<ProgrammeEntity> query = _dbContext.Programmes
                .AsNoTracking()
                .Include(p => p.Parent)
                .Include(p => p.Children)
                .Include(p => p.Organisation)
                .Include(p => p.Coordinators)
                .Include(p => p.Instructors)
                .Where(p => p.OrganisationEntityId == organisationEntity.Id);

            query = ApplyFilterQuery(query);

            // Apply search if requested
            if (!string.IsNullOrEmpty(q))
                query = query.WhereContains(q, nameof(ProgrammeEntity.NameJson),
                    nameof(ProgrammeEntity.PrimaryCode));

            // Apply teachingLanguage filter - TeachingLanguagesJson is a plain JSON-array-of-strings
            // column, so a substring Contains (portable LIKE across both providers) is the only query
            // this codebase has infrastructure for.
            if (!string.IsNullOrEmpty(teachingLanguage))
                query = query.Where(p =>
                    p.TeachingLanguagesJson != null && p.TeachingLanguagesJson.Contains(teachingLanguage));

            // Apply programmeType filter
            if (!string.IsNullOrEmpty(programmeType))
                query = query.Where(p => p.ProgrammeType != null && p.ProgrammeType == programmeType);

            // Apply qualificationAwarded filter
            if (!string.IsNullOrEmpty(qualificationAwarded))
                query = query.Where(p => p.QualificationAwarded != null && p.QualificationAwarded == qualificationAwarded);

            // Apply levelOfQualification filter
            if (!string.IsNullOrEmpty(levelOfQualification))
                query = query.Where(p => p.LevelOfQualification != null && p.LevelOfQualification == levelOfQualification);

            // Apply fieldsOfStudy filter
            if (!string.IsNullOrEmpty(fieldsOfStudy))
                query = query.Where(p => p.FieldsOfStudy != null && p.FieldsOfStudy == fieldsOfStudy);

            query = ConsumerKeyFilter.Apply(query, consumer);

            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);
            int totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            query = query.OrderBy(p => p.PrimaryCode);
            List<ProgrammeEntity> items = await query.Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize).ToListAsync(cancellationToken).ConfigureAwait(false);

            List<Programme> apiModels = [.. items.Select(item => item.ToApiModel(consumer))];

            PagedResult<Programme> result = new(apiModels, totalCount, validatedPage, validatedPageSize);
            return PagedResponseWithFieldSelection(result, fields);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    ///     Retrieves all groups for a specific organisation.
    ///     GET /api/organisations/{organisationId}/groups
    /// </summary>
    /// <param name="organisationId">The organisation ID.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="q">Search term.</param>
    /// <param name="groupType">Filter by group type.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{organisationId}/groups")]
    [ProducesResponseType(typeof(PagedResult<Group>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetGroupsByOrganisationId(
        string organisationId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? consumer,
        [FromQuery(Name = "fields")] string? fields,
        [FromQuery] string? q,
        [FromQuery] string? groupType,
        CancellationToken cancellationToken = default)
    {
        try
        {
            OrganisationEntity? organisationEntity = await _dbContext.Organisations
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.OrganisationId == organisationId, cancellationToken)
                .ConfigureAwait(false);

            if (organisationEntity == null)
                return Problem(
                    detail: "Organisation with ID not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            IQueryable<GroupEntity> query = _dbContext.Groups
                .AsNoTracking()
                .Include(g => g.Organisation)
                .Include(g => g.AcademicSession)
                .Include(g => g.CourseOfferings)
                .Include(g => g.ProgrammeOfferings)
                .Include(g => g.LearningComponentOfferings)
                .Include(g => g.TestComponentOfferings)
                .Where(g => g.OrganisationEntityId == organisationEntity.Id);

            query = ApplyFilterQuery(query);

            // Apply search if requested
            if (!string.IsNullOrEmpty(q))
                query = query.WhereContains(q, nameof(GroupEntity.NameJson), nameof(GroupEntity.PrimaryCode));

            // Apply groupType filter
            if (!string.IsNullOrEmpty(groupType))
                query = query.Where(g => g.GroupType != null && g.GroupType == groupType);

            query = ConsumerKeyFilter.Apply(query, consumer);

            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);
            int totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            query = query.OrderBy(g => g.PrimaryCode);
            List<GroupEntity> items = await query.Skip(ComputePageOffset(validatedPage, validatedPageSize)).Take(validatedPageSize)
                .ToListAsync(cancellationToken).ConfigureAwait(false);

            List<Group> apiModels = [.. items.Select(item => item.ToApiModel(consumer))];

            PagedResult<Group> result = new(apiModels, totalCount, validatedPage, validatedPageSize);
            return PagedResponseWithFieldSelection(result, fields);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    ///     Retrieves all course offerings for a specific organisation.
    ///     GET /api/organisations/{organisationId}/course-offerings
    /// </summary>
    /// <param name="organisationId">The organisation ID.</param>
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
    [HttpGet("{organisationId}/course-offerings")]
    [ProducesResponseType(typeof(PagedResult<CourseOffering>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCourseOfferingsByOrganisationId(
        string organisationId,
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
            OrganisationEntity? organisationEntity = await _dbContext.Organisations
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.OrganisationId == organisationId, cancellationToken)
                .ConfigureAwait(false);

            if (organisationEntity == null)
                return Problem(
                    detail: "Organisation with ID not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            IQueryable<CourseOfferingEntity> query = _dbContext.CourseOfferings
                .AsNoTracking()
                .Include(co => co.Course)
                .Include(co => co.Organisation)
                .Include(co => co.AcademicSession)
                .Include(co => co.Groups)
                .Include(co => co.ProgrammeOfferings)
                .Where(co => co.OrganisationEntityId == organisationEntity.Id);

            query = ApplyFilterQuery(query);

            // Apply search if requested
            if (!string.IsNullOrEmpty(q))
                query = query.WhereContains(q, nameof(CourseOfferingEntity.NameJson),
                    nameof(CourseOfferingEntity.PrimaryCode));

            // Apply teachingLanguage filter - TeachingLanguagesJson is a plain JSON-array-of-strings
            // column, so a substring Contains (portable LIKE across both providers) is the only query
            // this codebase has infrastructure for.
            if (!string.IsNullOrEmpty(teachingLanguage))
                query = query.Where(co =>
                    co.TeachingLanguagesJson != null && co.TeachingLanguagesJson.Contains(teachingLanguage));

            // Apply state filter (the spec's offeringState parameter - "state" is its actual wire name)
            if (!string.IsNullOrEmpty(state)) query = query.Where(co => co.State != null && co.State == state);

            // Apply resultExpected filter
            if (resultExpected.HasValue) query = query.Where(co => co.ResultExpected == resultExpected.Value);

            // Spec: since/until filter by the corresponding academic session's own dates, not the
            // offering's own.
            string effectiveSince = GetEffectiveSince(since);
            query = query.Where(co =>
                co.AcademicSession != null &&
                !string.IsNullOrEmpty(co.AcademicSession.StartDateTime) &&
                co.AcademicSession.StartDateTime.CompareTo(effectiveSince) >= 0);

            if (!string.IsNullOrEmpty(until))
            {
                string? normalizedUntil = NormalizeDateTimeFilterValue(until);
                query = query.Where(co =>
                    co.AcademicSession != null &&
                    !string.IsNullOrEmpty(co.AcademicSession.EndDateTime) &&
                    co.AcademicSession.EndDateTime.CompareTo(normalizedUntil) <= 0);
            }

            query = ConsumerKeyFilter.Apply(query, consumer);

            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);
            int totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            query = query.OrderBy(co => co.PrimaryCode);
            List<CourseOfferingEntity> items = await query.Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize).ToListAsync(cancellationToken).ConfigureAwait(false);

            List<CourseOffering> apiModels = [.. items.Select(item => item.ToApiModel(consumer))];

            PagedResult<CourseOffering> result = new(apiModels, totalCount, validatedPage, validatedPageSize);
            return PagedResponseWithFieldSelection(result, fields);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    ///     Retrieves all test components for a specific organisation.
    ///     GET /api/organisations/{organisationId}/test-components
    /// </summary>
    /// <param name="organisationId">The organisation ID.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="q">Search term.</param>
    /// <param name="teachingLanguage">Filter by teaching language.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{organisationId}/test-components")]
    [ProducesResponseType(typeof(PagedResult<TestComponent>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTestComponentsByOrganisationId(
        string organisationId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? consumer,
        [FromQuery(Name = "fields")] string? fields,
        [FromQuery] string? q,
        [FromQuery] string? teachingLanguage,
        CancellationToken cancellationToken = default)
    {
        try
        {
            OrganisationEntity? organisationEntity = await _dbContext.Organisations
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.OrganisationId == organisationId, cancellationToken)
                .ConfigureAwait(false);

            if (organisationEntity == null)
                return Problem(
                    detail: "Organisation with ID not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            IQueryable<TestComponentEntity> query = _dbContext.TestComponents
                .AsNoTracking()
                .Include(tc => tc.Course)
                .Include(tc => tc.Organisation)
                .Include(tc => tc.Parent)
                .Include(tc => tc.Children)
                .Include(tc => tc.LearningOutcomes)
                .Where(tc => tc.OrganisationEntityId == organisationEntity.Id);

            query = ApplyFilterQuery(query);

            // Apply search if requested
            if (!string.IsNullOrEmpty(q))
                query = query.WhereContains(q, nameof(TestComponentEntity.NameJson),
                    nameof(TestComponentEntity.PrimaryCode));

            // Apply teachingLanguage filter - TeachingLanguagesJson is a plain JSON-array-of-strings
            // column, so a substring Contains (portable LIKE across both providers) is the only query
            // this codebase has infrastructure for.
            if (!string.IsNullOrEmpty(teachingLanguage))
                query = query.Where(tc =>
                    tc.TeachingLanguagesJson != null && tc.TeachingLanguagesJson.Contains(teachingLanguage));

            query = ConsumerKeyFilter.Apply(query, consumer);

            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);
            int totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            query = query.OrderBy(tc => tc.PrimaryCode);
            List<TestComponentEntity> items = await query.Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize).ToListAsync(cancellationToken).ConfigureAwait(false);

            List<TestComponent> apiModels = [.. items.Select(item => item.ToApiModel(consumer))];

            PagedResult<TestComponent> result = new(apiModels, totalCount, validatedPage, validatedPageSize);
            return PagedResponseWithFieldSelection(result, fields);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    ///     Retrieves all test component offerings for a specific organisation.
    ///     GET /api/organisations/{organisationId}/test-component-offerings
    /// </summary>
    /// <param name="organisationId">The organisation ID.</param>
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
    [HttpGet("{organisationId}/test-component-offerings")]
    [ProducesResponseType(typeof(PagedResult<TestComponentOffering>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTestComponentOfferingsByOrganisationId(
        string organisationId,
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
            OrganisationEntity? organisationEntity = await _dbContext.Organisations
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.OrganisationId == organisationId, cancellationToken)
                .ConfigureAwait(false);

            if (organisationEntity == null)
                return Problem(
                    detail: "Organisation with ID not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            // Get test component offerings for this organisation (via TestComponent.OrganisationEntityId)
            IQueryable<TestComponentOfferingEntity> query = _dbContext.TestComponentOfferings
                .AsNoTracking()
                .Include(tco => tco.TestComponent)
                .Include(tco => tco.Organisation)
                .Include(tco => tco.AcademicSession)
                .Include(tco => tco.Rooms)
                .Include(tco => tco.CourseOfferings)
                .Include(tco => tco.Groups)
                .Where(tco => _dbContext.TestComponents
                    .AsNoTracking()
                    .Where(tc => tc.OrganisationEntityId == organisationEntity.Id)
                    .Select(tc => tc.Id)
                    .Contains(tco.TestComponentEntityId ?? Guid.Empty));

            query = ApplyFilterQuery(query);

            // Apply search if requested
            if (!string.IsNullOrEmpty(q))
                query = query.WhereContains(q, nameof(TestComponentOfferingEntity.NameJson),
                    nameof(TestComponentOfferingEntity.PrimaryCode));

            // Apply teachingLanguage filter - TeachingLanguagesJson is a plain JSON-array-of-strings
            // column, so a substring Contains (portable LIKE across both providers) is the only query
            // this codebase has infrastructure for.
            if (!string.IsNullOrEmpty(teachingLanguage))
                query = query.Where(tco =>
                    tco.TeachingLanguagesJson != null && tco.TeachingLanguagesJson.Contains(teachingLanguage));

            // Apply state filter (the spec's offeringState parameter - "state" is its actual wire name)
            if (!string.IsNullOrEmpty(state)) query = query.Where(tco => tco.State != null && tco.State == state);

            // Apply resultExpected filter
            if (resultExpected.HasValue) query = query.Where(tco => tco.ResultExpected == resultExpected.Value);

            // Spec: since/until filter by the corresponding academic session's own dates, not the
            // offering's own.
            string effectiveSince = GetEffectiveSince(since);
            query = query.Where(tco =>
                tco.AcademicSession != null &&
                !string.IsNullOrEmpty(tco.AcademicSession.StartDateTime) &&
                tco.AcademicSession.StartDateTime.CompareTo(effectiveSince) >= 0);

            if (!string.IsNullOrEmpty(until))
            {
                string? normalizedUntil = NormalizeDateTimeFilterValue(until);
                query = query.Where(tco =>
                    tco.AcademicSession != null &&
                    !string.IsNullOrEmpty(tco.AcademicSession.EndDateTime) &&
                    tco.AcademicSession.EndDateTime.CompareTo(normalizedUntil) <= 0);
            }

            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);
            int totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            query = query.OrderBy(tco => tco.TestComponentOfferingIdValue);
            List<TestComponentOfferingEntity> items = await query.Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize).ToListAsync(cancellationToken).ConfigureAwait(false);

            List<TestComponentOffering> apiModels = [.. items.Select(item => item.ToApiModel(consumer))];

            PagedResult<TestComponentOffering> result = new(apiModels, totalCount, validatedPage, validatedPageSize);
            return PagedResponseWithFieldSelection(result, fields);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    ///     Retrieves all learning components for a specific organisation.
    ///     GET /api/organisations/{organisationId}/learning-components
    /// </summary>
    /// <param name="organisationId">The organisation ID.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="q">Search term.</param>
    /// <param name="teachingLanguage">Filter by teaching language.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{organisationId}/learning-components")]
    [ProducesResponseType(typeof(PagedResult<LearningComponent>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLearningComponentsByOrganisationId(
        string organisationId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? consumer,
        [FromQuery(Name = "fields")] string? fields,
        [FromQuery] string? q,
        [FromQuery] string? teachingLanguage,
        CancellationToken cancellationToken = default)
    {
        try
        {
            OrganisationEntity? organisationEntity = await _dbContext.Organisations
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.OrganisationId == organisationId, cancellationToken)
                .ConfigureAwait(false);

            if (organisationEntity == null)
                return Problem(
                    detail: "Organisation with ID not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            // Get learning components for this organisation (via Course.OrganisationEntityId)
            IQueryable<LearningComponentEntity> query = _dbContext.LearningComponents
                .AsNoTracking()
                .Include(lc => lc.Course)
                .Include(lc => lc.Organisation)
                .Include(lc => lc.Parent)
                .Include(lc => lc.Children)
                .Include(lc => lc.LearningOutcomes)
                .Where(lc => _dbContext.Courses
                    .AsNoTracking()
                    .Where(c => c.OrganisationEntityId == organisationEntity.Id)
                    .Select(c => c.Id)
                    .Contains(lc.CourseEntityId ?? Guid.Empty));

            query = ApplyFilterQuery(query);

            // Apply search if requested
            if (!string.IsNullOrEmpty(q))
                query = query.WhereContains(q, nameof(LearningComponentEntity.NameJson),
                    nameof(LearningComponentEntity.PrimaryCode));

            // Apply teachingLanguage filter - TeachingLanguagesJson is a plain JSON-array-of-strings
            // column, so a substring Contains (portable LIKE across both providers) is the only query
            // this codebase has infrastructure for.
            if (!string.IsNullOrEmpty(teachingLanguage))
                query = query.Where(lc =>
                    lc.TeachingLanguagesJson != null && lc.TeachingLanguagesJson.Contains(teachingLanguage));

            query = ConsumerKeyFilter.Apply(query, consumer);

            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);
            int totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            query = query.OrderBy(lc => lc.PrimaryCode);
            List<LearningComponentEntity> items = await query.Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize).ToListAsync(cancellationToken).ConfigureAwait(false);

            List<LearningComponent> apiModels = [.. items.Select(item => item.ToApiModel(consumer))];

            PagedResult<LearningComponent> result = new(apiModels, totalCount, validatedPage, validatedPageSize);
            return PagedResponseWithFieldSelection(result, fields);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    ///     Retrieves all learning component offerings for a specific organisation.
    ///     GET /api/organisations/{organisationId}/learning-component-offerings
    /// </summary>
    /// <param name="organisationId">The organisation ID.</param>
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
    [HttpGet("{organisationId}/learning-component-offerings")]
    [ProducesResponseType(typeof(PagedResult<LearningComponentOffering>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLearningComponentOfferingsByOrganisationId(
        string organisationId,
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
            OrganisationEntity? organisationEntity = await _dbContext.Organisations
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.OrganisationId == organisationId, cancellationToken)
                .ConfigureAwait(false);

            if (organisationEntity == null)
                return Problem(
                    detail: "Organisation with ID not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            // Get learning component offerings for this organisation (via LearningComponent.Course.OrganisationEntityId)
            IQueryable<LearningComponentOfferingEntity> query = _dbContext.LearningComponentOfferings
                .AsNoTracking()
                .Include(lco => lco.LearningComponent)
                .Include(lco => lco.Organisation)
                .Include(lco => lco.AcademicSession)
                .Include(lco => lco.Rooms)
                .Include(lco => lco.CourseOfferings)
                .Include(lco => lco.Groups)
                .Where(lco => _dbContext.LearningComponents
                    .AsNoTracking()
                    .Where(lc => _dbContext.Courses
                        .AsNoTracking()
                        .Where(c => c.OrganisationEntityId == organisationEntity.Id)
                        .Select(c => c.Id)
                        .Contains(lc.CourseEntityId ?? Guid.Empty))
                    .Select(lc => lc.Id)
                    .Contains(lco.LearningComponentEntityId ?? Guid.Empty));

            query = ApplyFilterQuery(query);

            // Apply search if requested
            if (!string.IsNullOrEmpty(q))
                query = query.WhereContains(q, nameof(LearningComponentOfferingEntity.NameJson),
                    nameof(LearningComponentOfferingEntity.PrimaryCode));

            // Apply teachingLanguage filter - TeachingLanguagesJson is a plain JSON-array-of-strings
            // column, so a substring Contains (portable LIKE across both providers) is the only query
            // this codebase has infrastructure for.
            if (!string.IsNullOrEmpty(teachingLanguage))
                query = query.Where(lco =>
                    lco.TeachingLanguagesJson != null && lco.TeachingLanguagesJson.Contains(teachingLanguage));

            // Apply state filter (the spec's offeringState parameter - "state" is its actual wire name)
            if (!string.IsNullOrEmpty(state)) query = query.Where(lco => lco.State != null && lco.State == state);

            // Apply resultExpected filter
            if (resultExpected.HasValue) query = query.Where(lco => lco.ResultExpected == resultExpected.Value);

            // Spec: since/until filter by the corresponding academic session's own dates, not the
            // offering's own.
            string effectiveSince = GetEffectiveSince(since);
            query = query.Where(lco =>
                lco.AcademicSession != null &&
                !string.IsNullOrEmpty(lco.AcademicSession.StartDateTime) &&
                lco.AcademicSession.StartDateTime.CompareTo(effectiveSince) >= 0);

            if (!string.IsNullOrEmpty(until))
            {
                string? normalizedUntil = NormalizeDateTimeFilterValue(until);
                query = query.Where(lco =>
                    lco.AcademicSession != null &&
                    !string.IsNullOrEmpty(lco.AcademicSession.EndDateTime) &&
                    lco.AcademicSession.EndDateTime.CompareTo(normalizedUntil) <= 0);
            }

            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);
            int totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            query = query.OrderBy(lco => lco.LearningComponentOfferingIdValue);
            List<LearningComponentOfferingEntity> items = await query.Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize).ToListAsync(cancellationToken).ConfigureAwait(false);

            List<LearningComponentOffering> apiModels = [.. items.Select(item => item.ToApiModel(consumer))];

            PagedResult<LearningComponentOffering>
                result = new(apiModels, totalCount, validatedPage, validatedPageSize);
            return PagedResponseWithFieldSelection(result, fields);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    ///     Applies expand to an Organisation API model (includes related entities).
    /// </summary>
    /// <param name="apiModel">The Organisation API model to expand.</param>
    /// <param name="expand">The expand parameter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the expanded Organisation API
    ///     model.
    /// </returns>
    protected override async Task<Organisation> ApplyExpandAsync(
        Organisation apiModel,
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
                        OrganisationEntity? parentEntity = await _dbContext.Organisations
                            .AsNoTracking()
                            .FirstOrDefaultAsync(o => o.OrganisationId == apiModel.ParentId.Value, cancellationToken)
                            .ConfigureAwait(false);

                        if (parentEntity != null) apiModel.Parent = MapToApiModel(parentEntity, null);
                    }

                    break;

                case "children":
                    if (apiModel.ChildIds != null && apiModel.ChildIds.Length > 0)
                    {
                        List<OrganisationEntity> childEntities = await _dbContext.Organisations
                            .AsNoTracking()
                            .Where(o => apiModel.ChildIds.Select(c => c.Value).Contains(o.OrganisationId))
                            .ToListAsync(cancellationToken)
                            .ConfigureAwait(false);

                        apiModel.Children = [.. childEntities.Select(e => MapToApiModel(e, null))];
                    }

                    break;

                case "root":
                    if (!string.IsNullOrEmpty(apiModel.RootId?.Value))
                    {
                        OrganisationEntity? rootEntity = await _dbContext.Organisations
                            .AsNoTracking()
                            .FirstOrDefaultAsync(o => o.OrganisationId == apiModel.RootId.Value, cancellationToken)
                            .ConfigureAwait(false);

                        if (rootEntity != null) apiModel.Root = MapToApiModel(rootEntity, null);
                    }

                    break;
            }

        return apiModel;
    }
}
