using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;


public class RoomsRepository : BaseRepository<Room>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbContext"></param>
    public RoomsRepository(CoreDBContext dbContext) : base(dbContext)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    public Room? GetRoom(Guid roomId)
    {
        return dbContext.Rooms.FirstOrDefault(x => x.RoomId.Equals(roomId));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buildingId"></param>
    /// <returns></returns>
    public List<Room> GetRoomsByBuildingId(Guid buildingId)
    {
        return dbContext.Rooms.Where(o => o.BuildingId.Equals(buildingId)).ToList();
    }
}
