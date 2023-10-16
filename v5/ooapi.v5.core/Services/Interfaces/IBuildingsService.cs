using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces
{
    public interface IBuildingsService
    {
        Building Get(Guid buildingId, out ErrorResponse errorResponse);
        Pagination<Building> GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse errorResponse);
    }
}