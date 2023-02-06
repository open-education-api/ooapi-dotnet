using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;
public class BaseRepository<T> where T : class
{
    protected readonly CoreDBContext dbContext;

    public BaseRepository(CoreDBContext dbContext)

    {
        this.dbContext = dbContext;
    }

    public virtual Pagination<T> GetAllOrderedBy(DataRequestParameters dataRequestParameters, IQueryable<T> set = null)
    {

        set = set ?? dbContext.Set<T>().AsQueryable();

        //var searchedSet = !String.IsNullOrWhiteSpace(dataRequestParameters.PrimaryCodeSearch) ? OrderedQueryable.SearchByPrimaryCode<T>(set, dataRequestParameters.PrimaryCodeSearch) : set;
        var searchedSet = !String.IsNullOrWhiteSpace(dataRequestParameters.SearchTerm) ? OrderedQueryable.SearchBy<T>(set, dataRequestParameters.SearchTerm) : set;
        var filteredSet = (dataRequestParameters.Filters != null && dataRequestParameters.Filters.Count > 0) ? OrderedQueryable.FilterBy<T>(searchedSet, dataRequestParameters.Filters) : searchedSet;
    //    var orderedSet = (dataRequestParameters.Sort != null) ? OrderedQueryable.OrderBy<T>(filteredSet, dataRequestParameters.Sort) : filteredSet;

        return new Pagination<T>(filteredSet, dataRequestParameters ?? new DataRequestParameters());

    }



}