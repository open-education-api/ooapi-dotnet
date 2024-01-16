using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class EducationSpecificationsRepository : BaseRepository<EducationSpecification>, IEducationSpecificationsRepository
{
    public EducationSpecificationsRepository(ICoreDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Pagination<EducationSpecification>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        IQueryable<EducationSpecification> set = dbContext.EducationSpecificationsNoTracking.Include(x => x.Attributes).Include(x => x.OtherCodes);
        if (!string.IsNullOrEmpty(dataRequestParameters.Consumer))
        {
            set = set.Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));
        }

        return await GetAllOrderedByAsync(dataRequestParameters, set, cancellationToken);
    }

    public async Task<EducationSpecification> GetEducationSpecificationAsync(Guid educationSpecificationId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        IQueryable<EducationSpecification> set = dbContext.EducationSpecificationsNoTracking.Include(x => x.Attributes).Include(x => x.OtherCodes);
        var setIncluded = set;

        var result = await setIncluded.FirstAsync(x => x.EducationSpecificationId.Equals(educationSpecificationId), cancellationToken);
        result.ChildrenIds = await set.Where(x => x.ParentId.Equals(result.EducationSpecificationId)).Select(x => x.EducationSpecificationId).ToListAsync(cancellationToken);

        if (dataRequestParameters.Expand.Contains("parent", StringComparer.InvariantCultureIgnoreCase) && result.ParentId != null)
        {
            result.Parent = await set.FirstAsync(x => x.EducationSpecificationId.Equals(result.ParentId), cancellationToken);
            result.Parent.ChildrenIds = await set.Where(x => x.ParentId.Equals(result.Parent.EducationSpecificationId)).Select(x => x.EducationSpecificationId).ToListAsync(cancellationToken);
        }

        if (dataRequestParameters.Expand.Contains("children", StringComparer.InvariantCultureIgnoreCase) && result.ChildrenIds != null)
        {
            result.Children = await set.Where(x => x.ParentId.Equals(result.EducationSpecificationId)).ToListAsync(cancellationToken);
            foreach (var item in result.Children)
            {
                item.ChildrenIds = await set.Where(x => x.ParentId.Equals(item.EducationSpecificationId)).Select(x => x.EducationSpecificationId).ToListAsync(cancellationToken);
            }
        }

        if (dataRequestParameters.Expand.Contains("organization", StringComparer.InvariantCultureIgnoreCase))
        {
            result.Organization =await  dbContext.OrganizationsNoTracking.Include(x => x.Attributes).FirstAsync(x => x.OrganizationId.Equals(result.OrganizationId), cancellationToken);
            result.Organization.Parent = await dbContext.OrganizationsNoTracking.FirstOrDefaultAsync(x => x.OrganizationId.Equals(result.Organization.ParentId), cancellationToken);
            result.Organization.ChildrenIds = await dbContext.OrganizationsNoTracking.Where(x => x.ParentId.Equals(result.Organization.OrganizationId)).Select(x => x.OrganizationId).ToListAsync(cancellationToken);
        }
        return result;
    }

    public async Task<Pagination<EducationSpecification>> GetEducationSpecificationsByEducationSpecificationIdAsync(Guid educationSpecificationId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        IQueryable<EducationSpecification> set = dbContext.EducationSpecifications.Where(o => o.ParentId.Equals(educationSpecificationId)).Include(x => x.Attributes).Include(x => x.OtherCodes);
        if (!string.IsNullOrEmpty(dataRequestParameters.Consumer))
        {
            set = set.Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));
        }

        return await GetAllOrderedByAsync(dataRequestParameters, set, cancellationToken);
    }

    public async Task<Pagination<EducationSpecification>> GetEducationSpecificationsByOrganizationIdAsync(Guid organizationId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        IQueryable<EducationSpecification> set = dbContext.EducationSpecifications.Where(o => o.OrganizationId.Equals(organizationId)).Include(x => x.Attributes).Include(x => x.OtherCodes);
        if (!string.IsNullOrEmpty(dataRequestParameters.Consumer))
        {
            set = set.Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));
        }

        return await GetAllOrderedByAsync(dataRequestParameters, set, cancellationToken);
    }
}