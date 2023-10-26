using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

public interface IOrganizationsService
{
    Task<Organization?> GetAsync(Guid organizationId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
    Task<Pagination<Organization>> GetAllAsync(DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
}