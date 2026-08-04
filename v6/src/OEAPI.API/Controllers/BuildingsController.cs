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
///     Controller for building endpoints.
/// </summary>
/// <remarks>
///     Initializes a new instance of the BuildingsController class.
/// </remarks>
/// <param name="dbContext">The database context.</param>
/// <param name="logger">The logger.</param>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
[Tags("Buildings")]
public class BuildingsController(OEAPIDbContext dbContext, ILogger<BuildingsController> logger) : GenericEntityController<BuildingEntity, Building>(dbContext, logger, "Building", "buildings")
{
    private readonly OEAPIDbContext _dbContext = dbContext;

    /// <summary>Retrieves a list of all buildings.</summary>
    /// <param name="primaryCode">Filter by primary code.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response for Scalar/OpenAPI doc generation -
    // TApiModel can't be referenced in an attribute on the generic base class (CS0416). Behaviour is
    // unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(PagedResult<Building>), StatusCodes.Status200OK)]
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

    /// <summary>Retrieves a single building by ID.</summary>
    /// <param name="buildingId">The building ID.</param>
    /// <param name="expand">
    ///     Ignored - required by the base <see cref="GenericEntityController{TEntity,TApiModel}.GetById" />
    ///     override signature. <c>Building.address</c> is always populated directly (see
    ///     <c>BuildingMappingExtensions</c>), so there's nothing left for this resource to expand.
    /// </param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response - see the GetAll override above.
    // Behaviour is unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(Building), StatusCodes.Status200OK)]
    [HttpGet("{buildingId}")]
    public override Task<IActionResult> GetById(
        string buildingId,
        string? expand,
        string? fields,
        string? consumer,
        CancellationToken cancellationToken = default)
    {
        return base.GetById(buildingId, expand, fields, consumer, cancellationToken);
    }

    /// <summary>
    ///     Finds a building by unique identifier (BuildingId). Routes through
    ///     <see cref="GenericEntityController{TEntity,TApiModel}.Entities" /> (not a raw
    ///     <c>_dbContext.Buildings</c> query) so <see cref="ApplyIncludes" /> applies to the single-item
    ///     lookup path too, not just <c>GetAll</c>.
    /// </summary>
    /// <param name="idValue">The building ID value.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the building entity or null.</returns>
    protected override async Task<BuildingEntity?> FindByUniqueIdAsync(string idValue,
        CancellationToken cancellationToken = default)
    {
        return await Entities
            .FirstOrDefaultAsync(b => b.BuildingId == idValue, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    ///     Eagerly loads Address so <c>MapToApiModel</c> can read it directly - see
    ///     <c>BuildingMappingExtensions.ToApiModel</c> for why this is unconditional, not expand-gated.
    /// </summary>
    protected override IQueryable<BuildingEntity> ApplyIncludes(IQueryable<BuildingEntity> query)
    {
        return query.Include(b => b.Address);
    }

    // ========================================================================
    // Required abstract method implementations
    // ========================================================================

    /// <summary>
    ///     Maps a BuildingEntity to a Building API model.
    /// </summary>
    /// <param name="entity">The entity to map.</param>
    /// <param name="consumer">The requested consumer, for consumer-specific data.</param>
    /// <returns>The API model.</returns>
    protected override Building MapToApiModel(BuildingEntity entity, string? consumer)
    {
        return entity.ToApiModel(consumer);
    }

    /// <summary>
    ///     Maps a Building API model to a BuildingEntity.
    /// </summary>
    /// <param name="model">The API model to map.</param>
    /// <returns>The entity.</returns>
    protected override BuildingEntity MapToEntity(Building model)
    {
        return model.ToEntity();
    }

    /// <summary>
    ///     Updates a BuildingEntity from a Building API model.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="model">The API model with new data.</param>
    protected override void UpdateEntityFromApiModel(BuildingEntity entity, Building model)
    {
        entity.UpdateFrom(model);
    }

    /// <summary>
    ///     Gets the unique identifier for a building entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>The identifier as string.</returns>
    protected override string GetEntityId(BuildingEntity entity)
    {
        return entity.BuildingId;
    }

    // ========================================================================
    // Custom methods for BuildingsController
    // ========================================================================

    /// <summary>
    ///     Applies default ordering for buildings: by code, then by name.
    /// </summary>
    /// <param name="query">The query to order.</param>
    /// <returns>The ordered query.</returns>
    protected override IQueryable<BuildingEntity> ApplyDefaultOrdering(IQueryable<BuildingEntity> query)
    {
        return query.OrderBy(b => b.PrimaryCode).ThenBy(b => b.NameJson);
    }

    /// <summary>
    ///     Applies the <c>q</c> (search.yaml, wire name "q") filter to a <c>GetAll</c> query.
    /// </summary>
    protected override IQueryable<BuildingEntity> ApplyEntityFilters(IQueryable<BuildingEntity> query)
    {
        string? searchValue = GetQueryValue("q");
        if (!string.IsNullOrEmpty(searchValue))
            query = query.WhereContains(searchValue, nameof(BuildingEntity.NameJson),
                nameof(BuildingEntity.PrimaryCode));

        return query;
    }

    // ========================================================================
    // Nested endpoints for buildings/{buildingId}/rooms
    // ========================================================================

    /// <summary>
    ///     Retrieves all rooms for a specific building.
    ///     GET /api/buildings/{buildingId}/rooms
    /// </summary>
    /// <remarks>
    ///     Get a list of all rooms in a building.
    /// </remarks>
    /// <param name="buildingId">The building ID.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="q">Search term.</param>
    /// <param name="roomType">Filter by room type.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{buildingId}/rooms")]
    [ProducesResponseType(typeof(PagedResult<Room>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRoomsByBuildingId(
        string buildingId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? consumer,
        [FromQuery(Name = "fields")] string? fields,
        [FromQuery] string? q,
        [FromQuery] string? roomType,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // First, verify the building exists
            BuildingEntity? buildingEntity = await _dbContext.Buildings
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BuildingId == buildingId, cancellationToken)
                .ConfigureAwait(false);

            if (buildingEntity == null)
                return Problem(
                    detail: $"Building with ID '{buildingId}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            // Get rooms for this building
            IQueryable<RoomEntity> query = _dbContext.Rooms
                .AsNoTracking()
                .Include(r => r.Building)
                .Where(r => r.BuildingEntityId != null && r.BuildingEntityId == buildingEntity.Id);

            // Apply room type filter - compare against the same defaulted value the response uses
            // (RoomMappingExtensions: null/empty -> "general_purpose"), not the raw nullable column.
            if (!string.IsNullOrEmpty(roomType))
                query = query.Where(r =>
                    (string.IsNullOrEmpty(r.RoomType) ? "general_purpose" : r.RoomType).ToLower() ==
                    roomType.ToLower());

            // Apply the spec's filter_query DSL
            query = ApplyFilterQuery(query);

            // Apply search if requested
            if (!string.IsNullOrEmpty(q))
                query = query.WhereContains(q, nameof(RoomEntity.NameJson), nameof(RoomEntity.PrimaryCode),
                    nameof(RoomEntity.RoomType));

            // Apply consumer filtering
            query = ConsumerKeyFilter.Apply(query, consumer);

            // Apply pagination
            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);

            int totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            // Apply default ordering
            query = query.OrderBy(r => r.PrimaryCode).ThenBy(r => r.NameJson);

            List<RoomEntity> items = await query
                .Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            List<Room> apiModels = [.. items.Select(item => item.ToApiModel(consumer))];

            // Create paged result
            PagedResult<Room> result = new(
                apiModels, totalCount, validatedPage, validatedPageSize);

            return PagedResponseWithFieldSelection(result, fields);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}
