using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

public interface IGroupsService
{
    Task<Group?> GetAsync(Guid groupId, CancellationToken cancellationToken = default);
    Task<Pagination<Group>> GetAllAsync(DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
    Task<Pagination<Group>> GetGroupsByOrganizationIdAsync(DataRequestParameters dataRequestParameters, Guid organizationId, CancellationToken cancellationToken = default);
    Task<Pagination<Group>> GetGroupsByPersonIdAsync(DataRequestParameters dataRequestParameters, Guid personId, CancellationToken cancellationToken = default);
}