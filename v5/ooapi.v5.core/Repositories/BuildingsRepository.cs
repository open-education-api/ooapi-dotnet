using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class BuildingsRepository : BaseRepository<Building>, IBuildingsRepository
{
    public BuildingsRepository(ICoreDbContext dbContext) : base(dbContext)
    {
    }


    public async Task<Building?> GetBuildingAsync(Guid buildingId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Buildings.Include(x => x.Address).Include(x => x.Attributes).Include(x => x.OtherCodes).FirstOrDefaultAsync(x => x.BuildingId.Equals(buildingId), cancellationToken);
    }



    public async Task<Pagination<Building>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        IQueryable<Building> set = dbContext.Set<Building>().Include(x => x.Address).Include(x => x.Attributes).Include(x => x.OtherCodes).AsQueryable();
        return await GetAllOrderedByAsync(dataRequestParameters, set, cancellationToken);
    }
}