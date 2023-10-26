using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class ComponentsRepository : BaseRepository<Component>, IComponentsRepository
{
    public ComponentsRepository(ICoreDbContext dbContext) : base(dbContext)
    {
    }


    public async Task<Component?> GetComponentAsync(Guid componentId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Components.FirstOrDefaultAsync(x => x.ComponentId.Equals(componentId), cancellationToken);
    }

    public async Task<List<Component>> GetComponentsByCourseIdAsync(Guid courseId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Components.Where(o => o.CourseId.Equals(courseId)).ToListAsync(cancellationToken);
    }

    public async Task<List<Component>> GetComponentsByOrganizationIdAsync(Guid organizationId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Components.Where(o => o.OrganizationId.Equals(organizationId)).ToListAsync(cancellationToken);
    }
}