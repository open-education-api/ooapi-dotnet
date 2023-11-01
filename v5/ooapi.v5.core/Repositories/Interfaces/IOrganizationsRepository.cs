using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IOrganizationsRepository
{
    Task<Organization> GetOrganizationAsync(Guid organizationId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
    Task<Pagination<Organization>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
}