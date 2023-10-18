using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;


/// <typeparam name="T"></typeparam>
public class BaseRepository<T> where T : class
{

    protected readonly CoreDBContext dbContext;


    /// <param name="dbContext"></param>
    public BaseRepository(CoreDBContext dbContext)
    {
        this.dbContext = dbContext;
    }


    /// <param name="dataRequestParameters"></param>
    /// <param name="set"></param>
    /// <returns></returns>
    public virtual Pagination<T> GetAllOrderedBy(DataRequestParameters dataRequestParameters, IQueryable<T>? set = null)
    {
        // TODO: add logic for OrderBy related attributes. --> for example 'name' (a 'LanguageTypedString') from the related table 'Attributes'

        set ??= dbContext.Set<T>().AsQueryable();

        if (!string.IsNullOrEmpty(dataRequestParameters.PrimaryCodeSearch)) 
        { 
            dataRequestParameters.Filters.Add("primaryCode", dataRequestParameters.PrimaryCodeSearch); 
        }

        var searchedSet = !string.IsNullOrWhiteSpace(dataRequestParameters.SearchTerm) ? OrderedQueryable.SearchBy<T>(set, dataRequestParameters.SearchTerm) : set;
        var filteredSet = (dataRequestParameters.Filters != null && dataRequestParameters.Filters.Count > 0) ? OrderedQueryable.FilterBy<T>(searchedSet, dataRequestParameters.Filters) : searchedSet;

        return new Pagination<T>(filteredSet, dataRequestParameters);
    }
}