using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;


public class GroupsRepository : BaseRepository<Group>
{

    /// <param name="dbContext"></param>
    public GroupsRepository(CoreDBContext dbContext) : base(dbContext)
    {
    }


    /// <param name="groupId"></param>
    /// <returns></returns>
    public Group? GetGroup(Guid groupId)
    {
        return dbContext.Groups.FirstOrDefault(x => x.GroupId.Equals(groupId));
    }


    /// <param name="organizationId"></param>
    /// <returns></returns>
    public List<Group> GetGroupsByOrganizationId(Guid organizationId)
    {
        return dbContext.Groups.Where(o => o.OrganizationId.Equals(organizationId)).ToList();
    }


    /// <param name="personId"></param>
    /// <returns></returns>
    public List<Group> GetGroupsByPersonId(Guid personId)
    {
        return new List<Group>();
        // TODO
    }
}
