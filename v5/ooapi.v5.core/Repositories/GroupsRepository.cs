using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class GroupsRepository : BaseRepository<Group>, IGroupsRepository
{
    public GroupsRepository(ICoreDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Group?> GetGroupAsync(Guid groupId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Groups.FirstOrDefaultAsync(x => x.GroupId.Equals(groupId), cancellationToken);
    }

    public async Task<Pagination<Group>> GetGroupsByOrganizationIdAsync(Guid organizationId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        var set = dbContext.Groups.Where(o => o.OrganizationId.Equals(organizationId));

        return await GetAllOrderedByAsync(dataRequestParameters, set, cancellationToken);
    }

    public Task<Pagination<Group>> GetGroupsByPersonIdAsync(Guid personId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}