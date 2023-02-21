using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;
using System.Runtime.CompilerServices;

namespace ooapi.v5.core.Repositories;

public class EducationSpecificationsRepository : BaseRepository<EducationSpecification>
{
    public EducationSpecificationsRepository(CoreDBContext dbContext) : base(dbContext)
    {
        //
    }

    internal Pagination<EducationSpecification> GetAllOrderedBy(DataRequestParameters dataRequestParameters)
    {
        IQueryable<EducationSpecification> set = dbContext.Set<EducationSpecification>().Include(x => x.Attributes);
        bool includeConsumer = dataRequestParameters != null && !String.IsNullOrEmpty(dataRequestParameters.Consumer);
        if (includeConsumer)
        {
            set = set.Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));
        }
        return GetAllOrderedBy(dataRequestParameters, set);
    }

    public EducationSpecification GetEducationSpecification(Guid educationSpecificationId, List<string>? expand)
    {
        IQueryable<EducationSpecification> set = dbContext.Set<EducationSpecification>().Include(x => x.Attributes);

        bool getOrganization = expand != null && expand.Contains("organization", StringComparer.InvariantCultureIgnoreCase);
        if (getOrganization)
        {
            set = set.Include(x => x.Organization);
        }

        EducationSpecification result = set.FirstOrDefault(x => x.EducationSpecificationId.Equals(educationSpecificationId));
        result.ChildrenIds = dbContext.Set<EducationSpecification>().AsNoTracking().Where(x => x.ParentId.Equals(result.EducationSpecificationId)).Select(x => x.EducationSpecificationId).ToList();

        bool getParent = expand != null && expand.Contains("parent", StringComparer.InvariantCultureIgnoreCase);
        if (getParent && result.ParentId != null)
        {
            result.Parent = dbContext.Set<EducationSpecification>().Include(x => x.Attributes).AsNoTracking().FirstOrDefault(x => x.EducationSpecificationId.Equals(result.ParentId));
        }

        bool getChildren = expand != null && expand.Contains("children", StringComparer.InvariantCultureIgnoreCase);
        if (getChildren)
        {
            result.Children = dbContext.Set<EducationSpecification>().Include(x => x.Attributes).AsNoTracking().Where(x => x.ParentId.Equals(result.EducationSpecificationId)).ToList();
        }

        return result;
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
