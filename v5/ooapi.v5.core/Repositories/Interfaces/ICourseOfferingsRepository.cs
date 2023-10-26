using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface ICourseOfferingsRepository
{
    Task<CourseOffering?> GetCourseOfferingAsync(Guid courseOfferingId, CancellationToken cancellationToken = default);
    Task<Pagination<CourseOffering>> GetCourseOfferingByCourseIdAsync(Guid courseId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
    Task<Pagination<CourseOffering>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, IQueryable<CourseOffering>? set = null, CancellationToken cancellationToken = default);
}