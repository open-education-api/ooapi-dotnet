using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

/// <summary>
/// 
/// </summary>
public interface IOrganizationsService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="organizationId"></param>
    /// <param name="dataRequestParameters"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Organization? Get(Guid organizationId, DataRequestParameters dataRequestParameters, out ErrorResponse? errorResponse);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataRequestParameters"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Pagination<Organization>? GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse? errorResponse);
}