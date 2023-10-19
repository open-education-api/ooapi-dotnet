﻿using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class PersonsRepository : BaseRepository<Person>, IPersonsRepository
{
    public PersonsRepository(ICoreDbContext dbContext) : base(dbContext)
    {
    }

    public Person? GetPerson(Guid personId)
    {
        return dbContext.Persons.FirstOrDefault(x => x.PersonId.Equals(personId));
    }

    public List<Person> GetPersonsByGroupId(Guid groupId)
    {
        throw new NotImplementedException();
    }
}