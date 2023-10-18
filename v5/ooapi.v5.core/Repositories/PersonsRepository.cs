using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;


public class PersonsRepository : BaseRepository<Person>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbContext"></param>
    public PersonsRepository(CoreDBContext dbContext) : base(dbContext)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="personId"></param>
    /// <returns></returns>
    public Person? GetPerson(Guid personId)
    {
        return dbContext.Persons.FirstOrDefault(x => x.PersonId.Equals(personId));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="groupId"></param>
    /// <returns></returns>
    public List<Person> GetPersonsByGroupId(Guid groupId)
    {
        return new List<Person>();
    }
}
