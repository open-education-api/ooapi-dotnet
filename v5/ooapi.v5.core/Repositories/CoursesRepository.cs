using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class CoursesRepository : BaseRepository<Course>, ICoursesRepository
{
    public CoursesRepository(ICoreDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Course?> GetCourseAsync(Guid courseId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Courses.Include(x => x.Attributes).FirstOrDefaultAsync(x => x.CourseId.Equals(courseId), cancellationToken);
    }


    public async Task<Pagination<Course>> GetCoursesByEducationSpecificationIdAsync(Guid educationSpecificationId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        IQueryable<Course> set = dbContext.CoursesNoTracking.Where(o => o.EducationSpecificationId.Equals(educationSpecificationId)).Include(x => x.Attributes);
        if (!string.IsNullOrEmpty(dataRequestParameters.Consumer))
        {
            set = set.Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));
            
        }

        return await GetAllOrderedByAsync(dataRequestParameters, set, cancellationToken);
    }

    public Task<List<Course>> GetCoursesByOrganizationIdAsync(Guid organizationId, CancellationToken cancellationToken = default)
    {
        return dbContext.Courses.Include(x => x.Attributes).Where(o => o.OrganizationId.Equals(organizationId)).ToListAsync(cancellationToken);
    }

    public async Task<List<Course>> GetCoursesByProgramIdAsync(Guid programId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}