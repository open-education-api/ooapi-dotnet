using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

/// <summary>
/// 
/// </summary>
public class ComponentOfferingsRepository : BaseRepository<ComponentOffering>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbContext"></param>
    public ComponentOfferingsRepository(CoreDBContext dbContext) : base(dbContext)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="courseOfferingId"></param>
    /// <returns></returns>
    public ComponentOffering? GetComponentOffering(Guid courseOfferingId)
    {
        return dbContext.ComponentOfferings.Include(x => x.Attributes).FirstOrDefault(x => x.OfferingId.Equals(courseOfferingId));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="componentId"></param>
    /// <param name="dataRequestParameters"></param>
    /// <returns></returns>
    public Pagination<ComponentOffering> GetComponentOfferingByProgramId(Guid componentId, DataRequestParameters dataRequestParameters)
    {
        IQueryable<ComponentOffering> set = dbContext.ComponentOfferingsNoTracking.Where(o => o.Component.Equals(componentId)).Include(x => x.Attributes);

        if (dataRequestParameters != null && !string.IsNullOrEmpty(dataRequestParameters.Consumer))
        {
            set = set.Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));
            return GetAllOrderedBy(dataRequestParameters, set);
        }

        return new Pagination<ComponentOffering>();
    }
}
