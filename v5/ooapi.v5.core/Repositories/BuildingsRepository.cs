using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;


public class BuildingsRepository : BaseRepository<Building>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbContext"></param>
    public BuildingsRepository(CoreDBContext dbContext) : base(dbContext)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buildingId"></param>
    /// <returns></returns>
    public Building? GetBuilding(Guid buildingId)
    {
        return dbContext.Buildings.Include(x => x.Address).Include(x => x.Attributes).FirstOrDefault(x => x.BuildingId.Equals(buildingId));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataRequestParameters"></param>
    /// <returns></returns>
    public Pagination<Building> GetAllOrderedBy(DataRequestParameters dataRequestParameters)
    {
        IQueryable<Building> set = dbContext.Set<Building>().Include(x => x.Address).Include(x => x.Attributes).AsQueryable();
        return GetAllOrderedBy(dataRequestParameters, set);
    }
}
