using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

public interface IOrganizationsService
{
    Organization? Get(Guid organizationId, DataRequestParameters dataRequestParameters);
    Task<Pagination<Organization>> GetAll(DataRequestParameters dataRequestParameters);
}