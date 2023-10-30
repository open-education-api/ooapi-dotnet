using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

public interface IComponentsService
{
    Task<Component?> GetAsync(Guid componentId, CancellationToken cancellationToken = default);
    Task<Pagination<Component>> GetComponentsByCourseIdAsync(DataRequestParameters dataRequestParameters, Guid courseId, CancellationToken cancellationToken = default);
    Task<Pagination<Component>> GetComponentsByOrganizationIdAsync(DataRequestParameters dataRequestParameters, Guid organizationId, CancellationToken cancellationToken = default);
}