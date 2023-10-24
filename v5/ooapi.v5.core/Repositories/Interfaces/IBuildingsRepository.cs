using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IBuildingsRepository
{
    Building? GetBuilding(Guid buildingId);
    Pagination<Building> GetAllOrderedBy(DataRequestParameters dataRequestParameters);
}