using System.Text.Json;
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
///     Controller for group endpoints.
/// </summary>
/// <remarks>
///     Initializes a new instance of the GroupsController class.
/// </remarks>
/// <param name="dbContext">The database context.</param>
/// <param name="logger">The logger.</param>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
[Tags("Groups")]
public class GroupsController(OEAPIDbContext dbContext, ILogger<GroupsController> logger) : GenericEntityController<GroupEntity, Group>(dbContext, logger, "Group", "groups")
{
    private readonly OEAPIDbContext _dbContext = dbContext;

    /// <summary>Retrieves a list of all groups.</summary>
    /// <param name="primaryCode">Filter by primary code.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response for Scalar/OpenAPI doc generation -
    // TApiModel can't be referenced in an attribute on the generic base class (CS0416). Behaviour is
    // unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(PagedResult<Group>), StatusCodes.Status200OK)]
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

    /// <summary>Retrieves a single group by ID.</summary>
    /// <param name="groupId">The group ID.</param>
    /// <param name="expand">Comma-separated list of relations to expand, e.g. <c>organisation,academicSession</c>.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response - see the GetAll override above.
    // Behaviour is unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(Group), StatusCodes.Status200OK)]
    [HttpGet("{groupId}")]
    public override Task<IActionResult> GetById(
        string groupId,
        string? expand,
        string? fields,
        string? consumer,
        CancellationToken cancellationToken = default)
    {
        return base.GetById(groupId, expand, fields, consumer, cancellationToken);
    }

    /// <summary>
    ///     Applies the <c>groupType</c> and <c>q</c> (search) filters to a <c>GetAll</c> query.
    /// </summary>
    protected override IQueryable<GroupEntity> ApplyEntityFilters(IQueryable<GroupEntity> query)
    {
        string? groupType = GetQueryValue("groupType");
        if (!string.IsNullOrEmpty(groupType)) query = query.Where(g => g.GroupType == groupType);

        // Spec: q (search.yaml, wire name "q", not "search").
        string? searchValue = GetQueryValue("q");
        if (!string.IsNullOrEmpty(searchValue))
            query = query.WhereContains(searchValue, nameof(GroupEntity.NameJson), nameof(GroupEntity.PrimaryCode));

        return query;
    }

    /// <summary>
    ///     Eagerly loads the 4 typed offering collections (so <c>MapToApiModel</c> can flatten them into
    ///     the spec's polymorphic <c>offeringIds</c> without a separate query per row) plus
    ///     Organisation/AcademicSession (real FKs).
    /// </summary>
    protected override IQueryable<GroupEntity> ApplyIncludes(IQueryable<GroupEntity> query)
    {
        return query
            .Include(g => g.CourseOfferings)
            .Include(g => g.ProgrammeOfferings)
            .Include(g => g.LearningComponentOfferings)
            .Include(g => g.TestComponentOfferings)
            .Include(g => g.Organisation)
            .Include(g => g.AcademicSession);
    }

    // ========================================================================
    // Required abstract method implementations
    // ========================================================================

    /// <summary>
    ///     Maps a GroupEntity to a Group API model.
    /// </summary>
    /// <param name="entity">The entity to map.</param>
    /// <param name="consumer">The requested consumer, for consumer-specific data.</param>
    /// <returns>The API model.</returns>
    protected override Group MapToApiModel(GroupEntity entity, string? consumer)
    {
        return entity.ToApiModel(consumer);
    }

    /// <summary>
    ///     Maps a Group API model to a GroupEntity. Not currently called by any live code path (see the
    ///     note on writes in <see cref="GenericEntityController{TEntity,TApiModel}" />). Note: resolving
    ///     <c>model.OrganisationId</c>/<c>model.AcademicSessionId</c> (external string IDs) to the
    ///     entity's internal Guid FKs requires an async DB lookup that this synchronous method can't
    ///     perform - whichever explicit write action ends up calling this needs to resolve and set those
    ///     FKs itself after calling this method.
    /// </summary>
    /// <param name="model">The API model to map.</param>
    /// <returns>The entity.</returns>
    protected override GroupEntity MapToEntity(Group model)
    {
        return model.ToEntity();
    }

    /// <summary>
    ///     Updates a GroupEntity from a Group API model. See the note on <see cref="MapToEntity" />
    ///     regarding Organisation/AcademicSession FK resolution.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="model">The API model with new data.</param>
    protected override void UpdateEntityFromApiModel(GroupEntity entity, Group model)
    {
        entity.UpdateFrom(model);
    }

    /// <summary>
    ///     Gets the unique identifier for a group entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>The identifier as string.</returns>
    protected override string GetEntityId(GroupEntity entity)
    {
        return entity.GroupId;
    }

    // ========================================================================
    // Override methods for custom behaviour
    // ========================================================================

    /// <summary>
    ///     Applies default ordering for groups: by primary code, then by name.
    /// </summary>
    /// <param name="query">The query to order.</param>
    /// <returns>The ordered query.</returns>
    protected override IQueryable<GroupEntity> ApplyDefaultOrdering(IQueryable<GroupEntity> query)
    {
        return query.OrderBy(g => g.PrimaryCode).ThenBy(g => g.NameJson);
    }

    /// <summary>
    ///     Applies expand to a group API model (includes related entities).
    /// </summary>
    /// <param name="apiModel">The API model to expand.</param>
    /// <param name="expand">The expand parameter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the expanded API model.</returns>
    protected override async Task<Group> ApplyExpandAsync(
        Group apiModel,
        string expand,
        CancellationToken cancellationToken = default)
    {
        string[] expandOptions = expand.Split([','], StringSplitOptions.RemoveEmptyEntries);

        foreach (string option in expandOptions)
            switch (option.Trim().ToLowerInvariant())
            {
                case "organisation":
                    if (apiModel.OrganisationId != null && apiModel.Organisation == null)
                    {
                        OrganisationEntity? organisationEntity = await _dbContext.Organisations
                            .AsNoTracking()
                            .IncludeHierarchy()
                            .FirstOrDefaultAsync(o =>
                                    o.OrganisationId == apiModel.OrganisationId.Value ||
                                    o.Id.ToString() == apiModel.OrganisationId.Value,
                                cancellationToken)
                            .ConfigureAwait(false);

                        if (organisationEntity != null) apiModel.Organisation = organisationEntity.ToApiModel(null);
                    }

                    break;

                case "academicsession":
                    if (apiModel.AcademicSessionId != null && apiModel.AcademicSession == null)
                    {
                        AcademicSessionEntity? academicSessionEntity = await _dbContext.AcademicSessions
                            .AsNoTracking()
                            .IncludeHierarchy()
                            .FirstOrDefaultAsync(a =>
                                    a.AcademicSessionId == apiModel.AcademicSessionId.Value ||
                                    a.Id.ToString() == apiModel.AcademicSessionId.Value,
                                cancellationToken)
                            .ConfigureAwait(false);

                        if (academicSessionEntity != null)
                            apiModel.AcademicSession = academicSessionEntity.ToApiModel(null);
                    }

                    break;
            }

        return apiModel;
    }

    // Create/Delete removed: the spec defines no POST or DELETE for /groups.

    /// <summary>
    ///     Replaces a group, or creates one at the given id if it doesn't exist yet.
    ///     PUT /groups/{groupId}. Upsert semantics per spec: <c>200</c> if replacing an existing group,
    ///     <c>201</c> if creating a new one at the given id. Distinct from
    ///     <c>PUT /groups/{groupId}/memberships/{personId}</c> (the memberships sub-resource, already
    ///     implemented) - this replaces the group itself.
    /// </summary>
    /// <param name="groupId">The group ID.</param>
    /// <param name="model">The full group representation.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPut("{groupId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PutGroup(
        string groupId,
        [FromBody] Group model,
        CancellationToken cancellationToken = default)
    {
        try
        {
            GroupEntity? entity = await _dbContext.Groups
                .FirstOrDefaultAsync(g => g.GroupId == groupId, cancellationToken)
                .ConfigureAwait(false);

            bool isNew;
            if (entity == null)
            {
                isNew = true;
                entity = MapToEntity(model);
                entity.GroupId = groupId;
                _dbContext.Groups.Add(entity);
            }
            else
            {
                isNew = false;
                UpdateEntityFromApiModel(entity, model);
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

    // ========================================================================
    // Nested endpoints for groups/{groupId}/memberships
    // ========================================================================

    /// <summary>
    ///     Retrieves an ordered list of memberships (personIds that are members of this group, and
    ///     duration) for a specific group. GET /groups/{groupId}/memberships
    /// </summary>
    /// <param name="groupId">The group ID.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{groupId}/memberships")]
    [ProducesResponseType(typeof(PagedResult<Membership>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMembershipsByGroupId(
        string groupId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? consumer,
        [FromQuery] string? fields,
        CancellationToken cancellationToken = default)
    {
        try
        {
            GroupEntity? groupEntity = await _dbContext.Groups
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.GroupId == groupId, cancellationToken)
                .ConfigureAwait(false);

            if (groupEntity == null)
                return Problem(
                    detail: $"Group with ID '{groupId}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            IQueryable<MembershipEntity> query = _dbContext.Memberships
                .AsNoTracking()
                .Include(m => m.Person)
                .Where(m => m.GroupId == groupEntity.Id);

            query = ApplyFilterQuery(query);
            query = ConsumerKeyFilter.Apply(query, consumer);

            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);
            int totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            // Spec: ordered by personId.
            query = query.OrderBy(m => m.Person != null ? m.Person.PersonId : string.Empty);
            List<MembershipEntity> items = await query
                .Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            List<Membership> apiModels = [.. items.Select(item => item.ToApiModel(groupId, consumer))];
            PagedResult<Membership> result = new(apiModels, totalCount, validatedPage, validatedPageSize);
            return PagedResponseWithFieldSelection(result, fields);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    ///     Replaces or adds a single group member. PUT /groups/{groupId}/memberships/{personId}. This is
    ///     the spec's real way to add/replace a membership (the non-spec `PersonsController`/removed
    ///     `MembershipsController` routes it replaces have already been removed). Upsert semantics:
    ///     <c>200</c> if a membership already exists for this
    ///     (group, person) pair (a person can only be in a given group once, per the spec's own
    ///     description of the <c>personId</c> path parameter), <c>201</c> if a new one is created.
    /// </summary>
    /// <param name="groupId">The group ID.</param>
    /// <param name="personId">The person ID (the membership's own identifier, scoped to this group).</param>
    /// <param name="model">The membership representation.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPut("{groupId}/memberships/{personId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ReplaceMembershipInGroup(
        string groupId,
        string personId,
        [FromBody] Membership model,
        CancellationToken cancellationToken = default)
    {
        try
        {
            GroupEntity? groupEntity = await _dbContext.Groups
                .FirstOrDefaultAsync(g => g.GroupId == groupId, cancellationToken)
                .ConfigureAwait(false);

            if (groupEntity == null)
                return Problem(
                    detail: $"Group with ID '{groupId}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            PersonEntity? personEntity = await _dbContext.Persons
                .FirstOrDefaultAsync(p => p.PersonId == personId, cancellationToken)
                .ConfigureAwait(false);

            if (personEntity == null)
                return Problem(
                    detail: $"Person with ID '{personId}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            MembershipEntity? existingMembership = await _dbContext.Memberships
                .FirstOrDefaultAsync(m => m.GroupId == groupEntity.Id && m.PersonId == personEntity.Id,
                    cancellationToken)
                .ConfigureAwait(false);

            bool isNew = existingMembership == null;
            MembershipEntity entity = existingMembership ?? new MembershipEntity
            {
                MembershipIdValue = Guid.NewGuid().ToString(),
                GroupId = groupEntity.Id,
                PersonId = personEntity.Id,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            entity.Role = model.Role;
            entity.State = model.State;
            entity.StartDateTime = model.StartDateTime ?? entity.StartDateTime;
            entity.EndDateTime = model.EndDateTime ?? entity.EndDateTime;
            entity.ConsumerJson =
                model.Consumer != null ? JsonSerializer.Serialize(model.Consumer) : entity.ConsumerJson;
            entity.ExtJson = model.Ext != null ? JsonSerializer.Serialize(model.Ext) : entity.ExtJson;
            entity.ModifiedAt = DateTime.UtcNow;

            if (isNew) _dbContext.Memberships.Add(entity);

            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return isNew ? StatusCode(StatusCodes.Status201Created) : Ok();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}
