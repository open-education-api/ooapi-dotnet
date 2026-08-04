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
///     Controller for learning component endpoints.
/// </summary>
/// <remarks>
///     Initializes a new instance of the LearningComponentsController class.
/// </remarks>
/// <param name="dbContext">The database context.</param>
/// <param name="logger">The logger.</param>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
[Tags("LearningComponents")]
public class LearningComponentsController(OEAPIDbContext dbContext, ILogger<LearningComponentsController> logger) : GenericEntityController<LearningComponentEntity, LearningComponent>(dbContext, logger, "Learning Component", "learning-components")
{
    private readonly OEAPIDbContext _dbContext = dbContext;

    /// <summary>Retrieves a list of all learning components.</summary>
    /// <param name="primaryCode">Filter by primary code.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response for Scalar/OpenAPI doc generation -
    // TApiModel can't be referenced in an attribute on the generic base class (CS0416). Behaviour is
    // unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(PagedResult<LearningComponent>), StatusCodes.Status200OK)]
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

    /// <summary>Retrieves a single learning component by ID.</summary>
    /// <param name="learningComponentId">The learning component ID.</param>
    /// <param name="expand">Comma-separated list of relations to expand, e.g. <c>course,organisation</c>.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response - see the GetAll override above.
    // Behaviour is unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(LearningComponent), StatusCodes.Status200OK)]
    [HttpGet("{learningComponentId}")]
    public override Task<IActionResult> GetById(
        string learningComponentId,
        string? expand,
        string? fields,
        string? consumer,
        CancellationToken cancellationToken = default)
    {
        return base.GetById(learningComponentId, expand, fields, consumer, cancellationToken);
    }

    /// <summary>
    ///     Finds a learning component by unique identifier (ComponentId).
    /// </summary>
    /// <param name="idValue">The learning component ID value.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the learning component entity or
    ///     null.
    /// </returns>
    protected override async Task<LearningComponentEntity?> FindByUniqueIdAsync(string idValue,
        CancellationToken cancellationToken = default)
    {
        return await Entities
            .FirstOrDefaultAsync(lc => lc.ComponentId == idValue, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    ///     Eagerly loads the single-valued Course/Organisation/Parent navigations (and the
    ///     Children/LearningOutcomes collections, so <c>childIds</c>/<c>learningOutcomeIds</c> can be
    ///     populated without an extra query) - see
    ///     <see cref="GenericEntityController{TEntity,TApiModel}.ApplyIncludes" /> for why.
    /// </summary>
    protected override IQueryable<LearningComponentEntity> ApplyIncludes(IQueryable<LearningComponentEntity> query)
    {
        return query
            .Include(lc => lc.Course)
            .Include(lc => lc.Organisation)
            .Include(lc => lc.Parent)
            .Include(lc => lc.Children)
            .Include(lc => lc.LearningOutcomes);
    }

    // ========================================================================
    // Required abstract method implementations
    // ========================================================================

    /// <summary>
    ///     Maps a LearningComponentEntity to a LearningComponent API model.
    /// </summary>
    /// <param name="entity">The entity to map.</param>
    /// <param name="consumer">The requested consumer, for consumer-specific data.</param>
    /// <returns>The API model.</returns>
    protected override LearningComponent MapToApiModel(LearningComponentEntity entity, string? consumer)
    {
        return entity.ToApiModel(consumer);
    }

    /// <summary>
    ///     Maps a LearningComponent API model to a LearningComponentEntity.
    /// </summary>
    /// <param name="model">The API model to map.</param>
    /// <returns>The entity.</returns>
    protected override LearningComponentEntity MapToEntity(LearningComponent model)
    {
        return model.ToEntity();
    }

    /// <summary>
    ///     Updates a LearningComponentEntity from a LearningComponent API model.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="model">The API model with new data.</param>
    protected override void UpdateEntityFromApiModel(LearningComponentEntity entity, LearningComponent model)
    {
        entity.UpdateFrom(model);
    }

    /// <summary>
    ///     Gets the unique identifier for a learning component entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>The identifier as string.</returns>
    protected override string GetEntityId(LearningComponentEntity entity)
    {
        return entity.ComponentId;
    }

    // ========================================================================
    // Override methods for custom behaviour
    // ========================================================================

    /// <summary>
    ///     Applies default ordering for learning components: by primary code, then by name.
    /// </summary>
    /// <param name="query">The query to order.</param>
    /// <returns>The ordered query.</returns>
    protected override IQueryable<LearningComponentEntity> ApplyDefaultOrdering(
        IQueryable<LearningComponentEntity> query)
    {
        return query.OrderBy(lc => lc.PrimaryCode).ThenBy(lc => lc.NameJson);
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
    ///     Applies the <c>q</c> (search.yaml, wire name "q") and <c>teachingLanguage</c> filters to a
    ///     <c>GetAll</c> query - not formal parameters, since <c>GetAll</c>'s signature is fixed by the
    ///     generic base controller (mirrors <see cref="TestComponentsController.ApplyEntityFilters" />'s
    ///     pattern, and the same filters already implemented on this controller's own nested
    ///     <c>learning-component-offerings</c> endpoint).
    /// </summary>
    protected override IQueryable<LearningComponentEntity> ApplyEntityFilters(
        IQueryable<LearningComponentEntity> query)
    {
        string? searchValue = GetQueryValue("q");
        if (!string.IsNullOrEmpty(searchValue))
            query = query.WhereContains(searchValue, nameof(LearningComponentEntity.NameJson),
                nameof(LearningComponentEntity.PrimaryCode));

        string? teachingLanguage = GetQueryValue("teachingLanguage");
        if (!string.IsNullOrEmpty(teachingLanguage))
            query = query.Where(lc =>
                lc.TeachingLanguagesJson != null && lc.TeachingLanguagesJson.Contains(teachingLanguage));

        return query;
    }

    // Create/Update/Delete removed: the spec defines no write operations at all for
    // /learning-components.

    /// <summary>
    ///     Applies expand to a LearningComponent API model (includes related entities).
    /// </summary>
    /// <param name="apiModel">The LearningComponent API model to expand.</param>
    /// <param name="expand">The expand parameter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the expanded LearningComponent API
    ///     model.
    /// </returns>
    protected override async Task<LearningComponent> ApplyExpandAsync(
        LearningComponent apiModel,
        string expand,
        CancellationToken cancellationToken = default)
    {
        string[] expands = expand.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        // Cache the component entity for reuse across expand options
        LearningComponentEntity? componentEntity = null;

        foreach (string expandOption in expands)
            switch (expandOption.ToLower())
            {
                case "course":
                    // Find the learning component entity to get the course reference
                    componentEntity = await _dbContext.LearningComponents
                        .AsNoTracking()
                        .FirstOrDefaultAsync(lc => lc.ComponentId == apiModel.ComponentId, cancellationToken)
                        .ConfigureAwait(false);

                    if (componentEntity?.CourseEntityId.HasValue == true && apiModel.Course == null)
                    {
                        Guid courseEntityId = componentEntity.CourseEntityId.Value;
                        CourseEntity? courseEntity = await _dbContext.Courses
                            .AsNoTracking()
                            .Include(c => c.Organisation)
                            .Include(c => c.CourseCoordinatorEntities)
                            .Include(c => c.CourseInstructorEntities)
                            .Include(c => c.ProgrammeEntities)
                            .Include(c => c.LearningOutcomes)
                            .FirstOrDefaultAsync(c => c.Id == courseEntityId, cancellationToken)
                            .ConfigureAwait(false);

                        if (courseEntity != null) apiModel.Course = courseEntity.ToApiModel(null);
                    }

                    break;

                case "organisation":
                    // Find the learning component entity to get the organisation reference (if not already loaded)
                    componentEntity ??= await _dbContext.LearningComponents
                        .AsNoTracking()
                        .FirstOrDefaultAsync(lc => lc.ComponentId == apiModel.ComponentId, cancellationToken)
                        .ConfigureAwait(false);

                    if (componentEntity?.OrganisationEntityId.HasValue == true && apiModel.Organisation == null)
                    {
                        Guid organisationEntityId = componentEntity.OrganisationEntityId.Value;
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
                    if (!string.IsNullOrEmpty(apiModel.ParentId?.Value))
                    {
                        LearningComponentEntity? parentEntity = await Entities
                            .FirstOrDefaultAsync(lc => lc.ComponentId == apiModel.ParentId.Value, cancellationToken)
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
                        List<LearningComponentEntity> childEntities = await Entities
                            .Where(lc => lc.ParentEntityId != null && lc.Parent!.ComponentId == apiModel.ComponentId)
                            .ToListAsync(cancellationToken)
                            .ConfigureAwait(false);

                        apiModel.Children = [.. childEntities.Select(e => MapToApiModel(e, null))];
                    }

                    break;

                case "learning_outcomes":
                    if (apiModel.LearningOutcomeIds != null && apiModel.LearningOutcomeIds.Length > 0)
                    {
                        List<LearningOutcomeEntity> learningOutcomeEntities = await _dbContext.LearningOutcomes
                            .AsNoTracking()
                            .IncludeHierarchy()
                            .Where(lo =>
                                apiModel.LearningOutcomeIds.Select(id => id.Value).Contains(lo.LearningOutcomeId))
                            .ToListAsync(cancellationToken)
                            .ConfigureAwait(false);

                        if (learningOutcomeEntities.Count > 0)
                            apiModel.LearningOutcomes = [.. learningOutcomeEntities.Select(lo => lo.ToApiModel(null))];
                    }

                    break;
            }

        return apiModel;
    }

    /// <summary>
    ///     Retrieves all learning component offerings for a specific learning component.
    ///     GET /learning-components/{learningComponentId}/learning-component-offerings
    /// </summary>
    /// <param name="learningComponentId">The learning component ID.</param>
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
    [HttpGet("{learningComponentId}/learning-component-offerings")]
    [ProducesResponseType(typeof(PagedResult<LearningComponentOffering>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLearningComponentOfferingsByLearningComponentId(
        string learningComponentId,
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
            LearningComponentEntity? componentEntity = await _dbContext.LearningComponents
                .AsNoTracking()
                .FirstOrDefaultAsync(lc => lc.ComponentId == learningComponentId, cancellationToken)
                .ConfigureAwait(false);

            if (componentEntity == null)
                return Problem(
                    detail: $"Learning component with ID '{learningComponentId}' not found.",
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
                .Where(lco => lco.LearningComponentEntityId == componentEntity.Id);

            query = ApplyFilterQuery(query);

            if (!string.IsNullOrEmpty(q))
                query = query.WhereContains(q, nameof(LearningComponentOfferingEntity.NameJson),
                    nameof(LearningComponentOfferingEntity.PrimaryCode));

            // Apply teachingLanguage filter - TeachingLanguagesJson is a plain JSON-array-of-strings
            // column, so a substring Contains (portable LIKE across both providers) is the only query
            // this codebase has infrastructure for; see design.md Decision 4.
            if (!string.IsNullOrEmpty(teachingLanguage))
                query = query.Where(lco =>
                    lco.TeachingLanguagesJson != null && lco.TeachingLanguagesJson.Contains(teachingLanguage));

            // Apply state filter (the spec's offeringState parameter - "state" is its actual wire name)
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
}
