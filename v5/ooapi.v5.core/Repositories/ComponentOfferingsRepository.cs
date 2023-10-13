using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class ComponentOfferingsRepository : BaseRepository<ComponentOffering>
{
    public ComponentOfferingsRepository(ICoreDbContext dbContext) : base(dbContext)
    {
        //
    }

    public ComponentOffering GetComponentOffering(Guid courseOfferingId)
    {
        return dbContext.ComponentOfferings.Include(x => x.Attributes).FirstOrDefault(x => x.OfferingId.Equals(courseOfferingId));
    }

    public Pagination<ComponentOffering> GetComponentOfferingByProgramId(Guid componentId, DataRequestParameters dataRequestParameters)
    {
        IQueryable<ComponentOffering> set = dbContext.ComponentOfferingsNoTracking.Where(o => o.Component.Equals(componentId)).Include(x => x.Attributes);
        bool includeConsumer = dataRequestParameters != null && !String.IsNullOrEmpty(dataRequestParameters.Consumer);
        if (includeConsumer)
        {
            set = set.Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));
        }
        return GetAllOrderedBy(dataRequestParameters, set);
    }

}