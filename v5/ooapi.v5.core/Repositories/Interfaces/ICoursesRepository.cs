using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface ICoursesRepository
{
    Course? GetCourse(Guid courseId);
    Pagination<Course> GetCoursesByEducationSpecificationId(Guid educationSpecificationId, DataRequestParameters dataRequestParameters);
    List<Course> GetCoursesByOrganizationId(Guid organizationId);
    List<Course> GetCoursesByProgramId(Guid programId);
    Pagination<Course> GetAllOrderedBy(DataRequestParameters dataRequestParameters, IQueryable<Course>? set = null);
}