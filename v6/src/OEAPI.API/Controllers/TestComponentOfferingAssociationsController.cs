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
///     Controller for test component offering association endpoints.
/// </summary>
/// <remarks>
///     The top-level <c>GET /test-component-offering-associations</c> this class's
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
[Tags("TestComponentOfferingAssociations")]
public class TestComponentOfferingAssociationsController(OEAPIDbContext dbContext,
    ILogger<TestComponentOfferingAssociationsController> logger) : GenericEntityController<
    TestComponentOfferingAssociationEntity, TestComponentOfferingAssociation>(dbContext, logger, "Test Component Offering Association", "test-component-offering-associations")
{
    private readonly OEAPIDbContext _dbContext = dbContext;

    /// <summary>Retrieves a list of all test component offering associations.</summary>
    /// <param name="primaryCode">Filter by primary code.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response for Scalar/OpenAPI doc generation -
    // TApiModel can't be referenced in an attribute on the generic base class (CS0416). Behaviour is
    // unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(PagedResult<TestComponentOfferingAssociation>), StatusCodes.Status200OK)]
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

    /// <summary>Retrieves a single test component offering association by ID.</summary>
    /// <param name="testComponentOfferingAssociationId">The test component offering association ID.</param>
    /// <param name="expand">Comma-separated list of relations to expand, e.g. <c>testComponentOffering,person</c>.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response - see the GetAll override above.
    // Behaviour is unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(TestComponentOfferingAssociation), StatusCodes.Status200OK)]
    [HttpGet("{testComponentOfferingAssociationId}")]
    public override Task<IActionResult> GetById(
        string testComponentOfferingAssociationId,
        string? expand,
        string? fields,
        string? consumer,
        CancellationToken cancellationToken = default)
    {
        return base.GetById(testComponentOfferingAssociationId, expand, fields, consumer, cancellationToken);
    }

    /// <summary>
    ///     Finds a test component offering association by unique identifier
    ///     (TestComponentOfferingAssociationIdValue). Routes through
    ///     <see cref="GenericEntityController{TEntity,TApiModel}.Entities" /> (not a raw
    ///     <c>_dbContext.TestComponentOfferingAssociations</c> query) so <see cref="ApplyIncludes" />
    ///     applies to the single-item lookup path too, not just <c>GetAll</c>.
    /// </summary>
    /// <param name="idValue">The test component offering association ID value.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the test component offering
    ///     association entity or null.
    /// </returns>
    protected override async Task<TestComponentOfferingAssociationEntity?> FindByUniqueIdAsync(string idValue,
        CancellationToken cancellationToken = default)
    {
        return await Entities
            .FirstOrDefaultAsync(tcoa => tcoa.TestComponentOfferingAssociationIdValue == idValue, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    ///     Eagerly loads TestComponentOffering/Person/Attempts so <c>MapToApiModel</c> can read them
    ///     directly from the real FK relationships. Organisation is not included -
    ///     <c>TestComponentOfferingAssociation</c> has no <c>organisationId</c>/<c>organisation</c>
    ///     field per spec (see tier3 removal).
    /// </summary>
    protected override IQueryable<TestComponentOfferingAssociationEntity> ApplyIncludes(
        IQueryable<TestComponentOfferingAssociationEntity> query)
    {
        return query
            .Include(tcoa => tcoa.TestComponentOffering)
            .Include(tcoa => tcoa.Person)
            .Include(tcoa => tcoa.Attempts);
    }

    // Create/Delete removed: the spec defines no POST or DELETE at all for
    // /test-component-offering-associations.

    // ========================================================================
    // Required abstract method implementations
    // ========================================================================

    /// <summary>
    ///     Maps a TestComponentOfferingAssociationEntity to a TestComponentOfferingAssociation API model.
    /// </summary>
    /// <param name="entity">The entity to map.</param>
    /// <param name="consumer">The requested consumer, for consumer-specific data.</param>
    /// <returns>The API model.</returns>
    protected override TestComponentOfferingAssociation MapToApiModel(TestComponentOfferingAssociationEntity entity,
        string? consumer)
    {
        return entity.ToApiModel(consumer);
    }

    /// <summary>
    ///     Maps a TestComponentOfferingAssociation API model to a TestComponentOfferingAssociationEntity.
    ///     Not currently called by any live code path (see the note on writes in
    ///     <see cref="GenericEntityController{TEntity,TApiModel}" />). Note: resolving
    ///     <c>model.TestComponentOfferingId</c>/<c>model.PersonId</c> (external string IDs) to the
    ///     entity's internal Guid FKs requires an async DB lookup that this synchronous method can't
    ///     perform - whichever explicit write action ends up calling this needs to resolve and set
    ///     those FKs itself after calling this method.
    /// </summary>
    /// <param name="model">The API model to map.</param>
    /// <returns>The entity.</returns>
    protected override TestComponentOfferingAssociationEntity MapToEntity(TestComponentOfferingAssociation model)
    {
        return model.ToEntity();
    }

    /// <summary>
    ///     Updates a TestComponentOfferingAssociationEntity from a TestComponentOfferingAssociation API
    ///     model. See the note on <see cref="MapToEntity" /> regarding TestComponentOffering/Person/
    ///     Organisation FK resolution.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="model">The API model with new data.</param>
    protected override void UpdateEntityFromApiModel(TestComponentOfferingAssociationEntity entity,
        TestComponentOfferingAssociation model)
    {
        entity.UpdateFrom(model);
    }

    /// <summary>
    ///     Gets the unique identifier for a test component offering association entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>The identifier as string.</returns>
    protected override string GetEntityId(TestComponentOfferingAssociationEntity entity)
    {
        return entity.TestComponentOfferingAssociationIdValue;
    }

    // ========================================================================
    // Override methods for custom behaviour
    // ========================================================================

    /// <summary>
    ///     Applies default ordering for test component offering associations: by primary code.
    /// </summary>
    /// <param name="query">The query to order.</param>
    /// <returns>The ordered query.</returns>
    protected override IQueryable<TestComponentOfferingAssociationEntity> ApplyDefaultOrdering(
        IQueryable<TestComponentOfferingAssociationEntity> query)
    {
        return query.OrderBy(tcoa => tcoa.PrimaryCode);
    }

    /// <summary>
    ///     Applies expand to a test component offering association API model (includes related entities).
    /// </summary>
    /// <param name="apiModel">The API model to expand.</param>
    /// <param name="expand">The expand parameter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the expanded API model.</returns>
    protected override async Task<TestComponentOfferingAssociation> ApplyExpandAsync(
        TestComponentOfferingAssociation apiModel,
        string expand,
        CancellationToken cancellationToken = default)
    {
        string[] expandOptions = expand.Split([','], StringSplitOptions.RemoveEmptyEntries);

        foreach (string option in expandOptions)
            switch (option.Trim().ToLowerInvariant())
            {
                case "test_component_offering":
                    if (apiModel.TestComponentOfferingId != null && apiModel.TestComponentOffering == null)
                    {
                        TestComponentOfferingEntity? testComponentOfferingEntity = await _dbContext
                            .TestComponentOfferings
                            .AsNoTracking()
                            .Include(tco => tco.TestComponent)
                            .Include(tco => tco.Organisation)
                            .Include(tco => tco.AcademicSession)
                            .Include(tco => tco.Rooms)
                            .Include(tco => tco.CourseOfferings)
                            .Include(tco => tco.Groups)
                            .FirstOrDefaultAsync(tco =>
                                    tco.TestComponentOfferingIdValue == apiModel.TestComponentOfferingId.Value ||
                                    tco.Id.ToString() == apiModel.TestComponentOfferingId.Value,
                                cancellationToken)
                            .ConfigureAwait(false);

                        if (testComponentOfferingEntity != null)
                            apiModel.TestComponentOffering = testComponentOfferingEntity.ToApiModel(null);
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
                    // TestComponentOfferingAssociation has no rooms field of its own - the spec
                    // declares this expand value to populate the rooms of the association's own
                    // testComponentOffering, composing into that nested object the same way the
                    // "test_component_offering" case above expands it (reuses the already-expanded object
                    // if present, rather than re-fetching).
                    if (apiModel.TestComponentOfferingId != null)
                    {
                        TestComponentOfferingEntity? offeringEntity = await _dbContext.TestComponentOfferings
                            .AsNoTracking()
                            .Include(tco => tco.Rooms).ThenInclude(r => r.Building)
                            .FirstOrDefaultAsync(tco =>
                                    tco.TestComponentOfferingIdValue == apiModel.TestComponentOfferingId.Value ||
                                    tco.Id.ToString() == apiModel.TestComponentOfferingId.Value,
                                cancellationToken)
                            .ConfigureAwait(false);

                        if (offeringEntity != null)
                        {
                            apiModel.TestComponentOffering ??= offeringEntity.ToApiModel(null);
                            apiModel.TestComponentOffering.Rooms =
                                [.. offeringEntity.Rooms.Select(r => r.ToApiModel(null))];
                        }
                    }

                    break;

                case "attempts":
                    // TestComponentOfferingAssociation.attempts was declared on the schema but never
                    // wired to any expand case - attemptIds already resolves via the real Attempts
                    // relationship (see TestComponentOfferingAssociationMappingExtensions.ToApiModel),
                    // but there was no code path to populate the full expanded objects here at all,
                    // regardless of what was requested.
                    if (apiModel.AttemptIds != null && apiModel.AttemptIds.Length > 0)
                    {
                        List<TestComponentOfferingAssociationAttemptEntity> attemptEntities = await _dbContext
                            .TestComponentOfferingAssociationAttempts
                            .AsNoTracking()
                            .Include(a => a.Rooms)
                            .Include(a => a.Coordinator)
                            .Include(a => a.CourseOfferingAssociation)
                            .Include(a => a.TestComponentOfferingAssociation)
                            .Where(a => apiModel.AttemptIds.Select(id => id.Value).Contains(a.AttemptIdValue))
                            .ToListAsync(cancellationToken)
                            .ConfigureAwait(false);

                        if (attemptEntities.Count > 0)
                            apiModel.Attempts =
                                [.. attemptEntities.Select(a => TestComponentOfferingAssociationAttemptsController.MapToApiModel(a, null))];
                    }

                    break;
            }

        return apiModel;
    }

    /// <summary>
    ///     Replaces a test component offering association, or creates one at the given id if it doesn't
    ///     exist yet. PUT /test-component-offering-associations/{id}. Upsert semantics per spec:
    ///     <c>200</c> if replacing an existing association, <c>201</c> if creating a new one at the given
    ///     id. Takes the full <c>TestComponentOfferingAssociation</c> representation - a different, wider
    ///     contract than <see cref="PatchTestComponentOfferingAssociation" />'s narrow
    ///     <c>remoteState</c>/<c>result</c> merge-patch.
    /// </summary>
    /// <param name="id">The test component offering association ID.</param>
    /// <param name="model">The full test component offering association representation.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PutTestComponentOfferingAssociation(
        string id,
        [FromBody] TestComponentOfferingAssociation model,
        CancellationToken cancellationToken = default)
    {
        try
        {
            TestComponentOfferingAssociationEntity? entity = await _dbContext.TestComponentOfferingAssociations
                .FirstOrDefaultAsync(tcoa => tcoa.TestComponentOfferingAssociationIdValue == id, cancellationToken)
                .ConfigureAwait(false);

            bool isNew;
            if (entity == null)
            {
                isNew = true;
                entity = MapToEntity(model);
                entity.TestComponentOfferingAssociationIdValue = id;
                // Url isn't a writable field on the TestComponentOfferingAssociation model - the spec
                // exposes it only via the dedicated GET .../url sub-resource, always server-generated
                // (a deep link to start the testing tool), never client-supplied. Generated here
                // (not in ToEntity) because it needs the final, caller-chosen id from the PUT's own
                // URL, not the throwaway one ToEntity generates before this line overwrites it.
                entity.Url = $"https://example.org/test-tool/start/{id}";
                _dbContext.TestComponentOfferingAssociations.Add(entity);
            }
            else
            {
                isNew = false;
                UpdateEntityFromApiModel(entity, model);
            }

            if (!string.IsNullOrEmpty(model.TestComponentOfferingId?.Value))
            {
                TestComponentOfferingEntity? testComponentOfferingEntity = await _dbContext.TestComponentOfferings
                    .AsNoTracking()
                    .FirstOrDefaultAsync(tco => tco.TestComponentOfferingIdValue == model.TestComponentOfferingId.Value,
                        cancellationToken)
                    .ConfigureAwait(false);
                if (testComponentOfferingEntity != null)
                    entity.TestComponentOfferingEntityId = testComponentOfferingEntity.Id;
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
    ///     Applies a JSON Merge Patch (RFC 7396) to a test component offering association's
    ///     <c>remoteState</c>/<c>result</c> - the only fields the spec allows this endpoint to change.
    ///     PUT (full update) is separately implemented above (see <see cref="PutTestComponentOfferingAssociation" />).
    /// </summary>
    /// <param name="id">The test component offering association ID.</param>
    /// <param name="request">The merge-patch request body.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPatch("{id}")]
    [Consumes("application/merge-patch+json")]
    [ProducesResponseType(typeof(AssociationWriteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PatchTestComponentOfferingAssociation(
        string id,
        [FromBody] AssociationPatchRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            TestComponentOfferingAssociationEntity? entity = await _dbContext.TestComponentOfferingAssociations
                .FirstOrDefaultAsync(tcoa => tcoa.TestComponentOfferingAssociationIdValue == id, cancellationToken)
                .ConfigureAwait(false);

            if (entity == null)
                return Problem(
                    detail: $"TestComponentOfferingAssociation with ID '{id}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            if (request.RemoteState != null) entity.RemoteState = request.RemoteState;

            if (request.Result != null) entity.ResultJson = JsonSerializer.Serialize(request.Result);

            entity.ModifiedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Ok(new AssociationWriteResponse
            {
                AssociationId = entity.TestComponentOfferingAssociationIdValue,
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

    // ========================================================================
    // Nested endpoints for test-component-offering-associations/{id}/*
    // ========================================================================

    /// <summary>
    ///     Gets the URL of the test component association to start the testing tool.
    ///     GET /test-component-offering-associations/{id}/url
    /// </summary>
    /// <param name="testComponentOfferingAssociationId">The test component offering association ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{testComponentOfferingAssociationId}/url")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUrl(string testComponentOfferingAssociationId, CancellationToken cancellationToken = default)
    {
        try
        {
            TestComponentOfferingAssociationEntity? entity = await _dbContext.TestComponentOfferingAssociations
                .AsNoTracking()
                .FirstOrDefaultAsync(tcoa => tcoa.TestComponentOfferingAssociationIdValue == testComponentOfferingAssociationId, cancellationToken)
                .ConfigureAwait(false);

            if (entity == null)
                return Problem(
                    detail: $"TestComponentOfferingAssociation with ID '{testComponentOfferingAssociationId}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            return Ok(entity.Url);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    ///     Retrieves all attempts for a specific test component offering association.
    ///     GET /test-component-offering-associations/{id}/test-component-offering-association-attempts
    /// </summary>
    /// <param name="testComponentOfferingAssociationId">The test component offering association ID.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="attendance">Filter by attendance.</param>
    /// <param name="state">Filter by state.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="filterQuery">Generic RSQL-style filter query.</param>
    /// <param name="resultState">Filter by the attempt result's state.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{testComponentOfferingAssociationId}/test-component-offering-association-attempts")]
    [ProducesResponseType(typeof(PagedResult<TestComponentOfferingAssociationAttempt>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAttemptsByAssociationId(
        string testComponentOfferingAssociationId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? attendance,
        [FromQuery] string? state,
        [FromQuery] string? consumer,
        [FromQuery] string? fields,
        [FromQuery] string? filterQuery,
        [FromQuery] string? resultState,
        CancellationToken cancellationToken = default)
    {
        try
        {
            TestComponentOfferingAssociationEntity? associationEntity = await _dbContext
                .TestComponentOfferingAssociations
                .AsNoTracking()
                .FirstOrDefaultAsync(tcoa => tcoa.TestComponentOfferingAssociationIdValue == testComponentOfferingAssociationId, cancellationToken)
                .ConfigureAwait(false);

            if (associationEntity == null)
                return Problem(
                    detail: $"TestComponentOfferingAssociation with ID '{testComponentOfferingAssociationId}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            IQueryable<TestComponentOfferingAssociationAttemptEntity> query = _dbContext
                .TestComponentOfferingAssociationAttempts
                .AsNoTracking()
                .Include(a => a.Rooms)
                .Include(a => a.Coordinator)
                .Include(a => a.TestComponentOfferingAssociation)
                .Include(a => a.CourseOfferingAssociation)
                .Where(a => a.TestComponentOfferingAssociationEntityId == associationEntity.Id);

            query = ApplyFilterQuery(query);

            if (!string.IsNullOrEmpty(attendance)) query = query.Where(a => a.Attendance == attendance);

            if (!string.IsNullOrEmpty(state)) query = query.Where(a => a.State == state);

            query = query.OrderBy(a => a.AttemptIdValue);

            // resultState filters on Result.State, deserialized from the opaque ResultJson blob - no
            // portable SQL-level query exists for either provider (mirrors
            // PersonsController.GetCourseOfferingAssociationsByPersonId), so every attempt matching the
            // SQL-level filters above is mapped first, then filtered by resultState in memory, and only
            // then paginated - preserving correct totalCount/paging instead of paginating before this
            // filter and under-filling pages.
            List<TestComponentOfferingAssociationAttemptEntity> candidateEntities = await query
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            List<TestComponentOfferingAssociationAttempt> candidates = [.. candidateEntities.Select(item =>
                TestComponentOfferingAssociationAttemptsController.MapToApiModel(item, consumer))];

            if (!string.IsNullOrEmpty(resultState))
                candidates = [.. candidates.Where(c => c.Result != null && c.Result.State == resultState)];

            int totalCount = candidates.Count;
            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);

            List<TestComponentOfferingAssociationAttempt> apiModels = [.. candidates
                .Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize)];

            PagedResult<TestComponentOfferingAssociationAttempt> result = new(apiModels, totalCount, validatedPage,
                validatedPageSize);
            return PagedResponseWithFieldSelection(result, fields);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    ///     Inserts or replaces a single test component offering association attempt, scoped to a
    ///     specific association (an alternate path to the same upsert as
    ///     <see cref="TestComponentOfferingAssociationAttemptsController.Put" />, for systems that need to
    ///     process attempt results based on the association ID the attempt belongs to). The path's
    ///     association id is authoritative for the attempt's association FK, overriding any value in the
    ///     request body.
    ///     PUT /test-component-offering-associations/{id}/test-component-offering-association-attempt/{attemptId}
    /// </summary>
    /// <param name="id">The test component offering association ID.</param>
    /// <param name="attemptId">The attempt ID.</param>
    /// <param name="model">The full attempt representation.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPut("{id}/test-component-offering-association-attempt/{attemptId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PutAttemptByAssociationId(
        string id,
        string attemptId,
        [FromBody] TestComponentOfferingAssociationAttempt model,
        CancellationToken cancellationToken = default)
    {
        try
        {
            TestComponentOfferingAssociationEntity? associationEntity = await _dbContext
                .TestComponentOfferingAssociations
                .AsNoTracking()
                .FirstOrDefaultAsync(tcoa => tcoa.TestComponentOfferingAssociationIdValue == id, cancellationToken)
                .ConfigureAwait(false);

            if (associationEntity == null)
                return Problem(
                    detail: $"TestComponentOfferingAssociation with ID '{id}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            TestComponentOfferingAssociationAttemptEntity? attemptEntity = await _dbContext
                .TestComponentOfferingAssociationAttempts
                .FirstOrDefaultAsync(a => a.AttemptIdValue == attemptId, cancellationToken)
                .ConfigureAwait(false);

            bool isNew = attemptEntity == null;
            if (isNew)
            {
                attemptEntity = new TestComponentOfferingAssociationAttemptEntity
                {
                    AttemptIdValue = attemptId,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };
                _dbContext.TestComponentOfferingAssociationAttempts.Add(attemptEntity);
            }

            attemptEntity!.Opportunity = model.Opportunity ?? attemptEntity.Opportunity;
            attemptEntity.Attempt = model.Attempt ?? attemptEntity.Attempt;
            attemptEntity.State = model.State ?? attemptEntity.State;
            attemptEntity.StartDateTime = model.StartDateTime ?? attemptEntity.StartDateTime;
            attemptEntity.EndDateTime = model.EndDateTime ?? attemptEntity.EndDateTime;
            attemptEntity.Attendance = model.Attendance ?? attemptEntity.Attendance;
            attemptEntity.Irregularities = model.Irregularities ?? attemptEntity.Irregularities;
            attemptEntity.DocumentsJson = model.Documents != null
                ? JsonSerializer.Serialize(model.Documents)
                : attemptEntity.DocumentsJson;
            attemptEntity.ResultJson =
                model.Result != null ? JsonSerializer.Serialize(model.Result) : attemptEntity.ResultJson;
            attemptEntity.ConsumerJson = model.Consumer != null
                ? JsonSerializer.Serialize(model.Consumer)
                : attemptEntity.ConsumerJson;
            attemptEntity.ModifiedAt = DateTime.UtcNow;
            attemptEntity.TestComponentOfferingAssociationEntityId = associationEntity.Id;

            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return isNew ? StatusCode(StatusCodes.Status201Created) : Ok();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}
