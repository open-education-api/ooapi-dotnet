using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class GroupsRepository : BaseRepository<Group>
{
    public GroupsRepository(CoreDBContext dbContext) : base(dbContext)
    {
        //
    }

    public Group GetGroup(Guid groupId)
    {
        return dbContext.Groups.FirstOrDefault(x => x.GroupId.Equals(groupId));
    }

    public List<Group> GetGroupsByOrganizationId(Guid organizationId)
    {
        return dbContext.Groups.Where(o => o.Organization.Equals(organizationId)).ToList();
    }

    public List<Group> GetGroupsByPersonId(Guid personId)
    {
        return null;
        //TODO return dbContext.Groups.Where(o => o.Person.Equals(personId)).ToList();
    }

}
