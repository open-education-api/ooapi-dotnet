using ooapi.v5.core.Repositories;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

public class BuildingsService : ServiceBase, IBuildingsService
{
    private readonly BuildingsRepository _repository;

    public BuildingsService(CoreDBContext dbContext, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = new BuildingsRepository(dbContext);
    }

    public Pagination<Building> GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse errorResponse)
    {
        try
        {
            Pagination<Building> result = _repository.GetAllOrderedBy(dataRequestParameters);

            errorResponse = null;
            return result;
        }
        catch (Exception ex)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }

    public Building Get(Guid buildingId, out ErrorResponse errorResponse)
    {
        try
        {
            var item = _repository.GetBuilding(buildingId);

            errorResponse = null;
            return item;
        }
        catch (Exception ex)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }

    }

}
