using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class ServiceMetadataRepository : BaseRepository<Service>
{
    public ServiceMetadataRepository(ICoreDbContext dbContext) : base(dbContext)
    {
        //
    }

    public Service GetServiceMetadata()
    {
        return dbContext.Services.FirstOrDefault();
    }

}