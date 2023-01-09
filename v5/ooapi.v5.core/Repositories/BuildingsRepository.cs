using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class BuildingsRepository : BaseRepository<Building>
{
    public BuildingsRepository(CoreDBContext dbContext) : base(dbContext)
    {
        //
    }

    public Building GetBuilding(Guid buildingId)
    {
        return dbContext.Buildings.FirstOrDefault(x => x.BuildingId.Equals(buildingId));
    }

    internal Pagination<Building> GetAllBuildingOrderedBy(DataRequestParameters dataRequestParameters)
    {

        IQueryable<Building> buildingSet = dbContext.Set<Building>().Include(x => x.Address).AsQueryable();
        var searchedSet = !String.IsNullOrWhiteSpace(dataRequestParameters.SearchTerm) ? OrderedQueryable.SearchBy<Building>(buildingSet, dataRequestParameters.SearchTerm) : buildingSet;
        var filteredSet = (dataRequestParameters.Filters != null && dataRequestParameters.Filters.Count > 0) ? OrderedQueryable.FilterBy<Building>(searchedSet, dataRequestParameters.Filters) : searchedSet;
        var orderedSet = (dataRequestParameters.Sort != null) ? OrderedQueryable.OrderBy<Building>(filteredSet, dataRequestParameters.Sort) : filteredSet;

        return new Pagination<Building>(orderedSet, dataRequestParameters ?? new DataRequestParameters());

    }
}
