using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace ooapi.v5.core.Repositories;


public class ProgramsRepository : BaseRepository<Program>
{
    public ProgramsRepository(CoreDBContext dbContext) : base(dbContext)
    {
        //
    }

    public Program GetProgram(Guid programId, List<string> expand)
    {

        // expands: parent, organization, educationSpecification, children
        // nog te doen: , coordinators

        IQueryable<Program> set = dbContext.Set<Program>().AsNoTracking();

        Program result = set.FirstOrDefault(x => x.ProgramId.Equals(programId));
        result.ChildrenIds = dbContext.Set<Program>().AsNoTracking().Where(x => x.ParentId.Equals(result.ProgramId)).Select(x => x.ProgramId).ToList();
        result.ChildrenIds = dbContext.Set<Program>().AsNoTracking().Where(x => x.ParentId.Equals(result.ProgramId)).Select(x => x.ProgramId).ToList();

        bool getParent = expand != null && expand.Contains("parent", StringComparer.InvariantCultureIgnoreCase);
        if (getParent && result.ParentId != null)
        {
            result.Parent = dbContext.Set<Program>().AsNoTracking().FirstOrDefault(x => x.OrganizationId.Equals(result.ParentId));
        }

        bool getChildren = expand != null && expand.Contains("children", StringComparer.InvariantCultureIgnoreCase);
        if (getChildren)
        {
            result.Children = dbContext.Set<Program>().AsNoTracking().Where(x => x.ParentId.Equals(result.ProgramId)).ToList();
        }


        bool getOrganization = expand.Contains("organization", StringComparer.InvariantCultureIgnoreCase);
        if (getOrganization)
        {
            set = set.Include(x => x.Organization);
        }


        bool getEducationspecification = expand.Contains("educationspecification", StringComparer.InvariantCultureIgnoreCase);
        if (getEducationspecification)
        {
            set = set.Include(x => x.EducationSpecification);
        }

        return result;
    }

 

    public Pagination<Program> GetAllOrderedBy_Expand(DataRequestParameters dataRequestParameters)
    {
        IQueryable<Program> set = dbContext.Set<Program>().AsQueryable();
        return GetAllOrderedBy(dataRequestParameters, set);
    }

    public Pagination<Program> GetAllOrderedBy(DataRequestParameters dataRequestParameters)
    {
        IQueryable<Program> set = dbContext.Set<Program>().AsQueryable();
        return GetAllOrderedBy(dataRequestParameters, set);
    }

    public List<Program> GetProgramsByEducationSpecificationId(Guid educationSpecificationId)
    {
        return dbContext.Programs.Where(o => o.EducationSpecificationId.Equals(educationSpecificationId)).ToList();
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
