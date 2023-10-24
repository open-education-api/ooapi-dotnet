using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IAcademicSessionsRepository
{
    AcademicSession? GetAcademicSession(Guid academicSessionId, DataRequestParameters dataRequestParameters);
    AcademicSession? GetAcademicSession(string primaryCode, DataRequestParameters dataRequestParameters);
    Pagination<AcademicSession> GetAllOrderedBy(DataRequestParameters dataRequestParameters, string? academicSessionType = null);
    Pagination<AcademicSession> GetAllOrderedBy(DataRequestParameters dataRequestParameters, IQueryable<AcademicSession>? set = null);
}