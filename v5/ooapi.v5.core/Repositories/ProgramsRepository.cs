using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;


public class ProgramsRepository : BaseRepository<Program>
{
    public ProgramsRepository(ICoreDbContext dbContext) : base(dbContext)
    {
    }

    public Pagination<Program> GetAllOrderedBy(DataRequestParameters dataRequestParameters)
    {
        IQueryable<Program> set = dbContext.ProgramsNoTracking.Include(x => x.Attributes);
        if (!string.IsNullOrEmpty(dataRequestParameters.Consumer))
        {
            set = set.Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));
        }

        return GetAllOrderedBy(dataRequestParameters, set);
    }

    public Program? GetProgram(Guid programId, DataRequestParameters dataRequestParameters)
    {
        // expands: parent, organization, educationSpecification, children
        // nog te doen: , coordinators

        IQueryable<Program> set = dbContext.ProgramsNoTracking.Include(x => x.Attributes);

        var result = set.FirstOrDefault(x => x.ProgramId.Equals(programId));

        if (result is null)
        {
            return null;
        }

        result.ChildrenIds = dbContext.ProgramsNoTracking.Where(x => x.ParentId.Equals(result.ProgramId)).Select(x => x.ProgramId).ToList();

        if (dataRequestParameters.Expand.Contains("parent", StringComparer.InvariantCultureIgnoreCase) && result.ParentId != null)
        {
            result.Parent = dbContext.ProgramsNoTracking.First(x => x.ProgramId.Equals(result.ParentId));
            result.Parent.ChildrenIds = set.Where(x => x.ParentId.Equals(result.Parent.ProgramId)).Select(x => x.ProgramId).ToList();
        }

        if (dataRequestParameters.Expand.Contains("children", StringComparer.InvariantCultureIgnoreCase))
        {
            result.Children = dbContext.ProgramsNoTracking.Where(x => x.ParentId.Equals(result.ProgramId)).ToList();
            foreach (var item in result.Children)
            {
                item.ChildrenIds = set.Where(x => x.ParentId.Equals(item.ProgramId)).Select(x => x.ProgramId).ToList();
            }
        }

        if (dataRequestParameters.Expand.Contains("organization", StringComparer.InvariantCultureIgnoreCase))
        {
            result.Organization = dbContext.OrganizationsNoTracking.Include(x => x.Attributes).First(x => x.OrganizationId.Equals(result.OrganizationId));
            result.Organization.Parent = dbContext.OrganizationsNoTracking.FirstOrDefault(x => x.OrganizationId.Equals(result.Organization.ParentId));
            result.Organization.ChildrenIds = dbContext.OrganizationsNoTracking.Where(x => x.ParentId.Equals(result.Organization.OrganizationId)).Select(x => x.OrganizationId).ToList();
        }

        if (dataRequestParameters.Expand.Contains("educationspecification", StringComparer.InvariantCultureIgnoreCase))
        {
            result.EducationSpecification = dbContext.EducationSpecificationsNoTracking.Include(x => x.Attributes).First(x => x.EducationSpecificationId.Equals(result.EducationSpecificationId));
            Guid? educationSpecificationParentId = dbContext.EducationSpecificationsNoTracking
                .Where(x => x.EducationSpecificationId.Equals(result.EducationSpecification.ParentId))
                .Select(x => x.EducationSpecificationId)
                .FirstOrDefault();
            if (educationSpecificationParentId != null && educationSpecificationParentId != Guid.Empty)
                result.EducationSpecification.ParentId = dbContext.EducationSpecificationsNoTracking
                    .Where(x => x.EducationSpecificationId.Equals(result.EducationSpecification.ParentId))
                    .Select(x => x.EducationSpecificationId)
                    .FirstOrDefault();

            result.EducationSpecification.ChildrenIds = dbContext.EducationSpecificationsNoTracking.Where(x => x.ParentId.Equals(result.EducationSpecification.EducationSpecificationId)).Select(x => x.EducationSpecificationId).ToList();
        }

        return result;
    }

    public Pagination<Program> GetProgramsByEducationSpecificationId(Guid educationSpecificationId, DataRequestParameters dataRequestParameters)
    {
        IQueryable<Program> set = dbContext.ProgramsNoTracking.Where(o => o.EducationSpecificationId.Equals(educationSpecificationId)).Include(x => x.Attributes);

        if (!string.IsNullOrEmpty(dataRequestParameters.Consumer))
        {
            set = set.Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));
        }

        return GetAllOrderedBy(dataRequestParameters, set);
    }

    public List<Program> GetProgramsByOrganizationId(Guid organizationId)
    {
        return dbContext.Programs.Where(o => o.OrganizationId.Equals(organizationId)).ToList();
    }

    public List<Program> GetProgramsByProgramId(Guid programId)
    {
        return dbContext.Programs.Where(o => o.ParentId.Equals(programId)).ToList();
    }
}