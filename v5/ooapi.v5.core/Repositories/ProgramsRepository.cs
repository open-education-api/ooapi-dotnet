using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class ProgramsRepository : BaseRepository<Program>, IProgramsRepository
{
    public ProgramsRepository(ICoreDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Pagination<Program>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        IQueryable<Program> set = dbContext.ProgramsNoTracking;
        if (!string.IsNullOrEmpty(dataRequestParameters.Consumer))
        {
            set = set.Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));
        }

        set = set.Include(x => x.OtherCodes).Include(x => x.Address);

        return await GetAllOrderedByAsync(dataRequestParameters, set, cancellationToken);
    }

    public async Task<Program?> GetProgramAsync(Guid programId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        // expands: parent, organization, educationSpecification, children
        // nog te doen: , coordinators

        IQueryable<Program> set = dbContext.ProgramsNoTracking.Include(x => x.OtherCodes).Include(x => x.Address);


        var result = await set.FirstOrDefaultAsync(x => x.ProgramId.Equals(programId), cancellationToken);

        if (result is null)
        {
            return null;
        }

        result.ChildrenIds = await dbContext.ProgramsNoTracking.Where(x => x.ParentId.Equals(result.ProgramId)).Select(x => x.ProgramId).ToListAsync(cancellationToken);

        if (dataRequestParameters.Expand.Contains("parent", StringComparer.InvariantCultureIgnoreCase) && result.ParentId != null)
        {
            result.Parent = await dbContext.ProgramsNoTracking.FirstAsync(x => x.ProgramId.Equals(result.ParentId), cancellationToken);
            result.Parent.ChildrenIds = await set.Where(x => x.ParentId.Equals(result.Parent.ProgramId)).Select(x => x.ProgramId).ToListAsync(cancellationToken);
        }

        if (dataRequestParameters.Expand.Contains("children", StringComparer.InvariantCultureIgnoreCase))
        {
            result.Children = await dbContext.ProgramsNoTracking.Where(x => x.ParentId.Equals(result.ProgramId)).Include(x => x.Attributes).Include(x => x.OtherCodes).ToListAsync(cancellationToken);
            foreach (var item in result.Children)
            {
                item.ChildrenIds = await set.Where(x => x.ParentId.Equals(item.ProgramId)).Select(x => x.ProgramId).ToListAsync(cancellationToken);
            }
        }

        if (dataRequestParameters.Expand.Contains("organization", StringComparer.InvariantCultureIgnoreCase))
        {
            result.Organization = await dbContext.OrganizationsNoTracking.Include(x => x.Attributes).Include(x => x.OtherCodes).FirstAsync(x => x.OrganizationId.Equals(result.OrganizationId), cancellationToken);
            result.Organization.Parent = await dbContext.OrganizationsNoTracking.FirstOrDefaultAsync(x => x.OrganizationId.Equals(result.Organization.ParentId), cancellationToken);
            result.Organization.ChildrenIds = await dbContext.OrganizationsNoTracking.Where(x => x.ParentId.Equals(result.Organization.OrganizationId)).Select(x => x.OrganizationId).ToListAsync(cancellationToken);
        }

        if (dataRequestParameters.Expand.Contains("educationspecification", StringComparer.InvariantCultureIgnoreCase))
        {
            result.EducationSpecification = await dbContext.EducationSpecificationsNoTracking.Include(x => x.OtherCodes).FirstAsync(x => x.EducationSpecificationId.Equals(result.EducationSpecificationId), cancellationToken);
            Guid? educationSpecificationParentId = await dbContext.EducationSpecificationsNoTracking
                .Where(x => x.EducationSpecificationId.Equals(result.EducationSpecification.ParentId))
                .Select(x => x.EducationSpecificationId)
                .FirstOrDefaultAsync(cancellationToken);
            if (educationSpecificationParentId != Guid.Empty)
                result.EducationSpecification.ParentId = await dbContext.EducationSpecificationsNoTracking
                    .Where(x => x.EducationSpecificationId.Equals(result.EducationSpecification.ParentId))
                    .Select(x => x.EducationSpecificationId)
                    .FirstOrDefaultAsync(cancellationToken);

            result.EducationSpecification.ChildrenIds = await dbContext.EducationSpecificationsNoTracking.Where(x => x.ParentId.Equals(result.EducationSpecification.EducationSpecificationId)).Select(x => x.EducationSpecificationId).ToListAsync(cancellationToken);
        }

        return result;
    }

    public async Task<Pagination<Program>> GetProgramsByEducationSpecificationIdAsync(Guid educationSpecificationId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        IQueryable<Program> set = dbContext.ProgramsNoTracking.Include(x => x.OtherCodes).Include(x => x.Address).Where(o => o.EducationSpecificationId.Equals(educationSpecificationId));


        return await GetAllOrderedByAsync(dataRequestParameters, set, cancellationToken);
    }

    public async Task<Pagination<Program>> GetProgramsByOrganizationIdAsync(Guid organizationId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        IQueryable<Program> set = dbContext.Programs.Where(o => o.OrganizationId.Equals(organizationId)).Include(x => x.Attributes).Include(x => x.OtherCodes).Include(x => x.Address).Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));

        return await GetAllOrderedByAsync(dataRequestParameters, set, cancellationToken);
    }

    public async Task<Pagination<Program>> GetProgramsByProgramIdAsync(Guid programId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        IQueryable<Program> set = dbContext.Programs.Where(o => o.ParentId.Equals(programId)).Include(x => x.Attributes).Include(x => x.OtherCodes).Include(x => x.Address).Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));

        return await GetAllOrderedByAsync(dataRequestParameters, set, cancellationToken);
    }
}