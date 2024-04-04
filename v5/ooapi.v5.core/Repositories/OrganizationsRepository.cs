using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;
public class OrganizationsRepository : BaseRepository<Organization>, IOrganizationsRepository
{
    public OrganizationsRepository(ICoreDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Organization> GetOrganizationAsync(Guid organizationId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        IQueryable<Organization> set = dbContext.OrganizationsNoTracking.Include(x => x.Attributes).Include(x => x.OtherCodes);

        var result = await set.FirstAsync(x => x.OrganizationId.Equals(organizationId), cancellationToken);
        result.ChildrenIds = await set.Where(x => x.ParentId.Equals(result.OrganizationId)).Select(x => x.OrganizationId).ToListAsync(cancellationToken);

        var getParent = dataRequestParameters.Expand.Contains("parent", StringComparer.InvariantCultureIgnoreCase);
        if (getParent && result.ParentId != null)
        {
            result.Parent = await set.FirstAsync(x => x.OrganizationId.Equals(result.ParentId), cancellationToken);
            result.Parent.ChildrenIds = await set.Where(x => x.ParentId.Equals(result.Parent.OrganizationId)).Select(x => x.OrganizationId).ToListAsync(cancellationToken);
        }

        var getChildren = dataRequestParameters.Expand.Contains("children", StringComparer.InvariantCultureIgnoreCase);
        if (getChildren && result.ChildrenIds != null)
        {
            result.Children = await set.Where(x => x.ParentId.Equals(result.OrganizationId)).Include(x => x.Attributes).Include(x => x.OtherCodes).ToListAsync(cancellationToken);
            foreach (var item in result.Children)
            {
                item.ChildrenIds = await set.Where(x => x.ParentId.Equals(item.OrganizationId)).Select(x => x.OrganizationId).ToListAsync(cancellationToken);
            }
        }

        return result;
    }

    public async Task<Pagination<Organization>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        IQueryable<Organization> set = dbContext.OrganizationsNoTracking.Include(x => x.Attributes).Include(x => x.OtherCodes).Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));


        return await GetAllOrderedByAsync(dataRequestParameters, set, cancellationToken);
    }
}