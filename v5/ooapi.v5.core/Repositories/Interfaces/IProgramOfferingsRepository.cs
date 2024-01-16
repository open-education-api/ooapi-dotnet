using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IProgramOfferingsRepository
{
    Task<Pagination<ProgramOffering>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
    Task<ProgramOffering?> GetProgramOfferingAsync(Guid programOfferingId,
        CancellationToken cancellationToken = default);

    Task<Pagination<ProgramOffering>> GetProgramOfferingsByProgramIdAsync(Guid programId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
}