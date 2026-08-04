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
///     Controller for course offering endpoints.
/// </summary>
/// <remarks>
///     Initializes a new instance of the CourseOfferingsController class. The top-level
///     <c>GET /course-offerings</c> this class's <see cref="GenericEntityController{TEntity,TApiModel}" />
///     base provides has no canonical-spec counterpart (the spec only exposes this resource nested
///     under a parent, or by single-item <c>GET</c>) - off by default (<c>404</c>) unless the
///     deployment opts in via <c>Service:ExposeNonCanonicalListEndpoints</c>; see
///     <see cref="NonCanonicalTopLevelListRoutes" /> and <c>docs/archive/DECISIONS-AND-ACTIONS.md</c>.
/// </remarks>
/// <param name="dbContext">The database context.</param>
/// <param name="logger">The logger.</param>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
[Tags("CourseOfferings")]
public class CourseOfferingsController(OEAPIDbContext dbContext, ILogger<CourseOfferingsController> logger) : GenericEntityController<CourseOfferingEntity, CourseOffering>(dbContext, logger, "CourseOffering", "course-offerings")
{
    private readonly OEAPIDbContext _dbContext = dbContext;

    /// <summary>Retrieves a list of all course offerings.</summary>
    /// <param name="primaryCode">Filter by primary code.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response for Scalar/OpenAPI doc generation -
    // TApiModel can't be referenced in an attribute on the generic base class (CS0416). Behaviour is
    // unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(PagedResult<CourseOffering>), StatusCodes.Status200OK)]
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

    /// <summary>Retrieves a single course offering by ID.</summary>
    /// <param name="courseOfferingId">The course offering ID.</param>
    /// <param name="expand">Comma-separated list of relations to expand, e.g. <c>course,academicSession</c>.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response - see the GetAll override above.
    // Behaviour is unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(CourseOffering), StatusCodes.Status200OK)]
    [HttpGet("{courseOfferingId}")]
    public override Task<IActionResult> GetById(
        string courseOfferingId,
        string? expand,
        string? fields,
        string? consumer,
        CancellationToken cancellationToken = default)
    {
        return base.GetById(courseOfferingId, expand, fields, consumer, cancellationToken);
    }

    /// <summary>
    ///     Finds a course offering by unique identifier (CourseOfferingId). Routes through
    ///     <see cref="GenericEntityController{TEntity, TApiModel}.Entities" /> (not a raw
    ///     <c>_dbContext.CourseOfferings</c> query) so <see cref="ApplyIncludes" /> applies to the
    ///     single-item lookup path too, not just <c>GetAll</c>.
    /// </summary>
    /// <param name="idValue">The course offering ID value.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the course offering entity or
    ///     null.
    /// </returns>
    protected override async Task<CourseOfferingEntity?> FindByUniqueIdAsync(string idValue,
        CancellationToken cancellationToken = default)
    {
        return await Entities
            .FirstOrDefaultAsync(co => co.CourseOfferingId == idValue, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    ///     Eagerly loads Groups/ProgrammeOfferings/Course/Organisation/AcademicSession so
    ///     <c>MapToApiModel</c> can read them directly - see
    ///     <see cref="GenericEntityController{TEntity,TApiModel}.ApplyIncludes" /> for why.
    /// </summary>
    protected override IQueryable<CourseOfferingEntity> ApplyIncludes(IQueryable<CourseOfferingEntity> query)
    {
        return query
            .Include(co => co.Groups)
            .Include(co => co.ProgrammeOfferings)
            .Include(co => co.Course)
            .Include(co => co.Organisation)
            .Include(co => co.AcademicSession);
    }

    // ========================================================================
    // Required abstract method implementations
    // ========================================================================

    /// <summary>
    ///     Maps a CourseOfferingEntity to a CourseOffering API model.
    /// </summary>
    /// <param name="entity">The entity to map.</param>
    /// <param name="consumer">The requested consumer, for consumer-specific data.</param>
    /// <returns>The API model.</returns>
    protected override CourseOffering MapToApiModel(CourseOfferingEntity entity, string? consumer)
    {
        return entity.ToApiModel(consumer);
    }

    /// <summary>
    ///     Maps a CourseOffering API model to a CourseOfferingEntity. Not currently called by any live
    ///     code path (see the note on writes in <see cref="GenericEntityController{TEntity,TApiModel}" />).
    ///     Note: resolving <c>model.CourseId</c>/<c>model.OrganisationId</c>/<c>model.AcademicSessionId</c>
    ///     (external string IDs) to the entity's internal Guid FKs requires an async DB lookup that this
    ///     synchronous method can't perform - whichever explicit write action ends up calling this needs
    ///     to resolve and set those FKs itself after calling this method.
    /// </summary>
    /// <param name="model">The API model to map.</param>
    /// <returns>The entity.</returns>
    protected override CourseOfferingEntity MapToEntity(CourseOffering model)
    {
        return model.ToEntity();
    }

    /// <summary>
    ///     Updates a CourseOfferingEntity from a CourseOffering API model. See the note on
    ///     <see cref="MapToEntity" /> regarding Course/Organisation/AcademicSession FK resolution.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="model">The API model with new data.</param>
    protected override void UpdateEntityFromApiModel(CourseOfferingEntity entity, CourseOffering model)
    {
        entity.UpdateFrom(model);
    }

    /// <summary>
    ///     Gets the unique identifier for a course offering entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>The identifier as string.</returns>
    protected override string GetEntityId(CourseOfferingEntity entity)
    {
        return entity.CourseOfferingId;
    }

    // ========================================================================
    // Custom methods for CourseOfferingsController
    // ========================================================================

    /// <summary>
    ///     Applies default ordering for course offerings: by code, then by start date.
    /// </summary>
    /// <param name="query">The query to order.</param>
    /// <returns>The ordered query.</returns>
    protected override IQueryable<CourseOfferingEntity> ApplyDefaultOrdering(IQueryable<CourseOfferingEntity> query)
    {
        return query.OrderBy(co => co.PrimaryCode).ThenBy(co => co.StartDateTime);
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
    ///     Applies expand functionality for course offerings.
    /// </summary>
    /// <param name="apiModel">The API model to expand.</param>
    /// <param name="expand">The expand parameter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the expanded API model.</returns>
    protected override async Task<CourseOffering> ApplyExpandAsync(
        CourseOffering apiModel,
        string expand,
        CancellationToken cancellationToken = default)
    {
        string[] expands = expand.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        foreach (string expandOption in expands)
            switch (expandOption.ToLower())
            {
                case "course":
                    if (apiModel.Course == null && apiModel.CourseId != null &&
                        !string.IsNullOrEmpty(apiModel.CourseId.Value))
                    {
                        CourseEntity? courseEntity = await _dbContext.Courses
                            .AsNoTracking()
                            .Include(c => c.Organisation)
                            .Include(c => c.CourseCoordinatorEntities)
                            .Include(c => c.CourseInstructorEntities)
                            .Include(c => c.ProgrammeEntities)
                            .Include(c => c.LearningOutcomes)
                            .FirstOrDefaultAsync(c => c.CourseId == apiModel.CourseId.Value, cancellationToken)
                            .ConfigureAwait(false);

                        if (courseEntity != null) apiModel.Course = courseEntity.ToApiModel(null);
                    }

                    break;

                case "academic_session":
                    if (apiModel.AcademicSession == null && apiModel.AcademicSessionId != null &&
                        !string.IsNullOrEmpty(apiModel.AcademicSessionId.Value))
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

                case "organisation":
                    if (apiModel.Organisation == null && apiModel.OrganisationId != null &&
                        !string.IsNullOrEmpty(apiModel.OrganisationId.Value))
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

                case "programme_offering":
                    if (apiModel.ProgrammeOfferings == null && apiModel.ProgrammeOfferingIds != null &&
                        apiModel.ProgrammeOfferingIds.Length > 0)
                    {
                        List<ProgrammeOfferingEntity> programmeOfferingEntities = await _dbContext.ProgrammeOfferings
                            .AsNoTracking()
                            .Include(po => po.Programme)
                            .Include(po => po.Organisation)
                            .Include(po => po.AcademicSession)
                            .Include(po => po.Groups)
                            .Where(po =>
                                apiModel.ProgrammeOfferingIds.Select(id => id.Value)
                                    .Contains(po.ProgrammeOfferingIdValue))
                            .ToListAsync(cancellationToken)
                            .ConfigureAwait(false);

                        if (programmeOfferingEntities.Count > 0)
                            apiModel.ProgrammeOfferings = [.. programmeOfferingEntities.Select(e => e.ToApiModel(null))];
                    }

                    break;
            }

        return apiModel;
    }

    // ========================================================================
    // Nested endpoints for course-offerings/{courseOfferingId}/*
    // ========================================================================

    /// <summary>
    ///     Retrieves all groups associated with a specific course offering.
    ///     GET /api/course-offerings/{courseOfferingId}/groups
    /// </summary>
    /// <param name="courseOfferingId">The course offering ID.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="groupType">Filter by group type.</param>
    /// <param name="q">Search term.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="filterQuery">Generic RSQL-style filter query.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{courseOfferingId}/groups")]
    [ProducesResponseType(typeof(PagedResult<Group>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetGroupsByCourseOfferingId(
        string courseOfferingId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? groupType,
        [FromQuery] string? q,
        [FromQuery] string? consumer,
        [FromQuery] string? filterQuery,
        [FromQuery] string? fields,
        CancellationToken cancellationToken = default)
    {
        try
        {
            CourseOfferingEntity? offeringEntity = await _dbContext.CourseOfferings
                .AsNoTracking()
                .FirstOrDefaultAsync(co => co.CourseOfferingId == courseOfferingId, cancellationToken)
                .ConfigureAwait(false);

            if (offeringEntity == null)
                return Problem(
                    detail: $"CourseOffering with ID '{courseOfferingId}' not found.",
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
                .Where(g => g.CourseOfferings.Any(co => co.Id == offeringEntity.Id));

            query = ApplyFilterQuery(query);

            if (!string.IsNullOrEmpty(groupType)) query = query.Where(g => g.GroupType == groupType);

            if (!string.IsNullOrEmpty(q))
                query = query.WhereContains(q, nameof(GroupEntity.NameJson), nameof(GroupEntity.PrimaryCode));

            query = ConsumerKeyFilter.Apply(query, consumer);

            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);
            int totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            query = query.OrderBy(g => g.PrimaryCode);
            List<GroupEntity> items = await query
                .Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

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
    ///     Retrieves all course offering associations for a specific course offering.
    ///     GET /course-offerings/{courseOfferingId}/course-offering-associations
    /// </summary>
    /// <param name="courseOfferingId">The course offering ID.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="state">Filter by association state.</param>
    /// <param name="role">Filter by the person's role in the association.</param>
    /// <param name="resultState">Filter by the association result's state.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{courseOfferingId}/course-offering-associations")]
    [ProducesResponseType(typeof(PagedResult<CourseOfferingAssociation>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCourseOfferingAssociationsByCourseOfferingId(
        string courseOfferingId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? consumer,
        [FromQuery] string? fields,
        [FromQuery] string? state,
        [FromQuery] string? role,
        [FromQuery] string? resultState,
        CancellationToken cancellationToken = default)
    {
        try
        {
            CourseOfferingEntity? offeringEntity = await _dbContext.CourseOfferings
                .AsNoTracking()
                .FirstOrDefaultAsync(co => co.CourseOfferingId == courseOfferingId, cancellationToken)
                .ConfigureAwait(false);

            if (offeringEntity == null)
                return Problem(
                    detail: $"CourseOffering with ID '{courseOfferingId}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            IQueryable<CourseOfferingAssociationEntity> query = _dbContext.CourseOfferingAssociations
                .AsNoTracking()
                .Include(a => a.CourseOffering)
                .Include(a => a.Person)
                .Include(a => a.Organisation)
                .Where(a => a.CourseOfferingEntityId == offeringEntity.Id);

            query = ApplyFilterQuery(query);

            if (!string.IsNullOrEmpty(state)) query = query.Where(a => a.State != null && a.State == state);

            if (!string.IsNullOrEmpty(role)) query = query.Where(a => a.Role != null && a.Role == role);

            query = ConsumerKeyFilter.Apply(query, consumer);

            query = query.OrderBy(a => a.PrimaryCode);

            // resultState filters on Result.State, deserialized from the opaque ResultJson blob - no
            // portable SQL-level query exists for either provider (mirrors
            // PersonsController.GetCourseOfferingAssociationsByPersonId), so every association matching
            // the SQL-level filters above is mapped first, then filtered by resultState in memory, and
            // only then paginated - preserving correct totalCount/paging instead of paginating before
            // this filter and under-filling pages.
            List<CourseOfferingAssociationEntity> candidateEntities = await query
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            List<CourseOfferingAssociation> candidates =
                [.. candidateEntities.Select(item => item.ToApiModel(consumer))];

            if (!string.IsNullOrEmpty(resultState))
                candidates = [.. candidates.Where(c => c.Result != null && c.Result.State == resultState)];

            int totalCount = candidates.Count;
            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);

            List<CourseOfferingAssociation> apiModels = [.. candidates
                .Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize)];

            PagedResult<CourseOfferingAssociation>
                result = new(apiModels, totalCount, validatedPage, validatedPageSize);
            return PagedResponseWithFieldSelection(result, fields);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    ///     Retrieves all learning component offerings for a specific course offering.
    ///     GET /course-offerings/{courseOfferingId}/learning-component-offerings
    /// </summary>
    /// <param name="courseOfferingId">The course offering ID.</param>
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
    [HttpGet("{courseOfferingId}/learning-component-offerings")]
    [ProducesResponseType(typeof(PagedResult<LearningComponentOffering>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLearningComponentOfferingsByCourseOfferingId(
        string courseOfferingId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? consumer,
        [FromQuery] string? fields,
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
            CourseOfferingEntity? offeringEntity = await _dbContext.CourseOfferings
                .AsNoTracking()
                .FirstOrDefaultAsync(co => co.CourseOfferingId == courseOfferingId, cancellationToken)
                .ConfigureAwait(false);

            if (offeringEntity == null)
                return Problem(
                    detail: $"CourseOffering with ID '{courseOfferingId}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            IQueryable<LearningComponentOfferingEntity> query = _dbContext.LearningComponentOfferings
                .AsNoTracking()
                .Include(lco => lco.LearningComponent)
                .Include(lco => lco.Organisation)
                .Include(lco => lco.AcademicSession)
                .Include(lco => lco.Rooms)
                .Include(lco => lco.CourseOfferings)
                .Include(lco => lco.Groups)
                .Where(lco => lco.CourseOfferings.Any(co => co.Id == offeringEntity.Id));

            query = ApplyFilterQuery(query);

            if (!string.IsNullOrEmpty(q))
                query = query.WhereContains(q, nameof(LearningComponentOfferingEntity.NameJson),
                    nameof(LearningComponentOfferingEntity.PrimaryCode));

            if (!string.IsNullOrEmpty(teachingLanguage))
                query = query.Where(lco =>
                    lco.TeachingLanguagesJson != null && lco.TeachingLanguagesJson.Contains(teachingLanguage));

            if (!string.IsNullOrEmpty(state)) query = query.Where(lco => lco.State != null && lco.State == state);

            query = ConsumerKeyFilter.Apply(query, consumer);

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

            query = query.OrderBy(lco => lco.StartDateTime).ThenBy(lco => lco.LearningComponentOfferingIdValue);
            List<LearningComponentOfferingEntity> items = await query
                .Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

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
    ///     Retrieves all test component offerings for a specific course offering.
    ///     GET /course-offerings/{courseOfferingId}/test-component-offerings
    /// </summary>
    /// <param name="courseOfferingId">The course offering ID.</param>
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
    [HttpGet("{courseOfferingId}/test-component-offerings")]
    [ProducesResponseType(typeof(PagedResult<TestComponentOffering>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTestComponentOfferingsByCourseOfferingId(
        string courseOfferingId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? consumer,
        [FromQuery] string? fields,
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
            CourseOfferingEntity? offeringEntity = await _dbContext.CourseOfferings
                .AsNoTracking()
                .FirstOrDefaultAsync(co => co.CourseOfferingId == courseOfferingId, cancellationToken)
                .ConfigureAwait(false);

            if (offeringEntity == null)
                return Problem(
                    detail: $"CourseOffering with ID '{courseOfferingId}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            IQueryable<TestComponentOfferingEntity> query = _dbContext.TestComponentOfferings
                .AsNoTracking()
                .Include(tco => tco.TestComponent)
                .Include(tco => tco.Organisation)
                .Include(tco => tco.AcademicSession)
                .Include(tco => tco.Rooms)
                .Include(tco => tco.CourseOfferings)
                .Include(tco => tco.Groups)
                .Where(tco => tco.CourseOfferings.Any(co => co.Id == offeringEntity.Id));

            query = ApplyFilterQuery(query);

            if (!string.IsNullOrEmpty(q))
                query = query.WhereContains(q, nameof(TestComponentOfferingEntity.NameJson),
                    nameof(TestComponentOfferingEntity.PrimaryCode));

            if (!string.IsNullOrEmpty(teachingLanguage))
                query = query.Where(tco =>
                    tco.TeachingLanguagesJson != null && tco.TeachingLanguagesJson.Contains(teachingLanguage));

            if (!string.IsNullOrEmpty(state)) query = query.Where(tco => tco.State != null && tco.State == state);

            query = ConsumerKeyFilter.Apply(query, consumer);

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

            query = query.OrderBy(tco => tco.StartDateTime).ThenBy(tco => tco.TestComponentOfferingIdValue);
            List<TestComponentOfferingEntity> items = await query
                .Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

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
    ///     Replaces a course offering, or creates one at the given id if it doesn't exist yet.
    ///     PUT /course-offerings/{id}. Upsert semantics per spec: <c>200</c> if replacing an existing
    ///     course offering, <c>201</c> if creating a new one at the given id. Shares the same
    ///     full-representation body and FK-resolution logic as <see cref="PatchCourseOffering" /> - the
    ///     two are contractually identical for this resource except for the create-if-missing behaviour.
    /// </summary>
    /// <param name="id">The course offering ID.</param>
    /// <param name="model">The full course offering representation.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PutCourseOffering(
        string id,
        [FromBody] CourseOffering model,
        CancellationToken cancellationToken = default)
    {
        try
        {
            CourseOfferingEntity? entity = await _dbContext.CourseOfferings
                .FirstOrDefaultAsync(co => co.CourseOfferingId == id, cancellationToken)
                .ConfigureAwait(false);

            bool isNew;
            if (entity == null)
            {
                isNew = true;
                entity = MapToEntity(model);
                entity.CourseOfferingId = id;
                _dbContext.CourseOfferings.Add(entity);
            }
            else
            {
                isNew = false;
                UpdateEntityFromApiModel(entity, model);
            }

            if (!string.IsNullOrEmpty(model.CourseId?.Value))
            {
                CourseEntity? courseEntity = await _dbContext.Courses
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.CourseId == model.CourseId.Value, cancellationToken)
                    .ConfigureAwait(false);
                if (courseEntity != null) entity.CourseEntityId = courseEntity.Id;
            }

            if (!string.IsNullOrEmpty(model.OrganisationId?.Value))
            {
                OrganisationEntity? organisationEntity = await _dbContext.Organisations
                    .AsNoTracking()
                    .FirstOrDefaultAsync(o => o.OrganisationId == model.OrganisationId.Value, cancellationToken)
                    .ConfigureAwait(false);
                if (organisationEntity != null) entity.OrganisationEntityId = organisationEntity.Id;
            }

            if (!string.IsNullOrEmpty(model.AcademicSessionId?.Value))
            {
                AcademicSessionEntity? academicSessionEntity = await _dbContext.AcademicSessions
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.AcademicSessionId == model.AcademicSessionId.Value, cancellationToken)
                    .ConfigureAwait(false);
                if (academicSessionEntity != null) entity.AcademicSessionEntityId = academicSessionEntity.Id;
            }

            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return isNew ? StatusCode(StatusCodes.Status201Created) : Ok();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    ///     Updates a course offering from its full representation, per the spec's
    ///     <c>PATCH /course-offerings/{id}</c> (which - unlike the offering-association endpoints -
    ///     takes the same full <c>application/json</c> body as PUT, not a narrow merge-patch). Scalar/JSON
    ///     fields go through the existing <see cref="UpdateEntityFromApiModel" />; the single-valued
    ///     relationship fields (<c>courseId</c>, <c>organisationId</c>, <c>academicSessionId</c>) are
    ///     resolved here since that requires the async DB lookups <c>UpdateEntityFromApiModel</c> can't
    ///     perform - see the note on its use in <see cref="MapToEntity" />. Collection-valued relationships
    ///     (<c>programmeOfferingIds</c>, <c>groupIds</c>) are not settable through this endpoint - the
    ///     spec models those as separate resources/relationships, not writable via this fields.
    /// </summary>
    /// <param name="id">The course offering ID.</param>
    /// <param name="model">The full course offering representation.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PatchCourseOffering(
        string id,
        [FromBody] CourseOffering model,
        CancellationToken cancellationToken = default)
    {
        try
        {
            CourseOfferingEntity? entity = await _dbContext.CourseOfferings
                .FirstOrDefaultAsync(co => co.CourseOfferingId == id, cancellationToken)
                .ConfigureAwait(false);

            if (entity == null)
                return Problem(
                    detail: $"CourseOffering with ID '{id}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            UpdateEntityFromApiModel(entity, model);

            if (!string.IsNullOrEmpty(model.CourseId?.Value))
            {
                CourseEntity? courseEntity = await _dbContext.Courses
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.CourseId == model.CourseId.Value, cancellationToken)
                    .ConfigureAwait(false);
                if (courseEntity != null) entity.CourseEntityId = courseEntity.Id;
            }

            if (!string.IsNullOrEmpty(model.OrganisationId?.Value))
            {
                OrganisationEntity? organisationEntity = await _dbContext.Organisations
                    .AsNoTracking()
                    .FirstOrDefaultAsync(o => o.OrganisationId == model.OrganisationId.Value, cancellationToken)
                    .ConfigureAwait(false);
                if (organisationEntity != null) entity.OrganisationEntityId = organisationEntity.Id;
            }

            if (!string.IsNullOrEmpty(model.AcademicSessionId?.Value))
            {
                AcademicSessionEntity? academicSessionEntity = await _dbContext.AcademicSessions
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.AcademicSessionId == model.AcademicSessionId.Value, cancellationToken)
                    .ConfigureAwait(false);
                if (academicSessionEntity != null) entity.AcademicSessionEntityId = academicSessionEntity.Id;
            }

            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Ok();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}
