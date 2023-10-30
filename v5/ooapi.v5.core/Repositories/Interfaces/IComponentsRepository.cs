using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IComponentsRepository
{
    Task<Component?> GetComponentAsync(Guid componentId, CancellationToken cancellationToken = default);
    Task<Pagination<Component>> GetComponentsByCourseIdAsync(Guid courseId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
    Task<Pagination<Component>> GetComponentsByOrganizationIdAsync(Guid organizationId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
    Task<Pagination<Component>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, IQueryable<Component>? set = null, CancellationToken cancellationToken = default);
}