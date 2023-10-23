using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IPersonsRepository
{
    Person? GetPerson(Guid personId);
    List<Person> GetPersonsByGroupId(Guid groupId);
    Pagination<Person> GetAllOrderedBy(DataRequestParameters dataRequestParameters, IQueryable<Person>? set = null);
}