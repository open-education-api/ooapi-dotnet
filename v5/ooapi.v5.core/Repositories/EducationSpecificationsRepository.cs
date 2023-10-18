using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class EducationSpecificationsRepository : BaseRepository<EducationSpecification>
{
    public EducationSpecificationsRepository(ICoreDbContext dbContext) : base(dbContext)
    {
        //
    }

    internal Pagination<EducationSpecification> GetAllOrderedBy(DataRequestParameters dataRequestParameters)
    {
        IQueryable<EducationSpecification> set = dbContext.EducationSpecificationsNoTracking.Include(x => x.Attributes);
        bool includeConsumer = dataRequestParameters != null && !String.IsNullOrEmpty(dataRequestParameters.Consumer);
        if (includeConsumer)
        {
            set = set.Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));
        }
        return GetAllOrderedBy(dataRequestParameters, set);
    }

    public EducationSpecification GetEducationSpecification(Guid educationSpecificationId, DataRequestParameters dataRequestParameters)
    {
        IQueryable<EducationSpecification> set = dbContext.EducationSpecificationsNoTracking.Include(x => x.Attributes);
        IQueryable<EducationSpecification> setIncluded = set;

        EducationSpecification result = setIncluded.FirstOrDefault(x => x.EducationSpecificationId.Equals(educationSpecificationId));
        result.ChildrenIds = set.Where(x => x.ParentId.Equals(result.EducationSpecificationId)).Select(x => x.EducationSpecificationId).ToList();

        bool getParent = dataRequestParameters.Expand.Contains("parent", StringComparer.InvariantCultureIgnoreCase);
        if (getParent && result.ParentId != null)
        {
            result.Parent = set.FirstOrDefault(x => x.EducationSpecificationId.Equals(result.ParentId));
            result.Parent.ChildrenIds = set.Where(x => x.ParentId.Equals(result.Parent.EducationSpecificationId)).Select(x => x.EducationSpecificationId).ToList();
        }

        bool getChildren = dataRequestParameters.Expand.Contains("children", StringComparer.InvariantCultureIgnoreCase);
        if (getChildren && result.ChildrenIds != null)
        {
            result.Children = set.Where(x => x.ParentId.Equals(result.EducationSpecificationId)).ToList();
            foreach (var item in result.Children)
            {
                item.ChildrenIds = set.Where(x => x.ParentId.Equals(item.EducationSpecificationId)).Select(x => x.EducationSpecificationId).ToList();
            }
        }

        bool getOrganization = dataRequestParameters.Expand.Contains("organization", StringComparer.InvariantCultureIgnoreCase);
        if (getOrganization)
        {
            result.Organization = dbContext.OrganizationsNoTracking.Include(x=>x.Attributes).FirstOrDefault(x => x.OrganizationId.Equals(result.OrganizationId));
            result.Organization.Parent = dbContext.OrganizationsNoTracking.FirstOrDefault(x => x.OrganizationId.Equals(result.Organization.ParentId));
            result.Organization.ChildrenIds = dbContext.OrganizationsNoTracking.Where(x => x.ParentId.Equals(result.Organization.OrganizationId)).Select(x => x.OrganizationId).ToList();
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