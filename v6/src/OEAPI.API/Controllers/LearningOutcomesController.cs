using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Infrastructure.Data.Context;
using OEAPI.Infrastructure.Data.Entities;
using OEAPI.Infrastructure.Data.Mapping;
using OEAPI.Infrastructure.Query;
using OEAPI.Infrastructure.Query.Extensions;

namespace OEAPI.API.Controllers;

/// <summary>
///     Controller for learning outcome endpoints.
/// </summary>
/// <remarks>
///     Initializes a new instance of the LearningOutcomesController class.
/// </remarks>
/// <param name="dbContext">The database context.</param>
/// <param name="logger">The logger.</param>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
[Tags("LearningOutcomes")]
public class LearningOutcomesController(OEAPIDbContext dbContext, ILogger<LearningOutcomesController> logger) : GenericEntityController<LearningOutcomeEntity, LearningOutcome>(dbContext, logger, "Learning Outcome", "learning-outcomes")
{
    private readonly OEAPIDbContext _dbContext = dbContext;

    /// <summary>Retrieves a list of all learning outcomes.</summary>
    /// <param name="primaryCode">Filter by primary code.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response for Scalar/OpenAPI doc generation -
    // TApiModel can't be referenced in an attribute on the generic base class (CS0416). Behaviour is
    // unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(PagedResult<LearningOutcome>), StatusCodes.Status200OK)]
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

    /// <summary>Retrieves a single learning outcome by ID.</summary>
    /// <param name="learningOutcomeId">The learning outcome ID.</param>
    /// <param name="expand">Comma-separated list of relations to expand, e.g. <c>parents,children</c>.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response - see the GetAll override above.
    // Behaviour is unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(LearningOutcome), StatusCodes.Status200OK)]
    [HttpGet("{learningOutcomeId}")]
    public override Task<IActionResult> GetById(
        string learningOutcomeId,
        string? expand,
        string? fields,
        string? consumer,
        CancellationToken cancellationToken = default)
    {
        return base.GetById(learningOutcomeId, expand, fields, consumer, cancellationToken);
    }

    /// <summary>
    ///     Eagerly loads Organisation/Parents/Children so <c>MapToApiModel</c> can read them directly -
    ///     see <see cref="GenericEntityController{TEntity,TApiModel}.ApplyIncludes" /> for why. Also
    ///     means the base class's default <c>FindByUniqueIdAsync</c> (which matches on the
    ///     <c>LearningOutcomeId</c> property by convention) can be used as-is, so no override is needed
    ///     here anymore.
    /// </summary>
    protected override IQueryable<LearningOutcomeEntity> ApplyIncludes(IQueryable<LearningOutcomeEntity> query)
    {
        return query.IncludeHierarchy();
    }

    // ========================================================================
    // Required abstract method implementations
    // ========================================================================

    /// <summary>
    ///     Maps a LearningOutcomeEntity to a LearningOutcome API model.
    /// </summary>
    /// <param name="entity">The entity to map.</param>
    /// <param name="consumer">The requested consumer, for consumer-specific data.</param>
    /// <returns>The API model.</returns>
    protected override LearningOutcome MapToApiModel(LearningOutcomeEntity entity, string? consumer)
    {
        return entity.ToApiModel(consumer);
    }

    /// <summary>
    ///     Maps a LearningOutcome API model to a LearningOutcomeEntity. Not currently called by any
    ///     live code path (see the note on writes in <see cref="GenericEntityController{TEntity,TApiModel}" />).
    ///     Note: resolving <c>model.ParentIds</c> (external string IDs) to real
    ///     <see cref="LearningOutcomeEntity.Parents" /> relationships requires an async DB lookup that
    ///     this synchronous method can't perform - whichever explicit write action ends up calling this
    ///     needs to resolve and attach those relationships itself after calling this method.
    /// </summary>
    /// <param name="model">The API model to map.</param>
    /// <returns>The entity.</returns>
    protected override LearningOutcomeEntity MapToEntity(LearningOutcome model)
    {
        return model.ToEntity();
    }

    /// <summary>
    ///     Updates a LearningOutcomeEntity from a LearningOutcome API model.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="model">The API model with new data.</param>
    protected override void UpdateEntityFromApiModel(LearningOutcomeEntity entity, LearningOutcome model)
    {
        entity.UpdateFrom(model);
    }

    /// <summary>
    ///     Gets the unique identifier for a learning outcome entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>The identifier as string.</returns>
    protected override string GetEntityId(LearningOutcomeEntity entity)
    {
        return entity.LearningOutcomeId;
    }

    // ========================================================================
    // Override methods for custom behaviour
    // ========================================================================

    /// <summary>
    ///     Applies default ordering for learning outcomes: by primary code, then by name.
    /// </summary>
    /// <param name="query">The query to order.</param>
    /// <returns>The ordered query.</returns>
    protected override IQueryable<LearningOutcomeEntity> ApplyDefaultOrdering(IQueryable<LearningOutcomeEntity> query)
    {
        return query.OrderBy(lo => lo.PrimaryCode).ThenBy(lo => lo.NameJson);
    }

    /// <summary>
    ///     Applies the spec's <c>q</c> (search), <c>since</c> and <c>until</c> filters to a
    ///     <c>GetAll</c> query - not formal parameters, since <c>GetAll</c>'s signature is fixed by
    ///     the generic base controller (see
    ///     <see cref="GenericEntityController{TEntity,TApiModel}.ApplyEntityFilters" />; mirrors
    ///     <c>PersonsController.ApplyEntityFilters</c>'s <c>q</c> pattern).
    /// </summary>
    /// <remarks>
    ///     <c>LearningOutcomeCollection.yaml</c>'s <c>since</c>/<c>until</c> descriptions are
    ///     boilerplate copy-pasted from an offering-style endpoint ("filter by minimum start moment
    ///     for the corresponding academic session") that doesn't literally apply here - a
    ///     <see cref="LearningOutcome" /> has no academic-session relationship. Best-effort
    ///     spec-conformant mapping (see design.md Decision 4): filter on
    ///     <see cref="LearningOutcomeEntity.ValidFrom" />/<see cref="LearningOutcomeEntity.ValidTo" />
    ///     instead, the closest existing date-range pair on this entity - rows with a null
    ///     <c>ValidFrom</c>/<c>ValidTo</c> are kept rather than excluded (an unbounded validity range
    ///     shouldn't be treated as failing a date filter it never declared a value for).
    /// </remarks>
    protected override IQueryable<LearningOutcomeEntity> ApplyEntityFilters(IQueryable<LearningOutcomeEntity> query)
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
            query = query.Where(lo =>
                lo.NameJson.ToLower().Contains(searchTerm) ||
                (lo.Abbreviation != null && lo.Abbreviation.ToLower().Contains(searchTerm)) ||
                (lo.DescriptionJson != null && lo.DescriptionJson.ToLower().Contains(searchTerm)));
        }

        // Spec: since - kept rows whose ValidFrom is on/after the given value, or whose ValidFrom is
        // null (no lower bound declared, so it can't fail a "starts no earlier than" filter).
        string? sinceValue = GetQueryValue("since");
        if (!string.IsNullOrEmpty(sinceValue))
            query = query.Where(lo => lo.ValidFrom == null || lo.ValidFrom.CompareTo(sinceValue) >= 0);

        // Spec: until - kept rows whose ValidTo is on/before the given value, or whose ValidTo is
        // null (no upper bound declared, so it can't fail an "ends no later than" filter).
        string? untilValue = GetQueryValue("until");
        if (!string.IsNullOrEmpty(untilValue))
            query = query.Where(lo => lo.ValidTo == null || lo.ValidTo.CompareTo(untilValue) <= 0);

        return query;
    }

    // Create/Update/Delete removed: the spec defines no write operations at all for
    // /learning-outcomes.

    /// <summary>
    ///     Applies expand to a LearningOutcome API model (includes related entities).
    /// </summary>
    /// <param name="apiModel">The LearningOutcome API model to expand.</param>
    /// <param name="expand">The expand parameter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the expanded LearningOutcome API
    ///     model.
    /// </returns>
    protected override async Task<LearningOutcome> ApplyExpandAsync(
        LearningOutcome apiModel,
        string expand,
        CancellationToken cancellationToken = default)
    {
        string[] expands = expand.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        // Cache the outcome entity (with Parents/Children eager-loaded via ApplyIncludes) for reuse
        // across expand options.
        LearningOutcomeEntity? outcomeEntity = null;

        foreach (string expandOption in expands)
            switch (expandOption.ToLower())
            {
                case "parents":
                    outcomeEntity ??= await Entities
                        .FirstOrDefaultAsync(lo => lo.LearningOutcomeId == apiModel.LearningOutcomeId,
                            cancellationToken)
                        .ConfigureAwait(false);

                    if (outcomeEntity != null && outcomeEntity.Parents.Count > 0)
                        apiModel.Parents = [.. outcomeEntity.Parents.Select(e => MapToApiModel(e, null))];
                    break;

                case "children":
                    outcomeEntity ??= await Entities
                        .FirstOrDefaultAsync(lo => lo.LearningOutcomeId == apiModel.LearningOutcomeId,
                            cancellationToken)
                        .ConfigureAwait(false);

                    if (outcomeEntity != null && outcomeEntity.Children.Count > 0)
                        apiModel.Children = [.. outcomeEntity.Children.Select(e => MapToApiModel(e, null))];
                    break;

                // "organisation" is declared as an expandable value on this endpoint's own path (see
                // LearningOutcomeInstance.yaml), but LearningOutcome.yaml itself declares no
                // organisationId/organisation field at all - a spec self-contradiction, discovered
                // while implementing the tier3 removal of this fabricated field. Nothing to expand
                // into, so expand=organisation is now silently a no-op like any other unrecognised
                // expand value.
            }

        return apiModel;
    }
}
