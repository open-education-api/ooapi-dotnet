using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Repositories.Interfaces;
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

    public async Task<List<Room>> GetRoomsByBuildingIdAsync(Guid buildingId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Rooms.Where(o => o.BuildingId.Equals(buildingId)).ToListAsync(cancellationToken);
    }
}