using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

internal class OrganizationsService : ServiceBase, IOrganizationsService
{
    private readonly IOrganizationsRepository _repository;

    public OrganizationsService(CoreDBContext dbContext, IOrganizationsRepository repository, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = repository;
    }

    public Pagination<Organization> GetAll(DataRequestParameters dataRequestParameters)
    {
        Pagination<Organization> result = _repository.GetAllOrderedBy(dataRequestParameters);
        foreach (var item in result.Items)
        {
            SetAddresses(item);
        }
        return result;
    }

    private void SetAddresses(Organization item)
    {
        if (item.Address != null)
        {
            item.Addresses = new List<Address>();
            item.Addresses.AddRange(item.Address);
        }
    }

    public Organization? Get(Guid organizationId, DataRequestParameters dataRequestParameters)
    {
        return _repository.GetOrganization(organizationId, dataRequestParameters);
    }

}