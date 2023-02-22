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
        IQueryable<Organization> set = dbContext.Set<Organization>().AsNoTracking();

        Organization result = set.FirstOrDefault(x => x.OrganizationId.Equals(organizationId));
        result.ChildrenIds = dbContext.Set<Organization>().AsNoTracking().Where(x => x.ParentId.Equals(result.OrganizationId)).Select(x => x.OrganizationId).ToList();

        bool getParent = dataRequestParameters.Expand.Contains("parent", StringComparer.InvariantCultureIgnoreCase);
        if (getParent && result.ParentId != null)
        {
            result.Parent = dbContext.Set<Organization>().AsNoTracking().FirstOrDefault(x => x.OrganizationId.Equals(result.ParentId));
        }

        bool getChildren = dataRequestParameters.Expand.Contains("children", StringComparer.InvariantCultureIgnoreCase);
        if (getChildren)
        {
            result.Children = dbContext.Set<Organization>().AsNoTracking().Where(x => x.ParentId.Equals(result.OrganizationId)).ToList();
        }

        return result;
    }

    internal Pagination<Organization> GetAllOrderedBy(DataRequestParameters dataRequestParameters, Enums.OrganizationTypeEnum? organizationType = null)
    {
        var set = dbContext.Set<Organization>();
        IQueryable<Organization> coll = null;
        if (organizationType != null)
            coll = (set.Where(x => x.OrganizationType.Equals(organizationType)));
        else
            coll = set;
        coll = coll.Include(x => x.Address).AsQueryable();
        return GetAllOrderedBy(dataRequestParameters, set);
    }

}
