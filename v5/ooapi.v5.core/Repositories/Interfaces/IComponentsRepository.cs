using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IComponentsRepository
{
    Component? GetComponent(Guid componentId);
    List<Component> GetComponentsByCourseId(Guid courseId);
    List<Component> GetComponentsByOrganizationId(Guid organizationId);
    Task<Pagination<Component>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, IQueryable<Component>? set = null, CancellationToken cancellationToken = default);
}