using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OEAPI.API.Middleware;
using OEAPI.Core.Interfaces;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Infrastructure.Data.Context;
using OEAPI.Infrastructure.Data.Entities;
using OEAPI.Infrastructure.Data.Mapping;
using OEAPI.Infrastructure.Query;
using OEAPI.Infrastructure.Query.Extensions;

namespace OEAPI.API.Controllers;

/// <summary>
///     Controller for programme offering association endpoints.
/// </summary>
/// <remarks>
///     The top-level <c>GET /programme-offering-associations</c> this class's
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
[Tags("ProgrammeOfferingAssociations")]
public class ProgrammeOfferingAssociationsController(OEAPIDbContext dbContext,
    ILogger<ProgrammeOfferingAssociationsController> logger, ICurrentPersonProvider currentPersonProvider) : GenericEntityController<ProgrammeOfferingAssociationEntity,
    ProgrammeOfferingAssociation>(dbContext, logger, "Programme Offering Association", "programme-offering-associations")
{
    private readonly ICurrentPersonProvider _currentPersonProvider = currentPersonProvider;
    private readonly OEAPIDbContext _dbContext = dbContext;

    /// <summary>Retrieves a list of all programme offering associations.</summary>
    /// <param name="primaryCode">Filter by primary code.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response for Scalar/OpenAPI doc generation -
    // TApiModel can't be referenced in an attribute on the generic base class (CS0416). Behaviour is
    // unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(PagedResult<ProgrammeOfferingAssociation>), StatusCodes.Status200OK)]
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

    /// <summary>Retrieves a single programme offering association by ID.</summary>
    /// <param name="programmeOfferingAssociationId">The programme offering association ID.</param>
    /// <param name="expand">Comma-separated list of relations to expand, e.g. <c>programmeOffering,person</c>.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response - see the GetAll override above.
    // Behaviour is unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(ProgrammeOfferingAssociation), StatusCodes.Status200OK)]
    [HttpGet("{programmeOfferingAssociationId}")]
    public override Task<IActionResult> GetById(
        string programmeOfferingAssociationId,
        string? expand,
        string? fields,
        string? consumer,
        CancellationToken cancellationToken = default)
    {
        return base.GetById(programmeOfferingAssociationId, expand, fields, consumer, cancellationToken);
    }

    /// <summary>
    ///     Finds a programme offering association by unique identifier (ProgrammeOfferingAssociationIdValue).
    ///     Routes through <see cref="GenericEntityController{TEntity,TApiModel}.Entities" /> (not a raw
    ///     <c>_dbContext.ProgrammeOfferingAssociations</c> query) so <see cref="ApplyIncludes" /> applies
    ///     to the single-item lookup path too, not just <c>GetAll</c>.
    /// </summary>
    /// <param name="idValue">The programme offering association ID value.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the programme offering association
    ///     entity or null.
    /// </returns>
    protected override async Task<ProgrammeOfferingAssociationEntity?> FindByUniqueIdAsync(string idValue,
        CancellationToken cancellationToken = default)
    {
        return await Entities
            .FirstOrDefaultAsync(poa => poa.ProgrammeOfferingAssociationIdValue == idValue, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    ///     Eagerly loads ProgrammeOffering/Person so <c>MapToApiModel</c> can read them directly from
    ///     the real FK relationships. Organisation is not included -
    ///     <c>ProgrammeOfferingAssociation</c> has no <c>organisationId</c>/<c>organisation</c> field
    ///     per spec (see tier3 removal).
    /// </summary>
    protected override IQueryable<ProgrammeOfferingAssociationEntity> ApplyIncludes(
        IQueryable<ProgrammeOfferingAssociationEntity> query)
    {
        return query
            .Include(poa => poa.ProgrammeOffering)
            .Include(poa => poa.Person);
    }

    // ========================================================================
    // Required abstract method implementations
    // ========================================================================

    /// <summary>
    ///     Maps a ProgrammeOfferingAssociationEntity to a ProgrammeOfferingAssociation API model.
    /// </summary>
    /// <param name="entity">The entity to map.</param>
    /// <param name="consumer">The requested consumer, for consumer-specific data.</param>
    /// <returns>The API model.</returns>
    protected override ProgrammeOfferingAssociation MapToApiModel(ProgrammeOfferingAssociationEntity entity,
        string? consumer)
    {
        return entity.ToApiModel(consumer);
    }

    /// <summary>
    ///     Maps a ProgrammeOfferingAssociation API model to a ProgrammeOfferingAssociationEntity. Not
    ///     currently called by any live code path (see the note on writes in
    ///     <see cref="GenericEntityController{TEntity,TApiModel}" />). Note: resolving
    ///     <c>model.ProgrammeOfferingId</c>/<c>model.PersonId</c> (external string IDs) to the entity's
    ///     internal Guid FKs requires an async DB lookup that this synchronous method can't perform -
    ///     whichever explicit write action ends up calling this needs to resolve and set those FKs
    ///     itself after calling this method.
    /// </summary>
    /// <param name="model">The API model to map.</param>
    /// <returns>The entity.</returns>
    protected override ProgrammeOfferingAssociationEntity MapToEntity(ProgrammeOfferingAssociation model)
    {
        return model.ToEntity();
    }

    /// <summary>
    ///     Updates a ProgrammeOfferingAssociationEntity from a ProgrammeOfferingAssociation API model.
    ///     See the note on <see cref="MapToEntity" /> regarding ProgrammeOffering/Person/Organisation FK
    ///     resolution.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="model">The API model with new data.</param>
    protected override void UpdateEntityFromApiModel(ProgrammeOfferingAssociationEntity entity,
        ProgrammeOfferingAssociation model)
    {
        entity.UpdateFrom(model);
    }

    /// <summary>
    ///     Gets the unique identifier for a programme offering association entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>The identifier as string.</returns>
    protected override string GetEntityId(ProgrammeOfferingAssociationEntity entity)
    {
        return entity.ProgrammeOfferingAssociationIdValue;
    }

    // ========================================================================
    // Override methods for custom behaviour
    // ========================================================================

    /// <summary>
    ///     Applies default ordering for programme offering associations: by primary code.
    /// </summary>
    /// <param name="query">The query to order.</param>
    /// <returns>The ordered query.</returns>
    protected override IQueryable<ProgrammeOfferingAssociationEntity> ApplyDefaultOrdering(
        IQueryable<ProgrammeOfferingAssociationEntity> query)
    {
        return query.OrderBy(poa => poa.PrimaryCode);
    }

    /// <summary>
    ///     Applies expand to a programme offering association API model (includes related entities).
    /// </summary>
    /// <param name="apiModel">The API model to expand.</param>
    /// <param name="expand">The expand parameter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the expanded API model.</returns>
    protected override async Task<ProgrammeOfferingAssociation> ApplyExpandAsync(
        ProgrammeOfferingAssociation apiModel,
        string expand,
        CancellationToken cancellationToken = default)
    {
        string[] expandOptions = expand.Split([','], StringSplitOptions.RemoveEmptyEntries);

        foreach (string option in expandOptions)
            switch (option.Trim().ToLowerInvariant())
            {
                case "programme_offering":
                    if (apiModel.ProgrammeOfferingId != null && apiModel.ProgrammeOffering == null)
                    {
                        ProgrammeOfferingEntity? programmeOfferingEntity = await _dbContext.ProgrammeOfferings
                            .AsNoTracking()
                            .Include(po => po.Programme)
                            .Include(po => po.Organisation)
                            .Include(po => po.AcademicSession)
                            .Include(po => po.Groups)
                            .FirstOrDefaultAsync(po =>
                                    po.ProgrammeOfferingIdValue == apiModel.ProgrammeOfferingId.Value ||
                                    po.Id.ToString() == apiModel.ProgrammeOfferingId.Value,
                                cancellationToken)
                            .ConfigureAwait(false);

                        if (programmeOfferingEntity != null)
                            apiModel.ProgrammeOffering = programmeOfferingEntity.ToApiModel(null);
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
            }

        return apiModel;
    }

    /// <summary>
    ///     Enrols the currently authenticated caller into a programme offering. POST
    ///     /programme-offering-associations/external/me. The person is never taken from the request
    ///     body - per spec, it is obtained "from a well known endpoint" tied to the caller's
    ///     authenticated identity, resolved here via <see cref="ICurrentPersonProvider" /> (see that
    ///     interface's docs for why this is deployment specific). Returns <c>401</c> if no person can be
    ///     resolved for the caller (mirrors <c>GET /persons/me</c>), <c>404</c> if the referenced
    ///     programme offering or issuer organisation don't exist (this reference implementation does not
    ///     auto-provision organisations from request bodies), otherwise <c>201</c> with the new
    ///     association's id/state.
    /// </summary>
    /// <param name="request">The enrolment request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPost("external/me")]
    [ProducesResponseType(typeof(AssociationWriteResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostProgrammeOfferingAssociationExternalMe(
        [FromBody] ProgrammeOfferingAssociationExternalMeRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            AuthenticationResult? authResult = HttpContext.GetAuthenticationResult();
            string? personId = await _currentPersonProvider.GetCurrentPersonIdAsync(authResult, cancellationToken)
                .ConfigureAwait(false);
            if (string.IsNullOrEmpty(personId))
                return Problem(
                    detail: "No authenticated user context is available for this request.",
                    statusCode: StatusCodes.Status401Unauthorized,
                    title: "Unauthorized");

            PersonEntity? personEntity = await _dbContext.Persons
                .FirstOrDefaultAsync(p => p.PersonId == personId, cancellationToken)
                .ConfigureAwait(false);
            if (personEntity == null)
                return Problem(
                    detail: $"No person found for the authenticated user '{personId}'.",
                    statusCode: StatusCodes.Status401Unauthorized,
                    title: "Unauthorized");

            string? programmeOfferingIdValue = request.ProgrammeOfferingId?.Value ??
                                               request.ProgrammeOffering?.ProgrammeOfferingIdValue;
            if (string.IsNullOrEmpty(programmeOfferingIdValue))
                return Problem(
                    detail: "Either programmeOfferingId or programmeOffering must be supplied.",
                    statusCode: StatusCodes.Status400BadRequest,
                    title: "Bad Request");

            ProgrammeOfferingEntity? programmeOfferingEntity = await _dbContext.ProgrammeOfferings
                .FirstOrDefaultAsync(po => po.ProgrammeOfferingIdValue == programmeOfferingIdValue, cancellationToken)
                .ConfigureAwait(false);
            if (programmeOfferingEntity == null)
                return Problem(
                    detail: $"ProgrammeOffering with ID '{programmeOfferingIdValue}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            string issuerIdValue = request.Issuer?.OrganisationId ?? string.Empty;
            OrganisationEntity? organisationEntity = string.IsNullOrEmpty(issuerIdValue)
                ? null
                : await _dbContext.Organisations
                    .FirstOrDefaultAsync(o => o.OrganisationId == issuerIdValue, cancellationToken)
                    .ConfigureAwait(false);
            if (organisationEntity == null)
                return Problem(
                    detail: $"Issuer organisation with ID '{issuerIdValue}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            ProgrammeOfferingAssociationEntity entity = new()
            {
                ProgrammeOfferingAssociationIdValue = Guid.NewGuid().ToString(),
                PrimaryCodeType = request.PrimaryCode?.CodeType ?? string.Empty,
                PrimaryCode = request.PrimaryCode?.Code ?? string.Empty,
                Role = request.Role,
                StartDateTime = request.StartDateTime,
                ExpectedEndDateTime = request.ExpectedEndDateTime,
                ActualEndDateTime = request.ActualEndDateTime,
                State = request.State,
                RemoteState = request.RemoteState,
                ResultJson = request.Result != null ? JsonSerializer.Serialize(request.Result) : null,
                OtherCodes =
                    request.OtherCodes?.Select(oc => new OtherCodeEntity { CodeType = oc.CodeType, Code = oc.Code })
                        .ToList() ?? [],
                ConsumerJson = request.Consumer != null ? JsonSerializer.Serialize(request.Consumer) : null,
                ExtJson = request.Ext != null ? JsonSerializer.Serialize(request.Ext) : null,
                ProgrammeOfferingEntityId = programmeOfferingEntity.Id,
                PersonEntityId = personEntity.Id,
                OrganisationEntityId = organisationEntity.Id,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _dbContext.ProgrammeOfferingAssociations.Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return StatusCode(StatusCodes.Status201Created, new AssociationWriteResponse
            {
                AssociationId = entity.ProgrammeOfferingAssociationIdValue,
                Message =
                [
                    new LanguageTypedString { Language = "en-GB", Value = "Enrolment successful." }
                ],
                State = entity.State
            });
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    ///     Replaces a programme offering association, or creates one at the given id if it doesn't
    ///     exist yet. PUT /programme-offering-associations/{id}. Upsert semantics per spec: <c>200</c> if
    ///     replacing an existing association, <c>201</c> if creating a new one at the given id. Takes the
    ///     full <c>ProgrammeOfferingAssociation</c> representation - a different, wider contract than
    ///     <see cref="PatchProgrammeOfferingAssociation" />'s narrow <c>remoteState</c>/<c>result</c>
    ///     merge-patch.
    /// </summary>
    /// <param name="id">The programme offering association ID.</param>
    /// <param name="model">The full programme offering association representation.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PutProgrammeOfferingAssociation(
        string id,
        [FromBody] ProgrammeOfferingAssociation model,
        CancellationToken cancellationToken = default)
    {
        try
        {
            ProgrammeOfferingAssociationEntity? entity = await _dbContext.ProgrammeOfferingAssociations
                .FirstOrDefaultAsync(poa => poa.ProgrammeOfferingAssociationIdValue == id, cancellationToken)
                .ConfigureAwait(false);

            bool isNew;
            if (entity == null)
            {
                isNew = true;
                entity = MapToEntity(model);
                entity.ProgrammeOfferingAssociationIdValue = id;
                _dbContext.ProgrammeOfferingAssociations.Add(entity);
            }
            else
            {
                isNew = false;
                UpdateEntityFromApiModel(entity, model);
            }

            if (!string.IsNullOrEmpty(model.ProgrammeOfferingId?.Value))
            {
                ProgrammeOfferingEntity? programmeOfferingEntity = await _dbContext.ProgrammeOfferings
                    .AsNoTracking()
                    .FirstOrDefaultAsync(po => po.ProgrammeOfferingIdValue == model.ProgrammeOfferingId.Value,
                        cancellationToken)
                    .ConfigureAwait(false);
                if (programmeOfferingEntity != null) entity.ProgrammeOfferingEntityId = programmeOfferingEntity.Id;
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
    ///     Applies a JSON Merge Patch (RFC 7396) to a programme offering association's
    ///     <c>remoteState</c>/<c>result</c> - the only fields the spec allows this endpoint to change.
    ///     PUT (full update) is separately implemented above (see <see cref="PutProgrammeOfferingAssociation" />).
    /// </summary>
    /// <param name="id">The programme offering association ID.</param>
    /// <param name="request">The merge-patch request body.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPatch("{id}")]
    [Consumes("application/merge-patch+json")]
    [ProducesResponseType(typeof(AssociationWriteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PatchProgrammeOfferingAssociation(
        string id,
        [FromBody] AssociationPatchRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            ProgrammeOfferingAssociationEntity? entity = await _dbContext.ProgrammeOfferingAssociations
                .FirstOrDefaultAsync(poa => poa.ProgrammeOfferingAssociationIdValue == id, cancellationToken)
                .ConfigureAwait(false);

            if (entity == null)
                return Problem(
                    detail: $"ProgrammeOfferingAssociation with ID '{id}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            if (request.RemoteState != null) entity.RemoteState = request.RemoteState;

            if (request.Result != null) entity.ResultJson = JsonSerializer.Serialize(request.Result);

            entity.ModifiedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Ok(new AssociationWriteResponse
            {
                AssociationId = entity.ProgrammeOfferingAssociationIdValue,
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
