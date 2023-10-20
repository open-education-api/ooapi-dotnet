using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IServiceMetadataRepository
{
    Service GetServiceMetadata();
    Pagination<Service> GetAllOrderedBy(DataRequestParameters dataRequestParameters, IQueryable<Service>? set = null);
}