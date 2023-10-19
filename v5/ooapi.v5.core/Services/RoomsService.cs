using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

internal class RoomsService : ServiceBase, IRoomsService
{
    private readonly IRoomsRepository _repository;

    public RoomsService(ICoreDbContext dbContext, IRoomsRepository repository, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = repository;
    }

    public Pagination<Room> GetAll(DataRequestParameters dataRequestParameters)
    {
        return _repository.GetAllOrderedBy(dataRequestParameters);
    }

    public Room? Get(Guid roomId)
    {
        return _repository.GetRoom(roomId);
    }

    public Pagination<Room> GetRoomsByBuildingId(DataRequestParameters dataRequestParameters, Guid buildingId)
    {
        var result = _repository.GetRoomsByBuildingId(buildingId);
        return new Pagination<Room>(result.AsQueryable(), dataRequestParameters);
    }
}