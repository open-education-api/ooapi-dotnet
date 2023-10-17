using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces
{
    public interface ICoursesService
    {
        Course? Get(Guid courseId);
        Pagination<Course> GetAll(DataRequestParameters dataRequestParameters);
        Pagination<Course> GetCoursesByEducationSpecificationId(DataRequestParameters dataRequestParameters, Guid educationSpecificationId);
        Pagination<Course> GetCoursesByOrganizationId(DataRequestParameters dataRequestParameters, Guid organizationId);
        Pagination<Course> GetCoursesByProgramId(DataRequestParameters dataRequestParameters, Guid programId);
    }
}