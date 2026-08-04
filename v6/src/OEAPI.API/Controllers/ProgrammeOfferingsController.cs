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
///     Controller for programme offering endpoints.
/// </summary>
/// <remarks>
///     Initializes a new instance of the ProgrammeOfferingsController class. The top-level
///     <c>GET /programme-offerings</c> this class's <see cref="GenericEntityController{TEntity,TApiModel}" />
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
[Tags("ProgrammeOfferings")]
public class ProgrammeOfferingsController(OEAPIDbContext dbContext, ILogger<ProgrammeOfferingsController> logger) : GenericEntityController<ProgrammeOfferingEntity, ProgrammeOffering>(dbContext, logger, "ProgrammeOffering", "programme-offerings")
{
    private readonly OEAPIDbContext _dbContext = dbContext;

    /// <summary>Retrieves a list of all programme offerings.</summary>
    /// <param name="primaryCode">Filter by primary code.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response for Scalar/OpenAPI doc generation -
    // TApiModel can't be referenced in an attribute on the generic base class (CS0416). Behaviour is
    // unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(PagedResult<ProgrammeOffering>), StatusCodes.Status200OK)]
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

    /// <summary>Retrieves a single programme offering by ID.</summary>
    /// <param name="programmeOfferingId">The programme offering ID.</param>
    /// <param name="expand">Comma-separated list of relations to expand, e.g. <c>programme,organisation</c>.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response - see the GetAll override above.
    // Behaviour is unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(ProgrammeOffering), StatusCodes.Status200OK)]
    [HttpGet("{programmeOfferingId}")]
    public override Task<IActionResult> GetById(
        string programmeOfferingId,
        string? expand,
        string? fields,
        string? consumer,
        CancellationToken cancellationToken = default)
    {
        return base.GetById(programmeOfferingId, expand, fields, consumer, cancellationToken);
    }

    /// <summary>
    ///     Finds a programme offering by unique identifier (ProgrammeOfferingIdValue - the entity's ID
    ///     property doesn't match the base class's "{EntityName}Id" reflection convention, so this needs
    ///     an explicit override).
    /// </summary>
    protected override async Task<ProgrammeOfferingEntity?> FindByUniqueIdAsync(string idValue,
        CancellationToken cancellationToken = default)
    {
        return await Entities
            .FirstOrDefaultAsync(po => po.ProgrammeOfferingIdValue == idValue, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    ///     Eagerly loads Programme/Organisation/AcademicSession so <c>MapToApiModel</c> can read them
    ///     directly - see <see cref="GenericEntityController{TEntity,TApiModel}.ApplyIncludes" /> for why.
    /// </summary>
    protected override IQueryable<ProgrammeOfferingEntity> ApplyIncludes(IQueryable<ProgrammeOfferingEntity> query)
    {
        return query
            .Include(po => po.Programme)
            .Include(po => po.Organisation)
            .Include(po => po.AcademicSession)
            .Include(po => po.Groups);
    }

    // ========================================================================
    // Required abstract method implementations
    // ========================================================================

    /// <summary>
    ///     Maps a ProgrammeOfferingEntity to a ProgrammeOffering API model.
    /// </summary>
    /// <param name="entity">The entity to map.</param>
    /// <param name="consumer">The requested consumer, for consumer-specific data.</param>
    /// <returns>The API model.</returns>
    protected override ProgrammeOffering MapToApiModel(ProgrammeOfferingEntity entity, string? consumer)
    {
        return entity.ToApiModel(consumer);
    }

    /// <summary>
    ///     Maps a ProgrammeOffering API model to a ProgrammeOfferingEntity. Not currently called by any
    ///     live code path (see the note on writes in <see cref="GenericEntityController{TEntity,TApiModel}" />).
    ///     Note: resolving <c>model.ProgrammeId</c>/<c>model.OrganisationId</c>/<c>model.AcademicSessionId</c>
    ///     (external string IDs) to the entity's internal Guid FKs requires an async DB lookup that this
    ///     synchronous method can't perform - whichever explicit write action ends up calling this needs
    ///     to resolve and set those FKs itself after calling this method.
    /// </summary>
    /// <param name="model">The API model to map.</param>
    /// <returns>The entity.</returns>
    protected override ProgrammeOfferingEntity MapToEntity(ProgrammeOffering model)
    {
        return model.ToEntity();
    }

    /// <summary>
    ///     Updates a ProgrammeOfferingEntity from a ProgrammeOffering API model. See the note on
    ///     <see cref="MapToEntity" /> regarding Programme/Organisation/AcademicSession FK resolution.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="model">The API model with new data.</param>
    protected override void UpdateEntityFromApiModel(ProgrammeOfferingEntity entity, ProgrammeOffering model)
    {
        entity.UpdateFrom(model);
    }

    /// <summary>
    ///     Gets the unique identifier for a programme offering entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>The identifier as string.</returns>
    protected override string GetEntityId(ProgrammeOfferingEntity entity)
    {
        return entity.ProgrammeOfferingIdValue;
    }

    // ========================================================================
    // Custom methods for ProgrammeOfferingsController
    // ========================================================================

    /// <summary>
    ///     Applies default ordering for programme offerings: by code, then by start date.
    /// </summary>
    /// <param name="query">The query to order.</param>
    /// <returns>The ordered query.</returns>
    protected override IQueryable<ProgrammeOfferingEntity> ApplyDefaultOrdering(
        IQueryable<ProgrammeOfferingEntity> query)
    {
        return query.OrderBy(po => po.PrimaryCode).ThenBy(po => po.StartDateTime);
    }

    /// <summary>
    ///     Applies expand functionality for programme offerings.
    /// </summary>
    /// <param name="apiModel">The API model to expand.</param>
    /// <param name="expand">The expand parameter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the expanded API model.</returns>
    protected override async Task<ProgrammeOffering> ApplyExpandAsync(
        ProgrammeOffering apiModel,
        string expand,
        CancellationToken cancellationToken = default)
    {
        string[] expands = expand.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        foreach (string expandOption in expands)
            switch (expandOption.ToLower())
            {
                case "programme":
                    if (!string.IsNullOrEmpty(apiModel.ProgrammeId?.Value))
                    {
                        ProgrammeEntity? programmeEntity = await _dbContext.Programmes
                            .AsNoTracking()
                            .Include(p => p.Parent)
                            .Include(p => p.Children)
                            .Include(p => p.Organisation)
                            .Include(p => p.Coordinators)
                            .Include(p => p.Instructors)
                            .FirstOrDefaultAsync(p => p.ProgrammeId == apiModel.ProgrammeId.Value, cancellationToken)
                            .ConfigureAwait(false);

                        if (programmeEntity != null) apiModel.Programme = programmeEntity.ToApiModel(null);
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
            }

        return apiModel;
    }

    // ========================================================================
    // Nested endpoints for programme-offerings/{programmeOfferingId}/*
    // ========================================================================

    /// <summary>
    ///     Retrieves all programme offering associations for a specific programme offering.
    ///     GET /api/programme-offerings/{programmeOfferingId}/programme-offering-associations
    /// </summary>
    /// <param name="programmeOfferingId">The programme offering ID.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="filterQuery">The spec's filter_query DSL expression.</param>
    /// <param name="role">Filter by the person's role in the association.</param>
    /// <param name="state">Filter by state.</param>
    /// <param name="resultState">Filter by the association result's state.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{programmeOfferingId}/programme-offering-associations")]
    [ProducesResponseType(typeof(PagedResult<ProgrammeOfferingAssociation>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProgrammeOfferingAssociationsByProgrammeOfferingId(
        string programmeOfferingId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? consumer,
        [FromQuery(Name = "fields")] string? fields,
        [FromQuery] string? filterQuery,
        [FromQuery] string? role,
        [FromQuery] string? state,
        [FromQuery] string? resultState,
        CancellationToken cancellationToken = default)
    {
        try
        {
            ProgrammeOfferingEntity? offeringEntity = await _dbContext.ProgrammeOfferings
                .AsNoTracking()
                .FirstOrDefaultAsync(po => po.ProgrammeOfferingIdValue == programmeOfferingId, cancellationToken)
                .ConfigureAwait(false);

            if (offeringEntity == null)
                return Problem(
                    detail: $"ProgrammeOffering with ID '{programmeOfferingId}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            IQueryable<ProgrammeOfferingAssociationEntity> query = _dbContext.ProgrammeOfferingAssociations
                .AsNoTracking()
                .Include(a => a.ProgrammeOffering)
                .Include(a => a.Person)
                .Include(a => a.Organisation)
                .Where(a => a.ProgrammeOfferingEntityId == offeringEntity.Id);

            // Apply the spec's filter_query DSL
            query = ApplyFilterQuery(query);

            // Apply role filter
            if (!string.IsNullOrEmpty(role)) query = query.Where(a => a.Role != null && a.Role == role);

            // Apply state filter
            if (!string.IsNullOrEmpty(state)) query = query.Where(a => a.State != null && a.State == state);

            query = query.OrderBy(a => a.PrimaryCode);

            // resultState filters on Result.State, deserialized from the opaque ResultJson blob - no
            // portable SQL-level query exists for either provider (see design.md Decision 5), so every
            // association matching the SQL-level filters above is mapped first, then filtered by
            // resultState in memory, and only then paginated - preserving correct totalCount/paging
            // instead of paginating before this filter and under-filling pages.
            List<ProgrammeOfferingAssociationEntity> candidateEntities = await query
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            List<ProgrammeOfferingAssociation> candidates =
                [.. candidateEntities.Select(item => item.ToApiModel(consumer))];

            if (!string.IsNullOrEmpty(resultState))
                candidates = [.. candidates.Where(c => c.Result != null && c.Result.State == resultState)];

            int totalCount = candidates.Count;
            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);

            List<ProgrammeOfferingAssociation> apiModels = [.. candidates
                .Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize)];

            PagedResult<ProgrammeOfferingAssociation> result = new(apiModels, totalCount, validatedPage,
                validatedPageSize);
            return PagedResponseWithFieldSelection(result, fields);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    ///     Retrieves all groups associated with a specific programme offering.
    ///     GET /api/programme-offerings/{programmeOfferingId}/groups
    /// </summary>
    /// <param name="programmeOfferingId">The programme offering ID.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="groupType">Filter by group type.</param>
    /// <param name="q">Search term.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="filterQuery">Generic RSQL-style filter query.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{programmeOfferingId}/groups")]
    [ProducesResponseType(typeof(PagedResult<Group>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetGroupsByProgrammeOfferingId(
        string programmeOfferingId,
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
            ProgrammeOfferingEntity? offeringEntity = await _dbContext.ProgrammeOfferings
                .AsNoTracking()
                .FirstOrDefaultAsync(po => po.ProgrammeOfferingIdValue == programmeOfferingId, cancellationToken)
                .ConfigureAwait(false);

            if (offeringEntity == null)
                return Problem(
                    detail: $"ProgrammeOffering with ID '{programmeOfferingId}' not found.",
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
                .Where(g => g.ProgrammeOfferings.Any(po => po.Id == offeringEntity.Id));

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
    ///     Replaces a programme offering, or creates one at the given id if it doesn't exist yet.
    ///     PUT /programme-offerings/{id}. Upsert semantics per spec: <c>200</c> if replacing an existing
    ///     programme offering, <c>201</c> if creating a new one at the given id. Shares the same
    ///     full-representation body and FK-resolution logic as <see cref="PatchProgrammeOffering" /> - the
    ///     two are contractually identical for this resource except for the create-if-missing behaviour.
    /// </summary>
    /// <param name="id">The programme offering ID.</param>
    /// <param name="model">The full programme offering representation.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PutProgrammeOffering(
        string id,
        [FromBody] ProgrammeOffering model,
        CancellationToken cancellationToken = default)
    {
        try
        {
            ProgrammeOfferingEntity? entity = await _dbContext.ProgrammeOfferings
                .FirstOrDefaultAsync(po => po.ProgrammeOfferingIdValue == id, cancellationToken)
                .ConfigureAwait(false);

            bool isNew;
            if (entity == null)
            {
                isNew = true;
                entity = MapToEntity(model);
                entity.ProgrammeOfferingIdValue = id;
                _dbContext.ProgrammeOfferings.Add(entity);
            }
            else
            {
                isNew = false;
                UpdateEntityFromApiModel(entity, model);
            }

            if (!string.IsNullOrEmpty(model.ProgrammeId?.Value))
            {
                ProgrammeEntity? programmeEntity = await _dbContext.Programmes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.ProgrammeId == model.ProgrammeId.Value, cancellationToken)
                    .ConfigureAwait(false);
                if (programmeEntity != null) entity.ProgrammeEntityId = programmeEntity.Id;
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
    ///     Updates a programme offering from its full representation, per the spec's
    ///     <c>PATCH /programme-offerings/{id}</c> (which - unlike the offering-association endpoints -
    ///     takes the same full <c>application/json</c> body as PUT, not a narrow merge-patch). Scalar/JSON
    ///     fields go through the existing <see cref="UpdateEntityFromApiModel" />; the single-valued
    ///     relationship fields (<c>programmeId</c>, <c>organisationId</c>, <c>academicSessionId</c>) are
    ///     resolved here since that requires the async DB lookups <c>UpdateEntityFromApiModel</c> can't
    ///     perform - see the note on its use in <see cref="MapToEntity" />.
    /// </summary>
    /// <param name="id">The programme offering ID.</param>
    /// <param name="model">The full programme offering representation.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PatchProgrammeOffering(
        string id,
        [FromBody] ProgrammeOffering model,
        CancellationToken cancellationToken = default)
    {
        try
        {
            ProgrammeOfferingEntity? entity = await _dbContext.ProgrammeOfferings
                .FirstOrDefaultAsync(po => po.ProgrammeOfferingIdValue == id, cancellationToken)
                .ConfigureAwait(false);

            if (entity == null)
                return Problem(
                    detail: $"ProgrammeOffering with ID '{id}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            UpdateEntityFromApiModel(entity, model);

            if (!string.IsNullOrEmpty(model.ProgrammeId?.Value))
            {
                ProgrammeEntity? programmeEntity = await _dbContext.Programmes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.ProgrammeId == model.ProgrammeId.Value, cancellationToken)
                    .ConfigureAwait(false);
                if (programmeEntity != null) entity.ProgrammeEntityId = programmeEntity.Id;
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
