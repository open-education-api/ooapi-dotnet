using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

internal class ServiceMetadataService : ServiceBase, IServiceMetadataService
{
    private readonly IServiceMetadataRepository _repository;

    public ServiceMetadataService(ICoreDbContext dbContext, IServiceMetadataRepository repository, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = repository;
    }

    public async Task<Service> GetAsync(CancellationToken cancellationToken = default)
    {
        return await _repository.GetServiceMetadataAsync(cancellationToken);
    }
}