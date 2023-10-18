using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;


public class ComponentsRepository : BaseRepository<Component>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbContext"></param>
    public ComponentsRepository(CoreDBContext dbContext) : base(dbContext)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="componentId"></param>
    /// <returns></returns>
    public Component? GetComponent(Guid componentId)
    {
        return dbContext.Components.FirstOrDefault(x => x.ComponentId.Equals(componentId));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="courseId"></param>
    /// <returns></returns>
    public List<Component> GetComponentsByCourseId(Guid courseId)
    {
        return dbContext.Components.Where(o => o.CourseId.Equals(courseId)).ToList();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="organizationId"></param>
    /// <returns></returns>
    public List<Component> GetComponentsByOrganizationId(Guid organizationId)
    {
        return dbContext.Components.Where(o => o.OrganizationId.Equals(organizationId)).ToList();
    }
}
