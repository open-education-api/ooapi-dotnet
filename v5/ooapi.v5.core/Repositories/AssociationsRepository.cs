using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class AssociationsRepository : BaseRepository<Association>
{
    public AssociationsRepository(CoreDBContext dbContext) : base(dbContext)
    {
        //
    }

    public Association GetAssociation(Guid associationId)
    {
        return dbContext.Associations.FirstOrDefault(x => x.AssociationId.Equals(associationId));
    }

}
