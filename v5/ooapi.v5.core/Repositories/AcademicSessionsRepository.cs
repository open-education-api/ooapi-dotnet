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

    public AcademicSession GetAcademicSession(Guid academicSessionId)
    {
        return dbContext.AcademicSessions.FirstOrDefault(x => x.AcademicSessionId.Equals(academicSessionId));
    }

    internal Pagination<AcademicSession> GetAllOrderedBy(DataRequestParameters dataRequestParameters, Enums.AcademicSessionTypeEnum? academicSessionType = null)
    {
        IQueryable<AcademicSession> set = dbContext.Set<AcademicSession>().AsQueryable();

        if (academicSessionType != null)
            set = set.Where(x => x.AcademicSessionType.Equals(academicSessionType));
        return GetAllOrderedBy(dataRequestParameters, set);
    }

}
