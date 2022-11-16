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

}
