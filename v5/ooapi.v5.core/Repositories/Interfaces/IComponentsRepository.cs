using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IComponentsRepository
{
    Task<Component?> GetComponentAsync(Guid componentId, CancellationToken cancellationToken = default);
    Task<List<Component>> GetComponentsByCourseIdAsync(Guid courseId, CancellationToken cancellationToken = default);
    Task<List<Component>> GetComponentsByOrganizationIdAsync(Guid organizationId, CancellationToken cancellationToken = default);
    Task<Pagination<Component>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, IQueryable<Component>? set = null, CancellationToken cancellationToken = default);
}