using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

public interface IAcademicSessionsService
{
    Task<AcademicSession?> GetAsync(Guid academicSessionId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
    Task<Pagination<AcademicSession>> GetAllAsync(DataRequestParameters dataRequestParameters, string? academicSessionType, CancellationToken cancellationToken = default);
}