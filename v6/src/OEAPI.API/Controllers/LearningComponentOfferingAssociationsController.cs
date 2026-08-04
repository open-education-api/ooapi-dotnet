using System.Text.Json;
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
///     Controller for learning component offering association endpoints.
/// </summary>
/// <remarks>
///     The top-level <c>GET /learning-component-offering-associations</c> this class's
///     <see cref="GenericEntityController{TEntity,TApiModel}" /> base provides has no canonical-spec
///     counterpart (the spec only exposes this resource nested under a parent, or by single-item
///     <c>GET</c>) - off by default (<c>404</c>) unless the deployment opts in via
///     <c>Service:ExposeNonCanonicalListEndpoints</c>; see <see cref="NonCanonicalTopLevelListRoutes" />
///     and <c>docs/archive/DECISIONS-AND-ACTIONS.md</c>.
/// </remarks>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
[Tags("LearningComponentOfferingAssociations")]
public class LearningComponentOfferingAssociationsController(OEAPIDbContext dbContext,
    ILogger<LearningComponentOfferingAssociationsController> logger) : GenericEntityController<
    LearningComponentOfferingAssociationEntity, LearningComponentOfferingAssociation>(dbContext, logger, "Learning Component Offering Association", "learning-component-offering-associations")
{
    private readonly OEAPIDbContext _dbContext = dbContext;

    /// <summary>Retrieves a list of all learning component offering associations.</summary>
    /// <param name="primaryCode">Filter by primary code.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response for Scalar/OpenAPI doc generation -
    // TApiModel can't be referenced in an attribute on the generic base class (CS0416). Behaviour is
    // unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(PagedResult<LearningComponentOfferingAssociation>), StatusCodes.Status200OK)]
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

    /// <summary>Retrieves a single learning component offering association by ID.</summary>
    /// <param name="learningComponentOfferingAssociationId">The learning component offering association ID.</param>
    /// <param name="expand">Comma-separated list of relations to expand, e.g. <c>learningComponentOffering,person</c>.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response - see the GetAll override above.
    // Behaviour is unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(LearningComponentOfferingAssociation), StatusCodes.Status200OK)]
    [HttpGet("{learningComponentOfferingAssociationId}")]
    public override Task<IActionResult> GetById(
        string learningComponentOfferingAssociationId,
        string? expand,
        string? fields,
        string? consumer,
        CancellationToken cancellationToken = default)
    {
        return base.GetById(learningComponentOfferingAssociationId, expand, fields, consumer, cancellationToken);
    }

    /// <summary>
    ///     Finds a learning component offering association by unique identifier
    ///     (LearningComponentOfferingAssociationIdValue). Routes through
    ///     <see cref="GenericEntityController{TEntity,TApiModel}.Entities" /> (not a raw
    ///     <c>_dbContext.LearningComponentOfferingAssociations</c> query) so <see cref="ApplyIncludes" />
    ///     applies to the single-item lookup path too, not just <c>GetAll</c>.
    /// </summary>
    /// <param name="idValue">The learning component offering association ID value.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the learning component offering
    ///     association entity or null.
    /// </returns>
    protected override async Task<LearningComponentOfferingAssociationEntity?> FindByUniqueIdAsync(string idValue,
        CancellationToken cancellationToken = default)
    {
        return await Entities
            .FirstOrDefaultAsync(lcoa => lcoa.LearningComponentOfferingAssociationIdValue == idValue, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    ///     Eagerly loads LearningComponentOffering/Person so <c>MapToApiModel</c> can read them
    ///     directly from the real FK relationships. Organisation is not included -
    ///     <c>LearningComponentOfferingAssociation</c> has no <c>organisationId</c>/<c>organisation</c>
    ///     field per spec (see tier3 removal).
    /// </summary>
    protected override IQueryable<LearningComponentOfferingAssociationEntity> ApplyIncludes(
        IQueryable<LearningComponentOfferingAssociationEntity> query)
    {
        return query
            .Include(lcoa => lcoa.LearningComponentOffering)
            .Include(lcoa => lcoa.Person);
    }

    // ========================================================================
    // Required abstract method implementations
    // ========================================================================

    /// <summary>
    ///     Maps a LearningComponentOfferingAssociationEntity to a LearningComponentOfferingAssociation API model.
    /// </summary>
    /// <param name="entity">The entity to map.</param>
    /// <param name="consumer">The requested consumer, for consumer-specific data.</param>
    /// <returns>The API model.</returns>
    protected override LearningComponentOfferingAssociation MapToApiModel(
        LearningComponentOfferingAssociationEntity entity, string? consumer)
    {
        return entity.ToApiModel(consumer);
    }

    /// <summary>
    ///     Maps a LearningComponentOfferingAssociation API model to a
    ///     LearningComponentOfferingAssociationEntity. Not currently called by any live code path (see
    ///     the note on writes in <see cref="GenericEntityController{TEntity,TApiModel}" />). Note:
    ///     resolving <c>model.LearningComponentOfferingId</c>/<c>model.PersonId</c> (external string
    ///     IDs) to the entity's internal Guid FKs requires an async DB lookup that this synchronous
    ///     method can't perform - whichever explicit write action ends up calling this needs to resolve
    ///     and set those FKs itself after calling this method.
    /// </summary>
    /// <param name="model">The API model to map.</param>
    /// <returns>The entity.</returns>
    protected override LearningComponentOfferingAssociationEntity MapToEntity(
        LearningComponentOfferingAssociation model)
    {
        return model.ToEntity();
    }

    /// <summary>
    ///     Updates a LearningComponentOfferingAssociationEntity from a
    ///     LearningComponentOfferingAssociation API model. See the note on <see cref="MapToEntity" />
    ///     regarding LearningComponentOffering/Person/Organisation FK resolution.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="model">The API model with new data.</param>
    protected override void UpdateEntityFromApiModel(LearningComponentOfferingAssociationEntity entity,
        LearningComponentOfferingAssociation model)
    {
        entity.UpdateFrom(model);
    }

    /// <summary>
    ///     Gets the unique identifier for a learning component offering association entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>The identifier as string.</returns>
    protected override string GetEntityId(LearningComponentOfferingAssociationEntity entity)
    {
        return entity.LearningComponentOfferingAssociationIdValue;
    }

    // ========================================================================
    // Override methods for custom behaviour
    // ========================================================================

    /// <summary>
    ///     Applies default ordering for learning component offering associations: by primary code.
    /// </summary>
    /// <param name="query">The query to order.</param>
    /// <returns>The ordered query.</returns>
    protected override IQueryable<LearningComponentOfferingAssociationEntity> ApplyDefaultOrdering(
        IQueryable<LearningComponentOfferingAssociationEntity> query)
    {
        return query.OrderBy(lcoa => lcoa.PrimaryCode);
    }

    /// <summary>
    ///     Applies expand to a learning component offering association API model (includes related entities).
    /// </summary>
    /// <param name="apiModel">The API model to expand.</param>
    /// <param name="expand">The expand parameter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the expanded API model.</returns>
    protected override async Task<LearningComponentOfferingAssociation> ApplyExpandAsync(
        LearningComponentOfferingAssociation apiModel,
        string expand,
        CancellationToken cancellationToken = default)
    {
        string[] expandOptions = expand.Split([','], StringSplitOptions.RemoveEmptyEntries);

        foreach (string option in expandOptions)
            switch (option.Trim().ToLowerInvariant())
            {
                case "learning_component_offering":
                    if (apiModel.LearningComponentOfferingId != null && apiModel.LearningComponentOffering == null)
                    {
                        LearningComponentOfferingEntity? learningComponentOfferingEntity = await _dbContext
                            .LearningComponentOfferings
                            .AsNoTracking()
                            .Include(lco => lco.LearningComponent)
                            .Include(lco => lco.Organisation)
                            .Include(lco => lco.AcademicSession)
                            .Include(lco => lco.Rooms)
                            .Include(lco => lco.CourseOfferings)
                            .Include(lco => lco.Groups)
                            .FirstOrDefaultAsync(lco =>
                                    lco.LearningComponentOfferingIdValue ==
                                    apiModel.LearningComponentOfferingId.Value ||
                                    lco.Id.ToString() == apiModel.LearningComponentOfferingId.Value,
                                cancellationToken)
                            .ConfigureAwait(false);

                        if (learningComponentOfferingEntity != null)
                            apiModel.LearningComponentOffering = learningComponentOfferingEntity.ToApiModel(null);
                    }

                    break;

                case "person":
                    if (apiModel.PersonId != null && apiModel.Person == null)
                    {
                        PersonEntity? personEntity = await _dbContext.Persons
                            .AsNoTracking()
                            .FirstOrDefaultAsync(p =>
                                    p.PersonId == apiModel.PersonId.Value ||
                                    p.Id.ToString() == apiModel.PersonId.Value,
                                cancellationToken)
                            .ConfigureAwait(false);

                        if (personEntity != null) apiModel.Person = personEntity.ToApiModel(null);
                    }

                    break;

                case "rooms":
                    // LearningComponentOfferingAssociation has no rooms field of its own - the spec
                    // declares this expand value to populate the rooms of the association's own
                    // learningComponentOffering, composing into that nested object the same way the
                    // "learning_component_offering" case above expands it (reuses the already-expanded
                    // object if present, rather than re-fetching).
                    if (apiModel.LearningComponentOfferingId != null)
                    {
                        LearningComponentOfferingEntity? offeringEntity = await _dbContext
                            .LearningComponentOfferings
                            .AsNoTracking()
                            .Include(lco => lco.Rooms).ThenInclude(r => r.Building)
                            .FirstOrDefaultAsync(lco =>
                                    lco.LearningComponentOfferingIdValue ==
                                    apiModel.LearningComponentOfferingId.Value ||
                                    lco.Id.ToString() == apiModel.LearningComponentOfferingId.Value,
                                cancellationToken)
                            .ConfigureAwait(false);

                        if (offeringEntity != null)
                        {
                            apiModel.LearningComponentOffering ??= offeringEntity.ToApiModel(null);
                            apiModel.LearningComponentOffering.Rooms =
                                [.. offeringEntity.Rooms.Select(r => r.ToApiModel(null))];
                        }
                    }

                    break;
            }

        return apiModel;
    }

    // Create/Delete removed: the spec defines no POST or DELETE for
    // /learning-component-offering-associations.

    /// <summary>
    ///     Replaces a learning component offering association, or creates one at the given id if it
    ///     doesn't exist yet. PUT /learning-component-offering-associations/{id}. Upsert semantics per
    ///     spec: <c>200</c> if replacing an existing association, <c>201</c> if creating a new one at the
    ///     given id. Takes the full <c>LearningComponentOfferingAssociation</c> representation - a
    ///     different, wider contract than <see cref="PatchLearningComponentOfferingAssociation" />'s
    ///     narrow <c>remoteState</c>/<c>result</c> merge-patch.
    /// </summary>
    /// <param name="id">The learning component offering association ID.</param>
    /// <param name="model">The full learning component offering association representation.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PutLearningComponentOfferingAssociation(
        string id,
        [FromBody] LearningComponentOfferingAssociation model,
        CancellationToken cancellationToken = default)
    {
        try
        {
            LearningComponentOfferingAssociationEntity? entity = await _dbContext.LearningComponentOfferingAssociations
                .FirstOrDefaultAsync(lcoa => lcoa.LearningComponentOfferingAssociationIdValue == id, cancellationToken)
                .ConfigureAwait(false);

            bool isNew;
            if (entity == null)
            {
                isNew = true;
                entity = MapToEntity(model);
                entity.LearningComponentOfferingAssociationIdValue = id;
                _dbContext.LearningComponentOfferingAssociations.Add(entity);
            }
            else
            {
                isNew = false;
                UpdateEntityFromApiModel(entity, model);
            }

            if (!string.IsNullOrEmpty(model.LearningComponentOfferingId?.Value))
            {
                LearningComponentOfferingEntity? learningComponentOfferingEntity = await _dbContext
                    .LearningComponentOfferings
                    .AsNoTracking()
                    .FirstOrDefaultAsync(
                        lco => lco.LearningComponentOfferingIdValue == model.LearningComponentOfferingId.Value,
                        cancellationToken)
                    .ConfigureAwait(false);
                if (learningComponentOfferingEntity != null)
                    entity.LearningComponentOfferingEntityId = learningComponentOfferingEntity.Id;
            }

            if (!string.IsNullOrEmpty(model.PersonId?.Value))
            {
                PersonEntity? personEntity = await _dbContext.Persons
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.PersonId == model.PersonId.Value, cancellationToken)
                    .ConfigureAwait(false);
                if (personEntity != null) entity.PersonEntityId = personEntity.Id;
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
    ///     Applies a JSON Merge Patch (RFC 7396) to a learning component offering association's
    ///     <c>remoteState</c>/<c>result</c> - the only fields the spec allows this endpoint to change.
    ///     PUT (full update) is separately implemented above (see <see cref="PutLearningComponentOfferingAssociation" />).
    /// </summary>
    /// <param name="id">The learning component offering association ID.</param>
    /// <param name="request">The merge-patch request body.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPatch("{id}")]
    [Consumes("application/merge-patch+json")]
    [ProducesResponseType(typeof(AssociationWriteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PatchLearningComponentOfferingAssociation(
        string id,
        [FromBody] AssociationPatchRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            LearningComponentOfferingAssociationEntity? entity = await _dbContext.LearningComponentOfferingAssociations
                .FirstOrDefaultAsync(lcoa => lcoa.LearningComponentOfferingAssociationIdValue == id, cancellationToken)
                .ConfigureAwait(false);

            if (entity == null)
                return Problem(
                    detail: $"LearningComponentOfferingAssociation with ID '{id}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            if (request.RemoteState != null) entity.RemoteState = request.RemoteState;

            if (request.Result != null) entity.ResultJson = JsonSerializer.Serialize(request.Result);

            entity.ModifiedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Ok(new AssociationWriteResponse
            {
                AssociationId = entity.LearningComponentOfferingAssociationIdValue,
                Message =
                [
                    new LanguageTypedString { Language = "en-GB", Value = "The association has been updated." }
                ],
                State = entity.State
            });
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}
