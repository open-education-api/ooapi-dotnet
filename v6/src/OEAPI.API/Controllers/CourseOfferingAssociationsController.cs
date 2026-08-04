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
///     Controller for course offering association endpoints.
/// </summary>
/// <remarks>
///     The top-level <c>GET /course-offering-associations</c> this class's
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
[Tags("CourseOfferingAssociations")]
public class
    CourseOfferingAssociationsController(OEAPIDbContext dbContext,
    ILogger<CourseOfferingAssociationsController> logger, ICurrentPersonProvider currentPersonProvider) : GenericEntityController<CourseOfferingAssociationEntity,
    CourseOfferingAssociation>(dbContext, logger, "Course Offering Association", "course-offering-associations")
{
    private readonly ICurrentPersonProvider _currentPersonProvider = currentPersonProvider;
    private readonly OEAPIDbContext _dbContext = dbContext;

    /// <summary>Retrieves a list of all course offering associations.</summary>
    /// <param name="primaryCode">Filter by primary code.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response for Scalar/OpenAPI doc generation -
    // TApiModel can't be referenced in an attribute on the generic base class (CS0416). Behaviour is
    // unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(PagedResult<CourseOfferingAssociation>), StatusCodes.Status200OK)]
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

    /// <summary>Retrieves a single course offering association by ID.</summary>
    /// <param name="courseOfferingAssociationId">The course offering association ID.</param>
    /// <param name="expand">Comma-separated list of relations to expand, e.g. <c>courseOffering,person</c>.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response - see the GetAll override above.
    // Behaviour is unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(CourseOfferingAssociation), StatusCodes.Status200OK)]
    [HttpGet("{courseOfferingAssociationId}")]
    public override Task<IActionResult> GetById(
        string courseOfferingAssociationId,
        string? expand,
        string? fields,
        string? consumer,
        CancellationToken cancellationToken = default)
    {
        return base.GetById(courseOfferingAssociationId, expand, fields, consumer, cancellationToken);
    }

    /// <summary>
    ///     Finds a course offering association by unique identifier (CourseOfferingAssociationIdValue).
    ///     Routes through <see cref="GenericEntityController{TEntity,TApiModel}.Entities" /> (not a raw
    ///     <c>_dbContext.CourseOfferingAssociations</c> query) so <see cref="ApplyIncludes" /> applies to
    ///     the single-item lookup path too, not just <c>GetAll</c>.
    /// </summary>
    /// <param name="idValue">The course offering association ID value.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the course offering association
    ///     entity or null.
    /// </returns>
    protected override async Task<CourseOfferingAssociationEntity?> FindByUniqueIdAsync(string idValue,
        CancellationToken cancellationToken = default)
    {
        return await Entities
            .FirstOrDefaultAsync(coa => coa.CourseOfferingAssociationIdValue == idValue, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    ///     Eagerly loads CourseOffering/Person so <c>MapToApiModel</c> can read them directly from the
    ///     real FK relationships. Organisation is not included - <c>CourseOfferingAssociation</c> has
    ///     no <c>organisationId</c>/<c>organisation</c> field per spec (see tier3 removal).
    /// </summary>
    protected override IQueryable<CourseOfferingAssociationEntity> ApplyIncludes(
        IQueryable<CourseOfferingAssociationEntity> query)
    {
        return query
            .Include(coa => coa.CourseOffering)
            .Include(coa => coa.Person);
    }

    // ========================================================================
    // Required abstract method implementations
    // ========================================================================

    /// <summary>
    ///     Maps a CourseOfferingAssociationEntity to a CourseOfferingAssociation API model.
    /// </summary>
    /// <param name="entity">The entity to map.</param>
    /// <param name="consumer">The requested consumer, for consumer-specific data.</param>
    /// <returns>The API model.</returns>
    protected override CourseOfferingAssociation MapToApiModel(CourseOfferingAssociationEntity entity, string? consumer)
    {
        return entity.ToApiModel(consumer);
    }

    /// <summary>
    ///     Maps a CourseOfferingAssociation API model to a CourseOfferingAssociationEntity. Not
    ///     currently called by any live code path (see the note on writes in
    ///     <see cref="GenericEntityController{TEntity,TApiModel}" />). Note: resolving
    ///     <c>model.CourseOfferingId</c>/<c>model.PersonId</c> (external string IDs) to the entity's
    ///     internal Guid FKs requires an async DB lookup that this synchronous method can't perform -
    ///     whichever explicit write action ends up calling this needs to resolve and set those FKs
    ///     itself after calling this method.
    /// </summary>
    /// <param name="model">The API model to map.</param>
    /// <returns>The entity.</returns>
    protected override CourseOfferingAssociationEntity MapToEntity(CourseOfferingAssociation model)
    {
        return model.ToEntity();
    }

    /// <summary>
    ///     Updates a CourseOfferingAssociationEntity from a CourseOfferingAssociation API model. See the
    ///     note on <see cref="MapToEntity" /> regarding CourseOffering/Person/Organisation FK resolution.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="model">The API model with new data.</param>
    protected override void UpdateEntityFromApiModel(CourseOfferingAssociationEntity entity,
        CourseOfferingAssociation model)
    {
        entity.UpdateFrom(model);
    }

    /// <summary>
    ///     Gets the unique identifier for a course offering association entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>The identifier as string.</returns>
    protected override string GetEntityId(CourseOfferingAssociationEntity entity)
    {
        return entity.CourseOfferingAssociationIdValue;
    }

    // ========================================================================
    // Override methods for custom behaviour
    // ========================================================================

    /// <summary>
    ///     Applies default ordering for course offering associations: by primary code.
    /// </summary>
    /// <param name="query">The query to order.</param>
    /// <returns>The ordered query.</returns>
    protected override IQueryable<CourseOfferingAssociationEntity> ApplyDefaultOrdering(
        IQueryable<CourseOfferingAssociationEntity> query)
    {
        return query.OrderBy(coa => coa.PrimaryCode);
    }

    /// <summary>
    ///     Applies expand to a course offering association API model (includes related entities).
    /// </summary>
    /// <param name="apiModel">The API model to expand.</param>
    /// <param name="expand">The expand parameter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the expanded API model.</returns>
    protected override async Task<CourseOfferingAssociation> ApplyExpandAsync(
        CourseOfferingAssociation apiModel,
        string expand,
        CancellationToken cancellationToken = default)
    {
        string[] expandOptions = expand.Split([','], StringSplitOptions.RemoveEmptyEntries);

        foreach (string option in expandOptions)
            switch (option.Trim().ToLowerInvariant())
            {
                case "course_offering":
                    if (apiModel.CourseOfferingId != null && apiModel.CourseOffering == null)
                    {
                        CourseOfferingEntity? courseOfferingEntity = await _dbContext.CourseOfferings
                            .AsNoTracking()
                            .Include(co => co.Groups)
                            .Include(co => co.ProgrammeOfferings)
                            .Include(co => co.Course)
                            .Include(co => co.Organisation)
                            .Include(co => co.AcademicSession)
                            .FirstOrDefaultAsync(co =>
                                    co.CourseOfferingId == apiModel.CourseOfferingId.Value ||
                                    co.Id.ToString() == apiModel.CourseOfferingId.Value,
                                cancellationToken)
                            .ConfigureAwait(false);

                        if (courseOfferingEntity != null)
                            apiModel.CourseOffering = courseOfferingEntity.ToApiModel(null);
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

                case "academic_session":
                    // CourseOfferingAssociation itself has no academicSession field on this canonical
                    // endpoint (that field only ever appears on the separate
                    // /persons/{id}/course-offering-associations nested response - see
                    // fix-nested-endpoint-spec-param-gaps's design.md and the regression guard in
                    // NestedEndpointSpecParamGapsConformanceTests). Here, CourseOfferingAssociationInstance.yaml
                    // still declares academic_session as an expand value even though the response schema
                    // has no top-level property for it - it composes into the nested
                    // courseOffering.academicSession field instead, so courseOffering must also be
                    // populated for this to have anywhere to attach to.
                    if (apiModel.CourseOfferingId != null)
                    {
                        CourseOfferingEntity? courseOfferingEntity = await _dbContext.CourseOfferings
                            .AsNoTracking()
                            .Include(co => co.Groups)
                            .Include(co => co.ProgrammeOfferings)
                            .Include(co => co.Course)
                            .Include(co => co.Organisation)
                            .Include(co => co.AcademicSession)
                            .FirstOrDefaultAsync(co =>
                                    co.CourseOfferingId == apiModel.CourseOfferingId.Value ||
                                    co.Id.ToString() == apiModel.CourseOfferingId.Value,
                                cancellationToken)
                            .ConfigureAwait(false);

                        if (courseOfferingEntity != null)
                        {
                            apiModel.CourseOffering ??= courseOfferingEntity.ToApiModel(null);
                            if (courseOfferingEntity.AcademicSession != null)
                                apiModel.CourseOffering.AcademicSession =
                                    courseOfferingEntity.AcademicSession.ToApiModel(null);
                        }
                    }

                    break;
            }

        return apiModel;
    }

    // Delete removed: the spec defines no DELETE for /course-offering-associations.
    // Create (generic POST) removed: the spec's only POST for this resource is the distinct
    // /course-offering-associations/external/me implemented below.

    /// <summary>
    ///     Enrols the currently authenticated caller into a course offering. POST
    ///     /course-offering-associations/external/me. The person is never taken from the request body -
    ///     per spec, it is obtained "from a well known endpoint" tied to the caller's authenticated
    ///     identity, resolved here via <see cref="ICurrentPersonProvider" /> (see that interface's docs
    ///     for why this is deployment specific). Returns <c>401</c> if no person can be resolved for the
    ///     caller (mirrors <c>GET /persons/me</c>), <c>404</c> if the referenced course offering or
    ///     issuer organisation don't exist (this reference implementation does not auto-provision
    ///     organisations from request bodies), otherwise <c>201</c> with the new association's id/state.
    /// </summary>
    /// <param name="request">The enrolment request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPost("external/me")]
    [ProducesResponseType(typeof(AssociationWriteResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostCourseOfferingAssociationExternalMe(
        [FromBody] CourseOfferingAssociationExternalMeRequest request,
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

            string? courseOfferingIdValue =
                request.CourseOfferingId?.Value ?? request.CourseOffering?.CourseOfferingIdValue;
            if (string.IsNullOrEmpty(courseOfferingIdValue))
                return Problem(
                    detail: "Either courseOfferingId or courseOffering must be supplied.",
                    statusCode: StatusCodes.Status400BadRequest,
                    title: "Bad Request");

            CourseOfferingEntity? courseOfferingEntity = await _dbContext.CourseOfferings
                .FirstOrDefaultAsync(co => co.CourseOfferingId == courseOfferingIdValue, cancellationToken)
                .ConfigureAwait(false);
            if (courseOfferingEntity == null)
                return Problem(
                    detail: $"CourseOffering with ID '{courseOfferingIdValue}' not found.",
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

            CourseOfferingAssociationEntity entity = new()
            {
                CourseOfferingAssociationIdValue = Guid.NewGuid().ToString(),
                PrimaryCodeType = request.PrimaryCode?.CodeType ?? string.Empty,
                PrimaryCode = request.PrimaryCode?.Code ?? string.Empty,
                Role = request.Role,
                StartDateTime = request.StartDateTime,
                ExpectedEndDateTime = request.ExpectedEndDateTime,
                ActualEndDateTime = request.ActualEndDateTime,
                State = request.State,
                RemoteState = request.RemoteState,
                ResultJson = request.Result != null ? JsonSerializer.Serialize(request.Result) : null,
                StudyLoadJson = request.StudyLoad != null ? JsonSerializer.Serialize(request.StudyLoad) : null,
                OtherCodes =
                    request.OtherCodes?.Select(oc => new OtherCodeEntity { CodeType = oc.CodeType, Code = oc.Code })
                        .ToList() ?? [],
                ConsumerJson = request.Consumer != null ? JsonSerializer.Serialize(request.Consumer) : null,
                ExtJson = request.Ext != null ? JsonSerializer.Serialize(request.Ext) : null,
                CourseOfferingEntityId = courseOfferingEntity.Id,
                PersonEntityId = personEntity.Id,
                OrganisationEntityId = organisationEntity.Id,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _dbContext.CourseOfferingAssociations.Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return StatusCode(StatusCodes.Status201Created, new AssociationWriteResponse
            {
                AssociationId = entity.CourseOfferingAssociationIdValue,
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
    ///     Replaces a course offering association, or creates one at the given id if it doesn't exist
    ///     yet. PUT /course-offering-associations/{id}. Upsert semantics per spec: <c>200</c> if
    ///     replacing an existing association, <c>201</c> if creating a new one at the given id. Takes the
    ///     full <c>CourseOfferingAssociation</c> representation - a different, wider contract than
    ///     <see cref="PatchCourseOfferingAssociation" />'s narrow <c>remoteState</c>/<c>result</c>
    ///     merge-patch.
    /// </summary>
    /// <param name="id">The course offering association ID.</param>
    /// <param name="model">The full course offering association representation.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PutCourseOfferingAssociation(
        string id,
        [FromBody] CourseOfferingAssociation model,
        CancellationToken cancellationToken = default)
    {
        try
        {
            CourseOfferingAssociationEntity? entity = await _dbContext.CourseOfferingAssociations
                .FirstOrDefaultAsync(coa => coa.CourseOfferingAssociationIdValue == id, cancellationToken)
                .ConfigureAwait(false);

            bool isNew;
            if (entity == null)
            {
                isNew = true;
                entity = MapToEntity(model);
                entity.CourseOfferingAssociationIdValue = id;
                _dbContext.CourseOfferingAssociations.Add(entity);
            }
            else
            {
                isNew = false;
                UpdateEntityFromApiModel(entity, model);
            }

            if (!string.IsNullOrEmpty(model.CourseOfferingId?.Value))
            {
                CourseOfferingEntity? courseOfferingEntity = await _dbContext.CourseOfferings
                    .AsNoTracking()
                    .FirstOrDefaultAsync(co => co.CourseOfferingId == model.CourseOfferingId.Value, cancellationToken)
                    .ConfigureAwait(false);
                if (courseOfferingEntity != null) entity.CourseOfferingEntityId = courseOfferingEntity.Id;
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
    ///     Applies a JSON Merge Patch (RFC 7396) to a course offering association's
    ///     <c>remoteState</c>/<c>result</c> - the only fields the spec allows this endpoint to change.
    ///     PUT (full update) is separately implemented above (see <see cref="PutCourseOfferingAssociation" />).
    /// </summary>
    /// <param name="id">The course offering association ID.</param>
    /// <param name="request">The merge-patch request body.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPatch("{id}")]
    [Consumes("application/merge-patch+json")]
    [ProducesResponseType(typeof(AssociationWriteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PatchCourseOfferingAssociation(
        string id,
        [FromBody] AssociationPatchRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            CourseOfferingAssociationEntity? entity = await _dbContext.CourseOfferingAssociations
                .FirstOrDefaultAsync(coa => coa.CourseOfferingAssociationIdValue == id, cancellationToken)
                .ConfigureAwait(false);

            if (entity == null)
                return Problem(
                    detail: $"CourseOfferingAssociation with ID '{id}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            if (request.RemoteState != null) entity.RemoteState = request.RemoteState;

            if (request.Result != null) entity.ResultJson = JsonSerializer.Serialize(request.Result);

            entity.ModifiedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Ok(new AssociationWriteResponse
            {
                AssociationId = entity.CourseOfferingAssociationIdValue,
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
