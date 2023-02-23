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

    public AcademicSession GetAcademicSession(Guid academicSessionId, DataRequestParameters dataRequestParameters)
    {
        IQueryable<AcademicSession> set = dbContext.AcademicSessionsNoTracking.Include(x => x.Attributes);


        AcademicSession result = set.FirstOrDefault(x => x.AcademicSessionId.Equals(academicSessionId));
        result.ChildrenIds = dbContext.AcademicSessionsNoTracking.Where(x => x.ParentId.Equals(result.AcademicSessionId)).Select(x => x.AcademicSessionId).ToList();

        bool getParent = dataRequestParameters.Expand.Contains("parent", StringComparer.InvariantCultureIgnoreCase);
        if (getParent && result.ParentId != null)
        {
            result.Parent = set.FirstOrDefault(x => x.AcademicSessionId.Equals(result.ParentId));
        }

        bool getChildren = dataRequestParameters.Expand.Contains("children", StringComparer.InvariantCultureIgnoreCase);
        if (getChildren)
        {
            result.Children = set.Where(x => x.ParentId.Equals(result.AcademicSessionId)).ToList();
            foreach (var item in result.Children)
            {
                item.ChildrenIds = dbContext.AcademicSessionsNoTracking.Where(x => x.ParentId.Equals(item.AcademicSessionId)).Select(x => x.AcademicSessionId).ToList();
            }
        }

        bool getYear = dataRequestParameters.Expand.Contains("year", StringComparer.InvariantCultureIgnoreCase);
        if (getYear && result.YearId != null)
        {
            result.Year = set.FirstOrDefault(x => x.AcademicSessionId.Equals(result.YearId));
        }

        return result;
    }

    internal Pagination<AcademicSession> GetAllOrderedBy(DataRequestParameters dataRequestParameters, string? academicSessionType = null)
    {
        IQueryable<AcademicSession> set = dbContext.AcademicSessionsNoTracking.Include(x => x.Attributes);
        bool includeConsumer = dataRequestParameters != null && !String.IsNullOrEmpty(dataRequestParameters.Consumer);
        if (includeConsumer)
        {
            set = set.Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));
        }

        set = set.AsNoTracking().AsQueryable();

        if (academicSessionType != null)
            set = set.Where(x => x.AcademicSessionType.Equals(academicSessionType));
        return GetAllOrderedBy(dataRequestParameters, set);
    }

}
