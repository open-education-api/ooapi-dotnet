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

    public async Task<Pagination<Course>> GetCoursesByOrganizationIdAsync(Guid organizationId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        var set = dbContext.Courses.Include(x => x.Attributes).Where(o => o.OrganizationId.Equals(organizationId));

        return await GetAllOrderedByAsync(dataRequestParameters, set, cancellationToken);
    }

    public Task<Pagination<Course>> GetCoursesByProgramIdAsync(Guid programId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}