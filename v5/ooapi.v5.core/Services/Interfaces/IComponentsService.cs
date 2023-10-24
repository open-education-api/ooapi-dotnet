using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

public interface IComponentsService
{
    Component? Get(Guid componentId);
    Pagination<Component> GetComponentsByCourseId(DataRequestParameters dataRequestParameters, Guid courseId);
    Pagination<Component> GetComponentsByOrganizationId(DataRequestParameters dataRequestParameters, Guid organizationId);
}