using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface ICourseOfferingsRepository
{
    CourseOffering? GetCourseOffering(Guid courseOfferingId);
    Pagination<CourseOffering> GetCourseOfferingByCourseId(Guid courseId, DataRequestParameters dataRequestParameters);
    Pagination<CourseOffering> GetAllOrderedBy(DataRequestParameters dataRequestParameters, IQueryable<CourseOffering>? set = null);
}