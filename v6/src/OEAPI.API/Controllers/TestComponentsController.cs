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
///     Controller for test component endpoints.
/// </summary>
/// <remarks>
///     Initializes a new instance of the TestComponentsController class.
/// </remarks>
/// <param name="dbContext">The database context.</param>
/// <param name="logger">The logger.</param>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
[Tags("TestComponents")]
public class TestComponentsController(OEAPIDbContext dbContext, ILogger<TestComponentsController> logger) : GenericEntityController<TestComponentEntity, TestComponent>(dbContext, logger, "Test Component", "test-components")
{
    private readonly OEAPIDbContext _dbContext = dbContext;

    /// <summary>Retrieves a list of all test components.</summary>
    /// <param name="primaryCode">Filter by primary code.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response for Scalar/OpenAPI doc generation -
    // TApiModel can't be referenced in an attribute on the generic base class (CS0416). Behaviour is
    // unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(PagedResult<TestComponent>), StatusCodes.Status200OK)]
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

    /// <summary>Retrieves a single test component by ID.</summary>
    /// <param name="testComponentId">The test component ID.</param>
    /// <param name="expand">Comma-separated list of relations to expand, e.g. <c>course,organisation</c>.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response - see the GetAll override above.
    // Behaviour is unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(TestComponent), StatusCodes.Status200OK)]
    [HttpGet("{testComponentId}")]
    public override Task<IActionResult> GetById(
        string testComponentId,
        string? expand,
        string? fields,
        string? consumer,
        CancellationToken cancellationToken = default)
    {
        return base.GetById(testComponentId, expand, fields, consumer, cancellationToken);
    }

    /// <summary>
    ///     Finds a test component by unique identifier (ComponentId).
    /// </summary>
    /// <param name="idValue">The test component ID value.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the test component entity or null.</returns>
    protected override async Task<TestComponentEntity?> FindByUniqueIdAsync(string idValue,
        CancellationToken cancellationToken = default)
    {
        return await Entities
            .FirstOrDefaultAsync(tc => tc.ComponentId == idValue, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    ///     Eagerly loads the single-valued Course/Organisation/Parent navigations (and the
    ///     Children/LearningOutcomes collections, so <c>childIds</c>/<c>learningOutcomeIds</c> can be
    ///     populated without an extra query) - see
    ///     <see cref="GenericEntityController{TEntity,TApiModel}.ApplyIncludes" /> for why.
    /// </summary>
    protected override IQueryable<TestComponentEntity> ApplyIncludes(IQueryable<TestComponentEntity> query)
    {
        return query
            .Include(tc => tc.Course)
            .Include(tc => tc.Organisation)
            .Include(tc => tc.Parent)
            .Include(tc => tc.Children)
            .Include(tc => tc.LearningOutcomes);
    }

    // ========================================================================
    // Required abstract method implementations
    // ========================================================================

    /// <summary>
    ///     Maps a TestComponentEntity to a TestComponent API model.
    /// </summary>
    /// <param name="entity">The entity to map.</param>
    /// <param name="consumer">The requested consumer, for consumer-specific data.</param>
    /// <returns>The API model.</returns>
    protected override TestComponent MapToApiModel(TestComponentEntity entity, string? consumer)
    {
        return entity.ToApiModel(consumer);
    }

    /// <summary>
    ///     Maps a TestComponent API model to a TestComponentEntity.
    /// </summary>
    /// <param name="model">The API model to map.</param>
    /// <returns>The entity.</returns>
    protected override TestComponentEntity MapToEntity(TestComponent model)
    {
        return model.ToEntity();
    }

    /// <summary>
    ///     Updates a TestComponentEntity from a TestComponent API model.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="model">The API model with new data.</param>
    protected override void UpdateEntityFromApiModel(TestComponentEntity entity, TestComponent model)
    {
        entity.UpdateFrom(model);
    }

    /// <summary>
    ///     Gets the unique identifier for a test component entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>The identifier as string.</returns>
    protected override string GetEntityId(TestComponentEntity entity)
    {
        return entity.ComponentId;
    }

    // ========================================================================
    // Override methods for custom behaviour
    // ========================================================================

    /// <summary>
    ///     Applies default ordering for test components: by primary code, then by name.
    /// </summary>
    /// <param name="query">The query to order.</param>
    /// <returns>The ordered query.</returns>
    protected override IQueryable<TestComponentEntity> ApplyDefaultOrdering(IQueryable<TestComponentEntity> query)
    {
        return query.OrderBy(tc => tc.PrimaryCode).ThenBy(tc => tc.NameJson);
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
    ///     Applies the <c>q</c> (search.yaml, wire name "q"), <c>teachingLanguage</c> and
    ///     <c>resultExpected</c> filters to a <c>GetAll</c> query. <c>resultExpected</c> filters against
    ///     <see cref="TestComponentEntity.ResultExpected" />, a filter-only column with no corresponding
    ///     response field (<c>TestComponent.yaml</c> declares no <c>resultExpected</c> property - only
    ///     the collection endpoint's own query parameter).
    /// </summary>
    protected override IQueryable<TestComponentEntity> ApplyEntityFilters(IQueryable<TestComponentEntity> query)
    {
        // Spec: q (search.yaml, wire name "q", not "search").
        string? searchValue = GetQueryValue("q");
        if (!string.IsNullOrEmpty(searchValue))
            query = query.WhereContains(searchValue, nameof(TestComponentEntity.NameJson),
                nameof(TestComponentEntity.PrimaryCode));

        string? teachingLanguage = GetQueryValue("teachingLanguage");
        if (!string.IsNullOrEmpty(teachingLanguage))
            query = query.Where(tc =>
                tc.TeachingLanguagesJson != null && tc.TeachingLanguagesJson.Contains(teachingLanguage));

        string? resultExpectedValue = GetQueryValue("resultExpected");
        if (bool.TryParse(resultExpectedValue, out bool resultExpected))
            query = query.Where(tc => tc.ResultExpected == resultExpected);

        return query;
    }

    // Create/Update/Delete removed: the spec defines no write operations at all for
    // /test-components.

    /// <summary>
    ///     Applies expand to a TestComponent API model (includes related entities).
    /// </summary>
    /// <param name="apiModel">The TestComponent API model to expand.</param>
    /// <param name="expand">The expand parameter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the expanded TestComponent API
    ///     model.
    /// </returns>
    protected override async Task<TestComponent> ApplyExpandAsync(
        TestComponent apiModel,
        string expand,
        CancellationToken cancellationToken = default)
    {
        string[] expands = expand.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        // Cache the component entity for reuse across expand options
        TestComponentEntity? componentEntity = null;

        foreach (string expandOption in expands)
            switch (expandOption.ToLower())
            {
                case "course":
                    // Find the test component entity to get the course reference
                    componentEntity = await _dbContext.TestComponents
                        .AsNoTracking()
                        .FirstOrDefaultAsync(tc => tc.ComponentId == apiModel.ComponentId, cancellationToken)
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
                    // Find the test component entity to get the organisation reference (if not already loaded)
                    componentEntity ??= await _dbContext.TestComponents
                        .AsNoTracking()
                        .FirstOrDefaultAsync(tc => tc.ComponentId == apiModel.ComponentId, cancellationToken)
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
                        TestComponentEntity? parentEntity = await Entities
                            .FirstOrDefaultAsync(tc => tc.ComponentId == apiModel.ParentId.Value, cancellationToken)
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
                        List<TestComponentEntity> childEntities = await Entities
                            .Where(tc => tc.ParentEntityId != null && tc.Parent!.ComponentId == apiModel.ComponentId)
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
    ///     Retrieves all test component offerings for a specific test component.
    ///     GET /test-components/{testComponentId}/test-component-offerings
    /// </summary>
    /// <param name="testComponentId">The test component ID.</param>
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
    [HttpGet("{testComponentId}/test-component-offerings")]
    [ProducesResponseType(typeof(PagedResult<TestComponentOffering>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTestComponentOfferingsByTestComponentId(
        string testComponentId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? consumer,
        [FromQuery] string? fields,
        [FromQuery] string? q,
        [FromQuery] string? teachingLanguage,
        [FromQuery(Name = "state")] string? state,
        [FromQuery] bool? resultExpected,
        [FromQuery] string? since,
        [FromQuery] string? until,
        CancellationToken cancellationToken = default)
    {
        try
        {
            TestComponentEntity? componentEntity = await _dbContext.TestComponents
                .AsNoTracking()
                .FirstOrDefaultAsync(tc => tc.ComponentId == testComponentId, cancellationToken)
                .ConfigureAwait(false);

            if (componentEntity == null)
                return Problem(
                    detail: $"Test component with ID '{testComponentId}' not found.",
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
                .Where(tco => tco.TestComponentEntityId == componentEntity.Id);

            query = ApplyFilterQuery(query);

            if (!string.IsNullOrEmpty(q))
                query = query.WhereContains(q, nameof(TestComponentOfferingEntity.NameJson),
                    nameof(TestComponentOfferingEntity.PrimaryCode));

            query = ConsumerKeyFilter.Apply(query, consumer);

            if (!string.IsNullOrEmpty(teachingLanguage))
                query = query.Where(tco =>
                    tco.TeachingLanguagesJson != null && tco.TeachingLanguagesJson.Contains(teachingLanguage));

            if (!string.IsNullOrEmpty(state)) query = query.Where(tco => tco.State != null && tco.State == state);

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
}
