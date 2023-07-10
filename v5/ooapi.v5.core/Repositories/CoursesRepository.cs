using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class CoursesRepository : BaseRepository<Course>
{
    public CoursesRepository(CoreDBContext dbContext) : base(dbContext)
    {
        //
    }

    public Course GetCourse(Guid courseId)
    {
        return dbContext.Courses.Include(x => x.Attributes).FirstOrDefault(x => x.CourseId.Equals(courseId));
    }

    public Pagination<Course> GetCoursesByEducationSpecificationId(Guid educationSpecificationId, DataRequestParameters dataRequestParameters)
    {
        IQueryable<Course> set = dbContext.CoursesNoTracking.Where(o => o.EducationSpecificationId.Equals(educationSpecificationId)).Include(x => x.Attributes);
        bool includeConsumer = dataRequestParameters != null && !String.IsNullOrEmpty(dataRequestParameters.Consumer);
        if (includeConsumer)
        {
            set = set.Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));
        }
        return GetAllOrderedBy(dataRequestParameters, set);
    }

    public List<Course> GetCoursesByOrganizationId(Guid organizationId)
    {
        return dbContext.Courses.Include(x => x.Attributes).Where(o => o.OrganizationId.Equals(organizationId)).ToList();
    }

    public List<Course> GetCoursesByProgramId(Guid programId)
    {
        return null;
        //return dbContext.Courses.Where(o => o.Programs.Equals(programId)).ToList();
    }
}
