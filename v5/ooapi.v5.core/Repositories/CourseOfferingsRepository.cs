using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class CourseOfferingsRepository : BaseRepository<CourseOffering>, ICourseOfferingsRepository
{
    public CourseOfferingsRepository(ICoreDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<CourseOffering?> GetCourseOfferingAsync(Guid courseOfferingId, CancellationToken cancellationToken = default)
    {
        return await dbContext.CourseOfferings.Include(x => x.Attributes).FirstOrDefaultAsync(x => x.OfferingId.Equals(courseOfferingId), cancellationToken);
    }

    public async Task<Pagination<CourseOffering>> GetCourseOfferingByCourseIdAsync(Guid courseId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        IQueryable<CourseOffering> set = dbContext.CourseOfferingsNoTracking.Where(o => o.CourseId.Equals(courseId)).Include(x => x.Attributes);
        if(!string.IsNullOrWhiteSpace(dataRequestParameters.Consumer))
        {
            set = set.Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));
        }
        return await GetAllOrderedByAsync(dataRequestParameters, set, cancellationToken);
    }
}