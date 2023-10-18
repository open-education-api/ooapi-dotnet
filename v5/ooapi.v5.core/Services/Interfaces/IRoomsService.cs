using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;


public interface IRoomsService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="roomId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Room? Get(Guid roomId, out ErrorResponse? errorResponse);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataRequestParameters"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Pagination<Room>? GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse? errorResponse);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataRequestParameters"></param>
    /// <param name="buildingId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Pagination<Room>? GetRoomsByBuildingId(DataRequestParameters dataRequestParameters, Guid buildingId, out ErrorResponse? errorResponse);
}