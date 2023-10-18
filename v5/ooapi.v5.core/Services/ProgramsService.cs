using ooapi.v5.core.Repositories;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

internal class ProgramsService : ServiceBase, IProgramsService
{
    private readonly ProgramsRepository _repository;

    public ProgramsService(CoreDBContext dbContext, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = new ProgramsRepository(dbContext);
    }

    public Pagination<Program> GetAll(DataRequestParameters dataRequestParameters)
    {
        return _repository.GetAllOrderedBy(dataRequestParameters);
    }

    public Program? Get(Guid programId, DataRequestParameters dataRequestParameters)
    {
        return _repository.GetProgram(programId, dataRequestParameters);
    }

    public Pagination<Program> GetProgramsByEducationSpecificationId(DataRequestParameters dataRequestParameters, Guid educationSpecificationId)
    {
        return _repository.GetProgramsByEducationSpecificationId(educationSpecificationId, dataRequestParameters);
    }

    public Pagination<Program> GetProgramsByOrganizationId(DataRequestParameters dataRequestParameters, Guid organizationId)
    {
        var result = _repository.GetProgramsByOrganizationId(organizationId);
        return new Pagination<Program>(result.AsQueryable(), dataRequestParameters);
    }

    public Pagination<Program> GetProgramsByProgramId(DataRequestParameters dataRequestParameters, Guid programId)
    {
        var result = _repository.GetProgramsByProgramId(programId);
        return new Pagination<Program>(result.AsQueryable(), dataRequestParameters);
    }
}
