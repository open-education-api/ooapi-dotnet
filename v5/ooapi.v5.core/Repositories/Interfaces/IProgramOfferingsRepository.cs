using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IProgramOfferingsRepository
{
    ProgramOffering? GetProgramOffering(Guid programOfferingId);
    Task<Pagination<ProgramOffering>> GetProgramOfferingByProgramIdAsync(Guid programId, DataRequestParameters dataRequestParameters);
    Task<Pagination<ProgramOffering>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, IQueryable<ProgramOffering>? set = null, CancellationToken cancellationToken = default);
}