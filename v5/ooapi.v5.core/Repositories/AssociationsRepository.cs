using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

/// <summary>
/// 
/// </summary>
public class AssociationsRepository : BaseRepository<Association>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbContext"></param>
    public AssociationsRepository(CoreDBContext dbContext) : base(dbContext)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="associationId"></param>
    /// <returns></returns>
    public Association? GetAssociation(Guid associationId)
    {
        return dbContext.Associations.FirstOrDefault(x => x.AssociationId.Equals(associationId));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="personId"></param>
    /// <returns></returns>
    public List<Association> GetAssociationsByPersonId(Guid personId)
    {
        return dbContext.Associations.Where(o => o.PersonId.Equals(personId)).ToList();
    }
}
