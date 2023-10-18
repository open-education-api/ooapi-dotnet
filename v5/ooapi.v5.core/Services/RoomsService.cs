using ooapi.v5.core.Repositories;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;


public class RoomsService : ServiceBase, IRoomsService
{
    private readonly RoomsRepository _repository;

    public RoomsService(CoreDBContext dbContext, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = new RoomsRepository(dbContext);
    }


    /// <param name="dataRequestParameters"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    public Pagination<Room>? GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse? errorResponse)
    {
        try
        {
            var result = _repository.GetAllOrderedBy(dataRequestParameters);
            errorResponse = null;
            return result;
        }
        catch (Exception)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }


    /// <param name="roomId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    public Room? Get(Guid roomId, out ErrorResponse? errorResponse)
    {
        try
        {
            var item = _repository.GetRoom(roomId);

            errorResponse = null;
            return item;
        }
        catch (Exception)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }


    /// <param name="dataRequestParameters"></param>
    /// <param name="buildingId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    public Pagination<Room>? GetRoomsByBuildingId(DataRequestParameters dataRequestParameters, Guid buildingId, out ErrorResponse? errorResponse)
    {
        try
        {
            var result = _repository.GetRoomsByBuildingId(buildingId);
            var paginationResult = new Pagination<Room>(result.AsQueryable(), dataRequestParameters);
            errorResponse = null;
            return paginationResult;
        }
        catch (Exception)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }
}
