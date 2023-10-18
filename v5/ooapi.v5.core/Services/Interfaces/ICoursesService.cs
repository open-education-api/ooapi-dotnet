using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;


public interface ICoursesService
{

    /// <param name="courseId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Course? Get(Guid courseId, out ErrorResponse? errorResponse);


    /// <param name="dataRequestParameters"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Pagination<Course>? GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse? errorResponse);


    /// <param name="dataRequestParameters"></param>
    /// <param name="educationSpecificationId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Pagination<Course>? GetCoursesByEducationSpecificationId(DataRequestParameters dataRequestParameters, Guid educationSpecificationId, out ErrorResponse? errorResponse);


    /// <param name="dataRequestParameters"></param>
    /// <param name="organizationId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Pagination<Course>? GetCoursesByOrganizationId(DataRequestParameters dataRequestParameters, Guid organizationId, out ErrorResponse? errorResponse);


    /// <param name="dataRequestParameters"></param>
    /// <param name="programId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Pagination<Course>? GetCoursesByProgramId(DataRequestParameters dataRequestParameters, Guid programId, out ErrorResponse? errorResponse);
}