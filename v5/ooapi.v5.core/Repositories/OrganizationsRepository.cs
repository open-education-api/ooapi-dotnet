using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;


public class OrganizationsRepository : BaseRepository<Organization>
{
    public OrganizationsRepository(ICoreDbContext dbContext) : base(dbContext)
    {
    }


    /// <param name="organizationId"></param>
    /// <param name="dataRequestParameters"></param>
    /// <returns></returns>
    public Organization GetOrganization(Guid organizationId, DataRequestParameters dataRequestParameters)
    {
        IQueryable<Organization> set = dbContext.OrganizationsNoTracking.Include(x => x.Attributes);

        var result = set.First(x => x.OrganizationId.Equals(organizationId));
        result.ChildrenIds = set.Where(x => x.ParentId.Equals(result.OrganizationId)).Select(x => x.OrganizationId).ToList();

        var getParent = dataRequestParameters.Expand.Contains("parent", StringComparer.InvariantCultureIgnoreCase);
        if (getParent && result.ParentId != null)
        {
            result.Parent = set.First(x => x.OrganizationId.Equals(result.ParentId));
            result.Parent.ChildrenIds = set.Where(x => x.ParentId.Equals(result.Parent.OrganizationId)).Select(x => x.OrganizationId).ToList();
        }

        var getChildren = dataRequestParameters.Expand.Contains("children", StringComparer.InvariantCultureIgnoreCase);
        if (getChildren && result.ChildrenIds != null)
        {
            result.Children = set.Where(x => x.ParentId.Equals(result.OrganizationId)).ToList();
            foreach (var item in result.Children)
            {
                item.ChildrenIds = set.Where(x => x.ParentId.Equals(item.OrganizationId)).Select(x => x.OrganizationId).ToList();
            }
        }

        return result;
    }

    internal Pagination<Organization> GetAllOrderedBy(DataRequestParameters dataRequestParameters)
    {
        IQueryable<Organization> set = dbContext.OrganizationsNoTracking.Include(x => x.Attributes);

        if (!string.IsNullOrEmpty(dataRequestParameters.Consumer))
        {
            set = set.Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));
        }

        return GetAllOrderedBy(dataRequestParameters, set);
    }
}
