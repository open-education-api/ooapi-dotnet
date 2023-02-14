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
        return dbContext.Courses.FirstOrDefault(x => x.CourseId.Equals(courseId));
    }

    public List<Course> GetCoursesByEducationSpecificationId(Guid educationSpecificationId)
    {
        return dbContext.Courses.Where(o => o.EducationSpecificationId.Equals(educationSpecificationId)).ToList();
    }

    public List<Course> GetCoursesByOrganizationId(Guid organizationId)
    {
        return dbContext.Courses.Where(o => o.OrganizationId.Equals(organizationId)).ToList();
    }

    public List<Course> GetCoursesByProgramId(Guid programId)
    {
        return null;
        //return dbContext.Courses.Where(o => o.Programs.Equals(programId)).ToList();
    }
}
