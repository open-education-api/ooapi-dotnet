using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class AcademicSessionsRepository : BaseRepository<AcademicSession>
{
    public AcademicSessionsRepository(CoreDBContext dbContext) : base(dbContext)
    {
        //
    }

    public AcademicSession GetAcademicSession(Guid academicSessionId, List<string>? expand)
    {
        IQueryable<AcademicSession> set = dbContext.Set<AcademicSession>().AsNoTracking();

        AcademicSession result = set.FirstOrDefault(x => x.AcademicSessionId.Equals(academicSessionId));
        result.ChildrenIds = dbContext.Set<AcademicSession>().AsNoTracking().Where(x => x.ParentId.Equals(result.AcademicSessionId)).Select(x => x.AcademicSessionId).ToList();

        bool getParent = expand != null && expand.Contains("parent", StringComparer.InvariantCultureIgnoreCase);
        if (getParent && result.ParentId != null)
        {
            result.Parent = dbContext.Set<AcademicSession>().AsNoTracking().FirstOrDefault(x => x.AcademicSessionId.Equals(result.ParentId));
        }

        bool getChildren = expand != null && expand.Contains("children", StringComparer.InvariantCultureIgnoreCase);
        if (getChildren)
        {
            result.Children = dbContext.Set<AcademicSession>().AsNoTracking().Where(x => x.ParentId.Equals(result.AcademicSessionId)).ToList();
        }

        bool getYear = expand != null && expand.Contains("year", StringComparer.InvariantCultureIgnoreCase);
        if (getYear && result.YearId != null)
        {
            result.Year = dbContext.Set<AcademicSession>().AsNoTracking().FirstOrDefault(x => x.AcademicSessionId.Equals(result.YearId));
        }

        return result;
    }

    internal Pagination<AcademicSession> GetAllOrderedBy(DataRequestParameters dataRequestParameters, string? academicSessionType = null)
    {
        IQueryable<AcademicSession> set = dbContext.Set<AcademicSession>().AsNoTracking().AsQueryable();

        if (academicSessionType != null)
            set = set.Where(x => x.AcademicSessionType.Equals(academicSessionType));
        return GetAllOrderedBy(dataRequestParameters, set);
    }

}
