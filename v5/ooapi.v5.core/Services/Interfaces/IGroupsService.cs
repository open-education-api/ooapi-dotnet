using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces
{
    public interface IGroupsService
    {
        Group Get(Guid groupId, out ErrorResponse errorResponse);
        Pagination<Group> GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse errorResponse);
        Pagination<Group> GetGroupsByOrganizationId(DataRequestParameters dataRequestParameters, Guid organizationId, out ErrorResponse errorResponse);
        Pagination<Group> GetGroupsByPersonId(DataRequestParameters dataRequestParameters, Guid personId, out ErrorResponse errorResponse);
    }
}