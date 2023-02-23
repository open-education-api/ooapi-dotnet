using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class EducationSpecificationsRepository : BaseRepository<EducationSpecification>
{
    public EducationSpecificationsRepository(CoreDBContext dbContext) : base(dbContext)
    {
        //
    }

    internal Pagination<EducationSpecification> GetAllOrderedBy(DataRequestParameters dataRequestParameters)
    {
        IQueryable<EducationSpecification> set = dbContext.Set<EducationSpecification>().AsNoTracking().Include(x => x.Attributes);
        bool includeConsumer = dataRequestParameters != null && !String.IsNullOrEmpty(dataRequestParameters.Consumer);
        if (includeConsumer)
        {
            set = set.Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));
        }
        return GetAllOrderedBy(dataRequestParameters, set);
    }

    public EducationSpecification GetEducationSpecification(Guid educationSpecificationId, DataRequestParameters dataRequestParameters)
    {
        IQueryable<EducationSpecification> set = dbContext.Set<EducationSpecification>().AsNoTracking().Include(x => x.Attributes);
        IQueryable<EducationSpecification> setIncluded = set;

        bool getOrganization = dataRequestParameters.Expand.Contains("organization", StringComparer.InvariantCultureIgnoreCase);
        if (getOrganization)
        {
            setIncluded = setIncluded.Include(x => x.Organization);
        }

        EducationSpecification result = setIncluded.FirstOrDefault(x => x.EducationSpecificationId.Equals(educationSpecificationId));
        result.ChildrenIds = dbContext.Set<EducationSpecification>().AsNoTracking().Where(x => x.ParentId.Equals(result.EducationSpecificationId)).Select(x => x.EducationSpecificationId).ToList();

        bool getParent = dataRequestParameters.Expand.Contains("parent", StringComparer.InvariantCultureIgnoreCase);
        if (getParent && result.ParentId != null)
        {
            result.Parent = set.FirstOrDefault(x => x.EducationSpecificationId.Equals(result.ParentId));
        }

        bool getChildren = dataRequestParameters.Expand.Contains("children", StringComparer.InvariantCultureIgnoreCase);
        if (getChildren)
        {
            result.Children = set.Where(x => x.ParentId.Equals(result.EducationSpecificationId)).ToList();
            foreach (var item in result.Children)
            {
                item.ChildrenIds = dbContext.Set<EducationSpecification>().AsNoTracking().Where(x => x.ParentId.Equals(item.EducationSpecificationId)).Select(x => x.EducationSpecificationId).ToList();
            }
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
