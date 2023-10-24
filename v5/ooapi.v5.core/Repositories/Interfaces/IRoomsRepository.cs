using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IRoomsRepository
{
    Room? GetRoom(Guid roomId);
    List<Room> GetRoomsByBuildingId(Guid buildingId);
    Task<Pagination<Room>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, IQueryable<Room>? set = null, CancellationToken cancellationToken = default);
}