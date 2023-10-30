using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class BaseRepository<T> where T : class
{
    protected readonly ICoreDbContext dbContext;

    public BaseRepository(ICoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public virtual async Task<Pagination<T>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, IQueryable<T>? set = null, CancellationToken cancellationToken = default)
    {
        // TODO: add logic for OrderBy related attributes. --> for example 'name' (a 'LanguageTypedString') from the related table 'Attributes'

        set ??= dbContext.Set<T>().AsQueryable();

        if (!string.IsNullOrEmpty(dataRequestParameters.PrimaryCodeSearch)) 
        { 
            dataRequestParameters.Filters.Add("primaryCode", dataRequestParameters.PrimaryCodeSearch); 
        }

        var searchedSet = !string.IsNullOrWhiteSpace(dataRequestParameters.SearchTerm) ? OrderedQueryable.SearchBy<T>(set, dataRequestParameters.SearchTerm) : set;
        var filteredSet = (dataRequestParameters.Filters != null && dataRequestParameters.Filters.Count > 0) ? OrderedQueryable.FilterBy<T>(searchedSet, dataRequestParameters.Filters) : searchedSet;

        return await Pagination<T>.CreateAsync(filteredSet, dataRequestParameters);
    }
}