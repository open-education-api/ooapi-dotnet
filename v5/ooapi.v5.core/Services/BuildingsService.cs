using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

internal class BuildingsService : ServiceBase, IBuildingsService
{
    private readonly IBuildingsRepository _repository;

    public BuildingsService(ICoreDbContext dbContext, IBuildingsRepository repository, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = repository;
    }

    public Pagination<Building> GetAll(DataRequestParameters dataRequestParameters)
    {
        return _repository.GetAllOrderedBy(dataRequestParameters);
    }

    public Building? Get(Guid buildingId)
    {
        return _repository.GetBuilding(buildingId);
    }
}