using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces
{
    public interface IComponentsService
    {
        Component Get(Guid componentId, out ErrorResponse errorResponse);
        Pagination<Component> GetComponentsByCourseId(DataRequestParameters dataRequestParameters, Guid courseId, out ErrorResponse errorResponse);
        Pagination<Component> GetComponentsByOrganizationId(DataRequestParameters dataRequestParameters, Guid organizationId, out ErrorResponse errorResponse);
    }
}