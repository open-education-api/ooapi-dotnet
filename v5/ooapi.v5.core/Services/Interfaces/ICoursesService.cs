using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

public interface ICoursesService
{
    Task<Course?> GetAsync(Guid courseId, CancellationToken cancellationToken = default);
    Task<Pagination<Course>> GetAllAsync(DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
    Task<Pagination<Course>> GetCoursesByEducationSpecificationIdAsync(DataRequestParameters dataRequestParameters, Guid educationSpecificationId, CancellationToken cancellationToken = default);
    Task<Pagination<Course>> GetCoursesByOrganizationIdAsync(DataRequestParameters dataRequestParameters, Guid organizationId, CancellationToken cancellationToken = default);
    Task<Pagination<Course>> GetCoursesByProgramIdAsync(DataRequestParameters dataRequestParameters, Guid programId, CancellationToken cancellationToken = default);
}