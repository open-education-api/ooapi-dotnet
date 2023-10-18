using ooapi.v5.core.Repositories;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;


public class OrganizationsService : ServiceBase, IOrganizationsService
{
    private readonly OrganizationsRepository _repository;

    public OrganizationsService(CoreDBContext dbContext, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = new OrganizationsRepository(dbContext);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataRequestParameters"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    public Pagination<Organization>? GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse? errorResponse)
    {
        try
        {
            var result = _repository.GetAllOrderedBy(dataRequestParameters);
            foreach (var item in result.Items)
            {
                SetAddresses(item);
            }
            errorResponse = null;
            return result;
        }
        catch (Exception)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }

    private static void SetAddresses(Organization item)
    {
        if (item.Address != null)
        {
            item.Addresses = new List<Address>();
            item.Addresses.AddRange(item.Address);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="organizationId"></param>
    /// <param name="dataRequestParameters"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    public Organization? Get(Guid organizationId, DataRequestParameters dataRequestParameters, out ErrorResponse? errorResponse)
    {
        try
        {
            var item = _repository.GetOrganization(organizationId, dataRequestParameters);

            errorResponse = null;
            return item;
        }
        catch (Exception)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }
}
