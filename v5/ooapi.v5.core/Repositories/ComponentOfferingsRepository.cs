using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class ComponentOfferingsRepository : BaseRepository<ComponentOffering>, IComponentOfferingsRepository
{
    public ComponentOfferingsRepository(ICoreDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<ComponentOffering?> GetComponentOfferingAsync(Guid courseOfferingId, CancellationToken cancellationToken = default)
    {
        return await dbContext.ComponentOfferings.Include(x => x.Attributes).Include(x => x.OtherCodes).FirstOrDefaultAsync(x => x.OfferingId.Equals(courseOfferingId), cancellationToken);
    }

    public async Task<Pagination<ComponentOffering>> GetComponentOfferingByComponentIdAsync(Guid componentId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        IQueryable<ComponentOffering> set = dbContext.ComponentOfferingsNoTracking.Where(o => o.ComponentId.Equals(componentId)).Include(x => x.Attributes).Include(x => x.OtherCodes).Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));

        return await GetAllOrderedByAsync(dataRequestParameters, set, cancellationToken);
    }
}