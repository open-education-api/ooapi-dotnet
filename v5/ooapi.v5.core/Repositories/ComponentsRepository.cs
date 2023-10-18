using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class ComponentsRepository : BaseRepository<Component>
{
    public ComponentsRepository(ICoreDbContext dbContext) : base(dbContext)
    {
        //
    }

    public Component GetComponent(Guid componentId)
    {
        return dbContext.Components.FirstOrDefault(x => x.ComponentId.Equals(componentId));
    }

    public List<Component> GetComponentsByCourseId(Guid courseId)
    {
        return dbContext.Components.Where(o => o.CourseId.Equals(courseId)).ToList();
    }

    public List<Component> GetComponentsByOrganizationId(Guid organizationId)
    {
        return dbContext.Components.Where(o => o.OrganizationId.Equals(organizationId)).ToList();
    }
}