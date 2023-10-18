using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;


public class CoursesRepository : BaseRepository<Course>
{

    /// <param name="dbContext"></param>
    public CoursesRepository(CoreDBContext dbContext) : base(dbContext)
    {
    }


    /// <param name="courseId"></param>
    /// <returns></returns>
    public Course? GetCourse(Guid courseId)
    {
        return dbContext.Courses.Include(x => x.Attributes).FirstOrDefault(x => x.CourseId.Equals(courseId));
    }


    /// <param name="educationSpecificationId"></param>
    /// <param name="dataRequestParameters"></param>
    /// <returns></returns>
    public Pagination<Course> GetCoursesByEducationSpecificationId(Guid educationSpecificationId, DataRequestParameters dataRequestParameters)
    {
        IQueryable<Course> set = dbContext.CoursesNoTracking.Where(o => o.EducationSpecificationId.Equals(educationSpecificationId)).Include(x => x.Attributes);
        if (dataRequestParameters != null && !string.IsNullOrEmpty(dataRequestParameters.Consumer))
        {
            set = set.Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));
            return GetAllOrderedBy(dataRequestParameters, set);
        }

        return new Pagination<Course>();
    }


    /// <param name="organizationId"></param>
    /// <returns></returns>
    public List<Course> GetCoursesByOrganizationId(Guid organizationId)
    {
        return dbContext.Courses.Include(x => x.Attributes).Where(o => o.OrganizationId.Equals(organizationId)).ToList();
    }


    /// <param name="programId"></param>
    /// <returns></returns>
    public List<Course> GetCoursesByProgramId(Guid programId)
    {
        return new List<Course>();
    }
}
