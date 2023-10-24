using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

public interface IBuildingsService
{
    Building? Get(Guid buildingId);
    Task<Pagination<Building>> GetAll(DataRequestParameters dataRequestParameters);
}