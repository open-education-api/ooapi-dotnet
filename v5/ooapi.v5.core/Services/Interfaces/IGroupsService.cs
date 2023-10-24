using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

public interface IGroupsService
{
    Group? Get(Guid groupId);
    Pagination<Group> GetAll(DataRequestParameters dataRequestParameters);
    Pagination<Group> GetGroupsByOrganizationId(DataRequestParameters dataRequestParameters, Guid organizationId);
    Pagination<Group> GetGroupsByPersonId(DataRequestParameters dataRequestParameters, Guid personId);
}