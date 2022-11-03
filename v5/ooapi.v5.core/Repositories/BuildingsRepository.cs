using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class BuildingsRepository : BaseRepository<Building>
{
    public BuildingsRepository(CoreDBContext dbContext) : base(dbContext)
    {
        //
    }

    public Building GetBuilding(string buildingId)
    {
        return dbContext.Buildings.FirstOrDefault(x => x.BuildingId.Equals(buildingId));
    }

}
