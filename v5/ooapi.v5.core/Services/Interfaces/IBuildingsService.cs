using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

public interface IBuildingsService
{
    Task<Building?> GetAsync(Guid buildingId, CancellationToken cancellationToken = default);
    Task<Pagination<Building>> GetAllAsync(DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
}