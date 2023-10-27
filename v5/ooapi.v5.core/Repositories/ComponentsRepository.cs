using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Utility;
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

    public async Task<Pagination<Component>> GetComponentsByCourseIdAsync(Guid courseId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        var set  = dbContext.Components.Where(o => o.CourseId.Equals(courseId));

        return await GetAllOrderedByAsync(dataRequestParameters, set, cancellationToken);
    }

    public async Task<Pagination<Component>> GetComponentsByOrganizationIdAsync(Guid organizationId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
       var set = dbContext.Components.Where(o => o.OrganizationId.Equals(organizationId));

       return await GetAllOrderedByAsync(dataRequestParameters, set, cancellationToken);
    }
}