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


    public async Task<Pagination<ProgramOffering>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        IQueryable<ProgramOffering> set = dbContext.ProgramOfferingsNoTracking.Include(x => x.Attributes).Include(x => x.OtherCodes);
        if (!string.IsNullOrEmpty(dataRequestParameters.Consumer))
        {
            set = set.Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));
        }

        return await GetAllOrderedByAsync(dataRequestParameters, set, cancellationToken);
    }


    public async Task<ProgramOffering?> GetProgramOfferingAsync(Guid programOfferingId, CancellationToken cancellationToken = default)
    {
        return await dbContext.ProgramOfferings.Include(x => x.Attributes).Include(x => x.OtherCodes).FirstOrDefaultAsync(x => x.OfferingId.Equals(programOfferingId), cancellationToken);
    }

    public async Task<Pagination<ProgramOffering>> GetProgramOfferingsByProgramIdAsync(Guid programId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        IQueryable<ProgramOffering> set = dbContext.ProgramOfferingsNoTracking.Where(o => o.ProgramId.Equals(programId)).Include(x => x.Attributes).Include(x => x.OtherCodes);

        if (!string.IsNullOrEmpty(dataRequestParameters.Consumer))
        {
            set = set.Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));
        }

        return await GetAllOrderedByAsync(dataRequestParameters, set, cancellationToken);
    }
}