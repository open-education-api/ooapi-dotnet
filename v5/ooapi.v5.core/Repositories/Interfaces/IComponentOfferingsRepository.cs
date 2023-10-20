using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IComponentOfferingsRepository
{
    ComponentOffering? GetComponentOffering(Guid courseOfferingId);
    Pagination<ComponentOffering> GetComponentOfferingByComponentId(Guid componentId, DataRequestParameters dataRequestParameters);
    Pagination<ComponentOffering> GetAllOrderedBy(DataRequestParameters dataRequestParameters, IQueryable<ComponentOffering>? set = null);
}