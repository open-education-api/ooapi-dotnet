using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class OrganizationsRepository : BaseRepository<Organization>
{
    public OrganizationsRepository(CoreDBContext dbContext) : base(dbContext)
    {
        //
    }

    public Organization GetOrganization(Guid organizationId, DataRequestParameters dataRequestParameters)
    {
        IQueryable<Organization> set = dbContext.Set<Organization>().AsNoTracking().Include(x => x.Attributes);

        Organization result = set.FirstOrDefault(x => x.OrganizationId.Equals(organizationId));
        result.ChildrenIds = set.Where(x => x.ParentId.Equals(result.OrganizationId)).Select(x => x.OrganizationId).ToList();

        bool getParent = dataRequestParameters.Expand.Contains("parent", StringComparer.InvariantCultureIgnoreCase);
        if (getParent && result.ParentId != null)
        {
            result.Parent = set.FirstOrDefault(x => x.OrganizationId.Equals(result.ParentId));
            result.Parent.ChildrenIds = set.Where(x => x.ParentId.Equals(result.Parent.OrganizationId)).Select(x => x.OrganizationId).ToList();
        }

        bool getChildren = dataRequestParameters.Expand.Contains("children", StringComparer.InvariantCultureIgnoreCase);
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
        IQueryable<Organization> set = dbContext.Set<Organization>().AsNoTracking().Include(x => x.Attributes);
        bool includeConsumer = dataRequestParameters != null && !String.IsNullOrEmpty(dataRequestParameters.Consumer);
        if (includeConsumer)
        {
            set = set.Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));
        }
        return GetAllOrderedBy(dataRequestParameters, set);
        //var set = dbContext.Set<Organization>();
        //IQueryable<Organization> coll = null;
        //if (organizationType != null)
        //    coll = (set.Where(x => x.OrganizationType.Equals(organizationType)));
        //else
        //    coll = set;
        //coll = coll.Include(x => x.Address).AsQueryable();
        //return GetAllOrderedBy(dataRequestParameters, set);
    }

}
