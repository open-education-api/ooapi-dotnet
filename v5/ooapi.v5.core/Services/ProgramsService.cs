using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

internal class ProgramsService : ServiceBase, IProgramsService
{
    private readonly IProgramsRepository _repository;

    public ProgramsService(ICoreDbContext dbContext, IProgramsRepository repository, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = repository;
    }

    public async Task<Pagination<Program>> GetAllAsync(DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        return await _repository.GetAllOrderedByAsync(dataRequestParameters, cancellationToken);
    }

    public async Task<Program?> GetAsync(Guid programId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        return await _repository.GetProgramAsync(programId, dataRequestParameters, cancellationToken);
    }

    public async Task<Pagination<Program>> GetProgramsByEducationSpecificationIdAsync(DataRequestParameters dataRequestParameters, Guid educationSpecificationId, CancellationToken cancellationToken = default)
    {
        return await _repository.GetProgramsByEducationSpecificationIdAsync(educationSpecificationId, dataRequestParameters, cancellationToken);
    }

    public async Task<Pagination<Program>> GetProgramsByOrganizationIdAsync(DataRequestParameters dataRequestParameters, Guid organizationId, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetProgramsByOrganizationIdAsync(organizationId, cancellationToken);
        return new Pagination<Program>(result.AsQueryable(), dataRequestParameters);
    }

    public async Task<Pagination<Program>> GetProgramsByProgramIdAsync(DataRequestParameters dataRequestParameters, Guid programId, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetProgramsByProgramIdAsync(programId, cancellationToken);
        return new Pagination<Program>(result.AsQueryable(), dataRequestParameters);
    }
}