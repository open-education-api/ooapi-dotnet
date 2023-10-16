using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces
{
    public interface IPersonsService
    {
        Person Get(Guid personId, out ErrorResponse errorResponse);
        Pagination<Person> GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse errorResponse);
        Pagination<Person> GetPersonsByGroupId(DataRequestParameters dataRequestParameters, Guid groupId, out ErrorResponse errorResponse);
    }
}