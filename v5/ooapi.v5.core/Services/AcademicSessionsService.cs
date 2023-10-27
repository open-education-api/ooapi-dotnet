using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

internal class AcademicSessionsService : ServiceBase, IAcademicSessionsService
{
    private readonly IAcademicSessionsRepository _repository;

    public AcademicSessionsService(ICoreDbContext dbContext, IAcademicSessionsRepository repository, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = repository;
    }

    public async Task<Pagination<AcademicSession>> GetAllAsync(DataRequestParameters dataRequestParameters, string? academicSessionType, CancellationToken cancellationToken = default)
    {
        return await _repository.GetAllOrderedByAsync(dataRequestParameters, academicSessionType, cancellationToken);
    }

    public async Task<AcademicSession?> GetAsync(Guid academicSessionId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        return await _repository.GetAcademicSessionAsync(academicSessionId, dataRequestParameters, cancellationToken);
    }
}