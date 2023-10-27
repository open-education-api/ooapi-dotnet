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

    public async Task<Association?> GetAsync(Guid associationId, CancellationToken cancellationToken = default)
    {
        return await _repository.GetAssociationAsync(associationId, cancellationToken);
    }

    public async Task<Pagination<Association>> GetAssociationsByPersonIdAsync(DataRequestParameters dataRequestParameters, Guid personId, CancellationToken cancellationToken = default)
    {
        return await _repository.GetAssociationsByPersonIdAsync(personId, dataRequestParameters, cancellationToken);
    }
}