using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IAcademicSessionsRepository
{
    Task<AcademicSession?> GetAcademicSessionAsync(Guid academicSessionId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken);
    Task<AcademicSession?> GetAcademicSessionAsync(string primaryCode, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken);
    Task<Pagination<AcademicSession>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, string? academicSessionType = null, CancellationToken cancellationToken = default);
    Task<Pagination<AcademicSession>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, IQueryable<AcademicSession>? set = null, CancellationToken cancellationToken = default);
}