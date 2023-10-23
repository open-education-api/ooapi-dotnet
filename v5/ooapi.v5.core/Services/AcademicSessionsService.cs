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

    public Pagination<AcademicSession> GetAll(DataRequestParameters dataRequestParameters, string? academicSessionType)
    {
        return _repository.GetAllOrderedBy(dataRequestParameters, academicSessionType);
    }

    public AcademicSession? Get(Guid academicSessionId, DataRequestParameters dataRequestParameters)
    {
        return _repository.GetAcademicSession(academicSessionId, dataRequestParameters);
    }
}