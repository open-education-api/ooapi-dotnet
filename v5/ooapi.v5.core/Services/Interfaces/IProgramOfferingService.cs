using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

public interface IProgramOfferingService
{
    Task<ProgramOffering?> GetAsync(Guid programOfferingId, CancellationToken cancellationToken = default);
    Task<Pagination<ProgramOffering>> GetAllAsync(DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
}