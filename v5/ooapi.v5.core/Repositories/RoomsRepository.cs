using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class RoomsRepository : BaseRepository<Room>, IRoomsRepository
{
    public RoomsRepository(ICoreDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Room?> GetRoomAsync(Guid roomId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Rooms.FirstOrDefaultAsync(x => x.RoomId.Equals(roomId), cancellationToken);
    }

    public async Task<Pagination<Room>> GetRoomsByBuildingIdAsync(Guid buildingId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        var set = dbContext.Rooms.Where(o => o.BuildingId.Equals(buildingId));

        return await GetAllOrderedByAsync(dataRequestParameters, set, cancellationToken);
    }
}