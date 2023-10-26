using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class PersonsRepository : BaseRepository<Person>, IPersonsRepository
{
    public PersonsRepository(ICoreDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Person?> GetPersonAsync(Guid personId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Persons.FirstOrDefaultAsync(x => x.PersonId.Equals(personId), cancellationToken);
    }

    public async Task<List<Person>> GetPersonsByGroupIdAsync(Guid groupId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}