using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IServiceMetadataRepository
{
    Service GetServiceMetadata();
    Task<Pagination<Service>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, IQueryable<Service>? set = null, CancellationToken cancellationToken = default);
}