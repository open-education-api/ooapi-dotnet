using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Repositories.Interfaces;
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

    public async Task<List<Group>> GetGroupsByOrganizationIdAsync(Guid organizationId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Groups.Where(o => o.OrganizationId.Equals(organizationId)).ToListAsync(cancellationToken);
    }

    public async Task<List<Group>> GetGroupsByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}