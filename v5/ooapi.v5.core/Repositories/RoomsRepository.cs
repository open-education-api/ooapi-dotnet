using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;


public class RoomsRepository : BaseRepository<Room>
{
    public RoomsRepository(ICoreDbContext dbContext) : base(dbContext)
    {
    }


    /// <param name="roomId"></param>
    /// <returns></returns>
    public Room? GetRoom(Guid roomId)
    {
        return dbContext.Rooms.FirstOrDefault(x => x.RoomId.Equals(roomId));
    }


    /// <param name="buildingId"></param>
    /// <returns></returns>
    public List<Room> GetRoomsByBuildingId(Guid buildingId)
    {
        return dbContext.Rooms.Where(o => o.BuildingId.Equals(buildingId)).ToList();
    }
}