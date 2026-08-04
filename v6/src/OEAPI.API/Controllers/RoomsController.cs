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
///     Controller for room endpoints.
/// </summary>
/// <remarks>
///     Initializes a new instance of the RoomsController class.
/// </remarks>
/// <param name="dbContext">The database context.</param>
/// <param name="logger">The logger.</param>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
[Tags("Rooms")]
public class RoomsController(OEAPIDbContext dbContext, ILogger<RoomsController> logger) : GenericEntityController<RoomEntity, Room>(dbContext, logger, "Room", "rooms")
{
    private readonly OEAPIDbContext _dbContext = dbContext;

    /// <summary>Retrieves a list of all rooms.</summary>
    /// <param name="primaryCode">Filter by primary code.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response for Scalar/OpenAPI doc generation -
    // TApiModel can't be referenced in an attribute on the generic base class (CS0416). Behaviour is
    // unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(PagedResult<Room>), StatusCodes.Status200OK)]
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

    /// <summary>Retrieves a single room by ID.</summary>
    /// <param name="roomId">The room ID.</param>
    /// <param name="expand">Comma-separated list of relations to expand, e.g. <c>building</c>.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response - see the GetAll override above.
    // Behaviour is unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]
    [HttpGet("{roomId}")]
    public override Task<IActionResult> GetById(
        string roomId,
        string? expand,
        string? fields,
        string? consumer,
        CancellationToken cancellationToken = default)
    {
        return base.GetById(roomId, expand, fields, consumer, cancellationToken);
    }

    /// <summary>
    ///     Finds a room by unique identifier (RoomId). Routes through
    ///     <see cref="GenericEntityController{TEntity,TApiModel}.Entities" /> (not a raw
    ///     <c>_dbContext.Rooms</c> query) so <see cref="ApplyIncludes" /> applies to the single-item
    ///     lookup path too, not just <c>GetAll</c>.
    /// </summary>
    /// <param name="idValue">The room ID value.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the room entity or null.</returns>
    protected override async Task<RoomEntity?> FindByUniqueIdAsync(string idValue,
        CancellationToken cancellationToken = default)
    {
        return await Entities
            .FirstOrDefaultAsync(r => r.RoomId == idValue, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    ///     Eagerly loads Building so <c>MapToApiModel</c> can read it directly from the real FK
    ///     relationship.
    /// </summary>
    protected override IQueryable<RoomEntity> ApplyIncludes(IQueryable<RoomEntity> query)
    {
        return query.Include(r => r.Building);
    }

    // Create/Update/Delete removed: the spec defines no write operations at all for
    // /rooms.

    // ========================================================================
    // Required abstract method implementations
    // ========================================================================

    /// <summary>
    ///     Maps a RoomEntity to a Room API model.
    /// </summary>
    /// <param name="entity">The entity to map.</param>
    /// <param name="consumer">The requested consumer, for consumer-specific data.</param>
    /// <returns>The API model.</returns>
    protected override Room MapToApiModel(RoomEntity entity, string? consumer)
    {
        return entity.ToApiModel(consumer);
    }

    /// <summary>
    ///     Maps a Room API model to a RoomEntity.
    /// </summary>
    /// <param name="model">The API model to map.</param>
    /// <returns>The entity.</returns>
    protected override RoomEntity MapToEntity(Room model)
    {
        return model.ToEntity();
    }

    /// <summary>
    ///     Updates a RoomEntity from a Room API model.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="model">The API model with new data.</param>
    protected override void UpdateEntityFromApiModel(RoomEntity entity, Room model)
    {
        entity.UpdateFrom(model);
    }

    /// <summary>
    ///     Gets the unique identifier for a room entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>The identifier as string.</returns>
    protected override string GetEntityId(RoomEntity entity)
    {
        return entity.RoomId;
    }

    // ========================================================================
    // Override methods for custom behaviour
    // ========================================================================

    /// <summary>
    ///     Applies default ordering for rooms: by primary code, then by name.
    /// </summary>
    /// <param name="query">The query to order.</param>
    /// <returns>The ordered query.</returns>
    protected override IQueryable<RoomEntity> ApplyDefaultOrdering(IQueryable<RoomEntity> query)
    {
        return query.OrderBy(r => r.PrimaryCode).ThenBy(r => r.NameJson);
    }

    /// <summary>
    ///     Applies the <c>q</c> (search.yaml, wire name "q") and <c>roomType</c> filters to a
    ///     <c>GetAll</c> query.
    /// </summary>
    protected override IQueryable<RoomEntity> ApplyEntityFilters(IQueryable<RoomEntity> query)
    {
        string? searchValue = GetQueryValue("q");
        if (!string.IsNullOrEmpty(searchValue))
            query = query.WhereContains(searchValue, nameof(RoomEntity.NameJson), nameof(RoomEntity.PrimaryCode),
                nameof(RoomEntity.RoomType));

        string? roomType = GetQueryValue("roomType");
        if (!string.IsNullOrEmpty(roomType))
            // Compare against the same defaulted value the response uses (RoomMappingExtensions:
            // null/empty -> "general_purpose"), not the raw nullable column.
            query = query.Where(r =>
                (string.IsNullOrEmpty(r.RoomType) ? "general_purpose" : r.RoomType) == roomType);

        return query;
    }

    /// <summary>
    ///     Applies expand to a room API model (includes related entities).
    /// </summary>
    /// <param name="apiModel">The API model to expand.</param>
    /// <param name="expand">The expand parameter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the expanded API model.</returns>
    protected override async Task<Room> ApplyExpandAsync(
        Room apiModel,
        string expand,
        CancellationToken cancellationToken = default)
    {
        string[] expandOptions = expand.Split([','], StringSplitOptions.RemoveEmptyEntries);

        foreach (string option in expandOptions)
            switch (option.Trim().ToLowerInvariant())
            {
                case "building":
                    if (apiModel.BuildingId != null && apiModel.Building == null)
                    {
                        BuildingEntity? buildingEntity = await _dbContext.Buildings
                            .AsNoTracking()
                            .Include(b => b.Address)
                            .FirstOrDefaultAsync(b =>
                                    b.BuildingId == apiModel.BuildingId.Value ||
                                    b.Id.ToString() == apiModel.BuildingId.Value,
                                cancellationToken)
                            .ConfigureAwait(false);

                        if (buildingEntity != null) apiModel.Building = buildingEntity.ToApiModel(null);
                    }

                    break;
            }

        return apiModel;
    }
}
