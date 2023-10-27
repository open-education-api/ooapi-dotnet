using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IGroupsRepository
{
    Task<Group?> GetGroupAsync(Guid groupId, CancellationToken cancellationToken = default);
    Task<Pagination<Group>> GetGroupsByOrganizationIdAsync(Guid organizationId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
    Task<Pagination<Group>> GetGroupsByPersonIdAsync(Guid personId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);

    Task<Pagination<Group>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, IQueryable<Group>? set = null,
        CancellationToken cancellationToken = default);
}