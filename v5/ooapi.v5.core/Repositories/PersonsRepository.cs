using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class PersonsRepository : BaseRepository<Person>
{
    public PersonsRepository(ICoreDbContext dbContext) : base(dbContext)
    {
        //
    }

    public Person GetPerson(Guid personId)
    {
        return dbContext.Persons.FirstOrDefault(x => x.PersonId.Equals(personId));
    }

    public List<Person> GetPersonsByGroupId(Guid groupId)
    {
        throw new NotImplementedException();
        //TODO return dbContext.Persons.Where(o => o.GroupId.Equals(groupId)).ToList();
    }
}