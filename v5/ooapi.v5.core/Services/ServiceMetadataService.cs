using ooapi.v5.core.Repositories;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;


public class ServiceMetadataService : ServiceBase, IServiceMetadataService
{
    private readonly ServiceMetadataRepository _repository;

    public ServiceMetadataService(CoreDBContext dbContext, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = new ServiceMetadataRepository(dbContext);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    public Service? Get(out ErrorResponse? errorResponse)
    {
        try
        {
            var item = _repository.GetServiceMetadata();

            errorResponse = null;
            return item;
        }
        catch (Exception)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }

}
