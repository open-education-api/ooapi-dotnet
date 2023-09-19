//using Microsoft.EntityFrameworkCore;
//using ooapi.v5.core.Utility;
//using ooapi.v5.Models;

//namespace ooapi.v5.core.Repositories;

//public class OfferingsRepository : BaseRepository<Offering>
//{
//    public OfferingsRepository(CoreDBContext dbContext) : base(dbContext)
//    {
//        //
//    }

//    public Offering GetOffering(Guid programOfferingId)
//    {
//        return dbContext.ProgramOfferings.Include(x => x.Attributes).FirstOrDefault(x => x.OfferingId.Equals(programOfferingId));
//    }

//    public Pagination<ProgramOffering> GetProgramOfferingByProgramId(Guid programId, DataRequestParameters dataRequestParameters)
//    {
//        IQueryable<ProgramOffering> set = dbContext.ProgramOfferingsNoTracking.Where(o => o.ProgramId.Equals(programId)).Include(x => x.Attributes);
//        bool includeConsumer = dataRequestParameters != null && !String.IsNullOrEmpty(dataRequestParameters.Consumer);
//        if (includeConsumer)
//        {
//            set = set.Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));
//        }
//        return GetAllOrderedBy(dataRequestParameters, set);
//    }

//}
