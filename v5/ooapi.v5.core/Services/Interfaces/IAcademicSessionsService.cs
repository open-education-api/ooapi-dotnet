using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces
{
    public interface IAcademicSessionsService
    {
        AcademicSession Get(Guid academicSessionId, DataRequestParameters dataRequestParameters, out ErrorResponse errorResponse);
        Pagination<AcademicSession> GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse errorResponse, string? academicSessionType);
    }
}