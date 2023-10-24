using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

public interface IAcademicSessionsService
{
    AcademicSession? Get(Guid academicSessionId, DataRequestParameters dataRequestParameters);
    Pagination<AcademicSession> GetAll(DataRequestParameters dataRequestParameters, string? academicSessionType);
}