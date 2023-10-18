using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;


public class GroupsRepository : BaseRepository<Group>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbContext"></param>
    public GroupsRepository(CoreDBContext dbContext) : base(dbContext)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="groupId"></param>
    /// <returns></returns>
    public Group? GetGroup(Guid groupId)
    {
        return dbContext.Groups.FirstOrDefault(x => x.GroupId.Equals(groupId));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="organizationId"></param>
    /// <returns></returns>
    public List<Group> GetGroupsByOrganizationId(Guid organizationId)
    {
        return dbContext.Groups.Where(o => o.OrganizationId.Equals(organizationId)).ToList();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="personId"></param>
    /// <returns></returns>
    public List<Group> GetGroupsByPersonId(Guid personId)
    {
        return new List<Group>();
        // TODO
    }
}
