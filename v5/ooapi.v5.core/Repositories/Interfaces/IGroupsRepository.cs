using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IGroupsRepository
{
    Group? GetGroup(Guid groupId);
    List<Group> GetGroupsByOrganizationId(Guid organizationId);
    List<Group> GetGroupsByPersonId(Guid personId);

    Task<Pagination<Group>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, IQueryable<Group>? set = null,
        CancellationToken cancellationToken = default);
}