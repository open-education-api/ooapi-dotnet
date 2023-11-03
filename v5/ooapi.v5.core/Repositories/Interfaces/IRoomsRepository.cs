using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IRoomsRepository
{
    Task<Room?> GetRoomAsync(Guid roomId, CancellationToken cancellationToken = default);
    Task<Pagination<Room>> GetRoomsByBuildingIdAsync(Guid buildingId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
    Task<Pagination<Room>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, IQueryable<Room>? set = null, CancellationToken cancellationToken = default);
}