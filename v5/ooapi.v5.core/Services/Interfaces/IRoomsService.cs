using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces
{
    public interface IRoomsService
    {
        Room Get(Guid roomId, out ErrorResponse errorResponse);
        Pagination<Room> GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse errorResponse);
        Pagination<Room> GetRoomsByBuildingId(DataRequestParameters dataRequestParameters, Guid buildingId, out ErrorResponse errorResponse);
    }
}