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

    public async Task<Pagination<Room>> GetAllAsync(DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        return await _repository.GetAllOrderedByAsync(dataRequestParameters, null, cancellationToken);
    }

    public Task<Room?> GetAsync(Guid roomId, CancellationToken cancellationToken = default)
    {
        return _repository.GetRoomAsync(roomId, cancellationToken);
    }

    public async Task<Pagination<Room>> GetRoomsByBuildingIdAsync(DataRequestParameters dataRequestParameters, Guid buildingId, CancellationToken cancellationToken = default)
    {
        return await _repository.GetRoomsByBuildingIdAsync(buildingId, dataRequestParameters, cancellationToken);
    }
}