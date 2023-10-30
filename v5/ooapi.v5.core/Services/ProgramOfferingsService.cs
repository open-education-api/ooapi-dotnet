using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

internal class ProgramOfferingsService : ServiceBase, IProgramOfferingService
{
    private readonly IProgramOfferingsRepository _repository;

    public ProgramOfferingsService(ICoreDbContext dbContext, IProgramOfferingsRepository repository, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = repository;
    }

    public async Task<Pagination<ProgramOffering>> GetAllAsync(DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        return await _repository.GetAllOrderedByAsync(dataRequestParameters, null, cancellationToken);
    }

    public async Task<ProgramOffering?> GetAsync(Guid programOfferingId, CancellationToken cancellationToken = default)
    {
        return await _repository.GetProgramOfferingAsync(programOfferingId, cancellationToken);
    }
}