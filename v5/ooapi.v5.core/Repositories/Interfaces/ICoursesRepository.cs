using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface ICoursesRepository
{
    Task<Course?> GetCourseAsync(Guid courseId, CancellationToken cancellationToken = default);
    Task<Pagination<Course>> GetCoursesByEducationSpecificationIdAsync(Guid educationSpecificationId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
    Task<List<Course>> GetCoursesByOrganizationIdAsync(Guid organizationId, CancellationToken cancellationToken = default);
    Task<List<Course>> GetCoursesByProgramIdAsync(Guid programId, CancellationToken cancellationToken = default);
    Task<Pagination<Course>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, IQueryable<Course>? set = null, CancellationToken cancellationToken = default);
}