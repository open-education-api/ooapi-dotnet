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

    public Organization GetOrganization(Guid organizationId)
    {
        return dbContext.Organizations.FirstOrDefault(x => x.OrganizationId.Equals(organizationId));
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
