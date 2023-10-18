using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;


public class ServiceMetadataRepository : BaseRepository<Service>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbContext"></param>
    public ServiceMetadataRepository(CoreDBContext dbContext) : base(dbContext)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Service? GetServiceMetadata()
    {
        return dbContext.Services.FirstOrDefault();
    }

}
