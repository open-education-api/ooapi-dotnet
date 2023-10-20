using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

internal class AssociationsService : ServiceBase, IAssociationsService
{
    private readonly IAssociationsRepository _repository;

    public AssociationsService(ICoreDbContext dbContext, IAssociationsRepository repository, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = repository;
    }

    public Association? Get(Guid associationId)
    {
        return _repository.GetAssociation(associationId);
    }

    public Pagination<Association> GetAssociationsByPersonId(DataRequestParameters dataRequestParameters, Guid personId)
    {
        var result = _repository.GetAssociationsByPersonId(personId);
        return new Pagination<Association>(result.AsQueryable(), dataRequestParameters);
    }
}