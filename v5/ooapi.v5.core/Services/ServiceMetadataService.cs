using ooapi.v5.core.Repositories;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

internal class ServiceMetadataService : ServiceBase, IServiceMetadataService
{
    private readonly ServiceMetadataRepository _repository;

    public ServiceMetadataService(CoreDBContext dbContext, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = new ServiceMetadataRepository(dbContext);
    }

    public Service Get()
    {
        return _repository.GetServiceMetadata();
    }
}
