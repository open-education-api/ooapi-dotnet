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
///     Controller for test component offering endpoints.
/// </summary>
/// <remarks>
///     Initializes a new instance of the TestComponentOfferingsController class. The top-level
///     <c>GET /test-component-offerings</c> this class's
///     <see cref="GenericEntityController{TEntity,TApiModel}" /> base provides has no canonical-spec
///     counterpart (the spec only exposes this resource nested under a parent, or by single-item
///     <c>GET</c>) - off by default (<c>404</c>) unless the deployment opts in via
///     <c>Service:ExposeNonCanonicalListEndpoints</c>; see <see cref="NonCanonicalTopLevelListRoutes" />
///     and <c>docs/archive/DECISIONS-AND-ACTIONS.md</c>.
/// </remarks>
/// <param name="dbContext">The database context.</param>
/// <param name="logger">The logger.</param>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
[Tags("TestComponentOfferings")]
public class
    TestComponentOfferingsController(OEAPIDbContext dbContext, ILogger<TestComponentOfferingsController> logger) : GenericEntityController<TestComponentOfferingEntity, TestComponentOffering>(dbContext, logger, "TestComponentOffering", "test-component-offerings")
{
    private readonly OEAPIDbContext _dbContext = dbContext;

    /// <summary>Retrieves a list of all test component offerings.</summary>
    /// <param name="primaryCode">Filter by primary code.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response for Scalar/OpenAPI doc generation -
    // TApiModel can't be referenced in an attribute on the generic base class (CS0416). Behaviour is
    // unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(PagedResult<TestComponentOffering>), StatusCodes.Status200OK)]
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

    /// <summary>Retrieves a single test component offering by ID.</summary>
    /// <param name="testComponentOfferingId">The test component offering ID.</param>
    /// <param name="expand">Comma-separated list of relations to expand, e.g. <c>component,organisation</c>.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response - see the GetAll override above.
    // Behaviour is unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(TestComponentOffering), StatusCodes.Status200OK)]
    [HttpGet("{testComponentOfferingId}")]
    public override Task<IActionResult> GetById(
        string testComponentOfferingId,
        string? expand,
        string? fields,
        string? consumer,
        CancellationToken cancellationToken = default)
    {
        return base.GetById(testComponentOfferingId, expand, fields, consumer, cancellationToken);
    }

    /// <summary>
    ///     Finds a test component offering by unique identifier (TestComponentOfferingIdValue -
    ///     doesn't match the base class's "{EntityName}Id" reflection convention, so this needs an
    ///     explicit override).
    /// </summary>
    protected override async Task<TestComponentOfferingEntity?> FindByUniqueIdAsync(string idValue,
        CancellationToken cancellationToken = default)
    {
        return await Entities
            .FirstOrDefaultAsync(tco => tco.TestComponentOfferingIdValue == idValue, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    ///     Eagerly loads TestComponent/Organisation/AcademicSession/Rooms/CourseOfferings so
    ///     <c>MapToApiModel</c> can read them directly - see
    ///     <see cref="GenericEntityController{TEntity,TApiModel}.ApplyIncludes" /> for why.
    /// </summary>
    protected override IQueryable<TestComponentOfferingEntity> ApplyIncludes(
        IQueryable<TestComponentOfferingEntity> query)
    {
        return query
            .Include(tco => tco.TestComponent)
            .Include(tco => tco.Organisation)
            .Include(tco => tco.AcademicSession)
            .Include(tco => tco.Rooms)
            .Include(tco => tco.CourseOfferings)
            .Include(tco => tco.Groups);
    }

    // ========================================================================
    // Required abstract method implementations
    // ========================================================================

    /// <summary>
    ///     Maps a TestComponentOfferingEntity to a TestComponentOffering API model.
    /// </summary>
    /// <param name="entity">The entity to map.</param>
    /// <param name="consumer">The requested consumer, for consumer-specific data.</param>
    /// <returns>The API model.</returns>
    protected override TestComponentOffering MapToApiModel(TestComponentOfferingEntity entity, string? consumer)
    {
        return entity.ToApiModel(consumer);
    }

    /// <summary>
    ///     Maps a TestComponentOffering API model to a TestComponentOfferingEntity. Not currently
    ///     called by any live code path (see the note on writes in
    ///     <see cref="GenericEntityController{TEntity,TApiModel}" />). Note: resolving the model's
    ///     external string IDs (<c>componentId</c>, <c>organisationId</c>, <c>academicSessionId</c>,
    ///     <c>roomIds</c>, <c>courseOfferingIds</c>) to the entity's internal Guid FKs/relationships
    ///     requires async DB lookups that this synchronous method can't perform - whichever explicit
    ///     write action ends up calling this needs to resolve and attach those relationships itself
    ///     after calling this method.
    /// </summary>
    /// <param name="model">The API model to map.</param>
    /// <returns>The entity.</returns>
    protected override TestComponentOfferingEntity MapToEntity(TestComponentOffering model)
    {
        return model.ToEntity();
    }

    /// <summary>
    ///     Updates a TestComponentOfferingEntity from a TestComponentOffering API model. See the
    ///     note on <see cref="MapToEntity" /> regarding relationship resolution.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="model">The API model with new data.</param>
    protected override void UpdateEntityFromApiModel(TestComponentOfferingEntity entity, TestComponentOffering model)
    {
        entity.UpdateFrom(model);
    }

    /// <summary>
    ///     Gets the unique identifier for a test component offering entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>The identifier as string.</returns>
    protected override string GetEntityId(TestComponentOfferingEntity entity)
    {
        return entity.TestComponentOfferingIdValue;
    }

    // ========================================================================
    // Custom methods for TestComponentOfferingsController
    // ========================================================================

    /// <summary>
    ///     Applies default ordering for test component offerings: by code, then by start date.
    /// </summary>
    /// <param name="query">The query to order.</param>
    /// <returns>The ordered query.</returns>
    protected override IQueryable<TestComponentOfferingEntity> ApplyDefaultOrdering(
        IQueryable<TestComponentOfferingEntity> query)
    {
        return query.OrderBy(tco => tco.PrimaryCode).ThenBy(tco => tco.StartDateTime);
    }

    /// <summary>
    ///     Applies expand functionality for test component offerings.
    /// </summary>
    /// <param name="apiModel">The API model to expand.</param>
    /// <param name="expand">The expand parameter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the expanded API model.</returns>
    protected override async Task<TestComponentOffering> ApplyExpandAsync(
        TestComponentOffering apiModel,
        string expand,
        CancellationToken cancellationToken = default)
    {
        string[] expands = expand.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        foreach (string expandOption in expands)
            switch (expandOption.ToLower())
            {
                // Bug fix: this was previously "component" (not "test_component" or even
                // "testcomponent") - the spec's own snake_case wire value could never match it, so
                // `expand=test_component` silently no-op'd for every caller. Found while removing the
                // Replace("_", "") normalization codebase-wide and switching every case label to the
                // spec's literal spelling.
                case "test_component":
                    if (!string.IsNullOrEmpty(apiModel.TestComponentId?.Value))
                    {
                        TestComponentEntity? testComponentEntity = await _dbContext.TestComponents
                            .AsNoTracking()
                            .Include(tc => tc.Course)
                            .Include(tc => tc.Organisation)
                            .Include(tc => tc.Parent)
                            .Include(tc => tc.Children)
                            .Include(tc => tc.LearningOutcomes)
                            .FirstOrDefaultAsync(tc => tc.ComponentId == apiModel.TestComponentId.Value,
                                cancellationToken)
                            .ConfigureAwait(false);

                        if (testComponentEntity != null) apiModel.TestComponent = testComponentEntity.ToApiModel(null);
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

                // Not spec-declared for this operation's own `expand` enum (only `test_component`/
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

        return apiModel;
    }

    // ========================================================================
    // Nested endpoints for test-component-offerings/{testComponentOfferingId}/*
    // ========================================================================

    /// <summary>
    ///     Retrieves all test component offering associations for a specific test component offering.
    ///     GET /api/test-component-offerings/{testComponentOfferingId}/test-component-offering-associations
    /// </summary>
    /// <param name="testComponentOfferingId">The test component offering ID.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="filterQuery">Generic RSQL-style filter query.</param>
    /// <param name="role">Filter by the person's role in the association.</param>
    /// <param name="state">Filter by association state.</param>
    /// <param name="resultState">Filter by the association result's state.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{testComponentOfferingId}/test-component-offering-associations")]
    [ProducesResponseType(typeof(PagedResult<TestComponentOfferingAssociation>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAssociationsByTestComponentOfferingId(
        string testComponentOfferingId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? consumer,
        [FromQuery] string? fields,
        [FromQuery] string? filterQuery,
        [FromQuery] string? role,
        [FromQuery] string? state,
        [FromQuery] string? resultState,
        CancellationToken cancellationToken = default)
    {
        try
        {
            TestComponentOfferingEntity? offeringEntity = await _dbContext.TestComponentOfferings
                .AsNoTracking()
                .FirstOrDefaultAsync(tco => tco.TestComponentOfferingIdValue == testComponentOfferingId,
                    cancellationToken)
                .ConfigureAwait(false);

            if (offeringEntity == null)
                return Problem(
                    detail: $"TestComponentOffering with ID '{testComponentOfferingId}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            IQueryable<TestComponentOfferingAssociationEntity> query = _dbContext.TestComponentOfferingAssociations
                .AsNoTracking()
                .Include(a => a.TestComponentOffering)
                .Include(a => a.Person)
                .Include(a => a.Organisation)
                .Include(a => a.Attempts)
                .Where(a => a.TestComponentOfferingEntityId == offeringEntity.Id);

            query = ApplyFilterQuery(query);

            if (!string.IsNullOrEmpty(role)) query = query.Where(a => a.Role != null && a.Role == role);

            if (!string.IsNullOrEmpty(state)) query = query.Where(a => a.State != null && a.State == state);

            query = ConsumerKeyFilter.Apply(query, consumer);

            query = query.OrderBy(a => a.PrimaryCode);

            // resultState filters on Result.State, deserialized from the opaque ResultJson blob - no
            // portable SQL-level query exists for either provider (mirrors
            // PersonsController.GetCourseOfferingAssociationsByPersonId), so every association matching
            // the SQL-level filters above is mapped first, then filtered by resultState in memory, and
            // only then paginated - preserving correct totalCount/paging instead of paginating before
            // this filter and under-filling pages.
            List<TestComponentOfferingAssociationEntity> candidateEntities = await query
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            List<TestComponentOfferingAssociation> candidates =
                [.. candidateEntities.Select(item => item.ToApiModel(consumer))];

            if (!string.IsNullOrEmpty(resultState))
                candidates = [.. candidates.Where(c => c.Result != null && c.Result.State == resultState)];

            int totalCount = candidates.Count;
            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);

            List<TestComponentOfferingAssociation> apiModels = [.. candidates
                .Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize)];

            PagedResult<TestComponentOfferingAssociation> result = new(apiModels, totalCount, validatedPage,
                validatedPageSize);
            return PagedResponseWithFieldSelection(result, fields);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    ///     Retrieves all groups associated with a specific test component offering.
    ///     GET /api/test-component-offerings/{testComponentOfferingId}/groups
    /// </summary>
    /// <param name="testComponentOfferingId">The test component offering ID.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="groupType">Filter by group type.</param>
    /// <param name="q">Search term.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="filterQuery">Generic RSQL-style filter query.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{testComponentOfferingId}/groups")]
    [ProducesResponseType(typeof(PagedResult<Group>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetGroupsByTestComponentOfferingId(
        string testComponentOfferingId,
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
            TestComponentOfferingEntity? offeringEntity = await _dbContext.TestComponentOfferings
                .AsNoTracking()
                .FirstOrDefaultAsync(tco => tco.TestComponentOfferingIdValue == testComponentOfferingId,
                    cancellationToken)
                .ConfigureAwait(false);

            if (offeringEntity == null)
                return Problem(
                    detail: $"TestComponentOffering with ID '{testComponentOfferingId}' not found.",
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
                .Where(g => g.TestComponentOfferings.Any(tco => tco.Id == offeringEntity.Id));

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
    ///     Replaces a test component offering, or creates one at the given id if it doesn't exist yet.
    ///     PUT /test-component-offerings/{id}. Upsert semantics per spec: <c>200</c> if replacing an
    ///     existing offering, <c>201</c> if creating a new one at the given id. Shares the same
    ///     full-representation body and FK-resolution logic as
    ///     <see cref="PatchTestComponentOffering" /> - the two are contractually identical for this
    ///     resource except for the create-if-missing behaviour.
    /// </summary>
    /// <param name="id">The test component offering ID.</param>
    /// <param name="model">The full test component offering representation.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PutTestComponentOffering(
        string id,
        [FromBody] TestComponentOffering model,
        CancellationToken cancellationToken = default)
    {
        try
        {
            TestComponentOfferingEntity? entity = await _dbContext.TestComponentOfferings
                .FirstOrDefaultAsync(tco => tco.TestComponentOfferingIdValue == id, cancellationToken)
                .ConfigureAwait(false);

            bool isNew;
            if (entity == null)
            {
                isNew = true;
                entity = MapToEntity(model);
                entity.TestComponentOfferingIdValue = id;
                _dbContext.TestComponentOfferings.Add(entity);
            }
            else
            {
                isNew = false;
                UpdateEntityFromApiModel(entity, model);
            }

            if (!string.IsNullOrEmpty(model.TestComponentId?.Value))
            {
                TestComponentEntity? testComponentEntity = await _dbContext.TestComponents
                    .AsNoTracking()
                    .FirstOrDefaultAsync(tc => tc.ComponentId == model.TestComponentId.Value, cancellationToken)
                    .ConfigureAwait(false);
                if (testComponentEntity != null) entity.TestComponentEntityId = testComponentEntity.Id;
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
    ///     Updates a test component offering from its full representation, per the spec's
    ///     <c>PATCH /test-component-offerings/{id}</c> (which - unlike the offering-association
    ///     endpoints - takes the same full <c>application/json</c> body as PUT, not a narrow merge-patch).
    ///     Scalar/JSON fields go through the existing <see cref="UpdateEntityFromApiModel" />; the
    ///     single-valued relationship fields (<c>componentId</c>, <c>organisationId</c>,
    ///     <c>academicSessionId</c>) are resolved here since that requires the async DB lookups
    ///     <c>UpdateEntityFromApiModel</c> can't perform - see the note on its use in
    ///     <see cref="MapToEntity" />. Collection-valued relationships (<c>roomIds</c>,
    ///     <c>courseOfferingIds</c>, <c>groupIds</c>) are not settable through this endpoint.
    /// </summary>
    /// <param name="id">The test component offering ID.</param>
    /// <param name="model">The full test component offering representation.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PatchTestComponentOffering(
        string id,
        [FromBody] TestComponentOffering model,
        CancellationToken cancellationToken = default)
    {
        try
        {
            TestComponentOfferingEntity? entity = await _dbContext.TestComponentOfferings
                .FirstOrDefaultAsync(tco => tco.TestComponentOfferingIdValue == id, cancellationToken)
                .ConfigureAwait(false);

            if (entity == null)
                return Problem(
                    detail: $"TestComponentOffering with ID '{id}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            UpdateEntityFromApiModel(entity, model);

            if (!string.IsNullOrEmpty(model.TestComponentId?.Value))
            {
                TestComponentEntity? testComponentEntity = await _dbContext.TestComponents
                    .AsNoTracking()
                    .FirstOrDefaultAsync(tc => tc.ComponentId == model.TestComponentId.Value, cancellationToken)
                    .ConfigureAwait(false);
                if (testComponentEntity != null) entity.TestComponentEntityId = testComponentEntity.Id;
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
