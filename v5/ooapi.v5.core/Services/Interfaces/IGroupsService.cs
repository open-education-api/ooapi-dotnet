using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;


public interface IGroupsService
{

    /// <param name="groupId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Group? Get(Guid groupId, out ErrorResponse? errorResponse);


    /// <param name="dataRequestParameters"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Pagination<Group>? GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse? errorResponse);


    /// <param name="dataRequestParameters"></param>
    /// <param name="organizationId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Pagination<Group>? GetGroupsByOrganizationId(DataRequestParameters dataRequestParameters, Guid organizationId, out ErrorResponse? errorResponse);


    /// <param name="dataRequestParameters"></param>
    /// <param name="personId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Pagination<Group>? GetGroupsByPersonId(DataRequestParameters dataRequestParameters, Guid personId, out ErrorResponse? errorResponse);
}