using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class PersonsRepository : BaseRepository<Person>
{
    public PersonsRepository(CoreDBContext dbContext) : base(dbContext)
    {
        //
    }

    public Person GetPerson(Guid personId)
    {
        return dbContext.Persons.FirstOrDefault(x => x.PersonId.Equals(personId));
    }

}
