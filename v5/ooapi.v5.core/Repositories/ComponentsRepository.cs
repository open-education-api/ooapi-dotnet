using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class ComponentsRepository : BaseRepository<Component>
{
    public ComponentsRepository(CoreDBContext dbContext) : base(dbContext)
    {
        //
    }

    public Component GetComponent(Guid componentId)
    {
        return dbContext.Components.FirstOrDefault(x => x.ComponentId.Equals(componentId));
    }

}
