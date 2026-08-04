using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Infrastructure.Data.Context;
using OEAPI.Infrastructure.Data.Entities;
using OEAPI.Infrastructure.Data.Mapping;
using OEAPI.Infrastructure.Query.Consumers;

namespace OEAPI.API.Controllers;

/// <summary>
///     Controller for the <c>test-component-offering-associations-attempt</c> resource (note: singular
///     "attempt" - the spec's path for this resource is asymmetric with the plural
///     "test-component-offering-association-attempts" used by the nested listing endpoint on
///     <see cref="TestComponentOfferingAssociationsController" />). Doesn't extend
///     <see cref="GenericEntityController{TEntity,TApiModel}" />: the spec defines no bare
///     <c>GET /test-component-offering-associations-attempt</c> list endpoint for this resource (the
///     only way to list attempts is nested under a specific association), so inheriting the generic
///     base would add a non-spec endpoint - this is purpose-built with exactly the 3 operations the spec
///     defines (GET/PUT/PATCH by id).
/// </summary>
/// <remarks>
///     Initializes a new instance of the TestComponentOfferingAssociationAttemptsController class.
/// </remarks>
/// <param name="dbContext">The database context.</param>
/// <param name="logger">The logger.</param>
[ApiController]
[Route("test-component-offering-associations-attempt")]
[Produces("application/json")]
[Consumes("application/json")]
[Tags("TestComponentOfferingAssociationAttempts")]
public class TestComponentOfferingAssociationAttemptsController(
    OEAPIDbContext dbContext,
    ILogger<TestComponentOfferingAssociationAttemptsController> logger) : BaseApiController(logger)
{
    private readonly OEAPIDbContext _dbContext = dbContext;

    /// <summary>
    ///     Retrieves a single test component offering association attempt.
    ///     GET /test-component-offering-associations-attempt/{id}
    /// </summary>
    /// <param name="testComponentOfferingAssociationAttemptId">The attempt ID.</param>
    /// <param name="expand">
    ///     Comma-separated list of relationship fields to expand
    ///     (<c>rooms</c>, <c>coordinator</c>).
    /// </param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{testComponentOfferingAssociationAttemptId}")]
    [ProducesResponseType(typeof(TestComponentOfferingAssociationAttempt), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(
        string testComponentOfferingAssociationAttemptId,
        [FromQuery] string? expand,
        [FromQuery] string? consumer,
        CancellationToken cancellationToken = default)
    {
        try
        {
            TestComponentOfferingAssociationAttemptEntity? entity = await _dbContext
                .TestComponentOfferingAssociationAttempts
                .AsNoTracking()
                .Include(a => a.Rooms).ThenInclude(r => r.Building)
                .Include(a => a.Coordinator)
                .Include(a => a.TestComponentOfferingAssociation)
                .Include(a => a.CourseOfferingAssociation)
                .FirstOrDefaultAsync(a => a.AttemptIdValue == testComponentOfferingAssociationAttemptId, cancellationToken)
                .ConfigureAwait(false);

            if (entity == null || (!string.IsNullOrEmpty(consumer) && entity.ConsumerKey != consumer))
                return Problem(
                    detail: $"TestComponentOfferingAssociationAttempt with ID '{testComponentOfferingAssociationAttemptId}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            TestComponentOfferingAssociationAttempt apiModel = MapToApiModel(entity, consumer);
            ApplyExpand(apiModel, entity, expand);

            return Ok(apiModel);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    ///     Populates <see cref="TestComponentOfferingAssociationAttempt.Rooms" />/<c>Coordinator</c> with
    ///     full objects when requested via <c>expand</c> - <c>Rooms</c>/<c>Coordinator</c> are already
    ///     eager-loaded on <paramref name="entity" /> by <see cref="GetById" /> regardless (a single-row
    ///     lookup, so there's no extra-query cost to avoid), this just decides whether to surface them.
    /// </summary>
    private static void ApplyExpand(TestComponentOfferingAssociationAttempt apiModel,
        TestComponentOfferingAssociationAttemptEntity entity, string? expand)
    {
        if (string.IsNullOrEmpty(expand)) return;

        string[] expandOptions =
            expand.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        foreach (string option in expandOptions)
            switch (option.ToLowerInvariant())
            {
                case "rooms":
                    if (entity.Rooms.Count > 0) apiModel.Rooms = [.. entity.Rooms.Select(r => r.ToApiModel(null))];
                    break;

                case "coordinator":
                    if (entity.Coordinator != null) apiModel.Coordinator = entity.Coordinator.ToApiModel(null);
                    break;
            }
    }

    /// <summary>
    ///     Inserts or replaces a single test component offering association attempt (upsert - the spec's
    ///     PUT enrols a person in a specific attempt or updates an existing one, identified by the path
    ///     id in both cases).
    ///     PUT /test-component-offering-associations-attempt/{id}
    /// </summary>
    /// <param name="id">The attempt ID.</param>
    /// <param name="model">The full attempt representation.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Put(
        string id,
        [FromBody] TestComponentOfferingAssociationAttempt model,
        CancellationToken cancellationToken = default)
    {
        try
        {
            TestComponentOfferingAssociationAttemptEntity? entity = await _dbContext
                .TestComponentOfferingAssociationAttempts
                .FirstOrDefaultAsync(a => a.AttemptIdValue == id, cancellationToken)
                .ConfigureAwait(false);

            bool isNew = entity == null;
            if (isNew)
            {
                entity = new TestComponentOfferingAssociationAttemptEntity
                {
                    AttemptIdValue = id,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };
                _dbContext.TestComponentOfferingAssociationAttempts.Add(entity);
            }

            UpdateEntityFromApiModel(entity!, model);

            if (!string.IsNullOrEmpty(model.TestComponentOfferingAssociationId))
            {
                TestComponentOfferingAssociationEntity? associationEntity = await _dbContext
                    .TestComponentOfferingAssociations
                    .AsNoTracking()
                    .FirstOrDefaultAsync(
                        a => a.TestComponentOfferingAssociationIdValue == model.TestComponentOfferingAssociationId,
                        cancellationToken)
                    .ConfigureAwait(false);
                if (associationEntity != null) entity!.TestComponentOfferingAssociationEntityId = associationEntity.Id;
            }

            if (!string.IsNullOrEmpty(model.CourseOfferingAssociationId))
            {
                CourseOfferingAssociationEntity? courseOfferingAssociationEntity = await _dbContext
                    .CourseOfferingAssociations
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.CourseOfferingAssociationIdValue == model.CourseOfferingAssociationId,
                        cancellationToken)
                    .ConfigureAwait(false);
                if (courseOfferingAssociationEntity != null)
                    entity!.CourseOfferingAssociationEntityId = courseOfferingAssociationEntity.Id;
            }

            if (!string.IsNullOrEmpty(model.CoordinatorId?.Value))
            {
                PersonEntity? coordinatorEntity = await _dbContext.Persons
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.PersonId == model.CoordinatorId.Value, cancellationToken)
                    .ConfigureAwait(false);
                if (coordinatorEntity != null) entity!.CoordinatorEntityId = coordinatorEntity.Id;
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
    ///     Applies a JSON Merge Patch (RFC 7396) to an attempt's <c>result</c> - the only field the spec
    ///     allows this endpoint to change.
    ///     PATCH /test-component-offering-associations-attempt/{id}
    /// </summary>
    /// <param name="id">The attempt ID.</param>
    /// <param name="request">The merge-patch request body.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPatch("{id}")]
    [Consumes("application/merge-patch+json")]
    [ProducesResponseType(typeof(AssociationWriteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Patch(
        string id,
        [FromBody] AssociationPatchRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            TestComponentOfferingAssociationAttemptEntity? entity = await _dbContext
                .TestComponentOfferingAssociationAttempts
                .FirstOrDefaultAsync(a => a.AttemptIdValue == id, cancellationToken)
                .ConfigureAwait(false);

            if (entity == null)
                return Problem(
                    detail: $"TestComponentOfferingAssociationAttempt with ID '{id}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            if (request.Result != null) entity.ResultJson = JsonSerializer.Serialize(request.Result);

            entity.ModifiedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Ok(new AssociationWriteResponse
            {
                AssociationId = entity.AttemptIdValue,
                Message =
                [
                    new LanguageTypedString { Language = "en-GB", Value = "The attempt has been updated." }
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
    ///     Maps a TestComponentOfferingAssociationAttemptEntity to a TestComponentOfferingAssociationAttempt
    ///     API model. <c>Rooms</c>/<c>Coordinator</c> are always left null here regardless of what's
    ///     eager-loaded on <paramref name="entity" /> - see <see cref="ApplyExpand" />, which populates them
    ///     afterwards only when requested via <c>expand</c>, matching every other resource's convention of
    ///     only returning full expanded objects on request.
    /// </summary>
    internal static TestComponentOfferingAssociationAttempt MapToApiModel(
        TestComponentOfferingAssociationAttemptEntity entity, string? consumer)
    {
        return new TestComponentOfferingAssociationAttempt
        {
            AttemptId = entity.AttemptIdValue,
            Opportunity = entity.Opportunity,
            Attempt = entity.Attempt,
            State = entity.State,
            StartDateTime = entity.StartDateTime,
            EndDateTime = entity.EndDateTime,
            RoomIds = entity.Rooms.Count > 0
                ? [.. entity.Rooms.Select(r => new Identifier { Value = r.RoomId })]
                : null,
            Rooms = null, // Full objects only populated when expand=rooms is requested
            Attendance = entity.Attendance,
            Irregularities = entity.Irregularities,
            CoordinatorId = entity.Coordinator != null ? new Identifier { Value = entity.Coordinator.PersonId } : null,
            Coordinator = null, // Full object only populated when expand=coordinator is requested
            Documents = string.IsNullOrEmpty(entity.DocumentsJson)
                ? null
                : JsonSerializer.Deserialize<Document[]>(entity.DocumentsJson),
            Result = string.IsNullOrEmpty(entity.ResultJson)
                ? null
                : JsonSerializer.Deserialize<Result>(entity.ResultJson),
            Consumer = entity.ConsumerJson.ToGenericConsumer(consumer),
            CourseOfferingAssociationId = entity.CourseOfferingAssociation?.CourseOfferingAssociationIdValue,
            TestComponentOfferingAssociationId =
                entity.TestComponentOfferingAssociation?.TestComponentOfferingAssociationIdValue
        };
    }

    /// <summary>
    ///     Updates a TestComponentOfferingAssociationAttemptEntity from a
    ///     TestComponentOfferingAssociationAttempt API model. See <see cref="Put" /> for relationship FK
    ///     resolution (requires async DB lookups this method can't perform).
    /// </summary>
    private static void UpdateEntityFromApiModel(TestComponentOfferingAssociationAttemptEntity entity,
        TestComponentOfferingAssociationAttempt model)
    {
        entity.Opportunity = model.Opportunity ?? entity.Opportunity;
        entity.Attempt = model.Attempt ?? entity.Attempt;
        entity.State = model.State ?? entity.State;
        entity.StartDateTime = model.StartDateTime ?? entity.StartDateTime;
        entity.EndDateTime = model.EndDateTime ?? entity.EndDateTime;
        entity.Attendance = model.Attendance ?? entity.Attendance;
        entity.Irregularities = model.Irregularities ?? entity.Irregularities;
        entity.DocumentsJson =
            model.Documents != null ? JsonSerializer.Serialize(model.Documents) : entity.DocumentsJson;
        entity.ResultJson = model.Result != null ? JsonSerializer.Serialize(model.Result) : entity.ResultJson;
        entity.ConsumerJson = model.Consumer != null ? JsonSerializer.Serialize(model.Consumer) : entity.ConsumerJson;
        entity.ModifiedAt = DateTime.UtcNow;
    }
}
