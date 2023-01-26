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

    internal Pagination<Organization> GetAllOrderedBy(DataRequestParameters dataRequestParameters)
    {
        IQueryable<Organization> set = dbContext.Set<Organization>().Include(x => x.Address).AsQueryable();
        return GetAllOrderedBy(dataRequestParameters, set);
    }

}
