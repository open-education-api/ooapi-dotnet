using ooapi.v5.core.Models;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

public class OrganizationsService : ServiceBase, IOrganizationsService
{
    private readonly OrganizationsRepository _repository;

    public OrganizationsService(CoreDBContext dbContext, UserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = new OrganizationsRepository(dbContext);
    }

    public Pagination<Organization> GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse errorResponse)
    {
        try
        {
            Pagination<Organization> result = _repository.GetAllOrderedBy(dataRequestParameters);
            foreach (var item in result.Items)
            {
                SetAddresses(item);
            }
            errorResponse = null;
            return result;
        }
        catch (Exception ex)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }

    private void SetAddresses(Organization item)
    {
        if (item.Address != null)
        {
            item.Addresses = new List<Address>();
            item.Addresses.AddRange(item.Address);
        }
    }

    public Organization Get(Guid organizationId, DataRequestParameters dataRequestParameters, out ErrorResponse errorResponse)
    {
        try
        {
            var item = _repository.GetOrganization(organizationId, dataRequestParameters);

            errorResponse = null;
            return item;
        }
        catch (Exception ex)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }

}
