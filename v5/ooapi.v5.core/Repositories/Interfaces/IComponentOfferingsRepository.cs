using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IComponentOfferingsRepository
{
    Task<ComponentOffering?> GetComponentOfferingAsync(Guid courseOfferingId, CancellationToken cancellationToken = default);
    Task<Pagination<ComponentOffering>> GetComponentOfferingByComponentIdAsync(Guid componentId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
    Task<Pagination<ComponentOffering>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, IQueryable<ComponentOffering>? set = null, CancellationToken cancellationToken = default);
}