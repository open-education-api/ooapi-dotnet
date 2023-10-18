using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

/// <summary>
/// 
/// </summary>
public class EducationSpecificationsRepository : BaseRepository<EducationSpecification>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbContext"></param>
    public EducationSpecificationsRepository(CoreDBContext dbContext) : base(dbContext)
    {
    }

    internal Pagination<EducationSpecification>? GetAllOrderedBy(DataRequestParameters dataRequestParameters)
    {
        IQueryable<EducationSpecification> set = dbContext.EducationSpecificationsNoTracking.Include(x => x.Attributes);
        if (dataRequestParameters != null && !string.IsNullOrEmpty(dataRequestParameters.Consumer))
        {
            set = set.Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));
            return GetAllOrderedBy(dataRequestParameters, set);
        }

        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="educationSpecificationId"></param>
    /// <param name="dataRequestParameters"></param>
    /// <returns></returns>
    public EducationSpecification GetEducationSpecification(Guid educationSpecificationId, DataRequestParameters dataRequestParameters)
    {
        IQueryable<EducationSpecification> set = dbContext.EducationSpecificationsNoTracking.Include(x => x.Attributes);
        var setIncluded = set;

        var result = setIncluded.First(x => x.EducationSpecificationId.Equals(educationSpecificationId));
        result.ChildrenIds = set.Where(x => x.ParentId.Equals(result.EducationSpecificationId)).Select(x => x.EducationSpecificationId).ToList();

        if (dataRequestParameters.Expand.Contains("parent", StringComparer.InvariantCultureIgnoreCase) && result.ParentId != null)
        {
            result.Parent = set.First(x => x.EducationSpecificationId.Equals(result.ParentId));
            result.Parent.ChildrenIds = set.Where(x => x.ParentId.Equals(result.Parent.EducationSpecificationId)).Select(x => x.EducationSpecificationId).ToList();
        }

        if (dataRequestParameters.Expand.Contains("children", StringComparer.InvariantCultureIgnoreCase) && result.ChildrenIds != null)
        {
            result.Children = set.Where(x => x.ParentId.Equals(result.EducationSpecificationId)).ToList();
            foreach (var item in result.Children)
            {
                item.ChildrenIds = set.Where(x => x.ParentId.Equals(item.EducationSpecificationId)).Select(x => x.EducationSpecificationId).ToList();
            }
        }

        if (dataRequestParameters.Expand.Contains("organization", StringComparer.InvariantCultureIgnoreCase))
        {
            result.Organization = dbContext.OrganizationsNoTracking.Include(x => x.Attributes).First(x => x.OrganizationId.Equals(result.OrganizationId));
            result.Organization.Parent = dbContext.OrganizationsNoTracking.FirstOrDefault(x => x.OrganizationId.Equals(result.Organization.ParentId));
            result.Organization.ChildrenIds = dbContext.OrganizationsNoTracking.Where(x => x.ParentId.Equals(result.Organization.OrganizationId)).Select(x => x.OrganizationId).ToList();
        }
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="educationSpecificationId"></param>
    /// <returns></returns>
    public List<EducationSpecification> GetEducationSpecificationsByEducationSpecificationId(Guid educationSpecificationId)
    {
        return dbContext.EducationSpecifications.Where(o => o.ParentId.Equals(educationSpecificationId)).ToList();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="organizationId"></param>
    /// <returns></returns>
    public List<EducationSpecification> GetEducationSpecificationsByOrganizationId(Guid organizationId)
    {
        return dbContext.EducationSpecifications.Where(o => o.OrganizationId.Equals(organizationId)).ToList();
    }
}
