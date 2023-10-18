using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

/// <summary>
/// 
/// </summary>
public class ProgramOfferingsRepository : BaseRepository<ProgramOffering>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbContext"></param>
    public ProgramOfferingsRepository(CoreDBContext dbContext) : base(dbContext)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="programOfferingId"></param>
    /// <returns></returns>
    public ProgramOffering? GetProgramOffering(Guid programOfferingId)
    {
        return dbContext.ProgramOfferings.Include(x => x.Attributes).FirstOrDefault(x => x.OfferingId.Equals(programOfferingId));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="programId"></param>
    /// <param name="dataRequestParameters"></param>
    /// <returns></returns>
    public Pagination<ProgramOffering>? GetProgramOfferingByProgramId(Guid programId, DataRequestParameters dataRequestParameters)
    {
        IQueryable<ProgramOffering> set = dbContext.ProgramOfferingsNoTracking.Where(o => o.ProgramId.Equals(programId)).Include(x => x.Attributes);
        if (dataRequestParameters != null && !string.IsNullOrEmpty(dataRequestParameters.Consumer))
        {
            set = set.Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));
            return GetAllOrderedBy(dataRequestParameters, set);
        }

        return null;
    }

}
