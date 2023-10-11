using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces
{
    public interface ICoursesService
    {
        Course Get(Guid courseId, out ErrorResponse errorResponse);
        Pagination<Course> GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse errorResponse);
        Pagination<Course> GetCoursesByEducationSpecificationId(DataRequestParameters dataRequestParameters, Guid educationSpecificationId, out ErrorResponse errorResponse);
        Pagination<Course> GetCoursesByOrganizationId(DataRequestParameters dataRequestParameters, Guid organizationId, out ErrorResponse errorResponse);
        Pagination<Course> GetCoursesByProgramId(DataRequestParameters dataRequestParameters, Guid programId, out ErrorResponse errorResponse);
    }
}