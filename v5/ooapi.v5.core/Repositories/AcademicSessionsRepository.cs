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

}
