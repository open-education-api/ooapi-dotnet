using Microsoft.EntityFrameworkCore;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class EducationSpecificationsRepository : BaseRepository<EducationSpecification>
{
    public EducationSpecificationsRepository(CoreDBContext dbContext) : base(dbContext)
    {
        //
    }

    public EducationSpecification GetEducationSpecification(Guid educationSpecificationId, List<string> expand)
    {
        var set = dbContext.EducationSpecifications;

        if (expand != null && expand.Any())
        {

            bool getParent = expand.Contains("parent", StringComparer.InvariantCultureIgnoreCase);
            bool getOrganization = expand.Contains("organization", StringComparer.InvariantCultureIgnoreCase);

            if (getParent && getOrganization)
            {
                return set.Include(x => x.Parent)
                          .Include(x => x.Organization)
                          .FirstOrDefault(x => x.EducationSpecificationId.Equals(educationSpecificationId));
            }
            else if (getParent && !getOrganization)
            {
                return set.Include(x => x.Parent)
                          .FirstOrDefault(x => x.EducationSpecificationId.Equals(educationSpecificationId));
            }
            else if (!getParent && getOrganization)
            {
                return set.Include(x => x.Organization)
                          .FirstOrDefault(x => x.EducationSpecificationId.Equals(educationSpecificationId));
            }

        }

        return set.FirstOrDefault(x => x.EducationSpecificationId.Equals(educationSpecificationId));
    }

    public List<EducationSpecification> GetEducationSpecificationsByEducationSpecificationId(Guid educationSpecificationId)
    {
        return dbContext.EducationSpecifications.Where(o => o.ParentId.Equals(educationSpecificationId)).ToList();
    }

    public List<EducationSpecification> GetEducationSpecificationsByOrganizationId(Guid organizationId)
    {
        return dbContext.EducationSpecifications.Where(o => o.OrganizationId.Equals(organizationId)).ToList();
    }
}
