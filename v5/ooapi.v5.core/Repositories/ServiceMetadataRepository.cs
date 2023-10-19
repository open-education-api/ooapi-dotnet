using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class ServiceMetadataRepository : BaseRepository<Service>, IServiceMetadataRepository
{
    public ServiceMetadataRepository(ICoreDbContext dbContext) : base(dbContext)
    {
    }

    public Service GetServiceMetadata()
    {
        return dbContext.Services.First();
    }
}