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

}
