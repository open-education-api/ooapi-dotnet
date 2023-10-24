using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface ICourseOfferingsRepository
{
    CourseOffering? GetCourseOffering(Guid courseOfferingId);
    Task<Pagination<CourseOffering>> GetCourseOfferingByCourseIdAsync(Guid courseId, DataRequestParameters dataRequestParameters);
    Task<Pagination<CourseOffering>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, IQueryable<CourseOffering>? set = null, CancellationToken cancellationToken = default);
}