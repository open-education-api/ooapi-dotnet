using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class ProgramOfferingsRepository : BaseRepository<ProgramOffering>, IProgramOfferingsRepository
{
    public ProgramOfferingsRepository(ICoreDbContext dbContext) : base(dbContext)
    {
    }

    public ProgramOffering? GetProgramOffering(Guid programOfferingId)
    {
        return dbContext.ProgramOfferings.Include(x => x.Attributes).FirstOrDefault(x => x.OfferingId.Equals(programOfferingId));
    }

    public Pagination<ProgramOffering> GetProgramOfferingByProgramId(Guid programId, DataRequestParameters dataRequestParameters)
    {
        IQueryable<ProgramOffering> set = dbContext.ProgramOfferingsNoTracking.Where(o => o.ProgramId.Equals(programId)).Include(x => x.Attributes);
        if (!string.IsNullOrEmpty(dataRequestParameters.Consumer))
        {
            set = set.Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));
        }

        return GetAllOrderedBy(dataRequestParameters, set);
    }
}