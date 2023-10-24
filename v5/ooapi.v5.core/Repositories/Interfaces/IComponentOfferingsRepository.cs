using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IComponentOfferingsRepository
{
    ComponentOffering? GetComponentOffering(Guid courseOfferingId);
    Task<Pagination<ComponentOffering>> GetComponentOfferingByComponentIdAsync(Guid componentId, DataRequestParameters dataRequestParameters);
    Task<Pagination<ComponentOffering>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, IQueryable<ComponentOffering>? set = null, CancellationToken cancellationToken = default);
}