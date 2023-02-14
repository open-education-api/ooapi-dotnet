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
        // expands: parent, organization, educationSpecification
        // nog te doen: children, coordinators
        var set = dbContext.Programs;

        if (expand != null && expand.Any())
        {

            bool getParent = expand.Contains("parent", StringComparer.InvariantCultureIgnoreCase);
            bool getOrganization = expand.Contains("organization", StringComparer.InvariantCultureIgnoreCase);
            bool getEducationspecification = expand.Contains("educationspecification", StringComparer.InvariantCultureIgnoreCase);

            if (getParent && getOrganization && getEducationspecification)
            {
                return set.Include(x => x.Parent)
                          .Include(x => x.Organization)
                          .Include(x => x.EducationSpecification)
                          .FirstOrDefault(x => x.ProgramId.Equals(programId));
            }
            else if (getParent && getOrganization && !getEducationspecification) 
            {
                return set.Include(x => x.Parent)
                          .Include(x => x.Organization)
                          .FirstOrDefault(x => x.ProgramId.Equals(programId));
            }
            else if (getParent && !getOrganization && getEducationspecification) 
            {
                return set.Include(x => x.Parent)
                        .Include(x => x.EducationSpecification)
                        .FirstOrDefault(x => x.ProgramId.Equals(programId));
            }
            else if (!getParent && getOrganization && getEducationspecification)
            {
                return set.Include(x => x.Organization)
                    .Include(x => x.EducationSpecification)
                    .FirstOrDefault(x => x.ProgramId.Equals(programId));
            }

            else if (getParent && !getOrganization && !getEducationspecification)
            {
                return set.Include(x => x.Parent)
                    .FirstOrDefault(x => x.ProgramId.Equals(programId));
            }
            else if (!getParent && getOrganization && !getEducationspecification)
            {
                return set.Include(x => x.Organization)
                    .FirstOrDefault(x => x.ProgramId.Equals(programId));
            }
            else if (!getParent && !getOrganization && getEducationspecification)
            {
                return set.Include(x => x.EducationSpecification)
                    .FirstOrDefault(x => x.ProgramId.Equals(programId));
            }
        }

        return set.FirstOrDefault(x => x.ProgramId.Equals(programId));
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
