using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface ICoursesRepository
{
    Task<Course?> GetCourseAsync(Guid courseId, CancellationToken cancellationToken = default);
    Task<Pagination<Course>> GetCoursesByEducationSpecificationIdAsync(Guid educationSpecificationId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
    Task<Pagination<Course>> GetCoursesByOrganizationIdAsync(Guid organizationId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
    Task<Pagination<Course>> GetCoursesByProgramIdAsync(Guid programId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
    Task<Pagination<Course>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, IQueryable<Course>? set = null, CancellationToken cancellationToken = default);
}