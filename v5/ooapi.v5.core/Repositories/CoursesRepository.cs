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

}
