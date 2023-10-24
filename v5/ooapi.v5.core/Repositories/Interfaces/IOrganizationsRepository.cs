using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IOrganizationsRepository
{
    Organization GetOrganization(Guid organizationId, DataRequestParameters dataRequestParameters);
    Task<Pagination<Organization>> GetAllOrderedBy(DataRequestParameters dataRequestParameters);
}