using ooapi.v5.core.Repositories;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

internal class AcademicSessionsService : ServiceBase, IAcademicSessionsService
{
    private readonly AcademicSessionsRepository _repository;

    public AcademicSessionsService(CoreDBContext dbContext, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = new AcademicSessionsRepository(dbContext);
    }

    public Pagination<AcademicSession>? GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse? errorResponse, string? academicSessionType)
    {
        try
        {
            var result = _repository.GetAllOrderedBy(dataRequestParameters, academicSessionType);
            errorResponse = null;
            return result;
        }
        catch (Exception)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }

    public AcademicSession? Get(Guid academicSessionId, DataRequestParameters dataRequestParameters, out ErrorResponse? errorResponse)
    {
        try
        {
            var item = _repository.GetAcademicSession(academicSessionId, dataRequestParameters);

            errorResponse = null;
            return item;
        }
        catch (Exception)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }
}
