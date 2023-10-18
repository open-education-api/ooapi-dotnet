using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Utility;
using ooapi.v5.Enums;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;


public class AcademicSessionsRepository : BaseRepository<AcademicSession>
{

    /// <param name="dbContext"></param>
    public AcademicSessionsRepository(CoreDBContext dbContext) : base(dbContext)
    {
    }

    internal AcademicSession? GetAcademicSession(Guid academicSessionId, DataRequestParameters dataRequestParameters)
    {
        IQueryable<AcademicSession> set = dbContext.AcademicSessionsNoTracking.Include(x => x.Attributes);
        var result = set.FirstOrDefault(x => x.AcademicSessionId.Equals(academicSessionId));
        if (result == null)
        {
            return null;
        }

        return GetAcademicSession(result, set, dataRequestParameters);
    }

    internal AcademicSession? GetAcademicSession(string primaryCode, DataRequestParameters dataRequestParameters)
    {
        IQueryable<AcademicSession> set = dbContext.AcademicSessionsNoTracking.Include(x => x.Attributes);
        var result = set.FirstOrDefault(x => x.PrimaryCode.Equals(primaryCode));
        if (result == null)
        {
            return null;
        }

        return GetAcademicSession(result, set, dataRequestParameters);
    }

    private AcademicSession GetAcademicSession(AcademicSession result, IQueryable<AcademicSession> set, DataRequestParameters dataRequestParameters)
    {
        result.ChildrenIds = dbContext.AcademicSessionsNoTracking.Where(x => x.ParentId.Equals(result.AcademicSessionId)).Select(x => x.AcademicSessionId).ToList();

        if (dataRequestParameters.Expand.Contains("parent", StringComparer.InvariantCultureIgnoreCase) && result.ParentId != null)
        {
            result.Parent = set.FirstOrDefault(x => x.AcademicSessionId.Equals(result.ParentId));
            result.Parent.ChildrenIds = set.Where(x => x.ParentId.Equals(result.Parent.AcademicSessionId)).Select(x => x.AcademicSessionId).ToList();
        }

        if (dataRequestParameters.Expand.Contains("children", StringComparer.InvariantCultureIgnoreCase))
        {
            result.Children = set.Where(x => x.ParentId.Equals(result.AcademicSessionId)).ToList();
            foreach (var item in result.Children)
            {
                item.ChildrenIds = dbContext.AcademicSessionsNoTracking.Where(x => x.ParentId.Equals(item.AcademicSessionId)).Select(x => x.AcademicSessionId).ToList();
            }
        }

        if (dataRequestParameters.Expand.Contains("year", StringComparer.InvariantCultureIgnoreCase) && result.YearId != null)
        {
            result.Year = set.FirstOrDefault(x => x.AcademicSessionId.Equals(result.YearId));
        }

        return result;
    }

    internal Pagination<AcademicSession> GetAllOrderedBy(DataRequestParameters dataRequestParameters, string? academicSessionType = null)
    {
        IQueryable<AcademicSession> set = dbContext.AcademicSessionsNoTracking.Include(x => x.Attributes);
        if (dataRequestParameters != null && !string.IsNullOrEmpty(dataRequestParameters.Consumer))
        {
            set = set.Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));
            set = set.AsQueryable();

            if (academicSessionType != null)
            {
                academicSessionType = academicSessionType.Replace(" ", "_");
                if (Enum.TryParse(academicSessionType, true, out AcademicSessionType result))
                {
                    set = set.Where(x => x.AcademicSessionType.Equals(result));
                }
            }
            return GetAllOrderedBy(dataRequestParameters, set);
        }

        return new Pagination<AcademicSession>();
    }
}
