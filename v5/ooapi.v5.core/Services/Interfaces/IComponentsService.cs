using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;


public interface IComponentsService
{

    /// <param name="componentId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Component? Get(Guid componentId, out ErrorResponse? errorResponse);


    /// <param name="dataRequestParameters"></param>
    /// <param name="courseId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Pagination<Component>? GetComponentsByCourseId(DataRequestParameters dataRequestParameters, Guid courseId, out ErrorResponse? errorResponse);


    /// <param name="dataRequestParameters"></param>
    /// <param name="organizationId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Pagination<Component>? GetComponentsByOrganizationId(DataRequestParameters dataRequestParameters, Guid organizationId, out ErrorResponse? errorResponse);
}