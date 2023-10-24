using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

public interface IRoomsService
{
    Room? Get(Guid roomId);
    Task<Pagination<Room>> GetAll(DataRequestParameters dataRequestParameters);
    Pagination<Room> GetRoomsByBuildingId(DataRequestParameters dataRequestParameters, Guid buildingId);
}