using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

public interface IRoomsService
{
    Task<Room?> GetAsync(Guid roomId, CancellationToken cancellationToken = default);
    Task<Pagination<Room>> GetAllAsync(DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
    Task<Pagination<Room>> GetRoomsByBuildingIdAsync(DataRequestParameters dataRequestParameters, Guid buildingId, CancellationToken cancellationToken = default);
}