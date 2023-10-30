using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IBuildingsRepository
{
    Task<Building?> GetBuildingAsync(Guid buildingId, CancellationToken cancellationToken = default);
    Task<Pagination<Building>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
}