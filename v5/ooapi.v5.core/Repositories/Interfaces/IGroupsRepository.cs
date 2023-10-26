using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IGroupsRepository
{
    Task<Group?> GetGroupAsync(Guid groupId, CancellationToken cancellationToken = default);
    Task<List<Group>> GetGroupsByOrganizationIdAsync(Guid organizationId, CancellationToken cancellationToken = default);
    Task<List<Group>> GetGroupsByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default);

    Task<Pagination<Group>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, IQueryable<Group>? set = null,
        CancellationToken cancellationToken = default);
}